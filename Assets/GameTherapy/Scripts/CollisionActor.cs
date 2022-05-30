using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollisionActor : MonoBehaviour
{
    public LayerMask _LayerMask;
    
    public UnityEvent OnEnter;
    public UnityEvent OnExit;

    private void OnTriggerEnter(Collider collision)
    {
        //Debug.Log("OnCollisionEnter");
        if (( _LayerMask & (1 << collision.gameObject.layer)) != 0)
        {
            OnEnter?.Invoke();
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        //Debug.Log("OnCollisionExit");
        if (( _LayerMask & (1 << collision.gameObject.layer)) != 0)
        {
            OnExit?.Invoke();
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (( _LayerMask & (1 << collision.gameObject.layer)) != 0)
        {
            OnEnter?.Invoke();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (( _LayerMask & (1 << collision.gameObject.layer)) != 0)
        {
            OnExit?.Invoke();
        }
    }
}
