using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class BaseChunkShape
{
    public int Difficulty = 0;
    public List<FeatureDefinition> Map = new List<FeatureDefinition>();
 
    public BaseChunkShape()
    {
    }

    public enum TileTypes
    {
        Air,
        Platform,
        Platform50p,
        Tier1Enemy,
        Coin,
        Powerup,
        Liquid,
        Spike
    }

    public class FeatureDefinition
    {
        public Rect Rect;
        public TileTypes TileType;
    }

}
