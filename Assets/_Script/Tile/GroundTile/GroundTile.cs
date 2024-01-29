using UnityEngine;

namespace _Script.Tile
{
    [CreateAssetMenu(fileName = "GroundTile", menuName = "Data/Scriptable Tiles/Custom Tile")]
    public class GroundTile : UnityEngine.Tilemaps.Tile
    {
        public TileType Type;
    }
}