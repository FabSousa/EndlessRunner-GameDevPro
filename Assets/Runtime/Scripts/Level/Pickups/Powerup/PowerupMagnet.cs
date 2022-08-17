using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupMagnet : Pickups
{
    protected override void OnPickedUp(in CollisionInfo collisionInfo)
    {
        PlayerPowerups playerPowerups = collisionInfo.Powerups;
        playerPowerups.PowerupMagnet(collisionInfo);
    }
}
