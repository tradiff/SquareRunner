using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class WesternBiome : BaseBiome
{
    public WesternBiome()
    {
        this.Weight = 1;
        this.tileColor = new Color(0.45f, 0.29f, .20f);
        //this.backgroundPrefab = Resources.Load("backgrounds/Background_PurpleAnime_Prefab");
    }
}

