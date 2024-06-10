using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class Snail : MonoBehaviour, IEnemy
{

    [SerializeField] private float movingSpeed;
    [SerializeField] private GameObject pointA;
    [SerializeField] private GameObject pointB;

    private Rigidbody2D rb;
    private Transform currentPoint;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        //Initial destination
        currentPoint = pointA.transform;
    }

    private void Update()
    {
        MoveTowardPoint();

        SwitchPoint();
    }

    public void SwitchPoint()
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

    //Visualize the patrol path of the enemy
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pointA.transform.position, 0.5f);
        Gizmos.DrawWireSphere(pointB.transform.position, 0.5f);

        Gizmos.DrawLine(pointA.transform.position, pointB.transform.position);
    }
}   
