using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class ParticleSortingLayer : MonoBehaviour
{
    public string SortingLayerName;
    public int SortingOrder;

    void Start()
    {
        // Set the sorting layer of the particle system.
        particleSystem.renderer.sortingLayerName = SortingLayerName;
        particleSystem.renderer.sortingOrder = SortingOrder;
    }
}

