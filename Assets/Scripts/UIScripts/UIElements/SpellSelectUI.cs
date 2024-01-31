using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

/// <summary>
/// Author: Liz
/// Description: Manages the UI part of spell selection. some functionality, this is a prototype :3
/// </summary>
public class SpellSelectUI : BaseUIElement
{
    private const string SPELLBOX_ANIM_ENTER = "SpellBoxOpen";

    [SerializeField] private float _populateWithSpellsDelay = 0.5f;
    [Space]
    [SerializeField] private GameObject _spellCard;
    [Space]
    [SerializeField] private Transform _spellGrid;
    [SerializeField] private TextMeshProUGUI _headerText;
    [SerializeField] private Animator _spellBoxAnimator;
    private List<SpellCard> _spawnedSpellCards = new List<SpellCard>();
    private List<TestSpellSO> _spawnedSpells = new List<TestSpellSO>();

    /// <summary>
    /// Sets up text and spawns spell cards
    /// </summary>
    private void Start()
    {
        int curPlayer = ManagerParent.Instance.Spells.SpellSelectionModeToPlayer(ManagerParent.Instance.Spells.GetCurrentSpellSelectionMode());

        PopulateWithSpells(curPlayer);

        _headerText.text = "Fuse, " + ManagerParent.Instance.Game.GetPlayerName() + " " + curPlayer + ".";
    }

    /// <summary>
    /// Wrapper in case this ever needs to be called externally, but I doubt it does
    /// </summary>
    public void PopulateWithSpells(int curPlayer)
    {
        StartCoroutine(SpellPopulateProcess(curPlayer));
    }

    /// <summary>
    /// Enumerator for populating spells in a cool animation
    /// </summary>
    private IEnumerator SpellPopulateProcess(int curPlayer)
    {
        yield return new WaitForSecondsRealtime(_populateWithSpellsDelay);

        _spellBoxAnimator.Play(SPELLBOX_ANIM_ENTER);

        yield return new WaitForSecondsRealtime(GetAnimationTime(_spellBoxAnimator, SPELLBOX_ANIM_ENTER));

        for (int i = 0; i < ManagerParent.Instance.Options.GetSpellsToPick(); i++)
        {
            SpellCard newSpellCard = SpawnSpellCard();
            _spawnedSpellCards.Add(newSpellCard);
        }

        // Ensure opposing player input is disabled
        if (curPlayer == 1)
        {
            InputParent.Instance.SetSelectedUIObject(Player.two, null);
            InputParent.Instance.SetSelectedUIObject(Player.one, _spawnedSpellCards[0].gameObject);
            InputParent.Instance._P2Input.DisableInput();
        }
        else if (curPlayer == 2)
        {
            InputParent.Instance.SetSelectedUIObject(Player.one, null);
            InputParent.Instance.SetSelectedUIObject(Player.two, _spawnedSpellCards[0].gameObject);
            InputParent.Instance._P1Input.DisableInput();
        }
    }

    /// <summary>
    /// Spawns and prepares the spell card while adding it to correct lists
    /// </summary>
    private SpellCard SpawnSpellCard()
    {
        SpellManager psm = ManagerParent.Instance.Spells;
        int currentPlayer = psm.SpellSelectionModeToPlayer(psm.GetCurrentSpellSelectionMode());

        List<TestSpellSO> newSpells = psm.GetNewSpellsForPlayer(currentPlayer, _spawnedSpells);
        TestSpellSO spell = newSpells[Random.Range(0, newSpells.Count)];
        _spawnedSpells.Add(spell);

        SpellCard spellCard = Instantiate(_spellCard, _spellGrid).GetComponent<SpellCard>();
        spellCard.GiveSpell(spell, currentPlayer);

        return spellCard;
    }
    
    /// <summary>
    /// Removes each spell card that isn't selected
    /// </summary>
    /// <param name="selectedCard">The card the player picked</param>
    public void RemovePassedSpellCards(SpellCard selectedCard)
    {
        foreach (SpellCard card in _spawnedSpellCards)
        {
            if (card == selectedCard)
            {
                continue;
            }

            card.RemoveCard();
        }
    }
}