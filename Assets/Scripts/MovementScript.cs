using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float dashForce;
    [SerializeField] float maxSpeed;

    private Vector3 input;
    private Rigidbody rb;
    private PlayerActions _input;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

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
        input = new Vector3(horizontalInput, 0, veriticalInput);
        Move(input);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Dash(input, dashForce);
        }
        MaxSpeedControl();
    }

    private void MaxSpeedControl()
    {
        if (rb.velocity.magnitude > maxSpeed)
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
    }

    #region Movement Actions
    /// <summary>
    /// modular movement function, just give it proper input vector
    /// </summary>
    /// <param name="input"></param>
    private void Move(Vector3 input)
    {  
        rb.velocity = input * speed;
    }
    /// <summary>
    /// Allows the player to dash in the direction he is moving in
    /// </summary>
    /// <param name="input"></param>
    /// <param name="dashForce"></param>
    private void Dash(Vector3 input, float dashForce)
    {
        rb.AddForce(input * dashForce, ForceMode.Impulse);
        Debug.Log("Dash");
    }
    #endregion
}
