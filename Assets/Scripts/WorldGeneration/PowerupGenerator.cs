using UnityEngine;
using System.Collections;

public class PowerupGenerator : IChunkGenerator
{
    private WorldGenerator worldGenerator;
    public Object redMushroomPrefab = Resources.Load("entities/Red_Mushroom_Prefab");

    public PowerupGenerator()
    {
        worldGenerator = GameManager.Instance.WorldGenerator;
    }

    public void Generate(GameObject chunk, BaseChunkShape chunkShape, BaseBiome biome, bool buffered)
    {
        if (Random.Range(0, 10) == 0)
        {
            worldGenerator.CreateTile(chunk, redMushroomPrefab, 35, 4);
        }
    }
}
