using _Script.Tile;
using UnityEngine;

public class WaterGroundTile : GroundTileData
{
    public WaterGroundTile(Vector3Int coord, TileType typeOfTile, bool isPopulated) : base(coord, typeOfTile, isPopulated)
    {
    }
}