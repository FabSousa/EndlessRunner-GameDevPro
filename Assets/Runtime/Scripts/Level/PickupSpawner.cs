using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] pickupOptions;

    private GameObject currentPickup;

    public void SpawnPickups()
    {
        GameObject prefab = pickupOptions[Random.Range(0, pickupOptions.Length)];
        currentPickup = Instantiate(prefab, transform);
        currentPickup.transform.localPosition = Vector3.zero;
        currentPickup.transform.rotation = Quaternion.identity;
    }
}