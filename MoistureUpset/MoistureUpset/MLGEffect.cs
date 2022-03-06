using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class MLGCamera : MonoBehaviour
{
    public Material material;
    public bool FastApproximately(float a, float b, float threshold)
    {
        return ((a - b) < 0 ? ((a - b) * -1) : (a - b)) <= threshold;
    }
    // Start is called before the first frame update
    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {

        if (FastApproximately(material.GetFloat("_cr"), material.GetFloat("_dr"), .025f))
        {
            material.SetFloat("_dr", Mathf.Abs(.5f - material.GetFloat("_dr")));
        }
        material.SetFloat("_cr", Mathf.Lerp(material.GetFloat("_cr"), material.GetFloat("_dr"), Time.deltaTime * 1.75f));

        if (FastApproximately(material.GetFloat("_cg"), material.GetFloat("_dg"), .025f))
        {
            material.SetFloat("_dg", Mathf.Abs(.5f - material.GetFloat("_dg")));
        }
        material.SetFloat("_cg", Mathf.Lerp(material.GetFloat("_cg"), material.GetFloat("_dg"), Time.deltaTime * 1.25f));

        if (FastApproximately(material.GetFloat("_cb"), material.GetFloat("_db"), .025f))
        {
            material.SetFloat("_db", Mathf.Abs(.5f - material.GetFloat("_db")));
        }
        material.SetFloat("_cb", Mathf.Lerp(material.GetFloat("_cb"), material.GetFloat("_db"), Time.deltaTime * 2));


        Graphics.Blit(source, destination, material);
    }
}
