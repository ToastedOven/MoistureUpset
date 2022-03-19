using R2API;
using RoR2;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using HG;
using R2API.Utils;
using RoR2.ContentManagement;
using UnityEngine;

namespace MoistureUpset.Skins
{
    public static class SkinManager
    {
        public delegate SkinDef[] CreateSkin(GameObject bodyPrefab);

        internal static Dictionary<string, SkinDef> skins;

        private static Dictionary<string, CreateSkin> skinDelegates;

        internal static void Init()
        {
            LoadAllSkins();
            
            ContentManager.onContentPacksAssigned += FinalizeSkins;
        }

        private static void ExtractItems(GameObject[] bodyPrefabs)
        {
            var engiBody = bodyPrefabs.GetBodyPrefab("EngiBody");

            ItemDisplayRuleOverrides.ExportRuleSet(engiBody);
        }

        private static void FinalizeSkins(ReadOnlyArray<ReadOnlyContentPack> obj)
        {
            //ExtractItems(ContentManager._bodyPrefabs);
            AddSkinsToBodyPrefabs(ContentManager._bodyPrefabs);
        }

        // Add all the skins to load here
        private static void LoadAllSkins()
        {
            skins = new Dictionary<string, SkinDef>();
            skinDelegates = new Dictionary<string, CreateSkin>();


            //CommandoTest.Init();
            TF2Engi.Init();
            //JotaroCaptain.Init();
            //StarPlatinumLoader.Init();
            //

            AnimationReplacements.RunAll();

            On.RoR2.SurvivorCatalog.Init += AddSkinReloader;
        }

        private static void AddSkinsToBodyPrefabs(GameObject[] bodyPrefabs)
        {
            DebugClass.Log($"Loading skins...");
            
            foreach (var bodyName in skinDelegates.Keys)
            {
                DebugClass.Log($"Loading skins for {bodyPrefabs.GetBodyPrefab(bodyName).name}");

                SkinDef[] skinDefs = skinDelegates[bodyName].Invoke(bodyPrefabs.GetBodyPrefab(bodyName));

                bodyPrefabs[bodyPrefabs.GetBodyIndex(bodyName)].GetComponent<ModelLocator>().modelTransform.GetComponent<ModelSkinController>().skins = skinDefs;

                skins.Add(bodyName, skinDefs[skinDefs.Length - 1]);
            }
            
            skins.Clear();
            skinDelegates.Clear();
            
            DebugClass.Log($"Finished Loading skins!");
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

        //private static void AddSkinsToBodyPrefabs(ContentManager.orig_SetContentPacks orig, List<ReadOnlyContentPack> newContentPacks)
        //{
        //    if (Loaded)
        //        orig(newContentPacks);

        //    tempBodyPrefabs = newContentPacks[0].bodyPrefabs;

        //    //tempSurvivorDefs = newContentPacks[0].survivorDefs;

        //    DebugClass.Log($"Loading skins...");

        //    foreach (var bodyName in skinDelegates.Keys)
        //    {
        //        DebugClass.Log($"Loading skins for {newContentPacks[0].bodyPrefabs.GetBodyPrefab(bodyName).name}");

        //        SkinDef[] skinDefs = skinDelegates[bodyName].Invoke(newContentPacks[0].bodyPrefabs.GetBodyPrefab(bodyName));

        //        newContentPacks[0].bodyPrefabs[newContentPacks[0].bodyPrefabs.GetBodyIndex(bodyName)].GetComponent<ModelLocator>().modelTransform.GetComponent<ModelSkinController>().skins = skinDefs;

        //        skins.Add(bodyName, skinDefs[skinDefs.Length - 1]);
        //    }

        //    tempBodyPrefabs = Array.Empty<GameObject>();
        //    //tempSurvivorDefs = Array.Empty<SurvivorDef>();

        //    skins.Clear();
        //    skinDelegates.Clear();

        //    Loaded = true;

        //    DebugClass.Log($"Finished Loading skins!");

        //    orig(newContentPacks);
        //}

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

        internal static GameObject GetBodyPrefab(string bodyName)
        {
            for (int i = 0; i < ContentManager._bodyPrefabs.Length; i++)
            {
                if (string.Equals(ContentManager._bodyPrefabs[i].name, bodyName, StringComparison.InvariantCultureIgnoreCase))
                {
                    return ContentManager._bodyPrefabs[i];
                }
            }

            DebugClass.Log($"Couldn't find BodyPrefab: {bodyName}!");
            return null;
        }

        private static GameObject GetBodyPrefab(this GameObject[] bodyPrefabs, string bodyName)
        {
            for (int i = 0; i < bodyPrefabs.Length; i++)
            {
                if (string.Equals(bodyPrefabs[i].name, bodyName, StringComparison.InvariantCultureIgnoreCase))
                {
                    return bodyPrefabs[i];
                }
            }

            DebugClass.Log($"Couldn't find BodyPrefab: {bodyName}!");
            return null;
        }

        private static int GetBodyIndex(this GameObject[] bodyPrefabs, string bodyName)
        {
            for (int i = 0; i < bodyPrefabs.Length; i++)
            {
                if (string.Equals(bodyPrefabs[i].name, bodyName, StringComparison.InvariantCultureIgnoreCase))
                {
                    return i;
                }
            }

            DebugClass.Log($"Couldn't find BodyIndex: {bodyName}!");
            return 0;
        }
    }
}
