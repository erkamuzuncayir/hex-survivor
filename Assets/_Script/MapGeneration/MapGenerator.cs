using System;
using NaughtyAttributes;
using UnityEngine;

namespace _Script.MapGeneration
{
    public class MapGenerator : MonoBehaviour
    {
        [SerializeField] private MapDisplay _display;
        public int MapWidth;
        public int MapHeight;
        public float NoiseScale;

        public int octaves;
        [Range(0,1)]
        public float persistance;
        public float lacunarity;

        public int seed;
        public Vector2 offset;
        
        public bool AutoUpdate;
        
        
        [Button()]
        public void GenerateMap()
        {
            float[,] noiseMap = Noise.GenerateNoiseMap(MapWidth, MapHeight, seed, NoiseScale, octaves, persistance, lacunarity, offset);

            _display.DrawNoiseMap(noiseMap);
        }

        private void OnValidate()
        {
            if (MapWidth < 1)
                MapWidth = 1;
            
            if (MapHeight < 1)
                MapHeight = 1;

            if (lacunarity < 1)
                lacunarity = 1;

            if (octaves < 0)
                octaves = 0;
        }
    }
}