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

            for (int i = -1; i < 100; i++)
            {
                worldGenerator.CreateTile(chunk, bgWallPrefab, 0, i);
                worldGenerator.CreateTile(chunk, bgWallPrefab, 1, i);
            }

            
            for (int i = 0; i < chunkWidth; i++)
            {
                worldGenerator.CreateTile(chunk, groundPrefab, i);
                if (buffered)
                    yield return new WaitForEndOfFrame();
            }

            worldGenerator.CreateTile(chunk, groundPrefab, 0, 2);
            worldGenerator.CreateTile(chunk, groundPrefab, 1, 2);

            //for (int i = 3; i < 40; i++)
            //{
            //    worldGenerator.CreateTile(chunk, groundPrefab, i, 2);
            //    if (buffered)
            //        yield return new WaitForEndOfFrame();
            //}

            //if (Random.Range(0, 3) == 0)
            //{
            //    worldGenerator.CreateTile(chunk, redMushroomPrefab, chunkWidth / 2, 1);
            //}
            //yield return null;

            //if (Random.Range(0, 0) == 0)
            //{
            //    worldGenerator.CreateTile(chunk, koopaGreenPrefab, chunkWidth / 2 + 3, 1); 
            //}
        }

    }
}
