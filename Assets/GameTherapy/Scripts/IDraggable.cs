using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDraggable 
{
    public bool IsDragging { get; set; }
    public void Drag(Vector3 pos);
}
