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

namespace MoistureUpset
{
    public static class EnemyReplacements
    {
        private static void ReplaceModel(string prefab, string mesh, string png, int position = 0, bool replaceothers = false)
        {
            var fab = Resources.Load<GameObject>(prefab);
            var meshes = fab.GetComponentsInChildren<SkinnedMeshRenderer>();
            meshes[position].sharedMesh = Resources.Load<Mesh>(mesh);
            var texture = Resources.Load<Texture>(png);
            for (int i = 0; i < meshes[position].sharedMaterials.Length; i++)
            {
                meshes[position].sharedMaterials[i].shader = Shader.Find("Standard");
                meshes[position].sharedMaterials[i].color = Color.white;
                meshes[position].sharedMaterials[i].mainTexture = texture;
                meshes[position].sharedMaterials[i].SetTexture("_EmTex", texture);
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
        private static void ReplaceMeshRenderer(string prefab, string mesh, string png)
        {
            var fab = Resources.Load<GameObject>(prefab);
            var renderers = fab.GetComponentsInChildren<Renderer>();
            var meshes = fab.GetComponentsInChildren<MeshFilter>();
            var texture = Resources.Load<Texture>(png);
            for (int i = 0; i < renderers[0].sharedMaterials.Length; i++)
            {
                renderers[0].sharedMaterials[i].shader = Shader.Find("Standard");
                renderers[0].sharedMaterials[i].color = Color.white;
                renderers[0].sharedMaterials[i].mainTexture = texture;
                renderers[0].sharedMaterials[i].SetTexture("_EmTex", texture);
                renderers[0].sharedMaterials[i].SetTexture("_NormalTex", null);
            }
            meshes[0].sharedMesh = Resources.Load<Mesh>(mesh);
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
        private static void ReplaceModel(string prefab, string mesh, int position = 0)
        {
            var fab = Resources.Load<GameObject>(prefab);
            var meshes = fab.GetComponentsInChildren<SkinnedMeshRenderer>();
            meshes[position].sharedMesh = Resources.Load<Mesh>(mesh);
        }
        private static void ReplaceModel(GameObject fab, string mesh, int position = 0)
        {
            var meshes = fab.GetComponentsInChildren<SkinnedMeshRenderer>();
            meshes[position].sharedMesh = Resources.Load<Mesh>(mesh);
        }
        private static void LoadResource(string resource)
        {
            using (var assetStream = Assembly.GetExecutingAssembly().GetManifestResourceStream($"MoistureUpset.{resource}"))
            {
                var MainAssetBundle = AssetBundle.LoadFromStream(assetStream);

                ResourcesAPI.AddProvider(new AssetBundleResourcesProvider($"@MoistureUpset_{resource}", MainAssetBundle));
            }
        }
        public static void RunAll()
        {
            try
            {
                Lemurian();
                ElderLemurian();
                DEBUG();
                Golem();
                Bison();
                SolusUnit();
                Templar();
                Wisp();
                GreaterWisp();
                Imp();
                MiniMushroom();
                Beetle();
                TacoBell();
                Jelly();
                BeetleGuard();
                Shop();
                Sans();
                //SneakyFontReplacement();
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }
        }
        public static void DEBUG()
        {

            //ReplaceModel("prefabs/characterbodies/JellyfishBody", "@MoistureUpset_test:assets/kevinishomosex/JellyfishMesh.mesh");
            //ReplaceModel("prefabs/characterbodies/HermitCrabBody", "@MoistureUpset_test:assets/kevinishomosex/HermitCrabMesh.mesh");
            //ReplaceModel("prefabs/characterbodies/BellBody", "@MoistureUpset_test:assets/kevinishomosex/BellMesh.mesh");
            //ReplaceModel("prefabs/characterbodies/BeetleGuardBody", "@MoistureUpset_test:assets/kevinishomosex/BeetleGuardMesh.mesh");
            //ReplaceModel("prefabs/characterbodies/BeetleGuardAllyBody", "@MoistureUpset_test:assets/kevinishomosex/BeetleGuardMesh.mesh");
            //ReplaceModel("prefabs/characterbodies/VultureBody", "@MoistureUpset_test:assets/kevinishomosex/VultureMesh.mesh");
            //ReplaceModel("prefabs/characterbodies/ParentBody", "@MoistureUpset_test:assets/kevinishomosex/ParentMesh.mesh");
            //ReplaceModel("prefabs/characterbodies/BeetleQueen2Body", "@MoistureUpset_test:assets/kevinishomosex/BeetleQueenMesh.mesh");
            //ReplaceModel("prefabs/characterbodies/ClayBossBody", "@MoistureUpset_test:assets/kevinishomosex/ClayBossMesh.mesh");
            //ReplaceModel("prefabs/characterbodies/TitanBody", "@MoistureUpset_test:assets/kevinishomosex/Titan.mesh");
            //ReplaceModel("prefabs/characterbodies/TitanGoldBody", "@MoistureUpset_test:assets/kevinishomosex/GoldTitan.mesh");
            //ReplaceModel("prefabs/characterbodies/ShopkeeperBody", "@MoistureUpset_test:assets/kevinishomosex/NewtMesh.mesh");
            //ReplaceModel("prefabs/characterbodies/VagrantBody", "@MoistureUpset_test:assets/kevinishomosex/VagrantMesh.mesh");
            //ReplaceModel("prefabs/characterbodies/MagmaWormBody", "@MoistureUpset_test:assets/kevinishomosex/WormMesh.mesh", 1);
            //ReplaceModel("prefabs/characterbodies/ElectricWormBody", "@MoistureUpset_test:assets/kevinishomosex/WormMesh.mesh", 1);
            //ReplaceModel("prefabs/characterbodies/ImpBossBody", "@MoistureUpset_test:assets/kevinishomosex/ImpBossMesh.mesh");
            //ReplaceModel("prefabs/characterbodies/MiniMushroomBody", "@MoistureUpset_toad:assets/toad.mesh");

            //DebugClass.DebugBones("prefabs/characterbodies/JellyfishBody");
            //DebugClass.DebugBones("prefabs/characterbodies/HermitCrabBody");
            //DebugClass.DebugBones("prefabs/characterbodies/BellBody");
            //DebugClass.DebugBones("prefabs/characterbodies/BeetleGuardBody");
            //DebugClass.DebugBones("prefabs/characterbodies/BeetleGuardAllyBody");
            //DebugClass.DebugBones("prefabs/characterbodies/VultureBody");
            //DebugClass.DebugBones("prefabs/characterbodies/ParentBody");
            //DebugClass.DebugBones("prefabs/characterbodies/ParentPodBody");
            //DebugClass.DebugBones("prefabs/characterbodies/BeetleQueen2Body");
            //DebugClass.DebugBones("prefabs/characterbodies/ClayBossBody");
            //DebugClass.DebugBones("prefabs/characterbodies/TitanBody");
            //DebugClass.DebugBones("prefabs/characterbodies/TitanGoldBody");
            //DebugClass.DebugBones("prefabs/characterbodies/ShopkeeperBody");
            //DebugClass.DebugBones("prefabs/characterbodies/VagrantBody");
            //DebugClass.DebugBones("prefabs/characterbodies/MagmaWormBody");
            //DebugClass.DebugBones("prefabs/characterbodies/ElectricWormBody");
            //DebugClass.DebugBones("prefabs/characterbodies/ImpBossBody");

            var fab = Resources.Load<GameObject>("prefabs/networkedobjects/SurvivorPod");
            var renderers = fab.GetComponentsInChildren<Renderer>();
            var meshes = fab.GetComponentsInChildren<MeshFilter>();
            //DebugClass.DebugBones(fab);
            //renderers[0].material = Resources.Load<Material>("@MoistureUpset_droppod:assets/shrekpodmat.mat");
            //meshes[0].mesh = Resources.Load<Mesh>("@MoistureUpset_test:assets/door.mesh");
            //renderers[1].material = Resources.Load<Material>("@MoistureUpset_droppod:assets/shrekpodmat.mat");
            //meshes[1].mesh = Resources.Load<Mesh>("@MoistureUpset_test:assets/pod.mesh");

        }
        private static void Sans()
        {
            LoadResource("sans");
            ReplaceModel("prefabs/characterbodies/ImpBossBody", "@MoistureUpset_sans:assets/sans.mesh", "@MoistureUpset_sans:assets/sans.png");
            ReplaceMeshRenderer("prefabs/projectileghosts/ImpVoidspikeProjectileGhost", "@MoistureUpset_sans:assets/boner.mesh", "@MoistureUpset_sans:assets/boner.png");
            //ReplaceMeshRenderer("prefabs/projectiles/ImpCrawler", "@MoistureUpset_sans:assets/boner.mesh", "@MoistureUpset_sans:assets/boner.png");
            //replace imp ImpClawFX with bones
        }
        private static void Shop()
        {
            LoadResource("shop");
            ReplaceModel("prefabs/characterbodies/ShopkeeperBody", "@MoistureUpset_shop:assets/shop.mesh", "@MoistureUpset_shop:assets/shop.png");
        }
        private static void BeetleGuard()
        {
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
            LoadResource("jelly");
            ReplaceModel("prefabs/characterbodies/JellyfishBody", "@MoistureUpset_jelly:assets/jelly.mesh", "@MoistureUpset_jelly:assets/jelly.png");
            On.EntityStates.JellyfishMonster.DeathState.OnEnter += (orig, self) =>
            {
                //AkSoundEngine.PostEvent();
                //AkSoundEngine.ExecuteActionOnEvent(4193522322, AkActionOnEventType.AkActionOnEventType_Stop);
                orig(self);
            };
        }
        private static void TacoBell()
        {
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
            ReplaceMeshRenderer("prefabs/projectileghosts/BellBallGhost", "@MoistureUpset_tacobell:assets/toco.mesh", "@MoistureUpset_tacobell:assets/toco.png");
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
            //ReplaceModel("prefabs/characterbodies/MiniMushroomBody", "@MoistureUpset_toad:assets/toad.mesh", "@MoistureUpset_toad:assets/toad.png");
            //var fab = Resources.Load<GameObject>("prefabs/characterbodies/MiniMushroomBody");
            //var meshes = fab.GetComponentsInChildren<SkinnedMeshRenderer>();
            //meshes[0].sharedMesh = Resources.Load<Mesh>("@MoistureUpset_toad:assets/toad.mesh");
        }
        private static void Imp()
        {
            ReplaceModel("prefabs/characterbodies/ImpBody", "@MoistureUpset_dooter:assets/dooter.mesh", "@MoistureUpset_dooter:assets/dooter.png");
            //var fab = Resources.Load<GameObject>("prefabs/characterbodies/ImpBody");
            //var meshes = fab.GetComponentsInChildren<SkinnedMeshRenderer>();
            //var texture = Resources.Load<Texture>("@MoistureUpset_dooter:assets/dooter.png");
            //for (int i = 0; i < meshes[0].sharedMaterials.Length; i++)
            //{
            //    try
            //    {
            //        meshes[0].sharedMaterials[i].SetTexture("_PrintRamp", texture);
            //        meshes[0].sharedMaterials[i].SetTexture("_FresnelRamp", texture);
            //        foreach (var item in meshes[0].sharedMaterials[i].GetTexturePropertyNames())
            //        {
            //            Debug.Log($"---------{item}---------------{meshes[0].sharedMaterials[i].GetTexture(item)}");
            //        }
            //    }
            //    catch (Exception e)
            //    {
            //        Debug.Log(e);
            //    }
            //}


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
            ReplaceModel("prefabs/characterbodies/WispBody", "@MoistureUpset_wisp:assets/bahdog.mesh", "@MoistureUpset_wisp:assets/bahdog.png");
            ReplaceModel("prefabs/characterbodies/WispSoulBody", "@MoistureUpset_wisp:assets/bahdog.mesh", "@MoistureUpset_wisp:assets/bahdog.png");
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
                                item.SetTexture("_EmTex", t);
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
                                item.SetTexture("_EmTex", t);
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
    }
}
