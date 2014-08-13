using UnityEngine;
using System.Collections;

public class PowerupGenerator : IChunkGenerator
{
    private WorldGenerator worldGenerator;
    public Object redMushroomPrefab = Resources.Load("entities/Red_Mushroom_Prefab");

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
                // todo: replace with powerup prefab
                worldGenerator.CreateTile(chunk, biome.coinPrefab, feature.Rect.xMin, feature.Rect.yMin);
            }
        }
    }
}
