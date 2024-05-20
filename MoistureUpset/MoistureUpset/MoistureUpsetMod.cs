using BepInEx;
using R2API.Utils;
using RoR2;
using R2API;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using UnityEngine.Rendering.PostProcessing;
using LeTai.Asset.TranslucentImage;
using MoistureUpset.NetMessages;

namespace MoistureUpset
{
    [BepInDependency("com.bepis.r2api")]
    [BepInDependency("com.rune580.riskofoptions")]
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    [NetworkCompatibility]
    public class MoistureUpsetMod : BaseUnityPlugin // Finally renamed this to actually represent our mod.
    {
        public static MoistureUpsetMod Instance;
        
        public void Awake()
        {
            Instance = this;
            CommandHelper.AddToConsoleWhenReady();
            DebugClass.SetLogger(base.Logger);
            
            NetMessages.Register.Init();
            
            Assets.PopulateAssets();

            Settings.RunAll();

            MLG.Setup();
            
            Skins.SkinManager.Init();

            FixRunesFixBecauseWhyDidWeChangeSomethingThatWasAlreadyWorkingFFS();

            Assets.LoadSoundBanks();

            SoundAssets.RegisterSoundEvents();

            NetworkAssistant.InitSNA();
            

            EnemyReplacements.LoadResource("moisture_bonzibuddy");
            EnemyReplacements.LoadResource("moisture_bonzistatic");

            LanguageAPI.Add("MOISTURE_BONZIBUDDY_ACHIEVEMENT_NAME", "He awakens");
            LanguageAPI.Add("MOISTURE_BONZIBUDDY_ACHIEVEMENT_DESC", "The glass frog isn't what it seems");
            LanguageAPI.Add("MOISTURE_BONZIBUDDY_UNLOCKABLE_NAME", "He awakens");

            //RoR2.ContentManagement.ContentManager.collectContentPackProviders += delegate (RoR2.ContentManagement.ContentManager.AddContentPackProviderDelegate addContentPackProvider)
            //{
            //    addContentPackProvider(new MoistureUpsetContent());
            //};
            On.RoR2.SteamworksClientManager.ctor += (orig, self) =>
            {
                orig(self);
                BonziBuddy.SetupGameObjects();
            };
            //On.RoR2.ContentManagement.ContentManager.SetContentPacks += (orig, self) =>
            //{
            //    orig(self);

            //    //UnlockableDef s = ScriptableObject.CreateInstance<UnlockableDef>();
            //    //AchievementDef achievementDef = new AchievementDef
            //    //{
            //    //    identifier = "MOISTURE_BONZIBUDDY_ACHIEVEMENT_ID",
            //    //    unlockableRewardIdentifier = "MOISTURE_BONZIBUDDY_REWARD_ID",
            //    //    prerequisiteAchievementIdentifier = "MOISTURE_BONZIBUDDY_PREREQ_ID",
            //    //    nameToken = "MOISTURE_BONZIBUDDY_ACHIEVEMENT_NAME",
            //    //    descriptionToken = "MOISTURE_BONZIBUDDY_ACHIEVEMENT_DESC",
            //    //    iconPath = "@MoistureUpset_moisture_bonzistatic:assets/bonzibuddy/BonziIcon.png",
            //    //};
            //    //s.nameToken = "MOISTURE_BONZIBUDDY_UNLOCKABLE_NAME";
            //    ////s.cachedName = "He awakens";
            //    //s.getHowToUnlockString = (() => Language.GetStringFormatted("UNLOCK_VIA_ACHIEVEMENT_FORMAT", new object[]
            //    //{
            //    //    Language.GetString(achievementDef.nameToken),
            //    //    Language.GetString(achievementDef.descriptionToken)
            //    //}));
            //    //s.getUnlockedString = (() => Language.GetStringFormatted("UNLOCKED_FORMAT", new object[]
            //    //{
            //    //    Language.GetString(achievementDef.nameToken),
            //    //    Language.GetString(achievementDef.descriptionToken)
            //    //}));
            //    //List<UnlockableDef> unlockableDefs = new List<UnlockableDef>(RoR2.ContentManagement.ContentManager._unlockableDefs);
            //    //unlockableDefs.Add(s);
            //    //RoR2.ContentManagement.ContentManager._unlockableDefs = unlockableDefs.ToArray();



            //    orig(self);
            //};

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



            //    GameObject bonzi = Instantiate(Assets.Load<GameObject>("@MoistureUpset_moisture_bonzibuddy:assets/bonzibuddy/bonzibuddy.prefab"));
            //    DontDestroyOnLoad(bonzi);
            //    bonzi.GetComponent<RectTransform>().SetParent(RoR2Application.instance.mainCanvas.transform, false);
            //    bonzi.SetActive(true);
            //    bonzi.GetComponent<RectTransform>().anchorMin = Vector2.zero;
            //    bonzi.GetComponent<RectTransform>().anchorMax = Vector2.zero;
            //    bonzi.layer = 5;
            //    BonziBuddy.buddy = bonzi.AddComponent<BonziBuddy>();
            //};


            //UnlockableAPI.AddUnlockable<BonziUnlocked>(true);
            IntroReplaceAction();

        }
        internal void FixRunesFixBecauseWhyDidWeChangeSomethingThatWasAlreadyWorkingFFS()
        {
            EnemyReplacements.LoadBNK("ImMoist");
            EnemyReplacements.LoadBNK("ImReallyMoist");
            EnemyReplacements.LoadBNK("Risk2GaySounds");
        }
        internal static MusicController musicController;
        //private string PlaySound(On.RoR2.Chat.UserChatMessage.orig_ConstructChatString orig, Chat.UserChatMessage self)
        //{
        //    BonziBuddy.buddy.StartCoroutine(BonziBuddy.buddy.Speak(self.text));
        //    return orig(self);
        //}

        public void IntroReplaceAction()
        {
            if (Settings.OnlySurvivorSkins.Value)
            {
                SyncAudio.doMinecraftOofSound = false;
                SyncAudio.doShrineSound = false;
            }
            if (BigJank.getOptionValue(Settings.ReplaceIntroScene))
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

                var videoPlayer = Instantiate(Assets.Load<GameObject>("@MoistureUpset_Intro:assets/video/introplayer.prefab"));

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
            Assets.AddBundle("Resources.mu2intro");
            
            // using (var assetStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("MoistureUpset.Resources.mu2intro"))
            // {
            //     var MainAssetBundle = AssetBundle.LoadFromStream(assetStream);
            //
            //     ResourcesAPI.AddProvider(new AssetBundleResourcesProvider("@MoistureUpset_Intro", MainAssetBundle));
            // }
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
        public void Start()
        {
            //if (BigJank.getOptionValue(Settings.ReplaceIntroScene))
            //{
            //    RoR2.Console.instance.SubmitCmd((NetworkUser)null, "set_scene intro");
            //}

        }
    }
}
