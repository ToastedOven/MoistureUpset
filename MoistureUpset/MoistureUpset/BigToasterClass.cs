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
using System.IO;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Text;

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
            OnHit();
            ModifyChat();
            PreGame();
            INeedToSortThese();
            PlayerDeath();
            DifficultyIcons();
        }
        public static void INeedToSortThese()
        {
            //On.RoR2.BossGroup.UpdateBossMemories += (orig, self) =>
            //{
            //    orig(self);
            //    Debug.Log($"bossgroup--------{self.bestObservedName}");
            //};
            //On.RoR2.SceneDirector.Start += (orig, self) =>
            //{
            //    orig(self);
            //    Debug.Log($"asddddddddddddddddddddddddddd--------{self.teleporterSpawnCard.name}");
            //    foreach (var item in self.teleporterSpawnCard.prefab.GetComponents<Component>())
            //    {
            //        Debug.Log($"asddddddddddddddddddddddddddd--------{item}");
            //    }
            //    self.teleporterSpawnCard.prefab.GetComponent<BossGroup>().InvokeMethod("Start");
            //    Debug.Log($"bossgroup--------{self.teleporterSpawnCard.prefab.GetComponent<BossGroup>().bestObservedName}");
            //};
        }

        public static void DifficultyIcons()
        {
            UImods.ReplaceUIBetter("Textures/DifficultyIcons/texDifficultyEasyIcon", "MoistureUpset.Resources.easy.png");
            UImods.ReplaceUIBetter("textures/difficultyicons/texDifficultyEasyIconDisabled", "MoistureUpset.Resources.easyDisabled.png");

            UImods.ReplaceUIBetter("textures/difficultyicons/texDifficultyNormalIcon", "MoistureUpset.Resources.medium.png");
            UImods.ReplaceUIBetter("textures/difficultyicons/texDifficultyNormalIconDisabled", "MoistureUpset.Resources.mediumDisabled.png");

            UImods.ReplaceUIBetter("textures/difficultyicons/texDifficultyHardIcon", "MoistureUpset.Resources.hard.png");
            UImods.ReplaceUIBetter("textures/difficultyicons/texDifficultyHardIconDisabled", "MoistureUpset.Resources.hardDisabled.png");


            byte[] bytes = ByteReader.readbytes("MoistureUpset.Resources.pizzaroll.png");
            var r = Resources.LoadAll<GameObject>("prefabs/ui");
            foreach (var sex in r)
            {
                foreach (var item in sex.GetComponentsInChildren<UnityEngine.UI.Image>())
                {
                    try
                    {
                        //Debug.Log($"89-------{item.name}");
                        if (item.name == "Checkbox")
                        {
                            item.overrideSprite.texture.LoadImage(bytes);
                        }
                    }
                    catch (Exception)
                    {
                    }

                }
            }
            //var font = Resources.Load<Font>("@MoistureUpset_robloxfont:assets/roblox_font.ttf");
            //var resources2 = Resources.LoadAll<Font>("");
            //for (int i = 0; i < resources2.Length; i++)
            //{
            //    if (resources2[i].name.Contains("Font Texture") || resources2[i].name.Contains("Atlas"))
            //    {
            //        Debug.Log($"-----{resources2[i]}");
            //        //resources2[i].
            //    }
            //}
            //byte[] bytes = ByteReader.readbytes("MoistureUpset.Resources.font.png");
            //var resources = Resources.LoadAll<UnityEngine.Object>("");
            //for (int i = 0; i < resources.Length; i++)
            //{
            //        Debug.Log($"-----{resources[i]}");
            //    if (resources[i].name.Contains("Font Texture") || resources[i].name.Contains("Atlas"))
            //    {
            //        //resources[i].LoadImage(bytes);
            //    }
            //}
        }

        public static void PlayerDeath()
        {
            On.RoR2.GlobalEventManager.OnPlayerCharacterDeath += (orig, self, report, user) =>
            {
                orig(self, report, user);
                try
                {
                    if (!user)
                    {
                        return;
                    }
                    List<string> quotes = new List<string> { "I fucking hate this game", "I wasn't even trying", "If ya'll would help me I wouldn't have died...", "Nice one hit protection game", "HOW DID I DIE?????", "The first game was better", "Whatever", "Yeah alright, thats cool" };
                    if (report.attackerMaster.name.ToUpper().Contains("MAGMAWORM"))
                    {
                        quotes.Add("The magma worm is such bullshit");
                    }
                    else if (report.attackerMaster.name.ToUpper().Contains("ELECTRICWORM"))
                    {
                        quotes.Add("Why does it get lightning? It's already strong enough");
                    }
                    else if (report.attackerMaster.name.ToUpper().Contains("BROTHERHURT"))
                    {
                        quotes.Add("This final phase sucks so much");
                    }
                    else if (report.attackerMaster.name.ToUpper().Contains("WISPMASTER"))
                    {
                        quotes.Add("Unfucking dodgeable");
                    }
                    else if (report.attackerMaster.name.ToUpper().Contains("VAGRANT"))
                    {
                        quotes.Add("How are you supposed to dodge that????");
                    }
                    else if (report.attackerMaster.name.ToUpper().Contains("LEMURIANBRUISERMASTER"))
                    {
                        quotes.Add("The fire breath is so annoying");
                    }
                    Chat.SendBroadcastChat(new Chat.UserChatMessage
                    {
                        sender = user.gameObject,
                        text = quotes[UnityEngine.Random.Range(0, quotes.Count - 1)]
                    });
                }
                catch (Exception)
                {
                }
            };
        }
        public static void PreGame()
        {
            On.RoR2.VoteController.StartTimer += (orig, self) =>
            {
                if (!NetworkServer.active)
                {
                    Debug.LogWarning("[Server] function 'System.Void RoR2.VoteController::StartTimer()' called on client");
                    return;
                }
                if (self.timerIsActive)
                {
                    return;
                }
                self.NetworktimerIsActive = true;
            };
            On.RoR2.UI.PregameCharacterSelection.Awake += (orig, self) =>
            {
                orig(self);
                AkSoundEngine.SetRTPCValue("MainMenuMusic", 0);
            };
            On.RoR2.SceneCatalog.OnActiveSceneChanged += (orig, oldS, newS) =>
            {
                EnemyReplacements.ReplaceMeshRenderer(EntityStates.Bell.BellWeapon.ChargeTrioBomb.preppedBombPrefab, "@MoistureUpset_tacobell:assets/toco.mesh", "@MoistureUpset_tacobell:assets/toco.png");
                EnemyReplacements.ReplaceParticleSystemmesh(EntityStates.MiniMushroom.SporeGrenade.chargeEffectPrefab, "@MoistureUpset_toad:assets/toadbombfull.mesh", 1);
                var skin = EntityStates.MiniMushroom.SporeGrenade.chargeEffectPrefab.GetComponentsInChildren<ParticleSystemRenderer>()[1];
                //skin.sharedMaterial = Resources.Load<Material>("@MoistureUpset_toad:assets/toadbomb.mat");
                skin.sharedMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                skin.sharedMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                skin.sharedMaterial.SetInt("_ZWrite", 0);
                skin.sharedMaterial.DisableKeyword("_ALPHATEST_ON");
                skin.sharedMaterial.DisableKeyword("_ALPHABLEND_ON");
                skin.sharedMaterial.EnableKeyword("_ALPHAPREMULTIPLY_ON");
                skin.sharedMaterial.renderQueue = 3000;
                //try
                //{
                //    var pre = EntityStates.MiniMushroom.SporeGrenade.chargeEffectPrefab;
                //    Debug.Log("-" + pre);
                //    var gameobject = pre.GetComponentsInChildren<ParticleSystemRenderer>()[1];
                //    Debug.Log("-" + gameobject);
                //    var skin = gameobject.gameObject.AddComponent<SkinnedMeshRenderer>() as SkinnedMeshRenderer;
                //    Debug.Log("-" + skin);
                //    skin.transform.parent = gameobject.transform;
                //    skin.sharedMesh = Resources.Load<Mesh>("@MoistureUpset_toad:assets/toadbomblid.mesh");
                //    skin.sharedMaterial = Resources.Load<Material>("@MoistureUpset_toad:assets/toadbomb.mat");
                //    skin.sharedMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                //    skin.sharedMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                //    skin.sharedMaterial.SetInt("_ZWrite", 0);
                //    skin.sharedMaterial.DisableKeyword("_ALPHATEST_ON");
                //    skin.sharedMaterial.DisableKeyword("_ALPHABLEND_ON");
                //    skin.sharedMaterial.EnableKeyword("_ALPHAPREMULTIPLY_ON");
                //    skin.sharedMaterial.renderQueue = 3000;
                //}
                //catch (Exception)
                //{
                //}



                EntityStates.ImpBossMonster.GroundPound.slamEffectPrefab.GetComponentInChildren<ParticleSystemRenderer>().mesh = null;
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
                AkSoundEngine.ExecuteActionOnEvent(1462303513, AkActionOnEventType.AkActionOnEventType_Stop);
                AkSoundEngine.ExecuteActionOnEvent(816301922, AkActionOnEventType.AkActionOnEventType_Stop);
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
            On.RoR2.CreditsController.OnEnable += (orig, self) =>
            {
                orig(self);
                DebugClass.UIdebug();
                UImods.ReplaceUIObject("Image", "MoistureUpset.Resources.MoistureUpsetFinal.png");
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
                    else if (text[0] == "DEBUGMUSIC")
                    {
                        var c = GameObject.FindObjectOfType<MusicController>();
                        MusicAPI.GetCurrentSong(ref c);
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
        public static void DeathSound()
        {
            //On.RoR2.CharacterBody.Start += (orig, self) =>
            //{
            //    orig(self);
            //    try
            //    {
            //        Debug.Log($"sounddeath-------------{self.GetFieldValue<SfxLocator>("sfxLocator").deathSound}");
            //        //Debug.Log($"barksound-------------{self.GetFieldValue<SfxLocator>("sfxLocator").barkSound}");
            //        //Debug.Log($"falldamagesound-------------{self.GetFieldValue<SfxLocator>("sfxLocator").fallDamageSound}");
            //        //Debug.Log($"landingsound-------------{self.GetFieldValue<SfxLocator>("sfxLocator").landingSound}");
            //        //Debug.Log($"opensound-------------{self.GetFieldValue<SfxLocator>("sfxLocator").openSound}");
            //    }
            //    catch (Exception)
            //    {
            //    }
            //};
            //On.RoR2.FootstepHandler.Start += (orig, self) =>
            //{
            //    orig(self);
            //    try
            //    {
            //        Debug.Log($"footstep-------------{self.baseFootstepString}");
            //        Debug.Log($"footstep-override------------{self.sprintFootstepOverrideString}");
            //    }
            //    catch (Exception)
            //    {
            //    }

            //};
            //On.EntityStates.GenericCharacterDeath.PlayDeathSound += (orig, self) =>
            //{
            //    Util.PlaySound("EDeath", self.outer.gameObject);
            //};
        }
        public static void BossMusicAndFanFare()
        {
            //On.RoR2.CharacterBody.Awake += (orig, self) =>
            //{
            //    orig(self);
            //    Debug.Log($"name: {self.name}");
            //    Debug.Log($"base: {self.baseNameToken}");
            //    Debug.Log($"subtitle: {self.subtitleNameToken}");
            //    if (self.master.isBoss)
            //    {
            //        AkSoundEngine.PostEvent("PlayBossMusic", RoR2Application.instance.gameObject);
            //        Debug.Log($"its a boss");
            //    }
            //};
            On.RoR2.CharacterBody.GetSubtitle += (orig, self) =>
            {
                if (self.master && self.master.isBoss)
                {
                    var c = GameObject.FindObjectOfType<MusicController>();
                    AkSoundEngine.ExecuteActionOnEvent(2493198437, AkActionOnEventType.AkActionOnEventType_Stop);
                    AkSoundEngine.ExecuteActionOnEvent(291592398, AkActionOnEventType.AkActionOnEventType_Stop);
                    if (self.baseNameToken.ToUpper().Contains("IMP"))
                    {
                        AkSoundEngine.PostEvent("PlaySans", c.gameObject);
                    }
                    else
                    {
                        AkSoundEngine.PostEvent("PlayBossMusic", c.gameObject);
                    }//Imp Overlord
                    Debug.Log($"name: {self.name}");
                    Debug.Log($"base: {self.baseNameToken}");
                    Debug.Log($"subtitle: {self.subtitleNameToken}");
                }
                return orig(self);
            };
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
                    AkSoundEngine.ExecuteActionOnEvent(1462303513, AkActionOnEventType.AkActionOnEventType_Stop);
                    AkSoundEngine.SetRTPCValue("BossMusicActive", 0);
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
                    MusicAPI.StopSong(ref c, "muSong23");
                    var mainBody = NetworkUser.readOnlyLocalPlayersList[0].master?.GetBody();
                    //AkSoundEngine.PostEvent("EndBossMusic", c.gameObject);
                    AkSoundEngine.SetRTPCValue("BossMusicActive", 1);
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
