using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalColor : MonoBehaviour
{
    [SerializeField] private Color _portalColor;
    [SerializeField] private Material _portalMaterial;
    [SerializeField] private float _glowIntensity = 1;

    private List<MeshRenderer> _renderers = new List<MeshRenderer>();
    private Material _portalMatInstance;

    private void Start()
    {
        _portalMatInstance = new Material(_portalMaterial);
        foreach (MeshRenderer mr in GetComponentsInChildren<MeshRenderer>())
        {
            _renderers.Add(mr);
            mr.material = _portalMatInstance;
        }

        ApplyPortalColor();
    }

    private void ApplyPortalColor()
    {
        foreach (MeshRenderer mr in _renderers)
        {
            _portalMatInstance.SetColor("_EmissionColor", _portalColor * _glowIntensity);
        }
    }
}
