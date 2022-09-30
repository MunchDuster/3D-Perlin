using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TestRunner : MonoBehaviour
{
    public ComputeShader shader;
    public Material material;
    [Header("Setiings")]
    public float scale = 10;
    public int resolution = 256;
    
    public void Test()
    {
        RenderTexture texture = new RenderTexture(resolution, resolution, 24);
        texture.enableRandomWrite = true;
        texture.filterMode = FilterMode.Point;
        texture.Create();
        
        shader.SetTexture(0, "Result", texture);

        shader.SetInt("xPixels", resolution);
        shader.SetFloat("scale", scale);

        shader.Dispatch(0, texture.width/8, texture.height/8, 1);
        
        material.mainTexture = texture;
    }
}
