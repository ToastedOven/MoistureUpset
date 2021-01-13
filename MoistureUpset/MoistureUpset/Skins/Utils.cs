using R2API;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace MoistureUpset.Skins
{
    public static class Utils
    {
        public static void LoadAsset(string ResourceStream, string name = null)
        {
            using (var assetStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(ResourceStream))
            {
                var MainAssetBundle = AssetBundle.LoadFromStream(assetStream);

                if (name == null)
                {
                    ResourcesAPI.AddProvider(new AssetBundleResourcesProvider($"@MoistureUpset_{ResourceStream.Replace("MoustureUpset.", "")}", MainAssetBundle));
                }
                else
                {
                    ResourcesAPI.AddProvider(new AssetBundleResourcesProvider(name, MainAssetBundle));
                }
            }
        }
    }
}
