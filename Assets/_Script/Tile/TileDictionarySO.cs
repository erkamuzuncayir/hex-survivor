using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Script.Tile
{
    [CreateAssetMenu(fileName = "so_TileDictionary", menuName = "Data/Tile Dictionary")]
    public class TileDictionarySO : ScriptableObject
    {
        public List<TileKeyValuePair> GroundTiles = new List<TileKeyValuePair>();
    }

    [Serializable]
    public struct TileKeyValuePair
    {
        public Vector3Int Coord;
        public GroundTileData GroundTileData;

        public TileKeyValuePair(Vector3Int coord, GroundTileData groundTileData)
        {
            Coord = coord;
            GroundTileData = groundTileData;
        }
    }
}