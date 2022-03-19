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
using MoistureUpset.NetMessages;
using R2API.Networking.Interfaces;
using UnityEngine.AddressableAssets;
using MoistureUpset.Collabs;
using System.Collections;
using MoistureUpset.Fixers;

namespace MoistureUpset
{
    public static class EnemyReplacements
    {
        public static void ReplaceModel(string prefab, string mesh, string png, int position = 0, bool replaceothers = false)
        {
            var fab = Addressables.LoadAssetAsync<GameObject>(prefab).WaitForCompletion();
            var meshes = fab.GetComponentsInChildren<SkinnedMeshRenderer>();
            meshes[position].sharedMesh = Assets.Load<Mesh>(mesh);
            var texture = Assets.Load<Texture>(png);
            var blank = Assets.Load<Texture>("@MoistureUpset_na:assets/blank.png");
            for (int i = 0; i < meshes[position].sharedMaterials.Length; i++)
            {
                //Debug.Log($"-=============={meshes[position].sharedMaterials[i].shader.name}");
                //meshes[position].sharedMaterials[i].shader = Shader.Find("Hopoo Games/Deferred/Standard");
                if (prefab == "RoR2/Base/Titan/TitanGoldBody.prefab")
                {
                    //meshes[position].sharedMaterials[i].shader = LegacyShaderAPI.Find("Hopoo Games/Deferred/Standard");
                    meshes[position].sharedMaterials[i].shader = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Commando/CommandoBody.prefab").WaitForCompletion().GetComponentInChildren<SkinnedMeshRenderer>().material.shader;
                }
                else if (prefab == "RoR2/Base/Shopkeeper/ShopkeeperBody.prefab")
                {
                    //meshes[position].sharedMaterials[i] = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Commando/CommandoBody.prefab").WaitForCompletion().GetComponentInChildren<SkinnedMeshRenderer>().material;
                    meshes[position].sharedMaterials[i] = Assets.LoadMaterial(png);
                }
                meshes[position].sharedMaterials[i].color = Color.white;
                meshes[position].sharedMaterials[i].mainTexture = texture;
                meshes[position].sharedMaterials[i].SetTexture("_EmTex", blank);
                meshes[position].sharedMaterials[i].SetTexture("_NormalTex", null);
                if (png.Contains("frog"))
                {
                    meshes[position].sharedMaterials[i].SetTexture("_FresnelRamp", null);
                }
                if (png.Contains("shop"))
                {
                    meshes[position].sharedMaterials[i].SetTexture("_FlowHeightRamp", null);
                    meshes[position].sharedMaterials[i].SetTexture("_FlowHeightmap", null);
                }
                if (png.Contains("dankengine"))
                {
                    meshes[1].sharedMaterials[0].SetTexture("_MainTex", texture);
                    meshes[position].sharedMaterials[i].SetTexture("_FresnelRamp", null);
                    meshes[position].sharedMaterials[i].SetTexture("_FlowHeightRamp", null);
                    meshes[position].sharedMaterials[i].SetTexture("_FlowHeightmap", null);
                }
            }
            if (replaceothers)
            {
                for (int i = 0; i < meshes.Length; i++)
                {
                    if (i != position)
                    {
                        meshes[i].sharedMesh = Assets.Load<Mesh>(mesh);
                    }
                }
            }
        }
        public static void ReplaceModel(SkinnedMeshRenderer meshes, string mesh, string png)
        {
            meshes.sharedMesh = Assets.Load<Mesh>(mesh);
            var texture = Assets.Load<Texture>(png);
            var blank = Assets.Load<Texture>("@MoistureUpset_na:assets/blank.png");
            for (int i = 0; i < meshes.sharedMaterials.Length; i++)
            {
                meshes.sharedMaterials[i].color = Color.white;
                meshes.sharedMaterials[i].mainTexture = texture;
                meshes.sharedMaterials[i].SetTexture("_EmTex", blank);
                meshes.sharedMaterials[i].SetTexture("_NormalTex", null);
                if (png.Contains("frog"))
                {
                    meshes.sharedMaterials[i].SetTexture("_FresnelRamp", null);
                }
                if (png.Contains("shop"))
                {
                    meshes.sharedMaterials[i].SetTexture("_FlowHeightRamp", null);
                    meshes.sharedMaterials[i].SetTexture("_FlowHeightmap", null);
                }
            }
        }
        public static void ReplaceModel(GameObject fab, string mesh, string png, int position = 0, bool replaceothers = false)
        {
            var meshes = fab.GetComponentsInChildren<SkinnedMeshRenderer>();
            meshes[position].sharedMesh = Assets.Load<Mesh>(mesh);
            var texture = Assets.Load<Texture>(png);
            var blank = Assets.Load<Texture>("@MoistureUpset_na:assets/blank.png");
            for (int i = 0; i < meshes[position].sharedMaterials.Length; i++)
            {
                meshes[position].sharedMaterials[i].color = Color.white;
                meshes[position].sharedMaterials[i].mainTexture = texture;
                meshes[position].sharedMaterials[i].SetTexture("_EmTex", blank);
                meshes[position].sharedMaterials[i].SetTexture("_NormalTex", null);
                if (png.Contains("frog"))
                {
                    meshes[position].sharedMaterials[i].SetTexture("_FresnelRamp", null);
                }
                if (png.Contains("shop"))
                {
                    meshes[position].sharedMaterials[i].SetTexture("_FlowHeightRamp", null);
                    meshes[position].sharedMaterials[i].SetTexture("_FlowHeightmap", null);
                }
                //try
                //{
                //    foreach (var item in meshes[0].sharedMaterials[i].GetTexturePropertyNames())
                //    {
                //        Debug.Log($"---------{item}---------------{meshes[0].sharedMaterials[i].GetTexture(item)}");
                //    }
                //    Debug.Log($"------------------------{meshes[0].sharedMaterials[i]}");
                //}
                //catch (Exception e)
                //{
                //    Debug.Log(e);
                //}
            }
            if (replaceothers)
            {
                for (int i = 0; i < meshes.Length; i++)
                {
                    if (i != position)
                    {
                        meshes[i].sharedMesh = Assets.Load<Mesh>(mesh);
                    }
                }
            }
        }
        public static void ReplaceModel(string prefab, string mesh, int position = 0)
        {
            var fab = Addressables.LoadAssetAsync<GameObject>(prefab).WaitForCompletion();
            var meshes = fab.GetComponentsInChildren<SkinnedMeshRenderer>();
            meshes[position].sharedMesh = Assets.Load<Mesh>(mesh);
        }
        public static void ReplaceModel(GameObject fab, string mesh, int position = 0)
        {
            var meshes = fab.GetComponentsInChildren<SkinnedMeshRenderer>();
            meshes[position].sharedMesh = Assets.Load<Mesh>(mesh);
        }
        public static void ReplaceMaterial(string prefab, string material, int position = 0)
        {
            var fab = Addressables.LoadAssetAsync<GameObject>(prefab).WaitForCompletion();
            var mat = Assets.Load<Material>(material);
            var meshes = fab.GetComponentsInChildren<SkinnedMeshRenderer>();
            for (int i = 0; i < meshes[position].sharedMaterials.Length; i++)
            {
                Shader s = meshes[position].sharedMaterials[i].shader;
                meshes[position].sharedMaterials[i] = mat;
                meshes[position].sharedMaterials[i].shader = s;
            }
        }
        public static void ReplaceTexture(string prefab, string png, int position = 0)
        {
            var fab = Addressables.LoadAssetAsync<GameObject>(prefab).WaitForCompletion();
            var meshes = fab.GetComponentsInChildren<SkinnedMeshRenderer>();
            var texture = Assets.Load<Texture>(png);
            for (int i = 0; i < meshes[position].sharedMaterials.Length; i++)
            {
                if (prefab == "RoR2/Base/Shopkeeper/ShopkeeperBody.prefab" || prefab == "RoR2/Base/Titan/TitanGoldBody.prefab")
                {
                    //meshes[position].sharedMaterials[i].shader = LegacyShaderAPI.Find("Hopoo Games/Deferred/Standard");
                    meshes[position].sharedMaterials[i].shader = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Commando/CommandoBody.prefab").WaitForCompletion().GetComponentInChildren<SkinnedMeshRenderer>().material.shader;
                }
                meshes[position].sharedMaterials[i].color = Color.white;
                meshes[position].sharedMaterials[i].mainTexture = texture;
                meshes[position].sharedMaterials[i].SetTexture("_EmTex", RandomTwitch.blank);
                meshes[position].sharedMaterials[i].SetTexture("_NormalTex", null);
                if (png.Contains("frog"))
                {
                    meshes[position].sharedMaterials[i].SetTexture("_FresnelRamp", null);
                }
                if (png.Contains("shop"))
                {
                    meshes[position].sharedMaterials[i].SetTexture("_FlowHeightRamp", null);
                    meshes[position].sharedMaterials[i].SetTexture("_FlowHeightmap", null);
                }
            }
        }
        public static void ReplaceMeshFilter(string prefab, string mesh, string png, int position = 0)
        {
            var fab = Addressables.LoadAssetAsync<GameObject>(prefab).WaitForCompletion();
            var meshes = fab.GetComponentsInChildren<MeshFilter>();
            var texture = Assets.Load<Texture>(png);
            var renderers = fab.GetComponentsInChildren<Renderer>();
            for (int i = 0; i < renderers[position].sharedMaterials.Length; i++)
            {
                //renderers[position].sharedMaterials[i].shader = LegacyShaderAPI.Find("Hopoo Games/Deferred/Standard");
                renderers[position].sharedMaterials[i].shader = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Commando/CommandoBody.prefab").WaitForCompletion().GetComponentInChildren<SkinnedMeshRenderer>().material.shader;
                renderers[position].sharedMaterials[i].color = Color.white;
                renderers[position].sharedMaterials[i].mainTexture = texture;
                renderers[position].sharedMaterials[i].SetTexture("_EmTex", RandomTwitch.blank);
                renderers[position].sharedMaterials[i].SetTexture("_NormalTex", null);
            }

            meshes[position].sharedMesh = Assets.Load<Mesh>(mesh);
        }
        public static void ReplaceMeshFilter(GameObject prefab, string mesh, int position = 0)
        {
            var fab = prefab;
            var meshes = fab.GetComponentsInChildren<MeshFilter>();
            meshes[position].sharedMesh = Assets.Load<Mesh>(mesh);
        }
        public static void ReplaceMeshFilter(string prefab, string mesh, int position = 0)
        {
            var fab = Addressables.LoadAssetAsync<GameObject>(prefab).WaitForCompletion();
            var meshes = fab.GetComponentsInChildren<MeshFilter>();
            meshes[position].sharedMesh = Assets.Load<Mesh>(mesh);
        }
        public static void ReplaceParticleSystemmesh(GameObject fab, string mesh, int spot = 0)
        {
            var meshes = fab.GetComponentsInChildren<ParticleSystemRenderer>();
            meshes[spot].mesh = Assets.Load<Mesh>(mesh);
        }
        public static void ReplaceMeshRenderer(string f, string mesh, string png, int spot = 0)
        {
            var fab = Addressables.LoadAssetAsync<GameObject>(f).WaitForCompletion();
            var renderers = fab.GetComponentsInChildren<Renderer>();
            var meshes = fab.GetComponentsInChildren<MeshFilter>();
            var texture = Assets.Load<Texture>(png);
            for (int i = 0; i < renderers[spot].sharedMaterials.Length; i++)
            {
                renderers[spot].sharedMaterials[i].color = Color.white;
                renderers[spot].sharedMaterials[i].mainTexture = texture;
                renderers[spot].sharedMaterials[i].SetTexture("_EmTex", RandomTwitch.blank);
                renderers[spot].sharedMaterials[i].SetTexture("_NormalTex", null);
            }
            meshes[spot].sharedMesh = Assets.Load<Mesh>(mesh);
        }
        public static void ReplaceMeshRenderer(string f, string png, int spot = 0)
        {
            var fab = Addressables.LoadAssetAsync<GameObject>(f).WaitForCompletion();
            var renderers = fab.GetComponentsInChildren<Renderer>();
            var texture = Assets.Load<Texture>(png);
            for (int i = 0; i < renderers[spot].sharedMaterials.Length; i++)
            {
                renderers[spot].sharedMaterials[i].color = Color.white;
                renderers[spot].sharedMaterials[i].mainTexture = texture;
                renderers[spot].sharedMaterials[i].SetTexture("_EmTex", RandomTwitch.blank);
                renderers[spot].sharedMaterials[i].SetTexture("_NormalTex", null);
            }
        }
        public static void ReplaceMeshRenderer(GameObject fab, string mesh, string png, int spot = 0)
        {
            var renderers = fab.GetComponentsInChildren<Renderer>();
            var meshes = fab.GetComponentsInChildren<MeshFilter>();
            var texture = Assets.Load<Texture>(png);
            for (int i = 0; i < renderers[spot].sharedMaterials.Length; i++)
            {
                renderers[spot].sharedMaterials[i].color = Color.white;
                renderers[spot].sharedMaterials[i].mainTexture = texture;
                renderers[spot].sharedMaterials[i].SetTexture("_EmTex", RandomTwitch.blank);
                renderers[spot].sharedMaterials[i].SetTexture("_NormalTex", null);
            }
            meshes[spot].sharedMesh = Assets.Load<Mesh>(mesh);
        }
        public static void LoadResource(string resource)
        {
            Assets.AddBundle($"Models.{resource}");
            //DebugClass.Log($"Loading {resource}");
            // using (var assetStream = Assembly.GetExecutingAssembly().GetManifestResourceStream($"MoistureUpset.Models.{resource}"))
            // {
            //     var MainAssetBundle = AssetBundle.LoadFromStream(assetStream);
            //
            //     //ResourcesAPI.AddProvider(new AssetBundleResourcesProvider($"@MoistureUpset_{resource}", MainAssetBundle));
            //     Moisture_Upset.Moisture_Asset_Bundles.Add($"@MoistureUpset_{resource}", MainAssetBundle);
            // }
        }
        
        public static void LoadBNK(string bnk)
        {
            Assets.AddSoundBank($"{bnk}.bnk");
            
            // string s = $"MoistureUpset.bankfiles.{bnk}.bnk";
            // DebugClass.Log(s);
            // try
            // {
            //     using (var bankStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(s))
            //     {
            //         var bytes = new byte[bankStream.Length];
            //         bankStream.Read(bytes, 0, bytes.Length);
            //
            //         SoundBanks.Add(bytes);
            //     }
            // }
            // catch (Exception e)
            // {
            //     DebugClass.Log(e);
            // }
        }
        public static void RunAll()
        {
            try
            {
                ThanosQuotes();
                LoadBNK("ImWettest");
                LoadBNK("ImWettest2");
                DEBUG();
                Twitch();
                Cereal();
                Thanos();
                RobloxTitan();
                Alex();
                ElderLemurian();
                Lemurian();
                Golem();
                Bison();
                SolusUnit();
                Templar();
                Wisp();
                Sans();
                Imp();
                MiniMushroom();
                BeetleGuard();
                Beetle();
                TacoBell();
                Jelly();
                Shop();
                Names();
                Icons();
                _UI();
                NonEnemyNames();
                Shrines();
                LemmeSmash();
                Hagrid();
                Noodle();
                Skeleton();
                CrabRave();
                PUDDI();
                StringWorm();
                Discord();
                Copter();
                Rob();
                Nyan();
                Imposter();
                GreaterWisp();
                Collab();
                //SneakyFontReplacement();
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }
        }
        public static void DEBUG()
        {
            //DebugClass.DebugBones("RoR2/DLC1/Gup/GupBody.prefab");//gup
            //DebugClass.DebugBones("RoR2/DLC1/Gup/GeepBody.prefab");//geep
            //DebugClass.DebugBones("RoR2/DLC1/Gup/GipBody.prefab");//gip
            //DebugClass.DebugBones("RoR2/DLC1/VoidJailer/VoidJailerBody.prefab");//jailer
            //DebugClass.DebugBones("RoR2/DLC1/AcidLarva/AcidLarvaBody.prefab");//sonic
            //DebugClass.DebugBones("RoR2/DLC1/VoidBarnacle/VoidBarnacleBody.prefab");//barnicle
            //DebugClass.DebugBones("RoR2/DLC1/FlyingVermin/FlyingVerminBody.prefab");//blind pest
            //DebugClass.DebugBones("RoR2/DLC1/Vermin/VerminBody.prefab");//rats rats we are the rats
            //DebugClass.DebugBones("RoR2/Base/LunarExploder/LunarExploderBody.prefab");//lunar roller
            //DebugClass.DebugBones("RoR2/DLC1/MajorAndMinorConstruct/MinorConstructBody.prefab");//contructs???
            //DebugClass.DebugBones("RoR2/DLC1/MajorAndMinorConstruct/MajorConstructBody.prefab");//contructs???
            //DebugClass.DebugBones("RoR2/DLC1/MajorAndMinorConstruct/MegaConstructBody.prefab");//contructs???
            //DebugClass.DebugBones("RoR2/DLC1/ClayGrenadier/ClayGrenadierBody.prefab");//new clay guy
            //DebugClass.DebugBones("RoR2/DLC1/VoidMegaCrab/VoidMegaCrabBody.prefab");//pussy devastator
            //DebugClass.DebugBones("RoR2/Base/Grandparent/GrandParentBody.prefab");//grandparent
            //DebugClass.DebugBones("RoR2/DLC1/EliteVoid/VoidInfestorBody.prefab");//little shits that spawn when you get a void item
            //DebugClass.DebugBones("RoR2/DLC1/VoidRaidCrab/VoidRaidCrabBody.prefab");//voidling
            ////DebugClass.DebugBones("RoR2/DLC1/VoidRaidCrab/VoidRaidCrabJointBody.prefab");//voidling parts????



            //DebugClass.DebugBones("RoR2/DLC1/VoidSurvivor/VoidSurvivorBody.prefab");
            //DebugClass.DebugBones("RoR2/DLC1/Railgunner/RailgunnerBody.prefab");



            //LoadResource("moisture_testing");
            //ReplaceModel("RoR2/DLC1/AcidLarva/AcidLarvaBody.prefab", "@MoistureUpset_moisture_testing:assets/newenemies/bigma.mesh", "@MoistureUpset_moisture_testing:assets/newenemies/bigma.png");
            //ReplaceModel("RoR2/DLC1/AcidLarva/AcidLarvaBody.prefab", "@MoistureUpset_na:assets/na1.mesh", 1);
            //ReplaceModel("RoR2/DLC1/Gup/GupBody.prefab", "@MoistureUpset_moisture_testing:assets/newenemies/gup.mesh");
            //ReplaceModel("RoR2/DLC1/Gup/GeepBody.prefab", "@MoistureUpset_moisture_testing:assets/newenemies/gup1.mesh");
            //ReplaceModel("RoR2/DLC1/Gup/GipBody.prefab", "@MoistureUpset_moisture_testing:assets/newenemies/gup1.mesh");
            //ReplaceModel("RoR2/DLC1/VoidJailer/VoidJailerBody.prefab", "@MoistureUpset_moisture_testing:assets/newenemies/jailer.mesh");
            //ReplaceModel("RoR2/DLC1/VoidBarnacle/VoidBarnacleBody.prefab", "@MoistureUpset_moisture_testing:assets/newenemies/barnacle1.mesh");
            //ReplaceModel("RoR2/DLC1/FlyingVermin/FlyingVerminBody.prefab", "@MoistureUpset_moisture_testing:assets/newenemies/flying1.mesh");
            //ReplaceModel("RoR2/DLC1/Vermin/VerminBody.prefab", "@MoistureUpset_moisture_testing:assets/newenemies/vermin1.mesh");
            //ReplaceModel("RoR2/Base/LunarExploder/LunarExploderBody.prefab", "@MoistureUpset_moisture_testing:assets/newenemies/exploder1.mesh");
            //ReplaceModel("RoR2/DLC1/MajorAndMinorConstruct/MinorConstructBody.prefab", "@MoistureUpset_moisture_testing:assets/newenemies/minor1.mesh");
            //ReplaceModel("RoR2/DLC1/MajorAndMinorConstruct/MegaConstructBody.prefab", "@MoistureUpset_moisture_testing:assets/newenemies/mega.mesh");
            ////ReplaceModel("RoR2/DLC1/MajorAndMinorConstruct/MegaConstructBody.prefab", "@MoistureUpset_moisture_testing:assets/newenemies/mega2.mesh", 1);
            //ReplaceModel("RoR2/DLC1/ClayGrenadier/ClayGrenadierBody.prefab", "@MoistureUpset_moisture_testing:assets/newenemies/clay2.mesh", 1);
            //ReplaceModel("RoR2/DLC1/VoidMegaCrab/VoidMegaCrabBody.prefab", "@MoistureUpset_moisture_testing:assets/newenemies/megacrab.mesh");
            //ReplaceModel("RoR2/Base/Grandparent/GrandParentBody.prefab", "@MoistureUpset_moisture_testing:assets/newenemies/grandparent.mesh");
            //ReplaceModel("RoR2/DLC1/EliteVoid/VoidInfestorBody.prefab", "@MoistureUpset_moisture_testing:assets/newenemies/infestor1.mesh");
            //ReplaceModel("RoR2/DLC1/VoidRaidCrab/MiniVoidRaidCrabBodyPhase1.prefab", "@MoistureUpset_moisture_testing:assets/newenemies/raidcrab.mesh", "@MoistureUpset_moisture_testing:assets/newenemies/bigma.png");



        }
        private static void Icons()
        {
            if (BigJank.getOptionValue(Settings.FroggyChair))
                UImods.ReplaceTexture2D("RoR2/Base/Beetle/BeetleBody.png", "MoistureUpset.Resources.froggychair.png");
            if (BigJank.getOptionValue(Settings.Winston))
                UImods.ReplaceTexture2D("RoR2/Base/Beetle/BeetleGuardBody.png", "MoistureUpset.Resources.winston.png");
            if (BigJank.getOptionValue(Settings.Winston))
                UImods.ReplaceTexture2D("RoR2/Base/BeetleGland/BeetleGuardAllyBody.png", "MoistureUpset.Resources.winston.png");
            if (BigJank.getOptionValue(Settings.TacoBell))
                UImods.ReplaceTexture2D("RoR2/Base/Bell/BellBody.png", "MoistureUpset.Resources.tacobell.png");
            if (BigJank.getOptionValue(Settings.Thomas))
                UImods.ReplaceTexture2D("RoR2/Base/Bison/BisonBody.png", "MoistureUpset.Resources.thomas.png");
            if (BigJank.getOptionValue(Settings.Heavy))
                UImods.ReplaceTexture2D("RoR2/Base/ClayBruiser/ClayBruiserBody.png", "MoistureUpset.Resources.heavy.png");
            if (BigJank.getOptionValue(Settings.Robloxian))
                UImods.ReplaceTexture2D("RoR2/Base/Golem/GolemBody.png", "MoistureUpset.Resources.oof.png");
            if (BigJank.getOptionValue(Settings.Ghast))
                UImods.ReplaceTexture2D("RoR2/Base/GreaterWisp/GreaterWispBody.png", "MoistureUpset.Resources.ghast.png");
            if (BigJank.getOptionValue(Settings.TrumpetSkeleton))
                UImods.ReplaceTexture2D("RoR2/Base/Imp/ImpBody.png", "MoistureUpset.Resources.doot.png");
            if (BigJank.getOptionValue(Settings.Sans))
                UImods.ReplaceTexture2D("RoR2/Base/ImpBoss/ImpBossBody.png", "MoistureUpset.Resources.sans.png");
            if (BigJank.getOptionValue(Settings.Comedy))
                UImods.ReplaceTexture2D("RoR2/Base/Jellyfish/JellyfishBody.png", "MoistureUpset.Resources.joy.png");
            if (BigJank.getOptionValue(Settings.MikeWazowski))
                UImods.ReplaceTexture2D("RoR2/Base/Lemurian/LemurianBody.png", "MoistureUpset.Resources.mike.png");
            if (BigJank.getOptionValue(Settings.Bowser))
                UImods.ReplaceTexture2D("RoR2/Base/LemurianBruiser/LemurianBruiserBody.png", "MoistureUpset.Resources.bowser.png");
            if (BigJank.getOptionValue(Settings.ObamaPrism))
                UImods.ReplaceTexture2D("RoR2/Base/RoboBallBoss/RoboBallBossBody.png", "MoistureUpset.Resources.obamasphere.png");
            if (BigJank.getOptionValue(Settings.ObamaPrism))
                UImods.ReplaceTexture2D("RoR2/Base/RoboBallBoss/RoboBallMiniBody.png", "MoistureUpset.Resources.obamaprism.png");
            if (BigJank.getOptionValue(Settings.ObamaPrism))
                UImods.ReplaceTexture2D("RoR2/Base/RoboBallBoss/SuperRoboBallBossBody.png", "MoistureUpset.Resources.obamasphere.png");
            if (BigJank.getOptionValue(Settings.Dogplane))
                UImods.ReplaceTexture2D("RoR2/Base/Wisp/WispBody.png", "MoistureUpset.Resources.dogplane.png");
            if (BigJank.getOptionValue(Settings.Toad))
                UImods.ReplaceTexture2D("RoR2/Base/MiniMushroom/MiniMushroomBody.png", "MoistureUpset.Resources.toad.png");
            if (BigJank.getOptionValue(Settings.AlexJones))
                UImods.ReplaceTexture2D("RoR2/Base/Titan/TitanGoldBody.png", "MoistureUpset.Resources.alexjones.png");
            if (BigJank.getOptionValue(Settings.Hagrid))
                UImods.ReplaceTexture2D("RoR2/Base/Parent/ParentBody.png", "MoistureUpset.Resources.hagrid.png");
            if (BigJank.getOptionValue(Settings.RobloxTitan))
                UImods.ReplaceTexture2D("RoR2/Base/Titan/TitanBody.png", "MoistureUpset.Resources.buffroblox.png");
            if (BigJank.getOptionValue(Settings.LemmeSmash))
                UImods.ReplaceTexture2D("RoR2/Base/Vulture/VultureBody.png", "MoistureUpset.Resources.lemmesmash.png");
            if (BigJank.getOptionValue(Settings.CrabRave))
                UImods.ReplaceTexture2D("RoR2/Base/Nullifier/NullifierBody.png", "MoistureUpset.Resources.crab.png");
            if (BigJank.getOptionValue(Settings.SkeletonCrab))
                UImods.ReplaceTexture2D("RoR2/Base/HermitCrab/HermitCrabBody.png", "MoistureUpset.Resources.jockey.png");
            if (BigJank.getOptionValue(Settings.PoolNoodle))
                UImods.ReplaceTexture2D("RoR2/Base/MagmaWorm/MagmaWormBody.png", "MoistureUpset.Resources.noodle.png");
            if (BigJank.getOptionValue(Settings.Squirmles))
                UImods.ReplaceTexture2D("RoR2/Base/ElectricWorm/ElectricWormBody.png", "MoistureUpset.Resources.werm.png");
            if (BigJank.getOptionValue(Settings.GigaPuddi))
                UImods.ReplaceTexture2D("RoR2/Base/ClayBoss/ClayBossBody.png", "MoistureUpset.Resources.puddi.png");
            if (BigJank.getOptionValue(Settings.WanderingAtEveryone))
                UImods.ReplaceTexture2D("RoR2/Base/Vagrant/VagrantBody.png", "MoistureUpset.Resources.discord.png");
            if (BigJank.getOptionValue(Settings.Roflcopter))
                UImods.ReplaceTexture2D("RoR2/Base/LunarWisp/LunarWispBody.png", "MoistureUpset.Resources.rofl.png");
            if (BigJank.getOptionValue(Settings.Rob))
                UImods.ReplaceTexture2D("RoR2/Base/LunarGolem/LunarGolemBody.png", "MoistureUpset.Resources.rob.png");
            if (BigJank.getOptionValue(Settings.NyanCat))
                UImods.ReplaceTexture2D("RoR2/Base/Beetle/BeetleQueen2Body.png", "MoistureUpset.Resources.nyancat.png");
            if (BigJank.getOptionValue(Settings.Thanos))
                UImods.ReplaceTexture2D("RoR2/Base/Brother/texBrotherIcon.png", "MoistureUpset.Resources.thanos.png");
            if (BigJank.getOptionValue(Settings.Twitch))
                UImods.ReplaceTexture2D("RoR2/Base/Gravekeeper/GravekeeperBody.png", "MoistureUpset.Resources.twitch.png");
            if (BigJank.getOptionValue(Settings.Imposter))
                UImods.ReplaceTexture2D("RoR2/Base/Scav/ScavBody.png", "MoistureUpset.Resources.imposter.png");
            if (BigJank.getOptionValue(Settings.Imposter))
                UImods.ReplaceTexture2D("RoR2/Base/ScavLunar/ScavLunarBody.png", "MoistureUpset.Resources.imposter.png");
        }
        private static void NonEnemyNames()
        {
            //StringBuilder s = new StringBuilder();
            //On.RoR2.Run.Start += (orig, self) =>
            //{
            //    File.WriteAllText("CandiceDickFitInYourMouth.txt", s.ToString());
            //    orig(self);
            //};
            On.RoR2.Language.SetStringByToken += (orig, self, token, st) =>
            {
                //s.Append($"[{token}]    [{st}]" + '\n');
                if (BigJank.getOptionValue(Settings.NSFW) && BigJank.getOptionValue(Settings.DifficultyNames))
                {
                    if (st == "Drizzle")
                    {
                        st = "Jizzle";
                    }
                    else if (st == "Rainstorm")
                    {
                        st = "Jizzstorm";
                    }
                    else if (st == "Monsoon")
                    {
                        st = "Jizzoon";
                    }
                }
                if (BigJank.getOptionValue(Settings.InRunDifficultyNames))
                {
                    if (token == "DIFFICULTY_BAR_0")
                    {
                        st = "Baby Mode";
                    }
                    else if (token == "DIFFICULTY_BAR_1")
                    {
                        st = "Somebody once told me the";
                    }
                    else if (token == "DIFFICULTY_BAR_2")
                    {
                        st = "world was gonna roll me,";
                    }
                    else if (token == "DIFFICULTY_BAR_3")
                    {
                        st = "I ain't the sharpest tool";
                    }
                    else if (token == "DIFFICULTY_BAR_4")
                    {
                        st = "in the shed. She was looking";
                    }
                    else if (token == "DIFFICULTY_BAR_5")
                    {
                        st = "kind of dumb with her finger";
                    }
                    else if (token == "DIFFICULTY_BAR_6")
                    {
                        st = "and her thumb in the shape";
                    }
                    else if (token == "DIFFICULTY_BAR_7")
                    {
                        st = "of an L on her forehead.";
                    }
                    else if (token == "DIFFICULTY_BAR_8")
                    {
                        st = "Well, the years start coming";
                    }
                    else if (token == "DIFFICULTY_BAR_9")
                    {
                        st = " and they dont stop coming ";
                    }
                }
                if (BigJank.getOptionValue(Settings.ShrineChanges))
                {
                    if (token == "SHRINE_BLOOD_CONTEXT")
                    {
                        st = "Free Money";
                        if (BigJank.getOptionValue(Settings.CurrencyChanges))
                            st = "Free Tix";
                    }
                    else if (token == "SHRINE_BLOOD_USE_MESSAGE_2P")
                    {
                        st = "<style=cShrine>Wait it's not free. You have gained {1} gold.</color>";
                        if (BigJank.getOptionValue(Settings.CurrencyChanges))
                            st = "<style=cShrine>Wait it's not free. You have gained {1} tix.</color>";
                    }
                    else if (token == "SHRINE_BLOOD_USE_MESSAGE")
                    {
                        st = "<style=cShrine>{0} got stabbed for money, and has gained {1} gold.</color>";
                        if (BigJank.getOptionValue(Settings.CurrencyChanges))
                            st = "<style=cShrine>{0} got stabbed for money, and has gained {1} tix.</color>";
                    }


                    else if (token == "SHRINE_CHANCE_CONTEXT")
                    {
                        st = "Lose Money";
                    }
                    else if (token == "SHRINE_CHANCE_SUCCESS_MESSAGE_2P")
                    {
                        st = "<style=cShrine>You won the lottery!</color>";
                    }
                    else if (token == "SHRINE_CHANCE_SUCCESS_MESSAGE")
                    {
                        st = "<style=cShrine>{0} won the lottery!</color>";
                    }
                    else if (token == "SHRINE_CHANCE_FAIL_MESSAGE_2P")
                    {
                        st = "<style=cShrine>You lost money.</color>";
                        if (BigJank.getOptionValue(Settings.CurrencyChanges))
                            st = "<style=cShrine>You lost tix.</color>";
                    }
                    else if (token == "SHRINE_CHANCE_FAIL_MESSAGE")
                    {
                        st = "<style=cShrine>{0} lost money.</color>";
                        if (BigJank.getOptionValue(Settings.CurrencyChanges))
                            st = "<style=cShrine>{0} lost tix.</color>";
                    }


                    else if (token == "SHRINE_BOSS_CONTEXT")
                    {
                        st = "Get free stuff eventually";
                    }
                    else if (token == "SHRINE_BOSS_USE_MESSAGE_2P")
                    {
                        st = "<style=cShrine>You have earned some free stuff</color>";
                    }
                    else if (token == "SHRINE_BOSS_USE_MESSAGE")
                    {
                        st = "<style=cShrine>{0} has an overwhelming sense of superiority</color>";
                    }
                    else if (token == "SHRINE_BOSS_BEGIN_TRIAL")
                    {
                        st = "<style=cShrine>Let the challenge of the Mountain... begin!</style>";
                    }
                    else if (token == "SHRINE_BOSS_END_TRIAL")
                    {
                        st = "<style=cShrine>Your bravery is rewarded!</style>";
                    }
                }
                if (BigJank.getOptionValue(Settings.MiscOptions))
                {
                    if (token == "PLAYER_PING_COOLDOWN")
                    {
                        st = "<style=cEvent>Stop</style>";
                    }
                }
                if (BigJank.getOptionValue(Settings.Robloxian))
                {
                    if (token == "FAMILY_GOLEM")
                    {
                        st = "<style=cWorldEvent>[WARNING] It feels like 2008 in here..</style>";
                    }
                }
                if (BigJank.getOptionValue(Settings.Comedy))
                {
                    if (token == "FAMILY_JELLYFISH")
                    {
                        st = "<style=cWorldEvent>[WARNING] You hear a distant laugh track..</style>";
                    }
                }
                if (BigJank.getOptionValue(Settings.Dogplane))
                {
                    if (token == "FAMILY_WISP")
                    {
                        st = "<style=cWorldEvent>[WARNING] Habadabadaga..</style>";
                    }
                }
                if (BigJank.getOptionValue(Settings.TrumpetSkeleton))
                {
                    if (token == "FAMILY_IMP")
                    {
                        st = "<style=cWorldEvent>[WARNING] Doot doot..</style>";
                    }
                }


                if (BigJank.getOptionValue(Settings.AlexJones))
                {
                    if (token == "BAZAAR_SEER_GOLDSHORES")
                    {
                        st = "<style=cWorldEvent>You dream of fake news.</style>";
                    }
                }

                if (BigJank.getOptionValue(Settings.ObamaPrism))
                {
                    if (token == "VULTURE_EGG_WARNING")
                    {
                        st = "<style=cWorldEvent>You hear a distant press conference..</style>";
                    }
                }

                if (BigJank.getOptionValue(Settings.ObamaPrism))
                {
                    if (token == "VULTURE_EGG_BEGIN")
                    {
                        st = "<style=cWorldEvent>The press conference begins.</style>";
                    }
                }
                orig(self, token, st);
            };
        }
        private static void Shrines()
        {
        }
        private static void Names()
        {
            On.RoR2.Language.SetStringByToken += (orig, self, token, st) =>
            {
                if (st.Contains("Imp Overlord"))
                {
                    if (BigJank.getOptionValue(Settings.Sans))
                        st = st.Replace("Imp Overlord", "Sans");
                }
                else if (st == "Lord of the Red Plane")
                {
                    if (BigJank.getOptionValue(Settings.Sans))
                        st = "You're gonna have a bad time";
                }
                else if (st.Contains("Imp") && !st.Contains("Overlord") && !st.Contains("Impossible") && !st.Contains("Important") && !st.Contains("Improves"))
                {
                    if (BigJank.getOptionValue(Settings.TrumpetSkeleton))
                        st = st.Replace("Imp", "Trumpet Skeleton");
                }
                else if (st.Contains("Lesser Wisp"))
                {
                    if (BigJank.getOptionValue(Settings.Dogplane))
                        st = st.Replace("Lesser Wisp", "Dogplane");
                }
                else if (st.Contains("Jellyfish"))
                {
                    if (BigJank.getOptionValue(Settings.Comedy))
                        st = st.Replace("Jellyfish", "Comedy");
                }


                else if (st.Contains("Beetle Guard"))
                {
                    if (BigJank.getOptionValue(Settings.Winston))
                        st = st.Replace("Beetle Guard", "Winston");
                }
                else if (st.Contains("Beetle") && !st.Contains("Queen") && !st.Contains("Guard"))
                {
                    if (BigJank.getOptionValue(Settings.FroggyChair))
                        st = st.Replace("Beetle", "Froggy Chair");
                }


                else if (st.Contains("Elder Lemurian"))
                {
                    if (BigJank.getOptionValue(Settings.Bowser))
                        st = st.Replace("Elder Lemurian", "Bowser");
                }
                else if (st.Contains("Lemurian") && !st.Contains("Elder"))
                {
                    if (BigJank.getOptionValue(Settings.MikeWazowski))
                        st = st.Replace("Lemurian", "Mike Wazowski");
                }
                else if (st.Contains("Solus Probe"))
                {
                    if (BigJank.getOptionValue(Settings.ObamaPrism))
                        st = st.Replace("Solus Probe", "Obama Prism");
                }
                else if (st.Contains("Brass Contraption"))
                {
                    if (BigJank.getOptionValue(Settings.TacoBell))
                        st = st.Replace("Brass Contraption", "Taco Bell");
                }
                else if (st.Contains("Bighorn Bison"))
                {
                    if (BigJank.getOptionValue(Settings.Thomas))
                        st = st.Replace("Bighorn Bison", "Thomas");
                }
                else if (st.Contains("Stone Golem"))
                {
                    if (BigJank.getOptionValue(Settings.Robloxian))
                        st = st.Replace("Stone Golem", "Robloxian");
                }
                else if (st.Contains("Clay Templar"))
                {
                    if (BigJank.getOptionValue(Settings.Heavy))
                        st = st.Replace("Clay Templar", "Heavy");
                }
                else if (st.Contains("Greater Wisp"))
                {
                    if (BigJank.getOptionValue(Settings.Ghast))
                        st = st.Replace("Greater Wisp", "Ghast");
                }
                else if (st.Contains("Solus Control Unit"))
                {
                    if (BigJank.getOptionValue(Settings.ObamaPrism))
                        st = st.Replace("Solus Control Unit", "Obama Sphere");
                }
                else if (st == "Corrupted AI")
                {
                    if (BigJank.getOptionValue(Settings.ObamaPrism))
                        st = "Bringer of the Prisms";
                }
                else if (st == "Friend of Vultures")
                {
                    if (BigJank.getOptionValue(Settings.ObamaPrism))
                        st = "Friend of Prisms";
                }
                else if (st.Contains("Alloy Worship Unit"))
                {
                    if (BigJank.getOptionValue(Settings.ObamaPrism))
                        st = st.Replace("Alloy Worship Unit", "Obamium Worship Unit");
                }
                else if (st.Contains("Mini Mushrum"))
                {
                    if (BigJank.getOptionValue(Settings.Toad))
                        st = st.Replace("Mini Mushrum", "Toad");
                }
                else if (st.Contains("Aurelionite"))
                {
                    if (BigJank.getOptionValue(Settings.AlexJones))
                        st = st.Replace("Aurelionite", "Alex Jones");
                }
                else if (token == "TITANGOLD_BODY_SUBTITLE")
                {
                    if (BigJank.getOptionValue(Settings.AlexJones))
                        st = "Prince of the Social Media Shadow Realm";
                }
                else if (st.Contains("Stone Titan"))
                {
                    if (BigJank.getOptionValue(Settings.RobloxTitan))
                        st = st.Replace("Stone Titan", "Buff Robloxian");
                }
                else if (token == "TITAN_BODY_SUBTITLE")
                {
                    if (BigJank.getOptionValue(Settings.RobloxTitan))
                        st = "Oooooooooooooooooooooooooooof";
                }


                else if (st.Contains("Parent"))
                {
                    if (BigJank.getOptionValue(Settings.Hagrid))
                        st = st.Replace("Parent", "Hagrid");
                }


                else if (st.Contains("Alloy Vulture"))
                {
                    if (BigJank.getOptionValue(Settings.LemmeSmash))
                        st = st.Replace("Alloy Vulture", "Ron");
                }
                else if (st.Contains("Void Reaver"))
                {
                    if (BigJank.getOptionValue(Settings.CrabRave))
                        st = st.Replace("Void Reaver", "Crab Rave");
                }
                else if (st.Contains("Hermit Crab"))
                {
                    if (BigJank.getOptionValue(Settings.SkeletonCrab))
                        st = st.Replace("Hermit Crab", "Spider Jockey");
                }
                else if (st.Contains("Magma Worm"))
                {
                    if (BigJank.getOptionValue(Settings.PoolNoodle))
                        st = st.Replace("Magma Worm", "Pool Noodle");
                }
                else if (token == "MAGMAWORM_BODY_SUBTITLE")
                {
                    if (BigJank.getOptionValue(Settings.PoolNoodle))
                        st = "Defender of the pool";
                }
                else if (st.Contains("Overloading Worm"))
                {
                    if (BigJank.getOptionValue(Settings.Squirmles))
                        st = st.Replace("Overloading Worm", "Squirmle");
                }
                else if (token == "ELECTRICWORM_BODY_SUBTITLE")
                {
                    if (BigJank.getOptionValue(Settings.Squirmles))
                        st = "String not included";
                }

                else if (st.Contains("Clay Dunestrider"))
                {
                    if (BigJank.getOptionValue(Settings.GigaPuddi))
                        st = st.Replace("Clay Dunestrider", "Giga Puddi");
                }
                else if (token == "CLAYBOSS_BODY_SUBTITLE")
                {
                    if (BigJank.getOptionValue(Settings.GigaPuddi))
                        st = "Sugoku Dekkai, Giga Puddi!";
                }

                else if (st.Contains("Beetle Queen"))
                {
                    if (BigJank.getOptionValue(Settings.NyanCat))
                        st = st.Replace("Beetle Queen", "Nyan Cat");
                }
                else if (token == "BEETLEQUEEN_BODY_SUBTITLE")
                {
                    if (BigJank.getOptionValue(Settings.NyanCat))
                        st = "Nyan nyan nyan nyan nyan nyan nyan nyan nyan nyan!";
                }

                else if (token.Contains("LUNARGOLEM"))
                {
                    if (BigJank.getOptionValue(Settings.Rob))
                    {
                        st = st.Replace("Lunar Chimera", "Rob");
                        st = st.Replace("Zenith", "Meme");
                    }
                }
                else if (token.Contains("LUNARWISP"))
                {
                    if (BigJank.getOptionValue(Settings.Roflcopter))
                    {
                        st = st.Replace("Lunar Chimera", "Roflcopter");
                        st = st.Replace("Zenith", "Meme");
                    }
                }

                else if (st.Contains("Mithrix"))
                {
                    if (BigJank.getOptionValue(Settings.Thanos))
                        st = st.Replace("Mithrix", "Thanos");
                }
                else if (token == "BROTHER_BODY_SUBTITLE")
                {
                    if (BigJank.getOptionValue(Settings.Thanos))
                        st = "King of Balance";
                }

                else if (st.Contains("Grovetender"))
                {
                    if (BigJank.getOptionValue(Settings.Twitch))
                        st = st.Replace("Grovetender", "Twitch.exe");
                }
                else if (token == "GRAVEKEEPER_BODY_SUBTITLE")
                {
                    if (BigJank.getOptionValue(Settings.Twitch))
                        st = "Meme Cultivator";
                }

                else if (st.Contains("Artifact Reliquary"))
                {
                    if (BigJank.getOptionValue(Settings.Cereal))
                        st = st.Replace("Artifact Reliquary", "Reese's puffs");
                }
                else if (token == "ARTIFACTSHELL_BODY_DESCRIPTION")
                {
                    if (BigJank.getOptionValue(Settings.Cereal))
                        st = "Eat em up eat em up eat em up eat em up!";
                }

                else if (st.Contains("Wandering Vagrant"))
                {
                    if (BigJank.getOptionValue(Settings.WanderingAtEveryone))
                        st = st.Replace("Wandering Vagrant", "@Everyone");
                }
                else if (token == "VAGRANT_BODY_SUBTITLE")
                {
                    if (BigJank.getOptionValue(Settings.WanderingAtEveryone))
                        st = "PING!";
                }

                else if (st.Contains("Scavenger"))
                {
                    if (BigJank.getOptionValue(Settings.Imposter))
                        st = st.Replace("Scavenger", "<color=#D9262C>Crewmate</color>");
                }
                else if (token == "SCAV_BODY_SUBTITLE")
                {
                    if (BigJank.getOptionValue(Settings.Imposter))
                        st = "Idk, seems pretty sus to me";
                }

                else if (token == "GOLDCHEST_NAME")
                {
                    if (BigJank.getOptionValue(Settings.Interactables))
                        st = "Ender Chest";
                }
                else if (token == "GOLDCHEST_CONTEXT")
                {
                    if (BigJank.getOptionValue(Settings.Interactables))
                        st = "Open ender chest";
                }

                else if (token == "BARREL1_NAME")
                {
                    if (BigJank.getOptionValue(Settings.Interactables))
                        st = "Can";
                }
                else if (token == "BARREL1_CONTEXT")
                {
                    if (BigJank.getOptionValue(Settings.Interactables))
                        st = "Open can";
                }

                else if (token == "EQUIPMENTBARREL_NAME")
                {
                    if (BigJank.getOptionValue(Settings.Interactables))
                        st = "Shulker Box";
                }
                else if (token == "EQUIPMENTBARREL_CONTEXT")
                {
                    if (BigJank.getOptionValue(Settings.Interactables))
                        st = "Open shulker box";
                }

                else if (token == "MULTISHOP_TERMINAL_NAME")
                {
                    if (BigJank.getOptionValue(Settings.Interactables))
                        st = "Fidget Spinner";
                }
                else if (token == "MULTISHOP_TERMINAL_CONTEXT")
                {
                    if (BigJank.getOptionValue(Settings.Interactables))
                        st = "SPEEEEEEEEEEEEN";
                }
                else if (token == "SHOPKEEPER_BODY_NAME")
                {
                    if (BigJank.getOptionValue(Settings.Merchant))
                        st = "Beedle";
                }
                //else if (st.Contains("Jellyfish"))
                //{
                //    st = st.Replace("Jellyfish", "Comedy");
                //}
                orig(self, token, st);
            };
        }
        private static void ThanosQuotes()
        {
            if (BigJank.getOptionValue(Settings.Thanos))
                On.RoR2.Language.SetStringByToken += (orig, self, token, st) =>
                {
                    if (token == "BROTHER_SPAWN_PHASE1_1")
                    {
                        st = "When I’m done, half of humanity will still exist. Perfectly balanced, as all things should be. I hope they remember you.";
                    }
                    else if (token == "BROTHER_SPAWN_PHASE1_2")
                    {
                        st = "I will shred this universe down to it’s last atom and then, with the stones you’ve collected for me, create a new one. It is not what is lost but only what it is been given… a grateful universe.";
                    }
                    else if (token == "BROTHER_SPAWN_PHASE1_3")
                    {
                        st = "Dread it. Run from it. Destiny still arrives. Or should I say, I have.";
                    }
                    else if (token == "BROTHER_SPAWN_PHASE1_4")
                    {
                        st = "You couldn’t live with your own failure. Where did that bring you? Back to me.";
                    }


                    else if (token == "BROTHER_DAMAGEDEALT_1" || token == "BROTHERHURT_DAMAGEDEALT_1")
                    {
                        st = "You’re strong. But I could snap my fingers, and you’d all cease to exist.";
                    }
                    else if (token == "BROTHER_DAMAGEDEALT_2" || token == "BROTHERHURT_DAMAGEDEALT_2")
                    {
                        st = "Look. Pretty, isn’t it? Perfectly balanced. As all things should be.";
                    }
                    else if (token == "BROTHER_DAMAGEDEALT_3" || token == "BROTHERHURT_DAMAGEDEALT_3")
                    {
                        st = "I'm a survivor.";
                    }
                    else if (token == "BROTHER_DAMAGEDEALT_4" || token == "BROTHERHURT_DAMAGEDEALT_4")
                    {
                        st = "You're not the only one cursed with knowledge.";
                    }
                    else if (token == "BROTHER_DAMAGEDEALT_5" || token == "BROTHERHURT_DAMAGEDEALT_5")
                    {
                        st = "You should be grateful.";
                    }
                    else if (token == "BROTHER_DAMAGEDEALT_6" || token == "BROTHERHURT_DAMAGEDEALT_6")
                    {
                        st = "I don't even know who you are.";
                    }
                    else if (token == "BROTHER_DAMAGEDEALT_7" || token == "BROTHERHURT_DAMAGEDEALT_7")
                    {
                        st = "I am...inevitable.";
                    }
                    else if (token == "BROTHER_DAMAGEDEALT_8" || token == "BROTHERHURT_DAMAGEDEALT_8")
                    {
                        st = "Look. Pretty, isn’t it? Perfectly balanced. As all things should be.";
                    }
                    else if (token == "BROTHER_DAMAGEDEALT_9" || token == "BROTHERHURT_DAMAGEDEALT_9")
                    {
                        st = "I am...inevitable.";
                    }
                    else if (token == "BROTHER_DAMAGEDEALT_10" || token == "BROTHERHURT_DAMAGEDEALT_10")
                    {
                        st = "You should be grateful.";
                    }


                    else if (token == "BROTHER_KILL_1")
                    {
                        st = "I know what it’s like to lose. To feel so desperately that you’re right, yet to fail nonetheless.";
                    }
                    else if (token == "BROTHER_KILL_2")
                    {
                        st = "I ignored my destiny once, I can not do that again. Even for you. I’m sorry Little one.";
                    }
                    else if (token == "BROTHER_KILL_3")
                    {
                        st = "The universe required correction.";
                    }
                    else if (token == "BROTHER_KILL_4")
                    {
                        st = "I hope they remember you.";
                    }
                    else if (token == "BROTHER_KILL_5")
                    {
                        st = "A small price to pay for salvation.";
                    }


                    else if (token == "BROTHERHURT_KILL_1")
                    {
                        st = "I... had... to.";
                    }
                    else if (token == "BROTHERHURT_KILL_2")
                    {
                        st = "Rain fire!";
                    }
                    else if (token == "BROTHERHURT_KILL_3")
                    {
                        st = "You should’ve gone for the head.";
                    }
                    else if (token == "BROTHERHURT_KILL_4")
                    {
                        st = "Reality can be whatever I want.";
                    }
                    else if (token == "BROTHERHURT_KILL_5")
                    {
                        st = "Your optimism is misplaced.";
                    }


                    else if (token == "BROTHERHURT_DEATH_4")
                    {
                        st = "NO... NOT NOW...";
                    }
                    else if (token == "BROTHERHURT_DEATH_5")
                    {
                        st = "WHY... WHY NOW...?";
                    }
                    else if (token == "BROTHERHURT_DEATH_6")
                    {
                        st = "NO... NO...!";
                    }
                    orig(self, token, st);
                };
        }
        private static void _UI()
        {
            if (BigJank.getOptionValue(Settings.AwpUI))
                LoadBNK("awp");
            if (BigJank.getOptionValue(Settings.ChestNoises))
                LoadBNK("chestinteraction");
            LoadBNK("playerdeath");
        }
        private static void Sans()
        {
            if (!BigJank.getOptionValue(Settings.Sans))
                return;
            LoadBNK("sans");
            LoadResource("sans");
            ReplaceModel("RoR2/Base/ImpBoss/ImpBossBody.prefab", "@MoistureUpset_sans:assets/sans.mesh", "@MoistureUpset_sans:assets/sans.png");
            ReplaceMeshFilter("RoR2/Base/ImpBoss/ImpVoidspikeProjectileGhost.prefab", "@MoistureUpset_sans:assets/boner.mesh", "@MoistureUpset_sans:assets/boner.png");
        }
        private static void Shop()
        {
            if (!BigJank.getOptionValue(Settings.Merchant))
                return;
            LoadResource("shop");
            ReplaceModel("RoR2/Base/Shopkeeper/ShopkeeperBody.prefab", "@MoistureUpset_shop:assets/shop.mesh", "@MoistureUpset_shop:assets/shop.png");
            var fab = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Shopkeeper/ShopkeeperBody.prefab").WaitForCompletion();
            fab.GetComponentsInChildren<SkinnedMeshRenderer>()[0].sharedMaterial.shaderKeywords = null;


        }
        private static void BeetleGuard()
        {
            if (!BigJank.getOptionValue(Settings.Winston))
                return;
            LoadBNK("beetleguard");
            LoadResource("winston");
            var fab = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Beetle/BeetleGuardBody.prefab").WaitForCompletion();
            List<Transform> t = new List<Transform>();
            foreach (var item in fab.GetComponentsInChildren<Transform>())
            {
                if (!item.name.Contains("Hurtbox") && !item.name.Contains("IK") && !item.name.Contains("_end"))
                {
                    t.Add(item);
                }
            }
            foreach (var item in fab.GetComponentsInChildren<SkinnedMeshRenderer>())
            {
                item.bones = t.ToArray();
            }
            ReplaceModel("RoR2/Base/Beetle/BeetleGuardBody.prefab", "@MoistureUpset_winston:assets/winston.mesh", "@MoistureUpset_winston:assets/winston.png");
            fab = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/BeetleGland/BeetleGuardAllyBody.prefab").WaitForCompletion();
            t.Clear();
            foreach (var item in fab.GetComponentsInChildren<Transform>())
            {
                if (!item.name.Contains("Hurtbox") && !item.name.Contains("IK") && !item.name.Contains("_end"))
                {
                    t.Add(item);
                }
            }
            foreach (var item in fab.GetComponentsInChildren<SkinnedMeshRenderer>())
            {
                item.bones = t.ToArray();
            }
            ReplaceModel("RoR2/Base/BeetleGland/BeetleGuardAllyBody.prefab", "@MoistureUpset_winston:assets/winston.mesh", "@MoistureUpset_winston:assets/blinston.png");
            On.EntityStates.BeetleGuardMonster.FireSunder.OnEnter += (orig, self) =>
            {
                orig(self);
                AkSoundEngine.PostEvent("WinstonAttack2", self.outer.gameObject);
            };
            On.EntityStates.BeetleGuardMonster.GroundSlam.OnEnter += (orig, self) =>
            {
                orig(self);
                AkSoundEngine.PostEvent("WinstonAttack1", self.outer.gameObject);
            };


        }
        private static void Jelly()
        {
            if (!BigJank.getOptionValue(Settings.Comedy))
                return;
            LoadBNK("comedy");
            LoadResource("jelly");
            ReplaceModel("RoR2/Base/Jellyfish/JellyfishBody.prefab", "@MoistureUpset_jelly:assets/jelly.mesh", "@MoistureUpset_jelly:assets/jelly.png");
            On.EntityStates.JellyfishMonster.SpawnState.OnEnter += (orig, self) =>
            {
                orig(self);
                self.outer.commonComponents.sfxLocator.deathSound = "ComedyDeath";
            };
            On.EntityStates.JellyfishMonster.JellyNova.Detonate += (orig, self) =>
            {
                SoundAssets.PlaySound("JellyDetonate", self.outer.gameObject);
                //NetworkAssistant.playSound("JellyDetonate", self.outer.gameObject.transform.position);
                orig(self);
            };
        }
        private static void TacoBell()
        {
            if (!BigJank.getOptionValue(Settings.TacoBell))
                return;
            LoadBNK("tacobell");
            LoadResource("tacobell");
            On.EntityStates.Bell.DeathState.OnEnter += (orig, self) =>
            {
                AkSoundEngine.PostEvent("StopTacos", self.outer.gameObject);
                orig(self);
            };
            On.EntityStates.Bell.BellWeapon.ChargeTrioBomb.OnEnter += (orig, self) =>
            {
                try
                {
                    AkSoundEngine.PostEvent("StopTacos", self.outer.gameObject);
                    AkSoundEngine.PostEvent("TacoCreateAttack", self.outer.gameObject);
                }
                catch (Exception)
                {
                }
                orig(self);
            };
            ReplaceModel("RoR2/Base/Bell/BellBody.prefab", "@MoistureUpset_tacobell:assets/taco.mesh", "@MoistureUpset_tacobell:assets/taco.png");
            ReplaceMeshFilter("RoR2/Base/Bell/BellBallGhost.prefab", "@MoistureUpset_tacobell:assets/toco.mesh", "@MoistureUpset_tacobell:assets/toco.png");
        }
        private static void MiniMushroom()
        {
            if (!BigJank.getOptionValue(Settings.Toad))
                return;
            LoadBNK("toad");
            LoadResource("toad1");
            var fab = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/MiniMushroom/MiniMushroomBody.prefab").WaitForCompletion();
            List<Transform> t = new List<Transform>();
            foreach (var item in fab.GetComponentsInChildren<Transform>())
            {
                if (!item.name.Contains("Hurtbox") && !item.name.Contains("IK") && !item.name.Contains("_end") && !item.name.Contains("miniMush_R_Palps_02"))
                {
                    t.Add(item);
                }
            }
            for (int i = 0; i < 7; i++)
            {
                t.RemoveAt(t.Count - 1);
            }
            foreach (var item in fab.GetComponentsInChildren<SkinnedMeshRenderer>())
            {
                item.bones = t.ToArray();
            }
            ReplaceModel("RoR2/Base/MiniMushroom/MiniMushroomBody.prefab", "@MoistureUpset_toad1:assets/toad.mesh", "@MoistureUpset_toad1:assets/toad.png");
            ReplaceMeshFilter("RoR2/Base/MiniMushroom/SporeGrenadeGhost.prefab", "@MoistureUpset_toad1:assets/toadbomb.mesh", "@MoistureUpset_toad1:assets/toadbomb.png");
            var g = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/MiniMushroom/SporeGrenadeGhost.prefab").WaitForCompletion();
            var meshfilter = g.GetComponentInChildren<MeshFilter>();
            var skinnedmesh = meshfilter.gameObject.AddComponent<SkinnedMeshRenderer>() as SkinnedMeshRenderer;
            skinnedmesh.transform.position = g.transform.position;
            skinnedmesh.sharedMesh = Assets.Load<Mesh>("@MoistureUpset_toad1:assets/toadbomblid.mesh");

            skinnedmesh.sharedMaterial = Assets.Load<Material>("@MoistureUpset_toad1:assets/toadbomb.mat");
            skinnedmesh.sharedMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
            skinnedmesh.sharedMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            skinnedmesh.sharedMaterial.SetInt("_ZWrite", 0);
            skinnedmesh.sharedMaterial.DisableKeyword("_ALPHATEST_ON");
            skinnedmesh.sharedMaterial.DisableKeyword("_ALPHABLEND_ON");
            skinnedmesh.sharedMaterial.EnableKeyword("_ALPHAPREMULTIPLY_ON");
            skinnedmesh.sharedMaterial.renderQueue = 3000;
            meshfilter.transform.localScale *= .7f;

            var splat = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/MiniMushroom/SporeGrenadeProjectileDotZone.prefab").WaitForCompletion();
            splat.GetComponentInChildren<MeshRenderer>().gameObject.transform.localScale = new Vector3(2.4f, 0.6128117f, 2.4f);
            var texture = Assets.Load<Texture>("@MoistureUpset_toad1:assets/toadsplatcolorhighres.png");
            foreach (var item in splat.GetComponentsInChildren<ThreeEyedGames.Decal>())
            {
                item.Material.shaderKeywords = null;
                item.RenderMode = ThreeEyedGames.Decal.DecalRenderMode.Unlit;
                item.Material.color = Color.white;
                foreach (var mat in item.Material.GetTexturePropertyNames())
                {
                    //Debug.Log($"--=={item.Material.GetTexture(mat)}");
                    if (item.Material.GetTexture(mat) && item.Material.GetTexture(mat).name == "texMushDecalMask")
                    {
                        item.Material.SetTexture(mat, texture);
                        item.transform.localScale *= .95f;
                    }
                }
                item.Material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                item.Material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                item.Material.SetInt("_ZWrite", 0);
                item.Material.DisableKeyword("_ALPHATEST_ON");
                item.Material.DisableKeyword("_ALPHABLEND_ON");
                item.Material.EnableKeyword("_ALPHAPREMULTIPLY_ON");
                item.Material.renderQueue = 3000;
            }
        }
        private static void Imp()
        {
            if (!BigJank.getOptionValue(Settings.TrumpetSkeleton))
                return;
            LoadResource("dooter");
            ReplaceModel("RoR2/Base/Imp/ImpBody.prefab", "@MoistureUpset_dooter:assets/dooter.mesh", "@MoistureUpset_dooter:assets/dooter.png");


            On.EntityStates.ImpMonster.BlinkState.OnEnter += (orig, self) =>
            {
                Util.PlaySound("Doot", self.outer.gameObject);
                orig(self);
            };
            On.EntityStates.ImpMonster.SpawnState.OnEnter += (orig, self) =>
            {
                Util.PlaySound("Doot", self.outer.gameObject);
                orig(self);
            };
        }
        private static void GetChildren(Transform t, ref List<Transform> l, int depth = 0)
        {
            StringBuilder sb = new StringBuilder();
            l.Add(t);
            for (int i = 0; i < depth; i++)
            {
                sb.Append("====");
            }
            DebugClass.Log($"{sb.ToString()}=={depth}======{t.name}");
            for (int i = 0; i < t.childCount; i++)
            {
                GetChildren(t.GetChild(i), ref l, depth + 1);
            }
        }
        private static void Beetle()
        {
            if (!BigJank.getOptionValue(Settings.FroggyChair))
                return;
            LoadBNK("beetle");
            LoadResource("frog");
            var fab = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Beetle/BeetleBody.prefab").WaitForCompletion();
            fab.GetComponentInChildren<SfxLocator>().barkSound = "ChairIdle";
            List<Transform> t = new List<Transform>();
            //this is the fucking stupid but it works (minus claws)
            foreach (var item in fab.GetComponentsInChildren<Transform>())
            {
                if (!item.name.Contains("Hurtbox") && !item.name.Contains("BeetleBody") && !item.name.Contains("Mesh") && !item.name.Contains("mdl"))
                {
                    t.Add(item);
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
            foreach (var item in fab.GetComponentsInChildren<SkinnedMeshRenderer>())
            {
                item.bones = t.ToArray();
            }
            ReplaceModel("RoR2/Base/Beetle/BeetleBody.prefab", "@MoistureUpset_frog:assets/frogchair.mesh", "@MoistureUpset_frog:assets/frogchair.png");
        }
        private static void ElderLemurian()
        {
            if (!BigJank.getOptionValue(Settings.Bowser))
                return;
            LoadBNK("bowser");
            LoadResource("bowser");
            ReplaceModel("RoR2/Base/LemurianBruiser/LemurianBruiserBody.prefab", "@MoistureUpset_bowser:assets/bowser.mesh", "@MoistureUpset_bowser:assets/bowser.png");
            On.EntityStates.LemurianBruiserMonster.FireMegaFireball.OnEnter += (orig, self) =>
            {
                EntityStates.LemurianBruiserMonster.FireMegaFireball.attackString = "BowserFireBall";
                orig(self);
            };
            On.EntityStates.LemurianBruiserMonster.Flamebreath.OnEnter += (orig, self) =>
            {
                Util.PlaySound("BowserBreath", self.outer.gameObject);
                orig(self);
            };
            On.EntityStates.LemurianBruiserMonster.SpawnState.OnEnter += (orig, self) =>
            {
                EntityStates.LemurianBruiserMonster.SpawnState.spawnSoundString = "BowserSpawn";
                orig(self);
            };
            //On.EntityStates.GenericCharacterDeath.PlayDeathSound += (orig, self) =>
            //{
            //    if (self.outer.name.ToUpper().Contains("LEMURIANBRUISER"))
            //    {
            //        Util.PlaySound("BowserDeath", self.outer.gameObject);
            //    }
            //    orig(self);
            //};
        }
        private static void Templar()
        {
            if (!BigJank.getOptionValue(Settings.Heavy))
                return;
            LoadBNK("heavy");
            LoadResource("heavy");
            ReplaceModel("RoR2/Base/ClayBruiser/ClayBruiserBody.prefab", "@MoistureUpset_heavy:assets/heavy.mesh", "@MoistureUpset_heavy:assets/heavy.png");
            ReplaceModel("RoR2/Base/ClayBruiser/ClayBruiserBody.prefab", "@MoistureUpset_heavy:assets/minigun.mesh", "@MoistureUpset_heavy:assets/heavy.png", 1);

            var fab = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/ClayBruiser/ClayBruiserBody.prefab").WaitForCompletion();
            var meshes = fab.GetComponentsInChildren<SkinnedMeshRenderer>();
            for (int i = 0; i < meshes.Length; i++)
            {
                if (i != 0 && i != 1)
                {
                    meshes[i].sharedMesh = Assets.Load<Mesh>("@MoistureUpset_na:assets/na.mesh");
                }
            }

            On.EntityStates.ClayBruiser.Weapon.MinigunFire.OnEnter += (orig, self) =>
            {
                Util.PlaySound("HeavyFire", self.outer.gameObject);
                orig(self);
            };
            On.EntityStates.ClayBruiser.Weapon.FireSonicBoom.OnEnter += (orig, self) =>
            {
                Util.PlaySound("SonicBoom", self.outer.gameObject);
                orig(self);
            };
            On.EntityStates.ClayBruiserMonster.SpawnState.OnEnter += (orig, self) =>
            {
                EntityStates.ClayBruiserMonster.SpawnState.spawnSoundString = "HeavySpawn";
                orig(self);
            };
            //On.EntityStates.ClayBruiser.Weapon.MinigunSpinUp.OnEnter += (orig, self) =>
            //{
            //    Util.PlaySound("HeavySpottedPlayer", self.outer.gameObject);
            //    orig(self);
            //};
            //On.EntityStates.GenericCharacterDeath.PlayDeathSound += (orig, self) =>
            //{
            //    //Debug.Log($"selfname-------------{self.outer.name}");
            //    if (self.outer.name.ToUpper().Contains("CLAYBRUISER"))
            //    {
            //        Util.PlaySound("HeavyDeath", self.outer.gameObject);
            //    }
            //    orig(self);
            //};
        }
        private static void GreaterWisp()
        {
            if (!BigJank.getOptionValue(Settings.Ghast))
                return;
            LoadBNK("ghast");
            LoadResource("ghast");
            ReplaceModel("RoR2/Base/GreaterWisp/GreaterWispBody.prefab", "@MoistureUpset_ghast:assets/ghast.mesh", "@MoistureUpset_ghast:assets/ghast.png");
            var fab = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/GreaterWisp/GreaterWispBody.prefab").WaitForCompletion();
            fab.GetComponentInChildren<FlickerLight>().enabled = false;
            var fixer = fab.AddComponent<GhastFixerButTheGhastNotTheFireballs>();
            var components = fab.GetComponentsInChildren<Component>();
            foreach (var item in components)
            {
                if (item.name == "Fire" || item.name == "Flames")
                {
                    try
                    {
                        ((ParticleSystem)item).maxParticles = 0;
                    }
                    catch (Exception)
                    {
                    }
                }
            }

            fab = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/GreaterWisp/ChargeGreaterWisp.prefab").WaitForCompletion();
            fab.AddComponent<FireballFixer>();
            fab = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/GreaterWisp/WispCannonGhost.prefab").WaitForCompletion();
            fab.AddComponent<FireballFixer>();
            fab = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/GreaterWisp/OmniExplosionVFXGreaterWisp.prefab").WaitForCompletion();
            fab.AddComponent<GhastFixer>();
            fab.GetComponentInChildren<EffectComponent>().soundName = "MinecraftExplosion";

            bool doneit = false;
            On.EntityStates.GreaterWispMonster.SpawnState.OnEnter += (orig, self) =>
            {
                orig(self);
                if (!doneit)
                {
                    foreach (var item in self.outer.gameObject.GetComponentsInChildren<Light>())
                    {
                        item.enabled = false;
                    }
                    fab = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/GreaterWisp/GreaterWispBody.prefab").WaitForCompletion();
                    foreach (var item in fab.GetComponentsInChildren<Light>())
                    {
                        item.enabled = false;
                    }

                    fab = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/GreaterWisp/ChargeGreaterWisp.prefab").WaitForCompletion();
                    foreach (var item in fab.GetComponentsInChildren<Renderer>())
                    {
                        item.enabled = false;
                    }
                    foreach (var item in fab.GetComponentsInChildren<Light>())
                    {
                        item.enabled = false;
                    }


                    fab = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/GreaterWisp/WispCannonGhost.prefab").WaitForCompletion();
                    foreach (var item in fab.GetComponentsInChildren<Renderer>())
                    {
                        item.enabled = false;
                    }
                    foreach (var item in fab.GetComponentsInChildren<Light>())
                    {
                        item.enabled = false;
                    }


                    fab = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/GreaterWisp/OmniExplosionVFXGreaterWisp.prefab").WaitForCompletion();
                    foreach (var item in fab.GetComponentsInChildren<ParticleSystem>())
                    {
                        item.maxParticles = 0;
                    }
                    foreach (var item in fab.GetComponentsInChildren<Light>())
                    {
                        item.enabled = false;
                    }
                    doneit = true;
                }
            };
            On.EntityStates.GreaterWispMonster.SpawnState.OnEnter += (orig, self) =>
            {
                //DebugClass.Log($"----------{self.outer.gameObject}");
                //self.outer.gameObject.GetComponent<GhastFixer>().mat = self.outer.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().sharedMaterial;
                orig(self);
            };
            On.EntityStates.GreaterWispMonster.DeathState.OnEnter += (orig, self) =>
            {
                Util.PlaySound("GhastDeath", self.outer.gameObject);
                orig(self);
            };
            On.EntityStates.GreaterWispMonster.FireCannons.OnEnter += (orig, self) =>
            {
                Util.PlaySound("GhastAttack", self.outer.gameObject);
                self.outer.gameObject.GetComponent<GhastFixerButTheGhastNotTheFireballs>().Shot();
                orig(self);
            };
        }
        private static void Wisp()
        {
            if (!BigJank.getOptionValue(Settings.Dogplane))
                return;
            LoadBNK("dogplane");
            LoadResource("dogplane");
            ReplaceModel("RoR2/Base/Wisp/WispBody.prefab", "@MoistureUpset_dogplane:assets/bahdog.mesh", "@MoistureUpset_dogplane:assets/bahdog.png");
            ReplaceModel("RoR2/Base/WispOnDeath/WispSoulBody.prefab", "@MoistureUpset_dogplane:assets/bahdog.mesh", "@MoistureUpset_dogplane:assets/bahdog.png");
            On.EntityStates.Wisp1Monster.ChargeEmbers.OnEnter += (orig, self) =>
            {
                EntityStates.Wisp1Monster.ChargeEmbers.attackString = "DogCharge";
                orig(self);
            };
            On.EntityStates.Wisp1Monster.FireEmbers.OnEnter += (orig, self) =>
            {
                EntityStates.Wisp1Monster.FireEmbers.attackString = "DogFire";
                orig(self);
            };
            On.EntityStates.Wisp1Monster.SpawnState.OnEnter += (orig, self) =>
            {
                EntityStates.Wisp1Monster.SpawnState.spawnSoundString = "DogSpawn";
                orig(self);
            };
            var fab = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Wisp/WispBody.prefab").WaitForCompletion();
            var meshes = fab.GetComponentsInChildren<Component>();
            foreach (var item in meshes)
            {
                if (item.name == "Fire" || item.name == "Flames")
                {
                    try
                    {
                        ((ParticleSystem)item).maxParticles = 0;
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }
        private static void SolusUnit()
        {
            if (!BigJank.getOptionValue(Settings.ObamaPrism))
                return;
            LoadBNK("prism");
            LoadResource("obamaprism");
            ReplaceModel("RoR2/Base/RoboBallBoss/RoboBallMiniBody.prefab", "@MoistureUpset_obamaprism:assets/Obamium.mesh", "@MoistureUpset_obamaprism:assets/Obruhma.png");
            ReplaceModel("RoR2/Base/RoboBallBoss/RoboBallBossBody.prefab", "@MoistureUpset_obamaprism:assets/obamasphere.mesh", "@MoistureUpset_obamaprism:assets/Obruhma.png");
            ReplaceModel("RoR2/Base/RoboBallBoss/SuperRoboBallBossBody.prefab", "@MoistureUpset_obamaprism:assets/obamasphere.mesh", "@MoistureUpset_obamaprism:assets/Obruhma.png");
            ReplaceMeshFilter("RoR2/Base/RoboBallBoss/SuperRoboBallBossBody.prefab", "@MoistureUpset_obamaprism:assets/crown.mesh", "@MoistureUpset_obamaprism:assets/crown.png");
            ReplaceMeshFilter("RoR2/Base/RoboBallBoss/SuperRoboBallBossBody.prefab", "@MoistureUpset_na:assets/na1.mesh", "@MoistureUpset_obamaprism:assets/crown.png", 1);
            ReplaceMeshFilter("RoR2/Base/RoboBallBoss/SuperRoboBallBossBody.prefab", "@MoistureUpset_na:assets/na1.mesh", "@MoistureUpset_obamaprism:assets/crown.png", 2);
            ReplaceMeshFilter("RoR2/Base/RoboBallBoss/SuperRoboBallBossBody.prefab", "@MoistureUpset_na:assets/na1.mesh", "@MoistureUpset_obamaprism:assets/crown.png", 3);
            ReplaceMeshFilter("RoR2/Base/RoboBallBoss/SuperRoboBallBossBody.prefab", "@MoistureUpset_na:assets/na1.mesh", "@MoistureUpset_obamaprism:assets/crown.png", 4);
            ReplaceMeshFilter("RoR2/Base/RoboBallBoss/SuperRoboBallBossBody.prefab", "@MoistureUpset_na:assets/na1.mesh", "@MoistureUpset_obamaprism:assets/crown.png", 5);
            ReplaceMeshFilter("RoR2/Base/RoboBallBoss/SuperRoboBallBossBody.prefab", "@MoistureUpset_na:assets/na1.mesh", "@MoistureUpset_obamaprism:assets/crown.png", 6);

            ReplaceModel("RoR2/Base/RoboBallBuddy/RoboBallGreenBuddyBody.prefab", "@MoistureUpset_na:assets/na1.mesh", 0);
            ReplaceModel("RoR2/Base/RoboBallBuddy/RoboBallGreenBuddyBody.prefab", "@MoistureUpset_na:assets/na1.mesh", 1);
            ReplaceModel("RoR2/Base/RoboBallBuddy/RoboBallGreenBuddyBody.prefab", "@MoistureUpset_obamaprism:assets/Obamium.mesh", "@MoistureUpset_obamaprism:assets/Obruhma.png", 2);
            ReplaceMeshFilter("RoR2/Base/RoboBallBuddy/RoboBallGreenBuddyBody.prefab", "@MoistureUpset_na:assets/na1.mesh", 0);

            ReplaceModel("RoR2/Base/RoboBallBuddy/RoboBallRedBuddyBody.prefab", "@MoistureUpset_obamaprism:assets/Obamium.mesh", "@MoistureUpset_obamaprism:assets/Obruhma.png", 1);
            ReplaceMeshFilter("RoR2/Base/RoboBallBuddy/RoboBallRedBuddyBody.prefab", "@MoistureUpset_na:assets/na1.mesh", 0);
            ReplaceModel("RoR2/Base/RoboBallBuddy/RoboBallRedBuddyBody.prefab", "@MoistureUpset_na:assets/na1.mesh", 0);
            //"@MoistureUpset_na:assets/na1.mesh"
            On.EntityStates.RoboBallBoss.DeathState.OnEnter += (orig, self) =>
            {
                Util.PlaySound("ObamaDeath", self.outer.gameObject);
                orig(self);
            };
            On.EntityStates.RoboBallBoss.SpawnState.OnEnter += (orig, self) =>
            {
                Util.PlaySound("ObamaSpawn", self.outer.gameObject);
                orig(self);
            };
            On.EntityStates.RoboBallBoss.Weapon.ChargeEyeblast.OnEnter += (orig, self) =>
            {
                Util.PlaySound("ObamaCharge", self.outer.gameObject);
                orig(self);
            };
            On.EntityStates.RoboBallBoss.Weapon.DeployMinions.OnEnter += (orig, self) =>
            {
                Util.PlaySound("ObamaDeploy", self.outer.gameObject);
                orig(self);
            };
        }
        private static void Lemurian()
        {
            if (!BigJank.getOptionValue(Settings.MikeWazowski))
                return;
            LoadResource("mike");
            On.EntityStates.LemurianMonster.Bite.OnEnter += (orig, self) =>
            {
                Util.PlaySound("MikeAttack", self.outer.gameObject);
                orig(self);
            };
            On.EntityStates.LemurianMonster.ChargeFireball.OnEnter += (orig, self) =>
            {
                Util.PlaySound("MikeAttack", self.outer.gameObject);
                orig(self);
            };
            ReplaceModel("RoR2/Base/Lemurian/LemurianBody.prefab", "@MoistureUpset_mike:assets/mike.mesh", "@MoistureUpset_mike:assets/mike.png");
        }
        private static void Golem()
        {
            if (!BigJank.getOptionValue(Settings.Robloxian))
                return;
            LoadBNK("oof");
            LoadResource("noob");
            Material nolaser = UnityEngine.Object.Instantiate<Material>(Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Commando/CommandoBody.prefab").WaitForCompletion().GetComponentInChildren<SkinnedMeshRenderer>().material);
            nolaser.mainTexture = Assets.Load<Texture>("@MoistureUpset_noob:assets/Noob1Tex.png");
            Material laser = UnityEngine.Object.Instantiate<Material>(Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Commando/CommandoBody.prefab").WaitForCompletion().GetComponentInChildren<SkinnedMeshRenderer>().material);
            laser.mainTexture = Assets.Load<Texture>("@MoistureUpset_noob:assets/Noob1TexLaser.png");
            On.EntityStates.GolemMonster.ChargeLaser.OnEnter += (orig, self) =>
            {
                EntityStates.GolemMonster.ChargeLaser.attackSoundString = "GolemChargeLaser";
                self.outer.gameObject.GetComponent<GolemRandomizer>().Charge();
                //self.outer.gameObject.GetComponentInChildren<ModelLocator>().modelTransform.gameObject.GetComponentInChildren<CharacterModel>().baseRendererInfos[0].defaultMaterial = laser;
                orig(self);
            };
            On.EntityStates.GolemMonster.FireLaser.OnEnter += (orig, self) =>
            {
                EntityStates.GolemMonster.FireLaser.attackSoundString = "GolemFireLaser";
                self.outer.gameObject.GetComponent<GolemRandomizer>().Shoot();
                //self.outer.gameObject.GetComponentInChildren<ModelLocator>().modelTransform.gameObject.GetComponentInChildren<CharacterModel>().baseRendererInfos[0].defaultMaterial = nolaser;
                orig(self);
            };
            On.EntityStates.GolemMonster.ClapState.OnEnter += (orig, self) =>
            {
                try
                {

                    EntityStates.GolemMonster.ClapState.attackSoundString = "GolemMelee";
                }
                catch (Exception)
                {
                }
                orig(self);
            };
            ReplaceModel("RoR2/Base/Golem/GolemBody.prefab", "@MoistureUpset_noob:assets/N00b.mesh", "@MoistureUpset_noob:assets/Noob1Tex.png");
            On.EntityStates.GolemMonster.SpawnState.OnEnter += (orig, self) =>
            {
                Transform transform = self.outer.gameObject.GetComponentInChildren<ModelLocator>().modelTransform;
                if (transform)
                {
                    transform.GetComponent<PrintController>().printTime = 4;
                    transform.GetComponent<PrintController>().maxPrintHeight = 9;
                }
                orig(self);
            };
            //ReplaceModel("RoR2/Base/Golem/GolemBody.prefab", "@MoistureUpset_noob:assets/N00b.mesh", "@MoistureUpset_noob:assets/Noob1Tex.png");
            var fab = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Golem/GolemBody.prefab").WaitForCompletion();
            GolemRandomizer randomizer = fab.AddComponent<GolemRandomizer>();
            foreach (var item in fab.GetComponentInChildren<ModelLocator>().modelTransform.gameObject.GetComponentInChildren<CharacterModel>().GetComponentsInChildren<RoR2.ModelSkinController>())
            {
                for (int i = 0; i < item.skins.Length; i++)
                {
                    if (i != 0)
                    {
                        item.skins[i] = item.skins[0];
                    }
                }
            }

            GolemRandomizer.materials.Add(Assets.RobloxMaterial("@MoistureUpset_noob:assets/Noob1Tex.png"));
            GolemRandomizer.materials.Add(Assets.RobloxMaterial("@MoistureUpset_noob:assets/robloxcharacters/elsa.png"));
            GolemRandomizer.materials.Add(Assets.RobloxMaterial("@MoistureUpset_noob:assets/robloxcharacters/spiderman.png"));
            GolemRandomizer.materials.Add(Assets.RobloxMaterial("@MoistureUpset_noob:assets/robloxcharacters/pickle.png"));
            GolemRandomizer.materials.Add(Assets.RobloxMaterial("@MoistureUpset_noob:assets/robloxcharacters/guest.png"));
            GolemRandomizer.materials.Add(Assets.RobloxMaterial("@MoistureUpset_noob:assets/robloxcharacters/furry.png"));
            GolemRandomizer.materials.Add(Assets.RobloxMaterial("@MoistureUpset_noob:assets/robloxcharacters/naruto.png"));
            GolemRandomizer.materials.Add(Assets.RobloxMaterial("@MoistureUpset_noob:assets/robloxcharacters/fortnite.png"));
            GolemRandomizer.materials.Add(Assets.RobloxMaterial("@MoistureUpset_noob:assets/robloxcharacters/pizza.png"));
            GolemRandomizer.materials.Add(Assets.RobloxMaterial("@MoistureUpset_noob:assets/robloxcharacters/dream.png"));
            GolemRandomizer.materials.Add(Assets.RobloxMaterial("@MoistureUpset_noob:assets/robloxcharacters/soldier.png"));
            GolemRandomizer.materials.Add(Assets.RobloxMaterial("@MoistureUpset_noob:assets/robloxcharacters/police.png"));
            GolemRandomizer.materials.Add(Assets.RobloxMaterial("@MoistureUpset_noob:assets/robloxcharacters/inmate.png"));
            GolemRandomizer.materials.Add(Assets.RobloxMaterial("@MoistureUpset_noob:assets/robloxcharacters/robber.png"));
            GolemRandomizer.materials.Add(Assets.RobloxMaterial("@MoistureUpset_noob:assets/robloxcharacters/man.png"));
            GolemRandomizer.materials.Add(Assets.RobloxMaterial("@MoistureUpset_noob:assets/robloxcharacters/girl.png"));
            GolemRandomizer.materials.Add(Assets.RobloxMaterial("@MoistureUpset_noob:assets/robloxcharacters/default.png"));
            GolemRandomizer.materials.Add(Assets.RobloxMaterial("@MoistureUpset_noob:assets/robloxcharacters/dummy.png"));
            GolemRandomizer.materials.Add(Assets.RobloxMaterial("@MoistureUpset_noob:assets/robloxcharacters/builderman.png"));
            GolemRandomizer.materials.Add(Assets.RobloxMaterial("@MoistureUpset_noob:assets/robloxcharacters/telamon.png"));
            GolemRandomizer.materials.Add(Assets.RobloxMaterial("@MoistureUpset_noob:assets/robloxcharacters/toothy.png"));


            GolemRandomizer.meshes.Add(Assets.Load<Mesh>("@MoistureUpset_noob:assets/N00b.mesh"));
            GolemRandomizer.meshes.Add(Assets.Load<Mesh>("@MoistureUpset_noob:assets/robloxcharacters/elsa.mesh"));
            GolemRandomizer.meshes.Add(Assets.Load<Mesh>("@MoistureUpset_noob:assets/robloxcharacters/spiderman.mesh"));
            GolemRandomizer.meshes.Add(Assets.Load<Mesh>("@MoistureUpset_noob:assets/robloxcharacters/pickle.mesh"));
            GolemRandomizer.meshes.Add(Assets.Load<Mesh>("@MoistureUpset_noob:assets/robloxcharacters/guest.mesh"));
            GolemRandomizer.meshes.Add(Assets.Load<Mesh>("@MoistureUpset_noob:assets/robloxcharacters/furry.mesh"));
            GolemRandomizer.meshes.Add(Assets.Load<Mesh>("@MoistureUpset_noob:assets/robloxcharacters/naruto.mesh"));
            GolemRandomizer.meshes.Add(Assets.Load<Mesh>("@MoistureUpset_noob:assets/robloxcharacters/fortnite.mesh"));
            GolemRandomizer.meshes.Add(Assets.Load<Mesh>("@MoistureUpset_noob:assets/robloxcharacters/pizza.mesh"));
            GolemRandomizer.meshes.Add(Assets.Load<Mesh>("@MoistureUpset_noob:assets/robloxcharacters/dream.mesh"));
            GolemRandomizer.meshes.Add(Assets.Load<Mesh>("@MoistureUpset_noob:assets/robloxcharacters/soldier.mesh"));
            GolemRandomizer.meshes.Add(Assets.Load<Mesh>("@MoistureUpset_noob:assets/robloxcharacters/police.mesh"));
            GolemRandomizer.meshes.Add(Assets.Load<Mesh>("@MoistureUpset_noob:assets/robloxcharacters/inmate.mesh"));
            GolemRandomizer.meshes.Add(Assets.Load<Mesh>("@MoistureUpset_noob:assets/robloxcharacters/robber.mesh"));
            GolemRandomizer.meshes.Add(Assets.Load<Mesh>("@MoistureUpset_noob:assets/robloxcharacters/man.mesh"));
            GolemRandomizer.meshes.Add(Assets.Load<Mesh>("@MoistureUpset_noob:assets/robloxcharacters/girl.mesh"));
            GolemRandomizer.meshes.Add(Assets.Load<Mesh>("@MoistureUpset_noob:assets/N00b.mesh"));
            GolemRandomizer.meshes.Add(Assets.Load<Mesh>("@MoistureUpset_noob:assets/N00b.mesh"));
            GolemRandomizer.meshes.Add(Assets.Load<Mesh>("@MoistureUpset_noob:assets/robloxcharacters/builderman.mesh"));
            GolemRandomizer.meshes.Add(Assets.Load<Mesh>("@MoistureUpset_noob:assets/robloxcharacters/telamon.mesh"));
            GolemRandomizer.meshes.Add(Assets.Load<Mesh>("@MoistureUpset_noob:assets/robloxcharacters/toothy.mesh"));
        }
        private static void Bison()
        {
            if (!BigJank.getOptionValue(Settings.Thomas))
                return;
            LoadResource("thomas");
            LoadBNK("bison");
            On.EntityStates.Bison.Charge.OnEnter += (orig, self) =>
            {
                EntityStates.Bison.Charge.startSoundString = "BisonCharge";
                orig(self);
            };
            On.EntityStates.Bison.PrepCharge.OnEnter += (orig, self) =>
            {
                EntityStates.Bison.PrepCharge.enterSoundString = "BisonPrep";
                orig(self);
            };
            ReplaceModel("RoR2/Base/Bison/BisonBody.prefab", "@MoistureUpset_thomas:assets/thomas.mesh", "@MoistureUpset_thomas:assets/dankengine.png", 0, true);
        }
        private static void RobloxTitan()
        {
            if (!BigJank.getOptionValue(Settings.RobloxTitan))
                return;
            LoadResource("roblox");
            ReplaceModel("RoR2/Base/Titan/TitanBody.prefab", "@MoistureUpset_roblox:assets/robloxtitan.mesh", "@MoistureUpset_roblox:assets/robloxtitan.png");
            var fab = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Titan/TitanBody.prefab").WaitForCompletion();
            foreach (var item in fab.GetComponentsInChildren<RoR2.ModelSkinController>())
            {
                foreach (var info in item.skins[0].rendererInfos)
                {
                    if (info.defaultMaterial.name != "matTitan" && info.defaultMaterial.name != "Billboard")
                    {
                        info.defaultMaterial.color = new Color(0, 0, 0, 0);
                    }
                }
                for (int i = 0; i < item.skins.Length; i++)
                {
                    if (i != 0)
                    {
                        item.skins[i] = item.skins[0];
                    }
                }
            }
            try
            {
                Texture t = Assets.Load<Texture>("@MoistureUpset_na:assets/solid.png");
                var meshes = fab.GetComponentsInChildren<MeshRenderer>();
                for (int i = 0; i < meshes.Length; i++)
                {
                    if (meshes[i].name.StartsWith("spm") || meshes[i].name.StartsWith("bb"))
                    {
                        meshes[i].gameObject.SetActive(false);
                    }
                }
                var particles = fab.GetComponentsInChildren<ParticleSystem>();
                for (int i = 0; i < particles.Length; i++)
                {
                    if (particles[i].name.StartsWith("EyeGlow"))
                    {
                        particles[i].emissionRate = 0;
                    }
                }
                var light = fab.GetComponentInChildren<UnityEngine.Light>();
                light.intensity = 0;
            }
            catch (Exception e)
            {
                //Debug.Log(e);
            }
            On.EntityStates.TitanMonster.DeathState.OnEnter += (orig, self) =>
            {
                orig(self);
                if (!self.outer.gameObject.name.Contains("Gold"))
                {
                    AkSoundEngine.PostEvent("RobloxDeath", self.outer.gameObject);
                }
            };
            On.EntityStates.TitanMonster.FireFist.PlacePredictedAttack += (orig, self) =>
            {
                orig(self);
                if (!self.outer.gameObject.name.Contains("Gold"))
                {
                    AkSoundEngine.PostEvent("RobloxFist", self.outer.gameObject);
                }
            };
            Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Titan/TitanFistEffect.prefab").WaitForCompletion().AddComponent<Fixers.TitanFixer>();
            On.EntityStates.TitanMonster.FireFist.PlaceSingleDelayBlast += (orig, self, position, delay) =>
            {
                if (!self.outer.gameObject.name.Contains("Gold"))
                {
                    GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Common/GenericDelayBlast.prefab").WaitForCompletion(), position, Quaternion.identity);
                    AkSoundEngine.PostEvent("RobloxFist", gameObject);
                    GameObject.Destroy(gameObject);
                }
                orig(self, position, delay);
            };
            On.EntityStates.TitanMonster.SpawnState.OnEnter += (orig, self) =>
            {
                orig(self);
                if (!self.outer.gameObject.name.Contains("Gold"))
                {
                    AkSoundEngine.PostEvent("RobloxSpawn", self.outer.gameObject);
                }
            };
            On.EntityStates.TitanMonster.FireMegaLaser.OnEnter += (orig, self) =>
            {
                orig(self);
                if (!self.outer.gameObject.name.Contains("Gold"))
                {
                    AkSoundEngine.PostEvent("RobloxLaser", self.outer.gameObject);
                }
            };
            On.EntityStates.TitanMonster.RechargeRocks.OnEnter += (orig, self) =>
            {
                if (!self.outer.gameObject.name.Contains("Gold"))
                {
                    AkSoundEngine.PostEvent("RobloxRocks", self.outer.gameObject);
                    Texture t = Assets.Load<Texture>("@MoistureUpset_roblox:assets/nominecraft.png");
                    var particle = EntityStates.TitanMonster.RechargeRocks.rockControllerPrefab.GetComponentsInChildren<ParticleSystemRenderer>()[1];
                    var system = EntityStates.TitanMonster.RechargeRocks.rockControllerPrefab.GetComponentsInChildren<ParticleSystem>()[1];
                    system.startRotation = 0;
                    system.maxParticles = 1;
                    particle.material = Assets.Load<Material>("@MoistureUpset_moisture_twitch:assets/twitch/twitchemotesmat.mat");
                    try
                    {
                        particle.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                        particle.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                        particle.material.SetInt("_ZWrite", 0);
                        particle.material.DisableKeyword("_ALPHATEST_ON");
                        particle.material.DisableKeyword("_ALPHABLEND_ON");
                        particle.material.EnableKeyword("_ALPHAPREMULTIPLY_ON");
                        particle.material.renderQueue = 3000;
                        particle.material.mainTexture = t;
                    }
                    catch (Exception)
                    {
                    }
                }
                orig(self);
            };
        }
        private static void Alex()
        {
            if (!BigJank.getOptionValue(Settings.AlexJones))
                return;
            LoadResource("alexjones");
            ReplaceModel("RoR2/Base/Titan/TitanGoldBody.prefab", "@MoistureUpset_alexjones:assets/alexjones.mesh", "@MoistureUpset_alexjones:assets/alexjones.png");
            var fab = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Titan/TitanGoldBody.prefab").WaitForCompletion();
            try
            {
                Texture t = Assets.Load<Texture>("@MoistureUpset_na:assets/solid.png");
                var meshes = fab.GetComponentsInChildren<MeshRenderer>();
                for (int i = 0; i < meshes.Length; i++)
                {
                    meshes[i].gameObject.SetActive(false);
                }
                var particles = fab.GetComponentsInChildren<ParticleSystem>();
                for (int i = 0; i < particles.Length; i++)
                {
                    if (particles[i].name.StartsWith("EyeGlow"))
                    {
                        particles[i].emissionRate = 0;
                    }
                }
                var light = fab.GetComponentInChildren<UnityEngine.Light>();
                light.intensity = 0;
            }
            catch (Exception e)
            {
                //Debug.Log(e);
            }
            //On.EntityStates.TitanMonster.FireFist.PlaceSingleDelayBlast += (orig, self, position, delay) =>
            //{
            //    orig(self, position, delay);
            //    if (self.outer.gameObject.name.Contains("Gold"))
            //    {
            //        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Common/GenericDelayBlast.prefab"), position, Quaternion.identity);
            //        AkSoundEngine.PostEvent("AlexFistDelayed", gameObject);
            //        GameObject.Destroy(gameObject);
            //    }
            //};
            Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Titan/TitanGoldFistEffect.prefab").WaitForCompletion().AddComponent<Fixers.GoldTitanFixer>();
            //On.RoR2.EffectManager.SpawnEffect_EffectIndex_EffectData_bool += (orig, index, data, transmit) =>
            //{
            //    ////////////////////406
            //    //DebugClass.Log($"----------{}");'
            //    try
            //    {
            //        if (EffectCatalog.GetEffectDef(index).prefabName == "TitanGoldFistEffect" && !transmit)
            //        {
            //            RoR2.Audio.PointSoundManager.EmitSoundLocal(AkSoundEngine.GetIDFromString("OHSHITWADDUP"), data.origin);
            //        }
            //    }
            //    catch (Exception)
            //    {
            //    }
            //    orig(index, data, transmit);
            //};
            On.EntityStates.TitanMonster.ChargeGoldMegaLaser.FixedUpdate += (orig, self) =>
            {
                orig(self);
                AkSoundEngine.PostEvent("AlexCharge", self.outer.gameObject);
                AkSoundEngine.PostEvent("AlexLaser", self.outer.gameObject);
            };
            On.EntityStates.TitanMonster.DeathState.OnEnter += (orig, self) =>
            {
                orig(self);
                if (self.outer.gameObject.name.Contains("Gold"))
                {
                    AkSoundEngine.PostEvent("AlexDeath", self.outer.gameObject);
                }
            };
            On.EntityStates.TitanMonster.FireGoldFist.PlacePredictedAttack += (orig, self) =>
            {
                var yeet = self.fistEffectPrefab.GetComponentsInChildren<ParticleSystemRenderer>()[0];
                try
                {
                    yeet.mesh = Assets.Load<Mesh>("@MoistureUpset_alexjones:assets/datboi.mesh");
                    yeet.transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
                    yeet.material.mainTexture = Assets.Load<Texture>("@MoistureUpset_alexjones:assets/frogge.png");
                }
                catch (Exception)
                {
                }
                orig(self);
                AkSoundEngine.PostEvent("AlexFist", self.outer.gameObject);
            };
            On.EntityStates.TitanMonster.SpawnState.OnEnter += (orig, self) =>
            {
                orig(self);
                if (self.outer.gameObject.name.Contains("Gold"))
                {
                    //Debug.Log($"----{self.outer.commonComponents.teamComponent.teamIndex}");
                    if (self.outer.commonComponents.teamComponent.teamIndex == TeamIndex.Player)
                    {
                        AkSoundEngine.PostEvent("AlexSpawnAlly", self.outer.gameObject);
                    }
                    else
                    {
                        AkSoundEngine.PostEvent("AlexSpawn", self.outer.gameObject);
                    }
                }
            };
            On.EntityStates.TitanMonster.RechargeRocks.OnEnter += (orig, self) =>
            {
                Texture t = Assets.Load<Texture>("@MoistureUpset_alexjones:assets/datboi.png");
                var particle = EntityStates.TitanMonster.RechargeRocks.rockControllerPrefab.GetComponentsInChildren<ParticleSystemRenderer>()[1];
                var system = EntityStates.TitanMonster.RechargeRocks.rockControllerPrefab.GetComponentsInChildren<ParticleSystem>()[1];
                system.startRotation = 0;
                system.maxParticles = 1;
                particle.material = Assets.Load<Material>("@MoistureUpset_moisture_twitch:assets/twitch/twitchemotesmat.mat");
                try
                {
                    particle.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                    particle.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                    particle.material.SetInt("_ZWrite", 0);
                    particle.material.DisableKeyword("_ALPHATEST_ON");
                    particle.material.DisableKeyword("_ALPHABLEND_ON");
                    particle.material.EnableKeyword("_ALPHAPREMULTIPLY_ON");
                    particle.material.renderQueue = 3000;
                    particle.material.mainTexture = t;
                }
                catch (Exception)
                {
                }
                if (self.outer.gameObject.name.Contains("Gold"))
                {
                    AkSoundEngine.PostEvent("AlexRocks", self.outer.gameObject);
                }
                orig(self);
            };
        }
        private static void LemmeSmash()
        {
            if (!BigJank.getOptionValue(Settings.LemmeSmash))
                return;
            LoadBNK("vulture");
            LoadResource("lemmesmash");
            ReplaceModel("RoR2/Base/Vulture/VultureBody.prefab", "@MoistureUpset_lemmesmash:assets/lemmesmasheyes.mesh", "@MoistureUpset_lemmesmash:assets/lemmesmash.png");
            ReplaceModel("RoR2/Base/Vulture/VultureBody.prefab", "@MoistureUpset_lemmesmash:assets/lemmesmasheyes.mesh", "@MoistureUpset_na:assets/blank.png", 1);
            ReplaceModel("RoR2/Base/Vulture/VultureBody.prefab", "@MoistureUpset_lemmesmash:assets/kevinishomosex/vulturemesh.mesh", "@MoistureUpset_lemmesmash:assets/lemmesmash.png", 2);
            //I know this is shitty but it works and at this point im too scared to change it
            //Also this only happens at startup so who cares amirite?
            var fab = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Vulture/VultureBody.prefab").WaitForCompletion();

            var meshes = fab.GetComponentsInChildren<SkinnedMeshRenderer>();
            foreach (var item in meshes)
            {
                foreach (var name in item.sharedMaterial.GetTexturePropertyNames())
                {
                    if (!item.sharedMesh.name.Contains("eyes") && !item.sharedMesh.name.Contains("Eyeball"))
                    {
                        item.sharedMaterial.SetTexture("_FresnelRamp", null);
                        if (item.sharedMesh.name.Contains("FeatherMesh"))
                        {
                            item.sharedMaterial.mainTexture = Assets.Load<Texture>("@MoistureUpset_lemmesmash:assets/lemmefeather.png");
                        }
                    }
                }
            }
        }
        private static void Hagrid()
        {
            if (!BigJank.getOptionValue(Settings.Hagrid))
                return;
            LoadResource("hagrid");
            LoadBNK("hagrid");
            ReplaceModel("RoR2/Base/Parent/ParentBody.prefab", "@MoistureUpset_hagrid:assets/hagrid.mesh", "@MoistureUpset_hagrid:assets/hagrid.png");
            On.EntityStates.ParentMonster.LoomingPresence.SetPosition += (orig, self, pos) =>
            {
                orig(self, pos);
                AkSoundEngine.PostEvent("HagridTeleport", self.outer.gameObject);
            };
        }
        public static void MakePoolNoodleBlue()
        {
            BlueParticles("RoR2/Base/MagmaWorm/MagmaWormBody.prefab");
            BlueParticles("RoR2/Base/MagmaWorm/MagmaWormBurrow.prefab");
            BlueParticles("RoR2/Base/MagmaWorm/MagmaWormDeath.prefab");
            BlueParticles("RoR2/Base/MagmaWorm/MagmaWormDeathDust.prefab");
            BlueParticles("RoR2/Base/MagmaWorm/MagmaWormImpactExplosion.prefab");
            BlueParticles("RoR2/Junk/MagmaWorm/MagmaWormRupture.prefab");
            BlueParticles("RoR2/Junk/MagmaWorm/MagmaWormWarning.prefab");
        }
        private static void BlueParticles(string path)
        {
            var fab = Addressables.LoadAssetAsync<GameObject>(path).WaitForCompletion();
            foreach (var item in fab.GetComponentsInChildren<ParticleSystem>())
            {
                foreach (var thing in item.gameObject.GetComponentsInChildren<ParticleSystemRenderer>())
                {
                    if (thing.sharedMaterial.name.ToUpper().Contains("WORM") || thing.sharedMaterial.name.ToUpper().Contains("MAGMA"))
                    {
                        thing.sharedMaterial.SetVector("_TintColor", new Vector4(0, .47f, .75f, 1));
                        thing.sharedMaterial.SetVector("_EmissionColor", new Vector4(0, .47f, .75f, 1));
                    }
                    else
                    {
                        thing.material.SetVector("_TintColor", new Vector4(0, .47f, .75f, 1));
                        thing.material.SetVector("_EmissionColor", new Vector4(0, .47f, .75f, 1));
                    }
                }
            }
        }
        private static void Noodle()
        {
            On.RoR2.WormBodyPositions2.OnEnterSurface += (orig, self, point, normal) =>
            {
                orig(self, point, normal);
                if (self.name == "MagmaWormBody(Clone)")
                {

                    SoundAssets.PlaySound("NoodleSplash", self.netId);
                    //NetworkAssistant.playSound("NoodleSplash", self.gameObject.transform.position);
                }
            };
            On.RoR2.WormBodyPositions2.OnExitSurface += (orig, self, point, normal) =>
            {
                orig(self, point, normal);
                if (self.name == "MagmaWormBody(Clone)")
                {
                    SoundAssets.PlaySound("NoodleSplash", self.netId);
                    //NetworkAssistant.playSound("NoodleSplash", self.gameObject.transform.position);
                }
            };
            if (!BigJank.getOptionValue(Settings.PoolNoodle))
                return;
            LoadResource("noodle");


            var fab = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/MagmaWorm/MagmaWormBody.prefab").WaitForCompletion();
            List<Transform> t = new List<Transform>();
            foreach (var item in fab.GetComponentsInChildren<Transform>())
            {
                if (item.name.Contains("Head") && !item.name.Contains("_end") && !item.name.Contains("Center"))
                {
                    t.Add(item);
                }
            }
            foreach (var item in fab.GetComponentsInChildren<Transform>())
            {
                if (item.name.Contains("Jaw") && !item.name.Contains("_end"))
                {
                    t.Add(item);
                }
            }
            foreach (var item in fab.GetComponentsInChildren<Transform>())
            {
                if (item.name.Contains("eye.") && !item.name.Contains("_end"))
                {
                    t.Add(item);
                }
            }
            foreach (var item in fab.GetComponentsInChildren<Transform>())
            {
                if (item.name.Contains("Neck") && !item.name.Contains("_end"))
                {
                    t.Add(item);
                }
            }
            foreach (var item in fab.GetComponentsInChildren<SkinnedMeshRenderer>())
            {
                item.bones = t.ToArray();
            }
            ReplaceModel("RoR2/Base/MagmaWorm/MagmaWormBody.prefab", "@MoistureUpset_noodle:assets/noodle.mesh", "@MoistureUpset_noodle:assets/noodle.png");
            var mesh = fab.GetComponentInChildren<SkinnedMeshRenderer>();
            var blank = Assets.Load<Texture>("@MoistureUpset_na:assets/blank.png");
            for (int i = 0; i < mesh.sharedMaterials.Length; i++)
            {
                mesh.sharedMaterials[i].SetTexture("_FlowHeightRamp", blank);
                mesh.sharedMaterials[i].SetTexture("_FlowHeightmap", blank);
                mesh.sharedMaterials[i].SetTexture("_FlowTex", blank);
            }
            var fab2 = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/ElectricWorm/ElectricWormBody.prefab").WaitForCompletion();
            foreach (var item in fab.GetComponentsInChildren<UnityEngine.Rendering.PostProcessing.PostProcessVolume>())
            {
                var ting = fab2.GetComponentInChildren<UnityEngine.Rendering.PostProcessing.PostProcessVolume>();
                ((UnityEngine.Rendering.PostProcessing.PostProcessProfile)item.sharedProfile).settings = ((UnityEngine.Rendering.PostProcessing.PostProcessProfile)ting.sharedProfile).settings;
            }
            MakePoolNoodleBlue();
        }
        private static void Skeleton()
        {
            if (!BigJank.getOptionValue(Settings.CrabRave))
                return;
            LoadBNK("jockey");
            LoadResource("skeleton");
            //ReplaceTexture("RoR2/Base/HermitCrab/HermitCrabBody.prefab", "@MoistureUpset_skeleton:assets/skeleton.png");
            ReplaceModel("RoR2/Base/HermitCrab/HermitCrabBody.prefab", "@MoistureUpset_skeleton:assets/skeleton.mesh", "@MoistureUpset_skeleton:assets/skeleton.png");
            var fab = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/HermitCrab/HermitCrabBody.prefab").WaitForCompletion();
            var blank = Assets.Load<Texture>("@MoistureUpset_na:assets/blank.png");
            foreach (var item in fab.GetComponentsInChildren<SkinnedMeshRenderer>())
            {
                foreach (var name in item.sharedMaterial.GetTexturePropertyNames())
                {
                    if (item.sharedMaterial.GetTexture(name) && (item.sharedMaterial.GetTexture(name).name == "texBlackbeachDirt" || item.sharedMaterial.GetTexture(name).name == "texGPLichenTerrain"))
                    {
                        item.sharedMaterial.SetTexture(name, blank);
                    }
                }
            }
            ReplaceMeshFilter("RoR2/Base/HermitCrab/HermitCrabBombGhost.prefab", "@MoistureUpset_skeleton:assets/arrow.mesh", "@MoistureUpset_skeleton:assets/arrow.png");
            fab = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/HermitCrab/HermitCrabBombGhost.prefab").WaitForCompletion();
            fab.AddComponent<ArrowFixer>();
            fab.GetComponentInChildren<Rewired.ComponentControls.Effects.RotateAroundAxis>().speed = Rewired.ComponentControls.Effects.RotateAroundAxis.Speed.Stopped;
            fab.GetComponentInChildren<ParticleSystemRenderer>().enabled = false;
        }
        private static void CrabRave()
        {
            if (!BigJank.getOptionValue(Settings.CrabRave))
                return;
            LoadResource("crabrave");
            LoadBNK("crabrave");
            ReplaceModel("RoR2/Base/Nullifier/NullifierBody.prefab", "@MoistureUpset_crabrave:assets/crab.mesh", "@MoistureUpset_crabrave:assets/crab.png", 1);
            ReplaceModel("RoR2/Base/Nullifier/NullifierBody.prefab", "@MoistureUpset_na:assets/na.mesh", "@MoistureUpset_na:assets/blank.png");
        }
        private static void PUDDI()
        {
            if (!BigJank.getOptionValue(Settings.GigaPuddi))
                return;
            LoadResource("puddi");
            LoadBNK("puddi");
            ReplaceModel("RoR2/Base/ClayBoss/ClayBossBody.prefab", "@MoistureUpset_puddi:assets/puddi.mesh", "@MoistureUpset_puddi:assets/puddi.png");
            var fab = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/ClayBoss/ClayBossBody.prefab").WaitForCompletion();
            var mesh = fab.GetComponentInChildren<SkinnedMeshRenderer>();
            var blank = Assets.Load<Texture>("@MoistureUpset_na:assets/blank.png");
            for (int i = 0; i < mesh.sharedMaterials.Length; i++)
            {
                mesh.sharedMaterials[i].SetTexture("_PrintRamp", blank);
                mesh.sharedMaterials[i].SetTexture("_GreenChannelNormalTex", blank);
                mesh.sharedMaterials[i].SetTexture("_GreenChannelTex", blank);
                mesh.sharedMaterials[i].SetTexture("_SliceAlphaTex", blank);
            }
            ReplaceMeshFilter("RoR2/Base/ClayBoss/ClayPotProjectileGhost.prefab", "@MoistureUpset_puddi:assets/puddighost.mesh", "@MoistureUpset_puddi:assets/puddi.png");
            Vector4 color = new Vector4(87f / 2500f, 40f / 2500f, 17f / 2500f, 1);
            foreach (var item in fab.GetComponentsInChildren<LineRenderer>())
            {
                item.sharedMaterial.SetVector("_TintColor", color);
                item.sharedMaterial.SetVector("_EmissionColor", color);
            }
            fab = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/ClayBoss/TarballGhost.prefab").WaitForCompletion();
            var mesh2 = fab.GetComponentsInChildren<MeshRenderer>();
            foreach (var item in mesh2)
            {
                item.sharedMaterial = Assets.Load<Material>("@MoistureUpset_puddi:assets/puddi.mat");
            }
            fab.GetComponentsInChildren<ParticleSystemRenderer>()[1].gameObject.SetActive(false);
            fab.GetComponentsInChildren<TrailRenderer>()[0].sharedMaterial.SetVector("_EmissionColor", color);
            ReplaceMeshFilter("RoR2/Base/ClayBoss/TarballGhost.prefab", "@MoistureUpset_puddi:assets/puddighost.mesh", "@MoistureUpset_puddi:assets/puddi.png");
            fab = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/ExplosivePotDestructible/ExplosivePotDestructibleBody.prefab").WaitForCompletion();
            ReplaceMeshFilter("RoR2/Base/ExplosivePotDestructible/ExplosivePotDestructibleBody.prefab", "@MoistureUpset_puddi:assets/puddicontainer.mesh", "@MoistureUpset_puddi:assets/puddicontainer.png");

            fab = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/ClayBoss/ClayBossDeath.prefab").WaitForCompletion();
            foreach (var item in fab.GetComponentsInChildren<MeshFilter>())
            {
                item.sharedMesh = Assets.Load<Mesh>("@MoistureUpset_puddi:assets/puddighost.mesh");
                //item.transform.localScale *= 3;
            }
            foreach (var item in fab.GetComponentsInChildren<MeshRenderer>())
            {
                item.sharedMaterial.mainTexture = Assets.Load<Texture>("@MoistureUpset_puddi:assets/puddi.png");
            }
            fab = Addressables.LoadAssetAsync<GameObject>("RoR2/Junk/Pot2/Pot2Debris.prefab").WaitForCompletion();
            foreach (var item in fab.GetComponentsInChildren<MeshFilter>())
            {
                item.transform.localScale *= .5f;
                item.sharedMesh = Assets.Load<Mesh>("@MoistureUpset_puddi:assets/puddighost.mesh");
            }
        }
        private static void StringWorm()
        {
            if (!BigJank.getOptionValue(Settings.Squirmles))
                return;
            LoadResource("werm");
            LoadBNK("werm");
            var fab = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/ElectricWorm/ElectricWormBody.prefab").WaitForCompletion();
            List<Transform> t = new List<Transform>();
            foreach (var item in fab.GetComponentsInChildren<Transform>())
            {
                if (item.name.Contains("Head") && !item.name.Contains("_end") && !item.name.Contains("Center"))
                {
                    t.Add(item);
                }
            }
            foreach (var item in fab.GetComponentsInChildren<Transform>())
            {
                if (item.name.Contains("Jaw") && !item.name.Contains("_end"))
                {
                    t.Add(item);
                }
            }
            foreach (var item in fab.GetComponentsInChildren<Transform>())
            {
                if (item.name.Contains("eye.") && !item.name.Contains("_end"))
                {
                    t.Add(item);
                }
            }
            foreach (var item in fab.GetComponentsInChildren<Transform>())
            {
                if (item.name.Contains("Neck") && !item.name.Contains("_end"))
                {
                    t.Add(item);
                }
            }
            foreach (var item in fab.GetComponentsInChildren<SkinnedMeshRenderer>())
            {
                item.bones = t.ToArray();
            }
            ReplaceModel("RoR2/Base/ElectricWorm/ElectricWormBody.prefab", "@MoistureUpset_werm:assets/werm.mesh", "@MoistureUpset_werm:assets/werm.png");
            var mesh = fab.GetComponentInChildren<SkinnedMeshRenderer>();
            var blank = Assets.Load<Texture>("@MoistureUpset_na:assets/blank.png");
            for (int i = 0; i < mesh.sharedMaterials.Length; i++)
            {
                mesh.sharedMaterials[i].SetTexture("_FlowHeightRamp", blank);
                mesh.sharedMaterials[i].SetTexture("_FlowHeightmap", blank);
                mesh.sharedMaterials[i].SetTexture("_FlowTex", blank);
            }



            fab = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/ElectricWorm/ElectricWormSeekerProjectile.prefab").WaitForCompletion();
            var sb = fab.AddComponent<SeekingBullet>();
        }
        private static void Discord()
        {
            if (!BigJank.getOptionValue(Settings.WanderingAtEveryone))
                return;
            LoadResource("discord");
            LoadBNK("discord");
            ReplaceModel("RoR2/Base/Vagrant/VagrantBody.prefab", "@MoistureUpset_discord:assets/discord.mesh", "@MoistureUpset_discord:assets/discord.png");
            ReplaceModel("RoR2/Base/Vagrant/VagrantDeathExplosion.prefab", "@MoistureUpset_discord:assets/limb1.mesh");
            ReplaceModel("RoR2/Base/Vagrant/VagrantDeathExplosion.prefab", "@MoistureUpset_discord:assets/limb2.mesh", 1);
            ReplaceModel("RoR2/Base/Vagrant/VagrantDeathExplosion.prefab", "@MoistureUpset_discord:assets/limb3.mesh", 2);
            var fab = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Vagrant/VagrantBody.prefab").WaitForCompletion();
            var mesh = fab.GetComponentInChildren<SkinnedMeshRenderer>();
            Shader s = mesh.sharedMaterial.shader;
            mesh.sharedMaterial = Assets.Load<Material>("@MoistureUpset_discord:assets/discord.mat");
            mesh.sharedMaterial.shader = s;
            foreach (var item in fab.GetComponentsInChildren<RoR2.CharacterModel>())
            {
                item.baseLightInfos[0].defaultColor = new Color(0, 0, 0, 0);
                item.baseLightInfos[0].light.color = new Color(0, 0, 0, 0);
            }
            On.EntityStates.VagrantMonster.FireMegaNova.Detonate += (orig, self) =>
            {
                EntityStates.VagrantMonster.FireMegaNova.novaSoundString = "DiscordExplosion";
                orig(self);
            };


            //Play_vagrant_R_explode
            /*
                         ((AK.Wwise.BaseType)fab.GetComponentsInChildren<AkEvent>()[1].data).ObjectReference.SetFieldValue("objectName", "nyan");
            ((AK.Wwise.BaseType)fab.GetComponentsInChildren<AkEvent>()[1].data).ObjectReference.SetFieldValue("id", 1002825203);
             */

            fab = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Vagrant/VagrantCannonGhost.prefab").WaitForCompletion();
            var filter = fab.GetComponentsInChildren<MeshFilter>()[0];
            var renderer = fab.GetComponentsInChildren<MeshRenderer>()[0];
            filter.sharedMesh = Assets.Load<Mesh>("@MoistureUpset_discord:assets/discordprojectile.mesh");
            renderer.sharedMaterial = Assets.Load<Material>("@MoistureUpset_discord:assets/discordprojectile.mat");

            fab = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Vagrant/VagrantTrackingBombGhost.prefab").WaitForCompletion();
            filter = fab.GetComponentsInChildren<MeshFilter>()[0];
            filter.sharedMesh = Assets.Load<Mesh>("@MoistureUpset_na:assets/na1.mesh");
            filter = fab.GetComponentsInChildren<MeshFilter>()[1];
            filter.sharedMesh = Assets.Load<Mesh>("@MoistureUpset_na:assets/na1.mesh");

            fab.GetComponentsInChildren<ParticleSystemRenderer>()[1].gameObject.SetActive(false);
            var particle = fab.GetComponentsInChildren<ParticleSystemRenderer>()[0];
            particle.material = Assets.Load<Material>("@MoistureUpset_moisture_twitch:assets/twitch/twitchemotesmat.mat");
            try
            {
                particle.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                particle.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                particle.material.SetInt("_ZWrite", 0);
                particle.material.DisableKeyword("_ALPHATEST_ON");
                particle.material.DisableKeyword("_ALPHABLEND_ON");
                particle.material.EnableKeyword("_ALPHAPREMULTIPLY_ON");
                particle.material.renderQueue = 3000;
                particle.material.mainTexture = Assets.Load<Texture>("@MoistureUpset_discord:assets/ping.png");
            }
            catch (Exception)
            {
            }
        }
        private static void Copter()
        {
            if (!BigJank.getOptionValue(Settings.Roflcopter))
                return;
            var fab = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/LunarWisp/LunarWispBody.prefab").WaitForCompletion();
            fab.AddComponent<Helicopter>();
            fab.GetComponentsInChildren<ParticleSystem>()[0].maxParticles = 0;
            fab.GetComponentsInChildren<ParticleSystem>()[1].maxParticles = 0;
            fab.GetComponentsInChildren<ParticleSystem>()[2].maxParticles = 0;
            fab.GetComponentsInChildren<ParticleSystem>()[3].maxParticles = 0;
            LoadResource("roflcopter");
            LoadBNK("Roflcopter");
            ReplaceModel("RoR2/Base/LunarWisp/LunarWispBody.prefab", "@MoistureUpset_roflcopter:assets/roflcopter.mesh", "@MoistureUpset_roflcopter:assets/roflcopter.png");
        }
        private static void Rob()
        {
            if (!BigJank.getOptionValue(Settings.Rob))
                return;
            LoadResource("rob");
            ReplaceModel("RoR2/Base/LunarGolem/LunarGolemBody.prefab", "@MoistureUpset_rob:assets/rob.mesh", "@MoistureUpset_rob:assets/rob.png");
            LoadBNK("rob");
        }
        private static void Nyan()
        {
            if (!BigJank.getOptionValue(Settings.NyanCat))
                return;
            LoadResource("beetlequeen");
            LoadBNK("Nyam2");
            ReplaceModel("RoR2/Base/Beetle/BeetleQueen2Body.prefab", "@MoistureUpset_beetlequeen:assets/bosses/nyancat.mesh", "@MoistureUpset_na:assets/blank.png");
            var fab = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Beetle/BeetleQueen2Body.prefab").WaitForCompletion();
            foreach (var item in fab.GetComponentsInChildren<Light>())
            {
                item.gameObject.SetActive(false);
            }
            fab.GetComponentsInChildren<SkinnedMeshRenderer>()[0].sharedMaterial.SetTexture("_EmTex", Assets.Load<Texture>("@MoistureUpset_beetlequeen:assets/bosses/nyancat.png"));
            fab.GetComponentsInChildren<SkinnedMeshRenderer>()[0].sharedMaterial.SetVector("_EmColor", new Vector4(.4f, .4f, .4f, 1));

            fab = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Beetle/BeetleQueenSpitGhost.prefab").WaitForCompletion();
            fab.GetComponentsInChildren<TrailRenderer>()[1].gameObject.SetActive(false);
            fab.GetComponentsInChildren<TrailRenderer>()[0].material = Assets.Load<Material>("@MoistureUpset_beetlequeen:assets/bosses/trail.mat");
            var p = fab.GetComponentInChildren<ParticleSystem>();
            p.startSpeed = 0;
            p.simulationSpace = ParticleSystemSimulationSpace.Local;
            p.gravityModifier = 0;
            p.maxParticles = 1;
            p.startLifetime = 10;
            var shape = p.shape;
            shape.shapeType = ParticleSystemShapeType.Sprite;
            //fab = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Beetle/BeetleSpitExplosion.prefab").WaitForCompletion();
            fab.AddComponent<NyanFixer>();

            //((AK.Wwise.BaseType)fab.GetComponentsInChildren<AkEvent>()[1].data).ObjectReference.SetFieldValue("objectName", "nyan");
            //((AK.Wwise.BaseType)fab.GetComponentsInChildren<AkEvent>()[1].data).ObjectReference.SetFieldValue("id", (UInt32)1002825203);


            var vel = p.limitVelocityOverLifetime;
            vel.enabled = true;
            vel.limitMultiplier = 0;
            var succ = p.rotationBySpeed;
            succ.zMultiplier = 1;
            var part = fab.GetComponentInChildren<ParticleSystemRenderer>();
            try
            {
                part.material.shader = Shader.Find("Sprites/Default");
                part.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                part.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                part.material.SetInt("_ZWrite", 0);
                part.material.DisableKeyword("_ALPHATEST_ON");
                part.material.DisableKeyword("_ALPHABLEND_ON");
                part.material.EnableKeyword("_ALPHAPREMULTIPLY_ON");
                part.material.renderQueue = 3000;
                part.material.mainTexture = Assets.Load<Texture>("@MoistureUpset_beetlequeen:assets/bosses/nyanjpeg.png");
            }
            catch (Exception)
            {
            }
            //foreach (var particle in EntityStates.BeetleQueenMonster.FireSpit.effectPrefab.GetComponentsInChildren<ParticleSystemRenderer>())
            //{
            //    try
            //    {
            //        particle.material.shader = Shader.Find("Sprites/Default");
            //        particle.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
            //        particle.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            //        particle.material.SetInt("_ZWrite", 0);
            //        particle.material.DisableKeyword("_ALPHATEST_ON");
            //        particle.material.DisableKeyword("_ALPHABLEND_ON");
            //        particle.material.EnableKeyword("_ALPHAPREMULTIPLY_ON");
            //        particle.material.renderQueue = 3000;
            //        particle.material.mainTexture = Assets.Load<Texture>("@MoistureUpset_beetlequeen:assets/bosses/nyanstars.png");
            //    }
            //    catch (Exception)
            //    {
            //    }
            //}


            fab = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Beetle/BeetleWardOrbEffect.prefab").WaitForCompletion();
            fab.GetComponentsInChildren<SkinnedMeshRenderer>()[0].sharedMesh = Assets.Load<Mesh>("@MoistureUpset_beetlequeen:assets/bosses/Poptart.mesh");
            fab.GetComponentsInChildren<SkinnedMeshRenderer>()[0].material = Assets.Load<Material>("@MoistureUpset_beetlequeen:assets/bosses/nyancat.mat");
            fab.GetComponentsInChildren<TrailRenderer>()[0].material = Assets.Load<Material>("@MoistureUpset_beetlequeen:assets/bosses/trail.mat");
        }
        private static void Thanos()
        {
            if (!BigJank.getOptionValue(Settings.Thanos))
                return;
            LoadResource("thanos");
            LoadBNK("Thanos");
            ReplaceModel("RoR2/Base/Brother/BrotherBody.prefab", "@MoistureUpset_thanos:assets/bosses/thanos.mesh", "@MoistureUpset_thanos:assets/bosses/thanos.png", 0);
            ReplaceModel("RoR2/Base/Brother/BrotherBody.prefab", "@MoistureUpset_thanos:assets/bosses/infinityhammer.mesh", "@MoistureUpset_thanos:assets/bosses/thanos.png", 2);
            ReplaceMeshFilter("RoR2/Base/Brother/BrotherBody.prefab", "@MoistureUpset_na:assets/na1.mesh", 0);
            ReplaceMeshFilter("RoR2/Base/Brother/BrotherBody.prefab", "@MoistureUpset_na:assets/na1.mesh", 1);
            ReplaceMeshFilter("RoR2/Base/Brother/BrotherBody.prefab", "@MoistureUpset_na:assets/na1.mesh", 2);
            ReplaceModel("RoR2/Base/Brother/BrotherBody.prefab", "@MoistureUpset_na:assets/na1.mesh", 1);
            ReplaceModel("RoR2/Base/Brother/BrotherBody.prefab", "@MoistureUpset_na:assets/na1.mesh", 3);
            ReplaceModel("RoR2/Base/Brother/BrotherBody.prefab", "@MoistureUpset_na:assets/na1.mesh", 4);

            ReplaceModel("RoR2/Junk/BrotherGlass/BrotherGlassBody.prefab", "@MoistureUpset_thanos:assets/bosses/thanos.mesh", "@MoistureUpset_thanos:assets/bosses/thanos.png", 0);
            ReplaceModel("RoR2/Junk/BrotherGlass/BrotherGlassBody.prefab", "@MoistureUpset_thanos:assets/bosses/infinityhammer.mesh", "@MoistureUpset_thanos:assets/bosses/thanos.png", 2);
            ReplaceMeshFilter("RoR2/Junk/BrotherGlass/BrotherGlassBody.prefab", "@MoistureUpset_na:assets/na1.mesh", 0);
            ReplaceModel("RoR2/Junk/BrotherGlass/BrotherGlassBody.prefab", "@MoistureUpset_na:assets/na1.mesh", 1);
            ReplaceModel("RoR2/Junk/BrotherGlass/BrotherGlassBody.prefab", "@MoistureUpset_na:assets/na1.mesh", 3);


            ReplaceModel("RoR2/Base/Brother/BrotherHurtBody.prefab", "@MoistureUpset_thanos:assets/bosses/thanos.mesh", "@MoistureUpset_thanos:assets/bosses/thanos.png", 0);
            ReplaceMeshFilter("RoR2/Base/Brother/BrotherHurtBody.prefab", "@MoistureUpset_na:assets/na1.mesh", 0);
            ReplaceMeshFilter("RoR2/Base/Brother/BrotherHurtBody.prefab", "@MoistureUpset_na:assets/na1.mesh", 1);
            ReplaceMeshFilter("RoR2/Base/Brother/BrotherHurtBody.prefab", "@MoistureUpset_na:assets/na1.mesh", 2);
            ReplaceModel("RoR2/Base/Brother/BrotherHurtBody.prefab", "@MoistureUpset_na:assets/na1.mesh", 1);
        }
        private static void Cereal()
        {
            if (!BigJank.getOptionValue(Settings.Cereal))
                return;
            LoadResource("artifact");
            ReplaceMeshFilter("RoR2/Base/ArtifactShell/ArtifactShellBody.prefab", "@MoistureUpset_artifact:assets/bosses/bowl.mesh");
            var fab = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/ArtifactShell/ArtifactShellBody.prefab").WaitForCompletion();
            var mesh = fab.GetComponentsInChildren<MeshRenderer>();
            foreach (var item in mesh)
            {
                item.sharedMaterial = Assets.Load<Material>("@MoistureUpset_artifact:assets/bosses/bowl.mat");
            }
            fab = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/ArtifactShell/ArtifactShellSeekingSolarFlare.prefab").WaitForCompletion();
            var skinnedmesh = fab.AddComponent<SkinnedMeshRenderer>() as SkinnedMeshRenderer;
            skinnedmesh.sharedMesh = Assets.Load<Mesh>("@MoistureUpset_artifact:assets/bosses/box.mesh");
            skinnedmesh.sharedMaterial = Assets.Load<Material>("@MoistureUpset_artifact:assets/bosses/box.mat");
            On.EntityStates.ArtifactShell.WaitForKey.OnEnter += (orig, self) =>
            {
                orig(self);
                foreach (var item in GameObject.FindObjectsOfType<GameObject>())
                {
                    if (item.name.StartsWith("Ring,"))
                    {
                        item.SetActive(false);
                    }
                }
            };
            On.RoR2.ArtifactTrialMissionController.CombatState.OnEnter += (orig, self) =>
            {
                var mainBody = NetworkUser.readOnlyLocalPlayersList[0].master?.GetBody();
                AkSoundEngine.ExecuteActionOnEvent(3772119855, AkActionOnEventType.AkActionOnEventType_Stop);
                AkSoundEngine.PostEvent("ArtifactLoop", mainBody.gameObject);
                orig(self);
            };
            On.RoR2.ArtifactTrialMissionController.CombatState.OnExit += (orig, self) =>
            {
                try
                {
                    //var c = GameObject.FindObjectOfType<MusicController>();
                    var mainBody = NetworkUser.readOnlyLocalPlayersList[0].master?.GetBody();
                    AkSoundEngine.ExecuteActionOnEvent(1462303513, AkActionOnEventType.AkActionOnEventType_Stop);
                    AkSoundEngine.SetRTPCValue("BossMusicActive", 0);
                    AkSoundEngine.PostEvent("StopFanFare", Moisture_Upset.musicController.gameObject);
                    AkSoundEngine.SetRTPCValue("BossDead", 1f);
                    AkSoundEngine.PostEvent("PlayFanFare", Moisture_Upset.musicController.gameObject);
                }
                catch (Exception)
                {
                }
                orig(self);
            };
        }
        private static void Twitch()
        {
            LoadResource("moisture_twitch");
            if (!BigJank.getOptionValue(Settings.Twitch))
                return;
            LoadResource("twitch2");
            LoadBNK("Twitch");
            ReplaceModel("RoR2/Base/Gravekeeper/GravekeeperBody.prefab", "@MoistureUpset_na:assets/na1.mesh", 1);
            ReplaceModel("RoR2/Base/Gravekeeper/GravekeeperBody.prefab", "@MoistureUpset_moisture_twitch:assets/twitch.mesh", "@MoistureUpset_moisture_twitch:assets/twitch.png", 2);
            ReplaceModel("RoR2/Base/Gravekeeper/GravekeeperBody.prefab", "@MoistureUpset_na:assets/na1.mesh", 3);
            ReplaceModel("RoR2/Base/Gravekeeper/GravekeeperBody.prefab", "@MoistureUpset_na:assets/na1.mesh", 0);
            ReplaceMeshFilter("RoR2/Base/Gravekeeper/GravekeeperHookGhost.prefab", "@MoistureUpset_moisture_twitch:assets/Bosses/GAMER.mesh", "@MoistureUpset_moisture_twitch:assets/twitch.png");

            var fab = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Gravekeeper/GravekeeperHookGhost.prefab").WaitForCompletion();
            fab.GetComponentInChildren<TrailRenderer>().material = Assets.Load<Material>("@MoistureUpset_moisture_twitch:assets/Bosses/Matt.mat");

            fab = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Gravekeeper/GravekeeperTrackingFireballGhost.prefab").WaitForCompletion();
            fab.GetComponentInChildren<MeshRenderer>().gameObject.SetActive(false);
            fab.AddComponent<Collabs.RandomTwitch>();
            var particle = fab.GetComponentsInChildren<ParticleSystemRenderer>()[0];
            particle.material = Assets.Load<Material>("@MoistureUpset_moisture_twitch:assets/twitch/twitchemotesmat.mat");
            try
            {
                particle.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                particle.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                particle.material.SetInt("_ZWrite", 0);
                particle.material.DisableKeyword("_ALPHATEST_ON");
                particle.material.DisableKeyword("_ALPHABLEND_ON");
                particle.material.EnableKeyword("_ALPHAPREMULTIPLY_ON");
                particle.material.renderQueue = 3000;
            }
            catch (Exception)
            {
            }
            var p = fab.GetComponentsInChildren<ParticleSystem>()[0];
            p.startSpeed = 0;
            p.simulationSpace = ParticleSystemSimulationSpace.Local;
            p.gravityModifier = 0;
            p.maxParticles = 1;
            p.startLifetime = 100;
            var shape = p.shape;
            shape.shapeType = ParticleSystemShapeType.Sprite;

            var vel = p.limitVelocityOverLifetime;
            vel.enabled = true;
            vel.limitMultiplier = 0;
            var succ = p.rotationBySpeed;
            succ.zMultiplier = 1;


            On.EntityStates.GravekeeperMonster.Weapon.GravekeeperBarrage.OnEnter += (orig, self) =>
            {
                Util.PlaySound("TwitchMemes", self.outer.gameObject);
                orig(self);
            };
            On.EntityStates.GravekeeperBoss.FireHook.OnEnter += (orig, self) =>
            {
                Util.PlaySound("TwitchChains", self.outer.gameObject);
                orig(self);
            };
        }
        private static void ImposterChanger(string name)
        {
            var fab = Addressables.LoadAssetAsync<GameObject>($"RoR2/Base/ScavLunar/{name}.prefab").WaitForCompletion();
            Shader temp = fab.GetComponentsInChildren<SkinnedMeshRenderer>()[0].sharedMaterial.shader;
            fab.GetComponentsInChildren<SkinnedMeshRenderer>()[0].sharedMaterial = Assets.Load<Material>("@MoistureUpset_scavenger:assets/bosses/imposter.mat");
            fab.GetComponentsInChildren<SkinnedMeshRenderer>()[2].sharedMaterial = Assets.Load<Material>("@MoistureUpset_scavenger:assets/bosses/imposter.mat");
            fab.GetComponentsInChildren<SkinnedMeshRenderer>()[0].sharedMaterial.shader = temp;
            fab.GetComponentsInChildren<SkinnedMeshRenderer>()[2].sharedMaterial.shader = temp;
        }
        public static bool kindlyKillYourselfRune = true;
        private static void Imposter()
        {
            if (!BigJank.getOptionValue(Settings.Imposter))
                return;
            LoadResource("scavenger");
            LoadBNK("Abungus");
            ReplaceModel("RoR2/Base/Scav/ScavBody.prefab", "@MoistureUpset_scavenger:assets/bosses/Backpack.mesh", "@MoistureUpset_scavenger:assets/bosses/Amongus.png", 0);
            ReplaceModel("RoR2/Base/Scav/ScavBackpack.prefab", "@MoistureUpset_scavenger:assets/bosses/Backpackonly.mesh");
            ReplaceModel("RoR2/Base/Scav/ScavBody.prefab", "@MoistureUpset_na:assets/na1.mesh", 1);
            ReplaceModel("RoR2/Base/Scav/ScavBody.prefab", "@MoistureUpset_scavenger:assets/bosses/Body.mesh", "@MoistureUpset_scavenger:assets/bosses/Amongus.png", 2);
            ReplaceMeshFilter("RoR2/Base/Scav/ScavBody.prefab", "@MoistureUpset_scavenger:assets/bosses/gun.mesh");
            var fab = Addressables.LoadAssetAsync<GameObject>($"RoR2/Base/Scav/ScavBody.prefab").WaitForCompletion();
            Shader temp = fab.GetComponentsInChildren<SkinnedMeshRenderer>()[0].sharedMaterial.shader;
            fab.GetComponentsInChildren<SkinnedMeshRenderer>()[0].sharedMaterial = Assets.Load<Material>("@MoistureUpset_scavenger:assets/bosses/imposter.mat");
            fab.GetComponentsInChildren<SkinnedMeshRenderer>()[2].sharedMaterial = Assets.Load<Material>("@MoistureUpset_scavenger:assets/bosses/imposter.mat");
            fab.GetComponentsInChildren<SkinnedMeshRenderer>()[0].sharedMaterial.shader = temp;
            fab.GetComponentsInChildren<SkinnedMeshRenderer>()[2].sharedMaterial.shader = temp;

            fab = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Scav/ScavBody.prefab").WaitForCompletion();
            fab.AddComponent<AbungusColors>();
            On.EntityStates.ScavMonster.Death.OnEnter += (orig, self) =>
            {
                if (kindlyKillYourselfRune)
                {
                    kindlyKillYourselfRune = false;
                    try
                    {
                        var backpack = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Scav/ScavBackpack.prefab").WaitForCompletion();
                        GameObject g = self.outer.gameObject;
                        backpack.GetComponentInChildren<SkinnedMeshRenderer>().sharedMaterial = g.GetComponentInChildren<AbungusColors>().material;
                    }
                    catch (Exception)
                    {
                    }
                }
                orig(self);
            };

            ReplaceModel("RoR2/Base/ScavLunar/ScavLunar1Body.prefab", "@MoistureUpset_scavenger:assets/bosses/Backpack.mesh", "@MoistureUpset_scavenger:assets/bosses/AmongusWhite.png", 0);
            ReplaceModel("RoR2/Base/ScavLunar/ScavLunar1Body.prefab", "@MoistureUpset_na:assets/na1.mesh", 1);
            ReplaceModel("RoR2/Base/ScavLunar/ScavLunar1Body.prefab", "@MoistureUpset_scavenger:assets/bosses/Body.mesh", "@MoistureUpset_scavenger:assets/bosses/AmongusWhite.png", 2);
            ReplaceMeshFilter("RoR2/Base/ScavLunar/ScavLunar1Body.prefab", "@MoistureUpset_scavenger:assets/bosses/gun.mesh", 4);
            ImposterChanger("ScavLunar1Body");

            ReplaceModel("RoR2/Base/ScavLunar/ScavLunar2Body.prefab", "@MoistureUpset_scavenger:assets/bosses/Backpack.mesh", "@MoistureUpset_scavenger:assets/bosses/AmongusWhite.png", 0);
            ReplaceModel("RoR2/Base/ScavLunar/ScavLunar2Body.prefab", "@MoistureUpset_na:assets/na1.mesh", 1);
            ReplaceModel("RoR2/Base/ScavLunar/ScavLunar2Body.prefab", "@MoistureUpset_scavenger:assets/bosses/Body.mesh", "@MoistureUpset_scavenger:assets/bosses/AmongusWhite.png", 2);
            ReplaceMeshFilter("RoR2/Base/ScavLunar/ScavLunar2Body.prefab", "@MoistureUpset_scavenger:assets/bosses/gun.mesh", 4);
            ImposterChanger("ScavLunar2Body");

            ReplaceModel("RoR2/Base/ScavLunar/ScavLunar3Body.prefab", "@MoistureUpset_scavenger:assets/bosses/Backpack.mesh", "@MoistureUpset_scavenger:assets/bosses/AmongusWhite.png", 0);
            ReplaceModel("RoR2/Base/ScavLunar/ScavLunar3Body.prefab", "@MoistureUpset_na:assets/na1.mesh", 1);
            ReplaceModel("RoR2/Base/ScavLunar/ScavLunar3Body.prefab", "@MoistureUpset_scavenger:assets/bosses/Body.mesh", "@MoistureUpset_scavenger:assets/bosses/AmongusWhite.png", 2);
            ReplaceMeshFilter("RoR2/Base/ScavLunar/ScavLunar3Body.prefab", "@MoistureUpset_scavenger:assets/bosses/gun.mesh", 4);
            ImposterChanger("ScavLunar3Body");

            ReplaceModel("RoR2/Base/ScavLunar/ScavLunar4Body.prefab", "@MoistureUpset_scavenger:assets/bosses/Backpack.mesh", "@MoistureUpset_scavenger:assets/bosses/AmongusWhite.png", 0);
            ReplaceModel("RoR2/Base/ScavLunar/ScavLunar4Body.prefab", "@MoistureUpset_na:assets/na1.mesh", 1);
            ReplaceModel("RoR2/Base/ScavLunar/ScavLunar4Body.prefab", "@MoistureUpset_scavenger:assets/bosses/Body.mesh", "@MoistureUpset_scavenger:assets/bosses/AmongusWhite.png", 2);
            ReplaceMeshFilter("RoR2/Base/ScavLunar/ScavLunar4Body.prefab", "@MoistureUpset_scavenger:assets/bosses/gun.mesh", 4);
            ImposterChanger("ScavLunar4Body");

            ReplaceModel("RoR2/Base/ScavLunar/ScavLunarBackpack.prefab", "@MoistureUpset_scavenger:assets/bosses/Backpackonly.mesh", "@MoistureUpset_scavenger:assets/bosses/AmongusWhite.png");
        }

        private static void Collab()
        {
            Direseeker();
            PlayableLemurian();
            PlayableGrovetender();
            PlayableScavenger();
            PlayableTemplar();
            PlayableMithrix();
            PlayableLemurian();
            ChipTheBeetle();
        }
        private static void Direseeker()
        {
            try
            {
                if (BigJank.getOptionValue(Settings.DireSeeker))
                {
                    //Collabs.Direseeker.Run();
                    //DebugClass.Log($"Direseeker installed, modifying");
                }
            }
            catch (Exception)
            {
                DebugClass.Log($"Direseeker not installed, skipping");
            }
        }
        private static void PlayableLemurian()
        {
            try
            {
                if (BigJank.getOptionValue(Settings.MikeWazowski))
                {
                    //Collabs.PlayableLemurian.Run();
                    //DebugClass.Log($"Playable Lemurian installed, modifying");
                }
            }
            catch (Exception)
            {
                DebugClass.Log($"Playable Lemurian not installed, skipping");
            }
        }
        private static void PlayableGrovetender()
        {
            try
            {
                if (BigJank.getOptionValue(Settings.Twitch))
                {
                    //Collabs.m_PlayableGrovetender.Run();
                    //DebugClass.Log($"PlayableGrovetender installed, modifying");
                }
            }
            catch (Exception)
            {
                DebugClass.Log($"PlayableGrovetender not installed, skipping");
            }
        }
        private static void PlayableScavenger()
        {
            try
            {
                if (BigJank.getOptionValue(Settings.Imposter))
                {
                    //Collabs.PlayableScavenger.Run();
                    //DebugClass.Log($"PlayableScavenger installed, modifying");
                }
            }
            catch (Exception)
            {
                DebugClass.Log($"PlayableScavenger not installed, skipping");
            }
        }
        private static void PlayableTemplar()
        {
            try
            {
                if (BigJank.getOptionValue(Settings.Heavy))
                {
                    //Collabs.m_PlayableTemplar.Run();
                    //DebugClass.Log($"PlayableTemplar installed, modifying");
                }
            }
            catch (Exception)
            {
                DebugClass.Log($"PlayableTemplar not installed, skipping");
            }
        }
        private static void PlayableMithrix()
        {
            try
            {
                if (BigJank.getOptionValue(Settings.Thanos))
                {
                    //Collabs.PlayableMithrix.Run();
                    //DebugClass.Log($"PlayableMithrix installed, modifying");
                }
            }
            catch (Exception)
            {
                DebugClass.Log($"PlayableMithrix not installed, skipping");
            }
        }
        private static void ChipTheBeetle()
        {
            try
            {
                if (BigJank.getOptionValue(Settings.FroggyChair))
                {
                    //Collabs.PlayableBeetle.Run();
                    //DebugClass.Log($"ChipTheBeetle installed, modifying");
                }
            }
            catch (Exception)
            {
                DebugClass.Log($"ChipTheBeetle not installed, skipping");
            }
        }
    }
}