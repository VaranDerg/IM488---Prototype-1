using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpellCard : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _descriptionText;
    [SerializeField] private Image _icon;

    private void Start()
    {
        PrepareSpellCard();
    }

    private void PrepareSpellCard()
    {
        PlayerSpellManager psm = PlayerSpellManager.Instance;
        List<TestSpellSO> newSpells = psm.GetNewSpellsForPlayer(psm.SpellSelectionModeToPlayer(psm.GetCurrentSpellSelectionMode()));
        TestSpellSO spell = newSpells[Random.Range(0, newSpells.Count)];

        _nameText.text = spell.SpellName;
        _descriptionText.text = spell.SpellDescription;
        _icon.color = spell.SpellColor;
    }

    public void SelectSpell()
    {

    }
}