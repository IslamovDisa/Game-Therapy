using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISelectable
{
    void Select(int buttonIndex);
    void Deselect();
}
