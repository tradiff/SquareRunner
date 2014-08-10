using UnityEngine;
using System.Collections;

public class FlatShape : BaseChunkShape
{
    public FlatShape()
    {
        this.Difficulty = 1;

        this.Map.Add(new FeatureDefinition { Rect = new Rect(0, 0, GameManager.Instance.WorldGenerator.chunkWidth, 1), TileType = TileTypes.GroundN });
    }
}