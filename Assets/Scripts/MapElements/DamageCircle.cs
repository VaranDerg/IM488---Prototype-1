using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCircle : MonoBehaviour
{
    [SerializeField] float _closingTime;
    [SerializeField] float _damagePerSecond;
    [SerializeField] Vector3 _endingScale;
    List<ICanTakeDamage> _playersToDamage = new List<ICanTakeDamage>();
    Vector3 _startingLocalScale;

    [ContextMenu("CloseCircle")]
    public void StartCircleClosing()
    {
        StartCoroutine(CloseCircle());
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
                player.Damage(_damagePerSecond * Time.deltaTime, InvulnTypes.IGNOREINVULN);
            yield return null;
        }
    }    

    private void OnTriggerEnter(Collider collision)
    {
        ICanTakeDamage damageTarget = collision.GetComponent<ICanTakeDamage>();
        Debug.Log(damageTarget);
        if (_playersToDamage.Count == 0) return;
        Debug.Log(_playersToDamage.Contains(damageTarget));
        Debug.Log("HERE");
        if (damageTarget != null && _playersToDamage.Contains(damageTarget))
        {
            Debug.Log("ERROR");
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
