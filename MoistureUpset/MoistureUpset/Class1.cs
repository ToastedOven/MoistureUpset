using BepInEx;
using R2API.Utils;
using RoR2;
using R2API;
using R2API.MiscHelpers;
using System.Reflection;
using static R2API.SoundAPI;
using UnityEngine;
using UnityEditor;
using System;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using RoR2.UI;
using RiskOfOptions;
using System.Text;
using System.IO;

namespace MoistureUpset
{
    [BepInDependency("com.bepis.r2api")]
    [BepInDependency("com.rune580.riskofoptions")]
    //Change these
    [BepInPlugin("com.WetBoys.MoistureUpset", "Moisture Upset", "1.0.0")]
    [R2APISubmoduleDependency("SoundAPI", "PrefabAPI", "CommandHelper", "LoadoutAPI", "SurvivorAPI", "ResourcesAPI")]
    public class BigTest : BaseUnityPlugin
    {
        public void Awake()
        {
            Settings.RunAll();

            Assets.PopulateAssets();

            SurvivorLoaderAPI.LoadSurvivors();

            SoundAssets.RegisterSoundEvents();

            NetworkAssistant.InitSNA();

            On.RoR2.UI.CharacterSelectController.SelectSurvivor += CharacterSelectController_SelectSurvivor;

            On.RoR2.TeleporterInteraction.Awake += TeleporterInteraction_Awake;

            //ligmaballs();

            ItemDisplayPositionFixer.Init();

            R2API.Utils.CommandHelper.AddToConsoleWhenReady();
        }

        [ConCommand(commandName = "debugtime", flags = ConVarFlags.None, helpText = "Does the magic")]
        private static void DebugCommand(ConCommandArgs args)
        {
            RoR2.Console.instance.SubmitCmd(NetworkUser.readOnlyLocalPlayersList[0], "god");
            RoR2.Console.instance.SubmitCmd(NetworkUser.readOnlyLocalPlayersList[0], "noclip");
            RoR2.Console.instance.SubmitCmd(NetworkUser.readOnlyLocalPlayersList[0], "no_enemies");
            RoR2.Console.instance.SubmitCmd(NetworkUser.readOnlyLocalPlayersList[0], "kill_all");
            RoR2.Console.instance.SubmitCmd(NetworkUser.readOnlyLocalPlayersList[0], "give_money 1000000");
            RoR2.Console.instance.SubmitCmd(NetworkUser.readOnlyLocalPlayersList[0], "give_item SoldiersSyringe 100");
            RoR2.Console.instance.SubmitCmd(NetworkUser.readOnlyLocalPlayersList[0], "give_item AlienHead 100");
        }

        [ConCommand(commandName = "slowmotime", flags = ConVarFlags.None, helpText = "Does the magic")]
        private static void SlowmoCommand(ConCommandArgs args)
        {
            RoR2.Console.instance.SubmitCmd(NetworkUser.readOnlyLocalPlayersList[0], "god");
            RoR2.Console.instance.SubmitCmd(NetworkUser.readOnlyLocalPlayersList[0], "noclip");
            RoR2.Console.instance.SubmitCmd(NetworkUser.readOnlyLocalPlayersList[0], "give_money 1000000");
            RoR2.Console.instance.SubmitCmd(NetworkUser.readOnlyLocalPlayersList[0], "give_item SoldiersSyringe 100");
            RoR2.Console.instance.SubmitCmd(NetworkUser.readOnlyLocalPlayersList[0], "time_scale 0.1");
        }

        [ConCommand(commandName = "musicdebug", flags = ConVarFlags.None, helpText = "Spits currently playing music to console")]
        private static void MusicTest(ConCommandArgs args)
        {
            var c = GameObject.FindObjectOfType<MusicController>();
            Debug.Log($"-------------{c.GetPropertyValue<MusicTrackDef>("currentTrack").cachedName}");
        }

        [ConCommand(commandName = "getallgameobjects", flags = ConVarFlags.None, helpText = "yes")]
        private static void GameObjects(ConCommandArgs args)
        {
            DebugClass.GetAllGameObjects();
        }

        [ConCommand(commandName = "braindamage", flags = ConVarFlags.None, helpText = "Grabs all the meshes")]
        private static void RemoveThisLater(ConCommandArgs args)
        {
            var meshes = GameObject.FindObjectsOfType<MeshFilter>();


            try
            {
                using (StreamWriter sw = new StreamWriter($"export/bigboi.obj"))
                {
                    foreach (var mf in meshes)
                    {
                        sw.WriteLine(MeshToString(mf));
                    }
                    
                }
            }
            catch (Exception)
            {

            }

            //foreach (var mf in meshes)
            //{
                

            //}            
        }

        private static string MeshToString(MeshFilter mf)
        {
            Mesh m = mf.mesh;
            Material[] mats = new Material[0];

            if (mf.gameObject.GetComponent<MeshRenderer>() != null)
            {
                mats = mf.gameObject.GetComponent<MeshRenderer>().sharedMaterials;
            }
            else
            {
                mats = mf.gameObject.GetComponent<SkinnedMeshRenderer>().sharedMaterials;
            }

            StringBuilder sb = new StringBuilder();

            sb.Append("o ").Append(mf.name).Append("\n");
            foreach (Vector3 v in m.vertices)
            {
                sb.Append(string.Format("v {0} {1} {2}\n", v.x + mf.transform.position.x, v.y + mf.transform.position.y, v.z + mf.transform.position.z));
            }
            sb.Append("\n");
            foreach (Vector3 v in m.normals)
            {
                sb.Append(string.Format("vn {0} {1} {2}\n", v.x, v.y, v.z));
            }
            sb.Append("\n");
            foreach (Vector3 v in m.uv)
            {
                sb.Append(string.Format("vt {0} {1}\n", v.x, v.y));
            }
            for (int material = 0; material < m.subMeshCount; material++)
            {
                sb.Append("\n");
                sb.Append("usemtl ").Append(mats[material].name).Append("\n");
                sb.Append("usemap ").Append(mats[material].name).Append("\n");

                int[] triangles = m.GetTriangles(material);
                for (int i = 0; i < triangles.Length; i += 3)
                {
                    sb.Append(string.Format("f {0}/{0}/{0} {1}/{1}/{1} {2}/{2}/{2}\n",
                        triangles[i] + 1, triangles[i + 1] + 1, triangles[i + 2] + 1));
                }
            }
            return sb.ToString();
        }

        public static void ligmaballs()
        {
            var fortniteDance = Resources.Load<AnimationClip>("@MoistureUpset_fortnite:assets/dancemoves.anim");
            var fab = Resources.Load<GameObject>("prefabs/characterbodies/CommandoBody");

            //foreach (var item in fab.GetComponentsInChildren<Component>())
            //{
            //    Debug.Log($"--------------------------------------------------{item}");
            //}

            var anim = fab.GetComponentInChildren<Animator>();

            Debug.Log($"++++++++++++++++++++++++++++++++++++++++{anim}");

            //AnimatorController anim = new AnimatorController
            AnimatorOverrideController aoc = new AnimatorOverrideController(anim.runtimeAnimatorController);

            var anims = new List<KeyValuePair<AnimationClip, AnimationClip>>();
            foreach (var a in aoc.animationClips)
            {
                anims.Add(new KeyValuePair<AnimationClip, AnimationClip>(a, a));
            }
            aoc.ApplyOverrides(anims);
            anim.runtimeAnimatorController = aoc;
        }

        public void Start()
        {
            RoR2.Console.instance.SubmitCmd((NetworkUser)null, "set_scene title");
        }

        private void TeleporterInteraction_Awake(On.RoR2.TeleporterInteraction.orig_Awake orig, TeleporterInteraction self)
        {
            //self.shouldAttemptToSpawnShopPortal = true;
            //self.Network_shouldAttemptToSpawnShopPortal = true;
            //self.baseShopSpawnChance = 1;

            orig(self);

            //self.shouldAttemptToSpawnShopPortal = true;
            //self.Network_shouldAttemptToSpawnShopPortal = true;
        }

        private void CharacterSelectController_SelectSurvivor(On.RoR2.UI.CharacterSelectController.orig_SelectSurvivor orig, RoR2.UI.CharacterSelectController self, SurvivorIndex survivor)
        {
            self.selectedSurvivorIndex = survivor;

            //if (survivor == SurvivorIndex.Commando)
            //{
            //    AkSoundEngine.PostEvent("YourMother", self.characterDisplayPads[0].displayInstance.gameObject);
            //}

            orig(self, survivor);

            HGTextMeshProUGUI[] objects = GameObject.FindObjectsOfType<HGTextMeshProUGUI>();

            foreach (var item in objects)
            {
                if (item.text == "Locked In")
                {
                    Debug.Log(item.transform.parent.name);
                }
            }
        }
    }
}
