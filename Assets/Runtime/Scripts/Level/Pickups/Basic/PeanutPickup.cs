using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeanutPickup : Pickups
{
    protected override void OnPickedUp(in CollisionInfo collisionInfo)
    {
        GameMode.PeanutCount++;
    }
}
