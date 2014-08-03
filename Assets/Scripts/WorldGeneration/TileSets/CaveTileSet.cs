using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CaveTileSet : BaseTileSet
{
    public CaveTileSet()
    {
        bgPrefab = Resources.Load("backgrounds/Background_Cave_Prefab");

        var megaTiles = Resources.LoadAll<Sprite>("Textures/mega_tileset");

        groundNW = megaTiles.FirstOrDefault(sprite => sprite.name == "cave_NW");
        groundN = megaTiles.FirstOrDefault(sprite => sprite.name == "cave_N");
        groundNE = megaTiles.FirstOrDefault(sprite => sprite.name == "cave_NE");
        groundW = megaTiles.FirstOrDefault(sprite => sprite.name == "cave_W");
        groundC = megaTiles.FirstOrDefault(sprite => sprite.name == "cave_C");
        groundE = megaTiles.FirstOrDefault(sprite => sprite.name == "cave_E");
        groundSW = megaTiles.FirstOrDefault(sprite => sprite.name == "cave_SW");
        groundS = megaTiles.FirstOrDefault(sprite => sprite.name == "cave_S");
        groundSE = megaTiles.FirstOrDefault(sprite => sprite.name == "cave_SE");

        enemyTier1List = new List<string> { "koopa_troopa_green", "koopa_troopa_red" };
    }
}
