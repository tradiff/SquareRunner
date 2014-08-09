using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class CaveBiome : BaseBiome
{
    public CaveBiome()
    {
        this.Weight = 1;
        this.tileColor = new Color(127,0,255);
        this.backgroundPrefab = Resources.Load("backgrounds/Background_PurpleAnime_Prefab");
    }
}

