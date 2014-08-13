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
        if (Random.Range(0, 3) == 0)
        {
            // spawn coins

            var patternIndex = Random.Range(0, 3);

            if (patternIndex == 0)
            {
                for (int i = 10; i < 20; i++)
                {
                    worldGenerator.CreateTile(chunk, biome.coinPrefab, i, 4);
                    worldGenerator.CreateTile(chunk, biome.coinPrefab, i, 5);
                    if (buffered)
                        yield return new WaitForEndOfFrame();
                }
            }
            if (patternIndex == 1)
            {
                for (int i = 10; i < 20; i++)
                {
                    worldGenerator.CreateTile(chunk, biome.coinPrefab, i, 4);
                    worldGenerator.CreateTile(chunk, biome.coinPrefab, i, 5);
                    worldGenerator.CreateTile(chunk, biome.coinPrefab, i, 6);
                    if (buffered)
                        yield return new WaitForEndOfFrame();
                }
            }
            if (patternIndex == 2)
            {
                for (int i = 10; i < 20; i++)
                {
                    worldGenerator.CreateTile(chunk, biome.coinPrefab, i, 4);
                    worldGenerator.CreateTile(chunk, biome.coinPrefab, i, 5);
                    worldGenerator.CreateTile(chunk, biome.coinPrefab, i, 6);
                    worldGenerator.CreateTile(chunk, biome.coinPrefab, i, 7);
                    if (buffered)
                        yield return new WaitForEndOfFrame();
                }
            }
            if (patternIndex == 3)
            {
                for (int i = 10; i < 20; i++)
                {
                    worldGenerator.CreateTile(chunk, biome.coinPrefab, i, 4);
                    worldGenerator.CreateTile(chunk, biome.coinPrefab, i, 5);
                    worldGenerator.CreateTile(chunk, biome.coinPrefab, i, 6);
                    worldGenerator.CreateTile(chunk, biome.coinPrefab, i, 7);
                    worldGenerator.CreateTile(chunk, biome.coinPrefab, i, 8);
                    if (buffered)
                        yield return new WaitForEndOfFrame();
                }
            }


        }

    }



}
