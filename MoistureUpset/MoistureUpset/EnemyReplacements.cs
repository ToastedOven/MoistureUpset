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

namespace MoistureUpset
{
    public static class EnemyReplacements
    {
        public static void ReplaceModel(string prefab, string mesh, string png, int position = 0, bool replaceothers = false)
        {
            var fab = Resources.Load<GameObject>(prefab);
            var meshes = fab.GetComponentsInChildren<SkinnedMeshRenderer>();
            meshes[position].sharedMesh = Resources.Load<Mesh>(mesh);
            var texture = Resources.Load<Texture>(png);
            var blank = Resources.Load<Texture>("@MoistureUpset_na:assets/blank.png");
            for (int i = 0; i < meshes[position].sharedMaterials.Length; i++)
            {
                //Debug.Log($"-=============={meshes[position].sharedMaterials[i].shader.name}");
                //meshes[position].sharedMaterials[i].shader = Shader.Find("Standard");
                if (prefab == "prefabs/characterbodies/ShopkeeperBody" || prefab == "prefabs/characterbodies/TitanGoldBody")
                {
                    meshes[position].sharedMaterials[i].shader = Shader.Find("Standard");
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
                        meshes[i].sharedMesh = Resources.Load<Mesh>(mesh);
                    }
                }
            }
        }
        public static void ReplaceModel(SkinnedMeshRenderer meshes, string mesh, string png)
        {
            meshes.sharedMesh = Resources.Load<Mesh>(mesh);
            var texture = Resources.Load<Texture>(png);
            var blank = Resources.Load<Texture>("@MoistureUpset_na:assets/blank.png");
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
            meshes[position].sharedMesh = Resources.Load<Mesh>(mesh);
            var texture = Resources.Load<Texture>(png);
            var blank = Resources.Load<Texture>("@MoistureUpset_na:assets/blank.png");
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
                        meshes[i].sharedMesh = Resources.Load<Mesh>(mesh);
                    }
                }
            }
        }
        public static void ReplaceModel(string prefab, string mesh, int position = 0)
        {
            var fab = Resources.Load<GameObject>(prefab);
            var meshes = fab.GetComponentsInChildren<SkinnedMeshRenderer>();
            meshes[position].sharedMesh = Resources.Load<Mesh>(mesh);
        }
        public static void ReplaceModel(GameObject fab, string mesh, int position = 0)
        {
            var meshes = fab.GetComponentsInChildren<SkinnedMeshRenderer>();
            meshes[position].sharedMesh = Resources.Load<Mesh>(mesh);
        }
        public static void ReplaceMaterial(string prefab, string material, int position = 0)
        {
            var fab = Resources.Load<GameObject>(prefab);
            var mat = Resources.Load<Material>(material);
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
            var fab = Resources.Load<GameObject>(prefab);
            var meshes = fab.GetComponentsInChildren<SkinnedMeshRenderer>();
            var texture = Resources.Load<Texture>(png);
            var blank = Resources.Load<Texture>("@MoistureUpset_na:assets/blank.png");
            for (int i = 0; i < meshes[position].sharedMaterials.Length; i++)
            {
                if (prefab == "prefabs/characterbodies/ShopkeeperBody" || prefab == "prefabs/characterbodies/TitanGoldBody")
                {
                    meshes[position].sharedMaterials[i].shader = Shader.Find("Standard");
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
            }
        }
        public static void ReplaceMeshFilter(string prefab, string mesh, string png, int position = 0)
        {
            var fab = Resources.Load<GameObject>(prefab);
            var meshes = fab.GetComponentsInChildren<MeshFilter>();
            var texture = Resources.Load<Texture>(png);
            var renderers = fab.GetComponentsInChildren<Renderer>();
            for (int i = 0; i < renderers[position].sharedMaterials.Length; i++)
            {
                renderers[position].sharedMaterials[i].shader = Shader.Find("Standard");
                renderers[position].sharedMaterials[i].color = Color.white;
                renderers[position].sharedMaterials[i].mainTexture = texture;
                renderers[position].sharedMaterials[i].SetTexture("_EmTex", texture);
                renderers[position].sharedMaterials[i].SetTexture("_NormalTex", null);
            }
            meshes[position].sharedMesh = Resources.Load<Mesh>(mesh);
        }
        public static void ReplaceMeshFilter(GameObject prefab, string mesh, int position = 0)
        {
            var fab = prefab;
            var meshes = fab.GetComponentsInChildren<MeshFilter>();
            meshes[position].sharedMesh = Resources.Load<Mesh>(mesh);
        }
        public static void ReplaceMeshFilter(string prefab, string mesh, int position = 0)
        {
            var fab = Resources.Load<GameObject>(prefab);
            var meshes = fab.GetComponentsInChildren<MeshFilter>();
            meshes[position].sharedMesh = Resources.Load<Mesh>(mesh);
        }
        public static void ReplaceParticleSystemmesh(GameObject fab, string mesh, int spot = 0)
        {
            var meshes = fab.GetComponentsInChildren<ParticleSystemRenderer>();
            meshes[spot].mesh = Resources.Load<Mesh>(mesh);
        }
        public static void ReplaceMeshRenderer(string f, string mesh, string png, int spot = 0)
        {
            var fab = Resources.Load<GameObject>(f);
            var renderers = fab.GetComponentsInChildren<Renderer>();
            var meshes = fab.GetComponentsInChildren<MeshFilter>();
            var texture = Resources.Load<Texture>(png);
            for (int i = 0; i < renderers[spot].sharedMaterials.Length; i++)
            {
                renderers[spot].sharedMaterials[i].color = Color.white;
                renderers[spot].sharedMaterials[i].mainTexture = texture;
                renderers[spot].sharedMaterials[i].SetTexture("_EmTex", texture);
                renderers[spot].sharedMaterials[i].SetTexture("_NormalTex", null);
            }
            meshes[spot].sharedMesh = Resources.Load<Mesh>(mesh);
        }
        public static void ReplaceMeshRenderer(string f, string png, int spot = 0)
        {
            var fab = Resources.Load<GameObject>(f);
            var renderers = fab.GetComponentsInChildren<Renderer>();
            var texture = Resources.Load<Texture>(png);
            for (int i = 0; i < renderers[spot].sharedMaterials.Length; i++)
            {
                renderers[spot].sharedMaterials[i].color = Color.white;
                renderers[spot].sharedMaterials[i].mainTexture = texture;
                renderers[spot].sharedMaterials[i].SetTexture("_EmTex", texture);
                renderers[spot].sharedMaterials[i].SetTexture("_NormalTex", null);
            }
        }
        public static void ReplaceMeshRenderer(GameObject fab, string mesh, string png, int spot = 0)
        {
            var renderers = fab.GetComponentsInChildren<Renderer>();
            var meshes = fab.GetComponentsInChildren<MeshFilter>();
            var texture = Resources.Load<Texture>(png);
            for (int i = 0; i < renderers[spot].sharedMaterials.Length; i++)
            {
                renderers[spot].sharedMaterials[i].color = Color.white;
                renderers[spot].sharedMaterials[i].mainTexture = texture;
                renderers[spot].sharedMaterials[i].SetTexture("_EmTex", texture);
                renderers[spot].sharedMaterials[i].SetTexture("_NormalTex", null);
            }
            meshes[spot].sharedMesh = Resources.Load<Mesh>(mesh);
        }
        public static void LoadResource(string resource)
        {
            DebugClass.Log($"Loading {resource}");
            using (var assetStream = Assembly.GetExecutingAssembly().GetManifestResourceStream($"MoistureUpset.Models.{resource}"))
            {
                var MainAssetBundle = AssetBundle.LoadFromStream(assetStream);

                ResourcesAPI.AddProvider(new AssetBundleResourcesProvider($"@MoistureUpset_{resource}", MainAssetBundle));
            }
        }
        public static void LoadBNK(string bnk)
        {
            string s = $"MoistureUpset.bankfiles.{bnk}.bnk";
            DebugClass.Log(s);
            try
            {
                using (var bankStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(s))
                {
                    var bytes = new byte[bankStream.Length];
                    bankStream.Read(bytes, 0, bytes.Length);

                    SoundBanks.Add(bytes);
                }
            }
            catch (Exception e)
            {
                DebugClass.Log(e);
            }
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
                GreaterWisp();
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
                Collab();
                //SneakyFontReplacement();
            }
            catch (Exception e)
            {
                //Debug.Log(e);
            }
        }
        public static void DEBUG()
        {
        }
        private static void Icons()
        {
            if (BigJank.getOptionValue("Froggy Chair") == 1)
                UImods.ReplaceTexture2D("textures/bodyicons/BeetleBody", "MoistureUpset.Resources.froggychair.png");
            if (BigJank.getOptionValue("Winston") == 1)
                UImods.ReplaceTexture2D("textures/bodyicons/BeetleGuardBody", "MoistureUpset.Resources.winston.png");
            if (BigJank.getOptionValue("Winston") == 1)
                UImods.ReplaceTexture2D("textures/bodyicons/BeetleGuardAllyBody", "MoistureUpset.Resources.winston.png");
            if (BigJank.getOptionValue("Taco Bell") == 1)
                UImods.ReplaceTexture2D("textures/bodyicons/BellBody", "MoistureUpset.Resources.tacobell.png");
            if (BigJank.getOptionValue("Thomas") == 1)
                UImods.ReplaceTexture2D("textures/bodyicons/BisonBody", "MoistureUpset.Resources.thomas.png");
            if (BigJank.getOptionValue("Heavy") == 1)
                UImods.ReplaceTexture2D("textures/bodyicons/ClayBruiserBody", "MoistureUpset.Resources.heavy.png");
            if (BigJank.getOptionValue("Robloxian") == 1)
                UImods.ReplaceTexture2D("textures/bodyicons/GolemBody", "MoistureUpset.Resources.oof.png");
            if (BigJank.getOptionValue("Ghast") == 1)
                UImods.ReplaceTexture2D("textures/bodyicons/GreaterWispBody", "MoistureUpset.Resources.ghast.png");
            if (BigJank.getOptionValue("Trumpet Skeleton") == 1)
                UImods.ReplaceTexture2D("textures/bodyicons/ImpBody", "MoistureUpset.Resources.doot.png");
            if (BigJank.getOptionValue("Sans") == 1)
                UImods.ReplaceTexture2D("textures/bodyicons/ImpBossBody", "MoistureUpset.Resources.sans.png");
            if (BigJank.getOptionValue("Comedy") == 1)
                UImods.ReplaceTexture2D("textures/bodyicons/JellyfishBody", "MoistureUpset.Resources.joy.png");
            if (BigJank.getOptionValue("Mike Wazowski") == 1)
                UImods.ReplaceTexture2D("textures/bodyicons/LemurianBody", "MoistureUpset.Resources.mike.png");
            if (BigJank.getOptionValue("Bowser") == 1)
                UImods.ReplaceTexture2D("textures/bodyicons/LemurianBruiserBody", "MoistureUpset.Resources.bowser.png");
            if (BigJank.getOptionValue("Obama Prism") == 1)
                UImods.ReplaceTexture2D("textures/bodyicons/RoboBallBossBody", "MoistureUpset.Resources.obamasphere.png");
            if (BigJank.getOptionValue("Obama Prism") == 1)
                UImods.ReplaceTexture2D("textures/bodyicons/RoboBallMiniBody", "MoistureUpset.Resources.obamaprism.png");
            if (BigJank.getOptionValue("Obama prism") == 1)
                UImods.ReplaceTexture2D("textures/bodyicons/SuperRoboBallBossBody", "MoistureUpset.Resources.obamasphere.png");
            if (BigJank.getOptionValue("Dogplane") == 1)
                UImods.ReplaceTexture2D("textures/bodyicons/WispBody", "MoistureUpset.Resources.dogplane.png");
            if (BigJank.getOptionValue("Toad") == 1)
                UImods.ReplaceTexture2D("textures/bodyicons/MiniMushroomBody", "MoistureUpset.Resources.toad.png");
            if (BigJank.getOptionValue("Alex Jones") == 1)
                UImods.ReplaceTexture2D("textures/bodyicons/TitanGoldBody", "MoistureUpset.Resources.alexjones.png");
            if (BigJank.getOptionValue("Hagrid") == 1)
                UImods.ReplaceTexture2D("textures/bodyicons/ParentBody", "MoistureUpset.Resources.hagrid.png");
            if (BigJank.getOptionValue("Roblox Titan") == 1)
                UImods.ReplaceTexture2D("textures/bodyicons/TitanBody", "MoistureUpset.Resources.buffroblox.png");
            if (BigJank.getOptionValue("Lemme Smash") == 1)
                UImods.ReplaceTexture2D("textures/bodyicons/VultureBody", "MoistureUpset.Resources.lemmesmash.png");
            if (BigJank.getOptionValue("Crab Rave") == 1)
                UImods.ReplaceTexture2D("textures/bodyicons/NullifierBody", "MoistureUpset.Resources.crab.png");
            if (BigJank.getOptionValue("Skeleton Crab") == 1)
                UImods.ReplaceTexture2D("textures/bodyicons/HermitCrabBody", "MoistureUpset.Resources.jockey.png");
            if (BigJank.getOptionValue("Pool Noodle") == 1)
                UImods.ReplaceTexture2D("textures/bodyicons/MagmaWormBody", "MoistureUpset.Resources.noodle.png");
            if (BigJank.getOptionValue("Squirmles") == 1)
                UImods.ReplaceTexture2D("textures/bodyicons/ElectricWormBody", "MoistureUpset.Resources.werm.png");
            if (BigJank.getOptionValue("Giga Puddi") == 1)
                UImods.ReplaceTexture2D("textures/bodyicons/ClayBossBody", "MoistureUpset.Resources.puddi.png");
            if (BigJank.getOptionValue("WanderingAtEveryone") == 1)
                UImods.ReplaceTexture2D("textures/bodyicons/VagrantBody", "MoistureUpset.Resources.discord.png");
            if (BigJank.getOptionValue("Roflcopter") == 1)
                UImods.ReplaceTexture2D("textures/bodyicons/LunarWispBody", "MoistureUpset.Resources.rofl.png");
            if (BigJank.getOptionValue("Rob") == 1)
                UImods.ReplaceTexture2D("textures/bodyicons/LunarGolemBody", "MoistureUpset.Resources.rob.png");
            if (BigJank.getOptionValue("Nyan Cat") == 1)
                UImods.ReplaceTexture2D("textures/bodyicons/BeetleQueen2Body", "MoistureUpset.Resources.nyancat.png");
            if (BigJank.getOptionValue("Thanos") == 1)
                UImods.ReplaceTexture2D("textures/bodyicons/texBrotherIcon", "MoistureUpset.Resources.thanos.png");
            if (BigJank.getOptionValue("Twitch") == 1)
                UImods.ReplaceTexture2D("textures/bodyicons/GravekeeperBody", "MoistureUpset.Resources.twitch.png");
            if (BigJank.getOptionValue("Imposter") == 1)
                UImods.ReplaceTexture2D("textures/bodyicons/ScavBody", "MoistureUpset.Resources.imposter.png");
            if (BigJank.getOptionValue("Imposter") == 1)
                UImods.ReplaceTexture2D("textures/bodyicons/ScavLunarBody", "MoistureUpset.Resources.imposter.png");
        }
        private static void NonEnemyNames()
        {
            On.RoR2.Language.SetStringByToken += (orig, self, token, st) =>
            {
                if (BigJank.getOptionValue("NSFW") == 1 && BigJank.getOptionValue("Difficulty Names") == 1)
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
                if (BigJank.getOptionValue("In-Run Difficulty Names") == 1)
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
                if (BigJank.getOptionValue("Shrine Changes") == 1)
                {
                    if (token == "SHRINE_BLOOD_CONTEXT")
                    {
                        st = "Free Money";
                        if (BigJank.getOptionValue("Currency Changes") == 1)
                            st = "Free Tix";
                    }
                    else if (token == "SHRINE_BLOOD_USE_MESSAGE_2P")
                    {
                        st = "<style=cShrine>Wait it's not free. You have gained {1} gold.</color>";
                        if (BigJank.getOptionValue("Currency Changes") == 1)
                            st = "<style=cShrine>Wait it's not free. You have gained {1} tix.</color>";
                    }
                    else if (token == "SHRINE_BLOOD_USE_MESSAGE")
                    {
                        st = "<style=cShrine>{0} got stabbed for money, and has gained {1} gold.</color>";
                        if (BigJank.getOptionValue("Currency Changes") == 1)
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
                        if (BigJank.getOptionValue("Currency Changes") == 1)
                            st = "<style=cShrine>You lost tix.</color>";
                    }
                    else if (token == "SHRINE_CHANCE_FAIL_MESSAGE")
                    {
                        st = "<style=cShrine>{0} lost money.</color>";
                        if (BigJank.getOptionValue("Currency Changes") == 1)
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
                if (BigJank.getOptionValue("Misc") == 1)
                {
                    if (token == "PLAYER_PING_COOLDOWN")
                    {
                        st = "<style=cEvent>Stop</style>";
                    }
                }
                if (BigJank.getOptionValue("Robloxian") == 1)
                {
                    if (token == "FAMILY_GOLEM")
                    {
                        st = "<style=cWorldEvent>[WARNING] It feels like 2008 in here..</style>";
                    }
                }
                if (BigJank.getOptionValue("Comedy") == 1)
                {
                    if (token == "FAMILY_JELLYFISH")
                    {
                        st = "<style=cWorldEvent>[WARNING] You hear a distant laugh track..</style>";
                    }
                }
                if (BigJank.getOptionValue("Dogplane") == 1)
                {
                    if (token == "FAMILY_WISP")
                    {
                        st = "<style=cWorldEvent>[WARNING] Habadabadaga..</style>";
                    }
                }
                if (BigJank.getOptionValue("Trumpet Skeleton") == 1)
                {
                    if (token == "FAMILY_IMP")
                    {
                        st = "<style=cWorldEvent>[WARNING] Doot doot..</style>";
                    }
                }


                if (BigJank.getOptionValue("Alex Jones") == 1)
                {
                    if (token == "BAZAAR_SEER_GOLDSHORES")
                    {
                        st = "<style=cWorldEvent>You dream of fake news.</style>";
                    }
                }

                if (BigJank.getOptionValue("Obama Prism") == 1)
                {
                    if (token == "VULTURE_EGG_WARNING")
                    {
                        st = "<style=cWorldEvent>You hear a distant press conference..</style>";
                    }
                }

                if (BigJank.getOptionValue("Obama Prism") == 1)
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
            On.RoR2.ShrineChanceBehavior.AddShrineStack += (orig, self, activator) =>
            {
                float yes = self.GetFieldValue<int>("successfulPurchaseCount");
                orig(self, activator);
                if (self.GetFieldValue<int>("successfulPurchaseCount") == yes)
                {
                    //NetworkAssistant.playSound("ChanceFailure", activator.gameObject.transform.position);
                    //AkSoundEngine.PostEvent("ChanceFailure", mainBody.gameObject);

                    SoundAssets.PlaySound("ChanceFailure", activator.netId);
                }
                else
                {
                    //NetworkAssistant.playSound("ChanceSuccess", activator.gameObject.transform.position);
                    //AkSoundEngine.PostEvent("ChanceSuccess", mainBody.gameObject);

                    SoundAssets.PlaySound("ChanceSuccess", activator.netId);
                }
            };
        }
        private static void Names()
        {
            On.RoR2.Language.SetStringByToken += (orig, self, token, st) =>
            {
                if (st.Contains("Imp Overlord"))
                {
                    if (BigJank.getOptionValue("Sans") == 1)
                        st = st.Replace("Imp Overlord", "Sans");
                }
                else if (st == "Lord of the Red Plane")
                {
                    if (BigJank.getOptionValue("Sans") == 1)
                        st = "You're gonna have a bad time";
                }
                else if (st.Contains("Imp") && !st.Contains("Overlord") && !st.Contains("Impossible") && !st.Contains("Important") && !st.Contains("Improves"))
                {
                    if (BigJank.getOptionValue("Trumpet Skeleton") == 1)
                        st = st.Replace("Imp", "Trumpet Skeleton");
                }
                else if (st.Contains("Lesser Wisp"))
                {
                    if (BigJank.getOptionValue("Dogplane") == 1)
                        st = st.Replace("Lesser Wisp", "Dogplane");
                }
                else if (st.Contains("Jellyfish"))
                {
                    if (BigJank.getOptionValue("Comedy") == 1)
                        st = st.Replace("Jellyfish", "Comedy");
                }


                else if (st.Contains("Beetle Guard"))
                {
                    if (BigJank.getOptionValue("Winston") == 1)
                        st = st.Replace("Beetle Guard", "Winston");
                }
                else if (st.Contains("Beetle") && !st.Contains("Queen") && !st.Contains("Guard"))
                {
                    if (BigJank.getOptionValue("Froggy Chair") == 1)
                        st = st.Replace("Beetle", "Froggy Chair");
                }


                else if (st.Contains("Elder Lemurian"))
                {
                    if (BigJank.getOptionValue("Bowser") == 1)
                        st = st.Replace("Elder Lemurian", "Bowser");
                }
                else if (st.Contains("Lemurian") && !st.Contains("Elder"))
                {
                    if (BigJank.getOptionValue("Mike Wazowski") == 1)
                        st = st.Replace("Lemurian", "Mike Wazowski");
                }
                else if (st.Contains("Solus Probe"))
                {
                    if (BigJank.getOptionValue("Obama Prism") == 1)
                        st = st.Replace("Solus Probe", "Obama Prism");
                }
                else if (st.Contains("Brass Contraption"))
                {
                    if (BigJank.getOptionValue("Taco Bell") == 1)
                        st = st.Replace("Brass Contraption", "Taco Bell");
                }
                else if (st.Contains("Bighorn Bison"))
                {
                    if (BigJank.getOptionValue("Thomas") == 1)
                        st = st.Replace("Bighorn Bison", "Thomas");
                }
                else if (st.Contains("Stone Golem"))
                {
                    if (BigJank.getOptionValue("Robloxian") == 1)
                        st = st.Replace("Stone Golem", "Robloxian");
                }
                else if (st.Contains("Clay Templar"))
                {
                    if (BigJank.getOptionValue("Heavy") == 1)
                        st = st.Replace("Clay Templar", "Heavy");
                }
                else if (st.Contains("Greater Wisp"))
                {
                    if (BigJank.getOptionValue("Ghast") == 1)
                        st = st.Replace("Greater Wisp", "Ghast");
                }
                else if (st.Contains("Solus Control Unit"))
                {
                    if (BigJank.getOptionValue("Obama Prism") == 1)
                        st = st.Replace("Solus Control Unit", "Obama Sphere");
                }
                else if (st == "Corrupted AI")
                {
                    if (BigJank.getOptionValue("Obama Prism") == 1)
                        st = "Bringer of the Prisms";
                }
                else if (st == "Friend of Vultures")
                {
                    if (BigJank.getOptionValue("Obama Prism") == 1)
                        st = "Friend of Prisms";
                }
                else if (st.Contains("Alloy Worship Unit"))
                {
                    if (BigJank.getOptionValue("Obama Prism") == 1)
                        st = st.Replace("Alloy Worship Unit", "Obamium Worship Unit");
                }
                else if (st.Contains("Mini Mushrum"))
                {
                    if (BigJank.getOptionValue("Toad") == 1)
                        st = st.Replace("Mini Mushrum", "Toad");
                }
                else if (st.Contains("Aurelionite"))
                {
                    if (BigJank.getOptionValue("Alex Jones") == 1)
                        st = st.Replace("Aurelionite", "Alex Jones");
                }
                else if (token == "TITANGOLD_BODY_SUBTITLE")
                {
                    if (BigJank.getOptionValue("Alex Jones") == 1)
                        st = "Prince of the Social Media Shadow Realm";
                }
                else if (st.Contains("Stone Titan"))
                {
                    if (BigJank.getOptionValue("Roblox Titan") == 1)
                        st = st.Replace("Stone Titan", "Buff Robloxian");
                }
                else if (token == "TITAN_BODY_SUBTITLE")
                {
                    if (BigJank.getOptionValue("Roblox Titan") == 1)
                        st = "Oooooooooooooooooooooooooooof";
                }


                else if (st.Contains("Parent"))
                {
                    if (BigJank.getOptionValue("Hagrid") == 1)
                        st = st.Replace("Parent", "Hagrid");
                }


                else if (st.Contains("Alloy Vulture"))
                {
                    if (BigJank.getOptionValue("Lemme Smash") == 1)
                        st = st.Replace("Alloy Vulture", "Ron");
                }
                else if (st.Contains("Void Reaver"))
                {
                    if (BigJank.getOptionValue("Crab Rave") == 1)
                        st = st.Replace("Void Reaver", "Crab Rave");
                }
                else if (st.Contains("Hermit Crab"))
                {
                    if (BigJank.getOptionValue("Skeleton Crab") == 1)
                        st = st.Replace("Hermit Crab", "Spider Jockey");
                }
                else if (st.Contains("Magma Worm"))
                {
                    if (BigJank.getOptionValue("Pool Noodle") == 1)
                        st = st.Replace("Magma Worm", "Pool Noodle");
                }
                else if (token == "MAGMAWORM_BODY_SUBTITLE")
                {
                    if (BigJank.getOptionValue("Pool Noodle") == 1)
                        st = "Defender of the pool";
                }
                else if (st.Contains("Overloading Worm"))
                {
                    if (BigJank.getOptionValue("Squirmles") == 1)
                        st = st.Replace("Overloading Worm", "Squirmle");
                }
                else if (token == "ELECTRICWORM_BODY_SUBTITLE")
                {
                    if (BigJank.getOptionValue("Squirmles") == 1)
                        st = "String not included";
                }

                else if (st.Contains("Clay Dunestrider"))
                {
                    if (BigJank.getOptionValue("Giga Puddi") == 1)
                        st = st.Replace("Clay Dunestrider", "Giga Puddi");
                }
                else if (token == "CLAYBOSS_BODY_SUBTITLE")
                {
                    if (BigJank.getOptionValue("Giga Puddi") == 1)
                        st = "Sugoku Dekkai, Giga Puddi!";
                }

                else if (st.Contains("Beetle Queen"))
                {
                    if (BigJank.getOptionValue("Nyan Cat") == 1)
                        st = st.Replace("Beetle Queen", "Nyan Cat");
                }
                else if (token == "BEETLEQUEEN_BODY_SUBTITLE")
                {
                    if (BigJank.getOptionValue("Nyan Cat") == 1)
                        st = "Nyan nyan nyan nyan nyan nyan nyan nyan nyan nyan!";
                }

                else if (token.Contains("LUNARGOLEM"))
                {
                    if (BigJank.getOptionValue("Rob") == 1)
                    {
                        st = st.Replace("Lunar Chimera", "Rob");
                        st = st.Replace("Zenith", "Meme");
                    }
                }
                else if (token.Contains("LUNARWISP"))
                {
                    if (BigJank.getOptionValue("Roflcopter") == 1)
                    {
                        st = st.Replace("Lunar Chimera", "Roflcopter");
                        st = st.Replace("Zenith", "Meme");
                    }
                }

                else if (st.Contains("Mithrix"))
                {
                    if (BigJank.getOptionValue("Thanos") == 1)
                        st = st.Replace("Mithrix", "Thanos");
                }
                else if (token == "BROTHER_BODY_SUBTITLE")
                {
                    if (BigJank.getOptionValue("Thanos") == 1)
                        st = "King of Balance";
                }

                else if (st.Contains("Grovetender"))
                {
                    if (BigJank.getOptionValue("Twitch") == 1)
                        st = st.Replace("Grovetender", "Twitch.exe");
                }
                else if (token == "GRAVEKEEPER_BODY_SUBTITLE")
                {
                    if (BigJank.getOptionValue("Twitch") == 1)
                        st = "Meme Cultivator";
                }

                else if (st.Contains("Artifact Reliquary"))
                {
                    if (BigJank.getOptionValue("Cereal") == 1)
                        st = st.Replace("Artifact Reliquary", "Reese's puffs");
                }
                else if (token == "ARTIFACTSHELL_BODY_DESCRIPTION")
                {
                    if (BigJank.getOptionValue("Cereal") == 1)
                        st = "Eat em up eat em up eat em up eat em up!";
                }

                else if (st.Contains("Wandering Vagrant"))
                {
                    if (BigJank.getOptionValue("WanderingAtEveryone") == 1)
                        st = st.Replace("Wandering Vagrant", "@Everyone");
                }
                else if (token == "VAGRANT_BODY_SUBTITLE")
                {
                    if (BigJank.getOptionValue("WanderingAtEveryone") == 1)
                        st = "PING!";
                }

                else if (st.Contains("Scavenger"))
                {
                    if (BigJank.getOptionValue("Imposter") == 1)
                        st = st.Replace("Scavenger", "<color=#D9262C>Crewmate</color>");
                }
                else if (token == "SCAV_BODY_SUBTITLE")
                {
                    if (BigJank.getOptionValue("Imposter") == 1)
                        st = "Idk, seems pretty sus to me";
                }

                else if (token == "GOLDCHEST_NAME")
                {
                    if (BigJank.getOptionValue("Interactables") == 1)
                        st = "Ender Chest";
                }
                else if (token == "GOLDCHEST_CONTEXT")
                {
                    if (BigJank.getOptionValue("Interactables") == 1)
                        st = "Open ender chest";
                }

                else if (token == "BARREL1_NAME")
                {
                    if (BigJank.getOptionValue("Interactables") == 1)
                        st = "Can";
                }
                else if (token == "BARREL1_CONTEXT")
                {
                    if (BigJank.getOptionValue("Interactables") == 1)
                        st = "Open can";
                }

                else if (token == "EQUIPMENTBARREL_NAME")
                {
                    if (BigJank.getOptionValue("Interactables") == 1)
                        st = "Shulker Box";
                }
                else if (token == "EQUIPMENTBARREL_CONTEXT")
                {
                    if (BigJank.getOptionValue("Interactables") == 1)
                        st = "Open shulker box";
                }

                else if (token == "MULTISHOP_TERMINAL_NAME")
                {
                    if (BigJank.getOptionValue("Interactables") == 1)
                        st = "Fidget Spinner";
                }
                else if (token == "MULTISHOP_TERMINAL_CONTEXT")
                {
                    if (BigJank.getOptionValue("Interactables") == 1)
                        st = "SPEEEEEEEEEEEEN";
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
            if (BigJank.getOptionValue("Thanos") == 1)
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
            if (BigJank.getOptionValue("Awp UI") == 1)
                LoadBNK("awp");
            if (BigJank.getOptionValue("Chest noises") == 1)
                LoadBNK("chestinteraction");
            LoadBNK("playerdeath");
        }
        private static void Sans()
        {
            if (BigJank.getOptionValue("Sans") != 1)
                return;
            LoadBNK("sans");
            LoadResource("sans");
            ReplaceModel("prefabs/characterbodies/ImpBossBody", "@MoistureUpset_sans:assets/sans.mesh", "@MoistureUpset_sans:assets/sans.png");
            ReplaceMeshFilter("prefabs/projectileghosts/ImpVoidspikeProjectileGhost", "@MoistureUpset_sans:assets/boner.mesh", "@MoistureUpset_sans:assets/boner.png");
        }
        private static void Shop()
        {
            if (BigJank.getOptionValue("Merchant") != 1)
                return;
            LoadResource("shop");
            ReplaceModel("prefabs/characterbodies/ShopkeeperBody", "@MoistureUpset_shop:assets/shop.mesh", "@MoistureUpset_shop:assets/shop.png");
        }
        private static void BeetleGuard()
        {
            if (BigJank.getOptionValue("Winston") != 1)
                return;
            LoadBNK("beetleguard");
            LoadResource("winston");
            var fab = Resources.Load<GameObject>("prefabs/characterbodies/BeetleGuardBody");
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
            ReplaceModel("prefabs/characterbodies/BeetleGuardBody", "@MoistureUpset_winston:assets/winston.mesh", "@MoistureUpset_winston:assets/winston.png");
            fab = Resources.Load<GameObject>("prefabs/characterbodies/BeetleGuardAllyBody");
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
            ReplaceModel("prefabs/characterbodies/BeetleGuardAllyBody", "@MoistureUpset_winston:assets/winston.mesh", "@MoistureUpset_winston:assets/blinston.png");
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
            if (BigJank.getOptionValue("Comedy") != 1)
                return;
            LoadBNK("comedy");
            LoadResource("jelly");
            ReplaceModel("prefabs/characterbodies/JellyfishBody", "@MoistureUpset_jelly:assets/jelly.mesh", "@MoistureUpset_jelly:assets/jelly.png");
            On.EntityStates.JellyfishMonster.JellyNova.Detonate += (orig, self) =>
            {
                SoundAssets.PlaySound("JellyDetonate", self.outer.gameObject);
                //NetworkAssistant.playSound("JellyDetonate", self.outer.gameObject.transform.position);
                orig(self);
            };
        }
        private static void TacoBell()
        {
            if (BigJank.getOptionValue("Taco Bell") != 1)
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
            ReplaceModel("prefabs/characterbodies/BellBody", "@MoistureUpset_tacobell:assets/taco.mesh", "@MoistureUpset_tacobell:assets/taco.png");
            ReplaceMeshFilter("prefabs/projectileghosts/BellBallGhost", "@MoistureUpset_tacobell:assets/toco.mesh", "@MoistureUpset_tacobell:assets/toco.png");
        }
        //public static void ReplaceFont(string ogFont, string newFont)
        //{
        //    var font = Resources.Load<Font>(ogFont);
        //    foreach (var item in font.material.GetTexturePropertyNames())
        //    {
        //        Debug.Log($"namnesadasd----=-=-=-=-=-{item}");
        //    }
        //    Debug.Log($"fontname----=-=-=-=-=-{font.material.mainTexture.name}");
        //    font.material.SetTexture("_MainTex", Resources.Load<Font>(newFont).material.mainTexture);
        //    Debug.Log($"fontname----=-=-=-=-=-{font.material.mainTexture.name}");
        //    foreach (var item in font.material.GetTexturePropertyNames())
        //    {
        //        Debug.Log($"namnesadasd----=-=-=-=-=-{item}");
        //    }
        //    //var fab = Resources.Load<Font>(ogFont);
        //    //var fab2 = Resources.Load<Font>(newFont);
        //    //fab.characterInfo = fab2.characterInfo;
        //    //fab.fontNames = fab2.fontNames;
        //    //fab.hideFlags = fab2.hideFlags;
        //    //fab.material = fab2.material;
        //    //fab.name = fab2.name;
        //}
        //private static void SneakyFontReplacement()
        //{
        //    ReplaceFont("tmpfonts/fontsource/Bazaronite", "@MoistureUpset_robloxfont:assets/roblox_font.ttf");
        //    ReplaceFont("tmpfonts/fontsource/BOMBARD_", "@MoistureUpset_robloxfont:assets/roblox_font.ttf");
        //    ReplaceFont("tmpfonts/fontsource/NotoSans-Regular", "@MoistureUpset_robloxfont:assets/roblox_font.ttf");
        //    ReplaceFont("tmpfonts/fontsource/RiskofRainFont", "@MoistureUpset_robloxfont:assets/roblox_font.ttf");
        //    ReplaceFont("tmpfonts/fontsource/TRACER__", "@MoistureUpset_robloxfont:assets/roblox_font.ttf");
        //    ReplaceFont("tmpfonts/fontsource/VCR_OSD_MONO", "@MoistureUpset_robloxfont:assets/roblox_font.ttf");


        //    //fab = Resources.Load<Font>("tmpfonts/fontsource/BOMBARD_");
        //    //fab2 = Resources.Load<Font>("@MoistureUpset_robloxfont:assets/roblox_font.ttf");


        //    //fab = Resources.Load<Font>("tmpfonts/fontsource/NotoSans-Regular");
        //    //fab2 = Resources.Load<Font>("@MoistureUpset_robloxfont:assets/roblox_font.ttf");


        //    //fab = Resources.Load<Font>("tmpfonts/fontsource/RiskofRainFont");
        //    //fab2 = Resources.Load<Font>("@MoistureUpset_robloxfont:assets/roblox_font.ttf");


        //    //fab = Resources.Load<Font>("tmpfonts/fontsource/TRACER__");
        //    //fab2 = Resources.Load<Font>("@MoistureUpset_robloxfont:assets/roblox_font.ttf");

        //    //fab = Resources.Load<Font>("tmpfonts/fontsource/VCR_OSD_MONO");
        //    //fab2 = Resources.Load<Font>("@MoistureUpset_robloxfont:assets/roblox_font.ttf");
        //}
        private static void MiniMushroom()
        {
            if (BigJank.getOptionValue("Toad") != 1)
                return;
            LoadBNK("toad");
            LoadResource("toad1");
            var fab = Resources.Load<GameObject>("prefabs/characterbodies/MiniMushroomBody");
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
            ReplaceModel("prefabs/characterbodies/MiniMushroomBody", "@MoistureUpset_toad1:assets/toad.mesh", "@MoistureUpset_toad1:assets/toad.png");
            ReplaceMeshFilter("prefabs/projectileghosts/SporeGrenadeGhost", "@MoistureUpset_toad1:assets/toadbomb.mesh", "@MoistureUpset_toad1:assets/toadbomb.png");
            var g = Resources.Load<GameObject>("prefabs/projectileghosts/SporeGrenadeGhost");
            var meshfilter = g.GetComponentInChildren<MeshFilter>();
            var skinnedmesh = meshfilter.gameObject.AddComponent<SkinnedMeshRenderer>() as SkinnedMeshRenderer;
            skinnedmesh.transform.position = g.transform.position;
            skinnedmesh.sharedMesh = Resources.Load<Mesh>("@MoistureUpset_toad1:assets/toadbomblid.mesh");

            skinnedmesh.sharedMaterial = Resources.Load<Material>("@MoistureUpset_toad1:assets/toadbomb.mat");
            skinnedmesh.sharedMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
            skinnedmesh.sharedMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            skinnedmesh.sharedMaterial.SetInt("_ZWrite", 0);
            skinnedmesh.sharedMaterial.DisableKeyword("_ALPHATEST_ON");
            skinnedmesh.sharedMaterial.DisableKeyword("_ALPHABLEND_ON");
            skinnedmesh.sharedMaterial.EnableKeyword("_ALPHAPREMULTIPLY_ON");
            skinnedmesh.sharedMaterial.renderQueue = 3000;
            meshfilter.transform.localScale *= .7f;

            var splat = Resources.Load<GameObject>("prefabs/projectiles/SporeGrenadeProjectileDotZone");
            var texture = Resources.Load<Texture>("@MoistureUpset_toad1:assets/toadsplatcolorhighres.png");
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
            if (BigJank.getOptionValue("Trumpet Skeleton") != 1)
                return;
            LoadResource("dooter");
            ReplaceModel("prefabs/characterbodies/ImpBody", "@MoistureUpset_dooter:assets/dooter.mesh", "@MoistureUpset_dooter:assets/dooter.png");


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
            if (BigJank.getOptionValue("Froggy Chair") != 1)
                return;
            LoadBNK("beetle");
            LoadResource("frog");
            var fab = Resources.Load<GameObject>("prefabs/characterbodies/BeetleBody");
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
            ReplaceModel("prefabs/characterbodies/BeetleBody", "@MoistureUpset_frog:assets/frogchair.mesh", "@MoistureUpset_frog:assets/frogchair.png");
        }
        private static void ElderLemurian()
        {
            if (BigJank.getOptionValue("Bowser") != 1)
                return;
            LoadBNK("bowser");
            LoadResource("bowser");
            ReplaceModel("prefabs/characterbodies/LemurianBruiserBody", "@MoistureUpset_bowser:assets/bowser.mesh", "@MoistureUpset_bowser:assets/bowser.png");
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
            if (BigJank.getOptionValue("Heavy") != 1)
                return;
            LoadBNK("heavy");
            LoadResource("heavy");
            ReplaceModel("prefabs/characterbodies/ClayBruiserBody", "@MoistureUpset_heavy:assets/heavy.mesh", "@MoistureUpset_heavy:assets/heavy.png");
            ReplaceModel("prefabs/characterbodies/ClayBruiserBody", "@MoistureUpset_heavy:assets/minigun.mesh", "@MoistureUpset_heavy:assets/heavy.png", 1);

            var fab = Resources.Load<GameObject>("prefabs/characterbodies/ClayBruiserBody");
            var meshes = fab.GetComponentsInChildren<SkinnedMeshRenderer>();
            for (int i = 0; i < meshes.Length; i++)
            {
                if (i != 0 && i != 1)
                {
                    meshes[i].sharedMesh = Resources.Load<Mesh>("@MoistureUpset_na:assets/na.mesh");
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
            if (BigJank.getOptionValue("Ghast") != 1)
                return;
            LoadBNK("ghast");
            LoadResource("ghast");
            ReplaceModel("prefabs/characterbodies/GreaterWispBody", "@MoistureUpset_ghast:assets/ghast.mesh", "@MoistureUpset_ghast:assets/ghast.png");
            var fab = Resources.Load<GameObject>("prefabs/characterbodies/GreaterWispBody");
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
            On.EntityStates.GreaterWispMonster.DeathState.OnEnter += (orig, self) =>
            {
                Util.PlaySound("GhastDeath", self.outer.gameObject);
                orig(self);
            };
            On.EntityStates.GreaterWispMonster.FireCannons.OnEnter += (orig, self) =>
            {
                Util.PlaySound("GhastAttack", self.outer.gameObject);
                orig(self);
            };
            //On.EntityStates.EntityState.OnEnter += (orig, self) =>
            //{
            //    if (self.outer.gameObject.name.Contains("GreaterWispMaster"))
            //    {
            //        Util.PlaySound("GhastSpawn", self.outer.gameObject);
            //    }
            //    else if (self.outer.gameObject.name.Contains("GreaterWispBody"))
            //    {
            //        Util.PlaySound("GhastSpawn", self.outer.gameObject);
            //    }
            //    orig(self);
            //};
        }
        private static void Wisp()
        {
            if (BigJank.getOptionValue("Dogplane") != 1)
                return;
            LoadBNK("dogplane");
            LoadResource("dogplane");
            ReplaceModel("prefabs/characterbodies/WispBody", "@MoistureUpset_dogplane:assets/bahdog.mesh", "@MoistureUpset_dogplane:assets/bahdog.png");
            ReplaceModel("prefabs/characterbodies/WispSoulBody", "@MoistureUpset_dogplane:assets/bahdog.mesh", "@MoistureUpset_dogplane:assets/bahdog.png");
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
            var fab = Resources.Load<GameObject>("prefabs/characterbodies/WispBody");
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
            if (BigJank.getOptionValue("Obama Prism") != 1)
                return;
            LoadBNK("prism");
            LoadResource("obamaprism");
            ReplaceModel("prefabs/characterbodies/RoboBallMiniBody", "@MoistureUpset_obamaprism:assets/Obamium.mesh", "@MoistureUpset_obamaprism:assets/Obruhma.png");
            ReplaceModel("prefabs/characterbodies/RoboBallBossBody", "@MoistureUpset_obamaprism:assets/obamasphere.mesh", "@MoistureUpset_obamaprism:assets/Obruhma.png");
            ReplaceModel("prefabs/characterbodies/SuperRoboBallBossBody", "@MoistureUpset_obamaprism:assets/obamasphere.mesh", "@MoistureUpset_obamaprism:assets/Obruhma.png");
            ReplaceMeshFilter("prefabs/characterbodies/SuperRoboBallBossBody", "@MoistureUpset_obamaprism:assets/crown.mesh", "@MoistureUpset_obamaprism:assets/crown.png");
            ReplaceMeshFilter("prefabs/characterbodies/SuperRoboBallBossBody", "@MoistureUpset_na:assets/na1.mesh", "@MoistureUpset_obamaprism:assets/crown.png", 1);
            ReplaceMeshFilter("prefabs/characterbodies/SuperRoboBallBossBody", "@MoistureUpset_na:assets/na1.mesh", "@MoistureUpset_obamaprism:assets/crown.png", 2);
            ReplaceMeshFilter("prefabs/characterbodies/SuperRoboBallBossBody", "@MoistureUpset_na:assets/na1.mesh", "@MoistureUpset_obamaprism:assets/crown.png", 3);
            ReplaceMeshFilter("prefabs/characterbodies/SuperRoboBallBossBody", "@MoistureUpset_na:assets/na1.mesh", "@MoistureUpset_obamaprism:assets/crown.png", 4);
            ReplaceMeshFilter("prefabs/characterbodies/SuperRoboBallBossBody", "@MoistureUpset_na:assets/na1.mesh", "@MoistureUpset_obamaprism:assets/crown.png", 5);
            ReplaceMeshFilter("prefabs/characterbodies/SuperRoboBallBossBody", "@MoistureUpset_na:assets/na1.mesh", "@MoistureUpset_obamaprism:assets/crown.png", 6);
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
            if (BigJank.getOptionValue("Mike Wazowski") != 1)
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
            ReplaceModel("prefabs/characterbodies/LemurianBody", "@MoistureUpset_mike:assets/mike.mesh", "@MoistureUpset_mike:assets/mike.png");
        }
        private static void Golem()
        {
            if (BigJank.getOptionValue("Robloxian") != 1)
                return;
            LoadBNK("oof");
            LoadResource("noob");
            On.EntityStates.GolemMonster.ChargeLaser.OnEnter += (orig, self) =>
            {
                EntityStates.GolemMonster.ChargeLaser.attackSoundString = "GolemChargeLaser";
                try
                {
                    GameObject[] objects = GameObject.FindObjectsOfType<GameObject>();
                    GameObject g = self.outer.gameObject.GetComponent<Rigidbody>().gameObject;
                    for (int i = 0; i < objects.Length; i++)
                    {
                        if (objects[i] == g)
                        {
                            Texture t = Resources.Load<Texture>("@MoistureUpset_noob:assets/Noob1TexLaser.png");
                            var mesh = objects[i - 3].GetComponent<SkinnedMeshRenderer>();
                            foreach (var item in mesh.sharedMaterials)
                            {
                                item.mainTexture = t;
                            }
                            break;
                        }
                    }
                }
                catch (Exception e)
                {
                    //Debug.Log(e);
                }
                orig(self);
            };
            On.EntityStates.GolemMonster.FireLaser.OnEnter += (orig, self) =>
            {
                EntityStates.GolemMonster.FireLaser.attackSoundString = "GolemFireLaser";
                try
                {
                    GameObject[] objects = GameObject.FindObjectsOfType<GameObject>();
                    GameObject g = self.outer.gameObject.GetComponent<Rigidbody>().gameObject;
                    for (int i = 0; i < objects.Length; i++)
                    {
                        if (objects[i] == g)
                        {
                            Texture t = Resources.Load<Texture>("@MoistureUpset_noob:assets/Noob1Tex.png");
                            var mesh = objects[i - 3].GetComponent<SkinnedMeshRenderer>();
                            foreach (var item in mesh.sharedMaterials)
                            {
                                item.mainTexture = t;
                            }
                            break;
                        }
                    }
                }
                catch (Exception e)
                {
                    //Debug.Log(e);
                }
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
            ReplaceModel("prefabs/characterbodies/GolemBody", "@MoistureUpset_noob:assets/N00b.mesh", "@MoistureUpset_noob:assets/Noob1Tex.png");
        }
        private static void Bison()
        {
            if (BigJank.getOptionValue("Thomas") != 1)
                return;
            LoadResource("thomas");
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
            ReplaceModel("prefabs/characterbodies/BisonBody", "@MoistureUpset_thomas:assets/thomas.mesh", "@MoistureUpset_thomas:assets/dankengine.png", 0, true);
        }
        private static void RobloxTitan()
        {
            if (BigJank.getOptionValue("Roblox Titan") != 1)
                return;
            LoadResource("roblox");
            ReplaceModel("prefabs/characterbodies/TitanBody", "@MoistureUpset_roblox:assets/robloxtitan.mesh", "@MoistureUpset_roblox:assets/robloxtitan.png");
            var fab = Resources.Load<GameObject>("prefabs/characterbodies/TitanBody");
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
                Texture t = Resources.Load<Texture>("@MoistureUpset_na:assets/solid.png");
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
            On.RoR2.EffectManager.SpawnEffect_EffectIndex_EffectData_bool += (orig, index, data, transmit) =>
            {
                if ((int)index == 404 && !transmit)
                {
                    if (NetworkClient.active)
                    {
                        EffectDef effectDef = EffectCatalog.GetEffectDef(index);
                        var pre = effectDef.prefab;
                        var yeet = pre.GetComponentsInChildren<ParticleSystemRenderer>()[0];
                        yeet.material.mainTexture = Resources.Load<Texture>("@MoistureUpset_roblox:assets/robloxfist.png");
                        try
                        {
                            int num = UnityEngine.Random.Range(0, 100);
                            if (num == 99)
                            {
                                yeet.mesh = Resources.Load<Mesh>("@MoistureUpset_roblox:assets/jj5x5.mesh");
                                RoR2.Audio.PointSoundManager.EmitSoundLocal(AkSoundEngine.GetIDFromString("RobloxJJ5X5"), data.origin);
                            }
                            else
                                switch (num % 7)
                                {
                                    case 0:
                                        yeet.mesh = Resources.Load<Mesh>("@MoistureUpset_roblox:assets/pizza.mesh");
                                        RoR2.Audio.PointSoundManager.EmitSoundLocal(AkSoundEngine.GetIDFromString("RobloxPizza"), data.origin);
                                        break;
                                    case 1:
                                        yeet.mesh = Resources.Load<Mesh>("@MoistureUpset_roblox:assets/sword.mesh");
                                        RoR2.Audio.PointSoundManager.EmitSoundLocal(AkSoundEngine.GetIDFromString("RobloxSword"), data.origin);
                                        break;
                                    case 2:
                                        yeet.mesh = Resources.Load<Mesh>("@MoistureUpset_roblox:assets/cola.mesh");
                                        RoR2.Audio.PointSoundManager.EmitSoundLocal(AkSoundEngine.GetIDFromString("RobloxCola"), data.origin);
                                        break;
                                    case 3:
                                        yeet.mesh = Resources.Load<Mesh>("@MoistureUpset_roblox:assets/cake.mesh");
                                        RoR2.Audio.PointSoundManager.EmitSoundLocal(AkSoundEngine.GetIDFromString("RobloxCake"), data.origin);
                                        break;
                                    case 4:
                                        yeet.mesh = Resources.Load<Mesh>("@MoistureUpset_roblox:assets/burger.mesh");
                                        RoR2.Audio.PointSoundManager.EmitSoundLocal(AkSoundEngine.GetIDFromString("RobloxBurger"), data.origin);
                                        break;
                                    case 5:
                                        yeet.mesh = Resources.Load<Mesh>("@MoistureUpset_roblox:assets/gravity.mesh");
                                        RoR2.Audio.PointSoundManager.EmitSoundLocal(AkSoundEngine.GetIDFromString("RobloxGravity"), data.origin);
                                        break;
                                    case 6:
                                        yeet.mesh = Resources.Load<Mesh>("@MoistureUpset_roblox:assets/robloxtaco.mesh");
                                        RoR2.Audio.PointSoundManager.EmitSoundLocal(AkSoundEngine.GetIDFromString("RobloxTaco"), data.origin);
                                        break;
                                    default:
                                        break;
                                }
                            yeet.transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
                        }
                        catch (Exception e)
                        {
                            //Debug.Log(e);
                        }
                    }
                }
                orig(index, data, transmit);
            };
            On.EntityStates.TitanMonster.FireFist.PlaceSingleDelayBlast += (orig, self, position, delay) =>
            {
                if (!self.outer.gameObject.name.Contains("Gold"))
                {
                    GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/NetworkedObjects/GenericDelayBlast"), position, Quaternion.identity);
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
                    //var meshes = self.outer.gameObject.GetComponentsInChildren<MeshRenderer>();
                    //for (int i = 0; i < meshes.Length; i++)
                    //{
                    //    if (meshes[i].name.StartsWith("spm") || meshes[i].name.StartsWith("bb"))
                    //    {
                    //        meshes[i].gameObject.SetActive(false);
                    //    }
                    //    else
                    //    {
                    //        Debug.Log($"-------------{meshes[i].name}");
                    //    }
                    //}
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
                    Texture t = Resources.Load<Texture>("@MoistureUpset_roblox:assets/nominecraft.png");
                    var particle = EntityStates.TitanMonster.RechargeRocks.rockControllerPrefab.GetComponentsInChildren<ParticleSystemRenderer>()[1];
                    var system = EntityStates.TitanMonster.RechargeRocks.rockControllerPrefab.GetComponentsInChildren<ParticleSystem>()[1];
                    system.startRotation = 0;
                    system.maxParticles = 1;
                    try
                    {
                        particle.material.shader = Shader.Find("Sprites/Default");
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
            if (BigJank.getOptionValue("Alex Jones") != 1)
                return;
            LoadResource("alexjones");
            ReplaceModel("prefabs/characterbodies/TitanGoldBody", "@MoistureUpset_alexjones:assets/alexjones.mesh", "@MoistureUpset_alexjones:assets/alexjones.png");
            var fab = Resources.Load<GameObject>("prefabs/characterbodies/TitanGoldBody");
            try
            {
                Texture t = Resources.Load<Texture>("@MoistureUpset_na:assets/solid.png");
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
            //        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/NetworkedObjects/GenericDelayBlast"), position, Quaternion.identity);
            //        AkSoundEngine.PostEvent("AlexFistDelayed", gameObject);
            //        GameObject.Destroy(gameObject);
            //    }
            //};
            On.RoR2.EffectManager.SpawnEffect_EffectIndex_EffectData_bool += (orig, index, data, transmit) =>
            {
                ////////////////////406
                if ((int)index == 406 && !transmit)
                {
                    RoR2.Audio.PointSoundManager.EmitSoundLocal(AkSoundEngine.GetIDFromString("OHSHITWADDUP"), data.origin);
                }
                orig(index, data, transmit);
            };
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
                    yeet.mesh = Resources.Load<Mesh>("@MoistureUpset_alexjones:assets/datboi.mesh");
                    yeet.transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
                    yeet.material.mainTexture = Resources.Load<Texture>("@MoistureUpset_alexjones:assets/frogge.png");
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
                Texture t = Resources.Load<Texture>("@MoistureUpset_alexjones:assets/datboi.png");
                var particle = EntityStates.TitanMonster.RechargeRocks.rockControllerPrefab.GetComponentsInChildren<ParticleSystemRenderer>()[1];
                var system = EntityStates.TitanMonster.RechargeRocks.rockControllerPrefab.GetComponentsInChildren<ParticleSystem>()[1];
                system.startRotation = 0;
                system.maxParticles = 1;
                try
                {
                    particle.material.shader = Shader.Find("Sprites/Default");
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
            if (BigJank.getOptionValue("Lemme Smash") != 1)
                return;
            LoadBNK("vulture");
            LoadResource("lemmesmash");
            ReplaceModel("prefabs/characterbodies/VultureBody", "@MoistureUpset_lemmesmash:assets/lemmesmasheyes.mesh", "@MoistureUpset_lemmesmash:assets/lemmesmash.png");
            ReplaceModel("prefabs/characterbodies/VultureBody", "@MoistureUpset_lemmesmash:assets/lemmesmasheyes.mesh", "@MoistureUpset_na:assets/blank.png", 1);
            ReplaceModel("prefabs/characterbodies/VultureBody", "@MoistureUpset_lemmesmash:assets/kevinishomosex/vulturemesh.mesh", "@MoistureUpset_lemmesmash:assets/lemmesmash.png", 2);
            //I know this is shitty but it works and at this point im too scared to change it
            //Also this only happens at startup so who cares amirite?
            var fab = Resources.Load<GameObject>("prefabs/characterbodies/VultureBody");

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
                            item.sharedMaterial.mainTexture = Resources.Load<Texture>("@MoistureUpset_lemmesmash:assets/lemmefeather.png");
                        }
                    }
                }
            }
        }
        private static void Hagrid()
        {
            if (BigJank.getOptionValue("Hagrid") != 1)
                return;
            LoadResource("hagrid");
            LoadBNK("hagrid");
            ReplaceModel("prefabs/characterbodies/ParentBody", "@MoistureUpset_hagrid:assets/hagrid.mesh", "@MoistureUpset_hagrid:assets/hagrid.png");
            On.EntityStates.ParentMonster.LoomingPresence.SetPosition += (orig, self, pos) =>
            {
                orig(self, pos);
                AkSoundEngine.PostEvent("HagridTeleport", self.outer.gameObject);
            };
        }
        public static void MakePoolNoodleBlue()
        {
            BlueParticles("prefabs/characterbodies/MagmaWormBody");
            BlueParticles("prefabs/effects/MagmaWormBurrow");
            BlueParticles("prefabs/effects/MagmaWormDeath");
            BlueParticles("prefabs/effects/MagmaWormDeathDust");
            BlueParticles("prefabs/effects/MagmaWormImpactExplosion");
            BlueParticles("prefabs/effects/MagmaWormRupture");
            BlueParticles("prefabs/effects/MagmaWormWarning");
        }
        private static void BlueParticles(string path)
        {
            var fab = Resources.Load<GameObject>(path);
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
            if (BigJank.getOptionValue("Pool Noodle") != 1)
                return;
            LoadResource("noodle");


            var fab = Resources.Load<GameObject>("prefabs/characterbodies/MagmaWormBody");
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
            ReplaceModel("prefabs/characterbodies/MagmaWormBody", "@MoistureUpset_noodle:assets/noodle.mesh", "@MoistureUpset_noodle:assets/noodle.png");
            var mesh = fab.GetComponentInChildren<SkinnedMeshRenderer>();
            var blank = Resources.Load<Texture>("@MoistureUpset_na:assets/blank.png");
            for (int i = 0; i < mesh.sharedMaterials.Length; i++)
            {
                mesh.sharedMaterials[i].SetTexture("_FlowHeightRamp", blank);
                mesh.sharedMaterials[i].SetTexture("_FlowHeightmap", blank);
                mesh.sharedMaterials[i].SetTexture("_FlowTex", blank);
            }
            var fab2 = Resources.Load<GameObject>("prefabs/characterbodies/ElectricWormBody");
            foreach (var item in fab.GetComponentsInChildren<UnityEngine.Rendering.PostProcessing.PostProcessVolume>())
            {
                var ting = fab2.GetComponentInChildren<UnityEngine.Rendering.PostProcessing.PostProcessVolume>();
                ((UnityEngine.Rendering.PostProcessing.PostProcessProfile)item.sharedProfile).settings = ((UnityEngine.Rendering.PostProcessing.PostProcessProfile)ting.sharedProfile).settings;
            }
            MakePoolNoodleBlue();
        }
        private static void Skeleton()
        {
            if (BigJank.getOptionValue("Skeleton Crab") != 1)
                return;
            LoadBNK("jockey");
            LoadResource("skeleton");
            //ReplaceTexture("prefabs/characterbodies/HermitCrabBody", "@MoistureUpset_skeleton:assets/skeleton.png");
            ReplaceModel("prefabs/characterbodies/HermitCrabBody", "@MoistureUpset_skeleton:assets/skeleton.mesh", "@MoistureUpset_skeleton:assets/skeleton.png");
            var fab = Resources.Load<GameObject>("prefabs/characterbodies/HermitCrabBody");
            var blank = Resources.Load<Texture>("@MoistureUpset_na:assets/blank.png");
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
            ReplaceMeshFilter("prefabs/projectileghosts/HermitCrabBombGhost", "@MoistureUpset_skeleton:assets/arrow.mesh", "@MoistureUpset_skeleton:assets/arrow.png");
            fab = Resources.Load<GameObject>("prefabs/projectileghosts/HermitCrabBombGhost");
            fab.AddComponent<ArrowFixer>();
            fab.GetComponentInChildren<Rewired.ComponentControls.Effects.RotateAroundAxis>().speed = Rewired.ComponentControls.Effects.RotateAroundAxis.Speed.Stopped;
            fab.GetComponentInChildren<ParticleSystemRenderer>().enabled = false;
        }
        private static void CrabRave()
        {
            if (BigJank.getOptionValue("Crab Rave") != 1)
                return;
            LoadResource("crabrave");
            LoadBNK("crabrave");
            ReplaceModel("prefabs/characterbodies/NullifierBody", "@MoistureUpset_crabrave:assets/crab.mesh", "@MoistureUpset_crabrave:assets/crab.png", 1);
            ReplaceModel("prefabs/characterbodies/NullifierBody", "@MoistureUpset_na:assets/na.mesh", "@MoistureUpset_na:assets/blank.png");
        }
        private static void PUDDI()
        {
            if (BigJank.getOptionValue("Giga Puddi") != 1)
                return;
            LoadResource("puddi");
            LoadBNK("puddi");
            ReplaceModel("prefabs/characterbodies/ClayBossBody", "@MoistureUpset_puddi:assets/puddi.mesh", "@MoistureUpset_puddi:assets/puddi.png");
            var fab = Resources.Load<GameObject>("prefabs/characterbodies/ClayBossBody");
            var mesh = fab.GetComponentInChildren<SkinnedMeshRenderer>();
            var blank = Resources.Load<Texture>("@MoistureUpset_na:assets/blank.png");
            for (int i = 0; i < mesh.sharedMaterials.Length; i++)
            {
                mesh.sharedMaterials[i].SetTexture("_PrintRamp", blank);
                mesh.sharedMaterials[i].SetTexture("_GreenChannelNormalTex", blank);
                mesh.sharedMaterials[i].SetTexture("_GreenChannelTex", blank);
                mesh.sharedMaterials[i].SetTexture("_SliceAlphaTex", blank);
            }
            ReplaceMeshFilter("prefabs/projectileghosts/ClayPotProjectileGhost", "@MoistureUpset_puddi:assets/puddighost.mesh", "@MoistureUpset_puddi:assets/puddi.png");
            Vector4 color = new Vector4(87f / 2500f, 40f / 2500f, 17f / 2500f, 1);
            foreach (var item in fab.GetComponentsInChildren<LineRenderer>())
            {
                item.sharedMaterial.SetVector("_TintColor", color);
                item.sharedMaterial.SetVector("_EmissionColor", color);
            }
            fab = Resources.Load<GameObject>("prefabs/projectileghosts/TarballGhost");
            var mesh2 = fab.GetComponentsInChildren<MeshRenderer>();
            foreach (var item in mesh2)
            {
                item.sharedMaterial = Resources.Load<Material>("@MoistureUpset_puddi:assets/puddi.mat");
            }
            fab.GetComponentsInChildren<ParticleSystemRenderer>()[1].gameObject.SetActive(false);
            fab.GetComponentsInChildren<TrailRenderer>()[0].sharedMaterial.SetVector("_EmissionColor", color);
            ReplaceMeshFilter("prefabs/projectileghosts/TarballGhost", "@MoistureUpset_puddi:assets/puddighost.mesh", "@MoistureUpset_puddi:assets/puddi.png");
            fab = Resources.Load<GameObject>("prefabs/characterbodies/ExplosivePotDestructibleBody");
            ReplaceMeshFilter("prefabs/characterbodies/ExplosivePotDestructibleBody", "@MoistureUpset_puddi:assets/puddicontainer.mesh", "@MoistureUpset_puddi:assets/puddicontainer.png");

            fab = Resources.Load<GameObject>("prefabs/effects/ClayBossDeath");
            foreach (var item in fab.GetComponentsInChildren<MeshFilter>())
            {
                item.sharedMesh = Resources.Load<Mesh>("@MoistureUpset_puddi:assets/puddighost.mesh");
                //item.transform.localScale *= 3;
            }
            foreach (var item in fab.GetComponentsInChildren<MeshRenderer>())
            {
                item.sharedMaterial.mainTexture = Resources.Load<Texture>("@MoistureUpset_puddi:assets/puddi.png");
            }
            fab = Resources.Load<GameObject>("prefabs/Pot2Debris");
            foreach (var item in fab.GetComponentsInChildren<MeshFilter>())
            {
                item.transform.localScale *= .5f;
                item.sharedMesh = Resources.Load<Mesh>("@MoistureUpset_puddi:assets/puddighost.mesh");
            }
        }
        private static void StringWorm()
        {
            if (BigJank.getOptionValue("Squirmles") != 1)
                return;
            LoadResource("werm");
            LoadBNK("werm");
            var fab = Resources.Load<GameObject>("prefabs/characterbodies/ElectricWormBody");
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
            ReplaceModel("prefabs/characterbodies/ElectricWormBody", "@MoistureUpset_werm:assets/werm.mesh", "@MoistureUpset_werm:assets/werm.png");
            var mesh = fab.GetComponentInChildren<SkinnedMeshRenderer>();
            var blank = Resources.Load<Texture>("@MoistureUpset_na:assets/blank.png");
            for (int i = 0; i < mesh.sharedMaterials.Length; i++)
            {
                mesh.sharedMaterials[i].SetTexture("_FlowHeightRamp", blank);
                mesh.sharedMaterials[i].SetTexture("_FlowHeightmap", blank);
                mesh.sharedMaterials[i].SetTexture("_FlowTex", blank);
            }



            fab = Resources.Load<GameObject>("prefabs/projectiles/ElectricWormSeekerProjectile");
            var sb = fab.AddComponent<SeekingBullet>();
        }
        private static void Discord()
        {
            if (BigJank.getOptionValue("WanderingAtEveryone") != 1)
                return;
            LoadResource("discord");
            LoadBNK("discord");
            ReplaceModel("prefabs/characterbodies/VagrantBody", "@MoistureUpset_discord:assets/discord.mesh", "@MoistureUpset_discord:assets/discord.png");
            ReplaceModel("prefabs/effects/VagrantDeathExplosion", "@MoistureUpset_discord:assets/limb1.mesh");
            ReplaceModel("prefabs/effects/VagrantDeathExplosion", "@MoistureUpset_discord:assets/limb2.mesh", 1);
            ReplaceModel("prefabs/effects/VagrantDeathExplosion", "@MoistureUpset_discord:assets/limb3.mesh", 2);
            var fab = Resources.Load<GameObject>("prefabs/characterbodies/VagrantBody");
            var mesh = fab.GetComponentInChildren<SkinnedMeshRenderer>();
            Shader s = mesh.sharedMaterial.shader;
            mesh.sharedMaterial = Resources.Load<Material>("@MoistureUpset_discord:assets/discord.mat");
            mesh.sharedMaterial.shader = s;
            foreach (var item in fab.GetComponentsInChildren<RoR2.CharacterModel>())
            {
                item.baseLightInfos[0].defaultColor = new Color(0, 0, 0, 0);
                item.baseLightInfos[0].light.color = new Color(0, 0, 0, 0);
            }

            EntityStates.VagrantMonster.FireMegaNova.novaSoundString = "DiscordExplosion";


            //Play_vagrant_R_explode
            /*
                         ((AK.Wwise.BaseType)fab.GetComponentsInChildren<AkEvent>()[1].data).ObjectReference.SetFieldValue("objectName", "nyan");
            ((AK.Wwise.BaseType)fab.GetComponentsInChildren<AkEvent>()[1].data).ObjectReference.SetFieldValue("id", 1002825203);
             */

            fab = Resources.Load<GameObject>("prefabs/projectileghosts/VagrantCannonGhost");
            var filter = fab.GetComponentsInChildren<MeshFilter>()[0];
            var renderer = fab.GetComponentsInChildren<MeshRenderer>()[0];
            filter.sharedMesh = Resources.Load<Mesh>("@MoistureUpset_discord:assets/discordprojectile.mesh");
            renderer.sharedMaterial = Resources.Load<Material>("@MoistureUpset_discord:assets/discordprojectile.mat");

            fab = Resources.Load<GameObject>("prefabs/projectileghosts/VagrantTrackingBombGhost");
            filter = fab.GetComponentsInChildren<MeshFilter>()[0];
            filter.sharedMesh = Resources.Load<Mesh>("@MoistureUpset_na:assets/na1.mesh");
            filter = fab.GetComponentsInChildren<MeshFilter>()[1];
            filter.sharedMesh = Resources.Load<Mesh>("@MoistureUpset_na:assets/na1.mesh");

            fab.GetComponentsInChildren<ParticleSystemRenderer>()[1].gameObject.SetActive(false);
            var particle = fab.GetComponentsInChildren<ParticleSystemRenderer>()[0];
            try
            {
                particle.material.shader = Shader.Find("Sprites/Default");
                particle.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                particle.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                particle.material.SetInt("_ZWrite", 0);
                particle.material.DisableKeyword("_ALPHATEST_ON");
                particle.material.DisableKeyword("_ALPHABLEND_ON");
                particle.material.EnableKeyword("_ALPHAPREMULTIPLY_ON");
                particle.material.renderQueue = 3000;
                particle.material.mainTexture = Resources.Load<Texture>("@MoistureUpset_discord:assets/ping.png");
            }
            catch (Exception)
            {
            }
        }
        private static void Copter()
        {
            if (BigJank.getOptionValue("Roflcopter") != 1)
                return;
            var fab = Resources.Load<GameObject>("prefabs/characterbodies/LunarWispBody");
            fab.AddComponent<Helicopter>();
            fab.GetComponentsInChildren<ParticleSystem>()[0].maxParticles = 0;
            fab.GetComponentsInChildren<ParticleSystem>()[1].maxParticles = 0;
            fab.GetComponentsInChildren<ParticleSystem>()[2].maxParticles = 0;
            fab.GetComponentsInChildren<ParticleSystem>()[3].maxParticles = 0;
            LoadResource("roflcopter");
            LoadBNK("Roflcopter");
            ReplaceModel("prefabs/characterbodies/LunarWispBody", "@MoistureUpset_roflcopter:assets/roflcopter.mesh", "@MoistureUpset_roflcopter:assets/roflcopter.png");
        }
        private static void Rob()
        {
            if (BigJank.getOptionValue("Rob") != 1)
                return;
            LoadResource("rob");
            ReplaceModel("prefabs/characterbodies/LunarGolemBody", "@MoistureUpset_rob:assets/rob.mesh", "@MoistureUpset_rob:assets/rob.png");
            LoadBNK("rob");
        }
        private static void Nyan()
        {
            if (BigJank.getOptionValue("Nyan Cat") != 1)
                return;
            LoadResource("beetlequeen");
            LoadBNK("Nyam2");
            ReplaceModel("prefabs/characterbodies/BeetleQueen2Body", "@MoistureUpset_beetlequeen:assets/bosses/nyancat.mesh", "@MoistureUpset_na:assets/blank.png");
            var fab = Resources.Load<GameObject>("prefabs/characterbodies/BeetleQueen2Body");
            foreach (var item in fab.GetComponentsInChildren<Light>())
            {
                item.gameObject.SetActive(false);
            }
            fab.GetComponentsInChildren<SkinnedMeshRenderer>()[0].sharedMaterial.SetTexture("_EmTex", Resources.Load<Texture>("@MoistureUpset_beetlequeen:assets/bosses/nyancat.png"));
            fab.GetComponentsInChildren<SkinnedMeshRenderer>()[0].sharedMaterial.SetVector("_EmColor", new Vector4(.4f, .4f, .4f, 1));

            fab = Resources.Load<GameObject>("prefabs/projectileghosts/BeetleQueenSpitGhost");
            fab.GetComponentsInChildren<TrailRenderer>()[1].gameObject.SetActive(false);
            fab.GetComponentsInChildren<TrailRenderer>()[0].material = Resources.Load<Material>("@MoistureUpset_beetlequeen:assets/bosses/trail.mat");
            var p = fab.GetComponentInChildren<ParticleSystem>();
            p.startSpeed = 0;
            p.simulationSpace = ParticleSystemSimulationSpace.Local;
            p.gravityModifier = 0;
            p.maxParticles = 1;
            p.startLifetime = 10;
            var shape = p.shape;
            shape.shapeType = ParticleSystemShapeType.Sprite;

            ((AK.Wwise.BaseType)fab.GetComponentsInChildren<AkEvent>()[1].data).ObjectReference.SetFieldValue("objectName", "nyan");
            ((AK.Wwise.BaseType)fab.GetComponentsInChildren<AkEvent>()[1].data).ObjectReference.SetFieldValue("id", (UInt32)1002825203);


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
                part.material.mainTexture = Resources.Load<Texture>("@MoistureUpset_beetlequeen:assets/bosses/nyanjpeg.png");
            }
            catch (Exception)
            {
            }
            foreach (var particle in EntityStates.BeetleQueenMonster.FireSpit.effectPrefab.GetComponentsInChildren<ParticleSystemRenderer>())
            {
                try
                {
                    particle.material.shader = Shader.Find("Sprites/Default");
                    particle.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                    particle.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                    particle.material.SetInt("_ZWrite", 0);
                    particle.material.DisableKeyword("_ALPHATEST_ON");
                    particle.material.DisableKeyword("_ALPHABLEND_ON");
                    particle.material.EnableKeyword("_ALPHAPREMULTIPLY_ON");
                    particle.material.renderQueue = 3000;
                    particle.material.mainTexture = Resources.Load<Texture>("@MoistureUpset_beetlequeen:assets/bosses/nyanstars.png");
                }
                catch (Exception)
                {
                }
            }


            fab = Resources.Load<GameObject>("Prefabs/Effects/OrbEffects/BeetleWardOrbEffect");
            fab.GetComponentsInChildren<SkinnedMeshRenderer>()[0].sharedMesh = Resources.Load<Mesh>("@MoistureUpset_beetlequeen:assets/bosses/Poptart.mesh");
            fab.GetComponentsInChildren<SkinnedMeshRenderer>()[0].material = Resources.Load<Material>("@MoistureUpset_beetlequeen:assets/bosses/nyancat.mat");
            fab.GetComponentsInChildren<TrailRenderer>()[0].material = Resources.Load<Material>("@MoistureUpset_beetlequeen:assets/bosses/trail.mat");
        }
        private static void Thanos()
        {
            if (BigJank.getOptionValue("Thanos") != 1)
                return;
            LoadResource("thanos");
            LoadBNK("Thanos");
            ReplaceModel("prefabs/characterbodies/BrotherBody", "@MoistureUpset_thanos:assets/bosses/thanos.mesh", "@MoistureUpset_thanos:assets/bosses/thanos.png", 0);
            ReplaceModel("prefabs/characterbodies/BrotherBody", "@MoistureUpset_thanos:assets/bosses/infinityhammer.mesh", "@MoistureUpset_thanos:assets/bosses/thanos.png", 2);
            ReplaceMeshFilter("prefabs/characterbodies/BrotherBody", "@MoistureUpset_na:assets/na1.mesh", 0);
            ReplaceMeshFilter("prefabs/characterbodies/BrotherBody", "@MoistureUpset_na:assets/na1.mesh", 1);
            ReplaceMeshFilter("prefabs/characterbodies/BrotherBody", "@MoistureUpset_na:assets/na1.mesh", 2);
            ReplaceModel("prefabs/characterbodies/BrotherBody", "@MoistureUpset_na:assets/na1.mesh", 1);
            ReplaceModel("prefabs/characterbodies/BrotherBody", "@MoistureUpset_na:assets/na1.mesh", 3);
            ReplaceModel("prefabs/characterbodies/BrotherBody", "@MoistureUpset_na:assets/na1.mesh", 4);

            ReplaceModel("prefabs/characterbodies/BrotherGlassBody", "@MoistureUpset_thanos:assets/bosses/thanos.mesh", "@MoistureUpset_thanos:assets/bosses/thanos.png", 0);
            ReplaceModel("prefabs/characterbodies/BrotherGlassBody", "@MoistureUpset_thanos:assets/bosses/infinityhammer.mesh", "@MoistureUpset_thanos:assets/bosses/thanos.png", 2);
            ReplaceMeshFilter("prefabs/characterbodies/BrotherGlassBody", "@MoistureUpset_na:assets/na1.mesh", 0);
            ReplaceModel("prefabs/characterbodies/BrotherGlassBody", "@MoistureUpset_na:assets/na1.mesh", 1);
            ReplaceModel("prefabs/characterbodies/BrotherGlassBody", "@MoistureUpset_na:assets/na1.mesh", 3);


            ReplaceModel("prefabs/characterbodies/BrotherHurtBody", "@MoistureUpset_thanos:assets/bosses/thanos.mesh", "@MoistureUpset_thanos:assets/bosses/thanos.png", 0);
            ReplaceMeshFilter("prefabs/characterbodies/BrotherHurtBody", "@MoistureUpset_na:assets/na1.mesh", 0);
            ReplaceMeshFilter("prefabs/characterbodies/BrotherHurtBody", "@MoistureUpset_na:assets/na1.mesh", 1);
            ReplaceMeshFilter("prefabs/characterbodies/BrotherHurtBody", "@MoistureUpset_na:assets/na1.mesh", 2);
            ReplaceModel("prefabs/characterbodies/BrotherHurtBody", "@MoistureUpset_na:assets/na1.mesh", 1);
        }
        private static void Cereal()
        {
            if (BigJank.getOptionValue("Cereal") != 1)
                return;
            LoadResource("artifact");
            ReplaceMeshFilter("prefabs/characterbodies/ArtifactShellBody", "@MoistureUpset_artifact:assets/bosses/bowl.mesh");
            var fab = Resources.Load<GameObject>("prefabs/characterbodies/ArtifactShellBody");
            var mesh = fab.GetComponentsInChildren<MeshRenderer>();
            foreach (var item in mesh)
            {
                item.sharedMaterial = Resources.Load<Material>("@MoistureUpset_artifact:assets/bosses/bowl.mat");
            }
            fab = Resources.Load<GameObject>("prefabs/projectiles/ArtifactShellSeekingSolarFlare");
            var skinnedmesh = fab.AddComponent<SkinnedMeshRenderer>() as SkinnedMeshRenderer;
            skinnedmesh.sharedMesh = Resources.Load<Mesh>("@MoistureUpset_artifact:assets/bosses/box.mesh");
            skinnedmesh.sharedMaterial = Resources.Load<Material>("@MoistureUpset_artifact:assets/bosses/box.mat");
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
                    var c = GameObject.FindObjectOfType<MusicController>();
                    var mainBody = NetworkUser.readOnlyLocalPlayersList[0].master?.GetBody();
                    AkSoundEngine.ExecuteActionOnEvent(1462303513, AkActionOnEventType.AkActionOnEventType_Stop);
                    AkSoundEngine.SetRTPCValue("BossMusicActive", 0);
                    AkSoundEngine.PostEvent("StopFanFare", c.gameObject);
                    AkSoundEngine.SetRTPCValue("BossDead", 1f);
                    AkSoundEngine.PostEvent("PlayFanFare", c.gameObject);
                }
                catch (Exception)
                {
                }
                orig(self);
            };
        }
        private static void Twitch()
        {
            if (BigJank.getOptionValue("Twitch") != 1)
                return;
            LoadResource("moisture_twitch");
            LoadResource("twitch2");
            LoadBNK("Twitch");
            ReplaceModel("prefabs/characterbodies/GravekeeperBody", "@MoistureUpset_na:assets/na1.mesh", 1);
            ReplaceModel("prefabs/characterbodies/GravekeeperBody", "@MoistureUpset_moisture_twitch:assets/twitch.mesh", "@MoistureUpset_moisture_twitch:assets/twitch.png", 2);
            ReplaceModel("prefabs/characterbodies/GravekeeperBody", "@MoistureUpset_na:assets/na1.mesh", 3);
            ReplaceModel("prefabs/characterbodies/GravekeeperBody", "@MoistureUpset_na:assets/na1.mesh", 0);
            ReplaceMeshFilter("prefabs/projectileghosts/GravekeeperHookGhost", "@MoistureUpset_moisture_twitch:assets/bosses/GAMER.mesh", "@MoistureUpset_moisture_twitch:assets/twitch.png");
            var fab = Resources.Load<GameObject>("prefabs/projectileghosts/GravekeeperHookGhost");
            fab.GetComponentInChildren<TrailRenderer>().material = Resources.Load<Material>("@MoistureUpset_moisture_twitch:assets/bosses/matt.mat");


            fab = Resources.Load<GameObject>("prefabs/projectileghosts/GravekeeperTrackingFireballGhost");
            fab.GetComponentInChildren<MeshRenderer>().gameObject.SetActive(false);
            fab.AddComponent<Collabs.RandomTwitch>();
            var particle = fab.GetComponentsInChildren<ParticleSystemRenderer>()[0];
            try
            {
                particle.material.shader = Shader.Find("Sprites/Default");
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
            var fab = Resources.Load<GameObject>($"prefabs/characterbodies/{name}");
            Shader temp = fab.GetComponentsInChildren<SkinnedMeshRenderer>()[0].sharedMaterial.shader;
            fab.GetComponentsInChildren<SkinnedMeshRenderer>()[0].sharedMaterial = Resources.Load<Material>("@MoistureUpset_scavenger:assets/bosses/imposter.mat");
            fab.GetComponentsInChildren<SkinnedMeshRenderer>()[2].sharedMaterial = Resources.Load<Material>("@MoistureUpset_scavenger:assets/bosses/imposter.mat");
            fab.GetComponentsInChildren<SkinnedMeshRenderer>()[0].sharedMaterial.shader = temp;
            fab.GetComponentsInChildren<SkinnedMeshRenderer>()[2].sharedMaterial.shader = temp;
        }
        public static bool kindlyKillYourselfRune = true;
        private static void Imposter()
        {
            if (BigJank.getOptionValue("Imposter") != 1)
                return;
            LoadResource("scavenger");
            LoadBNK("Abungus");
            ReplaceModel("prefabs/characterbodies/ScavBody", "@MoistureUpset_scavenger:assets/bosses/Backpack.mesh", "@MoistureUpset_scavenger:assets/bosses/Amongus.png", 0);
            ReplaceModel("prefabs/networkedobjects/ScavBackpack", "@MoistureUpset_scavenger:assets/bosses/Backpackonly.mesh");
            ReplaceModel("prefabs/characterbodies/ScavBody", "@MoistureUpset_na:assets/na1.mesh", 1);
            ReplaceModel("prefabs/characterbodies/ScavBody", "@MoistureUpset_scavenger:assets/bosses/Body.mesh", "@MoistureUpset_scavenger:assets/bosses/Amongus.png", 2);
            ReplaceMeshFilter("prefabs/characterbodies/ScavBody", "@MoistureUpset_scavenger:assets/bosses/gun.mesh");
            ImposterChanger("ScavBody");
            var fab = Resources.Load<GameObject>("prefabs/characterbodies/ScavBody");
            fab.GetComponentInChildren<SkinnedMeshRenderer>().gameObject.AddComponent<AbungusColors>();
            Material mat = Resources.Load<Material>("@MoistureUpset_scavenger:assets/bosses/materials/gun.mat");
            On.RoR2.CharacterModel.UpdateRendererMaterials += (orig, self, r, m, i) =>
            {
                orig(self, r, m, i);
                try
                {
                    if (m.ToString().StartsWith("matScav"))
                    {
                        r.sharedMaterial = self.gameObject.GetComponentInChildren<AbungusColors>().material;
                    }
                    else if (m.ToString() == "matTrimSheetConstructionBlueScavenged (UnityEngine.Material)" || m.ToString() == "matTrimSheetConstructionBlueEmission (UnityEngine.Material)")
                    {
                        r.sharedMaterial = mat;
                    }
                }
                catch (Exception)
                {
                }
            };
            On.EntityStates.ScavMonster.Death.OnEnter += (orig, self) =>
            {
                if (kindlyKillYourselfRune)
                {
                    kindlyKillYourselfRune = false;
                    try
                    {
                        var backpack = Resources.Load<GameObject>("prefabs/networkedobjects/ScavBackpack");
                        GameObject[] objects = GameObject.FindObjectsOfType<GameObject>();
                        GameObject g = self.outer.gameObject;
                        for (int i = 0; i < objects.Length; i++)
                        {
                            if (objects[i] == g)
                            {
                                backpack.GetComponentInChildren<SkinnedMeshRenderer>().sharedMaterial = objects[i - 2].GetComponentInChildren<AbungusColors>().material;
                                break;
                            }
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
                orig(self);
            };

            ReplaceModel("prefabs/characterbodies/ScavLunar1Body", "@MoistureUpset_scavenger:assets/bosses/Backpack.mesh", "@MoistureUpset_scavenger:assets/bosses/AmongusWhite.png", 0);
            ReplaceModel("prefabs/characterbodies/ScavLunar1Body", "@MoistureUpset_na:assets/na1.mesh", 1);
            ReplaceModel("prefabs/characterbodies/ScavLunar1Body", "@MoistureUpset_scavenger:assets/bosses/Body.mesh", "@MoistureUpset_scavenger:assets/bosses/AmongusWhite.png", 2);
            ReplaceMeshFilter("prefabs/characterbodies/ScavLunar1Body", "@MoistureUpset_scavenger:assets/bosses/gun.mesh", 4);
            ImposterChanger("ScavLunar1Body");

            ReplaceModel("prefabs/characterbodies/ScavLunar2Body", "@MoistureUpset_scavenger:assets/bosses/Backpack.mesh", "@MoistureUpset_scavenger:assets/bosses/AmongusWhite.png", 0);
            ReplaceModel("prefabs/characterbodies/ScavLunar2Body", "@MoistureUpset_na:assets/na1.mesh", 1);
            ReplaceModel("prefabs/characterbodies/ScavLunar2Body", "@MoistureUpset_scavenger:assets/bosses/Body.mesh", "@MoistureUpset_scavenger:assets/bosses/AmongusWhite.png", 2);
            ReplaceMeshFilter("prefabs/characterbodies/ScavLunar2Body", "@MoistureUpset_scavenger:assets/bosses/gun.mesh", 4);
            ImposterChanger("ScavLunar2Body");

            ReplaceModel("prefabs/characterbodies/ScavLunar3Body", "@MoistureUpset_scavenger:assets/bosses/Backpack.mesh", "@MoistureUpset_scavenger:assets/bosses/AmongusWhite.png", 0);
            ReplaceModel("prefabs/characterbodies/ScavLunar3Body", "@MoistureUpset_na:assets/na1.mesh", 1);
            ReplaceModel("prefabs/characterbodies/ScavLunar3Body", "@MoistureUpset_scavenger:assets/bosses/Body.mesh", "@MoistureUpset_scavenger:assets/bosses/AmongusWhite.png", 2);
            ReplaceMeshFilter("prefabs/characterbodies/ScavLunar3Body", "@MoistureUpset_scavenger:assets/bosses/gun.mesh", 4);
            ImposterChanger("ScavLunar3Body");

            ReplaceModel("prefabs/characterbodies/ScavLunar4Body", "@MoistureUpset_scavenger:assets/bosses/Backpack.mesh", "@MoistureUpset_scavenger:assets/bosses/AmongusWhite.png", 0);
            ReplaceModel("prefabs/characterbodies/ScavLunar4Body", "@MoistureUpset_na:assets/na1.mesh", 1);
            ReplaceModel("prefabs/characterbodies/ScavLunar4Body", "@MoistureUpset_scavenger:assets/bosses/Body.mesh", "@MoistureUpset_scavenger:assets/bosses/AmongusWhite.png", 2);
            ReplaceMeshFilter("prefabs/characterbodies/ScavLunar4Body", "@MoistureUpset_scavenger:assets/bosses/gun.mesh", 4);
            ImposterChanger("ScavLunar4Body");

            ReplaceModel("prefabs/networkedobjects/ScavLunarBackpack", "@MoistureUpset_scavenger:assets/bosses/Backpackonly.mesh", "@MoistureUpset_scavenger:assets/bosses/AmongusWhite.png");
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
                if (BigJank.getOptionValue("DireSeeker") == 1)
                {
                    Collabs.Direseeker.Run();
                    DebugClass.Log($"Direseeker installed, modifying");
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
                if (BigJank.getOptionValue("Mike Wazowski") == 1)
                {
                    Collabs.PlayableLemurian.Run();
                    DebugClass.Log($"Playable Lemurian installed, modifying");
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
                if (BigJank.getOptionValue("Twitch") == 1)
                {
                    Collabs.m_PlayableGrovetender.Run();
                    DebugClass.Log($"PlayableGrovetender installed, modifying");
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
                if (BigJank.getOptionValue("Imposter") == 1)
                {
                    Collabs.PlayableScavenger.Run();
                    DebugClass.Log($"PlayableScavenger installed, modifying");
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
                if (BigJank.getOptionValue("Heavy") == 1)
                {
                    Collabs.m_PlayableTemplar.Run();
                    DebugClass.Log($"PlayableTemplar installed, modifying");
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
                if (BigJank.getOptionValue("Thanos") == 1)
                {
                    Collabs.PlayableMithrix.Run();
                    DebugClass.Log($"PlayableMithrix installed, modifying");
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
                if (BigJank.getOptionValue("Froggy Chair") == 1)
                {
                    Collabs.PlayableBeetle.Run();
                    DebugClass.Log($"ChipTheBeetle installed, modifying");
                }
            }
            catch (Exception)
            {
                DebugClass.Log($"ChipTheBeetle not installed, skipping");
            }
        }
    }
}