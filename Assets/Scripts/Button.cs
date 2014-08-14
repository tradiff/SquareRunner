using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {
        if (InputManager.Instance.StartTouch())
        {
            var screenPos = InputManager.Instance.GetTouch();
            Vector3 wp = GameManager.Instance.HudCamera.ScreenToWorldPoint(screenPos);
            Vector2 touchPos = new Vector2(wp.x, wp.y);

            Collider2D touchCollider2d = Physics2D.OverlapPoint(touchPos);
            if (touchCollider2d == this.collider2D)
            {
                GameManager.Instance.PauseGame(true);
            }
        }


    }
}
