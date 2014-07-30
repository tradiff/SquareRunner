using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Assets.Scripts.ChunkGenerators
{
    public class FlatGrassChunkGenerator : MonoBehaviour, IChunkGenerator
    {
        Object groundPrefab = Resources.Load("tiles/Grass_N_Prefab");
        Object redMushroomPrefab = Resources.Load("Red_Mushroom_Prefab");
        Object koopaGreenPrefab = Resources.Load("Koopa_Green_Prefab");

        public void Generate(GameObject chunk, float chunkWidth, bool buffered)
        {
            GameManager.Instance.WorldGenerator.StartChildCoroutine(GenerateCoroutine(chunk, chunkWidth, buffered));
        }


        private IEnumerator GenerateCoroutine(GameObject chunk, float chunkWidth, bool buffered)
        {
            
            for (int i = 0; i < chunkWidth; i++)
            {
                CreateTile(chunk, groundPrefab, i);
                if (buffered)
                    yield return new WaitForEndOfFrame();
            }

            for (int i = 3; i < 40; i++)
            {
                CreateTile(chunk, groundPrefab, i, 2);
                if (buffered)
                    yield return new WaitForEndOfFrame();
            }

            if (Random.Range(0, 3) == 0)
            {
                CreateTile(chunk, redMushroomPrefab, chunkWidth / 2, 1);
            }
            yield return null;

            if (Random.Range(0, 0) == 0)
            {
                CreateTile(chunk, koopaGreenPrefab, chunkWidth / 2 + 3, 1); 
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
