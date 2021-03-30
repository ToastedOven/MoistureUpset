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

namespace MoistureUpset
{
    [BepInDependency("com.bepis.r2api")]
    [BepInDependency("com.rune580.riskofoptions")]
    [BepInPlugin("com.gemumoddo.MoistureUpset", "Moisture Upset", "1.2.1")]
    [NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.EveryoneNeedSameModVersion)]
    [R2APISubmoduleDependency("SoundAPI", "PrefabAPI", "CommandHelper", "LoadoutAPI", "SurvivorAPI", "ResourcesAPI", "LanguageAPI", "NetworkingAPI", "UnlockablesAPI")]
    public class Moisture_Upset : BaseUnityPlugin // Finally renamed this to actually represent our mod.
    {
        public void Awake()
        {

            DebugClass.SetLogger(base.Logger);

            NetMessages.Register.Init();

            Settings.RunAll();

            Assets.PopulateAssets();
            //

            Skins.Utils.LoadAllSkins();

            SoundAssets.RegisterSoundEvents();

            NetworkAssistant.InitSNA();

            //On.RoR2.UI.CharacterSelectController.SelectSurvivor += CharacterSelectController_SelectSurvivor;

            //On.RoR2.TeleporterInteraction.Awake += TeleporterInteraction_Awake;

            //ligmaballs();

            ItemDisplayPositionFixer.Init();

            R2API.Utils.CommandHelper.AddToConsoleWhenReady();

            ModSettingsManager.addStartupListener(new UnityEngine.Events.UnityAction(IntroReplaceAction));



            EnemyReplacements.LoadResource("moisture_bonzibuddy");
            EnemyReplacements.LoadResource("moisture_bonzistatic");
            //On.RoR2.RoR2Application.OnLoad += (orig, self) =>
            //{
            //    orig(self);


            //    GameObject bonzi = Instantiate(Resources.Load<GameObject>("@MoistureUpset_moisture_bonzibuddy:assets/bonzibuddy/bonzibuddy.prefab"));
            //    DontDestroyOnLoad(bonzi);
            //    bonzi.GetComponent<RectTransform>().SetParent(RoR2Application.instance.mainCanvas.transform, false);
            //    bonzi.SetActive(true);
            //    bonzi.GetComponent<RectTransform>().anchorMin = Vector2.zero;
            //    bonzi.GetComponent<RectTransform>().anchorMax = Vector2.zero;
            //    bonzi.layer = 5;
            //    BonziBuddy.buddy = bonzi.AddComponent<BonziBuddy>();
            //};

            //LanguageAPI.Add("MOISTURE_BONZIBUDDY_ACHIEVEMENT_NAME", "He awakens");
            //LanguageAPI.Add("MOISTURE_BONZIBUDDY_ACHIEVEMENT_DESC", "The glass frog isn't what it seems");
            //LanguageAPI.Add("MOISTURE_BONZIBUDDY_UNLOCKABLE_NAME", "He awakens");
            //UnlockablesAPI.AddUnlockable<BonziUnlocked>(true);
        }
        //private string PlaySound(On.RoR2.Chat.UserChatMessage.orig_ConstructChatString orig, Chat.UserChatMessage self)
        //{
        //    BonziBuddy.buddy.StartCoroutine(BonziBuddy.buddy.Speak(self.text));
        //    return orig(self);
        //}

        public void IntroReplaceAction()
        {
            if (float.Parse(ModSettingsManager.getOptionValue("Only Survivor Skins")) == 1)
            {
                SyncAudio.doMinecraftOofSound = false;
                SyncAudio.doShrineSound = false;
            }
            if (BigJank.getOptionValue("Replace Intro Scene") == 1)
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

        public static void ligmaballs()
        {
            var fortniteDance = Resources.Load<AnimationClip>("@MoistureUpset_fortnite:assets/dancemoves.anim");
            var fab = Resources.Load<GameObject>("prefabs/characterbodies/CommandoBody");

            //foreach (var item in fab.GetComponentsInChildren<Component>())
            //{
            //    Debug.Log($"--------------------------------------------------{item}");
            //}

            var anim = fab.GetComponentInChildren<Animator>();

            //Debug.Log($"++++++++++++++++++++++++++++++++++++++++{anim}");

            //AnimatorController anim = new AnimatorController
            AnimatorOverrideController aoc = new AnimatorOverrideController(anim.runtimeAnimatorController);

            var anims = new List<KeyValuePair<AnimationClip, AnimationClip>>();
            foreach (var a in aoc.animationClips)
            {
                anims.Add(new KeyValuePair<AnimationClip, AnimationClip>(a, a));
            }
            aoc.ApplyOverrides(anims);
            anim.runtimeAnimatorController = aoc;
        }

        public void Start()
        {
            if (BigJank.getOptionValue("Replace Intro Scene") == 1)
            {
                RoR2.Console.instance.SubmitCmd((NetworkUser)null, "set_scene intro");
            }

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
