using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAi : MonoBehaviour
{
    public Transform target;

    [Header("Ai movement")]
    public float speed = 200f;
    public float nextWaypointDistance = 3f;
    public Transform enemyGFX;

    [Header("Ai detection")]
    public float detectionRadius = 10f;
    private bool isPlayerDetected = false;

    Path path;
    int currentWaypoint = 0;

    Seeker seeker;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {

        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, .5f);
        seeker.StartPath(rb.position, target.position, OnPathComplete);
    }

    void UpdatePath()
    {
        if (seeker.IsDone())
            seeker.StartPath(rb.position, target.position, OnPathComplete);
    }

    void  OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, target.position) <= detectionRadius)
        {
            isPlayerDetected = true;
        } else
        {
            isPlayerDetected = false;
        }

        if (isPlayerDetected)
        {
            if (path == null)
                return;

            Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
            Vector2 force = direction * speed * Time.deltaTime;

            rb.AddForce(force);

            float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
            if (distance < nextWaypointDistance)
            {
                currentWaypoint++;
            }

            if (rb.velocity.x >= 0.01f && force.x > 0f)
            {
                enemyGFX.localScale = new Vector3(-1f, 1f, 1f);
            }
            else if (rb.velocity.x <= -0.01f && force.x < 0f)
            {
                enemyGFX.localScale = new Vector3(1f, 1f, 1f);
            }

        }
               
    }
    private void OnDrawGizmosSelected()
    {
        // Draws a sphere around the enemy to represent the detection radius
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
