using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

namespace _Script.MapGeneration
{
    public class ProceduralTilemapGenerator : MonoBehaviour
    {
        [SerializeField] private Tilemap _groundTilemap;
        [SerializeField] private TileBase _waterTile;
        [SerializeField] private TileBase _beachTile;
        [SerializeField] private TileBase _grassTile;
        [SerializeField] private TileBase _mountainTile;
        
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
                    _groundTilemap.SetTile(new Vector3Int(x,y,1), SetTileBasedOnHeight(x,y));
                }
            }
            
        }

        private TileBase SetTileBasedOnHeight(int width, int height)
        {
            if(noiseMap[width, height] < 0.2f)
            {
                return _waterTile;
            }
            else if(noiseMap[width, height] < 0.4f)
            {
                return _beachTile;
            }

            else if(noiseMap[width, height] < 0.8f)
            {
                return _grassTile;
            }
            else
            {
                return _mountainTile;
            }
        }
    }
}
