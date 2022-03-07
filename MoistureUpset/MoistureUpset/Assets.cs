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

namespace MoistureUpset
{
    public static class Assets
    {
        private static Material _prefab;

        private static List<AssetBundle> _assetBundles = new List<AssetBundle>();
        private static Dictionary<string, int> _assetIndices = new Dictionary<string, int>();

        public static Material CreateMaterial(string texture)
        {
            if (!_prefab)
                _prefab = GameObject.Instantiate<Material>(Resources.Load<GameObject>("prefabs/characterbodies/commandobody").GetComponentInChildren<SkinnedMeshRenderer>().material);

            Material newMat = GameObject.Instantiate<Material>(_prefab);

            newMat.mainTexture = Resources.Load<Texture>(texture);

            newMat.SetColor("_Color", Color.white);
            newMat.SetFloat("_EmPower", 0f);
            newMat.SetColor("_EmColor", Color.white);
            newMat.SetTexture("_EmTex", null);
            newMat.SetFloat("_NormalStrength", 0.5f);
            newMat.SetTexture("_NormalTex", null);

            return newMat;
        }

        public static Material CopyMaterial(Texture texture)
        {
            if (!_prefab)
                _prefab = GameObject.Instantiate<Material>(Resources.Load<GameObject>("prefabs/characterbodies/commandobody").GetComponentInChildren<SkinnedMeshRenderer>().material);

            Material newMat = GameObject.Instantiate<Material>(_prefab);

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
        
        public static void AddBundle(string assetBundleLocation)
        {
            using var assetBundleStream = Assembly.GetExecutingAssembly().GetManifestResourceStream($"MoistureUpset.{assetBundleLocation}");
            AssetBundle assetBundle = AssetBundle.LoadFromStream(assetBundleStream);

            int index = _assetBundles.Count;
            _assetBundles.Add(assetBundle);

            
            // TODO remove "assets/" from the path
            foreach (var assetName in assetBundle.GetAllAssetNames())
                _assetIndices[assetName] = index;
            
            DebugClass.Log($"Loaded Asset: {assetBundleLocation}");
        }

        public static T Load<T>(string assetName) where T : UnityEngine.Object
        {
            if (assetName.StartsWith("MoistureUpset."))
                assetName = assetName.Replace("MoistureUpset.", "");
            
            if (assetName.StartsWith("MoistureUpset_"))
                assetName = assetName.Replace("MoistureUpset_", "");

            int index = _assetIndices[assetName];

            return _assetBundles[index].LoadAsset<T>(assetName);
        }
    }
}
