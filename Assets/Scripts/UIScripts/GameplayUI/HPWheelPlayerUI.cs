using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPWheelPlayerUI : BaseUIElement
{
    private const string ADJUSTVALUE_ANIM = "AdjustValue";
    private const string SHOWWHEEL_ANIM = "ShowWheel";
    private const string HIDEWHEEL_ANIM = "HideWheel";

    [SerializeField] private Gradient _gradient;
    [SerializeField] private float _wheelDisplayTime;
    [Space]
    [SerializeField] private Image _hpWheelImage;
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _wheelVisual;

    private float _curDisplayTime;

    private void Start()
    {
        ResetTimer();
        SetWheelFillState(1f);
        _wheelVisual.SetActive(false);
    }

    private void Update()
    {
        WheelDisplayTimer();
    }

    private void SetWheelFillState(float value)
    {
        _hpWheelImage.fillAmount = value;
        _hpWheelImage.color = _gradient.Evaluate(value);
    }

    public void SetWheelValue(float valueNormalized)
    {
        if (valueNormalized > 1 || valueNormalized < 0)
        {
            Debug.LogWarning($" Passed value ({valueNormalized}) is not normalized!");
            return;
        }

        ResetTimer();

        if (_wheelVisual.activeSelf)
        {
            _animator.Play(ADJUSTVALUE_ANIM);
        }
        else
        {
            Show();
        }

        SetWheelFillState(valueNormalized);
    }

    private void WheelDisplayTimer()
    {
        if (!_wheelVisual.activeSelf)
        {
            return;
        }

        _curDisplayTime -= Time.deltaTime;
        
        if (_curDisplayTime <= 0f)
        {
            ResetTimer();
            Hide();
        }
    }

    private void ResetTimer()
    {
        _curDisplayTime = _wheelDisplayTime;
    }

    private void Show()
    {
        _wheelVisual.SetActive(true);
        _animator.Play(SHOWWHEEL_ANIM);
    }

    private void Hide()
    {
        StartCoroutine(HideWheelProcess());
    }

    private IEnumerator HideWheelProcess()
    {
        _animator.Play(HIDEWHEEL_ANIM);

        yield return new WaitForSeconds(GetAnimationTime(_animator, HIDEWHEEL_ANIM));

        _wheelVisual.SetActive(false);
    }

    #region Testing

    private float _curHPTest = 1;

    public void AdjustHP(float amount)
    {
        _curHPTest += amount;

        SetWheelValue(_curHPTest);
    }

    #endregion
}
