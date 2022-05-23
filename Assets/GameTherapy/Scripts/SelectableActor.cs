using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SelectableActor : MonoBehaviour, ISelectable
{
    public UnityEvent OnSelect;
    public UnityEvent OnDeselect;
    
    public bool IsSelect { get; private set;  }
    
    public void Select()
    {
        IsSelect = true;
        OnSelect?.Invoke();
    }

    public void Deselect()
    {
        IsSelect = false;
        OnDeselect?.Invoke();
    }
}
