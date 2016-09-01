using UnityEngine;
using System;
using System.Collections;

public class Tiler : MonoBehaviour {
    public Rect texRect;
    public float scale = 1.0f;
    private Color[] pixel;


    [Serializable]
    public struct Table {
        public float cols;
        public float rows;
    }

    public GameObject tile;
    public Table table;

    private void PlaceTiles()
    {
        float unitScaling = ((table.rows / 2) - (tile.transform.localScale.x / 2));
        float xCoord = 0;
        float yCoord = 0;
        float sample = 0;
        float sample2 = 0;

        for (int x = 0; x < texRect.width; x++)
        {
            for (int y = 0; y < texRect.height; y++)
            {
                xCoord = texRect.x + x / texRect.width * scale;
                yCoord = texRect.y + y / texRect.height * scale;

                sample = Mathf.PerlinNoise(xCoord, yCoord) * 10;
                sample2 = Mathf.PerlinNoise(xCoord / 4, yCoord / 4) * 40;
                for (int i = 0; i < 3; i++)
                {
                    Instantiate(tile, new Vector3(1 * x, i + (int)sample + (int)sample2, 1 * y), Quaternion.identity);
                }
            }
        }
    }

    // Use this for initialization
    void Start () {
        PlaceTiles();
	}
	
	// Update is called once per frame
	void Update () {
        //PhysicsSkewedCall
        
	}
}
