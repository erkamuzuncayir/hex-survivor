using System.Collections.Generic;
using _Script.Tile;
using UnityEngine;

namespace _Script.System
{
    [CreateAssetMenu(fileName = "so_system_Pathfinding", menuName = "System/Pathfinding System")]
    public class PathfindingSystemSO : ScriptableObject
    {
        [SerializeField] private TileDictionarySO _so_tileDictionary;
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
                List<GroundTileData> toSearchNeighbors = new() { _targetTile };
                List<GroundTileData> processedNeighbors = new();
                List<GroundTileData> alternativeTargets = new();
                alternativeTargets.AddRange(_targetTile.Neighbors);
                GroundTileData alternateTarget;

                while (path == null)
                {
                    if (alternativeTargets.Count < 1)
                    {
                        alternativeTargets.AddRange(processedNeighbors[0].Neighbors);
                        processedNeighbors.RemoveAt(0);
                    }

                    GroundTileData nearestNeighbor = alternativeTargets[0];
                    for (int i = 1; i < alternativeTargets.Count; i++)
                        if (alternativeTargets[i].FValue < nearestNeighbor.FValue)
                            nearestNeighbor = alternativeTargets[i];

                    alternateTarget = nearestNeighbor;
                    toSearchNeighbors.Add(nearestNeighbor);
                    processedNeighbors.Add(nearestNeighbor);
                    alternativeTargets.Remove(nearestNeighbor);
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
            List<GroundTileData> path = SearchPath(_startTile, _targetTile, excludeActors);

            if (path == null)
            {
                List<GroundTileData> toSearchNeighbors = new() { _targetTile };
                List<GroundTileData> processedNeighbors = new();
                List<GroundTileData> alternativeTargets = new();
                alternativeTargets.AddRange(_targetTile.Neighbors);
                GroundTileData alternateTarget;

                while (path == null)
                {
                    if (alternativeTargets.Count < 1)
                    {
                        alternativeTargets.AddRange(processedNeighbors[0].Neighbors);
                        processedNeighbors.RemoveAt(0);
                    }

                    GroundTileData nearestNeighbor = alternativeTargets[0];
                    for (int i = 1; i < alternativeTargets.Count; i++)
                        if (alternativeTargets[i].FValue < nearestNeighbor.FValue)
                            nearestNeighbor = alternativeTargets[i];

                    alternateTarget = nearestNeighbor;
                    toSearchNeighbors.Add(nearestNeighbor);
                    processedNeighbors.Add(nearestNeighbor);
                    alternativeTargets.Remove(nearestNeighbor);
                    path = SearchPath(_startTile, alternateTarget, excludeActors);
                }
            }

            path.Reverse();
            return path.Count;
        }        
        
        private List<GroundTileData> SearchPath(GroundTileData startTile, GroundTileData targetTile, bool excludeActors)
        {
            List<GroundTileData> toSearch = new() { startTile };
            List<GroundTileData> processed = new();

            while (toSearch.Count > 0)
            {
                GroundTileData current = toSearch[0];
                foreach (GroundTileData t in toSearch)
                    if (t.FValue < current.FValue || (t.FValue == current.FValue && t.HValue < current.HValue))
                        current = t;

                processed.Add(current);
                toSearch.Remove(current);

                if (current == targetTile)
                {
                    GroundTileData currentPathTile = targetTile;
                    List<GroundTileData> path = new();
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
                        if (neighbor.ThisIsOnIt == WhatIsOnIt.Obstacle || processed.Contains(neighbor)) 
                            continue;
                    }
                    else
                    {
                        if (neighbor.ThisIsOnIt != WhatIsOnIt.Nothing || processed.Contains(neighbor)) 
                            continue;
                    }
                        

                    bool inSearch = toSearch.Contains(neighbor);
                    int costToNeighbor = current.GValue + current.GetDistance(neighbor);

                    if (inSearch && costToNeighbor >= neighbor.GValue) continue;
                    
                    neighbor.SetGValue(costToNeighbor);
                    neighbor.SetConnection(current);

                    if (inSearch) continue;
                    
                    neighbor.SetHValue(neighbor.GetDistance(targetTile));

                    toSearch.Add(neighbor);
                }
            }
            
            return null;
        }
    }
}