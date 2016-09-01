using UnityEngine;
using System.Collections;

public class player : MonoBehaviour {

    public bool AUTOJUMP;

    public float speed;
    public float jumpForce = 1.5f;
    public float maxJumpTimer = 1.5f;
    private bool grounded = false;
    private Rigidbody r;
    private float jumpTimer = 1.5f;

    // Use this for initialization
    void Start () {
        r = GetComponent<Rigidbody>();
	}

    // Update is called once per frame
    void Update() {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
        transform.position += movement * speed * Time.deltaTime;

        // Timer for jumping. -- Defines how long before the player's character hops
        // If we've been grounded for [jumpTimer] seconds
        if (Input.GetButtonDown("Jump"))
        {
            if (grounded)
            {
                if (jumpTimer <= 0f)
                {
                    jumpTimer = maxJumpTimer;
                    // Perform Jumping action
                    r.AddForce(Vector3.up * jumpForce);
                    // Play hop animation
                    // Send this location across network
                }
            }
        }
        if (jumpTimer >= 0f) jumpTimer -= Time.deltaTime;
        if (r.velocity.y <= 0.005f) grounded = true;
	}
}
