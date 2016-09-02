using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class player : MonoBehaviour {

    public bool AUTOJUMP;

    public Material teamMaterial;

    public float speed;
    public float jumpForce = 1.5f;
    public bool grounded = false;
    private Rigidbody r;
    private float jumpTimer = 1.5f;
    public Vector3 direction;

    // Use this for initialization
    void Start () {
        r = GetComponent<Rigidbody>();
        jumpTimer = speed;
    }

    // Update is called once per frame
    void FixedUpdate() {

        if (Input.GetButtonDown("Horizontal")) {
            direction = new Vector3((int)Input.GetAxisRaw("Horizontal"), 0, 0);
        }
        if (Input.GetButtonDown("Vertical"))
        {
            direction = new Vector3(0, 0, (int)Input.GetAxisRaw("Vertical"));
        }

        if (Mathf.Abs(r.velocity.y) <= 0.005f) grounded = true; else { grounded = false; }
        if (Input.GetButtonDown("Jump") || AUTOJUMP)
        {
            if (grounded)
            {
                if (jumpTimer <= 0f)
                {
                    jumpTimer = speed;
                    transform.Translate(direction);
                }
            }
        }
        if (jumpTimer >= 0f) jumpTimer -= Time.deltaTime;

        /*
        //Check if we are falling or not; sets our grounded to true if we aren't

        if (!grounded) {
            Vector3 movement = new Vector3(1, 0, 0);
            transform.Translate(movement * Time.deltaTime);
        }

        // Timer for jumping. -- Defines how long before the player's character hops
        // If we've been grounded for [jumpTimer] seconds
        */
    }
}
