using UnityEngine;
using System.Collections;

public class PU_BonusWarp : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.GetComponent<Player>();
        if (player == null)
            return;

        Debug.Log("PU_BonusWarp collide");

        GameManager.Instance.ChangeArea(GameManager.Areas.Bonus);

        Destroy(gameObject);
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
