using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private GameMode gameMode;

    private void OnTriggerEnter(Collider other)
    {
        ICollide collide = other.GetComponent<ICollide>();
        if (collide != null) collide.Collide(new CollisionInfo
        {
            Collider = other,
            Player = GetComponent<PlayerController>(),
            Powerups = GetComponent<PlayerPowerups>(),
        });
    }
}
