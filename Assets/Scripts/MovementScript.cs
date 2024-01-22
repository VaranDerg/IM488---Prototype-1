using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementScript : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float dashForce;
    [SerializeField] float dashTime;
    [SerializeField] float maxSpeed;

    public enum MovementState
    {
        Stationary,
        Moving,
        Dashing
    };
    private MovementState _moveState;
    private Vector3 _inputDirection;
    private Rigidbody rb;
    private PlayerActions _inputSystem;


    
    void FixedUpdate()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        Move();
        MaxSpeedControl();
    }

    private void MaxSpeedControl()
    {
        if (rb.velocity.magnitude > maxSpeed)
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
    }



    #region StartUp
    /// <summary>
    /// You know how start works I'm not going to explain it to you
    /// </summary>
    void Start()
    {
        VariableAssignment();
        InputSystemAssignment();
    }

    /// <summary>
    /// Assigns variables before play
    /// </summary>
    private void VariableAssignment()
    {
        _moveState = MovementState.Stationary;
        rb = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// Set up for the Input System
    /// </summary>
    private void InputSystemAssignment()
    {
        _inputSystem = new PlayerActions();
        _inputSystem.PlayerMovement.Enable();

        _inputSystem.PlayerMovement.Move.performed += MoveInput;
        _inputSystem.PlayerMovement.Move.canceled += context => _inputDirection = Vector2.zero;

        _inputSystem.PlayerMovement.Dash.performed += DashInput;
    }
    #endregion


    #region Input Actions
    /// <summary>
    /// Gets the wasd input and stores it in a Vector3
    /// </summary>
    /// <param name="context"></param>
    public void MoveInput(InputAction.CallbackContext context)
    {
        _inputDirection = new Vector3(context.ReadValue<Vector2>().x,0, context.ReadValue<Vector2>().y);
    }

    /// <summary>
    /// Allows the player to dash in the direction he is moving in
    /// </summary>
    /// <param name="context"></param>
    public void DashInput(InputAction.CallbackContext context)
    {
        if (_moveState == MovementState.Dashing)
            return;
        _moveState = MovementState.Dashing;
        rb.velocity = Vector3.zero;
        rb.AddForce(_inputDirection * dashForce, ForceMode.Impulse);
        //Debug.Log("Dash");
        StartCoroutine(DashCoolDown());
    }

    private IEnumerator DashCoolDown()
    {
        yield return new WaitForSeconds(dashTime);
        _moveState = MovementState.Stationary;
    }
    #endregion

    #region Movement
    /// <summary>
    /// modular movement function, just give it proper input vector
    /// </summary>
    /// <param name="input"></param>
    private void Move()
    {
        if (_moveState == MovementState.Dashing)
            return;
        rb.velocity = _inputDirection * speed;
    }
    #endregion
}
