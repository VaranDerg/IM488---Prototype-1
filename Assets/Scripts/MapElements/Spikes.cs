using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour,IMapElement
{
    [SerializeField] bool _instantDeathSpikes;
    
    [SerializeField] float _damageAmount;
    [SerializeField] float _damageRate;
    [SerializeField] Vector3 _activeLocation;
    [SerializeField] float _moveTime;
    private Coroutine _damageCoroutine;
    private Coroutine _movingCoroutine;
    bool _active;
    List<GameObject> _objectsInRange = new List<GameObject>();

    IEnumerator DamageTick()
    {
        while(_objectsInRange.Count > 0)
        {
            foreach (GameObject obj in _objectsInRange)
                obj.GetComponent<ICanTakeDamage>().Damage(_damageAmount,InvulnTypes.DASHINVULN);
            yield return new WaitForSeconds(_damageRate);
        }
        _damageCoroutine = null;
    }

    public void Activate()
    {
        if (_movingCoroutine == null)
            _movingCoroutine = StartCoroutine(MoveActivation());
    }

    IEnumerator MoveActivation()
    {
        float moveProgress = 0;
        
        Vector3 startLocation = transform.position;
        float _moveDirection =1;
        if(_active)
        {
            _moveDirection *= -1;
        }
        Vector3 targetLocation = transform.position + (_activeLocation*_moveDirection);

        while (moveProgress < 1)
        {
            moveProgress += Time.deltaTime/_moveTime;
            transform.position = Vector3.Lerp(startLocation, targetLocation, moveProgress);
            yield return null;
        }
        transform.position = targetLocation;

        ChangeActiveState();
        _movingCoroutine = null;
    }

    void ChangeActiveState()
    {
        _active = !_active;
        GetComponent<Collider>().enabled = _active;
        if (!_active) _objectsInRange = new List<GameObject>();
    }


    private void OnTriggerEnter(Collider collision)
    {
        if (collision.GetComponent<ICanTakeDamage>() == null)
            return;

        if (_instantDeathSpikes)
        {
            collision.GetComponent<ICanTakeDamage>().Damage(10, InvulnTypes.DASHINVULN);
            return;
        }
            
        _objectsInRange.Add(collision.gameObject);
        if(_damageCoroutine == null)
        {
            _damageCoroutine = StartCoroutine(DamageTick());
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.GetComponent<ICanTakeDamage>() == null)
            return;
        _objectsInRange.Remove(collision.gameObject);
    }

}
