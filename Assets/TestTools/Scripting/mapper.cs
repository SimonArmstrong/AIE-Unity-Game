using UnityEngine;
using System.Collections;

public class mapper : MonoBehaviour {
    public GameObject chunk;

    void Start() {
        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 2; j++)
            {
				Instantiate(chunk, new Vector3(i * chunk.GetComponent<Map>().texRect.width, 0, j * chunk.GetComponent<Map>().texRect.height), Quaternion.identity);
            }
        }
    }
}
