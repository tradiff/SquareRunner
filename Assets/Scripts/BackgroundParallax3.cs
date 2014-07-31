using System;
using UnityEngine;
using System.Collections;

public class BackgroundParallax3 : MonoBehaviour
{
    float camWidth = 0;
    float chunkWidth = 0;

    void Start()
    {
        var camHeight = 2 * Camera.main.orthographicSize;
        camWidth = camHeight * Camera.main.aspect;
        chunkWidth = GameManager.Instance.WorldGenerator.chunkWidth;
    }

    void Update()
    {
        //var parallaxOffset = (Camera.main.transform.position.x - transform.parent.transform.position.x) + (camWidth / 2);
        //var lerpedParallaxOffset = Mathf.Lerp(0, 1, parallaxOffset / (chunkWidth + camWidth));
        //Debug.Log(String.Format("camWidth:{0} parallaxOffset:{1} lerpedParallaxOffset:{2}", camWidth, parallaxOffset, lerpedParallaxOffset));
        //var bgPosition = Mathf.Lerp(0, 10, lerpedParallaxOffset);
        //transform.localPosition = new Vector3(bgPosition, transform.localPosition.y);
    }
}
