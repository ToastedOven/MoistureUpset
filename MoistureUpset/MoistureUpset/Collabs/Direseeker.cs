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
using DireseekerMod;

namespace MoistureUpset.Collabs
{
    public static class Direseeker
    {
        public static void Run()
        {
            EnemyReplacements.LoadResource("moisture_direseeker");
            DireseekerMod.Modules.Prefabs.bodyPrefab.GetComponentInChildren<CharacterModel>().baseRendererInfos[0].defaultMaterial = Resources.Load<Material>("@MoistureUpset_moisture_direseeker:assets/collab/giggabowser.mat");
            DireseekerMod.Modules.Prefabs.bodyPrefab.GetComponentInChildren<CharacterModel>().baseRendererInfos[0].defaultMaterial.shader = Resources.Load<GameObject>("prefabs/characterbodies/LemurianBruiserBody").GetComponentInChildren<SkinnedMeshRenderer>().sharedMaterial.shader;
            DireseekerMod.Modules.Prefabs.bodyPrefab.GetComponentsInChildren<MeshFilter>()[0].sharedMesh = Resources.Load<Mesh>("@MoistureUpset_na:assets/na1.mesh");
            DireseekerMod.Modules.Prefabs.bodyPrefab.GetComponentsInChildren<MeshFilter>()[1].sharedMesh = Resources.Load<Mesh>("@MoistureUpset_na:assets/na1.mesh");
            EnemyReplacements.ReplaceModel(DireseekerMod.Modules.Prefabs.bodyPrefab, "@MoistureUpset_moisture_direseeker:assets/collab/giggabowser.mesh");

            var skills = DireseekerMod.Modules.Prefabs.bodyPrefab.GetComponentInChildren<RoR2.SkillLocator>();
            skills.gameObject.AddComponent<DireSeekerTest>();


            LanguageAPI.Add("DIRESEEKER_BOSS_BODY_NAME", "Giga Bowser");
            LanguageAPI.Add("DIRESEEKER_BOSS_BODY_SUBTITLE", "Rawr! x3");
            LanguageAPI.Add("DIRESEEKER_BOSS_BODY_LORE", "Giga Bowser\n\nGiga Bowser is a giant Elder Lemurian that acts as a boss in the Stage 4 area Magma Barracks. Upon defeating it, the player will unlock the Miner character for future playthroughs. The path leading to Giga Bowser's location only appears in one of the three variants of the level, and even then Giga Bowser may or may not spawn with random chance. Completing the teleporter event will also prevent it from spawning.\nNote that in online co-op the boss may spawn for the Host, but not others, although they can still damage it.\nActivating the Artifact of Kin does not prevent it from appearing.\n\nCategories: Enemies | Bosses | Unlisted Enemies\n\nLanguages: Español");
            LanguageAPI.Add("DIRESEEKER_BOSS_BODY_OUTRO_FLAVOR", "Go to horny jail. B O N K");
            LanguageAPI.Add("DIRESEEKER_SPAWN_WARNING", "<style=cWorldEvent>You hear a distant uwuing..</style>");
            LanguageAPI.Add("DIRESEEKER_SPAWN_BEGIN", "<style=cWorldEvent>The uwuing grows loud.</style>");

            var fab = DireseekerMod.Modules.Assets.bossPortrait;
            byte[] bytes = ByteReader.readbytes("MoistureUpset.Resources.gigabowser.png");
            ((Texture2D)fab).LoadImage(bytes);
        }
    }
    public class DireSeekerTest : MonoBehaviour
    {
        string state1;
        string state2;
        void Start()
        {
            state1 = GetComponents<GenericSkill>()[0].stateMachine.state.ToString();
            state2 = GetComponents<GenericSkill>()[1].stateMachine.state.ToString();
            AkSoundEngine.PostEvent("DireSeekerSpawn", gameObject);
        }
        void Update()
        {
            if (state1 != GetComponents<GenericSkill>()[0].stateMachine.state.ToString())
            {
                state1 = GetComponents<GenericSkill>()[0].stateMachine.state.ToString();
                if (state1 == "DireseekerMod.States.FireUltraFireball")
                {
                    AkSoundEngine.PostEvent("DireSeekerFireball", gameObject);
                }
                else if (state1 == "EntityStates.GenericCharacterDeath")
                {
                    AkSoundEngine.PostEvent("DireSeekerDeath", gameObject);
                    AkSoundEngine.ExecuteActionOnEvent(541788247, AkActionOnEventType.AkActionOnEventType_Stop);
                }
            }
            if (state2 != GetComponents<GenericSkill>()[1].stateMachine.state.ToString())
            {
                state2 = GetComponents<GenericSkill>()[1].stateMachine.state.ToString();
                if (state2 == "DireseekerMod.States.Flamethrower")
                {
                    AkSoundEngine.PostEvent("DireSeekerFlame", gameObject);
                }
                else if (state2 == "DireseekerMod.States.FlamePillars")
                {
                    AkSoundEngine.PostEvent("DireSeekerPillar", gameObject);
                }
                else if (state2 == "DireseekerMod.States.Enrage")
                {
                    AkSoundEngine.PostEvent("DireSeekerRage", gameObject);
                }
                else if (state2 == "EntityStates.GenericCharacterDeath")
                {
                    AkSoundEngine.PostEvent("DireSeekerDeath", gameObject);
                    AkSoundEngine.ExecuteActionOnEvent(541788247, AkActionOnEventType.AkActionOnEventType_Stop);
                }
            }
        }
    }
}
