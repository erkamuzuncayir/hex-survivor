using System.Collections.Generic;
using _Script.Tile;
using UnityEngine;

namespace _Script.System
{
    [CreateAssetMenu(fileName = "so_system_Pathfinding", menuName = "System/Pathfinding System")]
    public class PathfindingSystemSO : ScriptableObject
    {
        [SerializeField] private TileDictionarySO _so_tileDictionary;
        private HashSet<GroundTileData> _toSearchNeighbors = new HashSet<GroundTileData>();
        private HashSet<GroundTileData> _processedNeighbors = new HashSet<GroundTileData>();
        private HashSet<GroundTileData> _alternativeTargets = new HashSet<GroundTileData>();
        private HashSet<GroundTileData> _toSearch = new HashSet<GroundTileData>();
        private HashSet<GroundTileData> _processed = new HashSet<GroundTileData>();
        private List<GroundTileData> _path = new List<GroundTileData>();
        
        private GroundTileData _startTile;
        private GroundTileData _targetTile;
        private bool _isAlternateTargetSearchStarted = false;
        
        public (bool isPathFound, List<GroundTileData> path) FindPath(int startTileDictIndex, int targetTileDictIndex, bool excludeActors)
        {
            _startTile = _so_tileDictionary.GroundTiles[startTileDictIndex].GroundTileData;
            _targetTile = _so_tileDictionary.GroundTiles[targetTileDictIndex].GroundTileData;
            List<GroundTileData> path = SearchPath(_startTile, _targetTile, excludeActors);

            if (path == null)
            {
                _toSearchNeighbors.Clear();
                _toSearchNeighbors.Add(_targetTile);
                _processedNeighbors.Clear();
                _alternativeTargets.Clear();
                foreach (var neighbor in _targetTile.Neighbors)
                    _alternativeTargets.Add(neighbor);
                GroundTileData alternateTarget;

                while (path == null)
                {
                    if (_alternativeTargets.Count < 1)
                    {
                        foreach (var neighbor in _processedNeighbors)
                            foreach (var n in neighbor.Neighbors)
                                _alternativeTargets.Add(n);
                        _processedNeighbors.Clear();
                    }

                    GroundTileData nearestNeighbor = null;
                    foreach (var neighbor in _alternativeTargets)
                    {
                        if (nearestNeighbor == null || neighbor.FValue < nearestNeighbor.FValue)
                            nearestNeighbor = neighbor;
                    }

                    alternateTarget = nearestNeighbor;
                    _toSearchNeighbors.Add(nearestNeighbor);
                    _processedNeighbors.Add(nearestNeighbor);
                    _alternativeTargets.Remove(nearestNeighbor);
                    path = SearchPath(_startTile, alternateTarget, excludeActors);
                }
            }

            path.Reverse();
            return (true, path);
        }

        public int DistanceCheckToPlayer(int startTileDictIndex, int targetTileDictIndex, bool excludeActors)
        {
            _startTile = _so_tileDictionary.GroundTiles[startTileDictIndex].GroundTileData;
            _targetTile = _so_tileDictionary.GroundTiles[targetTileDictIndex].GroundTileData;
            _path.Clear();
            _path = SearchPath(_startTile, _targetTile, excludeActors);

            if (_path == null)
            {
                _toSearchNeighbors.Clear();
                _toSearchNeighbors.Add(_targetTile);
                _processedNeighbors.Clear();
                _alternativeTargets.Clear();
                foreach (var neighbor in _targetTile.Neighbors)
                    _alternativeTargets.Add(neighbor);
                GroundTileData alternateTarget;

                while (_path == null)
                {
                    if (_alternativeTargets.Count < 1)
                    {
                        foreach (var neighbor in _processedNeighbors)
                            foreach (var n in neighbor.Neighbors)
                                _alternativeTargets.Add(n);
                        _processedNeighbors.Clear();
                    }

                    GroundTileData nearestNeighbor = null;
                    foreach (var neighbor in _alternativeTargets)
                    {
                        if (nearestNeighbor == null || neighbor.FValue < nearestNeighbor.FValue)
                            nearestNeighbor = neighbor;
                    }

                    alternateTarget = nearestNeighbor;
                    _toSearchNeighbors.Add(nearestNeighbor);
                    _processedNeighbors.Add(nearestNeighbor);
                    _alternativeTargets.Remove(nearestNeighbor);
                    _path = SearchPath(_startTile, alternateTarget, excludeActors);
                }
            }

            _path.Reverse();
            return _path.Count;
        }        
        
        private List<GroundTileData> SearchPath(GroundTileData startTile, GroundTileData targetTile, bool excludeActors)
        {
            _toSearch.Clear();
            _toSearch.Add(startTile);
            _processed.Clear();

            while (_toSearch.Count > 0)
            {
                GroundTileData current = null;
                foreach (var t in _toSearch)
                {
                    if (current == null || t.FValue < current.FValue || (t.FValue == current.FValue && t.HValue < current.HValue))
                        current = t;
                }

                _processed.Add(current);
                _toSearch.Remove(current);

                if (current == targetTile)
                {
                    GroundTileData currentPathTile = targetTile;
                    List<GroundTileData> path = new List<GroundTileData>();
                    while (currentPathTile != startTile)
                    {
                        path.Add(currentPathTile);
                        currentPathTile = currentPathTile.Connection;
                    }

                    return path;
                }

                foreach (GroundTileData neighbor in current.Neighbors)
                {
                    if(excludeActors)
                    {
                        if (neighbor.ThisIsOnIt == WhatIsOnIt.Obstacle || _processed.Contains(neighbor)) 
                            continue;
                    }
                    else
                    {
                        if (neighbor.ThisIsOnIt != WhatIsOnIt.Nothing || _processed.Contains(neighbor)) 
                            continue;
                    }
                        

                    bool inSearch = _toSearch.Contains(neighbor);
                    int costToNeighbor = current.GValue + GetManhattanDistance(current.Coord, neighbor.Coord);

                    if (inSearch && costToNeighbor >= neighbor.GValue) continue;
                    
                    neighbor.SetGValue(costToNeighbor);
                    neighbor.SetConnection(current);

                    if (inSearch) continue;
                    
                    neighbor.SetHValue(GetManhattanDistance(neighbor.Coord, targetTile.Coord));

                    _toSearch.Add(neighbor);
                }
            }
            
            return null;
        }
        
        private int GetManhattanDistance(Vector3Int coord1, Vector3Int coord2)
        {
            return Mathf.Abs(coord1.x - coord2.x) + Mathf.Abs(coord1.y - coord2.y);
        }
    }
}
