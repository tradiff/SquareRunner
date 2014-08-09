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
        GroundN = 1
    }

}
