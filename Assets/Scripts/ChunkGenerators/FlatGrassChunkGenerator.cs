using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Assets.Scripts.ChunkGenerators
{
    public class FlatGrassChunkGenerator : IChunkGenerator
    {
        Object groundPrefab = Resources.Load("tiles/Grass_N_Prefab");
        Object redMushroomPrefab = Resources.Load("Red_Mushroom_Prefab");
        Object koopaGreenPrefab = Resources.Load("Koopa_Green_Prefab");

        public GameObject Generate(GameObject chunk, float chunkWidth)
        {
            for (int i = 0; i < chunkWidth; i++)
            {
                var tile = (GameObject)UnityEngine.Object.Instantiate(groundPrefab, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
                tile.transform.parent = chunk.transform;
                tile.transform.localPosition = new Vector3(1 * i, -1, 0);
            }

            for (int i = 6; i < 12; i++)
            {
                var tile = (GameObject)UnityEngine.Object.Instantiate(groundPrefab, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
                tile.transform.parent = chunk.transform;
                tile.transform.localPosition = new Vector3(1 * i, 3, 0);
            }

            if (Random.Range(0, 3) == 0)
            {
                var shroom = (GameObject)UnityEngine.Object.Instantiate(redMushroomPrefab, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
                shroom.transform.parent = chunk.transform;
                shroom.transform.localPosition = new Vector3(chunkWidth / 2, 1, 0);
            }

            if (Random.Range(0, 0) == 0)
            {
                var enemy = (GameObject)UnityEngine.Object.Instantiate(koopaGreenPrefab, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
                enemy.transform.parent = chunk.transform;
                enemy.transform.localPosition = new Vector3(chunkWidth / 2 + 3, 1, 0);
            }


            return chunk;
        }
    }
}
