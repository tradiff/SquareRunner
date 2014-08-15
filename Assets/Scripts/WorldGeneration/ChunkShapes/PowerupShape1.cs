using UnityEngine;
using System.Collections;

public class PowerupShape1 : BaseChunkShape
{
    public PowerupShape1()
    {
        this.Difficulty = 1;

        this.Map.Add(new FeatureDefinition { Rect = new Rect { xMin = 0, xMax = 14, yMin = 0, yMax = 1 }, TileType = TileTypes.Platform });
        this.Map.Add(new FeatureDefinition { Rect = new Rect { xMin = 14, xMax = 19, yMin = 0, yMax = 1 }, TileType = TileTypes.Liquid });
        this.Map.Add(new FeatureDefinition { Rect = new Rect { xMin = 19, xMax = 50, yMin = 0, yMax = 1 }, TileType = TileTypes.Platform });


        this.Map.Add(new FeatureDefinition { Rect = new Rect { xMin = 1, xMax = 5, yMin = 3, yMax = 4 }, TileType = TileTypes.Platform });
        this.Map.Add(new FeatureDefinition { Rect = new Rect { xMin = 27, xMax = 34, yMin = 3, yMax = 4 }, TileType = TileTypes.Platform50p });

        this.Map.Add(new FeatureDefinition { Rect = new Rect { xMin = 10, xMax = 14, yMin = 6, yMax = 7 }, TileType = TileTypes.Platform });
        this.Map.Add(new FeatureDefinition { Rect = new Rect { xMin = 19, xMax = 29, yMin = 6, yMax = 7 }, TileType = TileTypes.Platform });
        this.Map.Add(new FeatureDefinition { Rect = new Rect { xMin = 32, xMax = 38, yMin = 6, yMax = 7 }, TileType = TileTypes.Platform });


        this.Map.Add(new FeatureDefinition { Rect = new Rect { xMin = 11, xMax = 14, yMin = 7, yMax = 9 }, TileType = TileTypes.Coin });
        this.Map.Add(new FeatureDefinition { Rect = new Rect { xMin = 20, xMax = 29, yMin = 7, yMax = 9 }, TileType = TileTypes.Coin });


        this.Map.Add(new FeatureDefinition { Rect = new Rect { xMin = 11, yMin = 1, width = 1, height = 1 }, TileType = TileTypes.Tier1Enemy });
        this.Map.Add(new FeatureDefinition { Rect = new Rect { xMin = 32, yMin = 1, width = 1, height = 1 }, TileType = TileTypes.Tier1Enemy });

        this.Map.Add(new FeatureDefinition { Rect = new Rect { xMin = 35, yMin = 7 }, TileType = TileTypes.Powerup });
    }
}