using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Let's make an array that contains Transforms
    [SerializeField]
    private Transform[] waypoints;
    // Target position Vector3 for the enemy to move towards
    private Vector2 targetPosition;

    [SerializeField]
    [Range(0, 1f)]
    private float moveSpeed;
    private int waypointIndex;

    private Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();

        // Target position is assigned the value of 0th index of waypoints-array.
        targetPosition = waypoints[0].position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, .5f * moveSpeed);

        // Distance method measures the distance between a and b
        if (Vector2.Distance(transform.position, targetPosition) < .25f)
        {
            if (waypointIndex >= waypoints.Length - 1)
            {
                waypointIndex = 0;

            }
            else
            {
                waypointIndex += 1;

            }

            targetPosition = waypoints[waypointIndex].position;

        }


    }
}
