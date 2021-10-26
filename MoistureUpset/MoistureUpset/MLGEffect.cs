using UnityEngine;
using System.Collections;
[ExecuteInEditMode]
public class MLGEffect : MonoBehaviour
{
    public float intensity;
    private Material material;
    // Creates a private material used to the effect
    void Awake()
    {
        material = new Material(Resources.Load<Shader>("@MoistureUpset_2014:assets/2014/TestShader.shader"));
    }

    // Postprocess the image
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination);
        return;
        if (intensity == 0)
        {
        }
        material.SetFloat("_bwBlend", intensity);
        Graphics.Blit(source, destination, material);
    }
}