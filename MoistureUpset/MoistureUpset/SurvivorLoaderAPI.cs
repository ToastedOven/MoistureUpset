using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using RoR2;
using R2API;
using R2API.Utils;
using R2API.AssetPlus;
using UnityEngine;

namespace MoistureUpset
{
    public static class SurvivorLoaderAPI
    {
        public static void LoadSurvivors()
        {
            PopulateAssets();
        }

        private static void PopulateAssets()
        {
            using (var assetStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("MoistureUpset.sploaderskin"))
            {
                var MainAssetBundle = AssetBundle.LoadFromStream(assetStream);

                ResourcesAPI.AddProvider(new AssetBundleResourcesProvider("@MoistureUpset", MainAssetBundle));
            }

            using (var assetStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("MoistureUpset.engineer"))
            {
                var MainAssetBundle = AssetBundle.LoadFromStream(assetStream);

                ResourcesAPI.AddProvider(new AssetBundleResourcesProvider("@MoistureUpset_engi", MainAssetBundle));
            }

            using (var assetStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("MoistureUpset.unifiedturret"))
            {
                var MainAssetBundle = AssetBundle.LoadFromStream(assetStream);

                ResourcesAPI.AddProvider(new AssetBundleResourcesProvider("@MoistureUpset_engi_turret", MainAssetBundle));
            }

            using (var assetStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("MoistureUpset.mobile"))
            {
                var MainAssetBundle = AssetBundle.LoadFromStream(assetStream);

                ResourcesAPI.AddProvider(new AssetBundleResourcesProvider("@MoistureUpset_engi_turret2", MainAssetBundle));
            }

            using (var assetStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("MoistureUpset.sentry2"))
            {
                var MainAssetBundle = AssetBundle.LoadFromStream(assetStream);

                ResourcesAPI.AddProvider(new AssetBundleResourcesProvider("@MoistureUpset_engi_sentry2", MainAssetBundle));
            }

            using (var assetStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("MoistureUpset.outhousebetter"))
            {
                var MainAssetBundle = AssetBundle.LoadFromStream(assetStream);

                ResourcesAPI.AddProvider(new AssetBundleResourcesProvider("@MoistureUpset_droppod", MainAssetBundle));
            }
            On.RoR2.SurvivorCatalog.Init += SurvivorCatalog_Init;

            On.RoR2.MasterSummon.Perform += MasterSummon_Perform;

            On.EntityStates.Engi.EngiWeapon.PlaceTurret.OnEnter += PlaceTurret_OnEnter;
        }

        private static void PlaceTurret_OnEnter(On.EntityStates.Engi.EngiWeapon.PlaceTurret.orig_OnEnter orig, EntityStates.Engi.EngiWeapon.PlaceTurret self)
        {
            orig(self);

        }

        private static CharacterMaster MasterSummon_Perform(On.RoR2.MasterSummon.orig_Perform orig, MasterSummon self)
        {
            CharacterMaster cm = orig(self);
            try
            {
                if (cm.minionOwnership.ownerMaster != null)
                {
                    if (cm.minionOwnership.ownerMaster.GetBody().skinIndex == 2 && cm.minionOwnership.ownerMaster.GetBody().name == "EngiBody(Clone)")
                    {
                        for (int i = 0; i < cm.GetBody().GetComponentInChildren<SkinnedMeshRenderer>().sharedMaterials.Length; i++)
                        {
                            cm.GetBody().GetComponentInChildren<SkinnedMeshRenderer>().sharedMaterials[i].mainTexture = Resources.Load<Texture>("@MoistureUpset_engi_turret2:assets/unified_turret_tex.png");
                            cm.GetBody().GetComponentInChildren<SkinnedMeshRenderer>().sharedMaterials[i].SetTexture("_EmTex", Resources.Load<Texture>("@MoistureUpset_engi_turret2:assets/unified_turret_tex.png"));

                        }
                        switch (cm.name)
                        {
                            case "EngiWalkerTurretMaster(Clone)":
                                cm.GetBody().GetComponentInChildren<SkinnedMeshRenderer>().sharedMesh = Resources.Load<Mesh>("@MoistureUpset_engi_turret:assets/walker_turret.mesh");
                                break;
                            case "EngiTurretMaster(Clone)":
                                cm.GetBody().GetComponentInChildren<SkinnedMeshRenderer>().sharedMesh = Resources.Load<Mesh>("@MoistureUpset_engi_turret:assets/normal_sentry.mesh");
                                break;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }


            return cm;
        }

        private static void SurvivorCatalog_Init(On.RoR2.SurvivorCatalog.orig_Init orig)
        {
            orig();

            AddStarPlatinumSkinToLoader();
            EngineerStuff("The Engineer", "THE_ENGINEER_SKIN", "@MoistureUpset_engi:assets/models_player_engineer_engineer_red.mat", "@MoistureUpset_engi:assets/engi.mesh", RoR2.SurvivorIndex.Engi);
            EditDropPod();
        }

        private static void EditDropPod()
        {
            var fab = Resources.Load<GameObject>("prefabs/networkedobjects/SurvivorPod");
            var renderers = fab.GetComponentsInChildren<Renderer>();
            var meshes = fab.GetComponentsInChildren<MeshFilter>();
            renderers[0].material = Resources.Load<Material>("@MoistureUpset_droppod:assets/shrekpodmat.mat");
            meshes[0].mesh = Resources.Load<Mesh>("@MoistureUpset_droppod:assets/outhouse.mesh");
            renderers[1].material = Resources.Load<Material>("@MoistureUpset_droppod:assets/shrekpodmat.mat");
            meshes[1].mesh = Resources.Load<Mesh>("@MoistureUpset_droppod:assets/door.mesh");




            //fab = Resources.Load<GameObject>("prefabs/characterbodies/EngiTurretBody");
            //renderers = fab.GetComponentsInChildren<Renderer>();
            //meshes = fab.GetComponentsInChildren<MeshFilter>();
            //renderers[0].material = Resources.Load<Material>("@MoistureUpset_engi_sentry2:assets/coolturretmat.mat");
            //meshes[0].mesh = Resources.Load<Mesh>("@MoistureUpset_engi_sentry2:assets/sentry2_optimized_reference.mesh");
            //fab = Resources.Load<GameObject>("prefabs/characterbodies/Turret1Body");
            //foreach (var item in fab.GetComponentsInChildren<Component>())
            //{
            //    Debug.Log($"------------------------------name: {item.name} type: {item.GetType()}");
            //}
        }
        private static void EngineerStuff(string _name, string _nameToken, string _mat1, string _mesh1, RoR2.SurvivorIndex _survivorIndex)
        {
            var survivorDef = SurvivorCatalog.GetSurvivorDef(_survivorIndex);
            var bodyPrefab = survivorDef.bodyPrefab;

            var renderers = bodyPrefab.GetComponentsInChildren<Renderer>();
            var skinController = bodyPrefab.GetComponentInChildren<ModelSkinController>();

            var turretRenderers1 = skinController.skins[1].minionSkinReplacements[0].minionBodyPrefab.GetComponentsInChildren<Renderer>();
            var turretRenderers2 = skinController.skins[1].minionSkinReplacements[1].minionBodyPrefab.GetComponentsInChildren<Renderer>();

            var normalTurretBodyPrefab = Resources.Load<GameObject>("prefabs/characterbodies/EngiTurretBody");

            var normalTRenderers = normalTurretBodyPrefab.GetComponentsInChildren<Renderer>();
            var normalTController = normalTurretBodyPrefab.GetComponentInChildren<ModelSkinController>();

            var turretSkinController = skinController.skins[1].minionSkinReplacements[0].minionBodyPrefab.GetComponentInChildren<ModelSkinController>();

            var mdl = skinController.gameObject;



            var skin = new LoadoutAPI.SkinDefInfo
            {
                Icon = LoadoutAPI.CreateSkinIcon(Color.red, Color.yellow, Color.red, Color.yellow),
                Name = _name,
                NameToken = _nameToken,
                RootObject = mdl,
                BaseSkins = new SkinDef[0],
                UnlockableName = "",
                GameObjectActivations = new SkinDef.GameObjectActivation[0],

                RendererInfos = new CharacterModel.RendererInfo[]
                {
                    new CharacterModel.RendererInfo
                    {
                        defaultMaterial = Resources.Load<Material>(_mat1),
                        defaultShadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On,
                        ignoreOverlays = false,
                        renderer = renderers[0]
                    }

                },
                MeshReplacements = new SkinDef.MeshReplacement[]
                {
                    new SkinDef.MeshReplacement
                    {
                        mesh = Resources.Load<Mesh>(_mesh1),
                        renderer = renderers[0]
                    }
                },
                ProjectileGhostReplacements = new SkinDef.ProjectileGhostReplacement[0]
            };
            On.RoR2.SkinDef.Awake += SkinDef_Awake;
            SkinDef test = LoadoutAPI.CreateNewSkinDef(skin);
            SkinDef minionReplacement = ScriptableObject.CreateInstance<RoR2.SkinDef>();
            SkinDef.MeshReplacement[] mr = new SkinDef.MeshReplacement[]
            {
                new SkinDef.MeshReplacement
                {
                    mesh = Resources.Load<Mesh>("@MoistureUpset_engi_sentry2:assets/sentry2_optimized_reference.mesh"),
                    renderer = normalTRenderers[0]
                },
                new SkinDef.MeshReplacement
                {
                    mesh = Resources.Load<Mesh>("@MoistureUpset_engi_sentry2:assets/sentry2_optimized_reference.mesh"),
                    renderer = normalTRenderers[0]
                },
                new SkinDef.MeshReplacement
                {
                    mesh = Resources.Load<Mesh>("@MoistureUpset_engi_sentry2:assets/sentry2_optimized_reference.mesh"),
                    renderer = normalTRenderers[0]
                },
            };

            CharacterModel.RendererInfo[] ri = new CharacterModel.RendererInfo[]
            {
                new CharacterModel.RendererInfo
                {
                    defaultMaterial = Resources.Load<Material>("@MoistureUpset_engi_sentry2:assets/coolturretmat.mat"),
                    defaultShadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On,
                    ignoreOverlays = false,
                    renderer = normalTRenderers[0]
                },
                new CharacterModel.RendererInfo
                {
                    defaultMaterial = Resources.Load<Material>("@MoistureUpset_engi_sentry2:assets/coolturretmat.mat"),
                    defaultShadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On,
                    ignoreOverlays = false,
                    renderer = normalTRenderers[0]
                },
                new CharacterModel.RendererInfo
                {
                    defaultMaterial = Resources.Load<Material>("@MoistureUpset_engi_sentry2:assets/coolturretmat.mat"),
                    defaultShadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On,
                    ignoreOverlays = false,
                    renderer = normalTRenderers[0]
                },
            };

            minionReplacement.meshReplacements = mr;
            minionReplacement.rendererInfos = ri;

            test.minionSkinReplacements = new SkinDef.MinionSkinReplacement[]
            {
                new SkinDef.MinionSkinReplacement
                {
                    minionSkin = minionReplacement,
                    minionBodyPrefab = normalTurretBodyPrefab
                }
            };



            On.RoR2.SkinDef.Awake -= SkinDef_Awake;

            Array.Resize(ref skinController.skins, skinController.skins.Length + 1);
            skinController.skins[skinController.skins.Length - 1] = test;

            Array.Resize(ref normalTController.skins, normalTController.skins.Length + 1);
            normalTController.skins[normalTController.skins.Length - 1] = minionReplacement;

            var skinsField = Reflection.GetFieldValue<SkinDef[][]>(typeof(BodyCatalog), "skins");
            skinsField[BodyCatalog.FindBodyIndex(bodyPrefab)] = skinController.skins;
            Reflection.SetFieldValue(typeof(BodyCatalog), "skins", skinsField);
        }

        private static void SkinDef_Awake(On.RoR2.SkinDef.orig_Awake orig, RoR2.SkinDef self)
        {

        }

        private static void AddStarPlatinumSkinToLoader()
        {
            var survivorDef = SurvivorCatalog.GetSurvivorDef(SurvivorIndex.Loader);
            var bodyPrefab = survivorDef.bodyPrefab;

            var renderers = bodyPrefab.GetComponentsInChildren<Renderer>();
            var skinController = bodyPrefab.GetComponentInChildren<ModelSkinController>();

            var mdl = skinController.gameObject;

            var skin = new LoadoutAPI.SkinDefInfo
            {
                Icon = LoadoutAPI.CreateSkinIcon(Color.black, Color.white, new Color(0.69F, 0.19F, 0.65F, 1F), Color.yellow),
                Name = "Star Platinum",
                NameToken = "STAR_PLATINUM_SKIN",
                RootObject = mdl,
                BaseSkins = new SkinDef[0],
                UnlockableName = "",
                GameObjectActivations = new SkinDef.GameObjectActivation[0],

                RendererInfos = new CharacterModel.RendererInfo[]
                {
                    new CharacterModel.RendererInfo
                    {
                        defaultMaterial = Resources.Load<Material>("@MoistureUpset:assets/starplatinummat.mat"),
                        defaultShadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On,
                        ignoreOverlays = false,
                        renderer = renderers[0]
                    },
                    new CharacterModel.RendererInfo
                    {
                        defaultMaterial = Resources.Load<Material>("@MoistureUpset:assets/starplatinummat.mat"),
                        defaultShadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On,
                        ignoreOverlays = false,
                        renderer = renderers[1]
                    },
                    new CharacterModel.RendererInfo
                    {
                        defaultMaterial = Resources.Load<Material>("@MoistureUpset:assets/starplatinummat.mat"),
                        defaultShadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On,
                        ignoreOverlays = false,
                        renderer = renderers[2]
                    },
                },

                MeshReplacements = new SkinDef.MeshReplacement[]
                {
                    new SkinDef.MeshReplacement
                    {
                        mesh = Resources.Load<Mesh>("@MoistureUpset:assets/sphist.mesh"),
                        renderer = renderers[0]
                    },
                    new SkinDef.MeshReplacement
                    {
                        mesh = Resources.Load<Mesh>("@MoistureUpset:assets/speniscloth.mesh"),
                        renderer = renderers[1]
                    },
                    new SkinDef.MeshReplacement
                    {
                        mesh = Resources.Load<Mesh>("@MoistureUpset:assets/splatinum.mesh"),
                        renderer = renderers[2]
                    }

                },
                ProjectileGhostReplacements = new SkinDef.ProjectileGhostReplacement[0],
                MinionSkinReplacements = new SkinDef.MinionSkinReplacement[0]
            };

            Array.Resize(ref skinController.skins, skinController.skins.Length + 1);
            skinController.skins[skinController.skins.Length - 1] = LoadoutAPI.CreateNewSkinDef(skin);

            var skinsField = Reflection.GetFieldValue<SkinDef[][]>(typeof(BodyCatalog), "skins");
            skinsField[BodyCatalog.FindBodyIndex(bodyPrefab)] = skinController.skins;
            Reflection.SetFieldValue(typeof(BodyCatalog), "skins", skinsField);
        }
    }
}
