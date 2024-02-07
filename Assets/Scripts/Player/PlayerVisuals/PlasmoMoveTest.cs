using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasmoMoveTest : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 7f;
    [SerializeField] private float _rotateSpeed = 10f;
    [Space]
    private bool _isWalking;
    private PlasmoVisuals _visuals;

    private void Start()
    {
        _visuals = GetComponentInChildren<PlasmoVisuals>();
    }

    private void Update()
    {
        _visuals.HandleWalking(_isWalking);
        HandleMovement();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _visuals.SetAnimationTrigger(PlasmoVisuals.PlasmoAnimationTrigger.Cast);
            _visuals.SetExpression(PlasmoVisuals.PlasmoExpression.Angry, 0.5f);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            _visuals.SetAnimationTrigger(PlasmoVisuals.PlasmoAnimationTrigger.Dash);
            _visuals.SetExpression(PlasmoVisuals.PlasmoExpression.Happy, 1f);
        }
    }

    private void HandleMovement()
    {
        Vector2 inputVector = new Vector2(0, 0);
        if (Input.GetKey(KeyCode.W))
        {
            inputVector.y = 1;
        }
        else if(Input.GetKey(KeyCode.S))
        {
            inputVector.y = -1;
        }
        else
        {
            inputVector.y = 0;
        }
        if (Input.GetKey(KeyCode.D))
        {
            inputVector.x = 1;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            inputVector.x = -1;
        }
        else
        {
            inputVector.x = 0;
        }

        Vector3 moveDir = new Vector3(inputVector.x, 0, inputVector.y);

        transform.position += moveDir * _moveSpeed * Time.deltaTime;

        _isWalking = moveDir != Vector3.zero;

        transform.forward = Vector3.Slerp(transform.forward, -moveDir, _rotateSpeed * Time.deltaTime);
    }
}