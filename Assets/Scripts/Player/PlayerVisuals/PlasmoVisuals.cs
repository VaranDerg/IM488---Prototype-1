using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasmoVisuals : MonoBehaviour
{
    private const string IS_WALKING = "IsWalking";

    [SerializeField] private PlasmoMoveTest _player;
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private Transform _lineRendererStart, _lineRendererEnd;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        HandleLineRenderer();
        //_animator.SetBool(IS_WALKING, _player.IsWalking());
    }

    private void HandleLineRenderer()
    {
        _lineRenderer.SetPosition(0, _lineRendererStart.position);
        _lineRenderer.SetPosition(1, _lineRendererEnd.position);
    }
}