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
using DireseekerMod;

namespace MoistureUpset.Collabs
{
    public static class Direseeker
    {
        public static void Run()
        {
            Debug.Log("---Direseeker implementation coming soonTM!");
            //DireseekerMod.Modules.Prefabs.bodyPrefab.GetComponentInChildren<CharacterModel>().baseRendererInfos[0].defaultMaterial = Resources.Load<GameObject>("prefabs/characterbodies/LemurianBruiserBody").GetComponentInChildren<SkinnedMeshRenderer>().sharedMaterial;
            //DireseekerMod.Modules.Prefabs.bodyPrefab.GetComponentsInChildren<MeshFilter>()[0].sharedMesh = Resources.Load<Mesh>("@MoistureUpset_NA:assets/na1.mesh");
            //DireseekerMod.Modules.Prefabs.bodyPrefab.GetComponentsInChildren<MeshFilter>()[1].sharedMesh = Resources.Load<Mesh>("@MoistureUpset_NA:assets/na1.mesh");
            //EnemyReplacements.ReplaceModel(DireseekerMod.Modules.Prefabs.bodyPrefab, "@MoistureUpset_bowser:assets/bowser.mesh");
        }
    }
}
