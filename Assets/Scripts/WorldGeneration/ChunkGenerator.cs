using UnityEngine;
using System.Collections;

public class ChunkGenerator
{
    private WorldGenerator worldGenerator;

    public ChunkGenerator()
    {
        worldGenerator = GameManager.Instance.WorldGenerator;
    }

    public void Generate(GameObject chunk, float chunkWidth, BaseChunkShape chunkShape, BaseTileSet tileSet, bool buffered)
    {
        worldGenerator.StartChildCoroutine(GenerateCoroutine(chunk, chunkWidth, chunkShape, tileSet, buffered));
    }

    public IEnumerator GenerateCoroutine(GameObject chunk, float chunkWidth, BaseChunkShape chunkShape, BaseTileSet tileSet, bool buffered)
    {
        worldGenerator.CreateBG(chunk, tileSet.bgPrefab);

        if (tileSet.NeedsTransition)
        {
            for (int y = 0; y < chunkShape.Map.GetLength(1); y++)
            {
                var tile = worldGenerator.CreateTile(chunk, tileSet.transitionTile, -1, y);
                var sr = tile.GetComponentInChildren<SpriteRenderer>();
                sr.sortingLayerName = "Background";
                sr.sortingOrder = 2;
                var tile2 = worldGenerator.CreateTile(chunk, tileSet.transitionTile, worldGenerator.chunkWidth, y);
                var sr2 = tile2.GetComponentInChildren<SpriteRenderer>();
                sr2.sortingLayerName = "Background";
                sr2.sortingOrder = 2;

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
                    case BaseChunkShape.TileTypes.GroundNW:
                        worldGenerator.CreatePlatform(chunk, new Rect(x, y, 1, 1));
                        worldGenerator.CreateTile(chunk, tileSet.groundNW, x, y);
                        break;
                    case BaseChunkShape.TileTypes.GroundN:
                        worldGenerator.CreatePlatform(chunk, new Rect(x, y, 1, 1));
                        worldGenerator.CreateTile(chunk, tileSet.groundN, x, y);
                        break;
                    case BaseChunkShape.TileTypes.GroundNE:
                        worldGenerator.CreatePlatform(chunk, new Rect(x, y, 1, 1));
                        worldGenerator.CreateTile(chunk, tileSet.groundNE, x, y);
                        break;
                    case BaseChunkShape.TileTypes.GroundW:
                        worldGenerator.CreateTile(chunk, tileSet.groundW, x, y);
                        break;
                    case BaseChunkShape.TileTypes.GroundC:
                        worldGenerator.CreateTile(chunk, tileSet.groundC, x, y);
                        break;
                    case BaseChunkShape.TileTypes.GroundE:
                        worldGenerator.CreateTile(chunk, tileSet.groundE, x, y);
                        break;
                    case BaseChunkShape.TileTypes.GroundSW:
                        worldGenerator.CreateTile(chunk, tileSet.groundSW, x, y);
                        break;
                    case BaseChunkShape.TileTypes.GroundS:
                        worldGenerator.CreateTile(chunk, tileSet.groundS, x, y);
                        break;
                    case BaseChunkShape.TileTypes.GroundSE:
                        worldGenerator.CreateTile(chunk, tileSet.groundSE, x, y);
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
