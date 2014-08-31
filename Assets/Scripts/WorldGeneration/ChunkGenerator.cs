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

    public void Generate(WorldChunk chunk, bool buffered)
    {
        worldGenerator.StartChildCoroutine(GenerateCoroutine(chunk, buffered));
    }

    public IEnumerator GenerateCoroutine(WorldChunk chunk, bool buffered)
    {
        if (chunk.Biome.backgroundPrefab != null)
            worldGenerator.CreateBG(chunk.gameObject, chunk.Biome.backgroundPrefab);

        // ragged top
        for (int x = 0; x < worldGenerator.chunkWidth; x++)
        {
            for (int y = (int)worldGenerator.chunkHeight - 2; y < worldGenerator.chunkHeight; y++)
            {
                if (Random.Range(0, 2) == 0)
                {
                    var tile = worldGenerator.CreateTile(chunk.gameObject, new Color(0.2f, 0.2f, 0.2f), x, y);
                }
            }
        }

        foreach (var feature in chunk.Shape.Map)
        {
            switch (feature.TileType)
            {
                case BaseChunkShape.TileTypes.Air:
                    break;
                case BaseChunkShape.TileTypes.Liquid:
                    var tile = worldGenerator.CreateTiles(chunk.gameObject, liquidPrefab, feature.Rect);
                    tile.GetComponentInChildren<SpriteRenderer>().color = chunk.Biome.waterColor;
                    var particleSystem = tile.GetComponentInChildren<ParticleSystem>();
                    particleSystem.emissionRate = 3 * feature.Rect.width;
                    particleSystem.startColor = chunk.Biome.waterColor;
                    break;
                case BaseChunkShape.TileTypes.Platform:
                    worldGenerator.CreatePlatform(chunk.gameObject, feature.Rect);
                    worldGenerator.CreateTiles(chunk.gameObject, chunk.Biome.tileColor, feature.Rect);
                    break;
                case BaseChunkShape.TileTypes.OneWayPlatform:
                    worldGenerator.CreateOneWayPlatform(chunk.gameObject, feature.Rect);
                    worldGenerator.CreateTiles(chunk.gameObject, chunk.Biome.tileColor, feature.Rect);
                    break;
                case BaseChunkShape.TileTypes.OneWayPlatform50p:
                    if (Random.Range(0, 2) == 0)
                    {
                        worldGenerator.CreateOneWayPlatform(chunk.gameObject, feature.Rect);
                        worldGenerator.CreateTiles(chunk.gameObject, chunk.Biome.tileColor, feature.Rect);
                    }
                    break;
                case BaseChunkShape.TileTypes.SpawnPoint:
                    worldGenerator.CreateTile(chunk.gameObject, spawnPointPrefab, feature.Rect.xMin, feature.Rect.yMin);
                    break;
                case BaseChunkShape.TileTypes.Spike:
                    for (var x = (int)feature.Rect.xMin; x < (int)feature.Rect.xMax; x++)
                    {
                        for (var y = (int)feature.Rect.yMin; y < (int)feature.Rect.yMax; y++)
                        {
                            worldGenerator.CreateTile(chunk.gameObject, spikePrefab, x, y);
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
