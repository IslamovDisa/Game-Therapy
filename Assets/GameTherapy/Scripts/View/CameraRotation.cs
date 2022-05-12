using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Transform _transform;

    public void Update()
    {
        transform.Rotate(0, _speed * Time.deltaTime, 0);
    }
}
