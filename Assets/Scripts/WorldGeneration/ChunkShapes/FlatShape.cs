using UnityEngine;
using System.Collections;

public class FlatShape : BaseChunkShape
{
    public FlatShape()
    {
        this.Difficulty = 1;

        for (int x = 0; x < this.Map.GetLength(0) ; x++)
        {
            for (int y = 0; y < this.Map.GetLength(1); y++)
            {
                if (y == 0) // ground
                {
                    this.Map[x, y] = TileTypes.GroundN;
                }
            }
        }
    }
}