using BepInEx;
using RoR2;
using R2API;
using R2API.MiscHelpers;
using R2API.Utils;
using System.Reflection;
using static R2API.SoundAPI;
using UnityEngine;
using MoistureUpset.InteractReplacements.SodaBarrel;

namespace MoistureUpset
{
    public static class Assets
    {
        public static void PopulateAssets()
        {
            using (var bankStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("MoistureUpset.ImMoist.bnk"))
            {
                var bytes = new byte[bankStream.Length];
                bankStream.Read(bytes, 0, bytes.Length);

                SoundBanks.Add(bytes);
            }

            using (var bankStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("MoistureUpset.ImReallyMoist.bnk"))
            {
                var bytes = new byte[bankStream.Length];
                bankStream.Read(bytes, 0, bytes.Length);

                SoundBanks.Add(bytes);
            }

            using (var bankStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("MoistureUpset.Risk2GaySounds.bnk"))
            {
                var bytes = new byte[bankStream.Length];
                bankStream.Read(bytes, 0, bytes.Length);

                SoundBanks.Add(bytes);
            }

            testSpritz();
        }

        private static void testSpritz()
        {
            Skins.Utils.LoadAsset("InteractReplacements.SodaBarrel.sodaspritz");

            On.RoR2.BarrelInteraction.OnInteractionBegin += BarrelInteraction_OnInteractionBegin;

            GameObject barrelPrefab = Resources.Load<GameObject>("prefabs/networkedobjects/chest/Barrel1");

            barrelPrefab.GetComponentInChildren<SkinnedMeshRenderer>().sharedMesh = Resources.Load<Mesh>("@MoistureUpset_InteractReplacements_SodaBarrel_sodaspritz:assets/sodafountain/cylinder.mesh");

            barrelPrefab.AddComponent<RandomizeSoda>();

            GameObject spritzPrefab = Resources.Load<GameObject>("@MoistureUpset_InteractReplacements_SodaBarrel_sodaspritz:assets/sodafountain/soda spritz.prefab");

            spritzPrefab.transform.SetParent(barrelPrefab.transform);

            spritzPrefab.transform.localPosition = new Vector3(0, 1.2f, -0.4f);
        }

        private static void BarrelInteraction_OnInteractionBegin(On.RoR2.BarrelInteraction.orig_OnInteractionBegin orig, BarrelInteraction self, Interactor activator)
        {
            orig(self, activator);

            Color spritz = self.gameObject.GetComponentInChildren<RandomizeSoda>().getSpritzColor();

            var col = self.gameObject.GetComponentInChildren<ParticleSystem>().colorOverLifetime;

            Gradient gradient = col.color.gradient;

            gradient.SetKeys(new GradientColorKey[] { new GradientColorKey(spritz, 0.0f ), new GradientColorKey(gradient.colorKeys[1].color, gradient.colorKeys[1].time)}, gradient.alphaKeys);

            col.color = gradient;

            self.gameObject.GetComponentInChildren<ParticleSystem>().Play();
        }
    }
}
