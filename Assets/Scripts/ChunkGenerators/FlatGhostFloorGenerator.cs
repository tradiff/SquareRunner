using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Assets.Scripts.ChunkGenerators
{
    public class FlatGhostFloorGenerator : IChunkGenerator
    {
        private WorldGenerator worldGenerator;
        Object bgPrefab = Resources.Load("backgrounds/Background_Ghost_Prefab");
        Object groundPrefab = Resources.Load("tiles/Ghost_Floor_Prefab");
        Object bgWallPrefab = Resources.Load("tiles/Ghost_BG_Wall_Prefab");

        public void Generate(GameObject chunk, float chunkWidth, bool buffered)
        {
            worldGenerator = GameManager.Instance.WorldGenerator;
            worldGenerator.StartChildCoroutine(GenerateCoroutine(chunk, chunkWidth, buffered));
        }


        private IEnumerator GenerateCoroutine(GameObject chunk, float chunkWidth, bool buffered)
        {
            worldGenerator.CreateBG(chunk, bgPrefab, -1, -1);

            // pass-through walls
            for (int i = -1; i < 100; i++)
            {
                worldGenerator.CreateTile(chunk, bgWallPrefab, 0, i);
                worldGenerator.CreateTile(chunk, bgWallPrefab, 1, i);
            }

            {
                worldGenerator.CreatePlatform(chunk, new Rect(0, -1, chunkWidth, 1));
                for (int i = 0; i < chunkWidth; i++)
                {
                    worldGenerator.CreateTile(chunk, groundPrefab, i);
                    if (buffered)
                        yield return new WaitForEndOfFrame();
                }
            }

            {
                worldGenerator.CreatePlatform(chunk, new Rect(0, 2, 2, 1));
                worldGenerator.CreateTile(chunk, groundPrefab, 0, 2);
                worldGenerator.CreateTile(chunk, groundPrefab, 1, 2);
            }

        }

    }
}
