using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasmoVisuals : MonoBehaviour
{
    private const string IS_WALKING = "Moving";
    private const string CAST_SPELL = "CastSpell";
    private const string DASH = "Dash";
    private const string WIN = "Win";
    private const string LOSE = "Lose";
    private const string TAKE_DAMAGE = "Hurt";

    [Header("Values")]
    [SerializeField] private float _antennaeMoveSpeed;
    [SerializeField] private float _rotateSpeed;
    [Space]
    [SerializeField] private float _glowColorChangeSpeed;
    [SerializeField] private float _glowIntensity;
    [Space]
    [SerializeField] private float _dashExpressionTime = 1f;
    [SerializeField] private float _castExpressionTime = 0.5f;
    [SerializeField] private float _hurtExpressionTime = 0.25f;
    [Space]
    [SerializeField] private float _iFrameBlinkInterval;

    [Header("General References")]
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private Animator _animator;
    [SerializeField] private Material _glowMaterial;

    [Header("Object References")]
    [SerializeField] private List<MeshRenderer> _glowingParts = new List<MeshRenderer>();
    [SerializeField] private List<MeshRenderer> _iFrameBlinkParts = new List<MeshRenderer>();
    [SerializeField] private Transform _objectToRotate;
    [SerializeField] private Transform _antennae, _antennaeFollowPoint;
    [SerializeField] private Transform _lineRendererStart, _lineRendererEnd;
    [SerializeField] private GameObject _eyeNeutral, _eyeHappy, _eyeAngry, _eyeSad;

    private Material _glowMaterialInstance;
    private Color _glowColorEnd;
    private Color _glowColorCurrent;
    private Vector3 _rotationEnd;
    private float _curExpressionTime;
    private float _curIFrameTime;
    private float _curIFrameBlinkInterval;

    public enum PlasmoExpression
    {
        Neutral,
        Happy,
        Angry,
        Sad,
    }

    public enum PlasmoAnimationTrigger
    {
        Cast,
        Win,
        Lose,
        Dash,
        Hurt,
    }

    private void Start()
    {
        PrepareEmissiveMaterials();
        PrepareAntennae();

        SetGlowState(true);
        SetGlowColor(Color.white);
    }

    private void Update()
    {
        HandleRotation();
        HandleLineRenderer();
        HandleGlowColor();
        HandleExpression();
        HandleInvincibleVisual();
        HandleAntennae();
    }

    private void PrepareEmissiveMaterials()
    {
        _glowMaterialInstance = new Material(_glowMaterial);

        foreach (MeshRenderer mesh in _glowingParts)
        {
            mesh.material = _glowMaterialInstance;
        }
    }

    private void PrepareAntennae()
    {
        _antennae.transform.SetParent(null);
    }

    public void HandleWalking(bool isWalking)
    {
        _animator.SetBool(IS_WALKING, isWalking);
    }

    private void HandleRotation()
    {
        _objectToRotate.forward = Vector3.Slerp(_objectToRotate.forward, -_rotationEnd, _rotateSpeed * Time.deltaTime);
    }

    private void HandleGlowColor()
    {
        if (_glowColorCurrent == _glowColorEnd)
        {
            return;
        }

        _glowColorCurrent = Color.Lerp(_glowColorCurrent, _glowColorEnd, _glowColorChangeSpeed * Time.deltaTime);
        _glowMaterialInstance.SetColor("_EmissionColor", _glowColorCurrent * _glowIntensity);
    }

    private void HandleLineRenderer()
    {
        _lineRenderer.SetPosition(0, _lineRendererStart.position);
        _lineRenderer.SetPosition(1, _lineRendererEnd.position);
    }

    private void HandleExpression()
    {
        if (_eyeNeutral.activeSelf)
        {
            return;
        }

        _curExpressionTime -= Time.deltaTime;

        if (_curExpressionTime <= 0f)
        {
            SetExpression(PlasmoExpression.Neutral);
        }
    }

    private void HandleInvincibleVisual()
    {
        if (_curIFrameTime <= 0f)
        {
            return;
        }

        HandleIFrameBlink();

        _curIFrameTime -= Time.deltaTime;

        if (_curIFrameTime <= 0f)
        {
            foreach (MeshRenderer mr in _iFrameBlinkParts)
            {
                mr.enabled = true;
            }
        }
    }

    private void HandleIFrameBlink()
    {
        _curIFrameBlinkInterval -= Time.deltaTime;

        if (_curIFrameBlinkInterval <= 0f)
        {
            foreach (MeshRenderer mr in _iFrameBlinkParts)
            {
                if (mr.enabled)
                {
                    mr.enabled = false;
                }
                else
                {
                    mr.enabled = true;
                }
            }

            _curIFrameBlinkInterval = _iFrameBlinkInterval;
        }
    }

    private void HandleAntennae()
    {
        _antennae.position = Vector3.Lerp(_antennae.position, _antennaeFollowPoint.position, _antennaeMoveSpeed * Time.deltaTime);
    }

    public void SetRotation(Vector3 inputDirection)
    {
        _rotationEnd = inputDirection;
    }

    public void SetGlowState(bool enabled)
    {
        if (enabled)
        {
            _glowMaterialInstance.EnableKeyword("_EMISSION");
        }
        else
        {
            _glowMaterialInstance.DisableKeyword("_EMISSION");
        }
    }

    public void SetExpression(PlasmoExpression newExpression)
    {
        _eyeNeutral.SetActive(false);
        _eyeAngry.SetActive(false);
        _eyeHappy.SetActive(false);
        _eyeSad.SetActive(false);

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
            case PlasmoExpression.Sad:
                _eyeSad.SetActive(true);
                break;
        }
    }

    public void SetExpression(PlasmoExpression newExpression, float time)
    {
        SetExpression(newExpression);

        _curExpressionTime = time;
    }

    public void SetIFrameTime(float time)
    {
        _curIFrameTime = time;
        _curIFrameBlinkInterval = _iFrameBlinkInterval;
    }

    public void SetAnimationTrigger(PlasmoAnimationTrigger trigger)
    {
        switch (trigger)
        {
            case PlasmoAnimationTrigger.Cast:
                _animator.SetTrigger(CAST_SPELL);
                break;
            case PlasmoAnimationTrigger.Dash:
                _animator.SetTrigger(DASH);
                break;
            case PlasmoAnimationTrigger.Win:
                _animator.SetTrigger(WIN);
                break;
            case PlasmoAnimationTrigger.Lose:
                _animator.SetTrigger(LOSE);
                break;
            case PlasmoAnimationTrigger.Hurt:
                _animator.SetTrigger(TAKE_DAMAGE);
                break;
        }
    }

    public void SetGlowColor(Color newColor)
    {
        _glowColorEnd = newColor;
    }

    public float GetDashExpressionTime()
    {
        return _dashExpressionTime;
    }

    public float GetCastExpressionTime()
    {
        return _castExpressionTime;
    }

    public float GetHurtExpressionTime()
    {
        return _hurtExpressionTime;
    }
}