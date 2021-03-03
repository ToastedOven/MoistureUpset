using R2API;
using RoR2;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace MoistureUpset.Skins
{
    public static class Utils
    {
        // Makes loading assets easier
        public static void LoadAsset(string ResourceStream, string name = null)
        {
            using (var assetStream = Assembly.GetExecutingAssembly().GetManifestResourceStream($"MoistureUpset.{ResourceStream}"))
            {
                var MainAssetBundle = AssetBundle.LoadFromStream(assetStream);

                if (name == null)
                {
                    name = $"@MoistureUpset_{ResourceStream.Replace(".", "_")}";
                }

                DebugClass.Log($"Loading Asset: {ResourceStream}");
                ResourcesAPI.AddProvider(new AssetBundleResourcesProvider(name, MainAssetBundle));
            }
        }

        // Add all the skins to load here
        public static void LoadAllSkins()
        {
            //CommandoTest.Init();
            TF2Engi.Init();
            //JotaroCaptain.Init();
            //StarPlatinumLoader.Init();

            //AnimationReplacements.RunAll();
            
            On.RoR2.SurvivorCatalog.Init += AddSkinReloader;
        }

        // Add component SkinReloader to all survivors
        private static void AddSkinReloader(On.RoR2.SurvivorCatalog.orig_Init orig)
        {
            orig();

            foreach (var item in SurvivorCatalog.allSurvivorDefs)
            {
                item.bodyPrefab.AddComponent<SkinReloader>();
            }
        }
    }
}
