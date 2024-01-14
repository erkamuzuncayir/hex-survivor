using System;
using System.Collections.Generic;
using _Script.Actors;
using _Script.PersonalAPI.Event;
using _Script.PersonalAPI.Input;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace _Script.Tile
{
    public class TilemapManager : MonoBehaviour
    {
        // Input Handler
        [SerializeField] private ClickInputHandler _clickInputHandler;

        // Tilemap Data
        [SerializeField] private Tilemap _baseTilemap;
        [SerializeField] private TileDictionarySO _so_tileDictionary;

        // Events
        [SerializeField] private IntEventSO _so_event_SelectedTileDictIndex;

        [SerializeField] private PlayerDataSO _playerDataSO;
        
        private void Awake()
        {
            BoundsInt baseTilemapBounds = _baseTilemap.cellBounds;
            float tilemapSize = GetTilemapSize(baseTilemapBounds);
            _so_tileDictionary.InitDictionary((int)tilemapSize);
            GetAllTiles(baseTilemapBounds);
            SetNeighbors();
        }

        private void OnEnable()
        {
            _clickInputHandler.OnClickPerformed += OnMouseClickPerformed;
        }

        private void OnDisable()
        {
            _clickInputHandler.OnClickPerformed -= OnMouseClickPerformed;
        }

        private void Start()
        {
            SetPlayerTileDictIndex();
        }
        
        // TODO: Maybe move this into Player.cs
        private void SetPlayerTileDictIndex()
        {
            GroundTileData tileUnderPlayer = _so_tileDictionary.GetTileData(_playerDataSO.PlayerCoord);
            _playerDataSO.TileUnderThePlayer = tileUnderPlayer;
            _playerDataSO.PlayerTileDictIndex = tileUnderPlayer.DictIndex;
        }

        private float GetTilemapSize(BoundsInt tilemapBounds)
        {
            float horSize = tilemapBounds.xMax - tilemapBounds.xMin;
            float verSize = tilemapBounds.yMax - tilemapBounds.yMin;
            return horSize * verSize;
        }

        private void GetAllTiles(BoundsInt tilemapBounds)
        {
            int tileDictIndex = 0;
            // loop over the bounds (from min to max) on both axes
            for (int x = tilemapBounds.min.x; x < tilemapBounds.max.x; x++)
            for (int y = tilemapBounds.min.y; y < tilemapBounds.max.y; y++)
            {
                Vector3Int cellPosition = new(x, y, 0);

                GroundTile tile = _baseTilemap.GetTile<GroundTile>(cellPosition);
                if (tile == null) continue;
                
                Vector3Int coord = new(x, y, 0);
                Vector3 tileWorldPos = _baseTilemap.CellToWorld(coord);
                
                bool isTilePopulated = tile.Type is TileType.Building or TileType.Water;
                
                _so_tileDictionary.GroundTiles[tileDictIndex] = new TileKeyValuePair(tileDictIndex, coord,
                    new GroundTileData(tileDictIndex, tileWorldPos, coord, tile.Type, isTilePopulated));
                tileDictIndex++;
            }

            Array.Resize(ref _so_tileDictionary.GroundTiles, tileDictIndex);
        }

        private void SetNeighbors()
        {
            for (int i = 0; i < _so_tileDictionary.GroundTiles.Length; i++)
            {
                TileKeyValuePair groundTile = _so_tileDictionary.GroundTiles[i];;
                List<GroundTileData> neighbors = new();
                List<Vector3Int> neighborPositions = GetNeighborPositions(groundTile.Coord);

                for (int k = 0; k < _so_tileDictionary.GroundTiles.Length; k++)
                {
                    TileKeyValuePair otherTile = _so_tileDictionary.GroundTiles[k];
                    if (neighborPositions.Contains(otherTile.Coord))
                    {
                        neighbors.Add(otherTile.GroundTileData);
                    }
                }

                groundTile.GroundTileData.Neighbors = neighbors;
            }
        }

        public List<Vector3Int> GetNeighborPositions(Vector3Int tileCoord)
        {
            List<Vector3Int> neighborPositions = new();

            if (tileCoord.y % 2 == 0)
            {
                neighborPositions.Add(new Vector3Int(tileCoord.x - 1, tileCoord.y + 1, 0));
                neighborPositions.Add(new Vector3Int(tileCoord.x, tileCoord.y + 1, 0));
                neighborPositions.Add(new Vector3Int(tileCoord.x - 1, tileCoord.y, 0));
                neighborPositions.Add(new Vector3Int(tileCoord.x + 1, tileCoord.y, 0));
                neighborPositions.Add(new Vector3Int(tileCoord.x - 1, tileCoord.y - 1, 0));
                neighborPositions.Add(new Vector3Int(tileCoord.x, tileCoord.y - 1, 0));
            }
            else
            {
                neighborPositions.Add(new Vector3Int(tileCoord.x, tileCoord.y + 1, 0));
                neighborPositions.Add(new Vector3Int(tileCoord.x + 1, tileCoord.y + 1, 0));
                neighborPositions.Add(new Vector3Int(tileCoord.x - 1, tileCoord.y, 0));
                neighborPositions.Add(new Vector3Int(tileCoord.x + 1, tileCoord.y, 0));
                neighborPositions.Add(new Vector3Int(tileCoord.x, tileCoord.y - 1, 0));
                neighborPositions.Add(new Vector3Int(tileCoord.x + 1, tileCoord.y - 1, 0));
            }

            return neighborPositions;
        }

        private void OnMouseClickPerformed(Vector2 inputWorldPos)
        {
            Vector3Int destinationCoord = _baseTilemap.WorldToCell(inputWorldPos);
            foreach (TileKeyValuePair groundTile in _so_tileDictionary.GroundTiles)
            {
                if(groundTile.Coord == destinationCoord)
                    _so_event_SelectedTileDictIndex.Raise(groundTile.DictIndex);
            }
        }
    }
}