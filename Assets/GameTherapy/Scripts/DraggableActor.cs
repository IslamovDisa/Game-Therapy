using UnityEngine;
using UnityEngine.AI;

public class DraggableActor : MonoBehaviour, IDraggable
{
    private bool _isDragging;
    public bool IsDragging
    {
        get => _isDragging;
        set
        {
            _isDragging = value;
            _agent.isStopped = _isDragging;
            _agent.ResetPath();
        }
    }

    [SerializeField] private NavMeshAgent _agent;
    
    public void Drag(Vector3 pos)
    {
        transform.position = pos;
    }
}
