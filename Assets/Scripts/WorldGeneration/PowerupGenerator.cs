using UnityEngine;
using System.Collections;

public class PowerupGenerator : IChunkGenerator
{
    private WorldGenerator worldGenerator;
    public Object powerupHatPrefab = Resources.Load("entities/Powerurp_Hat_Prefab");
    public Object powerupBonusPrefab = Resources.Load("entities/Powerurp_Bonus_Prefab");
    public Object powerupMagnetPrefab = Resources.Load("entities/Powerurp_Magnet_Prefab");

    public PowerupGenerator()
    {
        worldGenerator = GameManager.Instance.WorldGenerator;
    }

    public void Generate(WorldChunk chunk, bool buffered)
    {
        foreach (var feature in chunk.Shape.Map)
        {
            if (feature.TileType == BaseChunkShape.TileTypes.Powerup)
            {
                if (Random.Range(0, 3) == 0)
                    worldGenerator.CreateTile(chunk.gameObject, powerupHatPrefab, feature.Rect.xMin, feature.Rect.yMin);
                else if (Random.Range(0, 2) == 0)
                    worldGenerator.CreateTile(chunk.gameObject, powerupMagnetPrefab, feature.Rect.xMin, feature.Rect.yMin);
                else if (Random.Range(0, 5) == 0 && chunk.Biome.GetType() != typeof(BonusBiome))
                    worldGenerator.CreateTile(chunk.gameObject, powerupBonusPrefab, feature.Rect.xMin, feature.Rect.yMin);

            }
        }
    }
}
