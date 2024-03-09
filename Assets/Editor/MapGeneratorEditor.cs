using _Script.MapGeneration;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MapGenerator))]
public class MapGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        MapGenerator mapGen = (MapGenerator)target;
        
        if (DrawDefaultInspector())
        {
            Debug.Log("hey");
            if(mapGen.AutoUpdate)
                mapGen.GenerateMap();
        }
        
        if(GUILayout.Button("Generate"))
            mapGen.GenerateMap();
    }
}