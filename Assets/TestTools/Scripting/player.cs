// AUTHOR : Simon Armstrong
// DATED  : 9/16/2016

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class player : MonoBehaviour {

	public Material teamMaterial;

    public bool LERP = false;

	public float stepDelay;
	public float transitionSpeed;
	public bool PauseStepping = false;
    public int playerNumber;
    private float stepTimer = 1.5f;
    public bool grounded = false;
    private float jumpTimer = 1.5f;
    private Rigidbody r;
	private Vector3 newLocation;
	private Vector3 direction;
    private Manager.PlayerSettings playerSettings;
	private int currentSong = 0;

    // Returns the top most point of the map surface
    private Vector3 GetTopSurface()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, Vector3.down);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider != null && hit.collider.tag == "tile") { return hit.point; }
        }
        return transform.position;
    }

    // Use this for initialization
    void Start () {
        r = GetComponent<Rigidbody>();
		jumpTimer = stepDelay;
        Manager.players.Add(this);
        playerNumber = Manager.players.Count - 1;
        try { playerSettings = Manager.playerSettings[playerNumber]; }
        catch { Debug.Log("Failed to load Player " + playerNumber + "'s Settings. Reconfigure settings in previous scenes."); }
        newLocation = GetTopSurface();
        LoadPlayerSettings();
    }

    public void LoadPlayerSettings() {
        if (playerSettings.teamMat == null)
        {
            Debug.Log("Failed to find Team Material");
            return;
        }
        else {
            teamMaterial = playerSettings.teamMat;
        }
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


    private void UnifyPosition() {
        float x = transform.position.x, y = transform.position.z;
        Mathf.Round(x);
        Mathf.Round(y);

        if (LERP) transform.position = Vector3.Lerp(transform.position, newLocation, (transitionSpeed * 10) * Time.deltaTime);
        else { transform.position = newLocation; }
    }

    // FixedUpdate is called once per frame after physics calls
    void FixedUpdate() {
		MovementInput ();
        Ray moveRay = new Ray(transform.position, direction);
        RaycastHit colDetect;
        //Checks if we are falling; sets grounded to true if we're not falling
        //if (Mathf.Abs(r.velocity.y) <= 0.005f) grounded = true; else { grounded = false; }

        Ray fallRay = new Ray(transform.position, Vector3.down);
        RaycastHit fallCheck;
        grounded = Physics.Raycast(fallRay, out fallCheck, 0.2f) && fallCheck.collider.tag != null;        

        if (!PauseStepping) {
            if (grounded) {
                AnimationTriggerManager.OnLand();
                //When the step timer is up
                if (stepTimer <= 0f) {
                    // Reset stepTimer length
                    stepTimer = stepDelay;
                    // Sets the new point in which we want to move to...
                    newLocation = transform.position + direction;
                    if (Physics.Raycast(moveRay, out colDetect, 1.0f))
                    {
                        if (colDetect.collider != null)
                        {
                            newLocation.Set(newLocation.x, newLocation.y + 1, newLocation.z);
                        }
                    }
                    AnimationTriggerManager.OnStep();
                }
            } else { AnimationTriggerManager.OnFall(); }
        }
        if (!grounded) newLocation.Set(transform.position.x, GetTopSurface().y, transform.position.z);
        //Debug.DrawLine (transform.position, newLocation);
        //Debug.DrawLine(GetTopSurface(), transform.position, Color.red);
        // Begin stepTimer countdown
        if (stepTimer >= 0f) stepTimer -= Time.deltaTime;
        UnifyPosition();
    }
}
