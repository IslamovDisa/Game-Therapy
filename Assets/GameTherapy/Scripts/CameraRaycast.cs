using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CameraRaycast : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private LayerMask _groundLayerMask;
    [SerializeField] private LayerMask _characterLayerMask;
    [SerializeField] private Transform _debugTransform;
    
    [Space(10)]
    [SerializeField] private SelectionManager _selectManager;

    private const int MaxDistance = 1000;
    
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            var ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit, MaxDistance, _groundLayerMask))
            {
                if (_selectManager.CurrentSelection != null)
                {
                    var selectableActor = _selectManager.CurrentSelection as SelectableActor;
                    var movable = selectableActor?.GetComponent<IMovable>();
                    movable?.MoveTo(hit.point);
                }
            }
        }
        
        if (Input.GetMouseButton(1))
        {
            var ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit))
            {
                var selectableActor = hit.collider.gameObject.GetComponent<SelectableActor>();
                _selectManager.CurrentSelection = selectableActor;
            }
        }
    }
}
