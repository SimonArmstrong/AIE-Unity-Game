using UnityEngine;
using System.Collections;

public class follow : MonoBehaviour {

    public GameObject ToFollow;
    public float distanceToPlayer = 10f;
    public GameObject idlePoint;
    

	// Use this for initialization
	
	// Update is called once per frame
	void Update () {
		transform.LookAt (ToFollow.transform);

        //Draw ray to player
        Ray ray = new Ray(idlePoint.transform.position, ToFollow.transform.position);
        RaycastHit hit;

        // If the ray hits something that isn't a player...
        if (Physics.Raycast(ray, out hit)) {
            if (hit.collider.tag != "Player") {
                Debug.DrawLine(ray.origin, ray.direction * ToFollow.transform.position.magnitude, Color.green);
            }
        }
	}
}
