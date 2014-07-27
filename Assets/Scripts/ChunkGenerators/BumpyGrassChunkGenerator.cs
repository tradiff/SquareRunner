using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Assets.Scripts.ChunkGenerators
{
    public class BumpyGrassChunkGenerator : IChunkGenerator
    {
        public GameObject Generate(GameObject chunk, float chunkWidth)
        {
            var groundPrefab = Resources.Load("tiles/Grass_N_Prefab");
            var rightCornerPrefab = Resources.Load("tiles/Grass_NE_Prefab");
            var leftCornerPrefab = Resources.Load("tiles/Grass_NW_Prefab");

            int i = 0;

            CreateTile(chunk, groundPrefab, i++);
            CreateTile(chunk, groundPrefab, i++);
            CreateTile(chunk, groundPrefab, i++);
            CreateTile(chunk, rightCornerPrefab, i++);
            i++;
            i++;
            i++;
            i++;
            CreateTile(chunk, leftCornerPrefab, i++);

            CreateTile(chunk, groundPrefab, i++);
            CreateTile(chunk, groundPrefab, i++);
            CreateTile(chunk, groundPrefab, i++);
            CreateTile(chunk, rightCornerPrefab, i++);
            i++;
            i++;
            i++;
            i++;
            CreateTile(chunk, leftCornerPrefab, i++);

            while (i < chunkWidth)
            {
                CreateTile(chunk, groundPrefab, i++);
            }

            return chunk;
        }

        private void CreateTile(GameObject chunk, Object prefab, int i)
        {
            var tile = (GameObject)UnityEngine.Object.Instantiate(prefab, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
            tile.transform.parent = chunk.transform;
            tile.transform.localPosition = new Vector3(1 * i, -1, 0);
        }
    }
}
