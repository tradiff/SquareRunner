using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class BaseBiome : IWeighted
{
    public Object backgroundPrefab;

    public Object coinPrefab = Resources.Load("entities/Coin_Prefab");
    public Color tileColor;

    public List<string> enemyTier1List = new List<string>();

    public int Weight { get; set; }
    public bool IsSpecial = true;

}
