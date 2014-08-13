using UnityEngine;
using System.Collections;

public interface IChunkGenerator
{
    void Generate(GameObject chunk, BaseChunkShape chunkShape, BaseBiome biome, bool buffered);
}
