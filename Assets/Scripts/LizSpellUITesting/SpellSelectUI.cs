using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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

    private void Start()
    {
        PopulateWithSpells();

        int curPlayer = ManagerParent.Instance.Spells.SpellSelectionModeToPlayer(ManagerParent.Instance.Spells.GetCurrentSpellSelectionMode());
        _headerText.text = "Fuse, " + ManagerParent.Instance.Game.GetPlayerName() + " " + curPlayer + ".";
    }

    public void PopulateWithSpells()
    {
        StartCoroutine(SpellPopulateProcess());
    }

    private IEnumerator SpellPopulateProcess()
    {
        yield return new WaitForSecondsRealtime(_populateWithSpellsDelay);

        _spellBoxAnimator.Play(SPELLBOX_ANIM_ENTER);

        yield return new WaitForSecondsRealtime(GetAnimationTime(_spellBoxAnimator, SPELLBOX_ANIM_ENTER));

        for (int i = 0; i < ManagerParent.Instance.Options.GetSpellsToPick(); i++)
        {
            SpellCard newSpellCard = SpawnSpellCard();
            _spawnedSpellCards.Add(newSpellCard);
        }
    }

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