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
using RiskOfOptions;
using TMPro;
using R2API.Networking.Interfaces;
using UnityEngine.Animations;
using UnityEngine.UI;
using EmotesAPI;
using System.Collections;

namespace MoistureUpset.Skins
{
    public static class AnimationReplacements
    {
        public static void RunAll()
        {
            GameObject manager = new GameObject();
            manager.AddComponent<EffectManager>();
            UnityEngine.Object.DontDestroyOnLoad(manager);

            CustomEmotesAPI.AddNonAnimatingEmote("firework");
            CustomEmotesAPI.AddNonAnimatingEmote("debug time");
            CustomEmotesAPI.AddNonAnimatingEmote("kill stuff");
            CustomEmotesAPI.AddNonAnimatingEmote("end me");

            CustomEmotesAPI.AddNonAnimatingEmote("50 golems");
            CustomEmotesAPI.AddNonAnimatingEmote("getSongName");
            CustomEmotesAPI.AddNonAnimatingEmote("god");
            CustomEmotesAPI.AddNonAnimatingEmote("noclip");
            CustomEmotesAPI.AddNonAnimatingEmote("golemPlains");









            EnemyReplacements.LoadResource("kazotskykick");
            CustomEmotesAPI.AddCustomAnimation(Assets.Load<AnimationClip>("@MoistureUpset_kazotskykick:assets/kazotsky kick/Engineer Kazotsky Kick Start.anim"), false, secondaryAnimation: Assets.Load<AnimationClip>("@MoistureUpset_kazotskykick:assets/kazotsky kick/Engineer Kazotsky Kick Loop.anim"), syncAnim: true);
            
            
            
            CustomEmotesAPI.animChanged += CustomEmotesAPI_animChanged;

        }
        static BoneMapper bonemap;

        private static void CustomEmotesAPI_animChanged(string newAnimation, BoneMapper mapper)
        {
            bonemap = mapper;
            EffectManager.instance.mapper = bonemap;
            //if (newAnimation == "Default Dance")
            //{
            //    EffectManager.instance.DefaultClap();
            //}
            //else
            //{
            //    EffectManager.instance.StopDefaultClap();
            //}
            if (newAnimation == "firework")
            {
                EffectManager.instance.LaunchFirework();
            }
            else if (newAnimation == "debug time")
            {
                RoR2.Console.instance.SubmitCmd(NetworkUser.readOnlyLocalPlayersList[0], "god");
                RoR2.Console.instance.SubmitCmd(NetworkUser.readOnlyLocalPlayersList[0], "noclip");
                RoR2.Console.instance.SubmitCmd(NetworkUser.readOnlyLocalPlayersList[0], "no_enemies");
                RoR2.Console.instance.SubmitCmd(NetworkUser.readOnlyLocalPlayersList[0], "kill_all");
                RoR2.Console.instance.SubmitCmd(NetworkUser.readOnlyLocalPlayersList[0], "give_money 1000000");
                RoR2.Console.instance.SubmitCmd(NetworkUser.readOnlyLocalPlayersList[0], "give_item SoldiersSyringe 100");
                RoR2.Console.instance.SubmitCmd(NetworkUser.readOnlyLocalPlayersList[0], "give_item AlienHead 100");
                RoR2.Console.instance.SubmitCmd(NetworkUser.readOnlyLocalPlayersList[0], "give_item ShapedGlass 10");
            }
            else if (newAnimation == "kill stuff")
            {
                RoR2.Console.instance.SubmitCmd(NetworkUser.readOnlyLocalPlayersList[0], "god");
                RoR2.Console.instance.SubmitCmd(NetworkUser.readOnlyLocalPlayersList[0], "noclip");
                RoR2.Console.instance.SubmitCmd(NetworkUser.readOnlyLocalPlayersList[0], "give_item SoldiersSyringe 100");
                RoR2.Console.instance.SubmitCmd(NetworkUser.readOnlyLocalPlayersList[0], "give_item AlienHead 100");
                RoR2.Console.instance.SubmitCmd(NetworkUser.readOnlyLocalPlayersList[0], "give_item ShapedGlass 10");
            }
            else if (newAnimation == "end me")
            {
                RoR2.Console.instance.SubmitCmd(NetworkUser.readOnlyLocalPlayersList[0], "give_item ShapedGlass 10");
                RoR2.Console.instance.SubmitCmd(NetworkUser.readOnlyLocalPlayersList[0], "spawn_ai GolemMaster 10");
            }
            else if (newAnimation == "50 golems")
            {
                RoR2.Console.instance.SubmitCmd(NetworkUser.readOnlyLocalPlayersList[0], "spawn_ai GolemMaster 50");
            }
            else if (newAnimation == "noclip")
            {
                RoR2.Console.instance.SubmitCmd(NetworkUser.readOnlyLocalPlayersList[0], "noclip");
            }
            else if (newAnimation == "god")
            {
                RoR2.Console.instance.SubmitCmd(NetworkUser.readOnlyLocalPlayersList[0], "god");
            }
            else if (newAnimation == "getSongName")
            {
                DebugClass.Log($"song name: {MoistureUpsetMod.musicController.GetPropertyValue<MusicTrackDef>("currentTrack").cachedName}");
            }
            else if (newAnimation == "golemPlains")
            {
                RoR2.Console.instance.SubmitCmd(NetworkUser.readOnlyLocalPlayersList[0], "next_stage golemplains");
            }
        }
    }
    public class EffectManager : MonoBehaviour
    {
        public static EffectManager instance;
        internal BoneMapper mapper;
        internal IEnumerator clapRoutine = null;
        public EffectManager()
        {
            instance = this;
        }
        public void DefaultClap()
        {
            StopDefaultClap();
            clapRoutine = DefaultClapFX();
            StartCoroutine(clapRoutine);
        }
        public void StopDefaultClap()
        {
            if (clapRoutine != null)
            {
                StopCoroutine(clapRoutine);
            }
        }
        public IEnumerator DefaultClapFX()
        {
            yield return new WaitForSeconds(.3f);
            var trans = mapper.a2.GetBoneTransform(HumanBodyBones.LeftHand);
            var obj = Assets.Load<GameObject>("@MoistureUpset_moisture_animationreplacements:assets/animationreplacements/testeffects/clap.prefab");
            obj.transform.position = trans.position;
            GameObject.Instantiate(obj);
            yield return new WaitForSeconds(2.06666f);
            obj = Assets.Load<GameObject>("@MoistureUpset_moisture_animationreplacements:assets/animationreplacements/testeffects/clap.prefab");
            obj.transform.position = trans.position;
            GameObject.Instantiate(obj);
        }
        public void LaunchFirework()
        {
            var obj = Assets.Load<GameObject>("@MoistureUpset_moisture_animationreplacements:assets/animationreplacements/testeffects/firework.prefab");
            var trans = mapper.gameObject.transform;
            obj.transform.position = trans.position;
            if (!obj.GetComponent<Firework>())
            {
                obj.AddComponent<Firework>();
            }
            GameObject.Instantiate(obj);
        }
    }
    public class Firework : MonoBehaviour
    {
        float timer = 0;
        void Update()
        {
            timer += Time.deltaTime;
            if (timer > 6)
            {
                Destroy(gameObject);
            }
            else if (timer > 2)
            {
                gameObject.transform.Find("flares").gameObject.SetActive(false);
                gameObject.transform.Find("joy").gameObject.SetActive(true);
            }
            else
            {
                gameObject.transform.position += new Vector3(0, 70 * Time.deltaTime, 0);
            }
        }
    }
}
