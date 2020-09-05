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
        private static void ReplaceModel(string prefab, string mesh, string png, int position = 0, bool replaceothers = false)
        {
            var fab = Resources.Load<GameObject>(prefab);
            var meshes = fab.GetComponentsInChildren<SkinnedMeshRenderer>();
            meshes[position].sharedMesh = Resources.Load<Mesh>(mesh);
            var texture = Resources.Load<Texture>(png);
            for (int i = 0; i < meshes[position].sharedMaterials.Length; i++)
            {
                meshes[position].sharedMaterials[i].color = Color.white;
                meshes[position].sharedMaterials[i].mainTexture = texture;
                meshes[position].sharedMaterials[i].SetTexture("_EmTex", texture);
                meshes[position].sharedMaterials[i].SetTexture("_NormalTex", null);
                //try
                //{
                //    foreach (var item in meshes[0].sharedMaterials[i].GetTexturePropertyNames())
                //    {
                //        Debug.Log($"------------------------{item}");
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
        public static void RunAll()
        {
            try
            {
                Lemurian();
                LemurianBruiser();
                DEBUG();
                Golem();
                Bison();
                SolusUnit();
                Templar();
                Wisp();
                GreaterWisp();
                Beetle();
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }
        }
        public static void DEBUG()
        {
            //var fab = Resources.Load<GameObject>("prefabs/networkedobjects/SurvivorPod");
            //var renderers = fab.GetComponentsInChildren<Renderer>();
            //var meshes = fab.GetComponentsInChildren<MeshFilter>();
            //renderers[0].material = Resources.Load<Material>("@MoistureUpset_droppod:assets/shrekpodmat.mat");
            //meshes[0].mesh = Resources.Load<Mesh>("@MoistureUpset_droppod:assets/outhouse.mesh");
            //renderers[1].material = Resources.Load<Material>("@MoistureUpset_droppod:assets/shrekpodmat.mat");
            //meshes[1].mesh = Resources.Load<Mesh>("@MoistureUpset_droppod:assets/door.mesh");

        }
        public static void Beetle()
        {
            //ReplaceModel("prefabs/characterbodies/BeetleBody", "@MoistureUpset_chips:assets/chip.mesh", "@MoistureUpset_chips:assets/chip.png");
        }
        public static void Templar()
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
        }
        public static void GreaterWisp()
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
        }
        public static void Wisp()
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
        public static void SolusUnit()
        {
            ReplaceModel("prefabs/characterbodies/RoboBallMiniBody", "@MoistureUpset_obamaprism:assets/Obamium.mesh", "@MoistureUpset_obamaprism:assets/Obruhma.png");
            ReplaceModel("prefabs/characterbodies/RoboBallBossBody", "@MoistureUpset_obamaprism:assets/obamasphere.mesh", "@MoistureUpset_obamaprism:assets/Obruhma.png");
            ReplaceModel("prefabs/characterbodies/SuperRoboBallBossBody", "@MoistureUpset_obamaprism:assets/obamasphere.mesh", "@MoistureUpset_obamaprism:assets/Obruhma.png");
            On.EntityStates.RoboBallBoss.DeathState.OnEnter += (orig, self) =>
            {
                Util.PlaySound("ObamaDeath",self.outer.gameObject);
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
        public static void Lemurian()
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
        public static void LemurianBruiser()
        {
            //ReplaceModel("prefabs/characterbodies/LemurianBruiserBody", "@MoistureUpset_mike:assets/bruisermike.mesh", "@MoistureUpset_mike:assets/mikebruiser.png");
        }

        public static void Golem()
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
        public static void Bison()
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
