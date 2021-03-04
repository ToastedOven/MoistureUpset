using BepInEx;
using R2API.Utils;
using RoR2;
using R2API;
using R2API.MiscHelpers;
using System.Reflection;
using static R2API.SoundAPI;
using UnityEngine;
using UnityEditor;
using System;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using RoR2.UI;
using RiskOfOptions;
using System.Text;
using System.IO;
using UnityEngine.Video;
using RoR2.PostProcessing;
using UnityEngine.Rendering.PostProcessing;
using LeTai.Asset.TranslucentImage;
using R2API.Networking;
using MoistureUpset.NetMessages;
using System.Linq;
using UnityEngine.Assertions;

namespace MoistureUpset
{
    [BepInDependency("com.bepis.r2api")]
    [BepInDependency("com.rune580.riskofoptions")]
    [BepInPlugin("com.gemumoddo.MoistureUpset", "Moisture Upset", "1.2.0")]
    [NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.EveryoneNeedSameModVersion)]
    [R2APISubmoduleDependency("SoundAPI", "PrefabAPI", "CommandHelper", "LoadoutAPI", "SurvivorAPI", "ResourcesAPI", "LanguageAPI", "NetworkingAPI")]
    public class Moisture_Upset : BaseUnityPlugin // Finally renamed this to actually represent our mod.
    {
        public void Awake()
        {
            DebugClass.SetLogger(base.Logger);

            NetMessages.Register.Init();
            
            Settings.RunAll();

            Assets.PopulateAssets();

            Skins.Utils.LoadAllSkins();

            SoundAssets.RegisterSoundEvents();

            NetworkAssistant.InitSNA();

            //On.RoR2.UI.CharacterSelectController.SelectSurvivor += CharacterSelectController_SelectSurvivor;

            //On.RoR2.TeleporterInteraction.Awake += TeleporterInteraction_Awake;

            //ligmaballs();

            ItemDisplayPositionFixer.Init();

            R2API.Utils.CommandHelper.AddToConsoleWhenReady();

            ModSettingsManager.addStartupListener(new UnityEngine.Events.UnityAction(IntroReplaceAction));

            //AkSoundEngine.SetAudioInputCallbacks(fuckmeiguess, fuckmetoo);

            On.RoR2.Chat.UserChatMessage.ConstructChatString += PlaySound;
        }
        private string PlaySound(On.RoR2.Chat.UserChatMessage.orig_ConstructChatString orig, Chat.UserChatMessage self)
        {
            if (self.text == "start")
            {
                testingaudio = true;

                //AkExternalSourceInfo source = new AkExternalSourceInfo();
                //source.iExternalSrcCookie = AkSoundEngine.GetIDFromString("TestTTSAudio");
                //source.szFile = "joemama.wav";
                //source.idCodec = AkSoundEngine.AKCODECID_PCM;

                //AkSoundEngine.PostEvent("TestTTSAudio", GameObject.FindObjectOfType<GameObject>(), 0, null, null, 1, source);

                AkAudioInputManager.PostAudioInputEvent("TestLiveAudio", GameObject.FindObjectOfType<GameObject>(), fuckmeiguess, fuckmetoo);
            }
            else
            {
                testingaudio = false;
                AkSoundEngine.ExecuteActionOnEvent(899853287, AkActionOnEventType.AkActionOnEventType_Stop);
            }

            return orig(self);
        }

        

        private bool testingaudio = true;
        private uint length = 0;

        private bool fuckmeiguess(uint playingID, uint channelIndex, float[] samples)
        {
            //uint Frequency = 420;

            float[] left, right;

            readWav("joemama.wav", out left, out right);

            if (length >= (uint)left.Length)
            {
                length = 0;
            }

            DebugClass.Log($"Samples: {samples.Length}, Left: {left.Length}, Current: {length}");

            //samples = left;
            try
            {
                uint i = 0;

                for (i = 0; i < samples.Length; ++i)
                {
                    if (i >= left.Length)
                    {
                        break;
                    }
                    samples[i] = left[i + length];
                }
                length += i;
            }
            catch (Exception)
            {

                throw;
            }
            

            //samples = wavtofloatarray("joemama.wav");
            //for (uint i = 0; i < samples.Length; ++i)
            //
            //    samples[i] = UnityEngine.Mathf.Sin(Frequency * 2 * UnityEngine.Mathf.PI * (i + 0) / 48000);

            channelIndex = 1;

            //DebugClass.Log($"id:{playingID}, samples: {samples}, channlIndex: {channelIndex}");

            return testingaudio;
        }
        private void fuckmetoo(uint playingID, AkAudioFormat format)
        {
            uint samplerate, channels;

            getSampleRate("joemama.wav", out samplerate, out channels);

            format.channelConfig.uNumChannels = channels;
            format.uSampleRate = samplerate;
        }

        static void getSampleRate(string filename, out uint samplerate, out uint channels)
        {
            samplerate = 0;
            channels = 0;
            try
            {
                using (FileStream fs = File.Open(filename, FileMode.Open))
                {
                    BinaryReader reader = new BinaryReader(fs);

                    // chunk 0
                    int chunkID = reader.ReadInt32();
                    int fileSize = reader.ReadInt32();
                    int riffType = reader.ReadInt32();


                    // chunk 1
                    int fmtID = reader.ReadInt32();
                    int fmtSize = reader.ReadInt32(); // bytes for this chunk (expect 16 or 18)

                    // 16 bytes coming...
                    int fmtCode = reader.ReadInt16();
                    int Channels = reader.ReadInt16();
                    int sampleRate = reader.ReadInt32();
                    int byteRate = reader.ReadInt32();
                    int fmtBlockAlign = reader.ReadInt16();
                    int bitDepth = reader.ReadInt16();

                    samplerate = (uint)sampleRate;
                    channels = (uint)Channels;
                }
            }
            catch
            {
                Debug.Log("...Failed to load: " + filename);
            }
        }

        static bool readWav(string filename, out float[] L, out float[] R)
        {
            L = R = null;

            try
            {
                using (FileStream fs = File.Open(filename, FileMode.Open))
                {
                    BinaryReader reader = new BinaryReader(fs);

                    // chunk 0
                    int chunkID = reader.ReadInt32();
                    int fileSize = reader.ReadInt32();
                    int riffType = reader.ReadInt32();


                    // chunk 1
                    int fmtID = reader.ReadInt32();
                    int fmtSize = reader.ReadInt32(); // bytes for this chunk (expect 16 or 18)

                    // 16 bytes coming...
                    int fmtCode = reader.ReadInt16();
                    int channels = reader.ReadInt16();
                    int sampleRate = reader.ReadInt32();
                    int byteRate = reader.ReadInt32();
                    int fmtBlockAlign = reader.ReadInt16();
                    int bitDepth = reader.ReadInt16();

                    if (fmtSize == 18)
                    {
                        // Read any extra values
                        int fmtExtraSize = reader.ReadInt16();
                        reader.ReadBytes(fmtExtraSize);
                    }

                    // chunk 2
                    int dataID = reader.ReadInt32();
                    int bytes = reader.ReadInt32();

                    // DATA!
                    byte[] byteArray = reader.ReadBytes(bytes);

                    int bytesForSamp = bitDepth / 8;
                    int nValues = bytes / bytesForSamp;


                    float[] asFloat = null;
                    switch (bitDepth)
                    {
                        case 64:
                            double[]
                                asDouble = new double[nValues];
                            Buffer.BlockCopy(byteArray, 0, asDouble, 0, bytes);
                            asFloat = Array.ConvertAll(asDouble, e => (float)e);
                            break;
                        case 32:
                            asFloat = new float[nValues];
                            Buffer.BlockCopy(byteArray, 0, asFloat, 0, bytes);
                            break;
                        case 16:
                            Int16[]
                                asInt16 = new Int16[nValues];
                            Buffer.BlockCopy(byteArray, 0, asInt16, 0, bytes);
                            asFloat = Array.ConvertAll(asInt16, e => e / (float)(Int16.MaxValue + 1));
                            break;
                        default:
                            return false;
                    }

                    switch (channels)
                    {
                        case 1:
                            L = asFloat;
                            R = null;
                            return true;
                        case 2:
                            // de-interleave
                            int nSamps = nValues / 2;
                            L = new float[nSamps];
                            R = new float[nSamps];
                            for (int s = 0, v = 0; s < nSamps; s++)
                            {
                                L[s] = asFloat[v++];
                                R[s] = asFloat[v++];
                            }
                            return true;
                        default:
                            return false;
                    }
                }
            }
            catch
            {
                Debug.Log("...Failed to load: " + filename);
                return false;
            }

            return false;
        }


        //private void fuckmetoo(uint playingID, IntPtr format)
        //{
        //    DebugClass.Log($"id:{playingID}, format: {format}");
        //}

        //private bool fuckmeiguess(uint playingID, float[] samples, uint channelIndex, uint frames)
        //{
        //    playingID = 1;
        //    samples = wavtofloatarray("creeper.wav");
        //    channelIndex = 1;
        //    frames = 120;

        //    DebugClass.Log($"id:{playingID}, samples: {samples}, channlIndex: {channelIndex}, frames: {frames}");

        //    return true;
        //}

        //private float[] wavtofloatarray(string file)
        //{

        //    using (WaveFileReader reader = new WaveFileReader(file))
        //    {
        //        Assert.AreEqual(16, reader.WaveFormat.BitsPerSample, "Only works with 16 bit audio");
        //        byte[] buffer = new byte[reader.Length];
        //        int read = reader.Read(buffer, 0, buffer.Length);
        //        short[] sampleBuffer = new short[read / 2];
        //        Buffer.BlockCopy(buffer, 0, sampleBuffer, 0, read);
        //    }

        //    //float[] floatBuffer;
        //    //using (MediaFoundationReader media = new MediaFoundationReader(file))
        //    //{
        //    //    int _byteBuffer32_length = (int)media.Length * 2;
        //    //    int _floatBuffer_length = _byteBuffer32_length / sizeof(float);

        //    //    IWaveProvider stream32 = new Wave16ToFloatProvider(media);
        //    //    WaveBuffer _waveBuffer = new WaveBuffer(_byteBuffer32_length);
        //    //    stream32.Read(_waveBuffer, 0, (int)_byteBuffer32_length);
        //    //    floatBuffer = new float[_floatBuffer_length];

        //    //    for (int i = 0; i < _floatBuffer_length; i++)
        //    //    {
        //    //        floatBuffer[i] = _waveBuffer.FloatBuffer[i];
        //    //    }
        //    //}
        //    //return floatBuffer;

        //    //using (FileStream fs = File.Open(file, FileMode.Open))
        //    //{
        //    //    BinaryReader reader = new BinaryReader(fs);

        //    //    int chunkID = reader.ReadInt32();
        //    //    int fileSize = reader.ReadInt32();
        //    //    int riffType = reader.ReadInt32();
        //    //    int fmtID;

        //    //    long _position = reader.BaseStream.Position;
        //    //    while (_position != reader.BaseStream.Length - 1)
        //    //    {
        //    //        reader.BaseStream.Position = _position;
        //    //        int _fmtId = reader.ReadInt32();
        //    //        if (_fmtId == 544501094)
        //    //        {
        //    //            fmtID = _fmtId;
        //    //            break;
        //    //        }
        //    //        _position++;
        //    //    }
        //    //    int fmtSize = reader.ReadInt32();
        //    //    int fmtCode = reader.ReadInt16();

        //    //    int channels = reader.ReadInt16();
        //    //    int sampleRate = reader.ReadInt32();
        //    //    int byteRate = reader.ReadInt32();
        //    //    int fmtBlockAlign = reader.ReadInt16();
        //    //    int bitDepth = reader.ReadInt16();

        //    //    int fmtExtraSize;
        //    //    if (fmtSize == 18)
        //    //    {
        //    //        fmtExtraSize = reader.ReadInt16();
        //    //        reader.ReadBytes(fmtExtraSize);
        //    //    }

        //    //    int dataID = reader.ReadInt32();
        //    //    int dataSize = reader.ReadInt32();

        //    //    byte[] byteArray = reader.ReadBytes(dataSize);

        //    //    int bytesForSamp = bitDepth / 8;
        //    //    int samps = dataSize / bytesForSamp;

        //    //    float[] asFloat = null;
        //    //    DebugClass.Log($"BitDepth: {bitDepth}");
        //    //    switch (bitDepth)
        //    //    {
        //    //        case 16:
        //    //            Int16[] asInt16 = new Int16[samps];
        //    //            Buffer.BlockCopy(byteArray, 0, asInt16, 0, dataSize);
        //    //            IEnumerable<float> tempInt16 =
        //    //                from i in asInt16
        //    //                select i / (float)Int16.MaxValue;
        //    //            asFloat = tempInt16.ToArray();
        //    //            break;
        //    //        default:
        //    //            break;
        //    //    }

        //    //    return asFloat;
        //    //}
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
