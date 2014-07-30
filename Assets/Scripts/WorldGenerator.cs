﻿using Assets.Scripts.ChunkGenerators;
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


}
