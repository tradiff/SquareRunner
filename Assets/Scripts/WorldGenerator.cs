using Assets.Scripts.ChunkGenerators;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WorldGenerator : MonoBehaviour
{
    private Object worldChunkPrefab;

    private GameObject newestChunk = null;
    private float chunkWidth = 50;
    private List<IChunkGenerator> chunkGenerators = new List<IChunkGenerator>();
    private bool bumpyAdded = false;
    private float nextBGposition = -50;

    void Start()
    {
        GameManager.Instance.WorldGenerator = this;
        worldChunkPrefab = Resources.Load("WorldChunkPrefab");
        Reset();
    }

    void FixedUpdate()
    {
        if (GameManager.Instance.distanceTraveled > 10 && !bumpyAdded)
        {
            bumpyAdded = true;
            //chunkGenerators.Add(new BumpyGrassChunkGenerator());
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

    private int bgCount = 0;
    public void CreateBG(GameObject chunk, Object prefab, float x, float y = -1)
    {
        //if (bgCount++ != 2) return;
        var tile = (GameObject)UnityEngine.Object.Instantiate(prefab, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
        tile.transform.parent = chunk.transform;
        tile.transform.localPosition = new Vector3(7, -1, 0);
        //nextBGposition = nextBGposition + chunkWidth;
    }


}
