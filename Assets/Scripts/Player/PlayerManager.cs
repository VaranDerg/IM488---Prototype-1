using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private MovementScript _pMovement;
    [SerializeField] private PlayerHealth _pHealth;
    [SerializeField] private Controller _pController;

    public PlayerTag PlayerTag { get; private set; }

/*    [Header("Variables")]
    [SerializeField] private bool isP1;*/

    // Start is called before the first frame update
    void Start()
    {
        PlayerTag = GameRoundHandler.instance.AssignPlayer(gameObject);
    }

    public void PlayerStartingLocation(Vector3 startPos)
    {
        transform.position = startPos;
    }
}

public enum PlayerTag
{
    PLAYER1,
    PLAYER2
}
