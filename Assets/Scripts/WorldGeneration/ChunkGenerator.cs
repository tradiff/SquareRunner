using UnityEngine;
using System.Collections;

public class ChunkGenerator : IChunkGenerator
{
    private WorldGenerator worldGenerator;
    public Object liquidPrefab = Resources.Load("Liquid_Prefab");
    public Object spawnPointPrefab = Resources.Load("entities/SpawnTarget_Prefab");
    public Object spikePrefab = Resources.Load("entities/Spike_Prefab");

    public ChunkGenerator()
    {
        worldGenerator = GameManager.Instance.WorldGenerator;
    }

    public void Generate(GameObject chunk, BaseChunkShape chunkShape, BaseBiome biome, bool buffered)
    {
        chunk.GetComponent<WorldChunk>().Biome = biome;
        worldGenerator.StartChildCoroutine(GenerateCoroutine(chunk, chunkShape, biome, buffered));
    }

    public IEnumerator GenerateCoroutine(GameObject chunk, BaseChunkShape chunkShape, BaseBiome biome, bool buffered)
    {
        Debug.Log("generating " + chunkShape.Map.Count);
        if (biome.backgroundPrefab != null)
            worldGenerator.CreateBG(chunk, biome.backgroundPrefab);

        // ragged top
        for (int x = 0; x < worldGenerator.chunkWidth; x++)
        {
            for (int y = (int)worldGenerator.chunkHeight - 2; y < worldGenerator.chunkHeight; y++)
            {
                if (Random.Range(0, 2) == 0)
                {
                    var tile = worldGenerator.CreateTile(chunk, new Color(0.2f, 0.2f, 0.2f), x, y);
                }
            }
        }

        foreach (var feature in chunkShape.Map)
        {
            switch (feature.TileType)
            {
                case BaseChunkShape.TileTypes.Air:
                    break;
                case BaseChunkShape.TileTypes.Liquid:
                    var tile = worldGenerator.CreateTiles(chunk, liquidPrefab, feature.Rect);
                    tile.GetComponentInChildren<SpriteRenderer>().color = biome.waterColor;
                    var particleSystem = tile.GetComponentInChildren<ParticleSystem>();
                    particleSystem.emissionRate = 3 * feature.Rect.width;
                    particleSystem.startColor = biome.waterColor;
                    break;
                case BaseChunkShape.TileTypes.Platform:
                    worldGenerator.CreatePlatform(chunk, feature.Rect);
                    worldGenerator.CreateTiles(chunk, biome.tileColor, feature.Rect);
                    break;
                case BaseChunkShape.TileTypes.OneWayPlatform:
                    worldGenerator.CreateOneWayPlatform(chunk, feature.Rect);
                    worldGenerator.CreateTiles(chunk, biome.tileColor, feature.Rect);
                    break;
                case BaseChunkShape.TileTypes.OneWayPlatform50p:
                    if (Random.Range(0, 2) == 0)
                    {
                        worldGenerator.CreateOneWayPlatform(chunk, feature.Rect);
                        worldGenerator.CreateTiles(chunk, biome.tileColor, feature.Rect);
                    }
                    break;
                case BaseChunkShape.TileTypes.SpawnPoint:
                    worldGenerator.CreateTile(chunk, spawnPointPrefab, feature.Rect.xMin, feature.Rect.yMin);
                    break;
                case BaseChunkShape.TileTypes.Spike:
                    for (var x = (int)feature.Rect.xMin; x < (int)feature.Rect.xMax; x++)
                    {
                        for (var y = (int)feature.Rect.yMin; y < (int)feature.Rect.yMax; y++)
                        {
                            worldGenerator.CreateTile(chunk, spikePrefab, x, y);
                        }
                    }
                    break;
                default:
                    break;
            }
            //if (buffered)
            //    yield return new WaitForEndOfFrame();

        }

        yield return null;
    }
}
