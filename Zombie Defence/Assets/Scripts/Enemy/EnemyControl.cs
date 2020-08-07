using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyControl : MonoBehaviour
{
    #region Variable
    public NavMeshAgent agent;

    public GameObject targetPlayer;
    public GameObject targetTuret;
    #endregion

    #region Methods
    void Start()
    {
        
    }

    void Update()
    {
        MoveToPlayer();
    }

    // Enemy will folow the player/turet, it depends on that, if the player is nearer or the turet is nearer
    private void MoveToPlayer()
    {
        if(Vector3.Distance(transform.position, targetPlayer.transform.position) < Vector3.Distance(transform.position, targetTuret.transform.position))
        {
            agent.SetDestination(targetPlayer.transform.position);
        }
        else
        {
            agent.SetDestination(targetTuret.transform.position);
        }
    }
    #endregion
}
