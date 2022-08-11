using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private GameMode gameMode;
    [SerializeField] private GameObject powerupX2Particle;

    private void OnTriggerEnter(Collider other)
    {
        ICollide collide = other.GetComponent<ICollide>();
        if (collide != null) collide.Collide(new CollisionInfo
        {
            Collider = other,
            GameMode = gameMode,
            PowerupX2Particle = powerupX2Particle
        });
    }
}
