using UnityEngine;

namespace _Script.Tile
{
    public class EmptyGroundTile : GroundTileData
    {
        public EmptyGroundTile(int dictIndex, Vector3 worldPosition, Vector3Int coord, TileType typeOfTile, bool isPopulated) : base(dictIndex, worldPosition, coord, typeOfTile, isPopulated)
        {
        }
    }
}