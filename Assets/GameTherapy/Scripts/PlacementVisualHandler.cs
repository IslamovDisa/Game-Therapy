using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementVisualHandler : MonoBehaviour
{
    [SerializeField] private Renderer _renderer;
    [SerializeField] private Color _defaultColor = Color.white;
    [SerializeField] private Color _prohibitoryColor = Color.red;

    public void SetDefaultColor()
    {
        _renderer.material.color = _defaultColor;
    }
    
    public void SetProhibitoryColor()
    {
        _renderer.material.color = _prohibitoryColor;
    }
}
