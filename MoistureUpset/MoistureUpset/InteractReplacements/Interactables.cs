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
using MoistureUpset.InteractReplacements.SodaBarrel;

namespace MoistureUpset.InteractReplacements
{
    public static class Interactables
    {
        public static void Init()
        {
            Chests();
        }
        public static GameObject particles;
        public static void ReloadChests()
        {

            if (float.Parse(ModSettingsManager.getOptionValue("Minecraft Chests")) != 1)
                return;

            var fab = Resources.Load<GameObject>("prefabs/networkedobjects/chest/Chest1");
            EnemyReplacements.ReplaceModel("prefabs/networkedobjects/chest/Chest1", "@MoistureUpset_moisture_chests:assets/arbitraryfolder/smallchest.mesh", "@MoistureUpset_moisture_chests:assets/arbitraryfolder/smallchest.png");
            fab.GetComponentInChildren<SkinnedMeshRenderer>().sharedMaterial.SetTexture("_SplatmapTex", Resources.Load<Texture>("@MoistureUpset_moisture_chests:assets/arbitraryfolder/smallchestsplat.png"));



            EnemyReplacements.ReplaceModel("prefabs/networkedobjects/chest/CategoryChestDamage", "@MoistureUpset_moisture_chests:assets/arbitraryfolder/categorychest.mesh", "@MoistureUpset_moisture_chests:assets/arbitraryfolder/damagechest.png");
            EnemyReplacements.ReplaceMeshFilter("prefabs/networkedobjects/chest/CategoryChestDamage", "@MoistureUpset_na:assets/na1.mesh");
            fab = Resources.Load<GameObject>("prefabs/networkedobjects/chest/CategoryChestDamage");
            fab.GetComponentInChildren<SkinnedMeshRenderer>().sharedMaterial.SetTexture("_SplatmapTex", Resources.Load<Texture>("@MoistureUpset_moisture_chests:assets/arbitraryfolder/largechestsplat.png"));

            EnemyReplacements.ReplaceModel("prefabs/networkedobjects/chest/CategoryChestHealing", "@MoistureUpset_moisture_chests:assets/arbitraryfolder/categorychest.mesh", "@MoistureUpset_moisture_chests:assets/arbitraryfolder/healingchest.png");
            EnemyReplacements.ReplaceMeshFilter("prefabs/networkedobjects/chest/CategoryChestHealing", "@MoistureUpset_na:assets/na1.mesh");
            fab = Resources.Load<GameObject>("prefabs/networkedobjects/chest/CategoryChestHealing");
            fab.GetComponentInChildren<SkinnedMeshRenderer>().sharedMaterial.SetTexture("_SplatmapTex", Resources.Load<Texture>("@MoistureUpset_moisture_chests:assets/arbitraryfolder/largechestsplat.png"));

            EnemyReplacements.ReplaceModel("prefabs/networkedobjects/chest/CategoryChestUtility", "@MoistureUpset_moisture_chests:assets/arbitraryfolder/categorychest.mesh", "@MoistureUpset_moisture_chests:assets/arbitraryfolder/utilitychest.png");
            EnemyReplacements.ReplaceMeshFilter("prefabs/networkedobjects/chest/CategoryChestUtility", "@MoistureUpset_na:assets/na1.mesh");
            fab = Resources.Load<GameObject>("prefabs/networkedobjects/chest/CategoryChestUtility");
            fab.GetComponentInChildren<SkinnedMeshRenderer>().sharedMaterial.SetTexture("_SplatmapTex", Resources.Load<Texture>("@MoistureUpset_moisture_chests:assets/arbitraryfolder/largechestsplat.png"));

            EnemyReplacements.ReplaceModel("prefabs/networkedobjects/chest/EquipmentBarrel", "@MoistureUpset_moisture_chests:assets/arbitraryfolder/shulker.mesh", "@MoistureUpset_moisture_chests:assets/arbitraryfolder/shulker.png");
            fab = Resources.Load<GameObject>("prefabs/networkedobjects/chest/EquipmentBarrel");
            fab.GetComponentInChildren<SkinnedMeshRenderer>().sharedMaterial.SetInt("_Cull", 0);
            fab.AddComponent<equipmentbarrelfixer>();
            fab.GetComponentInChildren<SfxLocator>().openSound = "EquipmentBarrel";

            EnemyReplacements.ReplaceModel("prefabs/networkedobjects/chest/Chest2", "@MoistureUpset_moisture_chests:assets/arbitraryfolder/largechest.mesh", "@MoistureUpset_moisture_chests:assets/arbitraryfolder/largechest.png");
            EnemyReplacements.ReplaceModel("prefabs/networkedobjects/chest/GoldChest", "@MoistureUpset_moisture_chests:assets/arbitraryfolder/goldchest.mesh", "@MoistureUpset_moisture_chests:assets/arbitraryfolder/goldchest.png");
            fab = Resources.Load<GameObject>("prefabs/networkedobjects/chest/GoldChest");
            fab.GetComponentInChildren<SkinnedMeshRenderer>().material.shader = Resources.Load<GameObject>("prefabs/networkedobjects/chest/Chest2").GetComponentInChildren<SkinnedMeshRenderer>().material.shader;
            fab.GetComponentInChildren<SkinnedMeshRenderer>().material.mainTexture.filterMode = FilterMode.Point;
            fab.GetComponentInChildren<ParticleSystem>().maxParticles = 0;
            fab.GetComponentInChildren<SfxLocator>().openSound = "GoldChest";

            particles = Resources.Load<GameObject>("@MoistureUpset_moisture_chests:assets/arbitraryfolder/particles.prefab");
            particles.transform.SetParent(fab.transform);
            particles.transform.localPosition = Vector3.zero;

            EnemyReplacements.ReplaceModel("prefabs/networkedobjects/chest/Barrel1", "@MoistureUpset_InteractReplacements_SodaBarrel_sodaspritz:assets/sodafountain/cylinder.mesh");
            fab = Resources.Load<GameObject>("prefabs/networkedobjects/chest/Barrel1");
            fab.GetComponentInChildren<SkinnedMeshRenderer>().sharedMaterial.SetTexture("_SplatmapTex", Resources.Load<Texture>("@MoistureUpset_InteractReplacements_SodaBarrel_sodaspritz:assets/sodafountain/sodaSplatmap.png"));
            fab.AddComponent<RandomizeSoda>();
            {
                GameObject spritzPrefab = Resources.Load<GameObject>("@MoistureUpset_InteractReplacements_SodaBarrel_sodaspritz:assets/sodafountain/soda spritz.prefab");
                spritzPrefab.transform.SetParent(fab.transform);
                spritzPrefab.transform.localPosition = new Vector3(0, 1.2f, -0.4f);
            }
            fab.GetComponentInChildren<SfxLocator>().openSound = "Soda";
        }
        private static void Chests()
        {
            if (float.Parse(ModSettingsManager.getOptionValue("Minecraft Chests")) != 1)
                return;

            EnemyReplacements.LoadBNK("Chest");
            EnemyReplacements.LoadResource("moisture_chests");
            var fab = Resources.Load<GameObject>("prefabs/networkedobjects/chest/Chest1");
            EnemyReplacements.ReplaceModel("prefabs/networkedobjects/chest/Chest1", "@MoistureUpset_moisture_chests:assets/arbitraryfolder/smallchest.mesh", "@MoistureUpset_moisture_chests:assets/arbitraryfolder/smallchest.png");
            fab.GetComponentInChildren<SkinnedMeshRenderer>().sharedMaterial.SetTexture("_SplatmapTex", Resources.Load<Texture>("@MoistureUpset_moisture_chests:assets/arbitraryfolder/smallchestsplat.png"));



            EnemyReplacements.ReplaceModel("prefabs/networkedobjects/chest/CategoryChestDamage", "@MoistureUpset_moisture_chests:assets/arbitraryfolder/categorychest.mesh", "@MoistureUpset_moisture_chests:assets/arbitraryfolder/damagechest.png");
            EnemyReplacements.ReplaceMeshFilter("prefabs/networkedobjects/chest/CategoryChestDamage", "@MoistureUpset_na:assets/na1.mesh");
            fab = Resources.Load<GameObject>("prefabs/networkedobjects/chest/CategoryChestDamage");
            fab.GetComponentInChildren<SkinnedMeshRenderer>().sharedMaterial.SetTexture("_SplatmapTex", Resources.Load<Texture>("@MoistureUpset_moisture_chests:assets/arbitraryfolder/largechestsplat.png"));

            EnemyReplacements.ReplaceModel("prefabs/networkedobjects/chest/CategoryChestHealing", "@MoistureUpset_moisture_chests:assets/arbitraryfolder/categorychest.mesh", "@MoistureUpset_moisture_chests:assets/arbitraryfolder/healingchest.png");
            EnemyReplacements.ReplaceMeshFilter("prefabs/networkedobjects/chest/CategoryChestHealing", "@MoistureUpset_na:assets/na1.mesh");
            fab = Resources.Load<GameObject>("prefabs/networkedobjects/chest/CategoryChestHealing");
            fab.GetComponentInChildren<SkinnedMeshRenderer>().sharedMaterial.SetTexture("_SplatmapTex", Resources.Load<Texture>("@MoistureUpset_moisture_chests:assets/arbitraryfolder/largechestsplat.png"));

            EnemyReplacements.ReplaceModel("prefabs/networkedobjects/chest/CategoryChestUtility", "@MoistureUpset_moisture_chests:assets/arbitraryfolder/categorychest.mesh", "@MoistureUpset_moisture_chests:assets/arbitraryfolder/utilitychest.png");
            EnemyReplacements.ReplaceMeshFilter("prefabs/networkedobjects/chest/CategoryChestUtility", "@MoistureUpset_na:assets/na1.mesh");
            fab = Resources.Load<GameObject>("prefabs/networkedobjects/chest/CategoryChestUtility");
            fab.GetComponentInChildren<SkinnedMeshRenderer>().sharedMaterial.SetTexture("_SplatmapTex", Resources.Load<Texture>("@MoistureUpset_moisture_chests:assets/arbitraryfolder/largechestsplat.png"));

            EnemyReplacements.ReplaceModel("prefabs/networkedobjects/chest/EquipmentBarrel", "@MoistureUpset_moisture_chests:assets/arbitraryfolder/shulker.mesh", "@MoistureUpset_moisture_chests:assets/arbitraryfolder/shulker.png");
            fab = Resources.Load<GameObject>("prefabs/networkedobjects/chest/EquipmentBarrel");
            fab.GetComponentInChildren<SkinnedMeshRenderer>().sharedMaterial.SetInt("_Cull", 0);
            fab.AddComponent<equipmentbarrelfixer>();
            fab.GetComponentInChildren<SfxLocator>().openSound = "EquipmentBarrel";

            EnemyReplacements.ReplaceModel("prefabs/networkedobjects/chest/Chest2", "@MoistureUpset_moisture_chests:assets/arbitraryfolder/largechest.mesh", "@MoistureUpset_moisture_chests:assets/arbitraryfolder/largechest.png");
            EnemyReplacements.ReplaceModel("prefabs/networkedobjects/chest/GoldChest", "@MoistureUpset_moisture_chests:assets/arbitraryfolder/goldchest.mesh", "@MoistureUpset_moisture_chests:assets/arbitraryfolder/goldchest.png");
            fab = Resources.Load<GameObject>("prefabs/networkedobjects/chest/GoldChest");
            fab.GetComponentInChildren<SkinnedMeshRenderer>().material.shader = Resources.Load<GameObject>("prefabs/networkedobjects/chest/Chest2").GetComponentInChildren<SkinnedMeshRenderer>().material.shader;
            fab.GetComponentInChildren<SkinnedMeshRenderer>().material.mainTexture.filterMode = FilterMode.Point;
            fab.GetComponentInChildren<ParticleSystem>().maxParticles = 0;
            fab.GetComponentInChildren<SfxLocator>().openSound = "GoldChest";

            particles = Resources.Load<GameObject>("@MoistureUpset_moisture_chests:assets/arbitraryfolder/particles.prefab");
            particles.transform.SetParent(fab.transform);
            particles.transform.localPosition = Vector3.zero;

            Skins.Utils.LoadAsset("InteractReplacements.SodaBarrel.sodaspritz");
            EnemyReplacements.ReplaceModel("prefabs/networkedobjects/chest/Barrel1", "@MoistureUpset_InteractReplacements_SodaBarrel_sodaspritz:assets/sodafountain/cylinder.mesh");
            fab = Resources.Load<GameObject>("prefabs/networkedobjects/chest/Barrel1");
            fab.GetComponentInChildren<SkinnedMeshRenderer>().sharedMaterial.SetTexture("_SplatmapTex", Resources.Load<Texture>("@MoistureUpset_InteractReplacements_SodaBarrel_sodaspritz:assets/sodafountain/sodaSplatmap.png"));
            fab.AddComponent<RandomizeSoda>();
            {
                GameObject spritzPrefab = Resources.Load<GameObject>("@MoistureUpset_InteractReplacements_SodaBarrel_sodaspritz:assets/sodafountain/soda spritz.prefab");
                spritzPrefab.transform.SetParent(fab.transform);
                spritzPrefab.transform.localPosition = new Vector3(0, 1.2f, -0.4f);
            }
            fab.GetComponentInChildren<SfxLocator>().openSound = "Soda";
            On.RoR2.BarrelInteraction.OnInteractionBegin += SpraySoda;
            On.RoR2.BarrelInteraction.OnDeserialize += SpraySoda;
        }

        private static void SpraySoda(On.RoR2.BarrelInteraction.orig_OnDeserialize orig, BarrelInteraction self, NetworkReader reader, bool initialState)
        {
            orig(self, reader, initialState);

            if (self.GetFieldValue<bool>("opened"))
            {
                Color spritz = self.gameObject.GetComponentInChildren<RandomizeSoda>().getSpritzColor();

                var col = self.gameObject.GetComponentInChildren<ParticleSystem>().colorOverLifetime;

                Gradient gradient = col.color.gradient;

                gradient.SetKeys(new GradientColorKey[] { new GradientColorKey(spritz, 0.0f), new GradientColorKey(gradient.colorKeys[1].color, gradient.colorKeys[1].time) }, gradient.alphaKeys);

                col.color = gradient;

                self.gameObject.GetComponentInChildren<ParticleSystem>().Play();
            }
        }

        private static void SpraySoda(On.RoR2.BarrelInteraction.orig_OnInteractionBegin orig, BarrelInteraction self, Interactor activator)
        {
            orig(self, activator);

            Color spritz = self.gameObject.GetComponentInChildren<RandomizeSoda>().getSpritzColor();

            var col = self.gameObject.GetComponentInChildren<ParticleSystem>().colorOverLifetime;

            Gradient gradient = col.color.gradient;

            gradient.SetKeys(new GradientColorKey[] { new GradientColorKey(spritz, 0.0f), new GradientColorKey(gradient.colorKeys[1].color, gradient.colorKeys[1].time) }, gradient.alphaKeys);

            col.color = gradient;

            self.gameObject.GetComponentInChildren<ParticleSystem>().Play();
        }
    }
}
