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
            CustomEmotesAPI.AddCustomAnimation("Loser", "@MoistureUpset_moisture_animationreplacements:assets/animationreplacements/Loser.anim", true, "Loser", "LoserStop");
            CustomEmotesAPI.AddCustomAnimation("SPEEEN", "@MoistureUpset_moisture_animationreplacements:assets/animationreplacements/headspinstart.anim", false, secondaryAnimation: "@MoistureUpset_moisture_animationreplacements:assets/animationreplacements/headspinloop.anim");
            CustomEmotesAPI.AddCustomAnimation("Dab", "@MoistureUpset_moisture_animationreplacements:assets/animationreplacements/Dab.anim", false, "Dab", "DabStop", dab, hips);
            CustomEmotesAPI.AddCustomAnimation("Floss", "@MoistureUpset_moisture_animationreplacements:assets/animationreplacements/Floss.anim", true, "Floss", "FlossStop");
            CustomEmotesAPI.AddCustomAnimation("Default Dance", "@MoistureUpset_moisture_animationreplacements:assets/animationreplacements/Default Dance.anim", false, "DefaultDance", "DefaultDanceStop");
            CustomEmotesAPI.AddCustomAnimation("Caramelldansen", "@MoistureUpset_moisture_animationreplacements:assets/animationreplacements/Caramelldansen.anim", false);
            CustomEmotesAPI.AddCustomAnimation("Facepalm", "@MoistureUpset_moisture_animationreplacements:assets/animationreplacements/Facepalm.anim", false, "", "", Facepalm, hips);
            CustomEmotesAPI.AddCustomAnimation("Orange Justice", "@MoistureUpset_moisture_animationreplacements:assets/animationreplacements/Orange Justice.anim", true, "OrangeJustice", "OrangeJusticeStop");
            CustomEmotesAPI.AddCustomAnimation("nobones", "@MoistureUpset_moisture_animationreplacements:assets/animationreplacements/nobones.anim", true);
        }
    }
}
