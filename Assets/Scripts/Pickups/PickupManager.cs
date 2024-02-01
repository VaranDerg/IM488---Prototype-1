using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupManager : MonoBehaviour
{
    [SerializeField] List<GameObject> _pickUps;
    [SerializeField] List<GameObject> _spawnPoints;
    [SerializeField] float _timerBetweenPickup;
    private Vector3 lastSpawnLocation;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    IEnumerator SpawnPickupTimer()
    {
        while (ManagerParent.Instance.Game.PlayerHasWonRound)
        {
            yield return new WaitForSeconds(_timerBetweenPickup);
            SpawnObjectAtRandomLocation();
        }
    }

    void SpawnObjectAtRandomLocation()
    {
        GameObject pickup = _pickUps[Random.Range(0, _spawnPoints.Count)];
        Vector3 location = _spawnPoints[Random.Range(0, _spawnPoints.Count)].transform.position;

        Instantiate<pickup>
    }

    Vector3 RandomLocation()
    {
        return _spawnPoints[Random.Range(0, _spawnPoints.Count)].transform.position;
    }
}
