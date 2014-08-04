﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class WorldGenerator : MonoBehaviour
{
    private Object worldChunkPrefab;
    private Object platformPrefab;
    private Object tilePrefab;

    private int lastChunkPosX;
    public float chunkWidth;
    private List<BaseChunkShape> chunkShapes;
    private List<BaseTileSet> tileSets;
    private ChunkGenerator chunkGenerator;
    private CoinGenerator coinGenerator;
    private PowerupGenerator powerupGenerator;
    private EnemyGenerator enemyGenerator;
    private bool readyForChunks;
    private BaseTileSet lastTileSet;

    void Start()
    {
        GameManager.Instance.WorldGenerator = this;

        lastChunkPosX = -1;
        ///newestChunk = null;
        readyForChunks = false;
        chunkWidth = 50;
        chunkShapes = new List<BaseChunkShape>();
        tileSets = new List<BaseTileSet>();
        chunkGenerator = new ChunkGenerator();
        coinGenerator = new CoinGenerator();
        powerupGenerator = new PowerupGenerator();
        enemyGenerator = new EnemyGenerator();
        worldChunkPrefab = Resources.Load("WorldChunkPrefab");
        platformPrefab = Resources.Load("Platform_Prefab");
        tilePrefab = Resources.Load("Tile_Prefab");

        chunkShapes.Add(new FlatShape());
        chunkShapes.Add(new GapyShape1());
        chunkShapes.Add(new GapyShape2());

        tileSets.Add(new ForestTileSet());
        tileSets.Add(new GhostTileSet());
        tileSets.Add(new CaveTileSet());
        tileSets.Add(new CastleTileSet());

        ResetGame();
    }

    void FixedUpdate()
    {
        if (!readyForChunks) return;

        if (lastChunkPosX == -1)
        {
            Debug.Log("lastChunkPosX was null?!");
            GenerateWorldChunk(-50, true);
        }
        if (Camera.main.transform.position.x > lastChunkPosX)
        {
            Debug.Log("camera has moved.  making a new chunk");
            GenerateWorldChunk(lastChunkPosX + (int)chunkWidth, true);
        }
    }

    public void ResetGame()
    {
        readyForChunks = false;

        var chunkObjects = GameObject.FindGameObjectsWithTag("WorldChunkPrefab");
        foreach (var chunk in chunkObjects)
        {
            GameObject.Destroy(chunk);
        }

        //lastChunkPosX = 0;
        GenerateWorldChunk(-50, false);
        GenerateWorldChunk(0, false);
        readyForChunks = true;
    }

    private void GenerateWorldChunk(float positionX, bool buffered)
    {
        Debug.Log("new chunk at " + positionX);
        var eligibleChunkShapes = chunkShapes.Where(x => GameManager.Instance.distanceTraveled >= x.Difficulty - 1).ToList();

        var chunk = (GameObject)Instantiate(worldChunkPrefab, new Vector3(positionX, 0, 0), new Quaternion(0, 0, 0, 0));
        var shape = eligibleChunkShapes.RandomElement();
        BaseTileSet tileSet;
        if (lastTileSet == null || lastTileSet.IsSpecial)
            tileSet = new ForestTileSet();
        else
            tileSet = tileSets.Choose();
        chunkGenerator.Generate(chunk, chunkWidth, shape, tileSet, buffered);
        coinGenerator.Generate(chunk, chunkWidth, shape, tileSet, buffered);
        powerupGenerator.Generate(chunk, chunkWidth, shape, tileSet, buffered);
        enemyGenerator.Generate(chunk, chunkWidth, shape, tileSet, buffered);

        lastChunkPosX = (int)positionX;
        lastTileSet = tileSet;
        Debug.Log("lastChunkPosX = " + lastChunkPosX);
    }

    //private void GenerateWorldChunk(float positionX, bool buffered)
    //{
    //    Debug.Log("new chunk");
    //    var chunk = (GameObject)Instantiate(worldChunkPrefab, new Vector3(positionX, 0, 0), new Quaternion(0, 0, 0, 0));

    //    IChunkGenerator gen = chunkGenerators[Random.Range(0, chunkGenerators.Count)];
    //    gen.Generate(chunk, chunkWidth, buffered);

    //    newestChunk = chunk;
    //}

    public void StartChildCoroutine(IEnumerator coroutineMethod)
    {
        StartCoroutine(coroutineMethod);
    }

    public GameObject CreateTile(GameObject chunk, Object prefab, float x, float y = -1)
    {
        if (chunk != null)
        {
            var tile = (GameObject)UnityEngine.Object.Instantiate(prefab, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
            tile.transform.parent = chunk.transform;
            tile.transform.localPosition = new Vector3(x, y, 0);
            return tile;
        }
        return null;
    }

    public GameObject CreateTile(GameObject chunk, Sprite sprite, float x, float y = -1)
    {
        var tile = CreateTile(chunk, tilePrefab, x, y);
        if (tile != null)
        {
            var sr = tile.GetComponentInChildren<SpriteRenderer>();
            sr.sprite = sprite;
        }
        return tile;
    }

    public void CreatePlatform(GameObject chunk, Rect rect)
    {
        if (chunk != null)
        {
            var tile = (GameObject)UnityEngine.Object.Instantiate(platformPrefab, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
            tile.transform.parent = chunk.transform;
            tile.transform.localPosition = new Vector3(rect.x, rect.y, 0);
            tile.transform.localScale = new Vector3(rect.width, tile.transform.localScale.y, tile.transform.localScale.z);
        }
    }

    public void CreateBG(GameObject chunk, Object prefab)
    {
        if (chunk != null)
        {
            var tile = (GameObject)UnityEngine.Object.Instantiate(prefab, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
            tile.transform.parent = chunk.transform;
            tile.transform.localPosition = new Vector3(0, 0, 0);
        }
    }


}