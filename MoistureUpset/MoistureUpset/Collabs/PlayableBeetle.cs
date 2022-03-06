using BepInEx;
using R2API.Utils;
using RoR2;
using R2API;
using R2API.MiscHelpers;
using System.Reflection;
using static R2API.SoundAPI;
using System;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Text;
using RiskOfOptions;
using Chip;

namespace MoistureUpset.Collabs
{
    class PlayableBeetle
    {
        public static void Run()
        {
            Chip.Beetlegod beetlegod = new Beetlegod();
            foreach (var item in SurvivorCatalog.allSurvivorDefs)
            {
                if (item.bodyPrefab.name == "Chip")
                {
                    item.bodyPrefab.GetComponentInChildren<SkinnedMeshRenderer>().sharedMesh = Resources.Load<Mesh>("@MoistureUpset_frog:assets/frogchair.mesh");
                    item.displayPrefab.GetComponentInChildren<SkinnedMeshRenderer>().sharedMesh = Resources.Load<Mesh>("@MoistureUpset_frog:assets/frogchair.mesh");

                    List<Transform> t = new List<Transform>();
                    foreach (var boner in item.bodyPrefab.GetComponentsInChildren<Transform>())
                    {
                        if (!boner.name.Contains("Hurtbox") && !boner.name.Contains("BeetleBody") && !boner.name.Contains("Mesh") && !boner.name.Contains("mdl"))
                        {
                            t.Add(boner);
                        }
                    }
                    Transform temp = t[14];
                    t[14] = t[11];
                    t[11] = temp;
                    temp = t[15];
                    t[15] = t[12];
                    t[12] = temp;
                    temp = t[16];
                    t[16] = t[13];
                    t[13] = temp;
                    foreach (var boner in item.bodyPrefab.GetComponentsInChildren<SkinnedMeshRenderer>())
                    {
                        boner.bones = t.ToArray();
                    }


                    t = new List<Transform>();
                    foreach (var boner in item.displayPrefab.GetComponentsInChildren<Transform>())
                    {
                        if (!boner.name.Contains("Hurtbox") && !boner.name.Contains("BeetleBody") && !boner.name.Contains("Mesh") && !boner.name.Contains("mdl"))
                        {
                            t.Add(boner);
                        }
                    }
                    temp = t[14];
                    t[14] = t[11];
                    t[11] = temp;
                    temp = t[15];
                    t[15] = t[12];
                    t[12] = temp;
                    temp = t[16];
                    t[16] = t[13];
                    t[13] = temp;
                    foreach (var boner in item.displayPrefab.GetComponentsInChildren<SkinnedMeshRenderer>())
                    {
                        boner.bones = t.ToArray();
                    }
                }
            }

        }
    }
}
