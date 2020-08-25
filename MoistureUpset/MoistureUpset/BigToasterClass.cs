using BepInEx;
using R2API.Utils;
using RoR2;
using R2API;
using R2API.MiscHelpers;
using System.Reflection;
using static R2API.SoundAPI;
using System;

namespace MoistureUpset
{
    public static class BigToasterClass
    {
        public static void RunAll()
        {
            DeathSound();
            Somebody();
            BossMusic();
            DropRewards();
        }
        public static void DeathSound()
        {
            On.EntityStates.GenericCharacterDeath.PlayDeathSound += (orig, self) =>
            {
                Util.PlaySound("EDeath", self.outer.gameObject);
            };
        }
        public static void DropRewards()
        {
            On.RoR2.MusicController.UpdateTeleporterParameters += (orig, self, t, cT, tB) =>
            {
                try
                {
                    bool flag = true;
                    flag = t.holdoutZoneController.IsBodyInChargingRadius(tB);
                    AkSoundEngine.SetRTPCValue("isInPortalRange", (flag ? 1f : 0f));
                    if (TeleporterInteraction.instance.isCharged)
                    {
                        var mainBody = NetworkUser.readOnlyLocalPlayersList[0].master?.GetBody();
                        AkSoundEngine.PostEvent("EndBossMusic", mainBody.gameObject);
                    }
                }
                catch (Exception)
                {

                }
                orig(self, t, cT, tB);

            };
            On.RoR2.BossGroup.DropRewards += (orig, self) =>
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
                catch (Exception)
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
