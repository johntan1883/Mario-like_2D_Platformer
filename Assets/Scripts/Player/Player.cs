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

    [Header("Dead")]
    [SerializeField] private PlayerDeathAnimation playerDeathAnimation;

    [Header("SFX")]
    [SerializeField] private AudioClip jumpSFX;
    [SerializeField] private AudioClip dieSFX;

    private Rigidbody2D rb;
    private new Collider2D collider;
    private GameOverScreen gameOverScreen;
    private float horizontal;
    private float currentSpeed;
    private bool isFacingRight = true;
    private bool isWalking;
    private bool isJumping;
    private bool isSprinting;
    private bool isDead;

    // Properties to check player state
    public bool IsWalking => isWalking;
    public bool IsJumping => isJumping;
    public bool IsDead => isDead;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();

        gameOverScreen = FindAnyObjectByType<GameOverScreen>();
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

            SoundFXManager.Instance.PlaySoundFXClip(jumpSFX, transform, 1f, "SFX");
        }
    }

    public void PlayerDie()
    {
        isDead = true;

        if (isDead)
        {
            playerDeathAnimation.enabled = true;
            GetComponent<BoxCollider2D>().enabled = false;
            StopMovement();
            SoundFXManager.Instance.StopBackgroundMusic();
            SoundFXManager.Instance.PlaySoundFXClip(dieSFX, transform, 1f, "SFX");
            gameOverScreen.GameOver();

        }
    }

    private void StopMovement()
    {
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;
    }

    private void OnEnable()
    {
        rb.isKinematic = false;
        collider.enabled = true;
        rb.velocity = Vector2.zero;
        isJumping = false;
    }

    private void OnDisable()
    {
        rb.isKinematic = true;
        collider.enabled = false;
        rb.velocity = Vector2.zero;
        isJumping = false;
    }

    //Visualize the casting box
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position - transform.up * castDistance, boxSize);
    }
}
