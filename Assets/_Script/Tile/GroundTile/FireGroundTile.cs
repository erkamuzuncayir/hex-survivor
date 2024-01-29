using UnityEngine;

namespace _Script.Tile
{
    public class FireGroundTile : GroundTileData
    {
        public FireGroundTile(int dictIndex, Vector3 worldPosition, Vector3Int coord, TileType typeOfTile, bool isPopulated) : base(dictIndex, worldPosition, coord, typeOfTile, isPopulated)
        {
        }
    }
}