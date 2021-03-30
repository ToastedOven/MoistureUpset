using System;
using System.Collections.Generic;
using System.Text;
using Unity;
using UnityEngine;
using UnityEngine.Networking;

namespace MoistureUpset
{
    class NewSplatSystemRemover : MonoBehaviour
    {
        void Start()
        {
            List<string> strings = new List<string>();
            for (int i = 0; i < GetComponentInChildren<SkinnedMeshRenderer>().material.shaderKeywords.Length; i++)
            {
                if (GetComponentInChildren<SkinnedMeshRenderer>().material.shaderKeywords[i] != "USE_VERTEX_COLORS")
                {
                    strings.Add(GetComponentInChildren<SkinnedMeshRenderer>().material.shaderKeywords[i]);
                }
            }
            GetComponentInChildren<SkinnedMeshRenderer>().material.shaderKeywords = strings.ToArray();
        }
    }
}
