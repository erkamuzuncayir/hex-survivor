using UnityEngine;

public class WaterTile : TileData
{
    public WaterTile(Vector3Int coord, bool isPopulated) : base(coord, isPopulated)
    {
        TypeOfTile = TileType.Water;
    }
}
