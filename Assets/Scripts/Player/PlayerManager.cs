using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [Header("References")]
    //[SerializeField] private MovementScript _pMovement;
    [SerializeField] private PlayerHealth _pHealth;
    [SerializeField] private Controller _pController;

    public Player PlayerTag { get; private set; }

/*    [Header("Variables")]
    [SerializeField] private bool isP1;*/

    // Start is called before the first frame update
    void Start()
    {
        PlayerTag = GameRoundHandler.Instance.AssignPlayer(gameObject);

        MultiplayerManager.Instance.AssignPlayer(PlayerTag, this);
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
}
