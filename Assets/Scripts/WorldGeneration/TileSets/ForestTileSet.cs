using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ForestTileSet : BaseTileSet
{
    public ForestTileSet()
    {
        NeedsTransition = false;
        bgPrefab = Resources.Load("backgrounds/Background_Forest_Prefab");

        var megaTiles = Resources.LoadAll<Sprite>("Textures/mega_tileset");

        groundNW = megaTiles.FirstOrDefault(sprite => sprite.name == "grass_NW");
        groundN = megaTiles.FirstOrDefault(sprite => sprite.name == "grass_N");
        groundNE = megaTiles.FirstOrDefault(sprite => sprite.name == "grass_NE");
        groundW = megaTiles.FirstOrDefault(sprite => sprite.name == "grass_W");
        groundC = megaTiles.FirstOrDefault(sprite => sprite.name == "grass_C");
        groundE = megaTiles.FirstOrDefault(sprite => sprite.name == "grass_E");
        groundSW = megaTiles.FirstOrDefault(sprite => sprite.name == "grass_SW");
        groundS = megaTiles.FirstOrDefault(sprite => sprite.name == "grass_S");
        groundSE = megaTiles.FirstOrDefault(sprite => sprite.name == "grass_SE");

        enemyTier1List = new List<string> { "koopa_troopa_green", "koopa_troopa_red" };
        this.Weight = 10;
        this.IsSpecial = false;
    }
}
