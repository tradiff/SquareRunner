using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CastleTileSet : BaseTileSet
{
    public CastleTileSet()
    {
        bgPrefab = Resources.Load("backgrounds/Background_Castle_Prefab");

        var megaTiles = Resources.LoadAll<Sprite>("Textures/mega_tileset");

        groundNW = megaTiles.FirstOrDefault(sprite => sprite.name == "castle_NW");
        groundN = megaTiles.FirstOrDefault(sprite => sprite.name == "castle_N");
        groundNE = megaTiles.FirstOrDefault(sprite => sprite.name == "castle_NE");
        groundW = megaTiles.FirstOrDefault(sprite => sprite.name == "castle_W");
        groundC = megaTiles.FirstOrDefault(sprite => sprite.name == "castle_C");
        groundE = megaTiles.FirstOrDefault(sprite => sprite.name == "castle_E");
        groundSW = megaTiles.FirstOrDefault(sprite => sprite.name == "castle_SW");
        groundS = megaTiles.FirstOrDefault(sprite => sprite.name == "castle_S");
        groundSE = megaTiles.FirstOrDefault(sprite => sprite.name == "castle_SE");
        transitionTile = megaTiles.FirstOrDefault(sprite => sprite.name == "ghost_bg_wall");

        enemyTier1List = new List<string> { "dry_bones"};
        this.Weight = 1;
    }
}
