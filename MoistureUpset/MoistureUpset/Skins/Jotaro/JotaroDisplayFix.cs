using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using RoR2;

namespace MoistureUpset.Skins.Jotaro
{
    class JotaroDisplayFix : MonoBehaviour
    {
        private static Mesh CaptainMesh, CaptainHatMesh, CaptainChestArmorMesh, CaptainCoatMesh, CaptainGunArmMesh, CaptainHeadMesh, CaptainUndercoatMesh;

        private bool isJotaroSkin = false;
        private bool wasJotaroSkin = false;

        private void Start()
        {
            var fab = Resources.Load<GameObject>("prefabs/characterdisplays/CaptainDisplay");

            foreach (var smr in fab.GetComponentsInChildren<SkinnedMeshRenderer>())
            {
                switch (smr.sharedMesh.name)
                {
                    case "Captain":
                        CaptainMesh = smr.sharedMesh;
                        break;
                    case "CaptainChestArmor":
                        CaptainChestArmorMesh = smr.sharedMesh;
                        break;
                    case "CaptainCoat":
                        CaptainCoatMesh = smr.sharedMesh;
                        break;
                    case "CaptainGunArm":
                        CaptainGunArmMesh = smr.sharedMesh;
                        break;
                    case "CaptainHead":
                        CaptainHeadMesh = smr.sharedMesh;
                        break;
                    case "CaptainUndercoat":
                        CaptainUndercoatMesh = smr.sharedMesh;
                        break;
                }
            }

            CaptainHatMesh = fab.GetComponentInChildren<MeshFilter>().sharedMesh;
        }

        private void Update()
        {
            isJotaroSkin = false;

            foreach (var smr in GetComponentsInChildren<SkinnedMeshRenderer>())
            {
                //DebugClass.Log($"Token: {JotaroCaptain.NameToken}, name: {SkinHelper.skinNametoskinMeshName[JotaroCaptain.NameToken]}, name2: {smr.material.mainTexture.name}");
                if (smr.material.mainTexture.name.ToLower() == SkinHelper.SkinNameToSkinMeshName[JotaroCaptain.NameToken].ToLower())
                {
                    isJotaroSkin = true;
                }
            }
            //DebugClass.Log($"Is: {isJotaroSkin}, Was: {wasJotaroSkin}");

            if (wasJotaroSkin && !isJotaroSkin)
            {
                foreach (var smr in GetComponentsInChildren<SkinnedMeshRenderer>())
                {
                    switch (smr.gameObject.name)
                    {
                        case "Captain":
                            smr.sharedMesh = CaptainMesh;
                            break;
                        case "CaptainChestArmor":
                            smr.sharedMesh = CaptainChestArmorMesh;
                            break;
                        case "CaptainCoat":
                            smr.sharedMesh = CaptainCoatMesh;
                            break;
                        case "CaptainGunArm":
                            smr.sharedMesh = CaptainGunArmMesh;
                            break;
                        case "CaptainHead":
                            smr.sharedMesh = CaptainHeadMesh;
                            break;
                        case "CaptainUndercoat":
                            smr.sharedMesh = CaptainUndercoatMesh;
                            break;
                    }
                }

                GetComponentInChildren<MeshFilter>().sharedMesh = CaptainHatMesh;
            }

            wasJotaroSkin = isJotaroSkin;
        }
    }
}
