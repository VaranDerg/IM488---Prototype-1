using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Controller : MonoBehaviour
{

    /*public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    private void Update()
    {
        rb.velocity = movementInput * speed;

        if (rb.velocity.magnitude > maxSpeed)
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
    }*/
    [Header("Movement Variables")]
    [SerializeField] float speed;
    [SerializeField] float dashForce;
    [SerializeField] float dashTime;
    [SerializeField] float dashCooldownTime;
    [SerializeField] float maxSpeed;
    [Space]

    [Header("Spells")]
    //[SerializeField] private List<ISpell> spellList;
    [SerializeField] private List<AbstractSpell> manualSpellList;
    [SerializeField] private List<AbstractSpell> dashSpellList;
    

    public enum MovementState
    {
        Stationary,
        Moving,
        Dashing
    };
    private bool dashCoolingDown;
    private MovementState _moveState;
    private Vector3 _inputDirection;
    private Vector3 lastNonZeroMovement;
    private Rigidbody rb;

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

    public Vector3 GetMovementDirection()
    {
        return _inputDirection;
    }

    public Vector3 GetLastNonZeroMovement()
    {
        return lastNonZeroMovement;
    }

    public void AddManualSpellToList(AbstractSpell newSpell)
    {
        manualSpellList.Add(newSpell);
    }

    public void AddDashSpellToList(AbstractSpell newSpell)
    {
        dashSpellList.Add(newSpell);
    }


    //Temporary
    public void AddManualStartingSpells()
    {
        foreach (AbstractSpell currentSpell in GetComponentsInChildren<AbstractSpell>())
        {
            Debug.Log("FoundSpell");
            manualSpellList.Add(currentSpell);
        }
    }
    //Temporary
    public void AddDashStartingSpells()
    {
        foreach (AbstractSpell currentSpell in GetComponentsInChildren<AbstractSpell>())
        {
            Debug.Log("FoundSpell");
            dashSpellList.Add(currentSpell);
        }
    }


    #region StartUp
    /// <summary>
    /// You know how start works I'm not going to explain it to you
    /// </summary>
    void Start()
    {
        VariableAssignment();
    }

    /// <summary>
    /// Assigns variables before play
    /// </summary>
    private void VariableAssignment()
    {
        _moveState = MovementState.Stationary;
        rb = GetComponent<Rigidbody>();

        //TEMPORARY
        /*AddManualStartingSpells();
        AddDashStartingSpells();*/
    }

    #endregion


    #region Input Actions
    /// <summary>
    /// Gets the wasd input and stores it in a Vector3
    /// </summary>
    /// <param name="context"></param>
    public void MoveInput(InputAction.CallbackContext context)
    {
        _inputDirection = new Vector3(context.ReadValue<Vector2>().x, 0, context.ReadValue<Vector2>().y);

        if (_inputDirection != Vector3.zero)
            lastNonZeroMovement = _inputDirection;
    }

    /// <summary>
    /// Allows the player to dash in the direction he is moving in
    /// </summary>
    /// <param name="context"></param>
    public void DashInput(InputAction.CallbackContext context)
    {
        if (dashCoolingDown)
            return;
        _moveState = MovementState.Dashing;
        dashCoolingDown = true;

        rb.velocity = Vector3.zero;
        rb.AddForce(_inputDirection * dashForce, ForceMode.Impulse);

        DashSpellCast();

        StartCoroutine(DashProcess());
    }

    public void ManualCastInput(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            foreach (ISpell currentSpell in manualSpellList)
            {
                Debug.Log("Cast Manual Spell");
                currentSpell.Execute();
            }
        }
    }

    public void DashSpellCast()
    {
        foreach (ISpell currentSpell in dashSpellList)
        {
            Debug.Log("Cast Dash Spell");
            currentSpell.Execute();
        }
    }

    private IEnumerator DashProcess()
    {
        yield return new WaitForSeconds(dashTime);
        _moveState = MovementState.Stationary;
        yield return new WaitForSeconds(dashCooldownTime);
        dashCoolingDown = false;
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
