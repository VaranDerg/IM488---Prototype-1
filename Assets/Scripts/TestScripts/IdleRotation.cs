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
    [SerializeField] bool ignoreParentRotation = true;

    private Vector3 prevRotation = Vector3.zero;

    private void Awake()
    {
        prevRotation = transform.rotation.eulerAngles;
    }

    /// <summary>
    /// Rotation
    /// </summary>
    private void Update()
    {
        if (ignoreParentRotation)
        {
            transform.rotation = Quaternion.Euler(prevRotation);
            transform.Rotate((_rotateAngleNormalized * _rotateSpeed) * Time.deltaTime, Space.Self);
            prevRotation = transform.rotation.eulerAngles;
        }
        else
            transform.Rotate(_rotateAngleNormalized * _rotateSpeed * Time.deltaTime, Space.Self);
    }
}