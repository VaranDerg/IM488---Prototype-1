using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopupTextUI : BaseUIElement
{
    private const string HIDEPOPUP_ANIM = "HidePopup";

    [SerializeField] private float _rotationDeviation;
    [SerializeField] private float _floatSpeed;
    [Space]
    [SerializeField] private TextMeshProUGUI _popupText;
    [SerializeField] private Transform _textTransform;
    [SerializeField] private Animator _animator;

    private void Update()
    {
        transform.position += Vector3.up * _floatSpeed * Time.deltaTime;
    }

    public void PreparePopupText(string text, Color color, float lifetime)
    {
        _textTransform.rotation = Quaternion.Euler(0, 0, Random.Range(-_rotationDeviation, _rotationDeviation));

        _popupText.text = text;
        _popupText.color = color;

        StartCoroutine(PopupTextLifetime(lifetime));
    }

    private IEnumerator PopupTextLifetime(float lifetime)
    {
        float animTime = GetAnimationTime(_animator, HIDEPOPUP_ANIM);
        float timeBeforeAnim = lifetime - animTime;

        yield return new WaitForSeconds(timeBeforeAnim);

        _animator.Play(HIDEPOPUP_ANIM);

        yield return new WaitForSeconds(animTime);

        Destroy(gameObject);
    }
}
