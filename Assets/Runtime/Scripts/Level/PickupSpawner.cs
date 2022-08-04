using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    [SerializeField] private Pickups pickupPref;
    [SerializeField] private Transform start;
    [SerializeField] private Transform end;
    [SerializeField] private float spaceBetweenPickups = 2;
    [Range(0,1)] [SerializeField] private float chanceToSkipPosition = 0.1f;

    public void SpawnPickups(Vector3[] skipPositions)
    {
        Vector3 currentSpawnPosition = start.position;
        while (currentSpawnPosition.z < end.position.z)
        {
            if (!ShouldSkipPosition(currentSpawnPosition, skipPositions))
            {
                Pickups pickup = Instantiate(pickupPref, currentSpawnPosition, Quaternion.identity, transform);
            }
            currentSpawnPosition.z += spaceBetweenPickups;
        }
    }

    private bool ShouldSkipPosition(Vector3 currentSpawnPosition, Vector3[] skipPositions)
    {
        foreach (var skipPosition in skipPositions)
        {
            float skipStart = skipPosition.z - spaceBetweenPickups * 0.5f;
            float skipEnd = skipPosition.z + spaceBetweenPickups * 0.5f;

            if(currentSpawnPosition.z >= skipStart && currentSpawnPosition.z <= skipEnd)
            {
                return true;
            }
        }
        if (Random.value < chanceToSkipPosition) return true;
        return false;
    }
}