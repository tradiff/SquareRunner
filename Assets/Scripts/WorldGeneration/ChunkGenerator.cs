using UnityEngine;
using System.Collections;

public class ChunkGenerator
{
    private WorldGenerator worldGenerator;

    public ChunkGenerator()
    {
        worldGenerator = GameManager.Instance.WorldGenerator;
    }

    public void Generate(GameObject chunk, float chunkWidth, BaseChunkShape chunkShape, BaseBiome biome, bool buffered)
    {
        worldGenerator.StartChildCoroutine(GenerateCoroutine(chunk, chunkWidth, chunkShape, biome, buffered));
    }

    public IEnumerator GenerateCoroutine(GameObject chunk, float chunkWidth, BaseChunkShape chunkShape, BaseBiome biome, bool buffered)
    {
        Debug.Log("generating " + chunkShape.Map.Count);
        worldGenerator.CreateBG(chunk, biome.backgroundPrefab);

        //if (tileSet.NeedsTransition)
        //{
        //    for (int y = 0; y < chunkShape.Map.GetLength(1); y++)
        //    {
        //        var tile = worldGenerator.CreateTile(chunk, tileSet.transitionTile, -1, y);
        //        var sr = tile.GetComponentInChildren<SpriteRenderer>();
        //        sr.sortingLayerName = "Background";
        //        sr.sortingOrder = 2;
        //        var tile2 = worldGenerator.CreateTile(chunk, tileSet.transitionTile, worldGenerator.chunkWidth, y);
        //        var sr2 = tile2.GetComponentInChildren<SpriteRenderer>();
        //        sr2.sortingLayerName = "Background";
        //        sr2.sortingOrder = 2;

        //    }
        //}

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
                case BaseChunkShape.TileTypes.GroundN:
                    worldGenerator.CreatePlatform(chunk, feature.Rect);
                    worldGenerator.CreateTiles(chunk, biome.tileColor, feature.Rect);
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
