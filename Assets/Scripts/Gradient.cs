using UnityEngine;
using System.Collections;

public class Gradient : MonoBehaviour
{
    public Color topColor = Color.blue;
    public Color bottomColor = Color.white;

    void Awake()
    {
        var mesh = GetComponent<MeshFilter>().mesh;

        var colors = new Color[mesh.vertices.Length];
        colors[0] = bottomColor;
        colors[1] = topColor;
        colors[2] = bottomColor;
        colors[3] = topColor;
        mesh.colors = colors;

        Material mat = new Material("Shader \"Vertex Color Only\"{Subshader{BindChannels{Bind \"vertex\", vertex Bind \"color\", color}Pass{}}}");
        renderer.material = mat;
    }
}
