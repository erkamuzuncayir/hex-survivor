using _Script.MapGeneration;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MapGenerator))]
public class MapGeneratorEditor : UnityEditor.Editor
{
    public override void OnInspectorGUI()
    {
        MapGenerator mapGen = (MapGenerator)target;
        
        if (DrawDefaultInspector())
        {
            if(mapGen.AutoUpdate)
                mapGen.GenerateMap();
        }
        
        if(GUILayout.Button("Generate"))
            mapGen.GenerateMap();
    }
}