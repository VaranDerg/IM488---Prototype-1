using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Author: Liz
/// Description: Rotates an object
/// </summary>
public class TestObjectRotate : MonoBehaviour
{
    [SerializeField] private float _rotateSpeed;

    /// <summary>
    /// Rotation
    /// </summary>
    private void Update()
    {
        transform.Rotate(Vector3.forward * _rotateSpeed * Time.deltaTime, Space.Self);
    }
}
