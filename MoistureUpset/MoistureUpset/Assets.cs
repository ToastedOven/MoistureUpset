using System;
using System.Collections.Generic;
using BepInEx;
using RoR2;
using R2API;
using R2API.MiscHelpers;
using R2API.Utils;
using System.Reflection;
using static R2API.SoundAPI;
using UnityEngine;
using Object = UnityEngine.Object;
using UnityEngine.AddressableAssets;

namespace MoistureUpset
{
    public static class Assets
    {
        private static readonly List<AssetBundle> AssetBundles = new List<AssetBundle>();
        private static readonly Dictionary<string, int> AssetIndices = new Dictionary<string, int>();
        
        private static Material _prefab;

        public static Material LoadMaterial(string texture)
        {
            if (!_prefab)
                _prefab = Object.Instantiate<Material>(Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Commando/CommandoBody.prefab").WaitForCompletion().GetComponentInChildren<SkinnedMeshRenderer>().material);

            Material newMat = Object.Instantiate<Material>(_prefab);

            newMat.mainTexture = Load<Texture>(texture);

            newMat.SetColor("_Color", Color.white);
            newMat.SetFloat("_EmPower", 0f);
            newMat.SetColor("_EmColor", Color.white);
            newMat.SetTexture("_EmTex", null);
            newMat.SetTexture("_FresnelRamp", null);
            newMat.SetFloat("_NormalStrength", 0.5f);
            newMat.SetTexture("_NormalTex", null);

            newMat.name = texture;

            return newMat;
        }

        public static Material RobloxMaterial(string texture)
        {
            if (!_prefab)
                _prefab = Object.Instantiate<Material>(Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Golem/GolemBody.prefab").WaitForCompletion().GetComponentInChildren<SkinnedMeshRenderer>().material);

            Material newMat = Object.Instantiate<Material>(_prefab);

            newMat.mainTexture = Load<Texture>(texture);

            newMat.SetColor("_Color", Color.white);
            newMat.SetFloat("_EmPower", 0f);
            newMat.SetColor("_EmColor", Color.white);
            newMat.SetTexture("_EmTex", null);
            newMat.SetTexture("_FresnelRamp", null);
            newMat.SetFloat("_NormalStrength", 0.5f);
            newMat.SetTexture("_NormalTex", null);

            newMat.name = texture;

            return newMat;
        }

        public static Material CopyMaterial(Texture texture)
        {
            if (!_prefab)
                _prefab = Object.Instantiate<Material>(Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Commando/CommandoBody.prefab").WaitForCompletion().GetComponentInChildren<SkinnedMeshRenderer>().material);

            Material newMat = Object.Instantiate<Material>(_prefab);

            newMat.mainTexture = texture;

            newMat.SetColor("_Color", Color.white);
            newMat.SetFloat("_EmPower", 0f);
            newMat.SetColor("_EmColor", Color.white);
            newMat.SetTexture("_EmTex", null);
            newMat.SetFloat("_NormalStrength", 0.5f);
            newMat.SetTexture("_NormalTex", null);

            return newMat;
        }

        public static void PopulateAssets()
        {
            using (var bankStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("MoistureUpset.ImMoist.bnk"))
            {
                var bytes = new byte[bankStream.Length];
                bankStream.Read(bytes, 0, bytes.Length);

                SoundBanks.Add(bytes);
            }

            using (var bankStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("MoistureUpset.ImReallyMoist.bnk"))
            {
                var bytes = new byte[bankStream.Length];
                bankStream.Read(bytes, 0, bytes.Length);

                SoundBanks.Add(bytes);
            }

            using (var bankStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("MoistureUpset.Risk2GaySounds.bnk"))
            {
                var bytes = new byte[bankStream.Length];
                bankStream.Read(bytes, 0, bytes.Length);

                SoundBanks.Add(bytes);
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="assetBundleLocation"></param>
        public static void AddBundle(string assetBundleLocation)
        {
            using var assetBundleStream = Assembly.GetExecutingAssembly().GetManifestResourceStream($"MoistureUpset.{assetBundleLocation}");
            AssetBundle assetBundle = AssetBundle.LoadFromStream(assetBundleStream);

            int index = AssetBundles.Count;
            AssetBundles.Add(assetBundle);

            foreach (var assetName in assetBundle.GetAllAssetNames())
            {
                string path = assetName.ToLower();
                
                if (path.StartsWith("assets/"))
                    path = path.Remove(0, "assets/".Length);
                AssetIndices[path] = index;
            }

            DebugClass.Log($"Loaded AssetBundle: {assetBundleLocation}");
        }

        public static T Load<T>(string assetName) where T : UnityEngine.Object
        {
            if (assetName.Contains(":"))
            {
                string[] path = assetName.Split(':');

                assetName = path[1].ToLower();
            }
            if (assetName.StartsWith("assets/"))
                assetName = assetName.Remove(0, "assets/".Length);
            int index = AssetIndices[assetName];
            return AssetBundles[index].LoadAsset<T>($"assets/{assetName}");
        }
    }
}
