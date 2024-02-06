using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasmoVisuals : MonoBehaviour
{
    private const string IS_WALKING = "IsWalking";

    [Header("Values")]
    [SerializeField] private float _glowColorChangeSpeed;

    [Header("General References")]
    [SerializeField] private PlasmoMoveTest _player;
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private Animator _animator;

    [Header("Object References")]
    [SerializeField] private List<MeshRenderer> _glowingParts = new List<MeshRenderer>();
    [SerializeField] private Transform _lineRendererStart, _lineRendererEnd;
    [SerializeField] private GameObject _eyeNeutral, _eyeHappy, _eyeAngry;

    private Material _glowingMaterial;
    private Color _glowColorCurrent;
    private Color _glowColorEnd;

    public enum PlasmoExpression
    {
        Neutral,
        Happy,
        Angry,
    }

    private void Start()
    {
        if(_glowingParts.Count != 0)
        {
            _glowingMaterial = new Material(_glowingParts[0].material);
            _glowingMaterial.EnableKeyword("_EMISSION");

            _glowColorCurrent = _glowingMaterial.GetColor("_EmissionColor");

            foreach (MeshRenderer mesh in _glowingParts)
            {
                mesh.material = _glowingMaterial;
            }
        }
    }

    private void Update()
    {
        HandleLineRenderer();
        HandleGlowColor();
        //HandleAnimations();
    }

    private void HandleAnimations()
    {
        _animator.SetBool(IS_WALKING, _player.IsWalking());
    }

    private void HandleGlowColor()
    {
        _glowColorCurrent = Color.Lerp(_glowColorCurrent, _glowColorEnd, _glowColorChangeSpeed * Time.deltaTime);

        _glowingMaterial.SetColor("_EmissionColor", _glowColorCurrent * Mathf.LinearToGammaSpace(2f));
    }

    private void HandleLineRenderer()
    {
        _lineRenderer.SetPosition(0, _lineRendererStart.position);
        _lineRenderer.SetPosition(1, _lineRendererEnd.position);
    }

    public void SetExpression(PlasmoExpression newExpression)
    {
        _eyeNeutral.SetActive(false);
        _eyeAngry.SetActive(false);
        _eyeHappy.SetActive(false);

        switch (newExpression)
        {
            case PlasmoExpression.Neutral:
                _eyeNeutral.SetActive(true);
                break;
            case PlasmoExpression.Angry:
                _eyeAngry.SetActive(true);
                break;
            case PlasmoExpression.Happy:
                _eyeHappy.SetActive(true);
                break;
        }
    }

    public void SetGlowColor(Color newColor)
    {
        _glowColorEnd = newColor;
    }

    [ContextMenu("Become Red")]
    private void BecomeRed()
    {
        SetGlowColor(Color.red);
        Debug.Log("Setting color to red");
    }

    [ContextMenu("Become White")]
    private void BecomeWhite()
    {
        SetGlowColor(Color.white);
        Debug.Log("Setting color to white");
    }
}