using _Script.Tile;
using UnityEngine;

public class BuildingGroundTile : GroundTileData
{
    public BuildingGroundTile(Vector3Int coord, TileType typeOfTile, bool isPopulated) : base(coord, typeOfTile, isPopulated)
    {
    }
}