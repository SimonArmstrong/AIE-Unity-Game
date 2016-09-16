using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Map : MonoBehaviour {
    public Rect texRect;
    public float amplitude = 10;
    public float scale = 1.0f;
	public float rotation = 0f;
    private Color[] pixel;
	[HideInInspector]
	public static List<GameObject> tiles = new List<GameObject>();
    public int seed;

	public static GameObject GetTileFromPosition(Vector3 position) {
		for (int i = 0; i < tiles.Count; i++) {
			if (tiles [i].transform.position == position) {
				return tiles[i];
			}
		}
		return null;
    }

    [Serializable]
    public struct Table {
        public float cols;
        public float rows;
    }

    public GameObject tile;
    public Table table;

    public void PlaceTiles()
    {
        //seed = UnityEngine.Random.Range(0, 65534);
        if (tiles.Count > 0) {
            for (int i = 0; i < tiles.Count; i++) {
                DestroyImmediate(tiles[i]);
            }
            tiles.Clear();
        }

        float unitScaling = ((table.rows / 2) - (tile.transform.localScale.x / 2));
        float xCoord = 0;
        float yCoord = 0;
        float sample = 0;
        float sample2 = 0;
        float sample3 = 0;

        for (int x = 0; x < texRect.width; x++) {
            for (int y = 0; y < texRect.height; y++) {
                xCoord = texRect.x + x / texRect.width * scale + seed + amplitude;
                yCoord = texRect.y + y / texRect.height * scale + seed + amplitude;

                sample = Mathf.PerlinNoise(xCoord, yCoord) * amplitude;
                sample2 = Mathf.PerlinNoise(xCoord / 4, yCoord / 4) * 40;
                sample3 = Mathf.PerlinNoise(xCoord / 10, yCoord / 10) * 40;
                sample += sample2 + sample3;
                for (int i = 0; i < 3; i++) {
					
                    tiles.Add(Instantiate(tile, new Vector3(transform.position.x + 1 * x, i - (int)sample, transform.position.z + 1 * y), Quaternion.identity) as GameObject);
                }
            }
        }
    }

    // Use this for initialization
    void Start () {
        //PlaceTiles();
    }
	
	// Update is called once per frame
	void Update () {
        //PhysicsSkewedCall
        if (Input.GetKeyUp(KeyCode.F1))
        {
            PlaceTiles();
        }
	}
}
