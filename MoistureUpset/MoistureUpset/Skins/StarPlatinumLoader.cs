using R2API;
using R2API.Utils;
using RoR2;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace MoistureUpset.Skins
{
    // If you see this, this hasn't been fully implemented yet. Keep it a secret. - Rune
    // We have assets made for this skin, but there are numerous issues that make us feel that this skin isn't ready.
    public static class StarPlatinumLoader
    {
        private static readonly string Name = "Star Platinum";
        private static readonly string NameToken = "MOISTURE_UPSET_STAR_PLATINUM_LOADER_SKIN";

        // Runs on Awake
        public static void Init()
        {
            PopulateAssets();
            On.RoR2.SurvivorCatalog.Init += RegisterSkin;
        }

        // Load assets here
        private static void PopulateAssets()
        {
            using (var assetStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("MoistureUpset.sploaderskin"))
            {
                var MainAssetBundle = AssetBundle.LoadFromStream(assetStream);

                ResourcesAPI.AddProvider(new AssetBundleResourcesProvider("@MoistureUpset", MainAssetBundle));
            }
        }

        // Skindef stuff here
        private static void RegisterSkin(On.RoR2.SurvivorCatalog.orig_Init orig)
        {
            orig();

            //var survivorDef = RoR2Content.Survivors.Loader;
            //var bodyPrefab = survivorDef.bodyPrefab;

            //var renderers = bodyPrefab.GetComponentsInChildren<Renderer>();
            //var skinController = bodyPrefab.GetComponentInChildren<ModelSkinController>();

            //var mdl = skinController.gameObject;

            //var skin = new LoadoutAPI.SkinDefInfo
            //{
            //    Icon = LoadoutAPI.CreateSkinIcon(Color.black, Color.white, new Color(0.69F, 0.19F, 0.65F, 1F), Color.yellow),
            //    Name = Name,
            //    NameToken = NameToken,
            //    RootObject = mdl,
            //    BaseSkins = new SkinDef[0],
            //    UnlockableName = "",
            //    GameObjectActivations = new SkinDef.GameObjectActivation[0],

            //    RendererInfos = new CharacterModel.RendererInfo[]
            //    {
            //        new CharacterModel.RendererInfo
            //        {
            //            defaultMaterial = Resources.Load<Material>("@MoistureUpset:assets/starplatinummat.mat"),
            //            defaultShadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On,
            //            ignoreOverlays = false,
            //            renderer = renderers[0]
            //        },
            //        new CharacterModel.RendererInfo
            //        {
            //            defaultMaterial = Resources.Load<Material>("@MoistureUpset:assets/starplatinummat.mat"),
            //            defaultShadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On,
            //            ignoreOverlays = false,
            //            renderer = renderers[1]
            //        },
            //        new CharacterModel.RendererInfo
            //        {
            //            defaultMaterial = Resources.Load<Material>("@MoistureUpset:assets/starplatinummat.mat"),
            //            defaultShadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On,
            //            ignoreOverlays = false,
            //            renderer = renderers[2]
            //        },
            //    },

            //    MeshReplacements = new SkinDef.MeshReplacement[]
            //    {
            //        new SkinDef.MeshReplacement
            //        {
            //            mesh = Resources.Load<Mesh>("@MoistureUpset:assets/sphist.mesh"),
            //            renderer = renderers[0]
            //        },
            //        new SkinDef.MeshReplacement
            //        {
            //            mesh = Resources.Load<Mesh>("@MoistureUpset:assets/speniscloth.mesh"),
            //            renderer = renderers[1]
            //        },
            //        new SkinDef.MeshReplacement
            //        {
            //            mesh = Resources.Load<Mesh>("@MoistureUpset:assets/splatinum.mesh"),
            //            renderer = renderers[2]
            //        }

            //    },
            //    ProjectileGhostReplacements = new SkinDef.ProjectileGhostReplacement[0],
            //    MinionSkinReplacements = new SkinDef.MinionSkinReplacement[0]
            //};

            //Array.Resize(ref skinController.skins, skinController.skins.Length + 1);
            //skinController.skins[skinController.skins.Length - 1] = LoadoutAPI.CreateNewSkinDef(skin);

            //var skinsField = Reflection.GetFieldValue<SkinDef[][]>(typeof(BodyCatalog), "skins");
            //skinsField[BodyCatalog.FindBodyIndex(bodyPrefab)] = skinController.skins;
            //Reflection.SetFieldValue(typeof(BodyCatalog), "skins", skinsField);

            //LanguageAPI.Add(NameToken, Name);

            //DebugClass.Log($"Adding skin: {Name}");
        }
    }
}
