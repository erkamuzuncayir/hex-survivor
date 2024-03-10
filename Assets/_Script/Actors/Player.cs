using _Script.PersonalAPI.Data.RuntimeSet;
using _Script.Tile;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace _Script.Actors
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private PlayerDataSO _so_playerData;
        [SerializeField] private TileDictionarySO _so_tileDictionary;
        [SerializeField] private GameObjectRuntimeSet _so_rs_tilemap_ground;
        private Tilemap _tilemap_ground;
        
        private void Start()
        {
            _tilemap_ground = _so_rs_tilemap_ground.Items[0].GetComponent<Tilemap>();
            Vector3 position = transform.position;
            _so_playerData.PlayerCoord = _tilemap_ground.WorldToCell(position);
            SetPlayerTileDictIndex();
        }
        
        private void SetPlayerTileDictIndex()
        {
            GroundTileData tileUnderPlayer = _so_tileDictionary.GetTileData(_so_playerData.PlayerCoord);
            _so_playerData.TileUnderThePlayer = tileUnderPlayer;
            _so_playerData.PlayerTileDictIndex = tileUnderPlayer.DictIndex;
        }

    }
}
