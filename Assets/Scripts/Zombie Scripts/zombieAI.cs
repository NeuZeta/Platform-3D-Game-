using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class zombieAI : MonoBehaviour {

    [HideInInspector] public PatrolState patrolState;
    [HideInInspector] public AttackState attackState;
    [HideInInspector] public IEnemyState currentState;
    [HideInInspector] public NavMeshAgent navMeshAgent;

    public Transform[] waypoints;
    public int nextWaypoint = 0;
    public float timeBetweenAttacks = 2.0f;
    public int damageAttack = 1;
    public Animator anim;
    public AudioSource audioSource;
    public AudioClip[] zombieSounds;

    private float clipLength = 0;
    

    private void Awake()
    {
        //guardamos la referencia de nuestro NavMesh Agent
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
    void Start()
    {
        //creamos los estados de nuestra IA
        patrolState = new PatrolState(this);
        attackState = new AttackState(this);

        SetNextRandomWaypoint();

        //le decimos que inicialmente empezará patrullando
        currentState = patrolState;
        ZombieSoundsDependingOnState();
    }

    // Update is called once per frame
    void Update () {

        var speed = navMeshAgent.velocity.magnitude;
        anim.SetFloat("Speed", speed);
        currentState.UpdateState();
	}

    void ZombieSoundsDependingOnState()
    {
        if (currentState == patrolState)
        {
            InvokeRepeating("PickZombieSound", 1, 300 * Time.deltaTime);
        }
    }

    void PickZombieSound()
    {
        var soundIndex = Random.Range(0, zombieSounds.Length);

        AudioSource.PlayClipAtPoint(zombieSounds[soundIndex], this.transform.position);

        //audioSource.PlayOneShot(zombieSounds[soundIndex]);
        clipLength = zombieSounds[soundIndex].length;
    }


    //Ya que nuestros states no heredan de Monobehaviour, tendremos que avisarles cuando algo entra, está o sale de nuestro trigger
    private void OnTriggerEnter(Collider other)
    {
        currentState.OnTriggerEnter(other);
    }

    private void OnTriggerStay(Collider other)
    {
        currentState.OnTriggerStay(other);
    }

    private void OnTriggerExit(Collider other)
    {
        currentState.OnTriggerExit(other);
    }

    public void SetNextRandomWaypoint()
    {
        //Decidimos un nextWaypoint aleatorio
        nextWaypoint = Random.Range(0, waypoints.Length);
    }


}//ZombieAI
