using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupManager : MonoBehaviour
{
    [SerializeField] List<GameObject> _pickUps;
    //[SerializeField] List<GameObject> _spawnPoints;
    [SerializeField] List<PickupLocation> _spawnPoints;
    [SerializeField] float _timerBetweenPickup;
    [SerializeField] Vector3 _spawnOffset;
    private Vector3 lastSpawnLocation;

    public static PickupManager Instance;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
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
        GameObject rngLoc = RandomLocation();

        if (rngLoc == null)
        {
            //Prevents an NRE
            return;
        }

        GameObject newPickup = Instantiate(RandomPickup(), rngLoc.transform.position + _spawnOffset, Quaternion.identity);
        rngLoc.GetComponent<PickupLocation>().SetCurrentPickup(newPickup);
    }

    public void RemovePickupFromLocation(GameObject pickUp)
    {
        foreach(PickupLocation pl in _spawnPoints)
        {
            if (pl.GetCurrentPickup() == pickUp)
                pl.SetCurrentPickup(null);
        }
    }

/*    private bool ValidSpawnLocationExists()
    {
        foreach(PickupLocation pl in _spawnPoints)
        {
            if (pl.GetCurrentPickup() == null) return true;
        }
        return false;
    }*/

    GameObject RandomPickup()
    {
        return _pickUps[Random.Range(0, _pickUps.Count)];
    }

    GameObject RandomLocation()
    {
        GameObject spawnLoc;
        if(_spawnPoints.Count > 1)
        {
            do
            {
                spawnLoc = _spawnPoints[Random.Range(0, _spawnPoints.Count)].gameObject;
            }
            while (spawnLoc.transform.position == lastSpawnLocation );
            lastSpawnLocation = spawnLoc.transform.position;
            return spawnLoc;
        }

        if (_spawnPoints.Count < 1)
        {
            //Added this to prevent an NRE that would occur due to improper pickup spawn locations.
            return null;
        }

        return _spawnPoints[Random.Range(0, _spawnPoints.Count)].gameObject;
    }
}
