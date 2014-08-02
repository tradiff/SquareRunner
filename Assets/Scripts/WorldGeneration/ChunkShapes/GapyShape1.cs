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
                    if (x >= 0 && x <= 9)
                    {
                        this.Map[x, y] = TileTypes.GroundN;
                    }

                    if (x == 10)
                    {
                        this.Map[x, y] = TileTypes.GroundNE;
                    }

                    // gap

                    if (x == 14)
                    {
                        this.Map[x, y] = TileTypes.GroundNW;
                    }

                    if (x >= 15 && x <= 16)
                    {
                        this.Map[x, y] = TileTypes.GroundN;
                    }

                    if (x == 17)
                    {
                        this.Map[x, y] = TileTypes.GroundNE;
                    }

                    // gap

                    if (x == 20)
                    {
                        this.Map[x, y] = TileTypes.GroundNW;
                    }

                    if (x > 20)
                    {
                        this.Map[x, y] = TileTypes.GroundN;
                    }

                }
            }
        }

    }
}