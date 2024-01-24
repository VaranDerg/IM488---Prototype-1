using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private MovementScript _pMovement;
    [SerializeField] private PlayerHealth _pHealth;

    // Start is called before the first frame update
    void Start()
    {
        GameRoundHandler.instance.AssignPlayer(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
