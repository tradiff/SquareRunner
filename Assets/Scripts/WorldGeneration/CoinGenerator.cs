using UnityEngine;
using System.Collections;

public class CoinGenerator : IChunkGenerator
{
    private WorldGenerator worldGenerator;

    public CoinGenerator()
    {
        worldGenerator = GameManager.Instance.WorldGenerator;
    }

    public void Generate(GameObject chunk, BaseChunkShape chunkShape, BaseBiome biome, bool buffered)
    {
        worldGenerator.StartChildCoroutine(GenerateCoroutine(chunk, chunkShape, biome, buffered));
    }

    public IEnumerator GenerateCoroutine(GameObject chunk, BaseChunkShape chunkShape, BaseBiome biome, bool buffered)
    {
        if (biome.GetType() == typeof(BonusBiome))
        {
            for (int x = 0; x < 50; x++)
            {
                for (int y = 1; y < 7; y++)
                {
                    worldGenerator.CreateTile(chunk, biome.coinPrefab, x, y);
                    if (buffered)
                        yield return new WaitForEndOfFrame();
                }
            }

        }
        else
        {
            foreach (var feature in chunkShape.Map)
            {
                if (feature.TileType == BaseChunkShape.TileTypes.Coin)
                {
                    for (int x = (int)feature.Rect.xMin; x < (int)feature.Rect.xMax; x++)
                    {
                        for (int y = (int)feature.Rect.yMin; y < (int)feature.Rect.yMax; y++)
                        {
                            worldGenerator.CreateTile(chunk, biome.coinPrefab, x, y);
                        }
                    }
                    if (buffered)
                        yield return new WaitForEndOfFrame();
                }
            }
        }
    }



}
