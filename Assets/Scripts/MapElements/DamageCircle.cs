using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCircle : MonoBehaviour
{
    [SerializeField] float _timeBeforeStarting;
    [SerializeField] float _closingTime;
    [SerializeField] float _minDamagePerTick;
    [SerializeField] float _maxDamagePerTick;
    [SerializeField] float _tickRate;
    [SerializeField] float _damageScalingTime;
    [SerializeField] Vector3 _endingScale;
    List<ICanTakeDamage> _playersToDamage = new List<ICanTakeDamage>();
    Vector3 _startingLocalScale;
    float currentDPS;

    [ContextMenu("CloseCircle")]

    private void Start()
    {
        StartCoroutine(StartCircleClosing());
    }

    public IEnumerator StartCircleClosing()
    {
        yield return new WaitForSeconds(_timeBeforeStarting);
        StartCoroutine(CloseCircle());
        StartCoroutine(DamageScaling());
        StartCoroutine(DamageOutsidePlayers());
    }

    public IEnumerator CloseCircle()
    {
        float progress = 0;
        _startingLocalScale = transform.localScale;
        while(progress < 1)
        {
            progress += Time.deltaTime / _closingTime;
            transform.localScale = Vector3.Lerp(_startingLocalScale, _endingScale, progress);
            yield return null;
        }
        transform.localScale = _endingScale;
    }

    private IEnumerator DamageOutsidePlayers()
    {
        while(true)
        {
            foreach (ICanTakeDamage player in _playersToDamage)
                player.Damage(currentDPS, InvulnTypes.IGNOREINVULN);
            yield return new WaitForSeconds(_tickRate);
        }
    }  
    
    private IEnumerator DamageScaling()
    {
        float progress = 0;
        while(progress < 1)
        {
            progress += Time.deltaTime / _damageScalingTime;
            currentDPS = Mathf.Lerp(_minDamagePerTick, _maxDamagePerTick, progress);
            yield return null;
        }
        currentDPS = _maxDamagePerTick;
    }

    private void OnTriggerEnter(Collider collision)
    {
        ICanTakeDamage damageTarget = collision.GetComponent<ICanTakeDamage>();
        if (_playersToDamage.Count == 0) return;
        if (damageTarget != null && _playersToDamage.Contains(damageTarget))
        {
            _playersToDamage.Remove(damageTarget);
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        ICanTakeDamage damageTarget = collision.GetComponent<ICanTakeDamage>();
        if (damageTarget != null)
        {
            _playersToDamage.Add(damageTarget);
        }
    }
}
