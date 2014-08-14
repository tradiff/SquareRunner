using UnityEngine;
using System.Collections;

public class PowerupGenerator : IChunkGenerator
{
    private WorldGenerator worldGenerator;
    public Object powerupHatPrefab = Resources.Load("entities/Powerurp_Hat_Prefab");

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
                worldGenerator.CreateTile(chunk, powerupHatPrefab, feature.Rect.xMin, feature.Rect.yMin);
            }
        }
    }
}
