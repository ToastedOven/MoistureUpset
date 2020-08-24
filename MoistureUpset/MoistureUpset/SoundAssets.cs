using RoR2;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

namespace MoistureUpset
{
    public static class SoundAssets
    {
        private static NetworkUser[] users;

        private static bool[] belowThreshold;
        public static void RegisterSoundEvents()
        {
            On.RoR2.GlobalEventManager.OnCharacterHitGround += GlobalEventManager_OnCharacterHitGround;
            On.RoR2.GlobalEventManager.ServerDamageDealt += GlobalEventManager_ServerDamageDealt;
            On.RoR2.GlobalEventManager.OnCharacterDeath += GlobalEventManager_OnCharacterDeath;

            On.EntityStates.Engi.EngiWeapon.PlaceTurret.OnEnter += PlaceTurret_OnEnter;
        }

        private static void PlaceTurret_OnEnter(On.EntityStates.Engi.EngiWeapon.PlaceTurret.orig_OnEnter orig, EntityStates.Engi.EngiWeapon.PlaceTurret self)
        {
            orig(self);

            try
            {
                SoundNetworkAssistant.playSound("EngiBuildsTurret", self.outer.networkIdentity); 
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }
            
        }

        private static void GlobalEventManager_OnCharacterDeath(On.RoR2.GlobalEventManager.orig_OnCharacterDeath orig, GlobalEventManager self, DamageReport damageReport)
        {
            orig(self, damageReport);

            if (damageReport.victimMaster.GetBody().skinIndex == 2 && damageReport.victimBody.name == "EngiWalkerTurretBody(Clone)")
            {
                SoundNetworkAssistant.playSound("EngiTurretDies", damageReport.victimMaster.GetBody().networkIdentity);
            }

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
                        belowThreshold = new bool[users.Length];
                    }
                    bool failed = false;
                    int index = -1;

                    for (int i = 0; i < users.Length; i++)
                    {
                        if (damageReport.victimBody == users[i].master.GetBody())
                        {
                            if (users[i].master.GetBody() != null)
                            {
                                try
                                {
                                    index = i;
                                    SoundNetworkAssistant.playSound("MinecraftHurt", i);
                                }
                                catch
                                {
                                    failed = true;
                                }
                            }
                        }
                    }

                    if (damageReport.victimBody.name == "EngiBody(Clone)" && (damageReport.victim.health - damageReport.damageDealt) < (damageReport.victim.fullHealth * .3f) && damageReport.victimBody.skinIndex == 2)
                    {
                        try
                        {
                            if (index != -1)
                            {
                                SoundNetworkAssistant.playSound("EngiNeedsMedic", index);
                            }
                        }
                        catch (Exception e)
                        {
                            Debug.Log(e);
                        }
                    }
                    else if (damageReport.victimMaster.GetBody().skinIndex == 2 && (damageReport.victimBody.name == "EngiWalkerTurretBody(Clone)" || damageReport.victimBody.name == "EngiTurretBody(Clone)") && (damageReport.victim.health - damageReport.damageDealt) < 0)
                    {
                        try
                        {
                            SoundNetworkAssistant.playSound("EngiTurretDies", damageReport.victimMaster.GetBody().networkIdentity);
                        }
                        catch (Exception e)
                        {
                            Debug.Log(e);
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

                if (damageReport.victimBody.name == "EngiWalkerTurretBody(Clone)")
                {
                    Debug.Log(damageReport.victimMaster.minionOwnership.ownerMaster.GetBody().name);
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
