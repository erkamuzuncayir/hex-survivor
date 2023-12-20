using _Script.Tile;
using UnityEngine;

public class WaterTile : TileData
{
    public WaterTile(Vector3Int coord, TileType typeOfTile, bool isPopulated) : base(coord, typeOfTile, isPopulated)
    {
    }
}