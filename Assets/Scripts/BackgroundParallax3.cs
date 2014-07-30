using System;
using UnityEngine;
using System.Collections;

public class BackgroundParallax3 : MonoBehaviour
{
    

    void Start()
    {
    }

    void Update()
    {
        var camHeight = 2 * Camera.main.orthographicSize;
        var camWidth = camHeight * Camera.main.aspect;
        
        var parallaxOffset = (Camera.main.transform.position.x - transform.parent.transform.position.x) + (camWidth / 2);
        var lerpedParallaxOffset = Mathf.Lerp(0, 1, parallaxOffset/camWidth);
        var bgPosition = Mathf.Lerp(0, 2, lerpedParallaxOffset);
        transform.localPosition = new Vector3(bgPosition, transform.localPosition.y);
    }
}
