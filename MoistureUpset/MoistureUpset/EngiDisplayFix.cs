using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using RoR2;

namespace MoistureUpset
{

    // Still not the best idea, but much better than what I was doing previously. Technically there is like a few frames where you can see the old models, but oh well.
    internal class EngiDisplayFix : MonoBehaviour
    {
        private static Mesh _turretMesh;
        private static Mesh _walkerTurretMesh;
        private static Mesh _spiderMineMesh;
        private static Mesh _mineMesh;
        private static Material _mineTex;

        private void Update()
        {
            bool isTf2Skin = false;

            foreach (var smr in GetComponentsInChildren<SkinnedMeshRenderer>())
            {
                if (smr.sharedMesh.name == SkinHelper.SkinNameToSkinMeshName["THE_TF2_ENGINEER_SKIN"])
                    isTf2Skin = true;
            }


            if (isTf2Skin)
            {
                foreach (var smr in GetComponentsInChildren<SkinnedMeshRenderer>())
                {
                    switch (smr.sharedMesh.name)
                    {
                        case "EngiTurretMesh":
                            if (_turretMesh == null)
                            {
                                _turretMesh = smr.sharedMesh;
                            }
                            smr.sharedMesh = Assets.Load<Mesh>("assets/normal_sentry.mesh");
                            smr.material = Assets.LoadMaterial("assets/unified_turret_tex.png");
                            break;
                        case "EngiWalkerTurretMesh":
                            if (_walkerTurretMesh == null)
                            {
                                _walkerTurretMesh = smr.sharedMesh;
                            }
                            smr.sharedMesh = Assets.Load<Mesh>("assets/walker_turret.mesh");
                            smr.material = Assets.LoadMaterial("assets/unified_turret_tex.png");
                            break;
                        case "EngiSpiderMineMesh":
                            if (_spiderMineMesh == null)
                            {
                                _spiderMineMesh = smr.sharedMesh;
                            }
                            _mineTex = smr.material;

                            smr.sharedMesh = Assets.Load<Mesh>("assets/spidermine.mesh");
                            smr.material = Assets.LoadMaterial("assets/mines.png");
                            break;
                        case "EngiMineMesh":
                            if (_mineMesh == null)
                            {
                                _mineMesh = smr.sharedMesh;
                            }
                            _mineTex = smr.material;

                            smr.sharedMesh = Assets.Load<Mesh>("assets/harpoon.mesh");
                            smr.material = Assets.LoadMaterial("assets/mines.png");
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
                            if (_turretMesh)
                            {
                                smr.sharedMesh = _turretMesh;
                            }
                            break;
                        case "walker_turret":
                            if (_walkerTurretMesh)
                            {
                                smr.sharedMesh = _walkerTurretMesh;
                            }
                            break;
                        case "spidermine":
                            if (_spiderMineMesh && _mineTex)
                            {
                                smr.sharedMesh = _spiderMineMesh;
                                smr.material = _mineTex;
                            }
                            break;
                        case "harpoon":
                            if (_mineMesh && _mineTex)
                            {
                                smr.sharedMesh = _mineMesh;
                                smr.material = _mineTex;
                            }
                            break;
                    }
                }
            }
        }
    }
}
