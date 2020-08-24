using BepInEx;
using R2API.Utils;
using RoR2;
using R2API;
using R2API.MiscHelpers;
using System.Reflection;
using static R2API.SoundAPI;
using UnityEngine;
using System;

namespace MoistureUpset
{
    [BepInDependency("com.bepis.r2api")]
    //Change these
    [BepInPlugin("com.WetBoys.WetGamers", "We are really wet.", "0.6.9")]
    [R2APISubmoduleDependency("SoundAPI")]
    [R2APISubmoduleDependency(nameof(PrefabAPI), nameof(LoadoutAPI), nameof(SurvivorAPI), nameof(ResourcesAPI))]
    public class BigTest : BaseUnityPlugin
    {
        public void Awake()
        {
            using (var assetStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("MoistureUpset.tf8"))
            {
                var MainAssetBundle = AssetBundle.LoadFromStream(assetStream);
                //This string value will be used as a part of resource path. Prefferably it shoudl be mod name
                ResourcesAPI.AddProvider(new AssetBundleResourcesProvider("@MoistureUpset", MainAssetBundle));
            }
            SkinTest.SkinTest.AddLumberJackSkin();


            Assets.PopulateAssets();

            SoundAssets.RegisterSoundEvents();
            
            On.RoR2.UI.CharacterSelectController.SelectSurvivor += CharacterSelectController_SelectSurvivor;

            BigToasterClass.RunAll();
        }



        private void CharacterSelectController_SelectSurvivor(On.RoR2.UI.CharacterSelectController.orig_SelectSurvivor orig, RoR2.UI.CharacterSelectController self, SurvivorIndex survivor)
        {
            self.selectedSurvivorIndex = survivor;

            if (survivor == SurvivorIndex.Commando)
            {
                AkSoundEngine.PostEvent("YourMother", self.characterDisplayPads[0].displayInstance.gameObject);
            }
        }
    }
}
