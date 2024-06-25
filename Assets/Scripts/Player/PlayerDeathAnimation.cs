using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathAnimation : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Player player;

    private void Reset()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        UpdateSprite();
        DisablePhysics();
        StartCoroutine(Animate());
    }

    private void UpdateSprite()
    {
        spriteRenderer.enabled = true;
        spriteRenderer.sortingOrder = 10;
    }

    private void DisablePhysics()
    {
        Collider2D [] colliders = GetComponents<Collider2D>();
        
        foreach (Collider2D collider in colliders)
        {
            collider.enabled = false;
        }

        player.GetComponent<Rigidbody2D>().isKinematic = true;

        if (player != null)
        {
            player.enabled = false;
        }
    }

    private IEnumerator Animate()
    {
        float elapsed = 0f;
        float duration = 3f;

        float jumpVelocity = 10f;
        float gravity = -30f;

        Vector3 velocity = Vector3.up * jumpVelocity;

        while (elapsed < duration)
        {
            transform.position += velocity * Time.deltaTime;
            velocity.y += gravity * Time.deltaTime;
            elapsed += Time.deltaTime;
            yield return null;
        }
    }
}
