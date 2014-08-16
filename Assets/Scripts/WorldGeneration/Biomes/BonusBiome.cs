using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class BonusBiome : BaseBiome
{
    public BonusBiome()
    {
        this.Weight = 1;
        this.tileColor = Color.white;
        this.backgroundPrefab = Resources.Load("backgrounds/Background_Bonus_Prefab");
    }
}

