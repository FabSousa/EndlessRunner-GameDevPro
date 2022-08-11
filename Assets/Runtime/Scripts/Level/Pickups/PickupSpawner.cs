using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Pickups[] commonPickups;
    [SerializeField] private Pickups[] rarePickups;
    [SerializeField] private Pickups[] powerups;
    [SerializeField] private Transform start;
    [SerializeField] private Transform end;

    [Header("Variables")]
    [SerializeField] private float spaceBetweenPickups = 2;
    [Range(0,1)] [SerializeField] private float chanceToSkipPosition = 0.1f;
    [Range(0, 1)] [SerializeField] private float chanceToSpawnRarePickup = 0.1f;
    [Range(0, 1)] [SerializeField] private float chanceToSpawnPowerup = 0.1f;

    public void SpawnPickups(Vector3[] skipPositions)
    {
        if (Random.value <= chanceToSpawnPowerup) SpawnPowerup();
        else SpawnBasicPickupLine(skipPositions);
    }

    private void SpawnPowerup()
    {
        Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y, Random.Range(start.position.z, end.position.z));
        Pickups pickup = Instantiate(powerups[Random.Range(0, powerups.Length)], spawnPosition, Quaternion.identity, transform);
    }

    private void SpawnBasicPickupLine(Vector3[] skipPositions)
    {
        Pickups pickup;
        Vector3 currentSpawnPosition = start.position;
        while (currentSpawnPosition.z < end.position.z)
        {
            if (!ShouldSkipPosition(currentSpawnPosition, skipPositions))
            {
                if (Random.value <= chanceToSpawnRarePickup)
                    pickup = Instantiate(rarePickups[Random.Range(0, rarePickups.Length)], currentSpawnPosition, Quaternion.identity, transform);
                else
                    pickup = Instantiate(commonPickups[Random.Range(0, commonPickups.Length)], currentSpawnPosition, Quaternion.identity, transform);
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
        if (Random.value <= chanceToSkipPosition) return true;
        return false;
    }
}