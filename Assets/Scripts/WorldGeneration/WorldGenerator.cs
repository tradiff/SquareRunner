using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class WorldGenerator : MonoBehaviour
{
    private Object worldChunkPrefab;
    private Object platformPrefab;
    private Object tilePrefab;

    private GameObject newestChunk = null;
    public float chunkWidth = 50;
    private List<BaseChunkShape> chunkShapes = new List<BaseChunkShape>();
    private List<BaseTileSet> tileSets = new List<BaseTileSet>();
    private ChunkGenerator chunkGenerator;
    private CoinGenerator coinGenerator;
    private PowerupGenerator powerupGenerator;

    void Start()
    {
        GameManager.Instance.WorldGenerator = this;
        chunkGenerator = new ChunkGenerator();
        coinGenerator = new CoinGenerator();
        powerupGenerator = new PowerupGenerator();
        worldChunkPrefab = Resources.Load("WorldChunkPrefab");
        platformPrefab = Resources.Load("Platform_Prefab");
        tilePrefab = Resources.Load("Tile_Prefab");

        chunkShapes.Add(new FlatShape());
        chunkShapes.Add(new GapyShape1());
        chunkShapes.Add(new GapyShape2());

        tileSets.Add(new ForestTileSet());
        tileSets.Add(new GhostTileSet());

        Reset();
    }

    void FixedUpdate()
    {
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

        GenerateWorldChunk(-50, false);
        GenerateWorldChunk(0, false);

    }

    private void GenerateWorldChunk(float positionX, bool buffered)
    {
        Debug.Log("new chunk");
        var eligibleChunkShapes = chunkShapes.Where(x => GameManager.Instance.distanceTraveled >= x.Difficulty - 1).ToList();

        if (eligibleChunkShapes.Count > 0)
        {
            var chunk = (GameObject)Instantiate(worldChunkPrefab, new Vector3(positionX, 0, 0), new Quaternion(0, 0, 0, 0));
            var shape = eligibleChunkShapes[Random.Range(0, eligibleChunkShapes.Count)];
            var tileSet = tileSets[Random.Range(0, tileSets.Count)];
            chunkGenerator.Generate(chunk, chunkWidth, shape, tileSet, buffered);
            coinGenerator.Generate(chunk, chunkWidth, shape, tileSet, buffered);
            powerupGenerator.Generate(chunk, chunkWidth, shape, tileSet, buffered);

            newestChunk = chunk;
        }

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
        var tile = (GameObject)UnityEngine.Object.Instantiate(prefab, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
        tile.transform.parent = chunk.transform;
        tile.transform.localPosition = new Vector3(x, y, 0);
        return tile;
    }

    public void CreateTile(GameObject chunk, Sprite sprite, float x, float y = -1)
    {
        var tile = CreateTile(chunk, tilePrefab, x, y);
        var sr = tile.GetComponentInChildren<SpriteRenderer>();
        sr.sprite = sprite;
    }

    public void CreatePlatform(GameObject chunk, Rect rect)
    {
        var tile = (GameObject)UnityEngine.Object.Instantiate(platformPrefab, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
        tile.transform.parent = chunk.transform;
        tile.transform.localPosition = new Vector3(rect.x, rect.y, 0);
        tile.transform.localScale = new Vector3(rect.width, tile.transform.localScale.y, tile.transform.localScale.z);
    }

    public void CreateBG(GameObject chunk, Object prefab)
    {
        var tile = (GameObject)UnityEngine.Object.Instantiate(prefab, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
        tile.transform.parent = chunk.transform;
        tile.transform.localPosition = new Vector3(0, 0, 0);
    }


}
