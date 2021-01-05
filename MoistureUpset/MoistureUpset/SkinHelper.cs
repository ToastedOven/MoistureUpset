using RoR2;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;


namespace MoistureUpset
{
    public static class SkinHelper
    {
        public static Dictionary<string, string> skinNametoskinMeshName = new Dictionary<string, string>();

        public static void RegisterSkin(string skinName, string skinMeshName)
        {
            skinNametoskinMeshName.Add(skinName, skinMeshName);
        }

        public static bool isSkin(this CharacterBody cb, string skinName)
        {
            string meshName = skinNametoskinMeshName[skinName];

            foreach (var smr in cb.modelLocator.modelTransform.GetComponentsInChildren<SkinnedMeshRenderer>()) // I tried using the skincatalog previously, but for some reason the skincatalog keeps throwing a null reference exception.
            {
                //Debug.Log($"--------------------- {smr.sharedMesh.name}, {meshName}");

                if (smr.sharedMesh.name == meshName)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
