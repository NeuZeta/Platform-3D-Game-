using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IEnemyState
{
    zombieAI myEnemy;
 


    public PatrolState(zombieAI enemy)
    {
        myEnemy = enemy;
    }

    public void UpdateState()
    {
        if (myEnemy)
        {
            myEnemy.navMeshAgent.destination = myEnemy.waypoints[myEnemy.nextWaypoint].position;

            if (myEnemy.navMeshAgent.remainingDistance <= myEnemy.navMeshAgent.stoppingDistance)
            {
                myEnemy.SetNextRandomWaypoint();
            }
        }
    }

    public void GoToPatrolState() { }

    public void GoToAttackState()
    {  
        myEnemy.navMeshAgent.isStopped = true;
        myEnemy.navMeshAgent.speed = 4f;
        myEnemy.currentState = myEnemy.attackState; ;
    }

    public void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            GoToAttackState();
        }
    }

    public void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            GoToAttackState();
        }
    }

    public void OnTriggerExit(Collider col)
    {
    }

}//PatrolState

