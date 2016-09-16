using UnityEngine;
using System.Collections;

public class tile : MonoBehaviour {

    public bool on = false;
    private Material onCol;
    public Material offCol;
    public static LayerMask mask = 31;

    void Start() {
        GetComponent<Renderer>().material = offCol;
        gameObject.layer = mask;
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Yo");
        // player0, player1, player2, etc...
        if (other.tag == ("Player")) {
            on = true;
            onCol = other.gameObject.GetComponent<player>().teamMaterial;
            GetComponent<Renderer>().material = onCol;
        }
    }
}
