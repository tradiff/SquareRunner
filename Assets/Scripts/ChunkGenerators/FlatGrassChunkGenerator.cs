using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Assets.Scripts.ChunkGenerators
{
    public class FlatGrassChunkGenerator : IChunkGenerator
    {
        private WorldGenerator worldGenerator;
        Object bgPrefab = Resources.Load("backgrounds/Background_Forest_Prefab");
        Object groundPrefab = Resources.Load("tiles/Grass_N_Prefab");
        Object redMushroomPrefab = Resources.Load("Red_Mushroom_Prefab");
        Object koopaGreenPrefab = Resources.Load("Koopa_Green_Prefab");
        Object coinPrefab = Resources.Load("entities/Coin_Prefab");

        public void Generate(GameObject chunk, float chunkWidth, bool buffered)
        {
            worldGenerator = GameManager.Instance.WorldGenerator;
            worldGenerator.StartChildCoroutine(GenerateCoroutine(chunk, chunkWidth, buffered));
        }


        private IEnumerator GenerateCoroutine(GameObject chunk, float chunkWidth, bool buffered)
        {
            worldGenerator.CreateBG(chunk, bgPrefab, -1, -1);

            {
                worldGenerator.CreatePlatform(chunk, new Rect(0, -1, chunkWidth, 1));

                for (int i = 0; i < chunkWidth; i++)
                {
                    worldGenerator.CreateTile(chunk, groundPrefab, i);
                    if (buffered)
                        yield return new WaitForEndOfFrame();
                }
            }

            worldGenerator.CreatePlatform(chunk, new Rect(0, 2, 1, 1));
            worldGenerator.CreateTile(chunk, groundPrefab, 0, 2);

            {
                for (int i = 10; i < 20; i++)
                {
                    worldGenerator.CreateTile(chunk, coinPrefab, i, 4);
                    worldGenerator.CreateTile(chunk, coinPrefab, i, 5);
                    if (buffered)
                        yield return new WaitForEndOfFrame();
                }
            }

            if (Random.Range(0, 3) == 0)
            {
                worldGenerator.CreatePlatform(chunk, new Rect(3, 2, 37, 1));
                for (int i = 3; i < 40; i++)
                {
                    worldGenerator.CreateTile(chunk, groundPrefab, i, 2);
                    if (buffered)
                        yield return new WaitForEndOfFrame();
                }
            }

            if (Random.Range(0, 3) == 0)
            {
                worldGenerator.CreateTile(chunk, redMushroomPrefab, chunkWidth / 2, 1);
            }

            if (Random.Range(0, 0) == 0)
            {
                worldGenerator.CreateTile(chunk, koopaGreenPrefab, chunkWidth / 2 + 3, 1);
            }
        }
    }
}
