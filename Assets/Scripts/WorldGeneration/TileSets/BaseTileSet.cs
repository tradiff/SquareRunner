using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class BaseTileSet : IWeighted
{
    public bool NeedsTransition = true;
    public Object bgPrefab;
    public Sprite groundNW;
    public Sprite groundN;
    public Sprite groundNE;
    public Sprite groundW;
    public Sprite groundC;
    public Sprite groundE;
    public Sprite groundSW;
    public Sprite groundS;
    public Sprite groundSE;
    public Sprite transitionTile;

    public Object coinPrefab = Resources.Load("entities/Coin_Prefab");

    public List<string> enemyTier1List = new List<string>();

    public int Weight { get; set; }
    public bool IsSpecial = true;

}
