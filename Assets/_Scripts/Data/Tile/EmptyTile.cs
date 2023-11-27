using UnityEngine;

public class EmptyTile : TileData
{
    public EmptyTile(Vector3Int coord, bool isPopulated) : base(coord, isPopulated)
    {
        TypeOfTile = TileType.Empty;
    }
}