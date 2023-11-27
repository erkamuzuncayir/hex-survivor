using UnityEngine;

public class GrassTile : TileData
{
    public GrassTile(Vector3Int coord, bool isPopulated) : base(coord, isPopulated)
    {
        TypeOfTile = TileType.Grass;
        Debug.Log(TypeOfTile);
    }
}