using System.Collections.Generic;
using _Script.Actors;
using _Script.Data;
using _Script.System;
using _Script.System.StateSystem.State.PlayerState;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Pool;
using UnityEngine.Tilemaps;

namespace _Script.Tile
{
    public class GroundTilemapHover : MonoBehaviour
    {
        // Pool
        private ObjectPool<GameObject> _pool_tileHoverOverlay_piece;
        [SerializeField] private GameObject _tileHoverOverlay_piece;
        private const bool _COLLECTION_CHECK = true;
        [SerializeField] private int _tileHoverOverlayDefaultCount;
        [SerializeField] private int _tileHoverOverlayMaxCount;
        private List<GameObject> _instantiatedTileHoverOverlays = new();
        // Last overlay piece
        [SerializeField] private GameObject _tileHoverOverlay_piece_last;
        
        // Input Handler
        private InputAction _ia_mousePosition;
        [SerializeField] private HoverInputHandler _hoverInputHandler;
        
        // Tilemap
        [SerializeField] private Tilemap _baseTilemap;
        [SerializeField] private TileDictionarySO _so_tileDictionary;

        // Player
        [SerializeField] private PlayerDataSO _so_playerData;
        [SerializeField] private PlayerStateSO _so_state_player_selected;
        [SerializeField] private PlayerStateSO _so_state_player_move;
        [SerializeField] private PlayerStateSOReference _so_state_player_current;

        // Pathfinding
        [SerializeField] private PathfindingSystemSO _so_system_pathfinding;
        private List<GroundTileData> _highlightedPath = new();
        private int _lastTileDictIndex = -1;

        // Cache fields
        private GroundTileData _cacheGroundTileData;
        
        private void Awake()
        {
            CreateTileHoverOverlayPool();
        }

        private void CreateTileHoverOverlayPool()
        {
            _tileHoverOverlayDefaultCount = _so_playerData.MaxMoveCount;
            _tileHoverOverlayMaxCount = _so_playerData.MaxMoveCount * 5 /*TODO: add max move count in game settings */;
            _pool_tileHoverOverlay_piece = new ObjectPool<GameObject>(CreateTileHoverOverlay, OnGetTileHoverOverlayFromPool,
                OnReturnTileHoverOverlayToPool, OnDestroyTileHoverOverlay, _COLLECTION_CHECK,
                _tileHoverOverlayDefaultCount, _tileHoverOverlayMaxCount);
        }

        private GameObject CreateTileHoverOverlay()
        {
            return Instantiate(_tileHoverOverlay_piece);
        }

        private void OnEnable()
        {
            _hoverInputHandler.OnHoverPerformed += OnHoverPerformed;
            _hoverInputHandler.OnHoverCanceled += OnHoverCanceled;
        }

        private void OnDisable()
        {
            _hoverInputHandler.OnHoverPerformed -= OnHoverPerformed;
            _hoverInputHandler.OnHoverCanceled -= OnHoverCanceled;
        }
        
        private void OnHoverPerformed(Vector2 inputWorldPos)
        {
            if (_so_state_player_current.Value == _so_state_player_selected)
            {
                int dictIndex = FindSelectedTileDictIndex(inputWorldPos);

                if (dictIndex != -1 && dictIndex != _lastTileDictIndex)
                {
                    if (_lastTileDictIndex != -1)
                        RemoveOldHoverPath();
                    PredictPathForPlayer(dictIndex);
                    _lastTileDictIndex = dictIndex;
                }
            }
        }

        private int FindSelectedTileDictIndex(Vector2 inputWorldPos)
        {
            Vector3Int destinationCoord = _baseTilemap.WorldToCell(inputWorldPos);
            foreach (TileKeyValuePair groundTile in _so_tileDictionary.GroundTiles)
                if (groundTile.Coord == destinationCoord)
                    return groundTile.DictIndex;

            return -1;
        }

        private void PredictPathForPlayer(int targetTileDictIndex)
        {
            _highlightedPath =
                _so_system_pathfinding.FindPath(_so_playerData.PlayerTileDictIndex, targetTileDictIndex);

            int moveCount = _highlightedPath.Count;
            if (_highlightedPath.Count > _so_playerData.RemainingMoveCount)
                moveCount = _so_playerData.RemainingMoveCount;

            if (moveCount < 1) 
                return;
            
            // -1 Due to place last piece different colored.
            for (int i = 0; i < moveCount - 1; i++)
            {
                _cacheGroundTileData = _highlightedPath[i];
                _pool_tileHoverOverlay_piece.Get(out GameObject tileHoverOverlay);
                tileHoverOverlay.transform.position = _baseTilemap.GetCellCenterWorld(_cacheGroundTileData.Coord);
                _instantiatedTileHoverOverlays.Add(tileHoverOverlay);
            }
            _cacheGroundTileData = _highlightedPath[moveCount - 1];
            _tileHoverOverlay_piece_last.transform.position =
                _baseTilemap.GetCellCenterWorld(_cacheGroundTileData.Coord);
            _tileHoverOverlay_piece_last.SetActive(true);
            _baseTilemap.RefreshAllTiles();
        }

        private void OnGetTileHoverOverlayFromPool(GameObject obj)
        {
            obj.SetActive(true);
        }

        private void OnReturnTileHoverOverlayToPool(GameObject obj)
        {
            obj.SetActive(false);
        }

        private void OnDestroyTileHoverOverlay(GameObject obj)
        {
            Destroy(obj);
        }

        public void RemovePassedTileHoverOverlay()
        {
            if (_instantiatedTileHoverOverlays.Count > 0)
            {
                _pool_tileHoverOverlay_piece.Release(_instantiatedTileHoverOverlays[0]);
                _instantiatedTileHoverOverlays.RemoveAt(0);
            }
        }
        
        private void RemoveOldHoverPath()
        {
            for (int i = _instantiatedTileHoverOverlays.Count - 1; i >= 0; i--)
            {
                GameObject instantiated = _instantiatedTileHoverOverlays[i];
                _pool_tileHoverOverlay_piece.Release(instantiated);
                _instantiatedTileHoverOverlays.Remove(instantiated);
            }
            _tileHoverOverlay_piece_last.SetActive(false);
        }

        public void RemoveOldHoverPath(PlayerStateSO currentPlayerStateSO)
        {
            if (currentPlayerStateSO == _so_state_player_selected || currentPlayerStateSO == _so_state_player_move) 
                return;
            for (int i = _instantiatedTileHoverOverlays.Count - 1; i >= 0; i--)
            {
                GameObject instantiated = _instantiatedTileHoverOverlays[i];
                _pool_tileHoverOverlay_piece.Release(instantiated);
                _instantiatedTileHoverOverlays.Remove(instantiated);
            }
            _tileHoverOverlay_piece_last.SetActive(false);
        }

        private void OnHoverCanceled()
        {
        }
    }
}