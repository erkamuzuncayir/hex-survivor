using UnityEngine;

namespace _Script.Tile
{
    [CreateAssetMenu(fileName = "CustomTile", menuName = "Data/Scriptable Tiles/Custom Tile")]
    public class CustomTile : UnityEngine.Tilemaps.Tile
    {
        public TileType Type;
    }
}