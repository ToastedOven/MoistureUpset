using RoR2;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace MoistureUpset
{
    class AbungusColors : MonoBehaviour
    {
        public Material[] materials = { Resources.Load<Material>("@MoistureUpset_scavenger:assets/bosses/materials/Amongus.mat"), Resources.Load<Material>("@MoistureUpset_scavenger:assets/bosses/materials/AmongusCyan.mat"), Resources.Load<Material>("@MoistureUpset_scavenger:assets/bosses/materials/AmongusGreen.mat"), Resources.Load<Material>("@MoistureUpset_scavenger:assets/bosses/materials/AmongusOrange.mat"), Resources.Load<Material>("@MoistureUpset_scavenger:assets/bosses/materials/AmongusPink.mat"), Resources.Load<Material>("@MoistureUpset_scavenger:assets/bosses/materials/AmongusPurple.mat"), Resources.Load<Material>("@MoistureUpset_scavenger:assets/bosses/materials/AmongusRed.mat"), Resources.Load<Material>("@MoistureUpset_scavenger:assets/bosses/materials/AmongusYellow.mat"), Resources.Load<Material>("@MoistureUpset_scavenger:assets/bosses/materials/AmongusBrown.mat"), Resources.Load<Material>("@MoistureUpset_scavenger:assets/bosses/materials/AmongusBlue.mat"), Resources.Load<Material>("@MoistureUpset_scavenger:assets/bosses/materials/AmongusBlack.mat") };
        public Material material;
        void Start()
        {
            material = materials[UnityEngine.Random.Range(0, materials.Length)];
        }

    }
}
