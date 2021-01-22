using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using RoR2;

namespace MoistureUpset
{

    // Still not the best idea, but much better than what I was doing previously. Technically there is like a few frames where you can see the old models, but oh well.
    class EngiDisplayFix : MonoBehaviour
    {
        private static Mesh engiturretmesh;
        private static Mesh engiwalkerturretmesh;
        private static Mesh engispiderminemesh;
        private static Mesh engiminemesh;

        private static Material engiminetex;

        private void Start()
        {

        }


        private void Update()
        {
            bool isTF2Skin = false;

            foreach (var smr in GetComponentsInChildren<SkinnedMeshRenderer>())
            {
                if (smr.sharedMesh.name == SkinHelper.skinNametoskinMeshName["THE_TF2_ENGINEER_SKIN"])
                {
                    isTF2Skin = true;
                }
            }


            if (isTF2Skin)
            {
                foreach (var smr in GetComponentsInChildren<SkinnedMeshRenderer>())
                {
                    switch (smr.sharedMesh.name)
                    {
                        case "EngiTurretMesh":
                            if (engiturretmesh == null)
                            {
                                engiturretmesh = smr.sharedMesh;
                            }
                            smr.sharedMesh = Resources.Load<Mesh>("@MoistureUpset_unifiedturret:assets/normal_sentry.mesh");
                            smr.material = Assets.CreateMaterial("@MoistureUpset_unifiedturret:assets/unified_turret_tex.png");
                            break;
                        case "EngiWalkerTurretMesh":
                            if (engiwalkerturretmesh == null)
                            {
                                engiwalkerturretmesh = smr.sharedMesh;
                            }
                            smr.sharedMesh = Resources.Load<Mesh>("@MoistureUpset_unifiedturret:assets/walker_turret.mesh");
                            smr.material = Assets.CreateMaterial("@MoistureUpset_unifiedturret:assets/unified_turret_tex.png");
                            break;
                        case "EngiSpiderMineMesh":
                            if (engispiderminemesh == null)
                            {
                                engispiderminemesh = smr.sharedMesh;
                            }
                            engiminetex = smr.material;

                            smr.sharedMesh = Resources.Load<Mesh>("@MoistureUpset_Models_mines:assets/spidermine.mesh");
                            smr.material = Assets.CreateMaterial("@MoistureUpset_Models_mines:assets/mines.png");
                            break;
                        case "EngiMineMesh":
                            if (engiminemesh == null)
                            {
                                engiminemesh = smr.sharedMesh;
                            }
                            engiminetex = smr.material;

                            smr.sharedMesh = Resources.Load<Mesh>("@MoistureUpset_Models_mines:assets/harpoon.mesh");
                            smr.material = Assets.CreateMaterial("@MoistureUpset_Models_mines:assets/mines.png");
                            break;
                    }
                }
            }
            else
            {
                foreach (var smr in GetComponentsInChildren<SkinnedMeshRenderer>())
                {
                    switch (smr.sharedMesh.name)
                    {
                        case "Normal_Sentry":
                            if (engiturretmesh != null)
                            {
                                smr.sharedMesh = engiturretmesh;
                            }
                            break;
                        case "walker_turret":
                            if (engiwalkerturretmesh != null)
                            {
                                smr.sharedMesh = engiwalkerturretmesh;
                            }
                            break;
                        case "spidermine":
                            if (engispiderminemesh != null && engiminetex != null)
                            {
                                smr.sharedMesh = engispiderminemesh;
                                smr.material = engiminetex;
                            }
                            break;
                        case "harpoon":
                            if (engiminemesh != null && engiminetex != null)
                            {
                                smr.sharedMesh = engiminemesh;
                                smr.material = engiminetex;
                            }
                            break;
                    }
                }
            }
        }
    }
}
