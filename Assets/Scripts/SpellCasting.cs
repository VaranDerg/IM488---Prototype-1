using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCasting : MonoBehaviour
{
    [SerializeField] private float _elementSlotSize;
    [SerializeField] private List<Elements.SpellElement> _elementSlots;
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
        if (_elementSlots.Count >= _elementSlotSize)
            return;
        _elementSlots.Add(element);
    }
}
