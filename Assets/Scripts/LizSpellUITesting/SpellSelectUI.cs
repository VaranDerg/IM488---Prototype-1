using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SpellSelectUI : BaseUIElement
{
    private const string SPELLBOX_ANIM_ENTER = "SpellBoxOpen";

    [SerializeField] [Range(3, 5)] private int _spellOptionsGiven;
    [SerializeField] private float _populateWithSpellsDelay = 0.5f;
    [Space]
    [SerializeField] private GameObject _spellCard;
    [SerializeField] private Transform _spellBox;
    [Space]
    [SerializeField] private TextMeshProUGUI _headerText;
    [Space]
    [SerializeField] private Animator _spellBoxAnimator;

    public void PopulateWithSpells()
    {
        StartCoroutine(SpellPopulateProcess());
    }

    private IEnumerator SpellPopulateProcess()
    {
        yield return new WaitForSecondsRealtime(_populateWithSpellsDelay);

        _spellBoxAnimator.Play(SPELLBOX_ANIM_ENTER);

        yield return new WaitForSecondsRealtime(GetAnimationTime(_spellBoxAnimator, SPELLBOX_ANIM_ENTER));

        for (int i = 0; i < _spellOptionsGiven; i++)
        {
            SpawnSpellCard();
        }
    }

    private void SpawnSpellCard()
    {
        PlayerSpellManager psm = PlayerSpellManager.Instance;
        int currentPlayer = psm.SpellSelectionModeToPlayer(psm.GetCurrentSpellSelectionMode());
        List<TestSpellSO> newSpells = psm.GetNewSpellsForPlayer(currentPlayer);
        TestSpellSO spell = newSpells[Random.Range(0, newSpells.Count)];

        SpellCard spellCard = Instantiate(_spellCard, _spellBox).GetComponent<SpellCard>();
        spellCard.GiveSpell(spell, currentPlayer);
    }
}