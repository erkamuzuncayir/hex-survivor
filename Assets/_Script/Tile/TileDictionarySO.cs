using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Script.Tile
{
    [CreateAssetMenu(fileName = "so_TileDictionary", menuName = "Data/Tile Dictionary")]
    public class TileDictionarySO : ScriptableObject
    {
        public List<TileKeyValuePair> BaseTiles = new List<TileKeyValuePair>();
    }

    [Serializable]
    public struct TileKeyValuePair
    {
        public Vector3Int Coord;
        public TileData TileData;
    }
}