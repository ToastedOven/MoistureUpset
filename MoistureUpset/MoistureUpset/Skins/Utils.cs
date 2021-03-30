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
        public delegate SkinDef[] CreateSkin(GameObject bodyPrefab);

        private static bool Loaded = false;

        internal static Dictionary<string, SkinDef> skins;

        private static Dictionary<string, CreateSkin> skinDelegates;

        private static GameObject[] tempBodyPrefabs;

        //private static SurvivorDef[] tempSurvivorDefs;

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
        internal static void LoadAllSkins()
        {
            skins = new Dictionary<string, SkinDef>();
            skinDelegates = new Dictionary<string, CreateSkin>();

            On.RoR2.ContentManager.SetContentPacks += AddSkinsToBodyPrefabs;

            //CommandoTest.Init();
            TF2Engi.Init();
            //JotaroCaptain.Init();
            //StarPlatinumLoader.Init();
            //
            //

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

        internal static void AddSkin(string bodyName, CreateSkin del)
        {
            skinDelegates.Add(bodyName, del);
        }

        private static void AddSkinsToBodyPrefabs(On.RoR2.ContentManager.orig_SetContentPacks orig, List<ContentPack> newContentPacks)
        {
            if (Loaded)
                return;

            tempBodyPrefabs = newContentPacks[0].bodyPrefabs;

            //tempSurvivorDefs = newContentPacks[0].survivorDefs;

            DebugClass.Log($"Loading skins...");

            foreach (var bodyName in skinDelegates.Keys)
            {
                DebugClass.Log($"Loading skins for {newContentPacks[0].bodyPrefabs.GetBodyPrefab(bodyName).name}");

                SkinDef[] skinDefs = skinDelegates[bodyName].Invoke(newContentPacks[0].bodyPrefabs.GetBodyPrefab(bodyName));

                newContentPacks[0].bodyPrefabs[newContentPacks[0].bodyPrefabs.GetBodyIndex(bodyName)].GetComponent<ModelLocator>().modelTransform.GetComponent<ModelSkinController>().skins = skinDefs;

                skins.Add(bodyName, skinDefs[skinDefs.Length - 1]);
            }

            tempBodyPrefabs = Array.Empty<GameObject>();
            //tempSurvivorDefs = Array.Empty<SurvivorDef>();

            skins.Clear();
            skinDelegates.Clear();

            Loaded = true;

            DebugClass.Log($"Finished Loading skins!");

            orig(newContentPacks);
        }

        //internal static GameObject GetDisplayPrefab(string PrefabName)
        //{
        //    for (int i = 0; i < tempSurvivorDefs.Length; i++)
        //    {
        //        if (tempSurvivorDefs[i].displayPrefab.name.ToLower() == PrefabName.ToLower())
        //        {
        //            return tempSurvivorDefs[i].displayPrefab;
        //        }
        //    }

        //    DebugClass.Log($"Couldn't find DisplayPrefab: {PrefabName}!");
        //    return null;
        //}

        internal static GameObject GetBodyPrefab(string BodyName)
        {
            for (int i = 0; i < tempBodyPrefabs.Length; i++)
            {
                if (tempBodyPrefabs[i].name.ToLower() == BodyName.ToLower())
                {
                    return tempBodyPrefabs[i];
                }
            }

            DebugClass.Log($"Couldn't find BodyPrefab: {BodyName}!");
            return null;
        }

        private static GameObject GetBodyPrefab(this GameObject[] bodyPrefabs, string BodyName)
        {
            for (int i = 0; i < bodyPrefabs.Length; i++)
            {
                if (bodyPrefabs[i].name.ToLower() == BodyName.ToLower())
                {
                    return bodyPrefabs[i];
                }
            }

            DebugClass.Log($"Couldn't find BodyPrefab: {BodyName}!");
            return null;
        }

        private static int GetBodyIndex(this GameObject[] bodyPrefabs, string BodyName)
        {
            for (int i = 0; i < bodyPrefabs.Length; i++)
            {
                if (bodyPrefabs[i].name.ToLower() == BodyName.ToLower())
                {
                    return i;
                }
            }

            DebugClass.Log($"Couldn't find BodyIndex: {BodyName}!");
            return 0;
        }
    }
}
