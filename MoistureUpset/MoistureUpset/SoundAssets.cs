using MoistureUpset.NetMessages;
using R2API.Networking.Interfaces;
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

        public static void PlaySound(string soundId, GameObject gameObject)
        {
            new SyncAudio(gameObject.GetComponent<NetworkIdentity>().netId, soundId).Send(R2API.Networking.NetworkDestination.Clients);
        }
        public static void PlaySound(string soundId, NetworkInstanceId netId)
        {
            new SyncAudio(netId, soundId).Send(R2API.Networking.NetworkDestination.Clients);
        }
        public static void PlayJotaroSound(string soundId, GameObject gameObject)
        {
            new SyncAudioWithJotaroSubtitles(gameObject.GetComponent<NetworkIdentity>().netId, soundId).Send(R2API.Networking.NetworkDestination.Clients);
        }

        private static CharacterMaster MasterSummon_Perform(On.RoR2.MasterSummon.orig_Perform orig, MasterSummon self)
        {
            CharacterMaster cm = orig(self);

            try
            {
                if (cm.minionOwnership.ownerMaster != null)
                {
                    if (cm.minionOwnership.ownerMaster.GetBody().isSkin("THE_TF2_ENGINEER_SKIN") && cm.minionOwnership.ownerMaster.GetBody().name == "EngiBody(Clone)" && (cm.name == "EngiWalkerTurretMaster(Clone)" || cm.name == "EngiTurretMaster(Clone)"))
                    {
                        PlaySound("EngiBuildsTurret", cm.minionOwnership.ownerMaster.GetBody().gameObject);

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
            try
            {
                if (TeleporterInteraction.instance != null)
                {
                    try
                    {
                        if (teleporter.holdoutZoneController.IsBodyInChargingRadius(targetBody))
                        {
                            if (!inPortal && !TeleporterInteraction.instance.isCharged)
                            {
                                inPortal = true;
                                PlaySound("EngiChargingTeleporter", targetBody.gameObject);
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
                                    portalFinished = true;
                                    PlaySound("EngiChargingTeleporter", targetBody.gameObject);

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
            }
            catch (Exception e)
            {

            }
            

            orig(self, teleporter, cameraTransform, targetBody);
        }

        private static void GlobalEventManager_OnCharacterDeath(On.RoR2.GlobalEventManager.orig_OnCharacterDeath orig, GlobalEventManager self, DamageReport damageReport)
        {
            try
            {

                if (damageReport.victimTeamIndex == TeamIndex.Player)
                {
                    //Debug.Log(damageReport.victimBody.name);
                    if (damageReport.victimBody.isSkin("THE_TF2_ENGINEER_SKIN") && damageReport.victimBody.name == "EngiBody(Clone)")
                    {
                        PlaySound("EngiDying", damageReport.victimBody.gameObject);
                    }
                    else if (damageReport.victimMaster.minionOwnership.ownerMaster.GetBody().isSkin("THE_TF2_ENGINEER_SKIN") && (damageReport.victimBody.name == "EngiWalkerTurretBody(Clone)" || damageReport.victimBody.name == "EngiTurretBody(Clone)"))
                    {
                        //Debug.Log(damageReport.attackerBody.name);
                    }
                }
                else if (damageReport.victimTeamIndex == TeamIndex.Monster)
                {
                    //DebugClass.Log($"{damageReport.attackerMaster.GetBody()}");
                    if (damageReport.attackerMaster.GetBody().name == "EngiBody(Clone)" && damageReport.attackerMaster.GetBody().isSkin("THE_TF2_ENGINEER_SKIN"))
                    {
                        try
                        {
                            PlaySound("EngiKillsSomething", damageReport.attackerBody.gameObject);
                        }
                        catch (Exception e)
                        {
                            //Debug.Log(e);
                        }
                    }
                    else if (damageReport.attackerMaster.GetBody().name == "CaptainBody(Clone)" && damageReport.attackerMaster.GetBody().skinIndex == 2)
                    {
                        try
                        {
                            PlayJotaroSound("JotaroKillsSomething", damageReport.attackerBody.gameObject);
                        }
                        catch (Exception sds)
                        {
                            DebugClass.Log(sds);
                        }
                        
                    }
                    else if (damageReport.attackerMaster.minionOwnership != null)
                    {
                        if (damageReport.attackerMaster.minionOwnership.ownerMaster.GetBody().isSkin("THE_TF2_ENGINEER_SKIN") && damageReport.attackerMaster.minionOwnership.ownerMaster.GetBody().name == "EngiBody(Clone)")
                        {
                            try
                            {
                                PlaySound("EngiTurretKillsSomething", damageReport.attackerMaster.minionOwnership.ownerMaster.GetBody().gameObject);
                                //if (index != -1)
                                //{
                                //    NetworkAssistant.playSound("EngiTurretKillsSomething", index);
                                //}
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
                    PlaySound("MinecraftHurt", damageReport.victimBody.gameObject);

                    try
                    {
                        if (damageReport.victimBody.name == "EngiBody(Clone)" && (damageReport.victim.health - damageReport.damageDealt) < (damageReport.victim.fullHealth * .3f) && damageReport.victimBody.isSkin("THE_TF2_ENGINEER_SKIN"))
                        {
                            try
                            {
                                PlaySound("EngiNeedsMedic", damageReport.victimBody.gameObject);
                            }
                            catch (Exception e)
                            {
                                //Debug.Log(e);
                            }
                        }
                        else if (damageReport.victimMaster.minionOwnership != null)
                        {
                            if (damageReport.victimMaster.minionOwnership.ownerMaster.GetBody().isSkin("THE_TF2_ENGINEER_SKIN") && (( damageReport.victim.combinedHealth + 5 )- (damageReport.damageDealt)) <= 0 && damageReport.victimMaster.minionOwnership.ownerMaster.GetBody().name == "EngiBody(Clone)" && damageReport.victim.combinedHealth > 0)
                            {
                                try
                                {
                                    //if (index != -1)
                                    //{
                                    //    //NetworkAssistant.playSound("EngiTurretDies", index);
                                    //}
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
