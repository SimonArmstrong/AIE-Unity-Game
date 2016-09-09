using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class player : MonoBehaviour {

	public Material teamMaterial;

	public float stepDelay;
	public float transitionSpeed;
	public bool PauseStepping = false;
    public int playerNumber;
    private float stepTimer = 1.5f;
    private bool grounded = false;
    private float jumpTimer = 1.5f;
    private Rigidbody r;
	private Vector3 newLocation;
	private Vector3 direction;
    private Manager.PlayerSettings playerSettings;
	private int currentSong = 0;

    // Use this for initialization
    void Start () {
        r = GetComponent<Rigidbody>();
		jumpTimer = stepDelay;
        playerNumber = Manager.players.Count;
        Manager.players.Add(this);
        playerSettings = Manager.playerSettings[playerNumber];
    }

    public void LoadPlayerSettings() {
        teamMaterial = playerSettings.teamMat;
    }

	private void MovementInput() {
		if(Mathf.Abs(Input.GetAxisRaw("Horizontal")) >= 0.1f) {
			direction = new Vector3((int)Input.GetAxisRaw("Horizontal"), 0, 0);

            AnimationTriggerManager.OnDirectionChange(direction);
		}
		if (Mathf.Abs (Input.GetAxisRaw ("Vertical")) >= 0.1f) {
			direction = new Vector3(0, 0, (int)Input.GetAxisRaw("Vertical"));

            AnimationTriggerManager.OnDirectionChange(direction);
        }
	}

    private GameObject GetTopSurface() {
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(new Ray(transform.position, Vector3.down * 100), out hit)) {
            if(hit.collider.tag == "tile")
            {
                return hit.collider.gameObject;
            }
        }
        return new GameObject();
    }

    // Update is called once per frame
    void FixedUpdate() {
		MovementInput ();
		//Checks if we are falling; sets grounded to true if we're not falling
        if (Mathf.Abs(r.velocity.y) <= 0.005f) grounded = true; else { grounded = false; }
        if (!PauseStepping) {
            if (grounded) {
                AnimationTriggerManager.OnLand();
                //When the step timer is up
                if (stepTimer <= 0f) {
					// Reset stepTimer length
					stepTimer = stepDelay;
                    // Sets the new point in which we want to move to...
                    newLocation = transform.position + direction;

                    AnimationTriggerManager.OnStep();
                }
				//if (Map.GetTileFromPosition (newLocation) == null)
                {
                    // Moves the player to newLocation. Applies x10 scaling to transition speed for simpler tweaking
                    transform.position = Vector3.Lerp (transform.position, newLocation, (transitionSpeed * 10) * Time.deltaTime);
                    //transform.position = new Vector3();

                }
            } else { AnimationTriggerManager.OnFall(); }
        }
		Debug.DrawLine (transform.position, newLocation);
		// Begin stepTimer countdown
		if (stepTimer >= 0f) stepTimer -= Time.deltaTime;
	}
}
