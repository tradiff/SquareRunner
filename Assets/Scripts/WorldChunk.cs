using UnityEngine;
using System.Collections;

public class WorldChunk : MonoBehaviour
{
    public bool HasSpeedIncrease;
    public BaseChunkShape Shape;
    public BaseBiome Biome;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Player entered new chunk");
            other.GetComponent<Hero>().EnterChunk(this.gameObject);
        }
    }

}
