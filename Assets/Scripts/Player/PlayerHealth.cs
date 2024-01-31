using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour,ICanTakeDamage
{
    [SerializeField] private float _maxHealth;

    private float _currentHealth;

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

    private int NotThisPlayer()
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

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;

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

            ManagerParent.Instance.Game.IncreasePlayerScore(NotThisPlayer());
        }
    }
}