using BepInEx;
using R2API.Utils;
using RoR2;
using R2API;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using RoR2.UI;
using RiskOfOptions;
using UnityEngine.Video;
using UnityEngine.Rendering.PostProcessing;
using LeTai.Asset.TranslucentImage;
using MoistureUpset.NetMessages;
using R2API.Networking;
using System;

namespace MoistureUpset
{
    [BepInDependency("com.bepis.r2api")]
    [BepInDependency("com.rune580.riskofoptions")]
    [BepInPlugin("com.gemumoddo.MoistureUpset", "Moisture Upset", VERSION)]
    [NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.EveryoneNeedSameModVersion)]
    [R2APISubmoduleDependency("SoundAPI", "PrefabAPI", "CommandHelper", "LoadoutAPI", "SurvivorAPI", "ResourcesAPI", "LanguageAPI", "NetworkingAPI", "UnlockAPI")]
    public class Moisture_Upset : BaseUnityPlugin // Finally renamed this to actually represent our mod.
    {

        public const string VERSION = "1.3.6";
        public void Awake()
        {

            DebugClass.SetLogger(base.Logger);

            NetMessages.Register.Init();


            Assets.PopulateAssets();
            Settings.RunAll();
            EnemyReplaceMentsRunAll();

            //

            Skins.Utils.LoadAllSkins();

            SoundAssets.RegisterSoundEvents();

            //On.RoR2.UI.CharacterSelectController.SelectSurvivor += CharacterSelectController_SelectSurvivor;

            //On.RoR2.TeleporterInteraction.Awake += TeleporterInteraction_Awake;

            //ligmaballs();

            ItemDisplayPositionFixer.Init();

            R2API.Utils.CommandHelper.AddToConsoleWhenReady();

            ModSettingsManager.addStartupListener(new UnityEngine.Events.UnityAction(IntroReplaceAction));

            

            EnemyReplacements.LoadResource("moisture_bonzibuddy");
            EnemyReplacements.LoadResource("moisture_bonzistatic");

            LanguageAPI.Add("MOISTURE_BONZIBUDDY_ACHIEVEMENT_NAME", "He awakens");
            LanguageAPI.Add("MOISTURE_BONZIBUDDY_ACHIEVEMENT_DESC", "The glass frog isn't what it seems");
            LanguageAPI.Add("MOISTURE_BONZIBUDDY_UNLOCKABLE_NAME", "He awakens");

            //RoR2.ContentManagement.ContentManager.collectContentPackProviders += delegate (RoR2.ContentManagement.ContentManager.AddContentPackProviderDelegate addContentPackProvider)
            //{
            //    addContentPackProvider(new MoistureUpsetContent());
            //};

            On.RoR2.ContentManagement.ContentManager.SetContentPacks += (orig, self) =>
            {
                orig(self);

                //UnlockableDef s = ScriptableObject.CreateInstance<UnlockableDef>();
                //AchievementDef achievementDef = new AchievementDef
                //{
                //    identifier = "MOISTURE_BONZIBUDDY_ACHIEVEMENT_ID",
                //    unlockableRewardIdentifier = "MOISTURE_BONZIBUDDY_REWARD_ID",
                //    prerequisiteAchievementIdentifier = "MOISTURE_BONZIBUDDY_PREREQ_ID",
                //    nameToken = "MOISTURE_BONZIBUDDY_ACHIEVEMENT_NAME",
                //    descriptionToken = "MOISTURE_BONZIBUDDY_ACHIEVEMENT_DESC",
                //    iconPath = "@MoistureUpset_moisture_bonzistatic:assets/bonzibuddy/BonziIcon.png",
                //};
                //s.nameToken = "MOISTURE_BONZIBUDDY_UNLOCKABLE_NAME";
                ////s.cachedName = "He awakens";
                //s.getHowToUnlockString = (() => Language.GetStringFormatted("UNLOCK_VIA_ACHIEVEMENT_FORMAT", new object[]
                //{
                //    Language.GetString(achievementDef.nameToken),
                //    Language.GetString(achievementDef.descriptionToken)
                //}));
                //s.getUnlockedString = (() => Language.GetStringFormatted("UNLOCKED_FORMAT", new object[]
                //{
                //    Language.GetString(achievementDef.nameToken),
                //    Language.GetString(achievementDef.descriptionToken)
                //}));
                //List<UnlockableDef> unlockableDefs = new List<UnlockableDef>(RoR2.ContentManagement.ContentManager._unlockableDefs);
                //unlockableDefs.Add(s);
                //RoR2.ContentManagement.ContentManager._unlockableDefs = unlockableDefs.ToArray();

                GameObject bonzi = Instantiate(Resources.Load<GameObject>("@MoistureUpset_moisture_bonzibuddy:assets/bonzibuddy/bonzibuddy.prefab"));
                DontDestroyOnLoad(bonzi);
                bonzi.GetComponent<RectTransform>().SetParent(RoR2Application.instance.mainCanvas.transform, false);
                bonzi.SetActive(true);
                bonzi.GetComponent<RectTransform>().anchorMin = Vector2.zero;
                bonzi.GetComponent<RectTransform>().anchorMax = Vector2.zero;
                bonzi.layer = 5;
                BonziBuddy.buddy = bonzi.AddComponent<BonziBuddy>();

                orig(self);
            };

            //On.RoR2.RoR2Application.OnLoad += (orig, self) =>
            //{
            //    orig(self);

            //    UnlockableDef s = ScriptableObject.CreateInstance<UnlockableDef>();
            //    AchievementDef achievementDef = new AchievementDef
            //    {
            //        identifier = "MOISTURE_BONZIBUDDY_ACHIEVEMENT_ID",
            //        unlockableRewardIdentifier = "MOISTURE_BONZIBUDDY_REWARD_ID",
            //        prerequisiteAchievementIdentifier = "MOISTURE_BONZIBUDDY_PREREQ_ID",
            //        nameToken = "MOISTURE_BONZIBUDDY_ACHIEVEMENT_NAME",
            //        descriptionToken = "MOISTURE_BONZIBUDDY_ACHIEVEMENT_DESC",
            //        iconPath = "@MoistureUpset_moisture_bonzistatic:assets/bonzibuddy/BonziIcon.png",
            //    };
            //    s.nameToken = "MOISTURE_BONZIBUDDY_UNLOCKABLE_NAME";
            //    //s.cachedName = "He awakens";
            //    s.getHowToUnlockString = (() => Language.GetStringFormatted("UNLOCK_VIA_ACHIEVEMENT_FORMAT", new object[]
            //    {
            //        Language.GetString(achievementDef.nameToken),
            //        Language.GetString(achievementDef.descriptionToken)
            //    }));
            //    s.getUnlockedString = (() => Language.GetStringFormatted("UNLOCKED_FORMAT", new object[]
            //    {
            //        Language.GetString(achievementDef.nameToken),
            //        Language.GetString(achievementDef.descriptionToken)
            //    }));
            //    List<UnlockableDef> unlockableDefs = new List<UnlockableDef>(ContentManager.unlockableDefs);
            //    unlockableDefs.Add(s);
            //    ContentManager.unlockableDefs = unlockableDefs.ToArray();



            //    GameObject bonzi = Instantiate(Resources.Load<GameObject>("@MoistureUpset_moisture_bonzibuddy:assets/bonzibuddy/bonzibuddy.prefab"));
            //    DontDestroyOnLoad(bonzi);
            //    bonzi.GetComponent<RectTransform>().SetParent(RoR2Application.instance.mainCanvas.transform, false);
            //    bonzi.SetActive(true);
            //    bonzi.GetComponent<RectTransform>().anchorMin = Vector2.zero;
            //    bonzi.GetComponent<RectTransform>().anchorMax = Vector2.zero;
            //    bonzi.layer = 5;
            //    BonziBuddy.buddy = bonzi.AddComponent<BonziBuddy>();
            //};


            //UnlockableAPI.AddUnlockable<BonziUnlocked>(true);
        }
        //private string PlaySound(On.RoR2.Chat.UserChatMessage.orig_ConstructChatString orig, Chat.UserChatMessage self)
        //{
        //    BonziBuddy.buddy.StartCoroutine(BonziBuddy.buddy.Speak(self.text));
        //    return orig(self);
        //}
        internal static int completed = 0;
        public void EnemyReplaceMentsRunAll()
        {
            try
            {
                EnemyReplacements.LoadBNK("ImWettest");
                EnemyReplacements.LoadBNK("ImWettest2");
                StartCoroutine(EnemyReplacements.ThanosQuotes());
                StartCoroutine(EnemyReplacements.DEBUG());
                StartCoroutine(EnemyReplacements.Twitch());
                StartCoroutine(EnemyReplacements.Cereal());
                StartCoroutine(EnemyReplacements.Thanos());
                StartCoroutine(EnemyReplacements.RobloxTitan());
                StartCoroutine(EnemyReplacements.Alex());
                StartCoroutine(EnemyReplacements.ElderLemurian());
                StartCoroutine(EnemyReplacements.Lemurian());
                StartCoroutine(EnemyReplacements.Golem());
                StartCoroutine(EnemyReplacements.Bison());
                StartCoroutine(EnemyReplacements.SolusUnit());
                StartCoroutine(EnemyReplacements.Templar());
                StartCoroutine(EnemyReplacements.GreaterWisp());
                StartCoroutine(EnemyReplacements.Wisp());
                StartCoroutine(EnemyReplacements.Sans());
                StartCoroutine(EnemyReplacements.Imp());
                StartCoroutine(EnemyReplacements.MiniMushroom());
                StartCoroutine(EnemyReplacements.BeetleGuard());
                StartCoroutine(EnemyReplacements.Beetle());
                StartCoroutine(EnemyReplacements.TacoBell());
                StartCoroutine(EnemyReplacements.Jelly());
                StartCoroutine(EnemyReplacements.Shop());
                StartCoroutine(EnemyReplacements.Names());
                StartCoroutine(EnemyReplacements.Icons());
                StartCoroutine(EnemyReplacements._UI());
                StartCoroutine(EnemyReplacements.NonEnemyNames());
                StartCoroutine(EnemyReplacements.Shrines());
                StartCoroutine(EnemyReplacements.LemmeSmash());
                StartCoroutine(EnemyReplacements.Hagrid());
                StartCoroutine(EnemyReplacements.Noodle());
                StartCoroutine(EnemyReplacements.Skeleton());
                StartCoroutine(EnemyReplacements.CrabRave());
                StartCoroutine(EnemyReplacements.PUDDI());
                StartCoroutine(EnemyReplacements.StringWorm());
                StartCoroutine(EnemyReplacements.Discord());
                StartCoroutine(EnemyReplacements.Copter());
                StartCoroutine(EnemyReplacements.Rob());
                StartCoroutine(EnemyReplacements.Nyan());
                StartCoroutine(EnemyReplacements.Imposter());
                StartCoroutine(EnemyReplacements.Collab());
                DebugClass.Log($"Haulting until everything finishes");
                while (completed < 41)
                {
                    DebugClass.Log($"----------{completed}");
                }
                DebugClass.Log($"Done");
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }
        }

        public void IntroReplaceAction()
        {
            if (float.Parse(ModSettingsManager.getOptionValue("Only Survivor Skins")) == 1)
            {
                SyncAudio.doMinecraftOofSound = false;
                SyncAudio.doShrineSound = false;
            }
            if (BigJank.getOptionValue("Replace Intro Scene", "UI Changes"))
            {
                LoadIntro();

                SceneManager.sceneLoaded += SceneManager_sceneLoaded;
            }
        }

        private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            if (arg0.name == "intro")
            {
                var cutsceneController = GameObject.Find("CutsceneController");

                cutsceneController.GetComponentInChildren<StartEvent>().action.RemoveAllListeners();

                //GameObject.Find("SkipVoteOverlay").GetComponentInChildren<InputResponse>().onPress.AddListener(delegate { RoR2.Console.instance.SubmitCmd((NetworkUser)null, "set_scene title"); });

                //DestroyImmediate(cutsceneController);
                DestroyImmediate(GameObject.Find("Set 2 - Cabin"));
                DestroyImmediate(GameObject.Find("Set 4 - Cargo"));
                DestroyImmediate(GameObject.Find("Set 1 - Space"));
                DestroyImmediate(GameObject.Find("Set 3 - Space, Small Planet"));
                DestroyImmediate(GameObject.Find("cutscene intro"));
                DestroyImmediate(GameObject.Find("MainArea"));
                DestroyImmediate(GameObject.Find("Cutscene Space Skybox"));

                DestroyImmediate(GameObject.Find("GlobalPostProcessVolume"));
                DestroyImmediate(GameObject.Find("Scene Camera").GetComponent<BlurOptimized>());
                DestroyImmediate(GameObject.Find("Scene Camera").GetComponent<TranslucentImageSource>());
                DestroyImmediate(GameObject.Find("Scene Camera").GetComponent<PostProcessLayer>());

                var videoPlayer = Instantiate(Resources.Load<GameObject>("@MoistureUpset_Intro:assets/video/introplayer.prefab"));

                videoPlayer.GetComponentInChildren<VideoPlayer>().targetCamera = GameObject.Find("Scene Camera").GetComponent<Camera>();

                videoPlayer.GetComponentInChildren<VideoPlayer>().loopPointReached += IntroFinished;

                videoPlayer.GetComponentInChildren<VideoPlayer>().targetCameraAlpha = 1;

                videoPlayer.GetComponentInChildren<VideoPlayer>().Play();

                AkSoundEngine.PostEvent("PlayOpening", GameObject.FindObjectOfType<GameObject>());
            }

            //if (arg0.name == "title")
            //{
            //    var dialog = SimpleDialogBox.Create();

            //    dialog.headerToken = new SimpleDialogBox.TokenParamsPair
            //    { 
            //        token = "Hey!",
            //        formatParams = Array.Empty<object>()
            //    };

            //    dialog.descriptionToken = new SimpleDialogBox.TokenParamsPair
            //    {
            //        token = "Did you know you can customize this brain damage? \n Go to options and then mod options.",
            //        formatParams = Array.Empty<object>()
            //    };

            //    dialog.AddCancelButton("ok");
            //}
        }

        private void IntroFinished(VideoPlayer source)
        {
            RoR2.Console.instance.SubmitCmd((NetworkUser)null, "set_scene title");
        }

        private static void LoadIntro()
        {
            using (var assetStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("MoistureUpset.Resources.mu2intro"))
            {
                var MainAssetBundle = AssetBundle.LoadFromStream(assetStream);

                ResourcesAPI.AddProvider(new AssetBundleResourcesProvider("@MoistureUpset_Intro", MainAssetBundle));
            }
        }



        //[ConCommand(commandName = "slowmotime", flags = ConVarFlags.None, helpText = "Does the magic")]
        //private static void SlowmoCommand(ConCommandArgs args)
        //{
        //    RoR2.Console.instance.SubmitCmd(NetworkUser.readOnlyLocalPlayersList[0], "god");
        //    RoR2.Console.instance.SubmitCmd(NetworkUser.readOnlyLocalPlayersList[0], "noclip");
        //    RoR2.Console.instance.SubmitCmd(NetworkUser.readOnlyLocalPlayersList[0], "give_money 1000000");
        //    RoR2.Console.instance.SubmitCmd(NetworkUser.readOnlyLocalPlayersList[0], "give_item SoldiersSyringe 100");
        //    RoR2.Console.instance.SubmitCmd(NetworkUser.readOnlyLocalPlayersList[0], "time_scale 0.1");
        //}

        //[ConCommand(commandName = "musicdebug", flags = ConVarFlags.None, helpText = "Spits currently playing music to console")]
        //private static void MusicTest(ConCommandArgs args)
        //{
        //    var c = GameObject.FindObjectOfType<MusicController>();
        //    //Debug.Log($"-------------{c.GetPropertyValue<MusicTrackDef>("currentTrack").cachedName}");
        //}

        [ConCommand(commandName = "getallgameobjects", flags = ConVarFlags.None, helpText = "yes")]
        private static void GameObjects(ConCommandArgs args)
        {
            DebugClass.GetAllGameObjects();
        }

        [ConCommand(commandName = "getalltransforms", flags = ConVarFlags.None, helpText = "yes")]
        private static void Transforms(ConCommandArgs args)
        {
            DebugClass.GetAllTransforms();
        }

        private void TeleporterInteraction_Awake(On.RoR2.TeleporterInteraction.orig_Awake orig, TeleporterInteraction self)
        {
            //self.shouldAttemptToSpawnShopPortal = true;
            //self.Network_shouldAttemptToSpawnShopPortal = true;
            //self.baseShopSpawnChance = 1;

            orig(self);

            //self.shouldAttemptToSpawnShopPortal = true;
            //self.Network_shouldAttemptToSpawnShopPortal = true;
        }

        private void CharacterSelectController_SelectSurvivor(On.RoR2.UI.CharacterSelectController.orig_SelectSurvivor orig, RoR2.UI.CharacterSelectController self, SurvivorIndex survivor)
        {
            self.selectedSurvivorIndex = survivor;

            //if (survivor == SurvivorIndex.Commando)
            //{
            //    AkSoundEngine.PostEvent("YourMother", self.characterDisplayPads[0].displayInstance.gameObject);
            //}

            orig(self, survivor);

            HGTextMeshProUGUI[] objects = GameObject.FindObjectsOfType<HGTextMeshProUGUI>();

            foreach (var item in objects)
            {
                if (item.text == "Locked In")
                {
                    //Debug.Log(item.transform.parent.name);
                }
            }
        }
    }
}
