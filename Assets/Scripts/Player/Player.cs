using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float movingSpeed;
    [SerializeField] private float jumpForce = 5f;
    private bool isJumping;

    [Header("GroundCheck")]
    [SerializeField] private Vector2 boxSize;
    [SerializeField] private float castDistance;
    [SerializeField] private LayerMask groundLayer;
    //private bool isGrounded;

    private Rigidbody2D rb;
    private float horizontal;
    private bool isFacingRight = true;
    private bool isWalking;

    //To check the player condition
    public bool IsWalking()
    {
        return isWalking;
    }

    public bool IsJumping()
    {
        return isJumping;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        Flip();

        GroundCheck();
        Jump();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * movingSpeed, rb.velocity.y);
        isWalking = Mathf.Abs(horizontal) > 0.01f;
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f) 
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private void GroundCheck()
    {
        if (Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, castDistance, groundLayer))
        {
            //isGrounded = true;
            isJumping = false;
            Debug.Log("Player is grounded");
        }

        else
        {
            isJumping = true;
        }
    }
    
    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            rb.AddForce(new Vector2(rb.velocity.x, jumpForce));
        }
    }

    //Visualize the casting box
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position - transform.up * castDistance, boxSize);
    }
}
