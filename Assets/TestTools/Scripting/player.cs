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
    //Returns the top most point of the map
    private Vector3 GetTopSurface() {
        RaycastHit hit;
        Ray ray = new Ray(new Vector3(transform.position.x, transform.position.y + 10, transform.position.z), Vector3.down);
        if (Physics.Raycast(ray, out hit, 10))
        {
            if(hit.collider.tag == "tile") {
                return hit.point;
            }
        }
        return Vector3.zero;
    }

    private void UnifyPosition() {
        
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
        Debug.DrawLine(GetTopSurface(), transform.position, Color.red);
        // Begin stepTimer countdown
        if (stepTimer >= 0f) stepTimer -= Time.deltaTime;
	}
}
