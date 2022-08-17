using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupStar : Pickups
{
    protected override void OnPickedUp(in CollisionInfo collisionInfo)
    {
        PlayerPowerups playerPowerups = collisionInfo.Powerups;
        playerPowerups.PowerupStar(collisionInfo);
    }
}
