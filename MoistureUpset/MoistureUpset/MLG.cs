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
        float progress = 0;
        float timer = 0;
        public static CharacterBody localBody = null;
        public static void Setup()
        {
            EnemyReplacements.LoadResource("2014");
            EnemyReplacements.LoadBNK("MLG");
        }
        private void StopSounds()
        {
            AkSoundEngine.ExecuteActionOnEvent(3140226740, AkActionOnEventType.AkActionOnEventType_Stop);
            AkSoundEngine.ExecuteActionOnEvent(2691109812, AkActionOnEventType.AkActionOnEventType_Stop);
            AkSoundEngine.ExecuteActionOnEvent(704831768, AkActionOnEventType.AkActionOnEventType_Stop);
            AkSoundEngine.ExecuteActionOnEvent(704831771, AkActionOnEventType.AkActionOnEventType_Stop);
        }
        CameraRigController cam;
        float FOVtimer = 0;
        private void Hooks()
        {
            On.RoR2.PlayerCharacterMasterController.OnBodyStart += (orig, self) =>
            {
                orig(self);
                try
                {
                    StopSounds();
                    progress = 0;
                    timer = 0;
                    prevStage = 0;
                    musicLock = 0;
                    increasingDecay = 0;
                    localBody = NetworkUser.readOnlyLocalPlayersList[0].master?.GetBody();
                    GameObject player = localBody.gameObject;
                    memeEmitter = GameObject.Instantiate<GameObject>(Resources.Load<GameObject>("@MoistureUpset_2014:assets/2014/MLGEmitter.prefab"));
                    memeEmitter.transform.SetParent(player.transform);
                    memeEmitter.transform.localPosition = Vector3.zero;

                    //foreach (var item in Camera.allCameras)
                    //{
                    //    if (item.GetComponent<CameraRigController>())
                    //    {
                    //        DebugClass.Log($"----------{item}");
                    //    }
                    ////    if (!item.GetComponent<MLGEffect>())
                    ////    {
                    ////        var effect = item.gameObject.AddComponent<MLGEffect>();
                    ////        effect.Shader = Resources.Load<Shader>("@MoistureUpset_2014:assets/2014/TestShader.shader");
                    ////        effect.displacementMap = Resources.Load<Texture2D>("@MoistureUpset_moisture_bonzistatic:assets/bonzibuddy/test.png");
                    ////        effect.intensity = 0;
                    ////        effect.flipIntensity = 0;
                    ////        effect.colorIntensity = 0;
                    ////    }
                    //}
                    DebugClass.Log($"----------{GameObject.Find("Main Camera(Clone)")}");

                    DebugClass.Log($"----------got a camera   {GameObject.Find("Main Camera(Clone)")}     {cam}");
                }
                catch (Exception e)
                {
                    DebugClass.Log($"----------{e}");
                }
                //Main Camera(Clone)
                //foreach (var item in Camera.allCameras)
                //{
                //    if (!item.GetComponent<MLGEffect>())
                //    {
                //        var effect = item.gameObject.AddComponent<MLGEffect>();
                //        effect.Shader = Resources.Load<Shader>("@MoistureUpset_2014:assets/2014/TestShader.shader");
                //        effect.displacementMap = Resources.Load<Texture2D>("@MoistureUpset_moisture_bonzistatic:assets/bonzibuddy/test.png");
                //        effect.intensity = 0;
                //        effect.flipIntensity = 0;
                //        effect.colorIntensity = 0;
                //    }
                //}
            };
            On.RoR2.CharacterMaster.OnBodyDamaged += (orig, self, report) =>
            {
                try
                {
                    if (report.victim && report.attacker)
                    {
                        if (report.attackerBody == localBody)
                        {
                            progress += .00025f * report.damageDealt;
                            timer += .1f;
                            if (report.combinedHealthBeforeDamage < report.damageDealt)
                            {
                                timer += .75f;
                                progress += .045f;
                                if (progress > 2)
                                {
                                    cam = GameObject.Find("Main Camera(Clone)").GetComponent<CameraRigController>();
                                    cam.baseFov = 1;
                                    FOVtimer = .05f;
                                    //DebugClass.Log($"----------{cam.sceneCam.fieldOfView}");
                                    AkSoundEngine.PostEvent("EnemyKillWhileMaxDank", report.victimBody.gameObject);
                                }
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    DebugClass.Log($"----------{e}");
                }
                orig(self, report);
            };
        }
        void Start()
        {
            Hooks();
        }

        int prevStage = 0;
        float musicLock = 0;
        float increasingDecay = 0;
        void Update()
        {
            if (localBody != null)
            {
                if (FOVtimer >= 0)
                {
                    FOVtimer -= Time.deltaTime;
                }
                else
                {
                    cam = GameObject.Find("Main Camera(Clone)").GetComponent<CameraRigController>();
                    cam.baseFov = 60;
                }
                if (musicLock > 0)
                {
                    musicLock -= Time.deltaTime;
                }
                if (timer > 0)
                {
                    if (timer > 3)
                    {
                        timer = 3;
                    }
                    timer -= Time.deltaTime;
                    increasingDecay = 0;
                }
                else
                {
                    increasingDecay += Time.deltaTime * .0035f;
                }
                if (progress > 0 && timer <= 0)
                {
                    progress -= (Time.deltaTime * .02f) + (Time.deltaTime * increasingDecay);
                    if (progress < 0)
                    {
                        progress = 0;
                    }
                }
                if (progress > 3)
                {
                    progress = 2.9f;
                }
                if ((int)progress != prevStage)
                {
                    //we went up or down
                    if (musicLock <= 0)
                    {
                        switch ((int)progress)
                        {
                            case 0:
                                AkSoundEngine.PostEvent("NoMLG", localBody.gameObject);
                                break;
                            case 1:
                                if (prevStage == 0)
                                {
                                    AkSoundEngine.PostEvent("HopeWillDieStage1", localBody.gameObject);
                                }
                                else if (prevStage == 2)
                                {
                                    AkSoundEngine.PostEvent("HopeWillDieInterval", localBody.gameObject);
                                }
                                break;
                            case 2:
                                AkSoundEngine.PostEvent("HopeWillDieStage2", localBody.gameObject);
                                break;
                            default:
                                break;
                        }
                        musicLock = 1;
                        if ((int)progress > prevStage)
                        {
                            progress += .2f;
                        }
                        timer += 2;
                    }
                }
                memeEmitter.GetComponent<ParticleSystem>().maxParticles = 3000;
                var Emission = memeEmitter.GetComponent<ParticleSystem>().emission;
                var Shape = memeEmitter.GetComponent<ParticleSystem>().shape;
                Emission.rateOverTime = (int)progress * 270;
                Shape.radius = 250;
                prevStage = (int)progress;
                slider.GetComponent<Slider>().value = progress;
            }
        }
    }
}
