using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyBall : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool gravityReversed = false; // Tracks the current gravity state

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component
        Physics2D.gravity = new Vector2(0, -9.81f); // Set default gravity
    }

    void Update()
    {
        // Press G key to toggle gravity direction
        if (Input.GetKeyDown(KeyCode.G))
        {
            gravityReversed = !gravityReversed;

            // Change global gravity
            Physics2D.gravity = gravityReversed ? new Vector2(0, 9.81f) : new Vector2(0, -9.81f);

            // Reverse the ball's velocity instantly to reflect gravity change
            rb.velocity = new Vector2(rb.velocity.x, -rb.velocity.y);
        }
    }
}

