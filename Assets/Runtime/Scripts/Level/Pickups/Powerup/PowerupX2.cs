using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupX2 : Pickups
{
    protected override void OnPickedUp(in CollisionInfo collisionInfo)
    {
        PlayerPowerups playerPowerups = collisionInfo.Powerups;
        playerPowerups.PowerupX2(collisionInfo);
    }   
}
