using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour, ICanTakeDamage
{
    [Header("References")]
    //[SerializeField] private MovementScript _pMovement;
    [SerializeField] PlayerHealth _pHealth;
    [SerializeField] private Controller _pController;
    [SerializeField] private GameObject _spellAttachPoint;
    [SerializeField] private ElementalStats _elementalStats;
    [SerializeField] private PopupTextSource _textSource;
    [SerializeField] private HPWheelPlayerUI _hpWheel;

    public Player PlayerTag;

/*    [Header("Variables")]
    [SerializeField] private bool isP1;*/

    // Start is called before the first frame update
    void Start()
    {
        //PlayerTag = GameRoundHandler.Instance.AssignPlayer(gameObject);

        MultiplayerManager.Instance.AssignPlayer(PlayerTag, this);

        AttachAssociatedSpells();

        StartCoroutine(AttachInput());
    }

    IEnumerator AttachInput()
    {
        while(InputParent.Instance != null && !InputParent.Instance.AttachInput(_pController, PlayerTag))
        {
            yield return null;
        }
        //yield return new WaitForSeconds(3);
    }

    public void PlayerStartingLocation(Vector3 startPos)
    {
        transform.position = startPos;
    }

    public Vector3 GetMovementDirection()
    {
        return _pController.GetMovementDirection();
    }

    public Vector3 GetLastNonZeroMovement()
    {
        return _pController.GetLastNonZeroMovement();
    }

    public void Damage(float damage, InvulnTypes ignoreInvuln)
    {
        if (ManagerParent.Instance.Game.PlayerHasWonRound)
            return;
        if (_pHealth.InvulnerableTypeCheck(ignoreInvuln))
        {
            return;
        }
            
        _pHealth.TakeDamage(damage,ignoreInvuln);
    }

    private void AttachSpell(GameObject newSpell)
    {
        Instantiate(newSpell, _spellAttachPoint.transform).transform.SetParent(_spellAttachPoint.transform);
    }

    public void AttachAssociatedSpells()
    {
        foreach (TestSpellSO spell in ManagerParent.Instance.Spells.GetSpellListFromPlayer(PlayerTag))
        {
            AttachSpell(spell.GetPrefab());
            if(spell.SpellType.SpellTypeName == "Projectile")
            {
                AttachProjectileSpell(spell);
            }
        }
    }

    private void AttachProjectileSpell(TestSpellSO spell)
    {
        Transform firstSpell = _spellAttachPoint.transform.GetChild(0);

        if (firstSpell == null)
            return;

        //ProjectileSpell projectileSpell = firstSpell.GetComponent<ProjectileSpell>();
        StarterSpell starterSpell = firstSpell.GetComponent<StarterSpell>();
        starterSpell.ApplyElement(spell.SpellElement.element);
        //Debug.Log("Projectile: " + projectileSpell.name);
    }

    public void SpawnText(string text, Color color, float lifetime)
    {
        _textSource.DisplayPopup(text, color, lifetime);
    }

    public void DisableAssociatedSpells()
    {
        _spellAttachPoint.SetActive(false);
    }

    public void UpdateHPWheel()
    {
        _hpWheel.SetWheelValue(_pHealth.HealthPercent());
    }

    public PlayerHealth GetPlayerHealth()
    {
        return _pHealth;
    }

    public Controller GetPlayerController()
    {
        return _pController;
    }

    public ElementalStats GetElementalStats()
    {
        return _elementalStats;
    }
}