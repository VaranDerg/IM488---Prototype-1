using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [Header("References")]
    //[SerializeField] private MovementScript _pMovement;
    [SerializeField] private PlayerHealth _pHealth;
    [SerializeField] private Controller _pController;
    [SerializeField] private GameObject _spellAttachPoint;
    [SerializeField] private ElementalStats elementalStats;

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

    public void Damage(float damage)
    {
        _pHealth.TakeDamage(damage);
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
        }
        
    }

    public void AddElementalStat(Elements.SpellElement element)
    {
        elementalStats.AddElementStat(element);
    }
}