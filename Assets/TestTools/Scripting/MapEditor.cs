using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[CustomEditor (typeof(Map))]
public class MapEditor : Editor {
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Map mapper = (Map)target;
        if (GUILayout.Button("Build Map")) mapper.PlaceTiles();
        if (GUILayout.Button("Clear Tiles")) {
            GameObject[] tiles = GameObject.FindGameObjectsWithTag("tile");
            for (int i = 0; i < tiles.Length; i++) {
                Map.tiles.Clear();
                DestroyImmediate(tiles[i]);
            }
        }
    }
}
