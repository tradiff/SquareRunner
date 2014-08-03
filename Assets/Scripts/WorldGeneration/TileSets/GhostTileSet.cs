using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GhostTileSet : BaseTileSet
{
    public GhostTileSet()
    {
        bgPrefab = Resources.Load("backgrounds/Background_Ghost_Prefab");
        var megaTiles = Resources.LoadAll<Sprite>("Textures/mega_tileset");
        groundN = groundNE = groundNW = megaTiles.FirstOrDefault(sprite => sprite.name == "ghost_floor");
        enemyTier1List = new List<string> { "koopa_troopa_red" };
    }
}
