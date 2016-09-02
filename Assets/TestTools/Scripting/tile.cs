using UnityEngine;
using System.Collections;

public class tile : MonoBehaviour {

    public bool on = false;
    private Material onCol;
    public Material offCol;

    void Start() {
        GetComponent<Renderer>().material = offCol;        
    }

    void OnCollisionEnter(Collision other)
    {
        // player0, player1, player2, etc...
        if (other.collider.tag == ("Player")) {
            on = true;
            onCol = other.gameObject.GetComponent<player>().teamMaterial;
            GetComponent<Renderer>().material = onCol;
        }
    }
}
