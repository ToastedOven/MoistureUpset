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

            On.RoR2.SurvivorCatalog.Init += SurvivorCatalog_Init;
        }

        private static void SurvivorCatalog_Init(On.RoR2.SurvivorCatalog.orig_Init orig)
        {
            orig();

            AddStarPlatinumSkinToLoader();
        }

        private static void AddStarPlatinumSkinToLoader()
        {
            var survivorDef = SurvivorCatalog.GetSurvivorDef(SurvivorIndex.Loader);
            var bodyPrefab = survivorDef.bodyPrefab;

            var renderers = bodyPrefab.GetComponentsInChildren<Renderer>();
            var skinController = bodyPrefab.GetComponentInChildren<ModelSkinController>();

            foreach (var item in skinController.skins)
            {
                Debug.Log($"{item.nameToken}");
                foreach (var skinItem in item.baseSkins)
                {
                    Debug.Log($"{skinItem}");
                }
            }

            var mdl = skinController.gameObject;

            var skin = new LoadoutAPI.SkinDefInfo
            {
                Icon = LoadoutAPI.CreateSkinIcon(Color.black, Color.white, new Color(0.69F, 0.19F, 0.65F, 1F), Color.yellow),
                Name = "StarPlatinum",
                NameToken = "STARPLATINUM_SKIN",
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
                        renderer = renderers[2]
                    }
                },

                MeshReplacements = new SkinDef.MeshReplacement[]
                {
                    new SkinDef.MeshReplacement
                    {
                        mesh = Resources.Load<Mesh>("@MoistureUpset:assets/ora.mesh"),
                        renderer = renderers[2]
                    },
                    new SkinDef.MeshReplacement
                    {
                        mesh = Resources.Load<Mesh>("@MoistureUpset:assets/splatinum.mesh"),
                        renderer = renderers[2]
                    },

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
