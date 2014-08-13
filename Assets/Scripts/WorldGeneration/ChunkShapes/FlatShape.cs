using UnityEngine;
using System.Collections;

public class FlatShape : BaseChunkShape
{
    public FlatShape()
    {
        this.Difficulty = 1;

        this.Map.Add(new FeatureDefinition { Rect = new Rect { xMin = 0, xMax = 50, yMin = 0, height = 1}, TileType = TileTypes.Platform });

        this.Map.Add(new FeatureDefinition { Rect = new Rect { xMin = 12, xMax = 15, yMin = 1, height = 1 }, TileType = TileTypes.Tier1Enemy });
        this.Map.Add(new FeatureDefinition { Rect = new Rect { xMin = 32, xMax = 35, yMin = 1, height = 1 }, TileType = TileTypes.Tier1Enemy });
    }
}