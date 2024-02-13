using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingWall : MonoBehaviour,IMapElement
{
    [SerializeField] bool _idleRotate;
    [SerializeField] Vector3 _rotationDirection;

    [SerializeField] List<GameObject> _movePoints;
    [SerializeField] float _moveTime;

    private bool _moving;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(IdleRotation());
    }

    IEnumerator IdleRotation()
    {
        while(_idleRotate)
        {
            transform.eulerAngles += _rotationDirection * Time.deltaTime;
            yield return null;
        }
    }

    public void Activate()
    {
        if (!_moving)
            StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        _moving = true;
        float moveProgress = 0;
        Vector3 startLocation = transform.position;
        Vector3 targetLocation = DetermineMoveTarget();

        while (moveProgress < 1)
        {
            moveProgress += Time.deltaTime/_moveTime;
            transform.position = Vector3.Lerp(startLocation, targetLocation, moveProgress);
            yield return null;
        }
        transform.position = targetLocation;
        _moving = false;
    }

    Vector3 DetermineMoveTarget()
    {
        for(int i = 0; i <_movePoints.Count; i++)
        {
            if(transform.position == _movePoints[i].transform.position)
            {
                i++;
                if (i >= _movePoints.Count) i = 0;
                return _movePoints[i].transform.position;
            }
        }
        return Vector3.zero;
    }

}
