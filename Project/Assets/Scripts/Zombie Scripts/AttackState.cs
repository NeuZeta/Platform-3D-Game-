using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IEnemyState
{

    zombieAI myEnemy;
    float actualTimeBetweenAttacks = 0f;
    Vector3 playerPosition;
    float distanceToPlayer;

    public AttackState(zombieAI enemy)
    {
        myEnemy = enemy;
        
    }

    public void UpdateState()
    {
        actualTimeBetweenAttacks += Time.deltaTime;
    }

    public void GoToPatrolState()
    {
        myEnemy.anim.SetBool("Attack", false);
        myEnemy.navMeshAgent.speed = 1f;
        myEnemy.navMeshAgent.isStopped = false;
        myEnemy.currentState = myEnemy.patrolState;
    }

    public void GoToAttackState() { }

    public void OnTriggerEnter(Collider col) { }

    public void OnTriggerStay(Collider col)
    {
        if (col.tag == "Player")
        {
            playerPosition = col.transform.position;
            Vector3 lookInDirection = col.transform.position - myEnemy.transform.position;

            //rotando solamente en el eje Y
            myEnemy.transform.rotation = Quaternion.FromToRotation(Vector3.forward, new Vector3(lookInDirection.x, 0, lookInDirection.z));

            distanceToPlayer = Vector3.Distance(playerPosition, myEnemy.transform.position);

            if (distanceToPlayer > 1.8f)
            {
                myEnemy.anim.SetBool("Attack", false);
                myEnemy.navMeshAgent.isStopped = false;
                myEnemy.navMeshAgent.destination = playerPosition;

            }
            else
            {
                myEnemy.navMeshAgent.isStopped = true;

                Attack(col);
            }
        } else if (col.tag == "Zombified")
        {
            GoToPatrolState();
        }

    }

    public void OnTriggerExit(Collider col)
    {
        GoToPatrolState();
    }


    void Attack(Collider player)
    {
        myEnemy.anim.SetInteger("AttackIndex", Random.Range(0, 2));
        myEnemy.anim.SetBool("Attack", true);

        //Tiempo de atacar
        if (actualTimeBetweenAttacks > myEnemy.timeBetweenAttacks)
        {

            actualTimeBetweenAttacks = 0f;

            player.gameObject.GetComponent<ArmorScript>().Attack(myEnemy.damageAttack);

            myEnemy.audioSource.PlayOneShot(myEnemy.zombieSounds[Random.Range(0, myEnemy.zombieSounds.Length)]);

        }

    }



}
