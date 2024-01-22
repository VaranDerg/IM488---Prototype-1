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
    private Vector3 _input;
    private Rigidbody rb;
    private PlayerActions _inputSystem;



    // Update is called once per frame
    void FixedUpdate()
    {
        

        
        
    }

    private void Update()
    {
        //getting wasd input
        float horizontalInput = Input.GetAxis("Horizontal");
        float veriticalInput = Input.GetAxis("Vertical");
        //wasd input vector
        _input = new Vector3(horizontalInput, 0, veriticalInput);
        Move(_input);

        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            Dash(input, dashForce);
        }*/
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

        _inputSystem.PlayerMovement.Dash.performed += Dash;
    }
    #endregion


    #region Movement Actions
    /// <summary>
    /// modular movement function, just give it proper input vector
    /// </summary>
    /// <param name="input"></param>
    private void Move(Vector3 input)
    {
        if (_moveState == MovementState.Dashing)
            return;
        rb.velocity = input * speed;
    }

    /// <summary>
    /// Allows the player to dash in the direction he is moving in
    /// </summary>
    /// <param name="context"></param>
    public void Dash(InputAction.CallbackContext context)
    {
        if (_moveState == MovementState.Dashing)
            return;
        _moveState = MovementState.Dashing;
        rb.velocity = Vector3.zero;
        rb.AddForce(_input * dashForce, ForceMode.Impulse);
        //Debug.Log("Dash");
        StartCoroutine(DashCoolDown());
    }

    private IEnumerator DashCoolDown()
    {
        yield return new WaitForSeconds(dashTime);
        _moveState = MovementState.Stationary;
    }
    #endregion
}
