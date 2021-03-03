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
    public static class SkinHelper // Originally we used skinIndex == 2, but I realized that that may be incompatible with other skins for the engi, so we do this jank instead.
    {
        public static Dictionary<string, string> skinNametoskinMeshName = new Dictionary<string, string>();

        public static int engiTurretSkinIndex = -1;

        public static void RegisterSkin(string skinName, string skinMeshName)
        {
            skinNametoskinMeshName.Add(skinName, skinMeshName);
        }

        public static bool isSkin(this CharacterBody cb, string skinName)
        {
            if (!cb)
                return false;

            if (!cb.modelLocator)
                return false;

            if (!skinNametoskinMeshName.ContainsKey(skinName))
                return false;

            string meshName = skinNametoskinMeshName[skinName];
            

            foreach (var smr in cb.modelLocator.modelTransform.GetComponentsInChildren<SkinnedMeshRenderer>()) // I tried using the skincatalog previously, but for some reason the skincatalog keeps throwing a null reference exception.
            {                                                                                                  // Future Rune here, I think the skin catalog is only available during the survivorcatlog initlization. but I may just be stupid.
                // Some poeple decide not to use a sharedmesh with their skin, that causes issues, so we check to make sure this characterbody has a shared mesh.
                if (!smr.sharedMesh)
                    continue;

                if (smr.sharedMesh.name == meshName)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
