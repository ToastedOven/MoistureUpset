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
using Lemurian;

namespace MoistureUpset.Collabs
{
    class PlayableLemurian
    {
        public static void Run()
        {
            EnemyReplacements.ReplaceModel(Lemurian.Lemurian.ElderLemurianPrefab, "@MoistureUpset_bowser:assets/bowser.mesh");
            EnemyReplacements.ReplaceModel(Lemurian.Lemurian.LemurianPrefab, "@MoistureUpset_mike:assets/mike.mesh");
            foreach (var item in SurvivorCatalog.allSurvivorDefs)
            {
                if (item.bodyPrefab.name == "LEMURIAN_NAME")
                {
                    EnemyReplacements.ReplaceModel(item.displayPrefab, "@MoistureUpset_mike:assets/mike.mesh");
                }
            }
        }
    }
}
