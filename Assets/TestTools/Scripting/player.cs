using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class player : MonoBehaviour {

	public Material teamMaterial;

	public float stepDelay;
	public float transitionSpeed;
	public bool PauseStepping = false;
    private float stepTimer = 1.5f;
    private bool grounded = false;
    private Rigidbody r;
    private float jumpTimer = 1.5f;
	public Vector3 direction;
	private Vector3 newLocation;
	private int currentSong = 0;

    // Use this for initialization
    void Start () {
        r = GetComponent<Rigidbody>();
		jumpTimer = stepDelay;
    }

	private void MovementInput() {
		if(Mathf.Abs(Input.GetAxisRaw("Horizontal")) >= 0.1f) {
			direction = new Vector3((int)Input.GetAxisRaw("Horizontal"), 0, 0);
			newLocation = transform.position + direction;
		}
		if (Mathf.Abs (Input.GetAxisRaw ("Vertical")) >= 0.1f) {
			direction = new Vector3(0, 0, (int)Input.GetAxisRaw("Vertical"));
			newLocation = transform.position + direction;
		}
	}

    // Update is called once per frame
    void Update() {
		MovementInput ();
		//Checks if we are falling; sets grounded to true if we're not falling
        if (Mathf.Abs(r.velocity.y) <= 0.005f) grounded = true; else { grounded = false; }
        if (!PauseStepping) {
            if (grounded) {
				//When the step timer is up
				if (stepTimer <= 0f) {
					// Reset stepTimer length
					stepTimer = stepDelay;
					// Sets the new point in which we want to move to...

                }
				if (Map.GetTileFromPosition (newLocation) == null) {
					// Moves the player to newLocation. Applies x10 scaling to transition speed for simpler tweaking
					transform.position = Vector3.Lerp (transform.position, newLocation, (transitionSpeed * 10) * Time.deltaTime);
				}
            }
        }
		Debug.DrawLine (transform.position, newLocation);
		// Begin stepTimer countdown
		if (stepTimer >= 0f) stepTimer -= Time.deltaTime;
	}
}
