using UnityEngine;
using System.Collections;

public class GapyShape2 : BaseChunkShape
{
    public GapyShape2()
    {
        this.Difficulty = 0;
        this.Map.Add(new FeatureDefinition { Rect = new Rect(0, 0, 10, 1), TileType = TileTypes.Platform });
        this.Map.Add(new FeatureDefinition { Rect = new Rect(14, 0, 3, 3), TileType = TileTypes.Platform });
        this.Map.Add(new FeatureDefinition { Rect = new Rect(20, 0, GameManager.Instance.WorldGenerator.chunkWidth - 20, 1), TileType = TileTypes.Platform });


        this.Map.Add(new FeatureDefinition { Rect = new Rect { xMin = 27, width = 1, yMin = 1, height = 3 }, TileType = TileTypes.Spike });
        this.Map.Add(new FeatureDefinition { Rect = new Rect { xMin = 28, xMax = 35, yMin = 1, height = 3 }, TileType = TileTypes.Platform });



    }

}