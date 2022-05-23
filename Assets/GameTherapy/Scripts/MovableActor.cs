using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovableActor : MonoBehaviour, IMovable
{
    [SerializeField] private NavMeshAgent _agent;
    
    public void MoveTo(Vector3 position)
    {
        _agent.SetDestination(position);
    }
}
