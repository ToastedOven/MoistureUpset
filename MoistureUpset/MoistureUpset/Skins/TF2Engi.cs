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
    public static class TF2Engi
    {
        private static readonly string Name = "The Engineer";
        private static readonly string NameToken = "MOISTURE_UPSET_TF2_ENGINEER_ENGI_SKIN";

        // Runs on Awake
        public static void Init()
        {
            PopulateAssets();
            On.RoR2.SurvivorCatalog.Init += RegisterSkin;
            EngiDisplayFix();
        }

        // Load assets here
        private static void PopulateAssets()
        {
            Utils.LoadAsset("MoistureUpset.engineer");
            Utils.LoadAsset("MoistureUpset.Resources.tf2_engineer_icon");
            //using (var assetStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("MoistureUpset.engineer"))
            //{
            //    var MainAssetBundle = AssetBundle.LoadFromStream(assetStream);

            //    ResourcesAPI.AddProvider(new AssetBundleResourcesProvider("@MoistureUpset_engi", MainAssetBundle));
            //}
            //using (var assetStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("MoistureUpset.Resources.tf2_engineer_icon"))
            //{
            //    var MainAssetBundle = AssetBundle.LoadFromStream(assetStream);

            //    ResourcesAPI.AddProvider(new AssetBundleResourcesProvider("@MoistureUpset_engi_icon", MainAssetBundle));
            //}
        }

        // Skindef stuff here
        private static void RegisterSkin(On.RoR2.SurvivorCatalog.orig_Init orig)
        {
            var survivorDef = SurvivorCatalog.GetSurvivorDef(SurvivorIndex.Engi);
            var bodyPrefab = survivorDef.bodyPrefab;

            var engiTurretBodyPrefab = BodyCatalog.GetBodyPrefab(36);
            var engiTurretBodyRenderer = engiTurretBodyPrefab.GetComponentsInChildren<Renderer>();

            var engiWalkerTurretBodyPrefab = BodyCatalog.GetBodyPrefab(37);
            var engiWalkerTurretBodyRenderer = engiWalkerTurretBodyPrefab.GetComponentsInChildren<Renderer>();

            var renderers = bodyPrefab.GetComponentsInChildren<Renderer>();
            var skinController = bodyPrefab.GetComponentInChildren<ModelSkinController>();

            var mdl = skinController.gameObject;

            var TurretSkinController = engiTurretBodyPrefab.GetComponentInChildren<ModelSkinController>();
            var WalkerTurretSkinController = engiWalkerTurretBodyPrefab.GetComponentInChildren<ModelSkinController>();
        }

        // A working solution for the display elements to have the right skin.
        private static void EngiDisplayFix()
        {
            var fab = Resources.Load<GameObject>("prefabs/characterdisplays/EngiDisplay");

            fab.AddComponent<DisplayFix>(); // Still not a great system, but it works.
        }
    }
}
