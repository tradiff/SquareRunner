using UnityEngine;
using System.Collections;

public class PowerupGenerator
{
    private WorldGenerator worldGenerator;
    public Object redMushroomPrefab = Resources.Load("Red_Mushroom_Prefab");
    //public Object koopaGreenPrefab = Resources.Load("Koopa_Green_Prefab");

    public PowerupGenerator()
    {
        worldGenerator = GameManager.Instance.WorldGenerator;
    }

    public void Generate(GameObject chunk, float chunkWidth, BaseChunkShape chunkShape, BaseTileSet tileSet, bool buffered)
    {
        if (Random.Range(0, 10) == 0)
        {
            worldGenerator.CreateTile(chunk, redMushroomPrefab, 25, 4);
        }
    }
}
