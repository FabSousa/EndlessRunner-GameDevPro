using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleAnimationController : ObstacleDecoration
{
    [SerializeField] private Animator animator;

    public override void PlayCollisionFeedback()
    {
        base.PlayCollisionFeedback();
        //TODO: String pura
        animator.SetTrigger("Death");
    }

}
