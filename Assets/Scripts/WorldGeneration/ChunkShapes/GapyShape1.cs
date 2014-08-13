using UnityEngine;
using System.Collections;

public class GapyShape1 : BaseChunkShape
{
    public GapyShape1()
    {
        this.Difficulty = 10;

        this.Map.Add(new FeatureDefinition { Rect = new Rect(0, 0, 10, 1), TileType = TileTypes.Platform });
        this.Map.Add(new FeatureDefinition { Rect = new Rect(14, 0, 3, 1), TileType = TileTypes.Platform });
        this.Map.Add(new FeatureDefinition { Rect = new Rect(20, 0, GameManager.Instance.WorldGenerator.chunkWidth - 20, 1), TileType = TileTypes.Platform });




    }
}