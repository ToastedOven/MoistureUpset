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
            try
            {
                if (damageReport.victimTeamIndex == TeamIndex.Player)
                {
                    if (users == null)
                    {
                        users = NetworkUser.readOnlyInstancesList.ToArray();
                    }
                    bool failed = false;

                    for (int i = 0; i < users.Length; i++)
                    {
                        if (damageReport.victimBody == users[i].master.GetBody())
                        {
                            if (users[i].master.GetBody() != null)
                            {
                                try
                                {
                                    SoundNetworkAssistant.playSound("MinecraftHurt", i);
                                }
                                catch
                                {
                                    failed = true;
                                }
                            }
                        }
                    }

                    if (failed)
                    {
                        if (damageReport.victimBody.networkIdentity.gameObject != null)
                        {
                            SoundNetworkAssistant.playSound("MinecraftHurt", damageReport.victimBody.master.networkIdentity);
                        }
                        else
                        {
                            try
                            {
                                SoundNetworkAssistant.playSound("MinecraftHurt", damageReport.victimBody.master.gameObject.transform.position);
                            }
                            catch (Exception e)
                            {
                                Debug.Log(e);
                            }
                        }
                    }
                }

                Debug.Log(damageReport.victimBody.name);
            }
            finally
            {
                orig(damageReport);
            }


            //if (damageReport.attackerBody.skinIndex == 2)
            //{

            //}
        }

        private static void GlobalEventManager_OnCharacterHitGround(On.RoR2.GlobalEventManager.orig_OnCharacterHitGround orig, GlobalEventManager self, CharacterBody characterBody, UnityEngine.Vector3 impactVelocity)
        {
            //float before = characterBody.healthComponent.health;
            orig(self, characterBody, impactVelocity);
            //float after = characterBody.healthComponent.health;

            //if (before != after)
            //{
            //    AkSoundEngine.PostEvent("MinecraftCrunch", characterBody.gameObject);
            //}
        }
    }
}
