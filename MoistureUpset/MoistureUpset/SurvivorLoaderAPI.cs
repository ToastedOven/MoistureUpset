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
            On.EntityStates.Engi.EngiWeapon.PlaceTurret.OnExit += (orig, self) =>
            {
                orig(self);
                Debug.Log(self.turretMasterPrefab);
                Debug.Log(self.turretMasterPrefab.GetType());
            };
            var survivorDef = SurvivorCatalog.GetSurvivorDef(_survivorIndex);
            var bodyPrefab = survivorDef.bodyPrefab;

            var renderers = bodyPrefab.GetComponentsInChildren<Renderer>();
            var skinController = bodyPrefab.GetComponentInChildren<ModelSkinController>();

            var minionrenderers = Resources.Load<GameObject>("prefabs/charactermasters/EngiTurretMaster").GetComponentsInChildren<Renderer>();
            var minionobj = Resources.Load<GameObject>("prefabs/charactermasters/EngiTurretMaster").GetComponentInChildren<ModelSkinController>().gameObject;



            var mdl = skinController.gameObject;

            var skin = new LoadoutAPI.SkinDefInfo
            {
                Icon = LoadoutAPI.CreateSkinIcon(Color.black, Color.white, new Color(0.69F, 0.19F, 0.65F, 1F), Color.yellow),
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
                    },
                },
                MeshReplacements = new SkinDef.MeshReplacement[]
                {
                    new SkinDef.MeshReplacement
                    {
                        mesh = Resources.Load<Mesh>(_mesh1),
                        renderer = renderers[0]
                    },
                },
                ProjectileGhostReplacements = new SkinDef.ProjectileGhostReplacement[0],
                MinionSkinReplacements = new SkinDef.MinionSkinReplacement[0],
                //MinionSkinReplacements = new SkinDef.MinionSkinReplacement[]
                //{
                //    new SkinDef.MinionSkinReplacement
                //    {
                //        minionSkin = new SkinDef
                //        {
                //            meshReplacements = new SkinDef.MeshReplacement[]
                //            {
                //                 new SkinDef.MeshReplacement
                //                 {
                //                   mesh = Resources.Load<Mesh>("@MoistureUpset_engi_sentry2:assets/sentry2_optimized_reference.mesh"),
                //                   renderer = minionrenderers[0]
                //                 },
                //            },
                //            rendererInfos = new CharacterModel.RendererInfo[]
                //            {
                //                 new CharacterModel.RendererInfo
                //                 {
                //                   defaultMaterial = Resources.Load<Material>("@MoistureUpset_engi_sentry2:assets/coolturretmat.mat"),
                //                   defaultShadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On,
                //                   ignoreOverlays = false,
                //                   renderer = minionrenderers[0]
                //                 },
                //            },
                //        },
                //        //minionBodyPrefab = minionobj,
                //    },
                //},
            };




            Array.Resize(ref skinController.skins, skinController.skins.Length + 1);
            skinController.skins[skinController.skins.Length - 1] = LoadoutAPI.CreateNewSkinDef(skin);

            var skinsField = Reflection.GetFieldValue<SkinDef[][]>(typeof(BodyCatalog), "skins");
            skinsField[BodyCatalog.FindBodyIndex(bodyPrefab)] = skinController.skins;
            Reflection.SetFieldValue(typeof(BodyCatalog), "skins", skinsField);
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
