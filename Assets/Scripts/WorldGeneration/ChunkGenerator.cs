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
        //worldGenerator.CreateBG(chunk, tileSet.bgPrefab);

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
        for (int x = 0; x < chunkShape.Map.GetLength(0); x++)
        {
            for (int y = chunkShape.Map.GetLength(1) - 2; y < chunkShape.Map.GetLength(1); y++)
            {
                if (Random.RandomRange(0, 2) == 0)
                {
                    var tile = worldGenerator.CreateTile(chunk, Color.red, x, y);
                }
            }
        }



        for (int x = 0; x < chunkShape.Map.GetLength(0); x++)
        {
            for (int y = 0; y < chunkShape.Map.GetLength(1); y++)
            {
                var tileType = chunkShape.Map[x, y];
                switch (tileType)
                {
                    case BaseChunkShape.TileTypes.Air:
                        break;
                    case BaseChunkShape.TileTypes.GroundN:
                        worldGenerator.CreatePlatform(chunk, new Rect(x, y, 1, 1));
                        worldGenerator.CreateTile(chunk, biome.tileColor, x, y);
                        break;
                    default:
                        break;
                }

                //if (buffered && tileType != BaseChunkShape.TileTypes.Air)
                //    yield return new WaitForEndOfFrame();
            }
        }
        yield return null;
    }
}
