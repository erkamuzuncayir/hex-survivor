using UnityEngine;

public class FireTile : TileData
{
    public FireTile(Vector3Int coord, bool isPopulated) : base(coord, isPopulated)
    {
        TypeOfTile = TileType.Fire;
    }
}