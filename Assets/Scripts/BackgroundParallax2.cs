using UnityEngine;
using System.Collections;

public class BackgroundParallax2 : MonoBehaviour
{
    private float ParallaxScale = 2;
    private float Smoothing = 1;

    private Vector3 _lastCameraPosition;

    void Start()
    {
        _lastCameraPosition = Camera.main.transform.position;
    }

    void Update()
    {
        var parallax = (_lastCameraPosition.x - Camera.main.transform.position.x) * -ParallaxScale;

        var backgroundTargetPosition = transform.position.x + parallax;
        backgroundTargetPosition = Mathf.Lerp(transform.position.x, backgroundTargetPosition, Smoothing * Time.deltaTime);
        transform.position = new Vector3(backgroundTargetPosition, transform.position.y, transform.position.z);


        _lastCameraPosition = Camera.main.transform.position;
    }
}
