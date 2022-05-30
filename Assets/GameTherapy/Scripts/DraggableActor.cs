using System;
using UnityEngine;
using UnityEngine.AI;

public class DraggableActor : MonoBehaviour, IDraggable
{
    private bool _isDragging;
    
    [SerializeField] private string dragLayerName = "Drag";
    private int _defaultLayer;

    private void Awake()
    {
        _defaultLayer = gameObject.layer;
    }

    public bool IsDragging
    {
        get => _isDragging;
        set
        {
            _isDragging = value;

            gameObject.layer = _isDragging ? 
                               LayerMask.NameToLayer(dragLayerName) : 
                               _defaultLayer;

            if (_agent == null)
            {
                return;
            }

            if (_agent.enabled)
            {
                _agent.isStopped = _isDragging;
                _agent.ResetPath();
            }

            _agent.enabled = !_isDragging;
        }
    }

    [SerializeField] private NavMeshAgent _agent;
    
    public void Drag(Vector3 pos)
    {
        transform.position = pos;
    }
}
