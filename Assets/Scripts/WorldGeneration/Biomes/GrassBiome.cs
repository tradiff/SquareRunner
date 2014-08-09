using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class GrassBiome : BaseBiome
{
    public GrassBiome()
    {
        this.Weight = 1;
        this.IsSpecial = false;
        this.tileColor = Color.white;
        this.backgroundPrefab = Resources.Load("backgrounds/Background_Dust_Prefab");
    }
}

