using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WorldGenerator : MonoBehaviour
{
    private Object worldChunkPrefab;
    private Object groundPrefab;

    private GameObject newestChunk = null;
    private float chunkWidth = 0;

    // Use this for initialization
    void Start()
    {
        worldChunkPrefab = Resources.Load("WorldChunkPrefab");
        groundPrefab = Resources.Load("tiles/Ground_88_Prefab");
        GenerateWorldChunk(0);
        chunkWidth = getBounds(newestChunk).size.x;
        chunkWidth = 100;
        Debug.Log("chunkWidth = " + chunkWidth);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Debug.Log(newestChunk.transform.position.x);
        var chunkRight = newestChunk.transform.position.x + chunkWidth;

        if (chunkRight < 200)
        {
            GenerateWorldChunk(chunkRight);
            Debug.Log("new chunk");
        }
    }

    private void GenerateWorldChunk(float positionX)
    {
        var chunk = (GameObject)Instantiate(worldChunkPrefab, new Vector3(positionX, 0, 0), new Quaternion(0, 0, 0, 0));

        int chunkLength = 100;
        for (int i = 0; i < chunkLength; i++)
        {
            var tile = (GameObject)Instantiate(groundPrefab, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
            tile.transform.parent = chunk.transform;
            tile.transform.localPosition = new Vector3(1 * i, -1, 0);
        }

        newestChunk = chunk;
    }

    Bounds getBounds(GameObject objeto)
    {
        Bounds bounds;
        Renderer childRender;
        bounds = getRenderBounds(objeto);
        if (bounds.extents.x == 0)
        {
            bounds = new Bounds(objeto.transform.position, Vector3.zero);
            foreach (Transform child in objeto.transform)
            {
                childRender = child.GetComponent<Renderer>();
                if (childRender)
                {
                    bounds.Encapsulate(childRender.bounds);
                }
                else
                {
                    bounds.Encapsulate(getBounds(child.gameObject));
                }
            }
        }
        return bounds;
    }

    Bounds getRenderBounds(GameObject objeto)
    {
        Bounds bounds = new Bounds(Vector3.zero, Vector3.zero);
        Renderer render = objeto.GetComponent<Renderer>();
        if (render != null)
        {
            return render.bounds;
        }
        return bounds;
    }


}
