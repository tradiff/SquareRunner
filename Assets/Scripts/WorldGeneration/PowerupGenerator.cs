using UnityEngine;
using System.Collections;

public class PowerupGenerator
{
    private WorldGenerator worldGenerator;
    public Object redMushroomPrefab = Resources.Load("entities/Red_Mushroom_Prefab");

    public PowerupGenerator()
    {
        worldGenerator = GameManager.Instance.WorldGenerator;
    }

    public void Generate(GameObject chunk, float chunkWidth, BaseChunkShape chunkShape, BaseBiome biome, bool buffered)
    {
        if (Random.Range(0, 10) == 0)
        {
            worldGenerator.CreateTile(chunk, redMushroomPrefab, 35, 4);
        }
    }
}
