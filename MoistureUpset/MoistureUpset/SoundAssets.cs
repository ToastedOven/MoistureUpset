using RoR2;
using System;
using System.Linq;
using UnityEngine;

namespace MoistureUpset
{
    public static class SoundAssets
    {
        private static NetworkUser[] users;
        public static void RegisterSoundEvents()
        {
            On.RoR2.GlobalEventManager.OnCharacterHitGround += GlobalEventManager_OnCharacterHitGround;
            On.RoR2.GlobalEventManager.ServerDamageDealt += GlobalEventManager_ServerDamageDealt;
            On.RoR2.GlobalEventManager.OnCharacterDeath += GlobalEventManager_OnCharacterDeath;
        }

        private static void GlobalEventManager_OnCharacterDeath(On.RoR2.GlobalEventManager.orig_OnCharacterDeath orig, GlobalEventManager self, DamageReport damageReport)
        {
            orig(self, damageReport);

            //SoundNetworkAssistant.playSound("EDeath", damageReport.victimBody.networkIdentity);
        }

        private static void GlobalEventManager_ServerDamageDealt(On.RoR2.GlobalEventManager.orig_ServerDamageDealt orig, DamageReport damageReport)
        {
            orig(damageReport);

            if (users == null)
            {
                users = NetworkUser.readOnlyInstancesList.ToArray();
            }

            int index = 0;

            for (int i = 0; i < users.Length; i++)
            {
                if (damageReport.victimBody == users[i].master.GetBody())
                {
                    index = i;
                    break;
                }
            }

            if (damageReport.victimTeamIndex == TeamIndex.Player)
            {
                SoundNetworkAssistant.playSound("MinecraftHurt", index);
            }

            if (damageReport.attackerBody.skinIndex == 2)
            {
                
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
    }
}
