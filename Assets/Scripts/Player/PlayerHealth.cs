using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float _maxHealth;
    private float _currentHealth;

    [SerializeField] private float _damageIFrameLength;

    private bool _damageIFrames;

    //added to prevent repeated death calls
    private bool _dead;

    // Start is called before the first frame update
    void Start()
    {
        _currentHealth = _maxHealth;
    }

    private int ThisPlayer()
    {
        Player p = GetComponent<PlayerManager>().PlayerTag;

        if (p == Player.one)
        {
            return 1;
        }
        else
        {
            return 2;
        }
    }

    public int NotThisPlayer()
    {
        Player p = GetComponent<PlayerManager>().PlayerTag;

        if (p == Player.one)
        {
            return 2;
        }
        else
        {
            return 1;
        }
    }

    public void TakeDamage(float damage, InvulnTypes invulnType)
    {  
        _currentHealth -= damage;
        if(invulnType != InvulnTypes.FULLINVULN)
            StartCoroutine(DamageIFrameProcess());

        SetHPWheelValue();
        CheckForDeath();
    }

    public bool Heal(float healAmount)
    {
        if (_currentHealth == _maxHealth)
            return false;
        _currentHealth += healAmount;
        _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
        SetHPWheelValue();
        return true;
    }

    public bool InvulnerableTypeCheck(InvulnTypes invulnType)
    {
        switch (invulnType)
        {
            case (InvulnTypes.FULLINVULN):
                if (DamageInvulnerable() || DashInvulnerable()) return true;
                return false;
            case (InvulnTypes.DASHINVULN):
                if (DashInvulnerable()) return true;
                return false;
            case (InvulnTypes.IGNOREINVULN):
                return false;
        }
        return false;
    }

    private IEnumerator DamageIFrameProcess()
    {
        _damageIFrames = true;
        yield return new WaitForSeconds(_damageIFrameLength);
        _damageIFrames = false;
    }

    public bool DashInvulnerable()
    {
        if(GetComponent<PlayerManager>().GetPlayerController().GetMoveState() == Controller.MovementState.Dashing) return true;
        return false;
    }

    public bool DamageInvulnerable()
    {
        if (_damageIFrames) return true;
        return false;
    }

    /// <summary>
    /// Temporary workaround for HP
    /// </summary>
    private void SetHPWheelValue()
    {
        HPWheelUI[] wheels = FindObjectsOfType<HPWheelUI>();
        
        foreach (HPWheelUI wheel in wheels)
        {
            if (wheel.GetPlayer() == ThisPlayer())
            {
                wheel.SetWheelValue(_currentHealth / _maxHealth);
            }

            if (wheel.GetPlayer() == ThisPlayer())
            {
                wheel.SetWheelValue(_currentHealth / _maxHealth);
            }
        }
    }

    public void CheckForDeath()
    {
        //GameRoundHandler.Instance.DetermineWhoDied(
        if(_currentHealth <= 0 && !_dead)
        {
            if (ManagerParent.Instance.Game.PlayerHasWonRound)
            {
                return;
            }

            //GameRoundHandler.Instance.DetermineWhoDied(GetComponent<PlayerManager>().PlayerTag);
            _currentHealth = 0;
            _dead = true;

            StopPlayerAndSpellsOnDeath();
            ManagerParent.Instance.Game.StartTieWindow(this);
        }
    }


    private void StopPlayerAndSpellsOnDeath()
    {
        GetComponent<PlayerManager>().GetPlayerController().StopVelocity();
        GetComponent<PlayerManager>().GetPlayerController().enabled = false;
        GetComponent<PlayerManager>().DisableAssociatedSpells();
    }
}