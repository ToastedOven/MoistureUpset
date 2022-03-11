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
        public Material material;
        void Start()
        {
            material = materials[UnityEngine.Random.Range(0, materials.Length)];
        }

    }
}
