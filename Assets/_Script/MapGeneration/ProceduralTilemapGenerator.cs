using System;
using _Script.Tile;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

namespace _Script.MapGeneration
{
    public class ProceduralTilemapGenerator : MonoBehaviour
    {
        [SerializeField] private Tilemap _groundTilemap;
        [SerializeField] private GroundTileThresholdPair[] _groundTileThresholdPairs;
        
        private float[,] noiseMap;
        
        public int MapWidth;
        public int MapHeight;
        public float NoiseScale;
        
        public int Octaves;
        public float Persistance;
        public float Lacunarity;

        public int Seed;
        public Vector2 Offset;

        public bool AutoUpdate;
        
        [Button()]
        public void GenerateTilemap()
        {
            noiseMap = Noise.GenerateNoiseMap(MapWidth, MapHeight, Seed, NoiseScale, Octaves, Persistance, Lacunarity, Offset);

            FillTilemap();
        }

        private void FillTilemap()
        {
            int width = noiseMap.GetLength(0);
            int height = noiseMap.GetLength(1);
            _groundTilemap.ClearAllTiles();
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    _groundTilemap.SetTile(new Vector3Int(x,y,0), SetTileBasedOnHeight(noiseMap[x,y]));
                }
            }
            _groundTilemap.RefreshAllTiles();
            
        }

        private TileBase SetTileBasedOnHeight(float perlinValue)    
        {
            Array.Sort(_groundTileThresholdPairs, (x, y) => x.Threshold.CompareTo(y.Threshold));

            foreach (GroundTileThresholdPair pair in _groundTileThresholdPairs)
            {
                if (pair.Threshold > perlinValue)
                    return pair.Tile;
            }

            return null;
        }
    }

    [Serializable]
    public class GroundTileThresholdPair
    {
        public TileBase Tile;
        public float Threshold;
    }
}
