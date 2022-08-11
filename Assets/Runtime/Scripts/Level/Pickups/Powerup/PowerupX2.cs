using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupX2 : Pickups
{
    [SerializeField][Min(0)] private float powerupDuration;
    private const float ScoreMultplier = 2;
    private const float BaseScoreMultplier = 1;

    protected override void OnPickedUp(in CollisionInfo collisionInfo)
    { 
        StartCoroutine(ActivatePowerup(collisionInfo));
    }

    private IEnumerator ActivatePowerup(CollisionInfo collisionInfo)
    {
        GameMode.ScoreMultiplier = ScoreMultplier;
        collisionInfo.PowerupX2Particle.SetActive(true);
        yield return new WaitForSeconds(powerupDuration);
        GameMode.ScoreMultiplier = BaseScoreMultplier;
        collisionInfo.PowerupX2Particle.SetActive(false);
    }

    protected override void DestroyPickup()
    {
        Destroy(gameObject, powerupDuration+1);
    }
}
