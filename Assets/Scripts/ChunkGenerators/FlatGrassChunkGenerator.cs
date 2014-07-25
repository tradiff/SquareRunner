using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Assets.Scripts.ChunkGenerators
{
    public class FlatGrassChunkGenerator : IChunkGenerator
    {
        public GameObject Generate(GameObject chunk, float chunkWidth)
        {
            var groundPrefab = Resources.Load("tiles/Ground_88_Prefab");

            for (int i = 0; i < chunkWidth; i++)
            {
                var tile = (GameObject)UnityEngine.Object.Instantiate(groundPrefab, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
                tile.transform.parent = chunk.transform;
                tile.transform.localPosition = new Vector3(1 * i, -1, 0);
            }

            return chunk;
        }
    }
}
