using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "CustomTile", menuName = "Scriptable Tiles/Custom Tile")]
public class CustomTile : Tile
{
    public TileType Type;
}