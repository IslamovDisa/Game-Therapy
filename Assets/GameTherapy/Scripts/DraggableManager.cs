using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableManager : MonoBehaviour
{
    private IDraggable _currentDraggable;

    public IDraggable CurrentDraggable
    {
        get => _currentDraggable;
        set
        {
            if (_currentDraggable == value)
            {
                return;
            }

            if (_currentDraggable != null)
            {
                _currentDraggable.IsDragging = false;
            }

            _currentDraggable = value;
            if (_currentDraggable != null)
            {
                _currentDraggable.IsDragging = true;
            }
        }
    }
}
