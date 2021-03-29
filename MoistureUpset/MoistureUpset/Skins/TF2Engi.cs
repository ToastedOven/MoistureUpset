using HarmonyLib;
using MoistureUpset.Skins.Engi;
using MonoMod.RuntimeDetour;
using R2API;
using R2API.Utils;
using RoR2;
using RoR2.Projectile;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace MoistureUpset.Skins
{
    public static class TF2Engi
    {
        public static readonly string Name = "The Engineer";
        public static readonly string NameToken = "MOISTURE_UPSET_TF2_ENGINEER_ENGI_SKIN";

        // Runs on Awake
        public static void Init()
        {
            PopulateAssets();

            Utils.AddSkin("EngiTurretBody", EngiTurretSkin);
            Utils.AddSkin("EngiWalkerTurretBody", EngiWalkerTurretSkin);
            Utils.AddSkin("EngiBody", EngiSkin);

            On.RoR2.Projectile.ProjectileController.Start += ModifyProjectiles;
            On.EntityStates.Engi.EngiWeapon.PlaceTurret.OnEnter += ModifyTurretBlueprint;

            EngiDisplayFix();
            AddToPrefab();

            //On.RoR2.CharacterMaster.OnBodyDeath += KillCamTest;
            //On.RoR2.CharacterMaster.OnBodyDamaged += mostRecentAttacker;
        }


        

        private static void mostRecentAttacker(On.RoR2.CharacterMaster.orig_OnBodyDamaged orig, CharacterMaster self, DamageReport damageReport)
        {
            orig(self, damageReport);

            if (damageReport.victimBody.isSkin("THE_TF2_ENGINEER_SKIN"))
            {
                damageReport.victimBody.GetComponent<EngiKillCam>().attacker = damageReport.attackerBody;
            }
        }

        private static void KillCamTest(On.RoR2.CharacterMaster.orig_OnBodyDeath orig, CharacterMaster self, CharacterBody body)
        {
            orig(self, body);

            if (body.isSkin("THE_TF2_ENGINEER_SKIN"))
            {
                if (!self.preventGameOver)
                {
                    DebugClass.Log($"The Engi is Dead");

                    body.GetComponent<EngiKillCam>().DoKillCam();
                }
            }
        }

        // Load assets here
        private static void PopulateAssets()
        {
            Utils.LoadAsset("engineer");
            Utils.LoadAsset("Resources.tf2_engineer_icon");
            Utils.LoadAsset("Models.dispener");
            Utils.LoadAsset("Models.demopill");
            Utils.LoadAsset("Models.rocket");
            Utils.LoadAsset("Models.mines");
            Utils.LoadAsset("Models.oopsideletedtheoldresource");
            Utils.LoadAsset("unifiedturret");
            Utils.LoadAsset("Resources.medic");
        }

        private static SkinDef[] EngiTurretSkin(GameObject bodyPrefab)
        {
            var engiTurretBodyPrefab = bodyPrefab;

            engiTurretBodyPrefab.AddComponent<SkinReloader>();

            var engiTurretBodyRenderer = engiTurretBodyPrefab.GetComponentsInChildren<Renderer>();

            var TurretSkinController = engiTurretBodyPrefab.GetComponentInChildren<ModelSkinController>();

            var turretSkinDef = new LoadoutAPI.SkinDefInfo
            {
                Icon = LoadoutAPI.CreateSkinIcon(new Color(.75f, .14f, .37f, 1f), new Color(.003f, .05f, .14f, 1f), new Color(.25f, .04f, .15f, 1f), new Color(.96f, .66f, .45f, 1f)),
                Name = "Level 2 Sentry",
                NameToken = "EngiTurretBody",
                RootObject = TurretSkinController.gameObject,
                BaseSkins = new SkinDef[0],
                UnlockableName = "",
                GameObjectActivations = new SkinDef.GameObjectActivation[0],

                RendererInfos = new CharacterModel.RendererInfo[]
                {
                    new CharacterModel.RendererInfo
                    {
                        defaultMaterial = Assets.CreateMaterial("@MoistureUpset_unifiedturret:assets/unified_turret_tex.png"),
                        defaultShadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On,
                        ignoreOverlays = false,
                        renderer = engiTurretBodyRenderer[0]
                    }

                },
                MeshReplacements = new SkinDef.MeshReplacement[]
                {
                    new SkinDef.MeshReplacement
                    {
                        mesh = Resources.Load<Mesh>("@MoistureUpset_unifiedturret:assets/normal_sentry.mesh"),
                        renderer = engiTurretBodyRenderer[0]
                    }
                },
                ProjectileGhostReplacements = new SkinDef.ProjectileGhostReplacement[0],
                MinionSkinReplacements = new SkinDef.MinionSkinReplacement[0]
            };

            var bruh = LoadoutAPI.CreateNewSkinDef(turretSkinDef);

            Array.Resize(ref TurretSkinController.skins, TurretSkinController.skins.Length + 1);
            TurretSkinController.skins[TurretSkinController.skins.Length - 1] = bruh;

            return TurretSkinController.skins;
        }

        private static SkinDef[] EngiWalkerTurretSkin(GameObject bodyPrefab)
        {
            var engiWalkerTurretBodyPrefab = bodyPrefab;

            engiWalkerTurretBodyPrefab.AddComponent<SkinReloader>();

            var engiWalkerTurretBodyRenderer = engiWalkerTurretBodyPrefab.GetComponentsInChildren<Renderer>();

            var WalkerTurretSkinController = engiWalkerTurretBodyPrefab.GetComponentInChildren<ModelSkinController>();

            var walkerTurretSkinDef = new LoadoutAPI.SkinDefInfo
            {
                Icon = LoadoutAPI.CreateSkinIcon(new Color(.75f, .14f, .37f, 1f), new Color(.003f, .05f, .14f, 1f), new Color(.25f, .04f, .15f, 1f), new Color(.96f, .66f, .45f, 1f)),
                Name = "Level 1 Sentry",
                NameToken = "EngiWalkerTurretBody",
                RootObject = WalkerTurretSkinController.gameObject,
                BaseSkins = new SkinDef[0],
                UnlockableName = "",
                GameObjectActivations = new SkinDef.GameObjectActivation[0],

                RendererInfos = new CharacterModel.RendererInfo[]
                {
                    new CharacterModel.RendererInfo
                    {
                        defaultMaterial = Assets.CreateMaterial("@MoistureUpset_unifiedturret:assets/unified_turret_tex.png"),
                        defaultShadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On,
                        ignoreOverlays = false,
                        renderer = engiWalkerTurretBodyRenderer[0]
                    }

                },
                MeshReplacements = new SkinDef.MeshReplacement[]
                {
                    new SkinDef.MeshReplacement
                    {
                        mesh = Resources.Load<Mesh>("@MoistureUpset_unifiedturret:assets/walker_turret.mesh"),
                        renderer = engiWalkerTurretBodyRenderer[0]
                    }
                },
                ProjectileGhostReplacements = new SkinDef.ProjectileGhostReplacement[0],
                MinionSkinReplacements = new SkinDef.MinionSkinReplacement[0]
            };

            var ligma = LoadoutAPI.CreateNewSkinDef(walkerTurretSkinDef);

            Array.Resize(ref WalkerTurretSkinController.skins, WalkerTurretSkinController.skins.Length + 1);
            WalkerTurretSkinController.skins[WalkerTurretSkinController.skins.Length - 1] = ligma;

            return WalkerTurretSkinController.skins;
        }

        // Skindef stuff here
        private static SkinDef[] EngiSkin(GameObject bodyPrefab)
        {
            var renderers = bodyPrefab.GetComponentsInChildren<Renderer>();
            var skinController = bodyPrefab.GetComponentInChildren<ModelSkinController>();

            var mdl = skinController.gameObject;

            var engiTurretSkinDef = Utils.skins["EngiTurretBody"];
            var engiWalkerTurretSkinDef = Utils.skins["EngiWalkerTurretBody"];

            var skin = new LoadoutAPI.SkinDefInfo
            {
                Icon = Resources.Load<Sprite>("@MoistureUpset_Resources_tf2_engineer_icon:assets/tf2_engineer_icon.png"),
                Name = Name,
                NameToken = NameToken,
                RootObject = mdl,
                BaseSkins = new SkinDef[0],
                UnlockableName = "",
                GameObjectActivations = new SkinDef.GameObjectActivation[0],

                RendererInfos = new CharacterModel.RendererInfo[]
                {
                    new CharacterModel.RendererInfo
                    {
                        defaultMaterial = Assets.CreateMaterial("@MoistureUpset_engineer:assets/models_player_engineer_engineer_red.png"),
                        defaultShadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On,
                        ignoreOverlays = false,
                        renderer = renderers[0]
                    }

                },
                MeshReplacements = new SkinDef.MeshReplacement[]
                {
                    new SkinDef.MeshReplacement
                    {
                        mesh = Resources.Load<Mesh>("@MoistureUpset_Models_oopsideletedtheoldresource:assets/engi.mesh"),
                        renderer = renderers[0]
                    }
                },
                ProjectileGhostReplacements = new SkinDef.ProjectileGhostReplacement[0],
                MinionSkinReplacements = new SkinDef.MinionSkinReplacement[]
                {
                    new SkinDef.MinionSkinReplacement
                    {
                        minionBodyPrefab = Utils.GetBodyPrefab("EngiTurretBody"),
                        minionSkin = engiTurretSkinDef
                    },
                    new SkinDef.MinionSkinReplacement
                    {
                        minionBodyPrefab = Utils.GetBodyPrefab("EngiWalkerTurretBody"),
                        minionSkin = engiWalkerTurretSkinDef
                    }
                }
            };

            Array.Resize(ref skinController.skins, skinController.skins.Length + 1);
            skinController.skins[skinController.skins.Length - 1] = LoadoutAPI.CreateNewSkinDef(skin);

            //LanguageAPI.Add(NameToken, Name);

            SkinHelper.RegisterSkin("THE_TF2_ENGINEER_SKIN", "Engi");

            return skinController.skins;
        }

        // A working solution for the display elements to have the right skin.
        private static void EngiDisplayFix()
        {
            var fab = Resources.Load<GameObject>("prefabs/characterdisplays/EngiDisplay");

            fab.AddComponent<EngiDisplayFix>(); // Still not a great system, but it works.
        }

        // Projectile Replacements
        private static void ModifyProjectiles(On.RoR2.Projectile.ProjectileController.orig_Start orig, ProjectileController self)
        {
            orig(self);
            try
            {
                var cb = self.owner.GetComponentInChildren<CharacterBody>();

                if (cb != null)
                {
                    if (self.owner.name == "EngiBody(Clone)" && cb.isSkin("THE_TF2_ENGINEER_SKIN"))
                    {
                        if (self.ghost.name == "EngiSeekerGrenadeGhost(Clone)")
                        {
                            var meshes = self.ghost.gameObject.GetComponentsInChildren<MeshFilter>();

                            meshes[0].sharedMesh = Resources.Load<Mesh>("@MoistureUpset_Models_dispener:assets/dispenser.mesh");
                            meshes[1].sharedMesh = Resources.Load<Mesh>("@MoistureUpset_na:assets/na1.mesh");
                            meshes[2].sharedMesh = Resources.Load<Mesh>("@MoistureUpset_na:assets/na1.mesh");

                            self.ghost.gameObject.GetComponentInChildren<Renderer>().material.mainTexture = Resources.Load<Texture>("@MoistureUpset_Models_dispener:assets/dispenser.png");
                            self.ghost.gameObject.GetComponentInChildren<Renderer>().material.SetTexture("_EmTex", Resources.Load<Texture>("@MoistureUpset_Models_dispener:assets/dispenser.png"));
                            self.ghost.gameObject.GetComponentInChildren<Renderer>().material.SetTexture("_NormalTex", null);

                            meshes[0].transform.localScale = new Vector3(0.5f, 0.55f, 0.5f);
                            meshes[0].transform.localPosition += new Vector3(0f, 0f, 0.5f);

                            GameObject.DestroyImmediate(self.ghost.GetComponentInChildren<Rewired.ComponentControls.Effects.RotateAroundAxis>());

                            SoundAssets.PlaySound("EngiBuildsDispenser", cb.gameObject);

                        }
                        else if (self.ghost.name == "EngiGrenadeGhost(Clone)")
                        {
                            var meshes = self.ghost.gameObject.GetComponentsInChildren<MeshFilter>();

                            meshes[0].sharedMesh = Resources.Load<Mesh>("@MoistureUpset_Models_demopill:assets/demopill.mesh");

                            self.ghost.gameObject.GetComponentInChildren<Renderer>().material = Resources.Load<Material>("@MoistureUpset_Models_demopill:assets/demopill.mat");

                            meshes[0].transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
                        }
                        else if (self.ghost.name == "EngiHarpoonGhost(Clone)")
                        {
                            var meshes = self.ghost.gameObject.GetComponentsInChildren<MeshFilter>();

                            meshes[0].sharedMesh = Resources.Load<Mesh>("@MoistureUpset_Models_rocket:assets/rocket.mesh");

                            self.ghost.gameObject.GetComponentInChildren<MeshRenderer>().material = Assets.CreateMaterial("@MoistureUpset_engineer:assets/models_player_engineer_engineer_red.png");

                            meshes[0].transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
                        }
                        else if (self.ghost.name == "SpiderMineGhost(Clone)")
                        {
                            var meshes = self.ghost.gameObject.GetComponentsInChildren<SkinnedMeshRenderer>();

                            meshes[0].sharedMesh = Resources.Load<Mesh>("@MoistureUpset_Models_mines:assets/spidermine.mesh");

                            self.ghost.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material = Assets.CreateMaterial("@MoistureUpset_Models_mines:assets/mines.png");
                        }
                        else if (self.ghost.name == "EngiMineGhost(Clone)")
                        {
                            var meshes = self.ghost.gameObject.GetComponentsInChildren<SkinnedMeshRenderer>();

                            meshes[0].sharedMesh = Resources.Load<Mesh>("@MoistureUpset_Models_mines:assets/harpoon.mesh");

                            self.ghost.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material = Assets.CreateMaterial("@MoistureUpset_Models_mines:assets/mines.png");
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        // Gotta do this jank mess in order to make the blueprint look like the custom skinned turrets, but it works.
        private static void ModifyTurretBlueprint(On.EntityStates.Engi.EngiWeapon.PlaceTurret.orig_OnEnter orig, EntityStates.Engi.EngiWeapon.PlaceTurret self)
        {
            var cb = self.outer.GetComponentInChildren<CharacterBody>();

            GameObject tempPrefab = (GameObject)null;

            if (cb.isSkin("THE_TF2_ENGINEER_SKIN"))
            {
                if (self.blueprintPrefab != null)
                {
                    tempPrefab = GameObject.Instantiate<GameObject>(self.blueprintPrefab);

                    switch (self.blueprintPrefab.name)
                    {
                        case "EngiTurretBlueprints":
                            tempPrefab.GetComponentInChildren<SkinnedMeshRenderer>().sharedMesh = Resources.Load<Mesh>("@MoistureUpset_unifiedturret:assets/normal_sentry.mesh");
                            break;
                        case "EngiWalkerTurretBlueprints":
                            tempPrefab.GetComponentInChildren<SkinnedMeshRenderer>().sharedMesh = Resources.Load<Mesh>("@MoistureUpset_unifiedturret:assets/walker_turret.mesh");
                            break;
                    }

                    self.blueprintPrefab = tempPrefab;
                }
                else
                {
                    //Debug.LogWarning("is null");
                }

            }

            orig(self);

            if (tempPrefab != null)
            {
                GameObject.Destroy(tempPrefab);
            }
        }

        // Add stuff to the character prefab here
        private static void AddToPrefab()
        {
            GameObject engiBody = Resources.Load<GameObject>("prefabs/characterbodies/engibody"); // load engibody prefab
            engiBody.AddComponent<AddMedicIcon>();
            engiBody.AddComponent<EngiHurt>();
            engiBody.AddComponent<EngiKillCam>();
        }
    }
}
