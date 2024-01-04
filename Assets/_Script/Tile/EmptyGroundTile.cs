using _Script.Tile;
using UnityEngine;

public class EmptyGroundTile : GroundTileData
{
    public EmptyGroundTile(Vector3Int coord, TileType typeOfTile, bool isPopulated) : base(coord, typeOfTile, isPopulated)
    {
    }
}