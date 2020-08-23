using RoR2;

namespace MoistureUpset
{
    public static class SoundAssets
    {
        public static void RegisterSoundEvents()
        {
            On.RoR2.GlobalEventManager.ClientDamageNotified += GlobalEventManager_ClientDamageNotified;
            On.RoR2.GlobalEventManager.OnCharacterHitGround += GlobalEventManager_OnCharacterHitGround;
            On.RoR2.GlobalEventManager.ServerDamageDealt += GlobalEventManager_ServerDamageDealt;
        }

        private static void GlobalEventManager_ServerDamageDealt(On.RoR2.GlobalEventManager.orig_ServerDamageDealt orig, DamageReport damageReport)
        {
            orig(damageReport);

            if (damageReport.victimTeamIndex == TeamIndex.Player)
            {
                AkSoundEngine.PostEvent("MinecraftHurt", damageReport.victimBody.gameObject);
            }
        }

        private static void GlobalEventManager_OnCharacterHitGround(On.RoR2.GlobalEventManager.orig_OnCharacterHitGround orig, GlobalEventManager self, CharacterBody characterBody, UnityEngine.Vector3 impactVelocity)
        {
            float before = characterBody.healthComponent.health;
            orig(self, characterBody, impactVelocity);
            float after = characterBody.healthComponent.health;

            if (before != after)
            {
                AkSoundEngine.PostEvent("MinecraftCrunch", characterBody.gameObject);
            }
        }

        private static void GlobalEventManager_ClientDamageNotified(On.RoR2.GlobalEventManager.orig_ClientDamageNotified orig, DamageDealtMessage damageDealtMessage)
        {
            orig(damageDealtMessage);

            var playerList = NetworkUser.readOnlyLocalPlayersList;

            if (playerList == null)
            {
                return;
            }

            foreach (var item in playerList)
            {
                var mainBody = item.master?.GetBody();

                if (mainBody.gameObject == damageDealtMessage.victim)
                {
                    AkSoundEngine.PostEvent("MinecraftHurt", mainBody.gameObject);
                }
            }
        }
    }
}
