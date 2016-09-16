using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor (typeof(Map))]
public class MapEditor : Editor {
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Map mapper = (Map)target;
        if (GUILayout.Button("Build Map")) mapper.PlaceTiles();
    }
}
