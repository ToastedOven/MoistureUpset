using RoR2;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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

            On.RoR2.MasterSummon.Perform += MasterSummon_Perform;

            On.RoR2.MusicController.UpdateTeleporterParameters += MusicController_UpdateTeleporterParameters;
        }

        private static CharacterMaster MasterSummon_Perform(On.RoR2.MasterSummon.orig_Perform orig, MasterSummon self)
        {
            CharacterMaster cm = orig(self);

            try
            {
                if (cm.minionOwnership.ownerMaster != null)
                {
                    if (cm.minionOwnership.ownerMaster.GetBody().skinIndex == 2 && cm.minionOwnership.ownerMaster.GetBody().name == "EngiBody(Clone)" && (cm.name == "EngiWalkerTurretMaster(Clone)" || cm.name == "EngiTurretMaster(Clone)"))
                    {
                        for (int i = 0; i < NetworkUser.readOnlyInstancesList.Count; i++)
                        {
                            if (NetworkUser.readOnlyInstancesList[i].master.GetBody() == cm.minionOwnership.ownerMaster.GetBody())
                            {
                                NetworkAssistant.playSound("EngiBuildsTurret", i);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                //Debug.Log(e);
            }

            return cm;
        }

        private static void MusicController_UpdateTeleporterParameters(On.RoR2.MusicController.orig_UpdateTeleporterParameters orig, MusicController self, TeleporterInteraction teleporter, Transform cameraTransform, CharacterBody targetBody)
        {
            if (TeleporterInteraction.instance != null)
            {
                int index = -1;

                for (int i = 0; i < NetworkUser.readOnlyInstancesList.Count; i++)
                {
                    if (targetBody == NetworkUser.readOnlyInstancesList[i].master.GetBody() && targetBody.name == "EngiBody(Clone)")
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
                                NetworkAssistant.playSound("EngiChargingTeleporter", index);
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
                                    NetworkAssistant.playSound("EngiTeleporterComplete", index);
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            //Debug.Log(e);
                        }
                    }
                    else
                    {
                        portalFinished = false;
                    }
                }
                catch (Exception e)
                {
                    //Debug.Log(e);
                }
            }

            orig(self, teleporter, cameraTransform, targetBody);
        }

        private static void GlobalEventManager_OnCharacterDeath(On.RoR2.GlobalEventManager.orig_OnCharacterDeath orig, GlobalEventManager self, DamageReport damageReport)
        {
            try
            {
                int index = -1;

                for (int i = 0; i < NetworkUser.readOnlyInstancesList.Count; i++)
                {
                    if (damageReport.attackerBody == NetworkUser.readOnlyInstancesList[i].master.GetBody() || damageReport.attackerMaster.minionOwnership.ownerMaster.GetBody() == NetworkUser.readOnlyInstancesList[i].master.GetBody())
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
                        //Debug.Log(damageReport.victimBody.transform.position);
                        NetworkAssistant.playSound("EngiDying", damageReport.victimBody.transform.position);
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
                                NetworkAssistant.playSound("EngiKillsSomething", index);
                            }
                        }
                        catch (Exception e)
                        {
                            //Debug.Log(e);
                        }
                    }
                    else if (damageReport.attackerMaster.minionOwnership != null)
                    {
                        if (damageReport.attackerMaster.minionOwnership.ownerMaster.GetBody().skinIndex == 2 && damageReport.attackerMaster.minionOwnership.ownerMaster.GetBody().name == "EngiBody(Clone)")
                        {
                            try
                            {
                                if (index != -1)
                                {
                                    NetworkAssistant.playSound("EngiTurretKillsSomething", index);
                                }
                            }
                            catch (Exception e)
                            {
                                //Debug.Log(e);
                            }
                        }
                    }
                }
                
            }
            catch (Exception e)
            {
                //Debug.Log(e);
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
                        if (damageReport.victimBody == NetworkUser.readOnlyInstancesList[i].master.GetBody())
                        {
                            if (NetworkUser.readOnlyInstancesList[i].master.GetBody() != null)
                            {
                                try
                                {
                                    index = i;
                                    NetworkAssistant.playSound("MinecraftHurt", i);
                                }
                                catch
                                {

                                }
                            }
                        }
                        else if (damageReport.victimMaster.minionOwnership.ownerMaster.GetBody() == NetworkUser.readOnlyInstancesList[i].master.GetBody())
                        {
                            if (NetworkUser.readOnlyInstancesList[i].master.GetBody() != null)
                            {
                                try
                                {
                                    index = i;
                                    NetworkAssistant.playSound("MinecraftHurt", damageReport.victimBody.transform.position);
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
                                    NetworkAssistant.playSound("EngiNeedsMedic", index);
                                }
                            }
                            catch (Exception e)
                            {
                                //Debug.Log(e);
                            }
                        }
                        else if (damageReport.victimMaster.minionOwnership != null)
                        {
                            if (damageReport.victimMaster.minionOwnership.ownerMaster.GetBody().skinIndex == 2 && (( damageReport.victim.combinedHealth + 5 )- (damageReport.damageDealt)) <= 0 && damageReport.victimMaster.minionOwnership.ownerMaster.GetBody().name == "EngiBody(Clone)" && damageReport.victim.combinedHealth > 0)
                            {
                                try
                                {
                                    if (index != -1)
                                    {
                                        NetworkAssistant.playSound("EngiTurretDies", index);
                                    }
                                }
                                catch (Exception e)
                                {
                                    //Debug.Log(e);
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        //Debug.Log(e);
                    }
                }
            }
            catch (Exception e)
            {
                //Debug.Log(e);
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
