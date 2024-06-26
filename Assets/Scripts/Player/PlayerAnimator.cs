using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private const string IS_WALKING = "IsWalking";
    private const string IS_JUMPING = "IsJumping";
    private const string IS_DEAD = "IsDead";

    [SerializeField] private Player player;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        //Toggle the walking animation
        animator.SetBool(IS_WALKING, player.IsWalking);

        //Toggle the jump animation
        if (player.IsJumping == true)
        {
            animator.SetBool(IS_JUMPING, player.IsJumping);
        }
        else
        {
            animator.SetBool(IS_JUMPING, player.IsJumping);
        }

        animator.SetBool(IS_DEAD, player.IsDead);
    }
}
