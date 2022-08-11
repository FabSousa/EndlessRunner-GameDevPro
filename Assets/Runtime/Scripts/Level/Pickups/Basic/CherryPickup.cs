using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CherryPickup : Pickups
{
    protected override void OnPickedUp(in CollisionInfo collisionInfo)
    {
        GameMode.CherryCount++;
    }
}

