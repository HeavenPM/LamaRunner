using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerLocation : MonoBehaviour
{
    [SerializeField] private Transform player;

    public GameObject[] locationPrefabs;
    private List<GameObject> activeLocations = new List<GameObject>();

    private float spawnPosition = 0;
    private float locationLenght = 72;
    private int startLocationCount = 3;

    private void Start()
    {
        SpawnLocation(0);

        for (int i = 0; i < startLocationCount; i++)
        {
            SpawnLocation(Random.Range(1, locationPrefabs.Length));
        }
    }

    private void FixedUpdate()
    {
        if (player.position.z - 40 > spawnPosition - (startLocationCount * locationLenght))
        {
            SpawnLocation(Random.Range(1, locationPrefabs.Length));
            DeleteLocation();
        }
            
    }

    private void SpawnLocation(int indexLocation)
    {
        GameObject nextLocation = Instantiate(locationPrefabs[indexLocation], transform.forward * spawnPosition, transform.rotation);
        activeLocations.Add(nextLocation);
        spawnPosition += locationLenght;
    }

    private void DeleteLocation()
    {
        Destroy(activeLocations[0]);
        activeLocations.RemoveAt(0);
    }
}
