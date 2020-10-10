using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using RoR2;
using R2API;

namespace MoistureUpset
{
    class SkinReloader : MonoBehaviour
    {
        // For some reason clients will sometimes see the incorrect skin with custom skins, so I just add this to all survivors and engi turrets, in the hopes that it ensures the correct skin is displayed.

        // Also if you don't like how I'm applying the turret skins, go complain to the r2api people for not providing good documentation on how to implement minion skin replacements.
        private void Start()
        {
            var skinController = GetComponentInChildren<ModelSkinController>();

            int skinIndex;

            if (GetComponentInChildren<CharacterBody>().master.minionOwnership.ownerMaster == null)
            {
                skinIndex = (int)GetComponentInChildren<CharacterBody>().skinIndex;
            }
            else
            {
                skinIndex = (int)GetComponentInChildren<CharacterBody>().master.minionOwnership.ownerMaster.GetBody().skinIndex;
            }

            skinController.ApplySkin(skinIndex);

            switch (gameObject.name)
            {
                case "EngiTurretBody(Clone)":
                case "EngiWalkerTurretBody(Clone)":
                    foreach (var item in skinController.GetComponentInChildren<SkinnedMeshRenderer>().sharedMaterials)
                    {
                        //Debug.Log("replacing textures");
                        //Debug.Log($"Original Texture: {item.name}");
                        if (item.name == "matEngiTurret")
                        {
                            item.mainTexture = Resources.Load<Texture>("@MoistureUpset_engi_turret2:assets/unified_turret_tex.png");
                            item.SetTexture("_EmTex", Resources.Load<Texture>("@MoistureUpset_engi_turret2:assets/unified_turret_tex.png"));
                        }
                    }
                    break;
            }
        }
    }
}
