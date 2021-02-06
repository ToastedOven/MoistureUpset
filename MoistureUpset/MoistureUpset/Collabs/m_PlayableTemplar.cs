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
using PlayableTemplar;

namespace MoistureUpset.Collabs
{
    class m_PlayableTemplar
    {
        public static void Run()
        {
            PlayableTemplar.PlayableTemplar t = new PlayableTemplar.PlayableTemplar();
            EnemyReplacements.ReplaceModel(t.GetFieldValue<PlayableTemplar.PlayableTemplar>("instance").myCharacter, "@MoistureUpset_heavy:assets/heavy.mesh", 0);
            EnemyReplacements.ReplaceModel(t.GetFieldValue<PlayableTemplar.PlayableTemplar>("instance").myCharacter, "@MoistureUpset_heavy:assets/minigun.mesh", 1);
            EnemyReplacements.ReplaceModel(t.GetFieldValue<PlayableTemplar.PlayableTemplar>("instance").characterDisplay, "@MoistureUpset_heavy:assets/heavy.mesh", 0);
            EnemyReplacements.ReplaceModel(t.GetFieldValue<PlayableTemplar.PlayableTemplar>("instance").characterDisplay, "@MoistureUpset_heavy:assets/minigun.mesh", 1);

            var meshes = t.GetFieldValue<PlayableTemplar.PlayableTemplar>("instance").myCharacter.GetComponentsInChildren<SkinnedMeshRenderer>();
            for (int i = 0; i < meshes.Length; i++)
            {
                if (i != 0 && i != 1)
                {
                    meshes[i].sharedMesh = Resources.Load<Mesh>("@MoistureUpset_na:assets/na.mesh");
                }
            }
            meshes = t.GetFieldValue<PlayableTemplar.PlayableTemplar>("instance").characterDisplay.GetComponentsInChildren<SkinnedMeshRenderer>();
            for (int i = 0; i < meshes.Length; i++)
            {
                if (i != 0 && i != 1)
                {
                    meshes[i].sharedMesh = Resources.Load<Mesh>("@MoistureUpset_na:assets/na.mesh");
                }
            }
        }
    }
}
