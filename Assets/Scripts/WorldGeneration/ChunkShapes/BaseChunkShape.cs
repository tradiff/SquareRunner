using UnityEngine;
using System.Collections;

public abstract class BaseChunkShape
{
    public int Difficulty = 0;
    public TileTypes[,] Map;

    public BaseChunkShape()
    {
        Map = new TileTypes[(int)GameManager.Instance.WorldGenerator.chunkWidth, 10];
    }

    public enum TileTypes
    {
        Air = 0,
        GroundNW = 1,
        GroundN = 2,
        GroundNE = 3,
        GroundW = 4,
        GroundC = 5,
        GroundE = 6,
        GroundSW = 7,
        GroundS = 8,
        GroundSE = 9
    }

}
