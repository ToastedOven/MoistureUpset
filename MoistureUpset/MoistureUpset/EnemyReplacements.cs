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
            var blank = Resources.Load<Texture>("@MoistureUpset_NA:assets/blank.png");
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
        public static void ReplaceTexture(string prefab, string png, int position = 0)
        {
            var fab = Resources.Load<GameObject>(prefab);
            var meshes = fab.GetComponentsInChildren<SkinnedMeshRenderer>();
            var texture = Resources.Load<Texture>(png);
            var blank = Resources.Load<Texture>("@MoistureUpset_NA:assets/blank.png");
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
            var renderers = fab.GetComponentsInChildren<Renderer>();
            var meshes = fab.GetComponentsInChildren<MeshFilter>();
            var texture = Resources.Load<Texture>(png);
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
        public static void ReplaceMeshRenderer(string prefab, string mesh)
        {
            var fab = Resources.Load<GameObject>(prefab);
            var meshes = fab.GetComponentsInChildren<MeshFilter>();
            meshes[0].sharedMesh = Resources.Load<Mesh>(mesh);
        }
        public static void ReplaceMeshRenderer(GameObject fab, string mesh)
        {
            var meshes = fab.GetComponentsInChildren<MeshFilter>();
            meshes[0].sharedMesh = Resources.Load<Mesh>(mesh);
        }
        public static void ReplaceMeshRenderer(GameObject fab, string mesh, string png)
        {
            var renderers = fab.GetComponentsInChildren<Renderer>();
            var meshes = fab.GetComponentsInChildren<MeshFilter>();
            var texture = Resources.Load<Texture>(png);
            for (int i = 0; i < renderers[0].sharedMaterials.Length; i++)
            {
                renderers[0].sharedMaterials[i].color = Color.white;
                renderers[0].sharedMaterials[i].mainTexture = texture;
                renderers[0].sharedMaterials[i].SetTexture("_EmTex", texture);
                renderers[0].sharedMaterials[i].SetTexture("_NormalTex", null);
            }
            meshes[0].sharedMesh = Resources.Load<Mesh>(mesh);
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
        public static void LoadResource(string resource)
        {
            Debug.Log($"Loading {resource}");
            using (var assetStream = Assembly.GetExecutingAssembly().GetManifestResourceStream($"MoistureUpset.Models.{resource}"))
            {
                var MainAssetBundle = AssetBundle.LoadFromStream(assetStream);

                ResourcesAPI.AddProvider(new AssetBundleResourcesProvider($"@MoistureUpset_{resource}", MainAssetBundle));
            }
        }
        private static void LoadBNK(string bnk)
        {
            string s = $"MoistureUpset.bankfiles.{bnk}.bnk";
            Debug.Log(s);
            using (var bankStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(s))
            {
                var bytes = new byte[bankStream.Length];
                bankStream.Read(bytes, 0, bytes.Length);

                SoundBanks.Add(bytes);
            }
        }
        public static void RunAll()
        {
            try
            {
                RobloxTitan();
                Alex();
                ElderLemurian();
                Lemurian();
                DEBUG();
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
                //SneakyFontReplacement();
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }
        }
        public static void DEBUG()
        {
            var fab = Resources.Load<GameObject>("prefabs/networkedobjects/SurvivorPod");
            var renderers = fab.GetComponentsInChildren<Renderer>();
            var meshes = fab.GetComponentsInChildren<MeshFilter>();
            //DebugClass.DebugBones(fab);
            //renderers[0].material = Resources.Load<Material>("@MoistureUpset_droppod:assets/shrekpodmat.mat");
            //meshes[0].mesh = Resources.Load<Mesh>("@MoistureUpset_test:assets/door.mesh");
            //renderers[1].material = Resources.Load<Material>("@MoistureUpset_droppod:assets/shrekpodmat.mat");
            //meshes[1].mesh = Resources.Load<Mesh>("@MoistureUpset_test:assets/pod.mesh");

        }
        private static void Icons()
        {
            if (float.Parse(ModSettingsManager.getOptionValue("Froggy Chair")) == 1)
                UImods.ReplaceTexture2D("textures/bodyicons/BeetleBody", "MoistureUpset.Resources.froggychair.png");
            if (float.Parse(ModSettingsManager.getOptionValue("Winston")) == 1)
                UImods.ReplaceTexture2D("textures/bodyicons/BeetleGuardBody", "MoistureUpset.Resources.winston.png");
            if (float.Parse(ModSettingsManager.getOptionValue("Winston")) == 1)
                UImods.ReplaceTexture2D("textures/bodyicons/BeetleGuardAllyBody", "MoistureUpset.Resources.winston.png");
            if (float.Parse(ModSettingsManager.getOptionValue("Taco Bell")) == 1)
                UImods.ReplaceTexture2D("textures/bodyicons/BellBody", "MoistureUpset.Resources.tacobell.png");
            if (float.Parse(ModSettingsManager.getOptionValue("Thomas")) == 1)
                UImods.ReplaceTexture2D("textures/bodyicons/BisonBody", "MoistureUpset.Resources.thomas.png");
            if (float.Parse(ModSettingsManager.getOptionValue("Heavy")) == 1)
                UImods.ReplaceTexture2D("textures/bodyicons/ClayBruiserBody", "MoistureUpset.Resources.heavy.png");
            if (float.Parse(ModSettingsManager.getOptionValue("Robloxian")) == 1)
                UImods.ReplaceTexture2D("textures/bodyicons/GolemBody", "MoistureUpset.Resources.oof.png");
            if (float.Parse(ModSettingsManager.getOptionValue("Ghast")) == 1)
                UImods.ReplaceTexture2D("textures/bodyicons/GreaterWispBody", "MoistureUpset.Resources.ghast.png");
            if (float.Parse(ModSettingsManager.getOptionValue("Trumpet Skeleton")) == 1)
                UImods.ReplaceTexture2D("textures/bodyicons/ImpBody", "MoistureUpset.Resources.doot.png");
            if (float.Parse(ModSettingsManager.getOptionValue("Sans")) == 1)
                UImods.ReplaceTexture2D("textures/bodyicons/ImpBossBody", "MoistureUpset.Resources.sans.png");
            if (float.Parse(ModSettingsManager.getOptionValue("Comedy")) == 1)
                UImods.ReplaceTexture2D("textures/bodyicons/JellyfishBody", "MoistureUpset.Resources.joy.png");
            if (float.Parse(ModSettingsManager.getOptionValue("Mike Wazowski")) == 1)
                UImods.ReplaceTexture2D("textures/bodyicons/LemurianBody", "MoistureUpset.Resources.mike.png");
            if (float.Parse(ModSettingsManager.getOptionValue("Bowser")) == 1)
                UImods.ReplaceTexture2D("textures/bodyicons/LemurianBruiserBody", "MoistureUpset.Resources.bowser.png");
            if (float.Parse(ModSettingsManager.getOptionValue("Obama Prism")) == 1)
                UImods.ReplaceTexture2D("textures/bodyicons/RoboBallBossBody", "MoistureUpset.Resources.obamasphere.png");
            if (float.Parse(ModSettingsManager.getOptionValue("Obama Prism")) == 1)
                UImods.ReplaceTexture2D("textures/bodyicons/RoboBallMiniBody", "MoistureUpset.Resources.obamaprism.png");
            if (float.Parse(ModSettingsManager.getOptionValue("Obama prism")) == 1)
                UImods.ReplaceTexture2D("textures/bodyicons/SuperRoboBallBossBody", "MoistureUpset.Resources.obamasphere.png");
            if (float.Parse(ModSettingsManager.getOptionValue("Dogplane")) == 1)
                UImods.ReplaceTexture2D("textures/bodyicons/WispBody", "MoistureUpset.Resources.dogplane.png");
            if (float.Parse(ModSettingsManager.getOptionValue("Toad")) == 1)
                UImods.ReplaceTexture2D("textures/bodyicons/MiniMushroomBody", "MoistureUpset.Resources.toad.png");
            if (float.Parse(ModSettingsManager.getOptionValue("Alex Jones")) == 1)
                UImods.ReplaceTexture2D("textures/bodyicons/TitanGoldBody", "MoistureUpset.Resources.alexjones.png");
            if (float.Parse(ModSettingsManager.getOptionValue("Hagrid")) == 1)
                UImods.ReplaceTexture2D("textures/bodyicons/ParentBody", "MoistureUpset.Resources.hagrid.png");
            if (float.Parse(ModSettingsManager.getOptionValue("Roblox Titan")) == 1)
                UImods.ReplaceTexture2D("textures/bodyicons/TitanBody", "MoistureUpset.Resources.buffroblox.png");
            if (float.Parse(ModSettingsManager.getOptionValue("Lemme Smash")) == 1)
                UImods.ReplaceTexture2D("textures/bodyicons/VultureBody", "MoistureUpset.Resources.lemmesmash.png");
            //UImods.ReplaceUIBetter("textures/bodyicons/BeetleBody", "MoistureUpset.Resources.froggychair.png");
        }
        private static void NonEnemyNames()
        {
            On.RoR2.Language.SetStringByToken += (orig, self, token, st) =>
            {
                if (float.Parse(ModSettingsManager.getOptionValue("NSFW")) == 1 && float.Parse(ModSettingsManager.getOptionValue("Difficulty Names")) == 1)
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
                if (float.Parse(ModSettingsManager.getOptionValue("In-Run Difficulty Names")) == 1)
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
                if (float.Parse(ModSettingsManager.getOptionValue("Shrine Changes")) == 1)
                {
                    if (token == "SHRINE_BLOOD_CONTEXT")
                    {
                        st = "Free Money";
                    }
                    else if (token == "SHRINE_BLOOD_USE_MESSAGE_2P")
                    {
                        st = "<style=cShrine>Wait it's not free. You have gained {1} gold.</color>";
                    }
                    else if (token == "SHRINE_BLOOD_USE_MESSAGE")
                    {
                        st = "<style=cShrine>{0} got stabbed for money, and has gained {1} gold.</color>";
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
                    }
                    else if (token == "SHRINE_CHANCE_FAIL_MESSAGE")
                    {
                        st = "<style=cShrine>{0} lost money.</color>";
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
                if (float.Parse(ModSettingsManager.getOptionValue("Misc")) == 1)
                {
                    if (token == "PLAYER_PING_COOLDOWN")
                    {
                        st = "<style=cEvent>Stop</style>";
                    }
                }
                orig(self, token, st);
            };
        }
        private static void Shrines()
        {
            if (float.Parse(ModSettingsManager.getOptionValue("Shrine Changes")) != 1)
                return;
            On.RoR2.ShrineChanceBehavior.AddShrineStack += (orig, self, activator) =>
            {
                float yes = self.GetFieldValue<int>("successfulPurchaseCount");
                orig(self, activator);
                if (self.GetFieldValue<int>("successfulPurchaseCount") == yes)
                {
                    NetworkAssistant.playSound("ChanceFailure", activator.gameObject.transform.position);
                    //AkSoundEngine.PostEvent("ChanceFailure", mainBody.gameObject);
                }
                else
                {
                    NetworkAssistant.playSound("ChanceSuccess", activator.gameObject.transform.position);
                    //AkSoundEngine.PostEvent("ChanceSuccess", mainBody.gameObject);
                }
            };
        }
        private static void Names()
        {
            On.RoR2.Language.SetStringByToken += (orig, self, token, st) =>
            {
                if (st.Contains("Imp Overlord"))
                {
                    if (float.Parse(ModSettingsManager.getOptionValue("Sans")) == 1)
                        st = st.Replace("Imp Overlord", "Sans");
                }
                else if (st == "Lord of the Red Plane")
                {
                    if (float.Parse(ModSettingsManager.getOptionValue("Sans")) == 1)
                        st = "You're gonna have a bad time";
                }
                else if (st.Contains("Imp") && !st.Contains("Overlord") && !st.Contains("Impossible") && !st.Contains("Important") && !st.Contains("Improves"))
                {
                    if (float.Parse(ModSettingsManager.getOptionValue("Trumpet Skeleton")) == 1)
                        st = st.Replace("Imp", "Trumpet Skeleton");
                }
                else if (st.Contains("Lesser Wisp"))
                {
                    if (float.Parse(ModSettingsManager.getOptionValue("Dogplane")) == 1)
                        st = st.Replace("Lesser Wisp", "Dogplane");
                }
                else if (st.Contains("Jellyfish"))
                {
                    if (float.Parse(ModSettingsManager.getOptionValue("Comedy")) == 1)
                        st = st.Replace("Jellyfish", "Comedy");
                }


                else if (st.Contains("Beetle Guard"))
                {
                    if (float.Parse(ModSettingsManager.getOptionValue("Winston")) == 1)
                        st = st.Replace("Beetle Guard", "Winston");
                }
                else if (st.Contains("Beetle") && !st.Contains("Queen") && !st.Contains("Guard"))
                {
                    if (float.Parse(ModSettingsManager.getOptionValue("Froggy Chair")) == 1)
                        st = st.Replace("Beetle", "Froggy Chair");
                }


                else if (st.Contains("Elder Lemurian"))
                {
                    if (float.Parse(ModSettingsManager.getOptionValue("Bowser")) == 1)
                        st = st.Replace("Elder Lemurian", "Bowser");
                }
                else if (st.Contains("Lemurian") && !st.Contains("Elder"))
                {
                    if (float.Parse(ModSettingsManager.getOptionValue("Mike Wazowski")) == 1)
                        st = st.Replace("Lemurian", "Mike Wazowski");
                }
                else if (st.Contains("Solus Probe"))
                {
                    if (float.Parse(ModSettingsManager.getOptionValue("Obama Prism")) == 1)
                        st = st.Replace("Solus Probe", "Obama Prism");
                }
                else if (st.Contains("Brass Contraption"))
                {
                    if (float.Parse(ModSettingsManager.getOptionValue("Taco Bell")) == 1)
                        st = st.Replace("Brass Contraption", "Taco Bell");
                }
                else if (st.Contains("Bighorn Bison"))
                {
                    if (float.Parse(ModSettingsManager.getOptionValue("Thomas")) == 1)
                        st = st.Replace("Bighorn Bison", "Thomas");
                }
                else if (st.Contains("Stone Golem"))
                {
                    if (float.Parse(ModSettingsManager.getOptionValue("Robloxian")) == 1)
                        st = st.Replace("Stone Golem", "Robloxian");
                }
                else if (st.Contains("Clay Templar"))
                {
                    if (float.Parse(ModSettingsManager.getOptionValue("Heavy")) == 1)
                        st = st.Replace("Clay Templar", "Heavy");
                }
                else if (st.Contains("Greater Wisp"))
                {
                    if (float.Parse(ModSettingsManager.getOptionValue("Ghast")) == 1)
                        st = st.Replace("Greater Wisp", "Ghast");
                }
                else if (st.Contains("Solus Control Unit"))
                {
                    if (float.Parse(ModSettingsManager.getOptionValue("Obama Prism")) == 1)
                        st = st.Replace("Solus Control Unit", "Obama Sphere");
                }
                else if (st == "Corrupted AI")
                {
                    if (float.Parse(ModSettingsManager.getOptionValue("Obama Prism")) == 1)
                        st = "Bringer of the Prisms";
                }
                else if (st == "Friend of Vultures")
                {
                    if (float.Parse(ModSettingsManager.getOptionValue("Obama Prism")) == 1)
                        st = "Friend of Prisms";
                }
                else if (st.Contains("Alloy Worship Unit"))
                {
                    if (float.Parse(ModSettingsManager.getOptionValue("Obama Prism")) == 1)
                        st = st.Replace("Alloy Worship Unit", "Obamium Worship Unit");
                }
                else if (st.Contains("Mini Mushrum"))
                {
                    if (float.Parse(ModSettingsManager.getOptionValue("Toad")) == 1)
                        st = st.Replace("Mini Mushrum", "Toad");
                }
                else if (st.Contains("Aurelionite"))
                {
                    if (float.Parse(ModSettingsManager.getOptionValue("Alex Jones")) == 1)
                        st = st.Replace("Aurelionite", "Alex Jones");
                }
                else if (token == "TITANGOLD_BODY_SUBTITLE")
                {
                    if (float.Parse(ModSettingsManager.getOptionValue("Alex Jones")) == 1)
                        st = "Prince of the Social Media Shadow Realm";
                }
                else if (st.Contains("Stone Titan"))
                {
                    if (float.Parse(ModSettingsManager.getOptionValue("Roblox Titan")) == 1)
                        st = st.Replace("Stone Titan", "Buff Robloxian");
                }
                else if (token == "TITAN_BODY_SUBTITLE")
                {
                    if (float.Parse(ModSettingsManager.getOptionValue("Roblox Titan")) == 1)
                        st = "Oooooooooooooooooooooooooooof";
                }


                else if (st.Contains("Parent"))
                {
                    if (float.Parse(ModSettingsManager.getOptionValue("Hagrid")) == 1)
                        st = st.Replace("Parent", "Hagrid");
                }


                else if (st.Contains("Alloy Vulture"))
                {
                    if (float.Parse(ModSettingsManager.getOptionValue("Lemme Smash")) == 1)
                        st = st.Replace("Alloy Vulture", "Ron");
                }
                //else if (st.Contains("Jellyfish"))
                //{
                //    st = st.Replace("Jellyfish", "Comedy");
                //}
                orig(self, token, st);
            };
        }
        private static void _UI()
        {
            if (float.Parse(ModSettingsManager.getOptionValue("Awp UI")) == 1)
                LoadBNK("awp");
            if (float.Parse(ModSettingsManager.getOptionValue("Chest noises")) == 1)
                LoadBNK("chestinteraction");
            if (float.Parse(ModSettingsManager.getOptionValue("Player death sound")) == 1)
                LoadBNK("playerdeath");
        }
        private static void Sans()
        {
            if (float.Parse(ModSettingsManager.getOptionValue("Sans")) != 1)
                return;
            LoadBNK("sans");
            LoadResource("sans");
            ReplaceModel("prefabs/characterbodies/ImpBossBody", "@MoistureUpset_sans:assets/sans.mesh", "@MoistureUpset_sans:assets/sans.png");
            ReplaceMeshFilter("prefabs/projectileghosts/ImpVoidspikeProjectileGhost", "@MoistureUpset_sans:assets/boner.mesh", "@MoistureUpset_sans:assets/boner.png");
        }
        private static void Shop()
        {
            if (float.Parse(ModSettingsManager.getOptionValue("Merchant")) != 1)
                return;
            LoadResource("shop");
            ReplaceModel("prefabs/characterbodies/ShopkeeperBody", "@MoistureUpset_shop:assets/shop.mesh", "@MoistureUpset_shop:assets/shop.png");
        }
        private static void BeetleGuard()
        {
            if (float.Parse(ModSettingsManager.getOptionValue("Winston")) != 1)
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
            ReplaceModel("prefabs/characterbodies/BeetleGuardAllyBody", "@MoistureUpset_winston:assets/winston.mesh", "@MoistureUpset_winston:assets/winston.png");
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
            if (float.Parse(ModSettingsManager.getOptionValue("Comedy")) != 1)
                return;
            LoadBNK("comedy");
            LoadResource("jelly");
            ReplaceModel("prefabs/characterbodies/JellyfishBody", "@MoistureUpset_jelly:assets/jelly.mesh", "@MoistureUpset_jelly:assets/jelly.png");
            On.EntityStates.JellyfishMonster.JellyNova.Detonate += (orig, self) =>
            {
                AkSoundEngine.PostEvent("JellyDetonate", self.outer.gameObject);
                orig(self);
            };
        }
        private static void TacoBell()
        {
            if (float.Parse(ModSettingsManager.getOptionValue("Taco Bell")) != 1)
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
        //private static void ReplaceFont(string ogFont, string newFont)
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
            if (float.Parse(ModSettingsManager.getOptionValue("Toad")) != 1)
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
            if (float.Parse(ModSettingsManager.getOptionValue("Trumpet Skeleton")) != 1)
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
            Debug.Log($"{sb.ToString()}=={depth}======{t.name}");
            for (int i = 0; i < t.childCount; i++)
            {
                GetChildren(t.GetChild(i), ref l, depth + 1);
            }
        }
        private static void Beetle()
        {
            if (float.Parse(ModSettingsManager.getOptionValue("Froggy Chair")) != 1)
                return;
            LoadBNK("beetle");
            LoadResource("frog");
            var fab = Resources.Load<GameObject>("prefabs/characterbodies/BeetleBody");
            List<Transform> t = new List<Transform>();
            //this is the fucking stupid but it works (minus claws)
            foreach (var item in fab.GetComponentsInChildren<Transform>())
            {
                if (!item.name.Contains("Hurtbox") && !item.name.Contains("BeetleBody") && !item.name.Contains("Mesh") && !item.name.Contains("mdl"))
                {
                    t.Add(item);
                }
            }
            foreach (var item in fab.GetComponentsInChildren<SkinnedMeshRenderer>())
            {
                item.bones = t.ToArray();
            }
            ReplaceModel("prefabs/characterbodies/BeetleBody", "@MoistureUpset_frog:assets/frogchair.mesh", "@MoistureUpset_frog:assets/frogchair.png");
        }
        private static void ElderLemurian()
        {
            if (float.Parse(ModSettingsManager.getOptionValue("Bowser")) != 1)
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
            if (float.Parse(ModSettingsManager.getOptionValue("Heavy")) != 1)
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
                    meshes[i].sharedMesh = Resources.Load<Mesh>("@MoistureUpset_NA:assets/na.mesh");
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
            if (float.Parse(ModSettingsManager.getOptionValue("Ghast")) != 1)
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
            if (float.Parse(ModSettingsManager.getOptionValue("Dogplane")) != 1)
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
            if (float.Parse(ModSettingsManager.getOptionValue("Obama Prism")) != 1)
                return;
            LoadBNK("prism");
            LoadResource("obamaprism");
            ReplaceModel("prefabs/characterbodies/RoboBallMiniBody", "@MoistureUpset_obamaprism:assets/Obamium.mesh", "@MoistureUpset_obamaprism:assets/Obruhma.png");
            ReplaceModel("prefabs/characterbodies/RoboBallBossBody", "@MoistureUpset_obamaprism:assets/obamasphere.mesh", "@MoistureUpset_obamaprism:assets/Obruhma.png");
            ReplaceModel("prefabs/characterbodies/SuperRoboBallBossBody", "@MoistureUpset_obamaprism:assets/obamasphere.mesh", "@MoistureUpset_obamaprism:assets/Obruhma.png");
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
            if (float.Parse(ModSettingsManager.getOptionValue("Mike Wazowski")) != 1)
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
            if (float.Parse(ModSettingsManager.getOptionValue("Robloxian")) != 1)
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
                    Debug.Log(e);
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
                    Debug.Log(e);
                }
                orig(self);
            };
            On.EntityStates.GolemMonster.ClapState.OnEnter += (orig, self) =>
            {
                EntityStates.GolemMonster.ClapState.attackSoundString = "GolemMelee";
                orig(self);
            };
            ReplaceModel("prefabs/characterbodies/GolemBody", "@MoistureUpset_noob:assets/N00b.mesh", "@MoistureUpset_noob:assets/Noob1Tex.png");
        }
        private static void Bison()
        {
            if (float.Parse(ModSettingsManager.getOptionValue("Thomas")) != 1)
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
            if (float.Parse(ModSettingsManager.getOptionValue("Roblox Titan")) != 1)
                return;
            LoadResource("roblox");
            ReplaceModel("prefabs/characterbodies/TitanBody", "@MoistureUpset_roblox:assets/robloxtitan.mesh", "@MoistureUpset_roblox:assets/robloxtitan.png");
            var fab = Resources.Load<GameObject>("prefabs/characterbodies/TitanBody");
            try
            {
                Texture t = Resources.Load<Texture>("@MoistureUpset_NA:assets/solid.png");
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
                    //Debug.Log("safawpefo======== " +meshes[i].sharedMesh/*.sharedMesh*/);
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
                Debug.Log(e);
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
                            Debug.Log(e);
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
            if (float.Parse(ModSettingsManager.getOptionValue("Alex Jones")) != 1)
                return;
            LoadResource("alexjones");
            ReplaceModel("prefabs/characterbodies/TitanGoldBody", "@MoistureUpset_alexjones:assets/alexjones.mesh", "@MoistureUpset_alexjones:assets/alexjones.png");
            var fab = Resources.Load<GameObject>("prefabs/characterbodies/TitanGoldBody");
            try
            {
                Texture t = Resources.Load<Texture>("@MoistureUpset_NA:assets/solid.png");
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
                Debug.Log(e);
            }
            On.EntityStates.TitanMonster.FireFist.PlaceSingleDelayBlast += (orig, self, position, delay) =>
            {
                orig(self, position, delay);
                if (self.outer.gameObject.name.Contains("Gold"))
                {
                    GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(Resources.Load<GameObject>("Prefabs/NetworkedObjects/GenericDelayBlast"), position, Quaternion.identity);
                    AkSoundEngine.PostEvent("AlexFistDelayed", gameObject);
                    GameObject.Destroy(gameObject);
                }
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
                    Debug.Log($"----{self.outer.commonComponents.teamComponent.teamIndex}");
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
            if (float.Parse(ModSettingsManager.getOptionValue("Lemme Smash")) != 1)
                return;
            LoadBNK("vulture");
            LoadResource("lemmesmash");
            ReplaceModel("prefabs/characterbodies/VultureBody", "@MoistureUpset_lemmesmash:assets/lemmesmasheyes.mesh", "@MoistureUpset_NA:assets/blank.png", 1);
            ReplaceModel("prefabs/characterbodies/VultureBody", "@MoistureUpset_lemmesmash:assets/kevinishomosex/vulturemesh.mesh", "@MoistureUpset_lemmesmash:assets/lemmesmash.png", 2);
            //I know this is shitty but it works and at this point im too scared to change it
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
            if (float.Parse(ModSettingsManager.getOptionValue("Hagrid")) != 1)
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
        private static void BlueParticles(string path)
        {
            var fab = Resources.Load<GameObject>(path);
            foreach (var item in fab.GetComponentsInChildren<ParticleSystem>())
            {
                foreach (var thing in item.gameObject.GetComponentsInChildren<ParticleSystemRenderer>())
                {
                    thing.sharedMaterial.SetVector("_TintColor", new Vector4(0, .47f, .75f, 1));
                    thing.sharedMaterial.SetVector("_EmissionColor", new Vector4(0, .47f, .75f, 1));
                }
            }
        }
        private static void Noodle()
        {
            if (float.Parse(ModSettingsManager.getOptionValue("Pool Noodle")) != 1)
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
            var blank = Resources.Load<Texture>("@MoistureUpset_NA:assets/blank.png");
            for (int i = 0; i < mesh.sharedMaterials.Length; i++)
            {
                mesh.sharedMaterials[i].SetTexture("_FlowHeightRamp", blank);
                mesh.sharedMaterials[i].SetTexture("_FlowHeightmap", blank);
                mesh.sharedMaterials[i].SetTexture("_FlowTex", blank);
            }
            foreach (var item in fab.GetComponentsInChildren<UnityEngine.Rendering.PostProcessing.PostProcessVolume>())
            {
                Debug.Log($"-------------{item.blendDistance = 0}");
                Debug.Log($"-------------{item.profile}");
            }
            BlueParticles("prefabs/characterbodies/MagmaWormBody");
            BlueParticles("prefabs/effects/MagmaWormBurrow");
            BlueParticles("prefabs/effects/MagmaWormDeath");
            BlueParticles("prefabs/effects/MagmaWormDeathDust");
            BlueParticles("prefabs/effects/MagmaWormImpactExplosion");
            BlueParticles("prefabs/effects/MagmaWormRupture");
            BlueParticles("prefabs/effects/MagmaWormWarning");
        }
        private static void Skeleton()
        {
            if (float.Parse(ModSettingsManager.getOptionValue("Skeleton Crab")) != 1)
                return;
            LoadResource("skeleton");
            //ReplaceTexture("prefabs/characterbodies/HermitCrabBody", "@MoistureUpset_skeleton:assets/skeleton.png");
            ReplaceModel("prefabs/characterbodies/HermitCrabBody", "@MoistureUpset_skeleton:assets/skeleton.mesh", "@MoistureUpset_skeleton:assets/skeleton.png");
            var fab = Resources.Load<GameObject>("prefabs/characterbodies/HermitCrabBody");
            var blank = Resources.Load<Texture>("@MoistureUpset_NA:assets/blank.png");
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
        }
        private static void CrabRave()
        {
            if (float.Parse(ModSettingsManager.getOptionValue("Crab Rave")) != 1)
                return;
            LoadResource("crabrave");
            //ReplaceModel("prefabs/characterbodies/NullifierBody", "@MoistureUpset_crabrave:assets/crabrave.mesh", "@MoistureUpset_crabrave:assets/crabrave.png");
            ReplaceModel("prefabs/characterbodies/NullifierBody", "@MoistureUpset_crabrave:assets/kevinishomosex/nullifiermesh.mesh", 1);
        }
    }
}