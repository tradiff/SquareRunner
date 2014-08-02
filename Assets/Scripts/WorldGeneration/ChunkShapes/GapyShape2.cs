using UnityEngine;
using System.Collections;

public class GapyShape2 : BaseChunkShape
{
    public GapyShape2()
    {
        this.Difficulty = 0;

        for (int x = 0; x < this.Map.GetLength(0) ; x++)
        {
            for (int y = 0; y < this.Map.GetLength(1); y++)
            {
                if (y == 2)
                {
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
                }
                if (y == 1 || y == 0)
                {
                    if (x == 14)
                    {
                        this.Map[x, y] = TileTypes.GroundW;
                    }

                    if (x >= 15 && x <= 16)
                    {
                        this.Map[x, y] = TileTypes.GroundC;
                    }

                    if (x == 17)
                    {
                        this.Map[x, y] = TileTypes.GroundE;
                    }
                }



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