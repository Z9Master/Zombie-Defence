using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyControl : MonoBehaviour
{
    public NavMeshAgent agent;

    public GameObject targetPlayer;
    public GameObject targetTuret;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveToPlayer();
    }

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
}
