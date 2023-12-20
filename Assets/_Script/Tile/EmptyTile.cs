using _Script.Tile;
using UnityEngine;

public class EmptyTile : TileData
{
    public EmptyTile(Vector3Int coord, TileType typeOfTile, bool isPopulated) : base(coord, typeOfTile, isPopulated)
    {
    }
}