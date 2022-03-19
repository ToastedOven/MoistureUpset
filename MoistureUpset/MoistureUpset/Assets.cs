using System;
using System.Collections.Generic;
using System.Reflection;
using static R2API.SoundAPI;
using UnityEngine;
using Object = UnityEngine.Object;
using UnityEngine.AddressableAssets;

namespace MoistureUpset
{
    public static class Assets
    {
        private static readonly string[] KnownExtensions = { "png", "exe", "txt", "xcf", "bat" };
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

            // string[] keywords = newMat.shaderKeywords;
            //
            // List<string> newKeyWords = new List<string>();
            //
            // foreach (var keyword in keywords)
            // {
            //     if (!string.Equals(keyword, "print_cutoff", StringComparison.InvariantCultureIgnoreCase))
            //         newKeyWords.Add(keyword);
            // }
            //
            // newMat.shaderKeywords = newKeyWords.ToArray();
            
            newMat.SetInt("_PrintOn", 0);
            newMat.SetInt("_LimbRemovalOn", 1);

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

        internal static void PopulateAssets()
        {
            string[] resourceNames = Assembly.GetExecutingAssembly().GetManifestResourceNames();

            foreach (var resource in resourceNames)
            {
                ResourceType resourceType = GetResourceType(resource);

                switch (resourceType)
                {
                    case ResourceType.AssetBundle:
                        DebugClass.Log($"Loading AssetBundle {resource}");
                        LoadAssetBundle(resource);
                        break;
                    case ResourceType.SoundBank:
                        DebugClass.Log($"Loading SoundBank {resource}");
                        LoadSoundBank(resource);
                        break;
                    case ResourceType.Other:
                        DebugClass.Log($"Loading Other {resource}");
                        // The majority of this stuff is manually loaded as needed.
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        private static ResourceType GetResourceType(string resourceName)
        {
            string[] split = resourceName.Split('.');

            if (split.Length <= 0)
                throw new Exception($"Invalid asset found: {resourceName}");

            string lastItem = split[split.Length - 1];

            if (lastItem == "bnk")
                return ResourceType.SoundBank;

            if (Array.IndexOf(KnownExtensions, lastItem) >= 0)
                return ResourceType.Other;

            return ResourceType.AssetBundle;
        }

        private enum ResourceType
        {
            AssetBundle,
            SoundBank,
            Other
        }

        private static void LoadAssetBundle(string location)
        {
            using var assetBundleStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(location);
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

            DebugClass.Log($"Loaded AssetBundle: {location}");
        }

        private static void LoadSoundBank(string location)
        {
            using var bankStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(location);
            var bytes = new byte[bankStream!.Length];
            bankStream.Read(bytes, 0, bytes.Length);
            SoundBanks.Add(bytes);
        }
        
        [Obsolete("AssetBundles are loaded automatically, calling this does literally nothing")]
        public static void AddBundle(string assetBundleLocation)
        {
            // Empty method because I don't want to go and remove stuff right now.
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
