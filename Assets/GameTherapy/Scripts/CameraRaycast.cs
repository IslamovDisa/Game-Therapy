using System;
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
    [SerializeField] private DraggableManager _draggableManager;
    [SerializeField] private SpawnManager _spawnManager;
    
    private const int MaxDistance = 1000;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _spawnManager.CurrentSpawnObject = null;
        }
        
        if (Input.GetMouseButtonUp(2))
        {
            _draggableManager.CurrentDraggable = null;
        }
    }

    private void FixedUpdate()
    {
        if (_spawnManager.CurrentSpawnObject != null)
        {
            var ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit, MaxDistance, _groundLayerMask))
            {
                _spawnManager.CurrentSpawnObject.Drag(hit.point);
            }
            
            return;
        }

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
            if (Physics.Raycast(ray, out var hit, MaxDistance))
            {
                var selectableActor = hit.collider.gameObject.GetComponent<SelectableActor>();
                _selectManager.CurrentSelection = selectableActor;
            }
        }
        
        // TODO Draggble manager as selectable
        if (Input.GetMouseButtonDown(2))
        {
            var ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit, MaxDistance, _characterLayerMask))
            {
                var draggableActor = hit.collider.gameObject.GetComponent<DraggableActor>();
                _draggableManager.CurrentDraggable = draggableActor;
            }
        }

        if (Input.GetMouseButton(2))
        {
            var ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit, MaxDistance, _groundLayerMask))
            {
                _draggableManager.CurrentDraggable?.Drag(hit.point);
            }
        }
    }
}
