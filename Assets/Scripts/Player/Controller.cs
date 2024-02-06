using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Controller : MonoBehaviour, IScalable, ICanUsePortal
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
    [SerializeField] float _postDashStunDuration;
    [Space]

    [Header("Spells")]
    [SerializeField] private List<AbstractSpell> dashSpellList;
    

    public enum MovementState
    {
        Stationary,
        Moving,
        Dashing,
        PostDash
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

    public MovementState GetMoveState()
    {
        return _moveState;
    }

    public void StopVelocity()
    {
        rb.velocity = Vector3.zero;
    }

    public void TeleportTo(Vector3 newLoc)
    {
        transform.position = newLoc;
    }

    public void AddDashSpellToList(AbstractSpell newSpell)
    {
        dashSpellList.Add(newSpell);
    }

    public void Scale(ElementalStats stats)
    {
        ScaleSpeed(stats.GetStat(ScalableStat.MOVE_SPEED));
    }

    private void ScaleSpeed(float speedMult)
    {
        speed *= speedMult;
    }

    #region StartUp
    /// <summary>
    /// You know how start works I'm not going to explain it to you
    /// </summary>
    void Start()
    {
        VariableAssignment();

        StartCoroutine(DelayedScaling());
    }

    /// <summary>
    /// Assigns variables before play
    /// </summary>
    private void VariableAssignment()
    {
        _moveState = MovementState.Stationary;
        rb = GetComponent<Rigidbody>();
    }

    IEnumerator DelayedScaling()
    {
        yield return new WaitForFixedUpdate();
        Scale(GetComponent<ElementalStats>());
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
        {
            if(context.started)
                GetComponent<PlayerManager>().SpawnText("On Cooldown!", Color.red, 1.5f);
            return;
        }

            
        _moveState = MovementState.Dashing;
        dashCoolingDown = true;

        rb.velocity = Vector3.zero;
        rb.AddForce(_inputDirection * dashForce, ForceMode.Impulse);
        DashSpellCast();

        StartCoroutine(DashProcess());
    }

    public void DashSpellCast()
    {
        foreach (ISpell currentSpell in dashSpellList)
        {
            //Debug.Log("Cast Dash Spell");
            currentSpell.DelayedStartAura();
        }
    }

    private IEnumerator DashProcess()
    {
        yield return new WaitForSeconds(dashTime);
        _moveState = MovementState.PostDash;
        rb.velocity = Vector3.zero;
        yield return new WaitForSeconds(_postDashStunDuration);
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
        if (_moveState == MovementState.Dashing || _moveState == MovementState.PostDash)
            return;
        Vector3 vel = rb.velocity;
        vel.x = _inputDirection.x * speed;
        vel.z = _inputDirection.z * speed;
        rb.velocity = vel;
    }
    #endregion
}
