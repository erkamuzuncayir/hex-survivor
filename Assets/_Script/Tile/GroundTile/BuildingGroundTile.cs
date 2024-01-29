using UnityEngine;

namespace _Script.Tile
{
    public class BuildingGroundTile : GroundTileData
    {
        public BuildingGroundTile(int dictIndex, Vector3 worldPosition, Vector3Int coord, TileType typeOfTile, bool isPopulated) : base(dictIndex, worldPosition, coord, typeOfTile, isPopulated)
        {
        }
    }
}