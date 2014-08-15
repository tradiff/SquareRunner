﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class LavaCaveBiome : BaseBiome
{
    public LavaCaveBiome()
    {
        this.Weight = 1;
        this.tileColor = new Color(0.2f, 0.2f, 0.2f);
        this.backgroundPrefab = Resources.Load("backgrounds/Background_RedAnime_Prefab");
    }
}

