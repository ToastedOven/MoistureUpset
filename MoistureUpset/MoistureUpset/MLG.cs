using HG;
using MoistureUpset.NetMessages;
using R2API;
using R2API.Networking.Interfaces;
using R2API.Utils;
using RoR2;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using RoR2.Achievements;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MoistureUpset
{
    public class MLG : MonoBehaviour
    {
        public static MLG MemeMachine;
        GameObject memeEmitter = null;
        public GameObject slider = null;
        public GameObject UIAnimator = null;
        private ParticleSystem HitMarkerCreator = null;
        private ParticleSystem DeathEffects = null;
        public static float progress = 0;
        float timer = 0;
        public static CharacterBody localBody = null;
        GameObject HUD;
        Sprite stage0 = Assets.Load<Sprite>("@MoistureUpset_2014:assets/2014/Progress/Stage0.png");
        Sprite stage1 = Assets.Load<Sprite>("@MoistureUpset_2014:assets/2014/Progress/Stage1.png");
        Sprite stage2 = Assets.Load<Sprite>("@MoistureUpset_2014:assets/2014/Progress/Stage2Alt.png");
        GameObject sliderObject;
        int ENABLE = -1;
        public struct AudioTrack
        {
            public string Stage1;
            public string Stage2;
            public string Interval;
            public float Stage1StartDuration;
        }
        public List<AudioTrack> tracks = new List<AudioTrack>();
        public int ActiveTrack = -1;

        public static void Setup()
        {
            EnemyReplacements.LoadResource("2014");
            EnemyReplacements.LoadBNK("MLG");
        }
        private void StopSounds()
        {
            AkSoundEngine.ExecuteActionOnEvent(43089548, AkActionOnEventType.AkActionOnEventType_Stop);
            AkSoundEngine.ExecuteActionOnEvent(196276800, AkActionOnEventType.AkActionOnEventType_Stop);
            AkSoundEngine.ExecuteActionOnEvent(196276803, AkActionOnEventType.AkActionOnEventType_Stop);
            AkSoundEngine.ExecuteActionOnEvent(3140226740, AkActionOnEventType.AkActionOnEventType_Stop);
            AkSoundEngine.ExecuteActionOnEvent(2691109812, AkActionOnEventType.AkActionOnEventType_Stop);
            AkSoundEngine.ExecuteActionOnEvent(704831768, AkActionOnEventType.AkActionOnEventType_Stop);
            AkSoundEngine.ExecuteActionOnEvent(704831771, AkActionOnEventType.AkActionOnEventType_Stop);
            AkSoundEngine.ExecuteActionOnEvent(933420010, AkActionOnEventType.AkActionOnEventType_Stop);
            AkSoundEngine.ExecuteActionOnEvent(2961176058, AkActionOnEventType.AkActionOnEventType_Stop);
            AkSoundEngine.ExecuteActionOnEvent(2961176057, AkActionOnEventType.AkActionOnEventType_Stop);
            AkSoundEngine.ExecuteActionOnEvent(2017980238, AkActionOnEventType.AkActionOnEventType_Stop);
            AkSoundEngine.ExecuteActionOnEvent(575336414, AkActionOnEventType.AkActionOnEventType_Stop);
            AkSoundEngine.ExecuteActionOnEvent(575336413, AkActionOnEventType.AkActionOnEventType_Stop);
            AkSoundEngine.ExecuteActionOnEvent(2067785430, AkActionOnEventType.AkActionOnEventType_Stop);
            AkSoundEngine.ExecuteActionOnEvent(1576102486, AkActionOnEventType.AkActionOnEventType_Stop);
            AkSoundEngine.ExecuteActionOnEvent(1576102485, AkActionOnEventType.AkActionOnEventType_Stop);
            AkSoundEngine.SetRTPCValue("MLGActive", 0);
            ActiveTrack = UnityEngine.Random.Range(0, tracks.Count);
        }
        CameraRigController cam;
        float FOVtimer = 0;

        ShakeEmitter milkshake;
        float origFOV = -1;
        float currFOV = 60;
        private void Hooks()
        {
            On.EntityStates.GenericCharacterDeath.OnEnter += (orig, self) =>
            {
                orig(self);
                try
                {

                    if (self.outer.gameObject.GetComponent<HealthComponent>().lastHitAttacker.GetComponent<CharacterBody>() == localBody)
                    {
                        Vector3 pos = self.outer.gameObject.transform.position;
                        timer += 1.25f;
                        progress += .025f;
                        if (stage1Timer > tracks[ActiveTrack].Stage1StartDuration)
                        {
                            progress += .05f;
                            timer -= .75f;
                        }
                        if (progress > 2)
                        {
                            if (!cam)
                            {
                                cam = GameObject.Find("Main Camera(Clone)").GetComponent<CameraRigController>();
                                //new Material(Assets.Load<Shader>("@MoistureUpset_2014:assets/2014/TutorialShader.shader"));
                            }


                            milkshake = localBody.gameObject.AddComponent<ShakeEmitter>();
                            milkshake.wave = new Wave
                            {
                                amplitude = 1f,
                                frequency = 180f,
                                cycleOffset = 0f
                            };
                            milkshake.duration = .15f;
                            milkshake.radius = 400f;
                            milkshake.amplitudeTimeDecay = false;
                            if (origFOV == -1)
                            {
                                //origFOV = localBody.GetComponentInChildren<EntityStateMachine>().commonComponents.cameraTargetParams.cameraParams.data.fov.value;
                                origFOV = 60;
                            }
                            currFOV = 1;
                            //CharacterCameraParamsData dat = new CharacterCameraParamsData();
                            //dat.fov = 1;
                            //localBody.GetComponentInChildren<EntityStateMachine>().commonComponents.cameraTargetParams.AddParamsOverride(new CameraTargetParams.CameraParamsOverrideRequest
                            //{
                            //    cameraParamsData = dat
                            //}, 1);
                            //localBody.GetComponentInChildren<EntityStateMachine>().commonComponents.cameraTargetParams.cameraParams.data.fov = 1;
                            FOVtimer = .06f;


                            if (Vector3.Distance(localBody.transform.position, self.outer.gameObject.transform.position) > 50)
                            {
                                UIAnimator.GetComponent<Animator>().SetTrigger("QuickScope");
                                AkSoundEngine.PostEvent("FarEnemyKilled", localBody.gameObject);
                            }


                            DeathEffects.gameObject.transform.position = self.outer.gameObject.transform.position;
                            pos = self.outer.gameObject.transform.position;
                            pos.x += UnityEngine.Random.Range(-.5f, .5f);
                            pos.y += UnityEngine.Random.Range(-.5f, .5f);
                            pos.z += UnityEngine.Random.Range(-.5f, .5f);
                            ParticleSystem.EmitParams emitParams = new ParticleSystem.EmitParams
                            {
                                angularVelocity = 0,
                                position = pos,
                                velocity = Vector3.zero,
                                startLifetime = 2.5f
                            };
                            DeathEffects.Emit(emitParams, 1);
                            DeathEffects.gameObject.transform.position = pos;
                            AkSoundEngine.PostEvent("EnemyKillWhileMaxDank", DeathEffects.gameObject);

                            rainbowTimer = .75f;
                        }
                    }
                }
                catch (Exception)
                {
                }
            };
            On.RoR2.GlobalEventManager.ClientDamageNotified += (orig, message) =>
            {
                orig(message);
                try
                {
                    if (/*message.victim && */message.attacker)
                    {
                        if (message.attacker.GetComponent<CharacterBody>() == localBody)
                        {
                            progress += .00015f * message.damage;
                            timer += .5f;
                            if (stage1Timer > tracks[ActiveTrack].Stage1StartDuration)
                            {
                                progress += .0003f * message.damage;
                                timer -= .25f;
                            }
                            Vector3 pos = message.position;
                            if (progress > 1)
                            {
                                if (message.damageType == DamageType.BleedOnHit || message.damageType == DamageType.DoT || message.damageType == DamageType.PoisonOnHit || message.damageType == DamageType.IgniteOnHit)
                                {
                                    pos.x += UnityEngine.Random.Range(-.5f, .5f);
                                    pos.y += UnityEngine.Random.Range(-.5f, .5f);
                                    pos.z += UnityEngine.Random.Range(-.5f, .5f);
                                }
                                ParticleSystem.EmitParams emitParams = new ParticleSystem.EmitParams
                                {
                                    angularVelocity = 0,
                                    position = pos,
                                    startSize = 1,
                                    velocity = Vector3.zero
                                };
                                HitMarkerCreator.Emit(emitParams, 1);
                            }
                            if (progress > 2.9999f)
                            {
                                progress = 2.9999f;
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    DebugClass.Log($"----------{e}");
                }
            };
            On.RoR2.SceneCatalog.OnActiveSceneChanged += (orig, oldS, newS) =>
            {
                orig(oldS, newS);
                StopSounds();
                AkSoundEngine.SetRTPCValue("BeforeBoss", 0);
                progress = 0;
                bossOver = false;
            };
            On.RoR2.UI.ObjectivePanelController.FinishTeleporterObjectiveTracker.ctor += (orig, self) =>
            {
                orig(self);
                AkSoundEngine.SetRTPCValue("BeforeBoss", 0);
            };
            On.RoR2.CharacterBody.GetSubtitle += (orig, self) =>
            {
                if (self.baseNameToken == "ARTIFACTSHELL_BODY_NAME" || self.baseNameToken == "TITANGOLD_BODY_NAME" || self.baseNameToken == "SUPERROBOBALLBOSS_BODY_NAME" || self.baseNameToken == "BROTHER_BODY_NAME" || self.baseNameToken == "LUNARGOLEM_BODY_NAME" || self.baseNameToken == "LUNARWISP_BODY_NAME" || self.baseNameToken == "COMMANDO_BODY_NAME" || self.baseNameToken == "MERC_BODY_NAME" || self.baseNameToken == "ENGI_BODY_NAME" || self.baseNameToken == "HUNTRESS_BODY_NAME" || self.baseNameToken == "MAGE_BODY_NAME" || self.baseNameToken == "TOOLBOT_BODY_NAME" || self.baseNameToken == "TREEBOT_BODY_NAME" || self.baseNameToken == "LOADER_BODY_NAME" || self.baseNameToken == "CROCO_BODY_NAME" || self.baseNameToken == "CAPTAIN_BODY_NAME")
                {
                    return orig(self);
                }
                if (self.master && self.master.isBoss)
                {
                    AkSoundEngine.SetRTPCValue("BeforeBoss", 1);
                }
                return orig(self);
            };
        }
        bool active = false;
        void Start()
        {
            active = BigJank.getOptionValue(Settings.MLGMode);
            if (active)
            {
                Hooks();
            }
        }
        static internal bool bossOver = false;
        int prevStage = 0;
        float rainbowTimer = 0;
        float increasingDecay = 0;
        Color destColor = Color.red;
        bool FastApproximately(float a, float b, float threshold)
        {
            return ((a - b) < 0 ? ((a - b) * -1) : (a - b)) <= threshold;
        }
        bool ColorAlmostEqual(Color a, Color b, float howClose)
        {
            return FastApproximately(a.r, b.r, howClose) && FastApproximately(a.g, b.g, howClose) && FastApproximately(a.b, b.b, howClose);
        }
        bool runLock = false;
        bool stage1Active = false;
        float stage1Timer = 0;
        void FixedUpdate()
        {
            if (ENABLE == -1)
            {
                if (true)//get setting true
                {
                    ENABLE = 1;
                }
                else
                {
                    ENABLE = 0;
                }
            }
            if (ENABLE == 1 && localBody && active)
            {
                if (bossOver)
                {
                    progress = 0;
                }
                if (localBody.isSprinting && !runLock)
                {
                    if (localBody.moveSpeed > 30 && progress > 2)
                    {
                        runLock = true;
                        AkSoundEngine.PostEvent("PlayerRunWhileDank", localBody.gameObject);
                    }
                }
                else if (!localBody.isSprinting)
                {
                    runLock = false;
                }
                if (!ColorAlmostEqual(destColor, DeathEffects.gameObject.GetComponent<Renderer>().material.color, .1f))
                {
                    //HUDSimple(Clone)
                    //var thing = RoR2Application.instance;
                    DeathEffects.gameObject.GetComponent<Renderer>().material.color = Color.Lerp(DeathEffects.gameObject.GetComponent<Renderer>().material.color, destColor, /*Time.deltaTime*/ .013f * 25);
                }
                else
                {
                    if (ColorAlmostEqual(destColor, Color.red, .15f))
                    {
                        destColor = Color.green;
                    }
                    else if (ColorAlmostEqual(destColor, Color.green, .15f))
                    {
                        destColor = Color.blue;
                    }
                    else if (ColorAlmostEqual(destColor, Color.blue, .15f))
                    {
                        destColor = Color.red;
                    }
                }
                if (localBody.GetComponentInChildren<EntityStateMachine>().commonComponents.cameraTargetParams.cameraParams.data.fov.value != currFOV)
                {
                    localBody.GetComponentInChildren<EntityStateMachine>().commonComponents.cameraTargetParams.cameraParams.data.fov = Mathf.Lerp(localBody.GetComponentInChildren<EntityStateMachine>().commonComponents.cameraTargetParams.cameraParams.data.fov.value, currFOV, Time.deltaTime);
                }
                if (FOVtimer >= 0)
                {
                    FOVtimer -= /*Time.deltaTime*/ .01f;
                }
                else
                {
                    if (!cam)
                    {
                        cam = GameObject.Find("Main Camera(Clone)").GetComponent<CameraRigController>();
                        //var comp = cam.uiCam.gameObject.AddComponent<MLGCamera>();
                        //comp.material = Assets.Load<Material>("@MoistureUpset_2014:assets/2014/MLGCameraMaterial.mat");
                        //comp.material.shader = Assets.Load<Shader>("@MoistureUpset_2014:assets/2014/TutorialShader.shader");
                    }
                    if (origFOV != -1)
                    {
                        currFOV = origFOV;
                    }
                }
                if (rainbowTimer > 0)
                {
                    rainbowTimer -= /*Time.deltaTime*/ .013f;
                    if (HUD == null)
                    {
                        HUD = GameObject.Find("HUDSimple(Clone)");
                    }
                    else
                    {
                        foreach (var item in HUD.GetComponentsInChildren<CanvasRenderer>())
                        {
                            item.SetColor(DeathEffects.gameObject.GetComponent<Renderer>().material.color);
                        }
                    }
                }
                else if (rainbowTimer > -100)
                {
                    rainbowTimer = -101;
                    if (HUD == null)
                    {
                        HUD = GameObject.Find("HUDSimple(Clone)");
                    }
                    else
                    {
                        foreach (var item in HUD.GetComponentsInChildren<CanvasRenderer>())
                        {
                            item.SetColor(Color.white);
                        }
                    }
                }
                if (timer > 0)
                {
                    if (timer > 7)
                    {
                        timer = 7;
                    }
                    if (stage1Timer > tracks[ActiveTrack].Stage1StartDuration)
                    {
                        if (timer > 3)
                        {
                            timer = 3;
                        }
                    }
                    timer -= /*Time.deltaTime*/ .013f;
                    increasingDecay = 0;
                }
                else
                {
                    increasingDecay += /*Time.deltaTime*/ .011f * .0015f * (int)progress;
                    if (stage1Timer > tracks[ActiveTrack].Stage1StartDuration)
                    {
                        increasingDecay += /*Time.deltaTime*/ .011f * .0015f * (int)progress * 2;
                    }
                }
                if (progress > 0 && timer <= 0)
                {
                    progress -= (/*Time.deltaTime*/ .013f * .015f) + (/*Time.deltaTime*/ .013f * increasingDecay);
                    if (progress < 0)
                    {
                        progress = 0;
                    }
                }
                if (progress > 2.9999f)
                {
                    progress = 2.9999f;
                }
                if ((int)progress != prevStage)
                {
                    stage1Active = false;
                    stage1Timer = 0;
                    switch ((int)progress)
                    {
                        case 0:
                            slider.transform.Find("Image").gameObject.GetComponent<Image>().sprite = stage0;
                            //controller.GetPropertyValue<MusicTrackDef>("currentTrack").Play();
                            AkSoundEngine.SetRTPCValue("MLGActive", 0);
                            AkSoundEngine.PostEvent("NoMLG", localBody.gameObject);
                            ActiveTrack = UnityEngine.Random.Range(0, tracks.Count);
                            break;
                        case 1:
                            slider.transform.Find("Image").gameObject.GetComponent<Image>().sprite = stage1;
                            MoistureUpsetMod.musicController.GetPropertyValue<MusicTrackDef>("currentTrack").Stop();
                            AkSoundEngine.SetRTPCValue("MLGActive", 1);
                            if (prevStage == 0)
                            {
                                stage1Active = true;
                                AkSoundEngine.PostEvent(tracks[ActiveTrack].Stage1, localBody.gameObject);
                            }
                            else if (prevStage == 2)
                            {
                                AkSoundEngine.PostEvent(tracks[ActiveTrack].Interval, localBody.gameObject);
                            }
                            break;
                        case 2:
                            slider.transform.Find("Image").gameObject.GetComponent<Image>().sprite = stage2;
                            MoistureUpsetMod.musicController.GetPropertyValue<MusicTrackDef>("currentTrack").Stop();
                            AkSoundEngine.SetRTPCValue("MLGActive", 1);
                            AkSoundEngine.PostEvent(tracks[ActiveTrack].Stage2, localBody.gameObject);

                            break;
                        default:
                            break;
                    }
                    if ((int)progress > prevStage)
                    {
                        progress = (int)progress + .25f;
                        if (progress > 2.9999f)
                        {
                            progress = 2.9999f;
                        }
                    }
                    timer += 7;
                }
                if (stage1Active)
                {
                    stage1Timer += Time.deltaTime;
                }
                if (!sliderObject)
                {
                    sliderObject = slider.transform.Find("WhiteBackground").gameObject;
                }
                if (sliderObject && progress > 2)
                {
                    //sliderObject.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0.0004f);
                    sliderObject.GetComponent<Image>().color = (Color.Lerp(sliderObject.GetComponent<Image>().color, destColor, /*Time.deltaTime*/ .013f * 25));
                }
                else if (sliderObject)
                {
                    sliderObject.GetComponent<Image>().color = new Color(0, 60, 0);
                    //sliderObject.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0.0006f);
                }
                slider.transform.Find("Fill").gameObject.GetComponent<Image>().fillAmount = Mathf.Lerp(slider.transform.Find("Fill").gameObject.GetComponent<Image>().fillAmount, progress - (int)progress, Time.deltaTime * 2);
                memeEmitter.GetComponent<ParticleSystem>().maxParticles = 3000;
                var Emission = memeEmitter.GetComponent<ParticleSystem>().emission;
                var Shape = memeEmitter.GetComponent<ParticleSystem>().shape;
                Emission.rateOverTime = (int)progress * 270;
                Shape.radius = 250;
                prevStage = (int)progress;
                //slider.GetComponentInChildren<Slider>().value = progress - (int)progress;
                //slider.transform.Find("Outline").Find("Mask").GetComponent<RectTransform>().localPosition = new Vector3(4.4f * (progress - (int)progress), 0, 0.0005f);



                //Vector2 temp = RectTransformUtility.WorldToScreenPoint(Camera.current, transform.position);
                //Vector2 screenPos = new Vector2(temp.x / (float)Screen.width, temp.y / (float)Screen.height);
                //DebugClass.Log($"----------{screenPos.x}   {screenPos.y}");
                //slider.GetComponent<RectTransform>().localScale = new Vector3(.5f, .5f, .5f);
                //slider.GetComponent<RectTransform>().localPosition = new Vector3(Screen.width/2f, Screen.height/2f, 37);
            }
            else if (NetworkUser.readOnlyLocalPlayersList.Count > 0 && active)
            {
                localBody = NetworkUser.readOnlyLocalPlayersList[0].master?.GetBody();
                if (localBody)
                {
                    StopSounds();
                    progress = 0;
                    timer = 0;
                    prevStage = 0;
                    rainbowTimer = 0;
                    increasingDecay = 0;
                    GameObject player = localBody.gameObject;
                    if (!memeEmitter)
                        memeEmitter = GameObject.Instantiate<GameObject>(Assets.Load<GameObject>("@MoistureUpset_2014:assets/2014/MLGEmitter.prefab"));
                    memeEmitter.transform.SetParent(player.transform);
                    memeEmitter.transform.localPosition = Vector3.zero;

                    if (!HitMarkerCreator)
                    {
                        GameObject hitmarker = GameObject.Instantiate<GameObject>(Assets.Load<GameObject>("@MoistureUpset_2014:assets/2014/HitMarkerCreator.prefab"));
                        hitmarker.transform.SetParent(player.transform);
                        hitmarker.transform.localPosition = Vector3.zero;
                        HitMarkerCreator = hitmarker.GetComponent<ParticleSystem>();
                    }

                    if (!DeathEffects)
                    {
                        GameObject deatheffect = GameObject.Instantiate<GameObject>(Assets.Load<GameObject>("@MoistureUpset_2014:assets/2014/DeathEffectsCreator.prefab"));
                        DeathEffects = deatheffect.GetComponent<ParticleSystem>();
                    }

                    if (!slider)
                        slider = Instantiate(Assets.Load<GameObject>("@MoistureUpset_2014:assets/2014/Progress/DankMeter.prefab"));
                    //DontDestroyOnLoad(slider);
                    slider.GetComponent<RectTransform>().SetParent(GameObject.Find("HUDSimple(Clone)").transform.Find("MainContainer"), false);
                    slider.SetActive(true);
                    //slider.GetComponent<RectTransform>().localPosition = new Vector3(slider.GetComponent<RectTransform>().localPosition.x, slider.GetComponent<RectTransform>().localPosition.y + 5, slider.GetComponent<RectTransform>().localPosition.z);
                    //slider.GetComponent<RectTransform>().anchorMin = Vector2.zero;
                    //slider.GetComponent<RectTransform>().anchorMax = Vector2.zero;
                    //slider.layer = 5;
                    //foreach (var item in slider.GetComponentsInChildren<GameObject>())
                    //{
                    //    item.layer = 5;
                    //}
                    //slider.GetComponent<RectTransform>().localScale = new Vector3(.5f, .5f, .5f);
                    //slider.GetComponent<RectTransform>().localPosition = new Vector3(-929.4f, -550, 37);
                    //slider.GetComponent<RectTransform>().localEulerAngles = new Vector3(0, 10, 0);
                    //slider.GetComponent<RectTransform>().localScale = new Vector3(.4f, .4f, .4f);
                    MLG.MemeMachine.slider = slider;
                }
            }
        }
    }
}
