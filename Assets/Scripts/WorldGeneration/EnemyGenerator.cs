using UnityEngine;
using System.Collections;

public class EnemyGenerator : IChunkGenerator
{
    private WorldGenerator worldGenerator;
    public Object enemyTier1Prefab = Resources.Load("entities/Enemy_Tier1_Prefab");
    public Object enemyVinePrefab = Resources.Load("entities/Enemy_Vine_Prefab");
    public Object spikePrefab = Resources.Load("entities/Spike_Prefab");

    public EnemyGenerator()
    {
        worldGenerator = GameManager.Instance.WorldGenerator;
    }

    public void Generate(GameObject chunk, BaseChunkShape chunkShape, BaseBiome biome, bool buffered)
    {
        if (GameManager.Instance.Area == GameManager.Areas.Bonus)
        {
            return;
        }

        foreach (var feature in chunkShape.Map)
        {
            if (feature.TileType == BaseChunkShape.TileTypes.Tier1Enemy)
            {
                var r = Random.Range(0, 3);
                Object prefab = null;
                if (r == 0)
                {
                    prefab = enemyTier1Prefab;
                }
                if (r == 1)
                {
                    prefab = enemyVinePrefab;
                }

                if (prefab != null)
                {
                    for (var x = (int)feature.Rect.xMin; x < (int)feature.Rect.xMax; x += 2)
                    {
                        for (var y = (int)feature.Rect.yMin; y < (int)feature.Rect.yMax; y += 2)
                        {
                            worldGenerator.CreateTile(chunk, prefab, x, y);
                        }
                    }
                }

            }
            if (feature.TileType == BaseChunkShape.TileTypes.Spike)
            {
                for (var x = (int)feature.Rect.xMin; x < (int)feature.Rect.xMax; x++)
                {
                    for (var y = (int)feature.Rect.yMin; y < (int)feature.Rect.yMax; y++)
                    {
                        worldGenerator.CreateTile(chunk, spikePrefab, x, y);
                    }
                }

            }
        }
    }




}
