using UnityEngine;
using System.Collections;

public interface IChunkGenerator
{
    void Generate(WorldChunk chunk, bool buffered);
}
