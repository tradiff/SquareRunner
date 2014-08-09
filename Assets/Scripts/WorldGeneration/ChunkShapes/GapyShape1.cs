using UnityEngine;
using System.Collections;

public class GapyShape1 : BaseChunkShape
{
    public GapyShape1()
    {
        this.Difficulty = 10;

        for (int x = 0; x < this.Map.GetLength(0) ; x++)
        {
            for (int y = 0; y < this.Map.GetLength(1); y++)
            {
                if (y == 0) // ground
                {
                    if (x >= 0 && x <= 10)
                    {
                        this.Map[x, y] = TileTypes.GroundN;
                    }

                    // gap

                    if (x >= 14 && x <= 17)
                    {
                        this.Map[x, y] = TileTypes.GroundN;
                    }

                    // gap

                    if (x >= 20)
                    {
                        this.Map[x, y] = TileTypes.GroundN;
                    }

                }
            }
        }

    }
}