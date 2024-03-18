using System;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace _Script.MapGeneration
{
    public class ProceduralTilemapGenerator : MonoBehaviour
    {
        [SerializeField] private Tilemap _groundTilemap;
        [SerializeField] private GroundTileThresholdPair[] _groundTileThresholdPairs;
        private TileBase[] _groundTilesToBePlaced;

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

        [Button]
        public void GenerateTilemap()
        {
            noiseMap = Noise.GenerateNoiseMap(MapWidth, MapHeight, Seed, NoiseScale, Octaves, Persistance, Lacunarity,
                Offset);

            FillTilemap();
        }

        private void FillTilemap()
        {
            int width = noiseMap.GetLength(0);
            int height = noiseMap.GetLength(1);
            _groundTilesToBePlaced = new TileBase[width * height];
            for (int y = 0; y < height; y++)
            for (int x = 0; x < width; x++)
                _groundTilesToBePlaced[y * width + x] = SetTileBasedOnHeight(noiseMap[x, y]);
            BoundsInt tilemapBound = new(0, 0, 0, width, height, 1);

            _groundTilemap.ClearAllTiles();
            _groundTilemap.SetTilesBlock(tilemapBound, _groundTilesToBePlaced);
            _groundTilemap.RefreshAllTiles();
        }

        private TileBase SetTileBasedOnHeight(float perlinValue)
        {
            Array.Sort(_groundTileThresholdPairs, (x, y) => x.Threshold.CompareTo(y.Threshold));

            foreach (GroundTileThresholdPair pair in _groundTileThresholdPairs)
                if (pair.Threshold > perlinValue)
                    return pair.Tile;

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