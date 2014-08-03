using UnityEngine;
using System.Collections;

public class EnemyGenerator
{
    private WorldGenerator worldGenerator;
    public Object enemyTier1Prefab = Resources.Load("entities/Enemy_Tier1_Prefab");

    public EnemyGenerator()
    {
        worldGenerator = GameManager.Instance.WorldGenerator;
    }

    public void Generate(GameObject chunk, float chunkWidth, BaseChunkShape chunkShape, BaseTileSet tileSet, bool buffered)
    {
        //if (Random.Range(0, 2) == 0)
        //{
        var obj = worldGenerator.CreateTile(chunk, enemyTier1Prefab, 27, 4);
        var animator = obj.GetComponentInChildren<Animator>();
        //}
    }
}
