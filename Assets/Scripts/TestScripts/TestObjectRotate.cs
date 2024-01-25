using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestObjectRotate : MonoBehaviour
{
    [SerializeField] private float _rotateSpeed;

    private void Update()
    {
        transform.Rotate(Vector3.forward * _rotateSpeed * Time.deltaTime, Space.Self);
    }
}
