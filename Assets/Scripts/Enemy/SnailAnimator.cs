using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailAnimator : MonoBehaviour
{
    private const string IS_WALKING = "IsWalking";

    [SerializeField] private Snail snail;
    [SerializeField] private GameObject snailGameObject;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        animator.SetBool(IS_WALKING, snail.IsWalking);
    }
}
