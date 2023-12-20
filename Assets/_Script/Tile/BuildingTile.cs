using _Script.Tile;
using UnityEngine;

public class BuildingTile : TileData
{
    public BuildingTile(Vector3Int coord, TileType typeOfTile, bool isPopulated) : base(coord, typeOfTile, isPopulated)
    {
    }
}