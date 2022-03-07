using HarmonyLib;
using MoistureUpset.Skins.Engi;
using MonoMod.RuntimeDetour;
using R2API;
using R2API.Utils;
using RoR2;
using RoR2.Projectile;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Object = UnityEngine.Object;

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

            SkinManager.AddSkin("EngiTurretBody", EngiTurretSkin);
            SkinManager.AddSkin("EngiWalkerTurretBody", EngiWalkerTurretSkin);
            SkinManager.AddSkin("EngiBody", EngiSkin);

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

            if (damageReport.victimBody.IsSkin("THE_TF2_ENGINEER_SKIN"))
            {
                damageReport.victimBody.GetComponent<EngiKillCam>().attacker = damageReport.attackerBody;
            }
        }

        private static void KillCamTest(On.RoR2.CharacterMaster.orig_OnBodyDeath orig, CharacterMaster self, CharacterBody body)
        {
            orig(self, body);

            if (body.IsSkin("THE_TF2_ENGINEER_SKIN"))
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
            Assets.AddBundle("engineer");
            Assets.AddBundle("Resources.tf2_engineer_icon");
            Assets.AddBundle("Models.dispener");
            Assets.AddBundle("Models.demopill");
            Assets.AddBundle("Models.rocket");
            Assets.AddBundle("Models.mines");
            Assets.AddBundle("Models.oopsideletedtheoldresource");
            Assets.AddBundle("unifiedturret");
            Assets.AddBundle("Resources.medic");
        }

        private static SkinDef[] EngiTurretSkin(GameObject bodyPrefab)
        {
            var engiTurretBodyPrefab = bodyPrefab;

            engiTurretBodyPrefab.AddComponent<SkinReloader>();

            var engiTurretBodyRenderer = engiTurretBodyPrefab.GetComponentsInChildren<Renderer>();

            var turretSkinController = engiTurretBodyPrefab.GetComponentInChildren<ModelSkinController>();

            var turretSkinDef = new LoadoutAPI.SkinDefInfo
            {
                Icon = LoadoutAPI.CreateSkinIcon(new Color(.75f, .14f, .37f, 1f), new Color(.003f, .05f, .14f, 1f), new Color(.25f, .04f, .15f, 1f), new Color(.96f, .66f, .45f, 1f)),
                Name = "Level 2 Sentry",
                NameToken = "EngiTurretBody",
                RootObject = turretSkinController.gameObject,
                BaseSkins = Array.Empty<SkinDef>(),
                GameObjectActivations = Array.Empty<SkinDef.GameObjectActivation>(),

                RendererInfos = new CharacterModel.RendererInfo[]
                {
                    new CharacterModel.RendererInfo
                    {
                        defaultMaterial = Assets.LoadMaterial("assets/unified_turret_tex.png"),
                        defaultShadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On,
                        ignoreOverlays = false,
                        renderer = engiTurretBodyRenderer[0]
                    }

                },
                MeshReplacements = new SkinDef.MeshReplacement[]
                {
                    new SkinDef.MeshReplacement
                    {
                        mesh = Assets.Load<Mesh>("assets/normal_sentry.mesh"),
                        renderer = engiTurretBodyRenderer[0]
                    }
                },
                ProjectileGhostReplacements = Array.Empty<SkinDef.ProjectileGhostReplacement>(),
                MinionSkinReplacements = Array.Empty<SkinDef.MinionSkinReplacement>()
            };

            var bruh = LoadoutAPI.CreateNewSkinDef(turretSkinDef);

            Array.Resize(ref turretSkinController.skins, turretSkinController.skins.Length + 1);
            turretSkinController.skins[turretSkinController.skins.Length - 1] = bruh;

            return turretSkinController.skins;
        }

        private static SkinDef[] EngiWalkerTurretSkin(GameObject bodyPrefab)
        {
            var engiWalkerTurretBodyPrefab = bodyPrefab;

            engiWalkerTurretBodyPrefab.AddComponent<SkinReloader>();

            var engiWalkerTurretBodyRenderer = engiWalkerTurretBodyPrefab.GetComponentsInChildren<Renderer>();

            var walkerTurretSkinController = engiWalkerTurretBodyPrefab.GetComponentInChildren<ModelSkinController>();

            var walkerTurretSkinDef = new LoadoutAPI.SkinDefInfo
            {
                Icon = LoadoutAPI.CreateSkinIcon(new Color(.75f, .14f, .37f, 1f), new Color(.003f, .05f, .14f, 1f), new Color(.25f, .04f, .15f, 1f), new Color(.96f, .66f, .45f, 1f)),
                Name = "Level 1 Sentry",
                NameToken = "EngiWalkerTurretBody",
                RootObject = walkerTurretSkinController.gameObject,
                BaseSkins = Array.Empty<SkinDef>(),
                GameObjectActivations = Array.Empty<SkinDef.GameObjectActivation>(),

                RendererInfos = new CharacterModel.RendererInfo[]
                {
                    new CharacterModel.RendererInfo
                    {
                        defaultMaterial = Assets.LoadMaterial("assets/unified_turret_tex.png"),
                        defaultShadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On,
                        ignoreOverlays = false,
                        renderer = engiWalkerTurretBodyRenderer[0]
                    }

                },
                MeshReplacements = new SkinDef.MeshReplacement[]
                {
                    new SkinDef.MeshReplacement
                    {
                        mesh = Assets.Load<Mesh>("assets/walker_turret.mesh"),
                        renderer = engiWalkerTurretBodyRenderer[0]
                    }
                },
                ProjectileGhostReplacements = Array.Empty<SkinDef.ProjectileGhostReplacement>(),
                MinionSkinReplacements = Array.Empty<SkinDef.MinionSkinReplacement>()
            };

            var ligma = LoadoutAPI.CreateNewSkinDef(walkerTurretSkinDef);

            Array.Resize(ref walkerTurretSkinController.skins, walkerTurretSkinController.skins.Length + 1);
            walkerTurretSkinController.skins[walkerTurretSkinController.skins.Length - 1] = ligma;

            return walkerTurretSkinController.skins;
        }

        // Skindef stuff here
        private static SkinDef[] EngiSkin(GameObject bodyPrefab)
        {
            var renderers = bodyPrefab.GetComponentsInChildren<Renderer>();
            var skinController = bodyPrefab.GetComponentInChildren<ModelSkinController>();

            var mdl = skinController.gameObject;

            var engiTurretSkinDef = SkinManager.skins["EngiTurretBody"];
            var engiWalkerTurretSkinDef = SkinManager.skins["EngiWalkerTurretBody"];

            var skin = new LoadoutAPI.SkinDefInfo
            {
                Icon = Assets.Load<Sprite>("assets/tf2_engineer_icon.png"),
                Name = Name,
                NameToken = NameToken,
                RootObject = mdl,
                BaseSkins = Array.Empty<SkinDef>(),
                GameObjectActivations = Array.Empty<SkinDef.GameObjectActivation>(),

                RendererInfos = new CharacterModel.RendererInfo[]
                {
                    new CharacterModel.RendererInfo
                    {
                        defaultMaterial = Assets.LoadMaterial("assets/models_player_engineer_engineer_red.png"),
                        defaultShadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On,
                        ignoreOverlays = false,
                        renderer = renderers[0]
                    }

                },
                MeshReplacements = new SkinDef.MeshReplacement[]
                {
                    new SkinDef.MeshReplacement
                    {
                        mesh = Assets.Load<Mesh>("assets/engi.mesh"),
                        renderer = renderers[0]
                    }
                },
                ProjectileGhostReplacements = Array.Empty<SkinDef.ProjectileGhostReplacement>(),
                MinionSkinReplacements = new[]
                {
                    new SkinDef.MinionSkinReplacement
                    {
                        minionBodyPrefab = SkinManager.GetBodyPrefab("EngiTurretBody"),
                        minionSkin = engiTurretSkinDef
                    },
                    new SkinDef.MinionSkinReplacement
                    {
                        minionBodyPrefab = SkinManager.GetBodyPrefab("EngiWalkerTurretBody"),
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
            var fab = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Engi/EngiDisplay.prefab").WaitForCompletion();

            fab.AddComponent<EngiDisplayFix>(); // Still not a great system, but it works.
        }

        // Projectile Replacements
        private static void ModifyProjectiles(On.RoR2.Projectile.ProjectileController.orig_Start orig, ProjectileController self)
        {
            orig(self);
            try
            {
                var cb = self.owner.GetComponentInChildren<CharacterBody>();

                if (!cb)
                    return;

                if (self.owner.name != "EngiBody(Clone)" || !cb.IsSkin("THE_TF2_ENGINEER_SKIN"))
                    return;
                
                switch (self.ghost.name)
                {
                    case "EngiSeekerGrenadeGhost(Clone)":
                    {
                        var meshes = self.ghost.gameObject.GetComponentsInChildren<MeshFilter>();

                        meshes[0].sharedMesh = Assets.Load<Mesh>("assets/dispenser.mesh");
                        meshes[1].sharedMesh = Assets.Load<Mesh>("assets/na1.mesh");
                        meshes[2].sharedMesh = Assets.Load<Mesh>("assets/na1.mesh");

                        self.ghost.gameObject.GetComponentInChildren<Renderer>().material.mainTexture = Assets.Load<Texture>("assets/dispenser.png");
                        self.ghost.gameObject.GetComponentInChildren<Renderer>().material.SetTexture("_EmTex", Assets.Load<Texture>("assets/dispenser.png"));
                        self.ghost.gameObject.GetComponentInChildren<Renderer>().material.SetTexture("_NormalTex", null);

                        meshes[0].transform.localScale = new Vector3(0.5f, 0.55f, 0.5f);
                        meshes[0].transform.localPosition += new Vector3(0f, 0f, 0.5f);

                        Object.DestroyImmediate(self.ghost.GetComponentInChildren<Rewired.ComponentControls.Effects.RotateAroundAxis>());

                        SoundAssets.PlaySound("EngiBuildsDispenser", cb.gameObject);
                        break;
                    }
                    case "EngiGrenadeGhost(Clone)":
                    {
                        var meshes = self.ghost.gameObject.GetComponentsInChildren<MeshFilter>();

                        meshes[0].sharedMesh = Assets.Load<Mesh>("assets/demopill.mesh");

                        self.ghost.gameObject.GetComponentInChildren<Renderer>().material = Assets.Load<Material>("assets/demopill.mat");

                        meshes[0].transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
                        break;
                    }
                    case "EngiHarpoonGhost(Clone)":
                    {
                        var meshes = self.ghost.gameObject.GetComponentsInChildren<MeshFilter>();

                        meshes[0].sharedMesh = Assets.Load<Mesh>("assets/rocket.mesh");

                        self.ghost.gameObject.GetComponentInChildren<MeshRenderer>().material = Assets.LoadMaterial("assets/models_player_engineer_engineer_red.png");

                        meshes[0].transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
                        break;
                    }
                    case "SpiderMineGhost(Clone)":
                    {
                        var meshes = self.ghost.gameObject.GetComponentsInChildren<SkinnedMeshRenderer>();

                        meshes[0].sharedMesh = Assets.Load<Mesh>("assets/spidermine.mesh");

                        self.ghost.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material = Assets.LoadMaterial("assets/mines.png");
                        break;
                    }
                    case "EngiMineGhost(Clone)":
                    {
                        var meshes = self.ghost.gameObject.GetComponentsInChildren<SkinnedMeshRenderer>();

                        meshes[0].sharedMesh = Assets.Load<Mesh>("assets/harpoon.mesh");

                        self.ghost.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material = Assets.LoadMaterial("assets/mines.png");
                        break;
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

            if (cb.IsSkin("THE_TF2_ENGINEER_SKIN"))
            {
                if (self.blueprintPrefab != null)
                {
                    tempPrefab = GameObject.Instantiate<GameObject>(self.blueprintPrefab);

                    switch (self.blueprintPrefab.name)
                    {
                        case "EngiTurretBlueprints":
                            tempPrefab.GetComponentInChildren<SkinnedMeshRenderer>().sharedMesh = Assets.Load<Mesh>("assets/normal_sentry.mesh");
                            break;
                        case "EngiWalkerTurretBlueprints":
                            tempPrefab.GetComponentInChildren<SkinnedMeshRenderer>().sharedMesh = Assets.Load<Mesh>("assets/walker_turret.mesh");
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
            GameObject engiBody = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Engi/EngiBody.prefab").WaitForCompletion(); // load engibody prefab
            engiBody.AddComponent<AddMedicIcon>();
            engiBody.AddComponent<EngiHurt>();
            engiBody.AddComponent<EngiKillCam>();
        }
    }
}
