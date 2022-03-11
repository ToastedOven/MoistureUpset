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

namespace MoistureUpset.Skins
{
    public static class AnimationReplacements
    {
        public static void RunAll()
        {
            HumanBodyBones[] dab = new HumanBodyBones[] { HumanBodyBones.LeftUpperLeg, HumanBodyBones.RightUpperLeg };
            HumanBodyBones[] Facepalm = new HumanBodyBones[] { HumanBodyBones.LeftUpperLeg, HumanBodyBones.RightUpperLeg, HumanBodyBones.LeftUpperArm };
            HumanBodyBones[] hips = new HumanBodyBones[] { HumanBodyBones.Hips };
            EnemyReplacements.LoadResource("moisture_animationreplacements");
            EnemyReplacements.LoadBNK("Emotes");
            CustomEmotesAPI.AddCustomAnimation(Assets.Load<AnimationClip>("@MoistureUpset_moisture_animationreplacements:assets/animationreplacements/Loser.anim"), true, "Loser", "LoserStop", dimWhenClose: true);
            CustomEmotesAPI.AddCustomAnimation(Assets.Load<AnimationClip>("@MoistureUpset_moisture_animationreplacements:assets/animationreplacements/headspinstart.anim"), false, secondaryAnimation: Assets.Load<AnimationClip>("@MoistureUpset_moisture_animationreplacements:assets/animationreplacements/headspinloop.anim"));
            CustomEmotesAPI.AddCustomAnimation(Assets.Load<AnimationClip>("@MoistureUpset_moisture_animationreplacements:assets/animationreplacements/Dab.anim"), false, "Dab", "DabStop", dab, hips);
            CustomEmotesAPI.AddCustomAnimation(Assets.Load<AnimationClip>("@MoistureUpset_moisture_animationreplacements:assets/animationreplacements/Floss.anim"), true, "Floss", "FlossStop", dimWhenClose: true);
            CustomEmotesAPI.AddCustomAnimation(Assets.Load<AnimationClip>("@MoistureUpset_moisture_animationreplacements:assets/animationreplacements/Default Dance.anim"), false, "DefaultDance", "DefaultDanceStop", dimWhenClose: true);
            CustomEmotesAPI.AddCustomAnimation(Assets.Load<AnimationClip>("@MoistureUpset_moisture_animationreplacements:assets/animationreplacements/Caramelldansen.anim"), false);
            CustomEmotesAPI.AddCustomAnimation(Assets.Load<AnimationClip>("@MoistureUpset_moisture_animationreplacements:assets/animationreplacements/Facepalm.anim"), false, "", "", Facepalm, hips);
            CustomEmotesAPI.AddCustomAnimation(Assets.Load<AnimationClip>("@MoistureUpset_moisture_animationreplacements:assets/animationreplacements/Orange Justice.anim"), true, "OrangeJustice", "OrangeJusticeStop", dimWhenClose: true);
            CustomEmotesAPI.AddCustomAnimation(Assets.Load<AnimationClip>("@MoistureUpset_moisture_animationreplacements:assets/animationreplacements/nobones.anim"), true);
        }
    }
}
