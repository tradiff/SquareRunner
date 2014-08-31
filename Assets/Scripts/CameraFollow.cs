using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    float dampening = 5;
    float _shakeTime = 0;
    float _shakeIntensity = 0.3f;
    float _shakeDecay = 0.002f;

    // Update is called once per frame
    void Update()
    {
        
        var newX = Mathf.Lerp(transform.position.x, player.position.x + 10, dampening * Time.deltaTime);
        transform.position = new Vector3(newX, GameManager.Instance.Area == GameManager.Areas.Bonus ? 107f : 7f, -10);
        Shake();
    }

    void Shake()
    {
        if (_shakeTime > 0)
        {
            transform.position += Random.insideUnitSphere * _shakeIntensity;
            _shakeTime -= Time.deltaTime;
        }
    }

    public void StartShake()
    {
        _shakeTime = 1f;
    }
}
