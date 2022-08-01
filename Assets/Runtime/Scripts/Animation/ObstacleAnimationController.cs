using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleAnimationController : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        //TODO: String pura
        animator.SetTrigger("Death");
    }
}
