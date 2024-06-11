using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float movingSpeed;
    [SerializeField] private float sprintSpeed;
    [SerializeField] private float jumpForce = 5f;
    

    [Header("GroundCheck")]
    [SerializeField] private Vector2 boxSize;
    [SerializeField] private float castDistance;
    [SerializeField] private LayerMask groundLayer;
    //private bool isGrounded;

    private Rigidbody2D rb;
    private float horizontal;
    private float currentSpeed;
    private bool isFacingRight = true;
    private bool isWalking;
    private bool isJumping;
    private bool isSprinting;

    // Properties to check player state
    public bool IsWalking => isWalking;
    public bool IsJumping => isJumping;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Input();
        Flip();
        Jump();
    }

    private void Input()
    {
        horizontal = UnityEngine.Input.GetAxisRaw("Horizontal");

        isSprinting = UnityEngine.Input.GetKey(KeyCode.LeftShift);
    }

    private void FixedUpdate()
    {
        Sprint();
        GroundCheck();
        
        isWalking = Mathf.Abs(horizontal) > 0.01f;
    }

    private void Sprint()
    {
        if (!isJumping)
        {
            currentSpeed = isSprinting ? sprintSpeed : movingSpeed;
        }

        rb.velocity = new Vector2(horizontal * currentSpeed, rb.velocity.y);
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
        if (UnityEngine.Input.GetButtonDown("Jump") && !isJumping)
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
