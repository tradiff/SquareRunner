using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class BaseBiome : IWeighted
{
    public Object backgroundPrefab;

    public Object coinPrefab = Resources.Load("entities/Coin_Prefab");
    public Color tileColor;
    public Color waterColor = new Color(0.32f, 0.77f, 1f, 0.5f);
    public Color playerColor = new Color(1f, 1f, 1f, 1f);

    public List<string> enemyTier1List = new List<string>();

    public int Weight { get; set; }
    public bool IsSpecial = true;

    public virtual void UpdateChunk(WorldChunk chunk)
    {

    }
}
