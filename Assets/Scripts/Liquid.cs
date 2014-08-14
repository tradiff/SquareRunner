using UnityEngine;
using System.Collections;

public class Liquid : MonoBehaviour
{
    void Awake()
    {

        var animator = this.GetComponent<Animator>();
        animator.Play("liquid", -1, Random.Range(0f, 1f));
        //animator.playbackTime = Random.Range(animator.recorderStartTime, animator.recorderStopTime);
    }

    void Update()
    {

    }
}
