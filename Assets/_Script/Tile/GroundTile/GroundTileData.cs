using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Script.Tile
{
    public enum TileType
    {
        Grass,
        Water,
        Fire,
        Building,
        Empty
    }

    [Serializable]
    public class GroundTileData
    {
        public int DictIndex { get; private set; }
        public Vector3 WorldPosition { get; set; }
        public Vector3Int Coord { get; set; }
        public TileType TypeOfTile { get; private set; }
        public bool IsPlayerOnIt { get; set; }
        public bool IsPopulated { get; set; }
        [NonSerialized] public List<GroundTileData> Neighbors = new ();

        public GroundTileData(int dictIndex, Vector3 worldPosition, Vector3Int coord, TileType typeOfTile, bool isPopulated)
        {
            DictIndex = dictIndex;
            WorldPosition = worldPosition;
            Coord = coord;
            TypeOfTile = typeOfTile;
            IsPopulated = isPopulated;
        }

        public GroundTileData Connection { get; private set; }
        public int GValue { get; private set; }
        public int HValue { get; private set; }
        public int FValue => GValue + HValue;
    
        public void SetConnection(GroundTileData hexGroundTile)
        {
            Connection = hexGroundTile;
        }

        public void SetGValue(int g)
        {
            GValue = g;
        }

        public void SetHValue(int h)
        {
            HValue = h;
        }

        public int GetDistance(GroundTileData otherGroundTile)
        {
            int moveCount = 0;
            Vector3Int tempCoord = Coord;
            Vector3Int destinationCoord = otherGroundTile.Coord;
            while (tempCoord != destinationCoord)
            {
                Vector3Int move = new();
                if (tempCoord.y % 2 == 0)
                {
                    if (tempCoord.y > destinationCoord.y)
                    {
                        move.y--;
                        if (tempCoord.x > destinationCoord.x)
                            move.x--;
                    }
                    else if (tempCoord.y < destinationCoord.y)
                    {
                        move.y++;
                        if (tempCoord.x > destinationCoord.x)
                            move.x--;
                    }
                    else
                    {
                        if (tempCoord.x > destinationCoord.x)
                            move.x--;
                        else
                            move.x++;
                    }
                }
                else
                {
                    if (tempCoord.y > destinationCoord.y)
                    {
                        move.y--;
                        if (tempCoord.x < destinationCoord.x)
                            move.x++;
                    }
                    else if (tempCoord.y < destinationCoord.y)
                    {
                        move.y++;
                        if (tempCoord.x < destinationCoord.x)
                            move.x++;
                    }
                    else
                    {
                        if (tempCoord.x < destinationCoord.x)
                            move.x++;
                        else
                            move.x--;
                    }
                }

                tempCoord += move;
                moveCount++;
            }

            return moveCount;
        }
    }
}