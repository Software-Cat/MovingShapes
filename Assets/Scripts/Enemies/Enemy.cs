using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : MonoBehaviour
{
    protected Vector3 goal;
    private NavMeshAgent agent;

    protected virtual void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    protected virtual void Update()
    {
        agent.SetDestination(goal);
    }
}
