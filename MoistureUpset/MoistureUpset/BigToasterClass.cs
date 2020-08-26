using BepInEx;
using R2API.Utils;
using RoR2;
using R2API;
using R2API.MiscHelpers;
using System.Reflection;
using static R2API.SoundAPI;
using System;
using UnityEngine;
using UnityEngine.Networking;

namespace MoistureUpset
{
    public static class BigToasterClass
    {
        public static void RunAll()
        {
            DeathSound();
            Somebody();
            BossMusic();
            BossMusicAndFanFare();
            Death();
            OnHit();
        }
        public static void OnHit()
        {
            On.RoR2.HealthComponent.TakeDamage += (orig, self, info) =>
            {
                orig(self, info);
                try
                {
                    if (info.attacker)
                    {
                        CharacterBody characterBody = info.attacker.GetComponent<CharacterBody>();
                        if (characterBody)
                        {
                            var mainBody = NetworkUser.readOnlyLocalPlayersList[0].master?.GetBody();
                            if (characterBody.teamComponent.teamIndex == TeamIndex.Player && info.attacker == mainBody)
                            {
                                SoundNetworkAssistant.playSound("HitMarker", info.attacker.transform.position);
                            }
                        }
                    }
                }
                catch (Exception e)
                {

                }
            };
        }
        public static void Death()
        {
            On.RoR2.Chat.PlayerDeathChatMessage.ConstructChatString += (orig, self) =>
            {
                string text = orig(self);
                return $"{text} <style=cDeath>loser alert.</style>";
            };
        }
        public static void DeathSound()
        {
            On.EntityStates.GenericCharacterDeath.PlayDeathSound += (orig, self) =>
            {
                Util.PlaySound("EDeath", self.outer.gameObject);
            };
        }
        public static void BossMusicAndFanFare()
        {
            On.RoR2.MusicController.UpdateTeleporterParameters += (orig, self, t, cT, tB) =>
            {
                try
                {
                    bool flag = true;
                    flag = t.holdoutZoneController.IsBodyInChargingRadius(tB);
                    AkSoundEngine.SetRTPCValue("isInPortalRange", (flag ? 1f : 0f));
                }
                catch (Exception e)
                {

                }
                orig(self, t, cT, tB);

            };
            On.RoR2.UI.ObjectivePanelController.FindTeleporterObjectiveTracker.ctor += (orig, self) =>//probably working
            {
                orig(self);
                Chat.AddMessage("fanfaren't time");
                try
                {
                    AkSoundEngine.SetRTPCValue("BossDead", 0f);
                }
                catch (Exception e)
                {

                }

            };
            On.RoR2.UI.ObjectivePanelController.FinishTeleporterObjectiveTracker.ctor += (orig, self) =>
            {//probably working
                orig(self);
                Chat.AddMessage("fanfare time");
                try
                {
                    var mainBody = NetworkUser.readOnlyLocalPlayersList[0].master?.GetBody();
                    AkSoundEngine.PostEvent("EndBossMusic", mainBody.gameObject);
                    AkSoundEngine.PostEvent("StopFanFare", mainBody.gameObject);
                    AkSoundEngine.SetRTPCValue("BossDead", 1f);
                    AkSoundEngine.PostEvent("PlayFanFare", mainBody.gameObject);
                }
                catch (Exception)
                {
                }
            };
            On.RoR2.BossGroup.OnDisable += (orig, self) =>//probably working
            {
                orig(self);
                try
                {
                    var mainBody = NetworkUser.readOnlyLocalPlayersList[0].master?.GetBody();
                    Util.PlaySound("BossDied", mainBody.gameObject);
                }
                catch (Exception)
                {
                }
            };
            On.RoR2.BossGroup.OnEnable += (orig, self) =>
            {
                orig(self);
                try
                {
                    var mainBody = NetworkUser.readOnlyLocalPlayersList[0].master?.GetBody();
                    AkSoundEngine.PostEvent("PlayBossMusic", mainBody.gameObject);
                }
                catch (Exception e)
                {

                }
            };
        }
        public static void Somebody()
        {
            On.EntityStates.SurvivorPod.PreRelease.OnEnter += (orig, self) =>
            {
                orig(self);
                Util.PlaySound("somebody", self.outer.gameObject);
            };
        }
        public static void BossMusic()
        {
            On.RoR2.WwiseUtils.CommonWwiseIds.Init += (orig) =>
            {
                orig();
                //RoR2.WwiseUtils.CommonWwiseIds.bossfight = AkSoundEngine.GetIDFromString("ooflongestloop");
                try
                {
                    RoR2.WwiseUtils.CommonWwiseIds.alive = AkSoundEngine.GetIDFromString("ooflongestloop");
                    RoR2.WwiseUtils.CommonWwiseIds.dead = AkSoundEngine.GetIDFromString("ooflongestloop");
                }
                catch (Exception)
                {
                }
                //RoR2.WwiseUtils.CommonWwiseIds.gameplay = AkSoundEngine.GetIDFromString("ooflongestloop");
                //RoR2.WwiseUtils.CommonWwiseIds.logbook = AkSoundEngine.GetIDFromString("ooflongestloop");
                //RoR2.WwiseUtils.CommonWwiseIds.main = AkSoundEngine.GetIDFromString("ooflongestloop");
                //RoR2.WwiseUtils.CommonWwiseIds.menu = AkSoundEngine.GetIDFromString("ooflongestloop");
                //RoR2.WwiseUtils.CommonWwiseIds.none = AkSoundEngine.GetIDFromString("ooflongestloop");
                //RoR2.WwiseUtils.CommonWwiseIds.secretLevel = AkSoundEngine.GetIDFromString("ooflongestloop");
            };
            //On.RoR2.WwiseUtils.SoundbankLoader.Start += (orig, self) =>
            //{
            //    //orig(self);
            //    for (int i = 0; i < self.soundbankStrings.Length; i++)
            //    {
            //        if (self.soundbankStrings[i] == "Music")
            //        {
            //            AkBankManager.LoadBank(self.soundbankStrings[i], self.decodeBank, self.saveDecodedBank);
            //            //AkBankManager.LoadBank("MusicReplacement", self.decodeBank, self.saveDecodedBank);
            //            //System.Console.WriteLine($"Loading MusicReplacement before {self.soundbankStrings[i]}");
            //        }
            //        else
            //        {
            //            AkBankManager.LoadBank(self.soundbankStrings[i], self.decodeBank, self.saveDecodedBank);
            //        }
            //    }
            //};
        }
    }
}
