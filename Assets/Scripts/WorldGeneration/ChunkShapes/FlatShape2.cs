using UnityEngine;
using System.Collections;

public class FlatShape2 : BaseChunkShape
{
    public FlatShape2()
    {
        this.Difficulty = 1;

        this.Map.Add(new FeatureDefinition { Rect = new Rect { xMin = 0, xMax = 50, yMin = 0, height = 1}, TileType = TileTypes.Platform });

        this.Map.Add(new FeatureDefinition { Rect = new Rect { xMin = 7, xMax = 19, yMin = 3, height = 1 }, TileType = TileTypes.OneWayPlatform });
        this.Map.Add(new FeatureDefinition { Rect = new Rect { xMin = 27, xMax = 39, yMin = 3, height = 1 }, TileType = TileTypes.OneWayPlatform });

        this.Map.Add(new FeatureDefinition { Rect = new Rect { xMin = 7, xMax = 19, yMin = 4, height = 3 }, TileType = TileTypes.Coin });
        this.Map.Add(new FeatureDefinition { Rect = new Rect { xMin = 27, xMax = 39, yMin = 4, height = 3 }, TileType = TileTypes.Coin });


        this.Map.Add(new FeatureDefinition { Rect = new Rect { xMin = 16, xMax = 19, yMin = 1, height = 1 }, TileType = TileTypes.Tier1Enemy });
        this.Map.Add(new FeatureDefinition { Rect = new Rect { xMin = 32, xMax = 35, yMin = 1, height = 1 }, TileType = TileTypes.Tier1Enemy });

        this.Map.Add(new FeatureDefinition { Rect = new Rect { xMin = 1, width = 1, yMin = 10, height = 1 }, TileType = TileTypes.SpawnPoint });
    }
}