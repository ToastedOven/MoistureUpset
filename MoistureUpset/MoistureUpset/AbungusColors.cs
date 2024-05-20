using RoR2;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace MoistureUpset
{
    class AbungusColors : MonoBehaviour
    {
        public Material[] materials = { Assets.Load<Material>("@MoistureUpset_scavenger:assets/bosses/materials/Amongus.mat"), Assets.Load<Material>("@MoistureUpset_scavenger:assets/bosses/materials/AmongusCyan.mat"), Assets.Load<Material>("@MoistureUpset_scavenger:assets/bosses/materials/AmongusGreen.mat"), Assets.Load<Material>("@MoistureUpset_scavenger:assets/bosses/materials/AmongusOrange.mat"), Assets.Load<Material>("@MoistureUpset_scavenger:assets/bosses/materials/AmongusPink.mat"), Assets.Load<Material>("@MoistureUpset_scavenger:assets/bosses/materials/AmongusPurple.mat"), Assets.Load<Material>("@MoistureUpset_scavenger:assets/bosses/materials/AmongusRed.mat"), Assets.Load<Material>("@MoistureUpset_scavenger:assets/bosses/materials/AmongusYellow.mat"), Assets.Load<Material>("@MoistureUpset_scavenger:assets/bosses/materials/AmongusBrown.mat"), Assets.Load<Material>("@MoistureUpset_scavenger:assets/bosses/materials/AmongusBlue.mat"), Assets.Load<Material>("@MoistureUpset_scavenger:assets/bosses/materials/AmongusBlack.mat") };
        Material mat = Assets.Load<Material>("@MoistureUpset_scavenger:assets/bosses/materials/gun.mat");
        public Material material;
        void Start()
        {
            material = materials[UnityEngine.Random.Range(0, materials.Length)];
        }
        bool check = true;
        void Update()
        {
            if (check)
            {
                if (GetComponentInChildren<ModelLocator>().modelTransform.gameObject.GetComponentInChildren<CharacterModel>().baseRendererInfos[0].defaultMaterial != material)
                {
                    GetComponentInChildren<ModelLocator>().modelTransform.gameObject.GetComponentInChildren<CharacterModel>().baseRendererInfos[0].defaultMaterial = material;
                    GetComponentInChildren<ModelLocator>().modelTransform.gameObject.GetComponentInChildren<CharacterModel>().baseRendererInfos[3].defaultMaterial = mat;
                    check = false;
                }
            }
        }
    }
}