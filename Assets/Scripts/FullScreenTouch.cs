using UnityEngine;
using System.Collections;

public class FullScreenTouch : MonoBehaviour
{
    public static FullScreenTouch Instance
    {
        get
        {
            return _instance;
        }
    }
    private static FullScreenTouch _instance;

    private bool _wasTouching = false;
    public bool Touching;
    public bool StartTouch;

    void Awake()
    {
        _instance = this;
        Touching = false;
    }

    void Update()
    {
        StartTouch = (!_wasTouching && Touching);
        _wasTouching = Touching;
    }

    public void OnPointerDown()
    {
        Touching = true;
    }

    public void OnPointerUp()
    {
        Touching = false;
    }
}
