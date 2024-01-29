using System.Collections.Generic;
using _Script.Actors;
using _Script.Data;
using _Script.System;
using _Script.System.StateSystem.State.PlayerState;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using NotImplementedException = System.NotImplementedException;

namespace _Script.Tile
{
    public class GroundTilemapHover : MonoBehaviour
    {
        // Input Handler
        private InputAction _ia_mousePosition;
        [SerializeField] private HoverInputHandler _hoverInputHandler;

        // Tilemap
        [SerializeField] private Tilemap _baseTilemap;
        [SerializeField] private TileDictionarySO _so_tileDictionary;

        // Player
        [SerializeField] private PlayerDataSO _so_playerData;
        [SerializeField] private PlayerStateSO _so_state_player_selected;
        [SerializeField] private PlayerStateSOReference _so_state_player_current;

        // Pathfinding
        [SerializeField] private PathfindingSystemSO _so_system_pathfinding;
        private List<GroundTileData> _highlightedPath = new();
        private int _lastTileDictIndex = -1;
    
        // TODO: Add Cursor texture onto hover path
        [SerializeField] 
        
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
            if (_so_state_player_current.Value != _so_state_player_selected)
                return;
        
            int dictIndex = FindSelectedTileDictIndex(inputWorldPos);

            if (dictIndex != -1 && dictIndex != _lastTileDictIndex)
            {
                PredictPathForPlayer(dictIndex);
                _lastTileDictIndex = dictIndex;
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

            for (int i = 0; i < moveCount; i++)
            {
                GroundTileData groundTileData = _highlightedPath[i];
                _baseTilemap.GetTile<GroundTile>(groundTileData.Coord).color = Color.red;
                Debug.Log("redd");
            }
            _baseTilemap.RefreshAllTiles();
        }

        private void OnHoverCanceled()
        {
            
        }

        [Button]
        public void ClearTileColor()
        {
            foreach (TileKeyValuePair tileKeyValuePair in _so_tileDictionary.GroundTiles)
            {
                _baseTilemap.GetTile<GroundTile>(tileKeyValuePair.Coord).color = Color.white;
            }
            Debug.Log("Cleared");
        }
    }
}
