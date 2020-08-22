using BepInEx;
using RoR2;
using R2API;
using R2API.MiscHelpers;
using R2API.Utils;
using System.Reflection;
using static R2API.SoundAPI;

namespace MoistureUpset
{
    [BepInDependency("com.bepis.r2api")]
    //Change these
    [BepInPlugin("com.WetBoys.WetGamers", "We are really wet.", "0.6.9")]
    [R2APISubmoduleDependency("SoundAPI")]
    public class BigTest : BaseUnityPlugin
    {
        public void Awake()
        {
            Assets.PopulateAssets();
            
            On.RoR2.UI.CharacterSelectController.SelectSurvivor += CharacterSelectController_SelectSurvivor;

            On.RoR2.Stats.StatManager.OnDamageDealt += StatManager_OnDamageDealt;
        }

        private void StatManager_OnDamageDealt(On.RoR2.Stats.StatManager.orig_OnDamageDealt orig, DamageReport damageReport)
        {
            orig(damageReport);

            var mainBody = NetworkUser.readOnlyLocalPlayersList[0].master?.GetBody();

            if (damageReport.victim.body == mainBody)
            {
                AkSoundEngine.PostEvent("MinecraftHurt", mainBody.gameObject);

                if (damageReport.isFallDamage)
                {
                    AkSoundEngine.PostEvent("MinecraftCrunch", mainBody.gameObject);
                }
            }
        }

        private void CharacterSelectController_SelectSurvivor(On.RoR2.UI.CharacterSelectController.orig_SelectSurvivor orig, RoR2.UI.CharacterSelectController self, SurvivorIndex survivor)
        {
            self.selectedSurvivorIndex = survivor;

            if (survivor == SurvivorIndex.Commando)
            {
                Chat.AddMessage("Commando Selected");
                Chat.AddMessage($"{self.characterDisplayPads[0].displayInstance.gameObject}");
                AkSoundEngine.PostEvent("YourMother", self.characterDisplayPads[0].displayInstance.gameObject);

            }
        }
    }
}