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
    [SerializeField] private SelectManager _selectManager;

    private const int MaxDistance = 1000;
    
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            var ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit, MaxDistance, _groundLayerMask))
            {
                if (_selectManager.SelectedCharacter != null)
                {
                    _selectManager.SelectedCharacter.transform.position = hit.point;
                }
            }
        }
        
        if (Input.GetMouseButton(1))
        {
            var ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit, MaxDistance, _characterLayerMask))
            {
                var selectCharacterOnScene = hit.collider.gameObject.GetComponent<SelectCharacterOnScene>();

                if (_selectManager.SelectedCharacter != selectCharacterOnScene)
                {
                    if (_selectManager.SelectedCharacter != null)
                    {
                        _selectManager.SelectedCharacter.Deselect();
                    }

                    selectCharacterOnScene.Select(2);
                    _selectManager.SelectedCharacter = selectCharacterOnScene;
                }
            }
            else
            {
                if (_selectManager.SelectedCharacter != null)
                {
                    _selectManager.SelectedCharacter.Deselect();
                    _selectManager.SelectedCharacter = null;
                }
            }
        }
    }
}
