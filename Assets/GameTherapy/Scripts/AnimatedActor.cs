using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimatedActor : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private Animator _animator;
    
    private void Update()
    {
        _animator.SetFloat("Speed", _agent.desiredVelocity.magnitude / _agent.acceleration);
    }
}
