using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Assets.Scripts.ChunkGenerators
{
    interface IChunkGenerator
    {
        GameObject Generate(GameObject chunk, float chunkWidth);
    }
}
