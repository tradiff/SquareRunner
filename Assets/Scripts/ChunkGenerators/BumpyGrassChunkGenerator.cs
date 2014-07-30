using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Assets.Scripts.ChunkGenerators
{
    public class BumpyGrassChunkGenerator : IChunkGenerator
    {
        public void Generate(GameObject chunk, float chunkWidth, bool buffered)
        {
            GameManager.Instance.WorldGenerator.StartChildCoroutine(GenerateCoroutine(chunk, chunkWidth, buffered));
        }

        private IEnumerator GenerateCoroutine(GameObject chunk, float chunkWidth, bool buffered)
        {

            var groundPrefab = Resources.Load("tiles/Grass_N_Prefab");
            var rightCornerPrefab = Resources.Load("tiles/Grass_NE_Prefab");
            var leftCornerPrefab = Resources.Load("tiles/Grass_NW_Prefab");

            int i = 0;

            CreateTile(chunk, groundPrefab, i++);
            if (buffered)
                yield return new WaitForEndOfFrame();
            CreateTile(chunk, groundPrefab, i++);
            if (buffered)
                yield return new WaitForEndOfFrame();
            CreateTile(chunk, groundPrefab, i++);
            if (buffered)
                yield return new WaitForEndOfFrame();
            CreateTile(chunk, rightCornerPrefab, i++);
            if (buffered)
                yield return new WaitForEndOfFrame();
            i++;
            i++;
            i++;
            i++;
            CreateTile(chunk, leftCornerPrefab, i++);
            if (buffered)
                yield return new WaitForEndOfFrame();

            CreateTile(chunk, groundPrefab, i++);
            if (buffered)
                yield return new WaitForEndOfFrame();
            CreateTile(chunk, groundPrefab, i++);
            if (buffered)
                yield return new WaitForEndOfFrame();
            CreateTile(chunk, groundPrefab, i++);
            if (buffered)
                yield return new WaitForEndOfFrame();
            CreateTile(chunk, rightCornerPrefab, i++);
            if (buffered)
                yield return new WaitForEndOfFrame();
            i++;
            i++;
            i++;
            i++;
            CreateTile(chunk, leftCornerPrefab, i++);
            if (buffered)
                yield return new WaitForEndOfFrame();

            while (i < chunkWidth)
            {
                CreateTile(chunk, groundPrefab, i++);
                if (buffered)
                    yield return new WaitForEndOfFrame();
            }

        }

        private void CreateTile(GameObject chunk, Object prefab, float x, float y = -1)
        {
            var tile = (GameObject)UnityEngine.Object.Instantiate(prefab, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
            tile.transform.parent = chunk.transform;
            tile.transform.localPosition = new Vector3(x, y, 0);
        }
    }
}
