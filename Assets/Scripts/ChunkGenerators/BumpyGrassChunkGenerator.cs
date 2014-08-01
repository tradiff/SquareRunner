using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Assets.Scripts.ChunkGenerators
{
    public class BumpyGrassChunkGenerator : IChunkGenerator
    {
        private WorldGenerator worldGenerator;
        Object bgPrefab = Resources.Load("backgrounds/Background_Forest_Prefab");
        Object groundPrefab = Resources.Load("tiles/Grass_N_Prefab");
        Object rightCornerPrefab = Resources.Load("tiles/Grass_NE_Prefab");
        Object leftCornerPrefab = Resources.Load("tiles/Grass_NW_Prefab");

        public void Generate(GameObject chunk, float chunkWidth, bool buffered)
        {
            worldGenerator = GameManager.Instance.WorldGenerator;
            worldGenerator.StartChildCoroutine(GenerateCoroutine(chunk, chunkWidth, buffered));
        }

        private IEnumerator GenerateCoroutine(GameObject chunk, float chunkWidth, bool buffered)
        {
            worldGenerator.CreateBG(chunk, bgPrefab, -1, -1);


            int i = 0;
            int iPlatformStart = 0;
            int iPlatformEnd = 0;

            iPlatformStart = i;
            worldGenerator.CreateTile(chunk, groundPrefab, i++);
            if (buffered)
                yield return new WaitForEndOfFrame();
            worldGenerator.CreateTile(chunk, groundPrefab, i++);
            if (buffered)
                yield return new WaitForEndOfFrame();
            worldGenerator.CreateTile(chunk, groundPrefab, i++);
            if (buffered)
                yield return new WaitForEndOfFrame();
            worldGenerator.CreateTile(chunk, rightCornerPrefab, i++);
            if (buffered)
                yield return new WaitForEndOfFrame();

            iPlatformEnd = i;
            worldGenerator.CreatePlatform(chunk, new Rect(iPlatformStart, -1, iPlatformEnd - iPlatformStart, 1));


            i++;
            i++;
            i++;
            i++;


            iPlatformStart = i;
            worldGenerator.CreateTile(chunk, leftCornerPrefab, i++);
            if (buffered)
                yield return new WaitForEndOfFrame();

            worldGenerator.CreateTile(chunk, groundPrefab, i++);
            if (buffered)
                yield return new WaitForEndOfFrame();
            worldGenerator.CreateTile(chunk, groundPrefab, i++);
            if (buffered)
                yield return new WaitForEndOfFrame();
            worldGenerator.CreateTile(chunk, groundPrefab, i++);
            if (buffered)
                yield return new WaitForEndOfFrame();
            worldGenerator.CreateTile(chunk, rightCornerPrefab, i++);
            if (buffered)
                yield return new WaitForEndOfFrame();
            iPlatformEnd = i;
            worldGenerator.CreatePlatform(chunk, new Rect(iPlatformStart, -1, iPlatformEnd - iPlatformStart, 1));
            i++;
            i++;
            i++;
            i++;


            iPlatformStart = i;
            worldGenerator.CreateTile(chunk, leftCornerPrefab, i++);
            if (buffered)
                yield return new WaitForEndOfFrame();

            while (i < chunkWidth)
            {
                worldGenerator.CreateTile(chunk, groundPrefab, i++);
                if (buffered)
                    yield return new WaitForEndOfFrame();
            }
            iPlatformEnd = i;
            worldGenerator.CreatePlatform(chunk, new Rect(iPlatformStart, -1, iPlatformEnd - iPlatformStart, 1));

        }
    }
}
