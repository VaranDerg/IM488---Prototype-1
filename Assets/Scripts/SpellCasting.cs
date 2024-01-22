using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCasting : MonoBehaviour
{
    [SerializeField] private float _spellSlotSize;
    [SerializeField] private List<Elements.SpellElement> _spellSlots;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Ignore me I am just for testing purposes
        if(Input.GetKeyDown(KeyCode.P))
        {
            AddElementToSpellSlot(Elements.SpellElement.Placeholder);
        }
    }

    public void AddElementToSpellSlot(Elements.SpellElement element)
    {
        if (_spellSlots.Count >= _spellSlotSize)
            return;
        _spellSlots.Add(element);
    }
}
