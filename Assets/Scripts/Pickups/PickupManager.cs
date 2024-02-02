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
        StartCoroutine(SpawnPickupTimer());
    }

    IEnumerator SpawnPickupTimer()
    {
        yield return new WaitForSeconds(_timerBetweenPickup);
        while (!ManagerParent.Instance.Game.PlayerHasWonRound)
        {
            SpawnObjectAtRandomLocation();
            yield return new WaitForSeconds(_timerBetweenPickup);
        }
    }

    void SpawnObjectAtRandomLocation()
    {
        Instantiate(RandomPickup(), RandomLocation(), Quaternion.identity);
    }

    GameObject RandomPickup()
    {
        return _pickUps[Random.Range(0, _pickUps.Count)];
    }
    Vector3 RandomLocation()
    {
        Vector3 spawnLoc;
        if(_spawnPoints.Count > 1)
        {
            do
            {
                spawnLoc = _spawnPoints[Random.Range(0, _spawnPoints.Count)].transform.position;
            }
            while (spawnLoc == lastSpawnLocation);
            lastSpawnLocation = spawnLoc;
            return spawnLoc;
        }
        return _spawnPoints[Random.Range(0, _spawnPoints.Count)].transform.position;
    }
}
