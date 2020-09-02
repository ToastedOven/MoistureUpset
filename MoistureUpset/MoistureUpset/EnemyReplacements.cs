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

namespace MoistureUpset
{
    public static class EnemyReplacements
    {
        private static void ReplaceModel(string prefab, string mesh, string png)
        {
            var fab = Resources.Load<GameObject>(prefab);
            var renderers = fab.GetComponentsInChildren<Renderer>();
            var meshes = fab.GetComponentsInChildren<SkinnedMeshRenderer>();
            meshes[0].sharedMesh = Resources.Load<Mesh>(mesh);
            for (int i = 0; i < meshes[0].sharedMaterials.Length; i++)
            {
                meshes[0].sharedMaterials[i].mainTexture = Resources.Load<Texture>(png);
                meshes[0].sharedMaterials[i].SetTexture("_EmTex", Resources.Load<Texture>(png));
            }
        }
        public static void RunAll()
        {
            Lemurian();
            LemurianBruiser();
            //DebugClass.DebugBones(@"prefabs/characterbodies/GolemBody");
            Golem();
        }
        public static void Lemurian()
        {
            On.EntityStates.LemurianMonster.Bite.OnEnter += (orig, self) =>
            {
                EntityStates.LemurianMonster.Bite.attackString = "MikeAttack";
                orig(self);
            };
            On.EntityStates.LemurianMonster.ChargeFireball.OnEnter += (orig, self) =>
            {
                EntityStates.LemurianMonster.ChargeFireball.attackString = "MikeAttack";
                orig(self);
            };
            ReplaceModel("prefabs/characterbodies/LemurianBody", "@MoistureUpset_mike:assets/mike.mesh", "@MoistureUpset_mike:assets/mike.png");
        }
        public static void LemurianBruiser()
        {
            //ReplaceModel("prefabs/characterbodies/LemurianBruiserBody", "@MoistureUpset_mike:assets/bruisermike.mesh", "@MoistureUpset_mike:assets/mikebruiser.png");
        }

        public static void Golem()
        {
            On.EntityStates.GolemMonster.ChargeLaser.OnEnter += (orig, self) =>
            {
                EntityStates.GolemMonster.ChargeLaser.attackSoundString = "GolemChargeLaser";
                orig(self);
            };
            On.EntityStates.GolemMonster.FireLaser.OnEnter += (orig, self) =>
            {
                EntityStates.GolemMonster.FireLaser.attackSoundString = "GolemFireLaser";
                orig(self);
            };
            On.EntityStates.GolemMonster.ClapState.OnEnter += (orig, self) =>
            {
                EntityStates.GolemMonster.ClapState.attackSoundString = "GolemMelee";
                orig(self);
            };
            ReplaceModel("prefabs/characterbodies/GolemBody", "@MoistureUpset_noob:assets/N00b.mesh", "@MoistureUpset_noob:assets/Noob1Tex.png");
        }
    }
}
