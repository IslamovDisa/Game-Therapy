using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCharacterOnScene : MonoBehaviour, ISelectable
{
    [SerializeField] private GameObject _selectedQuad;
    
    public void Select(int buttonIndex)
    {
        _selectedQuad.SetActive(true);
    }

    public void Deselect()
    {
        _selectedQuad.SetActive(false);
    }
}
