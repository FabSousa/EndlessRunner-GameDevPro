using Unity.VisualScripting;
using UnityEngine;

public class TrackSegment : MonoBehaviour
{
    [SerializeField] private Transform start;
    [SerializeField] private Transform end;

    [SerializeField] private ObstacleSpawner[] obstacleSpawners;
    [SerializeField] private DecorationSpawner decorationSpawner;

    [SerializeField] private PickupSpawner[] pickupSpawners;

    public Transform Start => start;
    public Transform End => end;

    public float Length => Vector3.Distance(End.position, Start.position);
    public float SqrLength => (End.position - Start.position).sqrMagnitude;

    public ObstacleSpawner[] ObstacleSpawners => obstacleSpawners;
    public DecorationSpawner DecorationSpawner => decorationSpawner;
    public PickupSpawner[] PickupSpawners => pickupSpawners;

    [Header("Pickups Parameters")]
    [Range(0, 1)]
    [SerializeField] private float pickupSpawnChance = 0.3f;
    [SerializeField] private float additionalLineChance = 0.1f;

    private Vector3[] skipPositions;

    public void SpawnPickupLines()
    {
        foreach (var pickupSpawner in pickupSpawners)
        {
            if (Random.value <= pickupSpawnChance)
            {
                Vector3[] skipPositions = new Vector3[obstacleSpawners.Length];
                for (int i = 0; i < skipPositions.Length; i++)
                {
                    skipPositions[i] = obstacleSpawners[i].transform.position;
                }

                pickupSpawner.SpawnPickups(skipPositions);
                if (Random.value > additionalLineChance) break;
            }
        }
    }
}
