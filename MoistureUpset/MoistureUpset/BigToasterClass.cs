﻿using BepInEx;
using R2API.Utils;
using RoR2;
using R2API;
using R2API.MiscHelpers;
using System.Reflection;
using static R2API.SoundAPI;
using System;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

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
            ModifyChat();
            PreGame();
            INeedToSortThese();
        }
        public static void INeedToSortThese()
        {
        }
        public static void PreGame()
        {
            On.RoR2.UI.PregameCharacterSelection.Awake += (orig, self) =>
            {
                orig(self);
                AkSoundEngine.SetRTPCValue("MainMenuMusic", 0);
            };
            On.RoR2.SceneCatalog.OnActiveSceneChanged += (orig, oldS, newS) =>
            {
                orig(oldS, newS);
                try
                {
                    switch (newS.name)
                    {
                        case "logbook":
                            AkSoundEngine.SetRTPCValue("MainMenuMusic", 0f);
                            break;
                        case "title":
                            AkSoundEngine.SetRTPCValue("MainMenuMusic", 1);
                            AkSoundEngine.SetRTPCValue("LobbyActivated", 1);
                            break;
                        case "lobby":
                            AkSoundEngine.SetRTPCValue("LobbyActivated", 1);
                            AkSoundEngine.SetRTPCValue("MainMenuMusic", 0f);
                            break;
                        case "splash":
                            AkSoundEngine.PostEvent("PlayMainMenu", GameObject.FindObjectOfType<GameObject>());
                            AkSoundEngine.SetRTPCValue("MainMenuMusic", 0);
                            break;
                        default:
                            AkSoundEngine.SetRTPCValue("LobbyActivated", 0);
                            break;
                    }
                }
                catch (Exception)
                {
                }
                AkSoundEngine.SetRTPCValue("BossMusicActive", 0);
                //logbook
                //title
                //lobby
                //
                //
                //
            };
            On.RoR2.MusicController.UpdateState += (orig, self) =>
            {
                //muMenu
                orig(self);
                try
                {
                    string song = self.GetPropertyValue<MusicTrackDef>("currentTrack").cachedName;

                    if (song == "muMenu" || song == "muLogbook")
                    {
                        self.GetPropertyValue<MusicTrackDef>("currentTrack").Stop();
                    }

                    //muFULLSong07
                    //muFULLSong18
                    //muSong04

                    if (MusicAPI.ReplaceSong(ref self, "muSong04", "PlayShopMusic"))
                    {
                        AkSoundEngine.SetRTPCValue("BossDead", 0f);
                    }
                    //Debug.Log($"--------------{song}");
                }
                catch (Exception)
                {
                }
            };
            On.EntityStates.SpawnTeleporterState.OnEnter += (orig, self) =>
            {
                orig(self);
                try
                {
                    var c = GameObject.FindObjectOfType<MusicController>();
                    MusicAPI.StopCustomSong(ref c, "StopShopMusic");
                }
                catch (Exception)
                {
                }
            };
            On.RoR2.UI.MainMenu.MainMenuController.Update += (orig, self) =>
            {
                orig(self);
                UImods.ReplaceUIObject("LogoImage", "MoistureUpset.Resources.MoistureUpsetFinal.png");
                UImods.ReplaceUIObject("MousePointer", "MoistureUpset.Resources.robloxhover.png");
                UImods.ReplaceUIObject("MouseHover", "MoistureUpset.Resources.roblox.png");
            };
            On.RoR2.UI.MainMenu.MainMenuController.SetDesiredMenuScreen += (orig, self, menu) =>
            {
                orig(self, menu);
                try
                {
                    if (menu.name != "TitleMenu")
                    {
                        AkSoundEngine.SetRTPCValue("MainMenuMusic", 0.3f);
                    }
                    else
                    {
                        AkSoundEngine.SetRTPCValue("MainMenuMusic", 1);
                    }
                }
                catch (Exception)
                {
                }
            };
            //On.RoR2.TeleporterInteraction.AttemptToSpawnAllEligiblePortals += (orig, self) =>
            //{
            //    self.shouldAttemptToSpawnShopPortal = true;
            //    orig(self);
            //};
        }
        public static void ModifyChat()
        {
            On.RoR2.UI.ChatBox.SubmitChat += (orig, self) =>
            {
                //DebugClass.GetAllGameObjects();
                bool sendmessage = true;
                try
                {
                    int num = -1;
                    string[] text = self.inputField.text.ToUpper().Split(' ');
                    if (text[0] == "HITMARKER" || text[0] == "HITSOUND")
                    {
                        num = Int32.Parse(text[1]);
                    }
                    else if (text[0] == "HIT" && (text[1] == "SOUND" || text[1] == "MARKER"))
                    {
                        num = Int32.Parse(text[2]);
                    }
                    else if (text[0] == "HITMARKERVOLUME")
                    {
                        if (!Directory.Exists(@"BepInEx\plugins\MoistureUpset"))
                        {
                            Directory.CreateDirectory(@"BepInEx\plugins\MoistureUpset");
                        }
                        if (File.Exists(@"BepInEx\plugins\MoistureUpset\HitMarkerNoise.BlameRuneForThis"))
                        {
                            string line;
                            using (StreamReader r = new StreamReader(@"BepInEx\plugins\MoistureUpset\HitMarkerNoise.BlameRuneForThis"))
                            {
                                line = r.ReadToEnd();
                            }
                            int readnum = Int32.Parse(line);
                            Chat.AddMessage($"HitMarkerVolume: {readnum}");
                        }
                        sendmessage = false;
                    }
                    else if (text[0] == "HELP")
                    {
                        Chat.AddMessage("-Type 'hitmarker' followed by a number 0-100 to change the hitmarker volume\n-Type 'hitmarkervolume' to check the volume of the hitmarker");
                        sendmessage = false;
                    }
                    //else if (text[0] == "TOGGLERUNMUSIC")
                    //{
                    //    if (!Directory.Exists(@"BepInEx\plugins\MoistureUpset"))
                    //    {
                    //        Directory.CreateDirectory(@"BepInEx\plugins\MoistureUpset");
                    //    }
                    //    if (File.Exists(@"BepInEx\plugins\MoistureUpset\CustomRunMusic.BlameRuneForThis"))
                    //    {
                    //        string line;
                    //        using (StreamReader r = new StreamReader(@"BepInEx\plugins\MoistureUpset\CustomRunMusic.BlameRuneForThis"))
                    //        {
                    //            line = r.ReadToEnd();
                    //        }
                    //        int readnum = Int32.Parse(line);
                    //        readnum = (readnum == 0 ? 1 : 0);
                    //        Chat.AddMessage($"CustomRunMusic: " + (readnum == 0 ? "false, changes will take place starting next level" : "true"));
                    //        AkSoundEngine.SetRTPCValue("CustomRunMusic", readnum);
                    //        File.WriteAllText(@"BepInEx\plugins\MoistureUpset\CustomRunMusic.BlameRuneForThis", string.Empty);
                    //        using (StreamWriter r = File.CreateText(@"BepInEx\plugins\MoistureUpset\CustomRunMusic.BlameRuneForThis"))
                    //        {
                    //            r.Write(readnum);
                    //        }
                    //    }
                    //    sendmessage = false;
                    //}
                    if (num != -1)
                    {
                        if (!Directory.Exists(@"BepInEx\plugins\MoistureUpset"))
                        {
                            Directory.CreateDirectory(@"BepInEx\plugins\MoistureUpset");
                        }
                        AkSoundEngine.SetRTPCValue("RuneBadNoise", num);
                        File.WriteAllText(@"BepInEx\plugins\MoistureUpset\HitMarkerNoise.BlameRuneForThis", string.Empty);
                        using (StreamWriter r = File.CreateText(@"BepInEx\plugins\MoistureUpset\HitMarkerNoise.BlameRuneForThis"))
                        {
                            r.Write(num);
                        }
                        sendmessage = false;
                    }
                }
                catch (Exception)
                {
                }
                if (!sendmessage)
                {
                    self.inputField.text = "";
                }
                orig(self);
            };
        }
        public static void OnHit()
        {
            On.RoR2.UI.CrosshairManager.RefreshHitmarker += (orig, self, crit) =>
            {
                if (!crit)
                {
                    AkSoundEngine.PostEvent("HitMarker", RoR2Application.instance.gameObject);
                }
                else
                {
                    crit = false;
                    AkSoundEngine.PostEvent("CritMarker", RoR2Application.instance.gameObject);
                }
                orig(self, crit);
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
                catch (Exception)
                {

                }
                orig(self, t, cT, tB);

            };
            On.RoR2.UI.ObjectivePanelController.FindTeleporterObjectiveTracker.ctor += (orig, self) =>
            {
                orig(self);
                try
                {
                    AkSoundEngine.SetRTPCValue("BossDead", 0f);
                }
                catch (Exception)
                {

                }

            };
            On.RoR2.UI.ObjectivePanelController.ActivateGoldshoreBeaconTracker.ctor += (orig, self) =>
            {
                orig(self);
                try
                {
                    AkSoundEngine.SetRTPCValue("BossDead", 0f);
                }
                catch (Exception)
                {

                }

            };
            On.RoR2.UI.ObjectivePanelController.DestroyTimeCrystals.ctor += (orig, self) =>
            {
                orig(self);
                try
                {
                    AkSoundEngine.SetRTPCValue("BossDead", 0f);
                }
                catch (Exception)
                {

                }

            };
            On.RoR2.UI.ObjectivePanelController.FinishTeleporterObjectiveTracker.ctor += (orig, self) =>
            {
                orig(self);
                try
                {
                    var c = GameObject.FindObjectOfType<MusicController>();
                    var mainBody = NetworkUser.readOnlyLocalPlayersList[0].master?.GetBody();
                    AkSoundEngine.PostEvent("EndBossMusic", c.gameObject);
                    AkSoundEngine.PostEvent("StopFanFare", c.gameObject);
                    AkSoundEngine.SetRTPCValue("BossDead", 1f);
                    AkSoundEngine.PostEvent("PlayFanFare", c.gameObject);
                }
                catch (Exception)
                {
                }
            };
            On.RoR2.BossGroup.OnMemberLost += (orig, self, master) =>
            {
                orig(self, master);
                try
                {
                    var mainBody = NetworkUser.readOnlyLocalPlayersList[0].master?.GetBody();
                    var c = GameObject.FindObjectOfType<MusicController>();
                    if (self.combatSquad.readOnlyMembersList.Count == 0)
                    {
                        Util.PlaySound("BossDied", c.gameObject);
                    }
                }
                catch (Exception)
                {
                }
            };

            On.RoR2.BossGroup.DefeatBossObjectiveTracker.ctor += (orig, self) =>
            {
                orig(self);
                try
                {
                    var c = GameObject.FindObjectOfType<MusicController>();
                    var mainBody = NetworkUser.readOnlyLocalPlayersList[0].master?.GetBody();
                    AkSoundEngine.PostEvent("EndBossMusic", c.gameObject);
                    AkSoundEngine.SetRTPCValue("BossMusicActive", 1);
                    AkSoundEngine.PostEvent("PlayBossMusic", c.gameObject);
                    var con = GameObject.FindObjectOfType<MusicController>();
                    MusicAPI.StopCustomSong(ref con, "StopLevelMusic");
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
                //loading the hitmarker noise cause this spot seemed like a good idea
                try
                {
                    if (!Directory.Exists(@"BepInEx\plugins\MoistureUpset"))
                    {
                        Directory.CreateDirectory(@"BepInEx\plugins\MoistureUpset");
                    }
                    if (File.Exists(@"BepInEx\plugins\MoistureUpset\HitMarkerNoise.BlameRuneForThis"))
                    {
                        string line;
                        using (StreamReader r = new StreamReader(@"BepInEx\plugins\MoistureUpset\HitMarkerNoise.BlameRuneForThis"))
                        {
                            line = r.ReadToEnd();
                        }
                        int readnum = Int32.Parse(line);
                        AkSoundEngine.SetRTPCValue("RuneBadNoise", readnum);
                    }
                    else
                    {
                        using (StreamWriter r = File.CreateText(@"BepInEx\plugins\MoistureUpset\HitMarkerNoise.BlameRuneForThis"))
                        {
                            r.Write(100);
                            AkSoundEngine.SetRTPCValue("RuneBadNoise", 100);
                        }
                    }
                    if (File.Exists(@"BepInEx\plugins\MoistureUpset\CustomRunMusic.BlameRuneForThis"))
                    {
                        string line;
                        using (StreamReader r = new StreamReader(@"BepInEx\plugins\MoistureUpset\CustomRunMusic.BlameRuneForThis"))
                        {
                            line = r.ReadToEnd();
                        }
                        int readnum = Int32.Parse(line);
                        AkSoundEngine.SetRTPCValue("CustomRunMusic", readnum);
                    }
                    else
                    {
                        using (StreamWriter r = File.CreateText(@"BepInEx\plugins\MoistureUpset\CustomRunMusic.BlameRuneForThis"))
                        {
                            r.Write(0);
                            AkSoundEngine.SetRTPCValue("CustomRunMusic", 0);
                        }
                    }
                }
                catch (Exception)
                {
                }
            };
        }
    }
}
