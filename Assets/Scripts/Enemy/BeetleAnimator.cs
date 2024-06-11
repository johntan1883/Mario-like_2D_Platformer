using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeetleAnimator : MonoBehaviour
{
    private const string IS_WALKING = "IsWalking";

    [SerializeField] private Beetle beetle;
    [SerializeField] private GameObject beetleGameObject;
    [SerializeField] private float delay;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        animator.SetBool(IS_WALKING, beetle.IsWalking);

        if (beetle.IsWalking == false) 
        {
            Destroy(beetleGameObject, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length + delay);
        }
    }
}
