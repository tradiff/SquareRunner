using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Assets.Scripts.ChunkGenerators
{
    interface IChunkGenerator
    {
        void Generate(GameObject chunk, float chunkWidth, bool buffered);
    }
}
