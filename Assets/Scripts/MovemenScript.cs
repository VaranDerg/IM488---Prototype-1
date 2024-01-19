using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class MovemenScript : MonoBehaviour
{
    // Start is called before the first frame update

    Vector3 input;
    Rigidbody rb;
    [SerializeField] float speed;
    [SerializeField] float dashForce;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //getting wasd input
        float horizontalInput = Input.GetAxis("Horizontal");
        float veriticalInput = Input.GetAxis("Vertical");
        //wasd input vector
        input = new Vector3 (horizontalInput, 0,veriticalInput);
        Move(input);

        if (Input.GetKey(KeyCode.Space))
        {
            Dash(input, dashForce);
        }
        
    }
    /// <summary>
    /// modular movement function, just give it proper input vector
    /// </summary>
    /// <param name="input"></param>
    private void Move(Vector3 input)
    {
        
        rb.velocity = input * speed * Time.deltaTime;


    }
    /// <summary>
    /// Allows the player to dash in the direction he is moving in
    /// </summary>
    /// <param name="input"></param>
    /// <param name="dashForce"></param>
    private void Dash(Vector3 input, float dashForce)
    {
        rb.AddForce(input * dashForce, ForceMode.Impulse);
    }
}
