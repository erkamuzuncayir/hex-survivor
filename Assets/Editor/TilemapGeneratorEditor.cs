using _Script.MapGeneration;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(ProceduralTilemapGenerator))]
    public class TilemapGeneratorEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            ProceduralTilemapGenerator proceduralTilemapGenerator = (ProceduralTilemapGenerator)target;
        
            if (DrawDefaultInspector())
            {
                if(proceduralTilemapGenerator.AutoUpdate)
                    proceduralTilemapGenerator.GenerateTilemap();
            }
        
            if(GUILayout.Button("Generate Tilemap"))
                proceduralTilemapGenerator.GenerateTilemap();
        }
    }
}