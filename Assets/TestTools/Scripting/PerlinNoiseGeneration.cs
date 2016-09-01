using UnityEngine;
using System.Collections;

public class PerlinNoiseGeneration : MonoBehaviour {
    public Rect texRect;
    public float scale = 1.0f;
    private Texture2D perlinTex;
    private Color[] pixel;
    private Renderer rend;

    private void Start() {
        rend = GetComponent<Renderer>();
        perlinTex = new Texture2D((int)texRect.width, (int)texRect.height);
        pixel = new Color[perlinTex.width * perlinTex.height];
        rend.material.mainTexture = perlinTex;
    }

    private void PlaceTile(float heightOffset) {
    }

    private void GenerateNoise() {
        for (int x = 0; x < perlinTex.width; x++)
        {
            for (int y = 0; y < perlinTex.height; y++)
            {
                float xCoord = texRect.x + x / perlinTex.width * scale;
                float yCoord = texRect.y + y / perlinTex.height * scale;
                float sample = Mathf.PerlinNoise(xCoord, yCoord);
            }
        }
    }

    private void Update() {
        GenerateNoise();
    }
}
