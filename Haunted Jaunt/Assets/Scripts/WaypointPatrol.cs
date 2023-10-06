using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaypointPatrol : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public Transform[] waypoints; //array to set waypoints
    int m_CurrentWaypointIndex; //current point of index

    // Start is called before the first frame update
    void Start()
    {
        //set destination
        navMeshAgent.SetDestination(waypoints[0].position);
    }

    // Update is called once per frame
    void Update()
    {
        if(navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance)
        {
            //update current point of index
            m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % waypoints.Length;

            //sets new destination index
            navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
        }
    }
}
