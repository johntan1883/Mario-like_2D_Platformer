using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beetle : MonoBehaviour, IEnemy
{
    [SerializeField] private float movingSpeed;
    [SerializeField] private GameObject pointA;
    [SerializeField] private GameObject pointB;
    [SerializeField] private Rigidbody2D playerRigidBody;
    [SerializeField] private float HitStompForce;

    [Header("Collison Check")]
    [SerializeField] private float hitBoxCastDistance;
    [SerializeField] private float killBoxCastDistance;
    [SerializeField] private LayerMask hitLayer;
    [SerializeField] private Vector2 hitBoxSize;
    [SerializeField] private Vector2 killBoxSize;

    [Header("Player Reference")]
    [SerializeField] private Player player;

    private Rigidbody2D rb;
    private Transform currentPoint;
    private bool isWalking = true;

    // Properties to check enemy state
    public bool IsWalking => isWalking;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        //Initial destination
        currentPoint = pointA.transform;
    }

    private void Update()
    {
        if (isWalking)
        {
            MoveTowardPoint();
        }
        SwitchPoint();

        PlayerCollisionCheck();
    }

    public void SwitchPoint()
    {
        if (isWalking)
        {
            if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointB.transform)
            {
                Flip();
                Debug.Log("Reach point B");
                currentPoint = pointA.transform;
            }

            if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointA.transform)
            {
                Flip();
                Debug.Log("Reach point A");
                currentPoint = pointB.transform;
            }
        }
    }

    public void MoveTowardPoint()
    {
        Vector2 point = currentPoint.position - transform.position;

        if (currentPoint == pointB.transform)
        {
            rb.velocity = new Vector2(movingSpeed, 0f);
        }
        else
        {
            rb.velocity = new Vector2(-movingSpeed, 0f);
        }
    }

    public void Flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    private void PlayerCollisionCheck()
    {
        RaycastHit2D topHit = Physics2D.BoxCast(transform.position, hitBoxSize, 0, -transform.up, hitBoxCastDistance, hitLayer);
        if (topHit)
        {
            isWalking = false;
            HitStomp();
        }

        // Check collision with player to the left of the beetle
        Collider2D leftHit = Physics2D.OverlapBox(transform.position + Vector3.left * killBoxCastDistance, killBoxSize, 0, hitLayer);
        if (leftHit)
        {
            Debug.Log("Player Die");
            player.PlayerDie();
        }

        // Check collision with player to the right of the beetle
        Collider2D rightHit = Physics2D.OverlapBox(transform.position + Vector3.right * killBoxCastDistance, killBoxSize, 0, hitLayer);
        if (rightHit)
        {
            Debug.Log("Player Die");
            player.PlayerDie();
        }
    }

    private void HitStomp()
    {
        playerRigidBody.velocity = new Vector2(playerRigidBody.velocity.x, HitStompForce);
    }

    //Visualize the patrol path of the enemy
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pointA.transform.position, 0.5f);
        Gizmos.DrawWireSphere(pointB.transform.position, 0.5f);

        Gizmos.DrawLine(pointA.transform.position, pointB.transform.position);

        Gizmos.DrawWireCube(transform.position - transform.up * hitBoxCastDistance, hitBoxSize);

        // Visualize the left and right overlap boxes
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + Vector3.left * killBoxCastDistance, killBoxSize);
        Gizmos.DrawWireCube(transform.position + Vector3.right * killBoxCastDistance, killBoxSize);
    }
}

    

