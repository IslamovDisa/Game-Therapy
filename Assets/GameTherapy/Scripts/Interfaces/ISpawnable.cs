using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISpawnable
{
    void Spawn(int index);
    void Despawn(GameObject value);
}
