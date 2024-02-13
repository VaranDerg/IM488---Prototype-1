using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupCoinVisual : MonoBehaviour
{
    [Header("Values")]
    [SerializeField] private float _rotateSpeed;

    [Header("Visuals")]
    [SerializeField] private MeshRenderer _faceMesh;
    [SerializeField] private Material _faceMat;
    [SerializeField] private ParticleSystem _particles;

    /// <summary>
    /// Rotates the Coin
    /// </summary>
    private void Update()
    {
        transform.Rotate(Vector3.up * _rotateSpeed * Time.deltaTime);
    }

    /// <summary>
    /// Prepares the Coin's Visual
    /// </summary>
    /// <param name="pickupData">The Pickup data to be reflected in the visual</param>
    public void PreparePickupCoinVisual(PickupDataSO pickupData)
    {
        Material coloredMat = new Material(_faceMat);
        coloredMat.color = pickupData.PickupColor;
        _faceMesh.material = coloredMat;

        ParticleSystem.MainModule main = _particles.main;
        main.startColor = pickupData.PickupColor;
    }
}
