using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    private Player player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") == true)
        {
            player = collision.GetComponent<Player>();
            player.PlayerDie();
        }
        else
        {
            Destroy(collision.gameObject);
        }
    }
}
