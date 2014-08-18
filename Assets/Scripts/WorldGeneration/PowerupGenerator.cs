using UnityEngine;
using System.Collections;

public class PowerupGenerator : IChunkGenerator
{
    private WorldGenerator worldGenerator;
    public Object powerupHatPrefab = Resources.Load("entities/Powerurp_Hat_Prefab");
    public Object powerupBonusPrefab = Resources.Load("entities/Powerurp_Bonus_Prefab");

    public PowerupGenerator()
    {
        worldGenerator = GameManager.Instance.WorldGenerator;
    }

    public void Generate(GameObject chunk, BaseChunkShape chunkShape, BaseBiome biome, bool buffered)
    {
        foreach (var feature in chunkShape.Map)
        {
            if (feature.TileType == BaseChunkShape.TileTypes.Powerup)
            {
                if (Random.Range(0, 3) == 0)
                    worldGenerator.CreateTile(chunk, powerupHatPrefab, feature.Rect.xMin, feature.Rect.yMin);
                else if (Random.Range(0, 5) == 0)
                    worldGenerator.CreateTile(chunk, powerupBonusPrefab, feature.Rect.xMin, feature.Rect.yMin);

            }
        }
    }
}
