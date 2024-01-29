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

    //TESTING PURPOSES ONLY
    void Update()
    {
        //Don't leave debugs in before finalizing! - Liz
        //if(Input.GetKeyDown(KeyCode.Y))
        //{
        //    TakeDamage(1);
        //}
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

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;

        SetHPWheelValue();
        CheckForDeath();
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

            ManagerParent.Instance.Game.IncreasePlayerScore(ThisPlayer());
        }
    }
}