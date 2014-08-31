using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class BaseChunkShape
{
    public int Difficulty = 0;
    public List<FeatureDefinition> Map = new List<FeatureDefinition>();
    public bool CanKill = false;
 
    public BaseChunkShape()
    {
    }

    public enum TileTypes
    {
        Air,
        Platform,
        OneWayPlatform,
        OneWayPlatform50p,
        Tier1Enemy,
        Coin,
        Powerup,
        Liquid,
        Spike,
        SpawnPoint
    }

    public class FeatureDefinition
    {
        public Rect Rect;
        public TileTypes TileType;
    }

}
