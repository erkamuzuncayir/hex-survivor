using System;
using System.Collections.Generic;
using UnityEngine;

public enum TileType
{
    Grass,
    Water,
    Fire,
    Building,
    Empty
}

public class TileData
{
    public TileType TypeOfTile;
    public Vector3Int Coord { get; }
    public bool IsPopulated;
    [NonSerialized] public List<TileData> Neighbors;

    public TileData(Vector3Int coord, bool isPopulated)
    {
        Coord = coord;
        IsPopulated = isPopulated;
    }

    public TileData Connection { get; private set; }
    public int GValue { get; private set; }
    public int HValue { get; private set; }
    public int FValue => GValue + HValue;
    
    public void SetConnection(TileData hexTile)
    {
        Connection = hexTile;
    }

    public void SetGValue(int g)
    {
        GValue = g;
    }

    public void SetHValue(int h)
    {
        HValue = h;
    }

    public int GetDistance(TileData otherTile)
    {
        int moveCount = 0;
        Vector3Int tempCoord = Coord;
        Vector3Int destinationCoord = otherTile.Coord;
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