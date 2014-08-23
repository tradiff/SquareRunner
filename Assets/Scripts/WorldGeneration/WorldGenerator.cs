using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class WorldGenerator : MonoBehaviour
{
    private Object worldChunkPrefab;
    private Object platformPrefab;
    private Object tilePrefab;
    private Object speedIncreasePrefab;

    private int lastChunkPosX;
    public float chunkWidth;
    public float chunkHeight;
    private List<BaseChunkShape> chunkShapes;
    private List<BaseBiome> biomes;
    private ChunkGenerator chunkGenerator;
    private CoinGenerator coinGenerator;
    private PowerupGenerator powerupGenerator;
    private EnemyGenerator enemyGenerator;
    private bool readyForChunks;
    private BaseBiome lastBiome;

    void Start()
    {
        GameManager.Instance.WorldGenerator = this;

        lastChunkPosX = -1;
        readyForChunks = false;
        chunkWidth = 50;
        chunkHeight = 14;
        chunkShapes = new List<BaseChunkShape>();
        biomes = new List<BaseBiome>();
        chunkGenerator = new ChunkGenerator();
        coinGenerator = new CoinGenerator();
        powerupGenerator = new PowerupGenerator();
        enemyGenerator = new EnemyGenerator();
        worldChunkPrefab = Resources.Load("WorldChunkPrefab");
        platformPrefab = Resources.Load("Platform_Prefab");
        tilePrefab = Resources.Load("Tile_Prefab");
        speedIncreasePrefab = Resources.Load("SpeedChangeTrigger_Prefab");

        chunkShapes.Add(new FlatShape());
        chunkShapes.Add(new FlatShape2());
        chunkShapes.Add(new PowerupShape1());
        chunkShapes.Add(new GapyShape1());
        chunkShapes.Add(new GapyShape2());
        chunkShapes.Add(new ChrisShape1());

        biomes.Add(new GrassBiome());
        biomes.Add(new CaveBiome());
        biomes.Add(new StormyBiome());
        biomes.Add(new LavaCaveBiome());
        biomes.Add(new WesternBiome());

        GameManager.Instance.ResetGame();
    }

    void FixedUpdate()
    {
        if (!readyForChunks) return;

        if (GameManager.Instance.Area == GameManager.Areas.Bonus) return;

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

    List<GameObject> bonusWorldChunks = new List<GameObject>();
    public void GenerateBonusChunks()
    {
        bonusWorldChunks.Add(GenerateBonusWorldChunk(0, new FlatShape()));
        bonusWorldChunks.Add(GenerateBonusWorldChunk(50, null));
        bonusWorldChunks.Add(GenerateBonusWorldChunk(100, null));
        bonusWorldChunks.Add(GenerateBonusWorldChunk(150, new EmptyShape()));
    }
    public void DestroyBonusChunks()
    {
        foreach (var item in bonusWorldChunks)
        {
            Destroy(item);
        }
        bonusWorldChunks.Clear();
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
        speedIncreaseCount = 0;
        GenerateWorldChunk(-50, false);
        GenerateWorldChunk(0, false);
        readyForChunks = true;
    }

    private GameObject GenerateBonusWorldChunk(float positionX, BaseChunkShape shape)
    {
        Debug.Log("new chunk at " + positionX);
        var eligibleChunkShapes = chunkShapes.Where(x => GameManager.Instance.distanceTraveled >= x.Difficulty - 1).ToList();

        var chunk = (GameObject)Instantiate(worldChunkPrefab, new Vector3(positionX, GameManager.Instance.Area == GameManager.Areas.Bonus ? 100 : 0, 0), new Quaternion(0, 0, 0, 0));
        if (shape == null)
            shape = eligibleChunkShapes.RandomElement();

        BaseBiome biome = new BonusBiome();

        chunkGenerator.Generate(chunk, shape, biome, false);
        coinGenerator.Generate(chunk, shape, biome, false);
        powerupGenerator.Generate(chunk, shape, biome, false);
        return chunk;
    }


    private bool speedInreaseTesting = false;
    private int speedIncreaseCount = 0;
    private GameObject GenerateWorldChunk(float positionX, bool buffered)
    {
        Debug.Log("new chunk at " + positionX);
        bool speedIncreaseChunk = false;

        if (speedInreaseTesting)
        {
            if (speedIncreaseCount == 0 && GameManager.Instance.distanceTraveled > 100)
                speedIncreaseChunk = true;
            if (speedIncreaseCount == 1 && GameManager.Instance.distanceTraveled > 200)
                speedIncreaseChunk = true;
            if (speedIncreaseCount == 2 && GameManager.Instance.distanceTraveled > 300)
                speedIncreaseChunk = true;
            if (speedIncreaseCount == 3 && GameManager.Instance.distanceTraveled > 400)
                speedIncreaseChunk = true;
        }
        else
        {
            if (speedIncreaseCount == 0 && GameManager.Instance.distanceTraveled > 300)
                speedIncreaseChunk = true;
            if (speedIncreaseCount == 1 && GameManager.Instance.distanceTraveled > 500)
                speedIncreaseChunk = true;
            if (speedIncreaseCount == 2 && GameManager.Instance.distanceTraveled > 750)
                speedIncreaseChunk = true;
            if (speedIncreaseCount == 3 && GameManager.Instance.distanceTraveled > 1000)
                speedIncreaseChunk = true;
        }



        var eligibleChunkShapes = chunkShapes.Where(x => GameManager.Instance.distanceTraveled >= x.Difficulty - 1).ToList();

        var chunk = (GameObject)Instantiate(worldChunkPrefab, new Vector3(positionX, GameManager.Instance.Area == GameManager.Areas.Bonus ? 100 : 0, 0), new Quaternion(0, 0, 0, 0));
        BaseChunkShape shape;
        if (buffered == false || speedIncreaseChunk == true)
            shape = new FlatShape();
        else
            shape = eligibleChunkShapes.RandomElement();

        BaseBiome biome;
        if (buffered == false || lastBiome == null || lastBiome.IsSpecial)
            biome = new GrassBiome();
        else
            biome = biomes.Choose();

        chunkGenerator.Generate(chunk, shape, biome, buffered);
        coinGenerator.Generate(chunk, shape, biome, buffered);
        powerupGenerator.Generate(chunk, shape, biome, buffered);
        if (GameManager.Instance.distanceTraveled > 1 && !speedIncreaseChunk)
            enemyGenerator.Generate(chunk, shape, biome, buffered);

        if (speedIncreaseChunk)
        {
            this.CreateTile(chunk, speedIncreasePrefab, 2, 0);
            speedIncreaseCount++;
        }

        lastChunkPosX = (int)positionX;
        lastBiome = biome;
        Debug.Log("lastChunkPosX = " + lastChunkPosX);
        return chunk;
    }



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

    public GameObject CreateTile(GameObject chunk, Color color, float x, float y = -1)
    {
        var tile = CreateTile(chunk, tilePrefab, x, y);
        if (tile != null)
        {
            tile.GetComponentInChildren<SpriteRenderer>().color = color;
        }
        return tile;
    }

    public void CreateTiles(GameObject chunk, Color color, Rect rect)
    {
        if (chunk != null)
        {
            var tile = (GameObject)UnityEngine.Object.Instantiate(tilePrefab, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
            tile.transform.parent = chunk.transform;
            tile.transform.localPosition = new Vector3(rect.x, rect.y, 0);
            tile.transform.localScale = new Vector3(rect.width, rect.height, tile.transform.localScale.z);
            tile.GetComponentInChildren<SpriteRenderer>().color = color;
        }
    }

    public void CreatePlatform(GameObject chunk, Rect rect)
    {
        if (chunk != null)
        {
            var tile = (GameObject)UnityEngine.Object.Instantiate(platformPrefab, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
            tile.transform.parent = chunk.transform;
            tile.transform.localPosition = new Vector3(rect.x, rect.y, 0);
            tile.transform.localScale = new Vector3(rect.width, rect.height, tile.transform.localScale.z);
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
