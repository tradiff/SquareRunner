using UnityEngine;
using System.Collections;

public class ChrisShape1 : BaseChunkShape
{
    public ChrisShape1()
    {
        this.Difficulty = 1;

        this.Map.Add(new FeatureDefinition { Rect = new Rect { xMin = 0, xMax = 10, yMin = 0, height = 1 }, TileType = TileTypes.Platform });
        this.Map.Add(new FeatureDefinition { Rect = new Rect { xMin = 13, xMax = 15, yMin = 0, height = 1 }, TileType = TileTypes.Platform });
        this.Map.Add(new FeatureDefinition { Rect = new Rect { xMin = 18, xMax = 33, yMin = 0, height = 1 }, TileType = TileTypes.Platform });
        this.Map.Add(new FeatureDefinition { Rect = new Rect { xMin = 43, xMax = 45, yMin = 0, height = 1 }, TileType = TileTypes.Platform });
        this.Map.Add(new FeatureDefinition { Rect = new Rect { xMin = 49, xMax = 50, yMin = 0, height = 1 }, TileType = TileTypes.Platform });

        this.Map.Add(new FeatureDefinition { Rect = new Rect { xMin = 10, xMax = 13, yMin = 0, height = 1 }, TileType = TileTypes.Liquid });
        this.Map.Add(new FeatureDefinition { Rect = new Rect { xMin = 15, xMax = 18, yMin = 0, height = 1 }, TileType = TileTypes.Liquid });
        this.Map.Add(new FeatureDefinition { Rect = new Rect { xMin = 33, xMax = 43, yMin = 0, height = 1 }, TileType = TileTypes.Liquid });
        this.Map.Add(new FeatureDefinition { Rect = new Rect { xMin = 45, xMax = 49, yMin = 0, height = 1 }, TileType = TileTypes.Liquid });

        this.Map.Add(new FeatureDefinition { Rect = new Rect { xMin = 10, xMax = 13, yMin = 2, height = 3 }, TileType = TileTypes.Coin });
        this.Map.Add(new FeatureDefinition { Rect = new Rect { xMin = 15, xMax = 18, yMin = 2, height = 3 }, TileType = TileTypes.Coin });
        this.Map.Add(new FeatureDefinition { Rect = new Rect { xMin = 23, xMax = 27, yMin = 2, height = 5 }, TileType = TileTypes.Coin });
        this.Map.Add(new FeatureDefinition { Rect = new Rect { xMin = 33, xMax = 43, yMin = 2, height = 3 }, TileType = TileTypes.Coin });
        this.Map.Add(new FeatureDefinition { Rect = new Rect { xMin = 45, xMax = 49, yMin = 2, height = 3 }, TileType = TileTypes.Coin });

        this.Map.Add(new FeatureDefinition { Rect = new Rect { xMin = 5, width = 1, yMin = 10, height = 1 }, TileType = TileTypes.SpawnPoint });
    }
}