using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupLocation : MonoBehaviour
{
    private GameObject _currentPickup;

    public GameObject GetCurrentPickup()
    {
        return _currentPickup;
    }

    public void SetCurrentPickup(GameObject pickup)
    {
        if(_currentPickup != null)
        {
            Destroy(_currentPickup);
        }
        _currentPickup = pickup;
    }
}
