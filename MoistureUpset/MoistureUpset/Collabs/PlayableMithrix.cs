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
using PlayerMithrix;

namespace MoistureUpset.Collabs
{
    class PlayableMithrix
    {
        public static void Run()
        {
            EnemyReplacements.ReplaceModel(PlayerMithrix.MithrixPlugin.myCharacter, "@MoistureUpset_thanos:assets/bosses/thanos.mesh", 0);
            EnemyReplacements.ReplaceModel(PlayerMithrix.MithrixPlugin.myCharacter, "@MoistureUpset_thanos:assets/bosses/infinityhammer.mesh", 2);
            EnemyReplacements.ReplaceModel(PlayerMithrix.MithrixPlugin.myCharacter, "@MoistureUpset_na:assets/na1.mesh", 1);
            EnemyReplacements.ReplaceModel(PlayerMithrix.MithrixPlugin.myCharacter, "@MoistureUpset_na:assets/na1.mesh", 3);
            EnemyReplacements.ReplaceModel(PlayerMithrix.MithrixPlugin.myCharacter, "@MoistureUpset_na:assets/na1.mesh", 4);
            EnemyReplacements.ReplaceMeshFilter(PlayerMithrix.MithrixPlugin.myCharacter, "@MoistureUpset_na:assets/na1.mesh", 0);
            EnemyReplacements.ReplaceMeshFilter(PlayerMithrix.MithrixPlugin.myCharacter, "@MoistureUpset_na:assets/na1.mesh", 1);
            EnemyReplacements.ReplaceMeshFilter(PlayerMithrix.MithrixPlugin.myCharacter, "@MoistureUpset_na:assets/na1.mesh", 2);




            EnemyReplacements.ReplaceModel(PlayerMithrix.MithrixPlugin.characterDisplay, "@MoistureUpset_thanos:assets/bosses/thanos.mesh", 0);
            EnemyReplacements.ReplaceModel(PlayerMithrix.MithrixPlugin.characterDisplay, "@MoistureUpset_thanos:assets/bosses/infinityhammer.mesh", 2);
            EnemyReplacements.ReplaceModel(PlayerMithrix.MithrixPlugin.characterDisplay, "@MoistureUpset_NA:assets/na1.mesh", 1);
            EnemyReplacements.ReplaceModel(PlayerMithrix.MithrixPlugin.characterDisplay, "@MoistureUpset_NA:assets/na1.mesh", 3);
            EnemyReplacements.ReplaceModel(PlayerMithrix.MithrixPlugin.characterDisplay, "@MoistureUpset_NA:assets/na1.mesh", 4);
            EnemyReplacements.ReplaceMeshFilter(PlayerMithrix.MithrixPlugin.characterDisplay, "@MoistureUpset_NA:assets/na1.mesh", 0);
            EnemyReplacements.ReplaceMeshFilter(PlayerMithrix.MithrixPlugin.characterDisplay, "@MoistureUpset_NA:assets/na1.mesh", 1);
            EnemyReplacements.ReplaceMeshFilter(PlayerMithrix.MithrixPlugin.characterDisplay, "@MoistureUpset_NA:assets/na1.mesh", 2);
        }
    }
}
