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
//using PlayableGrovetender;

namespace MoistureUpset.Collabs
{
    class m_PlayableGrovetender
    {
        public static Texture[] textures = { Assets.Load<Texture>("@MoistureUpset_moisture_twitch:assets/twitch/yep.png"), Assets.Load<Texture>("@MoistureUpset_moisture_twitch:assets/twitch/trihard.png"), Assets.Load<Texture>("@MoistureUpset_moisture_twitch:assets/twitch/poggers.png"), Assets.Load<Texture>("@MoistureUpset_moisture_twitch:assets/twitch/pjsalt.png"), Assets.Load<Texture>("@MoistureUpset_moisture_twitch:assets/twitch/pepehands.png"), Assets.Load<Texture>("@MoistureUpset_moisture_twitch:assets/twitch/monkaw.png"), Assets.Load<Texture>("@MoistureUpset_moisture_twitch:assets/twitch/monkagiga.png"), Assets.Load<Texture>("@MoistureUpset_moisture_twitch:assets/twitch/lul.png"), Assets.Load<Texture>("@MoistureUpset_moisture_twitch:assets/twitch/kreygasm.png"), Assets.Load<Texture>("@MoistureUpset_moisture_twitch:assets/twitch/kappa.png"), Assets.Load<Texture>("@MoistureUpset_moisture_twitch:assets/twitch/jebaited.png"), Assets.Load<Texture>("@MoistureUpset_moisture_twitch:assets/twitch/heyguys.png"), Assets.Load<Texture>("@MoistureUpset_moisture_twitch:assets/twitch/dansgame.png"), Assets.Load<Texture>("@MoistureUpset_moisture_twitch:assets/twitch/biblethump.png"), Assets.Load<Texture>("@MoistureUpset_moisture_twitch:assets/twitch/babyrage.png"), Assets.Load<Texture>("@MoistureUpset_moisture_twitch:assets/twitch/4head.png"), Assets.Load<Texture>("@MoistureUpset_moisture_twitch:assets/twitch/5head.png"), Assets.Load<Texture>("@MoistureUpset_moisture_twitch:assets/twitch/ayayaya.png"), Assets.Load<Texture>("@MoistureUpset_moisture_twitch:assets/twitch/D.png"), Assets.Load<Texture>("@MoistureUpset_moisture_twitch:assets/twitch/kekw.jpg"), Assets.Load<Texture>("@MoistureUpset_moisture_twitch:assets/twitch/lulw.png"), Assets.Load<Texture>("@MoistureUpset_moisture_twitch:assets/twitch/peepohappy.png"), Assets.Load<Texture>("@MoistureUpset_moisture_twitch:assets/twitch/pepega.png"), Assets.Load<Texture>("@MoistureUpset_moisture_twitch:assets/twitch/sadge.png"), Assets.Load<Texture>("@MoistureUpset_twitch2:assets/twitch2/feelsweirdman.png") };
        //public static void Run()
        //{

        //    EnemyReplacements.ReplaceModel(PlayableGrovetender.GrovetenderPlugin.myCharacter, "@MoistureUpset_na:assets/na1.mesh", 0);
        //    EnemyReplacements.ReplaceModel(PlayableGrovetender.GrovetenderPlugin.myCharacter, "@MoistureUpset_na:assets/na1.mesh", 1);
        //    EnemyReplacements.ReplaceModel(PlayableGrovetender.GrovetenderPlugin.myCharacter, "@MoistureUpset_moisture_twitch:assets/twitch.mesh", 2);
        //    EnemyReplacements.ReplaceModel(PlayableGrovetender.GrovetenderPlugin.myCharacter, "@MoistureUpset_na:assets/na1.mesh", 3);


        //    EnemyReplacements.ReplaceModel(PlayableGrovetender.GrovetenderPlugin.characterDisplay, "@MoistureUpset_na:assets/na1.mesh", 0);
        //    EnemyReplacements.ReplaceModel(PlayableGrovetender.GrovetenderPlugin.characterDisplay, "@MoistureUpset_na:assets/na1.mesh", 1);
        //    EnemyReplacements.ReplaceModel(PlayableGrovetender.GrovetenderPlugin.characterDisplay, "@MoistureUpset_moisture_twitch:assets/twitch.mesh", 2);
        //    EnemyReplacements.ReplaceModel(PlayableGrovetender.GrovetenderPlugin.characterDisplay, "@MoistureUpset_na:assets/na1.mesh", 3);


        //    var fab = PlayableGrovetender.GrovetenderPlugin.healWispGhost;
        //    fab.AddComponent<RandomTwitch>();
        //    fab.GetComponentInChildren<MeshRenderer>().gameObject.SetActive(false);
        //    var particle = fab.GetComponentsInChildren<ParticleSystemRenderer>()[0];
        //    try
        //    {
        //        particle.material.shader = Shader.Find("Sprites/Default");
        //        particle.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
        //        particle.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        //        particle.material.SetInt("_ZWrite", 0);
        //        particle.material.DisableKeyword("_ALPHATEST_ON");
        //        particle.material.DisableKeyword("_ALPHABLEND_ON");
        //        particle.material.EnableKeyword("_ALPHAPREMULTIPLY_ON");
        //        particle.material.renderQueue = 3000;
        //    }
        //    catch (Exception)
        //    {
        //    }
        //    var p = fab.GetComponentsInChildren<ParticleSystem>()[0];
        //    p.startSpeed = 0;
        //    p.simulationSpace = ParticleSystemSimulationSpace.Local;
        //    p.gravityModifier = 0;
        //    p.maxParticles = 1;
        //    p.startLifetime = 100;
        //    var shape = p.shape;
        //    shape.shapeType = ParticleSystemShapeType.Sprite;

        //    var vel = p.limitVelocityOverLifetime;
        //    vel.enabled = true;
        //    vel.limitMultiplier = 0;
        //    var succ = p.rotationBySpeed;
        //    succ.zMultiplier = 1;
        //}
    }
    public class RandomTwitch : MonoBehaviour
    {
        public static Texture blank = Assets.Load<Texture>("@MoistureUpset_na:assets/blank.png");
        void Start()
        {
            int num = UnityEngine.Random.Range(0, m_PlayableGrovetender.textures.Length);
            GetComponentInChildren<ParticleSystemRenderer>().material.mainTexture = m_PlayableGrovetender.textures[num];
        }
    }
}
