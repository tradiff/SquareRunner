using UnityEngine;
using System.Collections;
using System.Linq;

public class CoinGenerator : IChunkGenerator
{
    private WorldGenerator worldGenerator;

    public CoinGenerator()
    {
        worldGenerator = GameManager.Instance.WorldGenerator;
    }

    public void Generate(WorldChunk chunk, bool buffered)
    {
        worldGenerator.StartChildCoroutine(GenerateCoroutine(chunk, buffered));
    }

    public IEnumerator GenerateCoroutine(WorldChunk chunk, bool buffered)
    {
        if (chunk.Biome.GetType() == typeof(BonusBiome))
        {
            var features = chunk.Shape.Map.Where(x => x.TileType == BaseChunkShape.TileTypes.OneWayPlatform || x.TileType == BaseChunkShape.TileTypes.OneWayPlatform50p || x.TileType == BaseChunkShape.TileTypes.Platform);
            var overlap = false;
            for (int x = 0; x < 50; x++)
            {
                for (int y = 1; y < 7; y++)
                {
                    overlap = false;
                    foreach (var feature in features)
                    {
                        if (feature.Rect.Contains(new Vector2(x,y)))
                        {
                            overlap = true;
                            break;
                        }
                    }

                    if (!overlap)
                    {
                        var coin = worldGenerator.CreateTile(chunk.gameObject, chunk.Biome.coinPrefab, x, y);
                        Component.Destroy(coin.GetComponentInChildren<Animator>());
                        if (buffered)
                            yield return new WaitForEndOfFrame();
                    }

                }
            }

        }
        else
        {
            foreach (var feature in chunk.Shape.Map)
            {
                if (feature.TileType == BaseChunkShape.TileTypes.Coin)
                {
                    for (int x = (int)feature.Rect.xMin; x < (int)feature.Rect.xMax; x++)
                    {
                        for (int y = (int)feature.Rect.yMin; y < (int)feature.Rect.yMax; y++)
                        {
                            worldGenerator.CreateTile(chunk.gameObject, chunk.Biome.coinPrefab, x, y);
                        }
                    }
                    if (buffered)
                        yield return new WaitForEndOfFrame();
                }
            }
        }
        chunk.Biome.UpdateChunk(chunk);
    }



}
