using System;
using UnityEngine;

namespace _Script.Tile
{
    [CreateAssetMenu(fileName = "so_TileDictionary", menuName = "Data/Tile Dictionary")]
    public class TileDictionarySO : ScriptableObject
    {
        public TileKeyValuePair[] GroundTiles;

        public void InitDictionary(int tileCount)
        {
            GroundTiles = new TileKeyValuePair[tileCount];
        }

        public GroundTileData GetTileData(int index, Vector3Int coord)
        {
            if (GroundTiles[index].Coord == coord)
                return GroundTiles[index].GroundTileData;
            
            Debug.LogError($"there is no tile on {coord}!");
            return new EmptyGroundTile(-1, Vector3.zero, Vector3Int.zero, TileType.Empty, false);
        }

        
        public GroundTileData GetTileData(Vector3Int coord)
        {
            for (int i = 0; i < GroundTiles.Length; i++)
                if (GroundTiles[i].Coord == coord)
                    return GroundTiles[i].GroundTileData;
            
            Debug.LogError($"there is no tile on {coord}!");
            return new EmptyGroundTile(-1, Vector3.zero, Vector3Int.zero, TileType.Empty, false);
        }
    }
    
    [Serializable]
    public struct TileKeyValuePair
    {
        public int DictIndex;
        public Vector3Int Coord;
        public GroundTileData GroundTileData;

        public TileKeyValuePair(int dictIndex, Vector3Int coord, GroundTileData groundTileData)
        {
            DictIndex = dictIndex;
            Coord = coord;
            GroundTileData = groundTileData;
        }
    }
}