using UnityEngine;
using System.Collections;

public class BackgroundParallax : MonoBehaviour
{
    public Transform[] Backgrounds;
    public float ParallaxScale;
    public float ParallaxReductionFactor;
    public float Smoothing;

    private Vector3 _lastPosition;

    void Start()
    {
        _lastPosition = transform.position;
    }

    void Update()
    {
        var parallax = (_lastPosition.x - transform.position.x) * -ParallaxScale;

        for (int i = 0; i < Backgrounds.Length; i++)
        {
            var backgroundTargetPosition = Backgrounds[i].position.x + parallax * (i * ParallaxReductionFactor + 1);
            Backgrounds[i].position = Vector3.Lerp(
                Backgrounds[i].position, // from
                new Vector3(backgroundTargetPosition, Backgrounds[i].position.y, Backgrounds[i].position.z), // to
                Smoothing * Time.deltaTime);
        }
        _lastPosition = transform.position;
    }
}
