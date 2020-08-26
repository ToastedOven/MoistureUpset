using RoR2;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

namespace MoistureUpset
{
    public static class SoundAssets
    {
        private static bool inPortal = false;
        private static bool portalFinished = false;
        public static void RegisterSoundEvents()
        {
            On.RoR2.GlobalEventManager.OnCharacterHitGround += GlobalEventManager_OnCharacterHitGround;
            On.RoR2.GlobalEventManager.ServerDamageDealt += GlobalEventManager_ServerDamageDealt;
            On.RoR2.GlobalEventManager.OnCharacterDeath += GlobalEventManager_OnCharacterDeath;

            On.RoR2.MusicController.UpdateTeleporterParameters += MusicController_UpdateTeleporterParameters;

            On.EntityStates.Engi.EngiWeapon.PlaceTurret.OnEnter += PlaceTurret_OnEnter;
        }

        private static void MusicController_UpdateTeleporterParameters(On.RoR2.MusicController.orig_UpdateTeleporterParameters orig, MusicController self, TeleporterInteraction teleporter, Transform cameraTransform, CharacterBody targetBody)
        {
            if (TeleporterInteraction.instance != null)
            {
                int index = -1;

                for (int i = 0; i < NetworkUser.readOnlyInstancesList.Count; i++)
                {
                    if (targetBody == NetworkUser.readOnlyInstancesList[i].master.GetBody())
                    {
                        if (NetworkUser.readOnlyInstancesList[i].master.GetBody() != null)
                        {
                            index = i;
                        }
                    }
                }

                try
                {
                    if (teleporter.holdoutZoneController.IsBodyInChargingRadius(targetBody))
                    {
                        if (!inPortal && !TeleporterInteraction.instance.isCharged)
                        {
                            if (index != -1)
                            {
                                inPortal = true;
                                SoundNetworkAssistant.playSound("EngiChargingTeleporter", index);
                            }
                        }
                    }
                    else if (!teleporter.holdoutZoneController.IsBodyInChargingRadius(targetBody) && inPortal)
                    {
                        inPortal = false;
                    }

                    if (TeleporterInteraction.instance.isCharged)
                    {
                        try
                        {
                            if (!portalFinished)
                            {
                                if (index != -1)
                                {
                                    portalFinished = true;
                                    SoundNetworkAssistant.playSound("EngiTeleporterComplete", index);
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            Debug.Log(e);
                        }
                    }
                    else
                    {
                        portalFinished = false;
                    }
                }
                catch (Exception e)
                {
                    Debug.Log(e);
                }
            }

            orig(self, teleporter, cameraTransform, targetBody);
        }

        private static void PlaceTurret_OnEnter(On.EntityStates.Engi.EngiWeapon.PlaceTurret.orig_OnEnter orig, EntityStates.Engi.EngiWeapon.PlaceTurret self)
        {
            orig(self);

            try
            {
                //SoundNetworkAssistant.playSound("EngiBuildsTurret", self.outer.gameObject.); 
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }
            
        }

        private static void GlobalEventManager_OnCharacterDeath(On.RoR2.GlobalEventManager.orig_OnCharacterDeath orig, GlobalEventManager self, DamageReport damageReport)
        {
            try
            {
                int index = -1;

                for (int i = 0; i < NetworkUser.readOnlyInstancesList.Count; i++)
                {
                    if (damageReport.attackerBody == NetworkUser.readOnlyInstancesList[i].master.GetBody())
                    {
                        if (NetworkUser.readOnlyInstancesList[i].master.GetBody() != null)
                        {
                            try
                            {
                                index = i;
                            }
                            catch
                            {

                            }
                        }
                    }
                }

                if (damageReport.victimTeamIndex == TeamIndex.Player)
                {
                    if (damageReport.victimBody.skinIndex == 2 && damageReport.victimBody.name == "EngiBody(Clone)")
                    {
                        SoundNetworkAssistant.playSound("EngiDying", damageReport.victimBody.transform.position);
                    }
                }
                else if (damageReport.victimTeamIndex == TeamIndex.Monster)
                {
                    if (damageReport.attackerMaster.GetBody().name == "EngiBody(Clone)" && damageReport.attackerMaster.GetBody().skinIndex == 2)
                    {
                        try
                        {
                            if (index != -1)
                            {
                                SoundNetworkAssistant.playSound("EngiKillsSomething", index);
                            }
                        }
                        catch (Exception e)
                        {
                            Debug.Log(e);
                        }
                    }
                    //else if (damageReport.attackerMaster.minionOwnership != null)
                    //{
                    //    if (damageReport.attackerMaster.minionOwnership.ownerMaster.GetBody().skinIndex == 2 && damageReport.attackerMaster.minionOwnership.ownerMaster.GetBody().name == "EngiBody(Clone)" && (damageReport.victim.health - damageReport.damageDealt) <= 0 && damageReport.victim.health > 0)
                    //    {
                    //        try
                    //        {
                    //            if (index != -1)
                    //            {
                    //                Debug.Log(damageReport.attackerBody.name);
                    //                SoundNetworkAssistant.playSound("EngiTurretKillsSomething", index);
                    //            }
                    //        }
                    //        catch (Exception e)
                    //        {
                    //            Debug.Log(e);
                    //        }
                    //    }
                    //}
                }
                
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }
            finally
            {
                orig(self, damageReport);
            }
            
        }

        private static void GlobalEventManager_ServerDamageDealt(On.RoR2.GlobalEventManager.orig_ServerDamageDealt orig, DamageReport damageReport)
        {
            try
            {
                if (damageReport.victimTeamIndex == TeamIndex.Player)
                {
                    int index = -1;

                    for (int i = 0; i < NetworkUser.readOnlyInstancesList.Count; i++)
                    {
                        if (damageReport.victimBody == NetworkUser.readOnlyInstancesList[i].master.GetBody() || damageReport.victimMaster.minionOwnership.ownerMaster.GetBody() == NetworkUser.readOnlyInstancesList[i].master.GetBody())
                        {
                            if (NetworkUser.readOnlyInstancesList[i].master.GetBody() != null)
                            {
                                try
                                {
                                    index = i;
                                    SoundNetworkAssistant.playSound("MinecraftHurt", i);
                                }
                                catch
                                {

                                }
                            }
                        }
                    }
                    try
                    {
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
                        //else if (damageReport.victimMaster.minionOwnership != null)
                        //{
                        //    if (damageReport.victimMaster.minionOwnership.ownerMaster.GetBody().skinIndex == 2 && (damageReport.victim.health - (damageReport.damageDealt)) <= 0 && damageReport.victimMaster.minionOwnership.ownerMaster.GetBody().name == "EngiBody(Clone)" && damageReport.victim.health > 0)
                        //    {
                        //        try
                        //        {
                        //            Debug.Log($"brh {index}");
                        //            if (index != -1)
                        //            {
                        //                SoundNetworkAssistant.playSound("EngiTurretDies", index);
                        //            }
                        //        }
                        //        catch (Exception e)
                        //        {
                        //            Debug.Log(e);
                        //        }
                        //    }
                        //}
                    }
                    catch (Exception e)
                    {
                        Debug.Log(e);
                    }
                }
            }
            finally
            {
                orig(damageReport);
            }

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
