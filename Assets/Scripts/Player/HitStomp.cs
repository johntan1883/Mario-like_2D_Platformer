using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitStomp : MonoBehaviour
{
    [SerializeField] private float bounceForce;
    [SerializeField] private Rigidbody2D playerRb;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            playerRb.velocity = new Vector2(playerRb.velocity.x, bounceForce);
        }
    }
}
