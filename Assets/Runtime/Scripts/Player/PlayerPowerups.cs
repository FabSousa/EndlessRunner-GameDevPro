using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPowerups : MonoBehaviour
{
    [Header("PowerupX2")]
    [SerializeField] private GameObject powerupX2Particle;
    [SerializeField][Min(0)] private float x2Duration;
    private int x2Instances = 0;
    private const float ScoreMultplier = 2;
    private const float BaseScoreMultplier = 1;

    [Header("Star")]
    [SerializeField] private GameObject starParticle;
    [SerializeField][Min(0)] private float starDuration;
    private int starInstances = 0;

    [Header("Magnet")]
    [SerializeField] private GameObject magnetParticle;
    [SerializeField][Min(0)] private float magnetDuration;
    private int magnetInstances = 0;


    public void PowerupX2(CollisionInfo collisionInfo)
    {
        StartCoroutine(ActivatePowerupX2(collisionInfo));
    }

    private IEnumerator ActivatePowerupX2(CollisionInfo collisionInfo)
    {
        powerupX2Particle.SetActive(true);
        GameMode.ScoreMultiplier = ScoreMultplier;

        x2Instances++;
        yield return new WaitForSeconds(x2Duration);
        x2Instances--;

        if (x2Instances == 0)
        {
            powerupX2Particle.SetActive(false);
            GameMode.ScoreMultiplier = BaseScoreMultplier;
        }
    }


    public void PowerupStar(CollisionInfo collisionInfo)
    {
        StartCoroutine(ActivatePowerupStar(collisionInfo));
    }

    private IEnumerator ActivatePowerupStar(CollisionInfo collisionInfo)
    {
        starParticle.SetActive(true);
        collisionInfo.GameMode.CanDie = false;

        starInstances++;
        yield return new WaitForSeconds(starDuration);
        starInstances--;

        if (starInstances == 0)
        {
            starParticle.SetActive(false);
            collisionInfo.GameMode.CanDie = true;
        }
    }


    public void PowerupMagnet(CollisionInfo collisionInfo)
    {
        StartCoroutine(ActivatePowerupMagnet(collisionInfo));
    }

    private IEnumerator ActivatePowerupMagnet(CollisionInfo collisionInfo)
    {
        magnetParticle.SetActive(true);
        Debug.Log("Magnet activate"); //Activate

        magnetInstances++;
        yield return new WaitForSeconds(magnetDuration);
        magnetInstances--;

        if (magnetInstances == 0)
        {
            magnetParticle.SetActive(false);
            Debug.Log("Magnet deactivate"); //Deactivate
        }
    }
}
