using UnityEngine;

public class BuildingTile : TileData
{
    public BuildingTile(Vector3Int coord, bool isPopulated) : base(coord, isPopulated)
    {
        TypeOfTile = TileType.Building;
    }
}