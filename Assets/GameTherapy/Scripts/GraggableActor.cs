using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraggableActor : MonoBehaviour
{
    private float _dist;
    private bool _dragging = false;
    private Vector3 _offset;
    private Transform _toDrag;
 
    // Update is called once per frame
    private void Update()
    {
        Vector3 v3;
        if (Input.GetMouseButtonDown(2))
        {
            _dragging = false;
            return;
        }
 
        //var touch = Input.touches[0];
        var pos = Input.mousePosition;
        if (Input.GetMouseButton(2))
        {
            var ray = Camera.main.ScreenPointToRay(pos);

            if (Physics.Raycast(ray, out var hit))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    _toDrag = hit.transform;
                    _dist = hit.transform.position.z - Camera.main.transform.position.z;
                    
                    v3 = new Vector3(pos.x, pos.y, _dist);
                    v3 = Camera.main.ScreenToWorldPoint(v3);
                    _offset = _toDrag.position - v3;
                    _dragging = true;
                }
            }
        }
 
        if (_dragging && Input.GetMouseButton(2))
        {
            v3 = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _dist);
            v3 = Camera.main.ScreenToWorldPoint(v3);
            _toDrag.position = v3 + _offset;
        }
 
        if (_dragging && Input.GetMouseButtonUp(2))
        {
            _dragging = false;
        }
    }
}
