using Assets.Scripts.ChunkGenerators;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WorldGenerator : MonoBehaviour
{
    private Object worldChunkPrefab;
    private Object platformPrefab;

    private GameObject newestChunk = null;
    public float chunkWidth = 50;
    private List<IChunkGenerator> chunkGenerators = new List<IChunkGenerator>();
    private bool bumpyAdded = false;

    void Start()
    {
        GameManager.Instance.WorldGenerator = this;
        worldChunkPrefab = Resources.Load("WorldChunkPrefab");
        platformPrefab = Resources.Load("Platform_Prefab");
        Reset();
    }

    void FixedUpdate()
    {
        if (GameManager.Instance.distanceTraveled > 10 && !bumpyAdded)
        {
            bumpyAdded = true;
            chunkGenerators.Add(new BumpyGrassChunkGenerator());
        }

        if (newestChunk == null)
        {
            GenerateWorldChunk(-50, true);
        }
        if (Camera.main.transform.position.x > newestChunk.transform.position.x)
        {
            var chunkRight = newestChunk.transform.position.x + chunkWidth;
            GenerateWorldChunk(chunkRight, true);
        }
    }

    public void Reset()
    {
        newestChunk = null;
        chunkGenerators.Clear();
        chunkGenerators.Add(new FlatGrassChunkGenerator());
        chunkGenerators.Add(new FlatGhostFloorGenerator());
        GenerateWorldChunk(-50, false);
        GenerateWorldChunk(0, false);
    }

    private void GenerateWorldChunk(float positionX, bool buffered)
    {
        Debug.Log("new chunk");
        var chunk = (GameObject)Instantiate(worldChunkPrefab, new Vector3(positionX, 0, 0), new Quaternion(0, 0, 0, 0));

        IChunkGenerator gen = chunkGenerators[Random.Range(0, chunkGenerators.Count)];
        gen.Generate(chunk, chunkWidth, buffered);

        newestChunk = chunk;
    }

    public void StartChildCoroutine(IEnumerator coroutineMethod)
    {
        StartCoroutine(coroutineMethod);
    }

    public void CreateTile(GameObject chunk, Object prefab, float x, float y = -1)
    {
        var tile = (GameObject)UnityEngine.Object.Instantiate(prefab, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
        tile.transform.parent = chunk.transform;
        tile.transform.localPosition = new Vector3(x, y, 0);
    }

    public void CreatePlatform(GameObject chunk, Rect rect)
    {
        var tile = (GameObject)UnityEngine.Object.Instantiate(platformPrefab, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
        tile.transform.parent = chunk.transform;
        tile.transform.localPosition = new Vector3(rect.x, rect.y, 0);
        tile.transform.localScale = new Vector3(rect.width, tile.transform.localScale.y, tile.transform.localScale.z);
    }

    public void CreateBG(GameObject chunk, Object prefab, float x, float y = -1)
    {
        var tile = (GameObject)UnityEngine.Object.Instantiate(prefab, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
        tile.transform.parent = chunk.transform;
        tile.transform.localPosition = new Vector3(0, -1, 0);
    }


}
