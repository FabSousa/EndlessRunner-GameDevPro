using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct CollisionInfo
{
    public Collider Collider;
    public GameMode GameMode;
    public PlayerPowerups Powerups;
}

public interface ICollide
{
    void Collide(in CollisionInfo collisionInfo);
}