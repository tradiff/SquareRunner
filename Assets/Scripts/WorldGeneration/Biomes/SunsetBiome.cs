using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class SunsetBiome : BaseBiome
{
    public SunsetBiome()
    {
        this.Weight = 1;
        this.tileColor = Color.black;
        this.playerColor = Color.black;
        this.waterColor = new Color(0.32f, 0.77f, 1f, 1f);
        this.backgroundPrefab = Resources.Load("backgrounds/Background_Sunset_Prefab");
    }
    public override void UpdateChunk(GameObject chunk)
    {
        var sr = chunk.GetComponentsInChildren<SpriteRenderer>(true);
        foreach (var item in sr)
        {
            if (item.tag != "Liquid" && item.tag != "Background")
                item.color = Color.black;
        }
    }
}

