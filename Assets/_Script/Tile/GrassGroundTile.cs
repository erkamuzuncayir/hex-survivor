using _Script.Tile;
using UnityEngine;

public class GrassGroundTile : GroundTileData
{
    public GrassGroundTile(Vector3Int coord, TileType typeOfTile, bool isPopulated) : base(coord, typeOfTile, isPopulated)
    {
    }
}