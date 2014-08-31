using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class WorldGenerator : MonoBehaviour
{
    private Object worldChunkPrefab;
    private Object platformPrefab;
    private Object _oneWayPlatformPrefab;
    private Object tilePrefab;
    private Object speedIncreasePrefab;

    private int lastChunkPosX;
    public float chunkWidth;
    public float chunkHeight;
    private List<BaseChunkShape> _chunkShapes;
    private List<BaseBiome> _biomes;
    private List<IChunkGenerator> _generators;
    private bool readyForChunks;
    private BaseBiome lastBiome;
    List<WorldChunk> bonusWorldChunks = new List<WorldChunk>();
    private bool speedInreaseTesting = false;
    private int speedIncreaseCount = 0;

    void Start()
    {
        GameManager.Instance.WorldGenerator = this;

        lastChunkPosX = -1;
        readyForChunks = false;
        chunkWidth = 50;
        chunkHeight = 14;

        worldChunkPrefab = Resources.Load("WorldChunkPrefab");
        platformPrefab = Resources.Load("Platform_Prefab");
        _oneWayPlatformPrefab = Resources.Load("OneWayPlatform_Prefab");
        tilePrefab = Resources.Load("Tile_Prefab");
        speedIncreasePrefab = Resources.Load("SpeedChangeTrigger_Prefab");


        _generators = new List<IChunkGenerator>();
        _generators.Add(new ChunkGenerator());
        _generators.Add(new CoinGenerator());
        _generators.Add(new PowerupGenerator());
        _generators.Add(new EnemyGenerator());

        _chunkShapes = new List<BaseChunkShape>();
        _chunkShapes.Add(new FlatShape());
        _chunkShapes.Add(new FlatShape2());
        _chunkShapes.Add(new PowerupShape1());
        _chunkShapes.Add(new GapyShape1());
        _chunkShapes.Add(new GapyShape2());
        _chunkShapes.Add(new ChrisShape1());

        _biomes = new List<BaseBiome>();
        _biomes.Add(new GrassBiome());
        _biomes.Add(new CaveBiome());
        _biomes.Add(new StormyBiome());
        _biomes.Add(new LavaCaveBiome());
        _biomes.Add(new WesternBiome());
        _biomes.Add(new SunsetBiome());

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

        speedIncreaseCount = 0;
        GenerateWorldChunk(-50, false);
        GenerateWorldChunk(0, false);
        readyForChunks = true;
    }

    private WorldChunk GenerateBonusWorldChunk(float positionX, BaseChunkShape shape)
    {
        Debug.Log("new chunk at " + positionX);
        var eligibleChunkShapes = _chunkShapes.Where(x => GameManager.Instance.distanceTraveled >= x.Difficulty - 1 && !x.CanKill).ToList();

        var chunkGO = (GameObject)Instantiate(worldChunkPrefab, new Vector3(positionX, GameManager.Instance.Area == GameManager.Areas.Bonus ? 100 : 0, 0), new Quaternion(0, 0, 0, 0));
        var chunk = chunkGO.GetComponent<WorldChunk>();
        if (shape == null)
            shape = eligibleChunkShapes.RandomElement();

        chunk.Biome = new BonusBiome();
        chunk.Shape = shape;

        foreach (var gen in _generators)
        {
            gen.Generate(chunk, false);
        }
        return chunk;
    }


    private WorldChunk GenerateWorldChunk(float positionX, bool buffered)
    {
        Debug.Log("new chunk at " + positionX);

        var eligibleChunkShapes = _chunkShapes.Where(x => GameManager.Instance.distanceTraveled >= x.Difficulty - 1).ToList();

        var chunkGO = (GameObject)Instantiate(worldChunkPrefab, new Vector3(positionX, GameManager.Instance.Area == GameManager.Areas.Bonus ? 100 : 0, 0), new Quaternion(0, 0, 0, 0));
        var chunk = chunkGO.GetComponent<WorldChunk>();
        chunk.HasSpeedIncrease = DueForSpeedIncrease();

        if (buffered == false || chunk.HasSpeedIncrease == true)
            chunk.Shape = new FlatShape();
        else
            chunk.Shape = eligibleChunkShapes.RandomElement();

        if (buffered == false || lastBiome == null || lastBiome.IsSpecial)
            chunk.Biome = new GrassBiome();
        else
            chunk.Biome = _biomes.Choose();

        foreach (var gen in _generators)
        {
            gen.Generate(chunk, false);
        }

        if (chunk.HasSpeedIncrease)
        {
            this.CreateTile(chunk.gameObject, speedIncreasePrefab, 2, 0);
            speedIncreaseCount++;
        }

        lastChunkPosX = (int)positionX;
        lastBiome = chunk.Biome;
        Debug.Log("lastChunkPosX = " + lastChunkPosX);
        chunk.Biome.UpdateChunk(chunk);
        return chunk;
    }

    private bool DueForSpeedIncrease()
    {
        if (speedInreaseTesting)
        {
            if (speedIncreaseCount == 0 && GameManager.Instance.distanceTraveled > 100)
                return true;
            if (speedIncreaseCount == 1 && GameManager.Instance.distanceTraveled > 200)
                return true;
            if (speedIncreaseCount == 2 && GameManager.Instance.distanceTraveled > 300)
                return true;
            if (speedIncreaseCount == 3 && GameManager.Instance.distanceTraveled > 400)
                return true;
        }
        else
        {
            if (speedIncreaseCount == 0 && GameManager.Instance.distanceTraveled > 300)
                return true;
            if (speedIncreaseCount == 1 && GameManager.Instance.distanceTraveled > 500)
                return true;
            if (speedIncreaseCount == 2 && GameManager.Instance.distanceTraveled > 750)
                return true;
            if (speedIncreaseCount == 3 && GameManager.Instance.distanceTraveled > 1000)
                return true;
        }
        return false;
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

    public GameObject CreateTiles(GameObject chunk, Object prefab, Rect rect)
    {
        if (chunk != null)
        {
            var tile = (GameObject)UnityEngine.Object.Instantiate(prefab, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
            tile.transform.parent = chunk.transform;
            tile.transform.localPosition = new Vector3(rect.x, rect.y, 0);
            tile.transform.localScale = new Vector3(rect.width, rect.height, tile.transform.localScale.z);
            return tile;
        }
        return null;
    }

    public GameObject CreateTiles(GameObject chunk, Color color, Rect rect)
    {
        var tile = CreateTiles(chunk, tilePrefab, rect);
        if (tile != null)
        {
            tile.GetComponentInChildren<SpriteRenderer>().color = color;
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
            tile.transform.localScale = new Vector3(rect.width, rect.height, tile.transform.localScale.z);
        }
    }
    public void CreateOneWayPlatform(GameObject chunk, Rect rect)
    {
        if (chunk != null)
        {
            var tile = (GameObject)UnityEngine.Object.Instantiate(_oneWayPlatformPrefab, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
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
