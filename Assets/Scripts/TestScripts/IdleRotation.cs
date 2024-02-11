using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Author: Liz
/// Description: Rotates an object
/// </summary>
public class IdleRotation : MonoBehaviour
{
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private Vector3 _rotateAngleNormalized = Vector3.forward;

    /// <summary>
    /// Rotation
    /// </summary>
    private void Update()
    {
        transform.Rotate(_rotateAngleNormalized * _rotateSpeed * Time.deltaTime, Space.Self);
    }
}