using RoR2;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace MoistureUpset
{
    public static class ItemDisplayPositionFixer // This is what I think is the best way to do item positions for skins and characters.
    {
        public static ItemDisplayRuleSet TF2_Engi_IDRS;
        public static void Init()
        {

            GenerateIDRSEngi();
        }

        private static void GenerateIDRSEngi()
        {
            var engiBody = Resources.Load<GameObject>("prefabs/characterbodies/EngiBody");

            var cm = engiBody.GetComponentInChildren<CharacterModel>();

            TF2_Engi_IDRS = cm.itemDisplayRuleSet; // We are creating a new ItemDisplayRuleSet for our Engi skin. this lets us provide custom transforms and change the parent of the items when displayed.


            // Lens Maker Glasses
            TF2_Engi_IDRS.FindItemDisplayRuleGroup("CritGlasses").rules[0].localPos = new Vector3(-0.0019f, 0.4301f, 0.2229f);
            TF2_Engi_IDRS.FindItemDisplayRuleGroup("CritGlasses").rules[0].localAngles = new Vector3(-349.51f, 0, 0);
            TF2_Engi_IDRS.FindItemDisplayRuleGroup("CritGlasses").rules[0].localScale = new Vector3(0.2f, 0.2f, 0.2f);

            // Predatory Instinct
            TF2_Engi_IDRS.FindItemDisplayRuleGroup("AttackSpeedOnCrit").rules[0].localPos = new Vector3(0, 0.45f, 0.25f);
            TF2_Engi_IDRS.FindItemDisplayRuleGroup("AttackSpeedOnCrit").rules[0].localScale = new Vector3(0.35f, 0.35f, 0.35f);

            // Steak
            TF2_Engi_IDRS.FindItemDisplayRuleGroup("RegenOnKill").rules[0].localPos = new Vector3(-0.158f, -0.251f, -0.341f);
            TF2_Engi_IDRS.FindItemDisplayRuleGroup("RegenOnKill").rules[0].localAngles = new Vector3(47.971f, -109.439f, -25.023f);
            TF2_Engi_IDRS.FindItemDisplayRuleGroup("RegenOnKill").rules[0].localScale = new Vector3(0.2f, 0.2f, 0.2f);

            // Warbanner
            TF2_Engi_IDRS.FindItemDisplayRuleGroup("WardOnLevel").rules[0].localPos = new Vector3(0.046f, 0.213f, -0.133f);
            TF2_Engi_IDRS.FindItemDisplayRuleGroup("WardOnLevel").rules[0].localAngles = new Vector3(-90f, 0, 90f);
            TF2_Engi_IDRS.FindItemDisplayRuleGroup("WardOnLevel").rules[0].localScale = new Vector3(0.5f, 0.5f, 0.5f);

            // TopazBrooch
            TF2_Engi_IDRS.FindItemDisplayRuleGroup("BarrierOnKill").rules[0].localPos = new Vector3(-0.0998f, 0.0842f, 0.176f);
            TF2_Engi_IDRS.FindItemDisplayRuleGroup("BarrierOnKill").rules[0].localAngles = new Vector3(74.32f, -19.18f, 0);
            TF2_Engi_IDRS.FindItemDisplayRuleGroup("BarrierOnKill").rules[0].localScale = new Vector3(0.7f, 0.7f, 0.7f);

            // ShieldGenerator AKA the worst item.
            TF2_Engi_IDRS.FindItemDisplayRuleGroup("PersonalShield").rules[0].localPos = new Vector3(0, 0.2565f, 0.1201f);
            TF2_Engi_IDRS.FindItemDisplayRuleGroup("PersonalShield").rules[0].localAngles = new Vector3(-66.67f, 0, 0);
            TF2_Engi_IDRS.FindItemDisplayRuleGroup("PersonalShield").rules[0].localScale = new Vector3(0.15f, 0.15f, 0.15f);

            // Hoof Fast Feet
            // Hey you! Yeah you!
            // If you know how limb masks work and know how to implement them for custom skins, hit me up.
            // I want to make the hoof look correct.
            TF2_Engi_IDRS.FindItemDisplayRuleGroup("Hoof").rules[0].localPos = new Vector3(-0.058f, 0.303f, -0.118f);
            TF2_Engi_IDRS.FindItemDisplayRuleGroup("Hoof").rules[0].localAngles = new Vector3(75.08206f, 19.42f, 4.364746e-06f);
            TF2_Engi_IDRS.FindItemDisplayRuleGroup("Hoof").rules[0].localScale = new Vector3(0.1f, 0.1f, 0.1f);

            // Crit Scythe
            TF2_Engi_IDRS.FindItemDisplayRuleGroup("HealOnCrit").rules[0].localPos = new Vector3(0.095f, 0.231f, -0.52f);
            TF2_Engi_IDRS.FindItemDisplayRuleGroup("HealOnCrit").rules[0].localAngles = new Vector3(-53.836f, 76.72601f, -72.995f);
            TF2_Engi_IDRS.FindItemDisplayRuleGroup("HealOnCrit").rules[0].localScale = new Vector3(0.6f, 0.6f, 0.6f);

            // Yooka Laylee
            TF2_Engi_IDRS.FindItemDisplayRuleGroup("ChainLightning").rules[0].localPos = new Vector3(-0.286f, -0.246f, -0.355f);
            TF2_Engi_IDRS.FindItemDisplayRuleGroup("ChainLightning").rules[0].localAngles = new Vector3(-8.4f, -86.51f, -12.2f);
            TF2_Engi_IDRS.FindItemDisplayRuleGroup("ChainLightning").rules[0].localScale = new Vector3(0.7f, 0.7f, 0.7f);

            // Stealth Warkit
            TF2_Engi_IDRS.FindItemDisplayRuleGroup("Phasing").rules[0].localPos = new Vector3(0, -0.3532f, 0.2324f);
            TF2_Engi_IDRS.FindItemDisplayRuleGroup("Phasing").rules[0].localAngles = new Vector3(-79.43f, 0, 0);
            TF2_Engi_IDRS.FindItemDisplayRuleGroup("Phasing").rules[0].localScale = new Vector3(0.25f, 0.25f, 0.25f);

            // Bandolier AKA Disappointment
            TF2_Engi_IDRS.FindItemDisplayRuleGroup("Bandolier").rules[0].localPos = new Vector3(-0.006f, 0.015f, 0.004f);
            TF2_Engi_IDRS.FindItemDisplayRuleGroup("Bandolier").rules[0].localAngles = new Vector3(-35.005f, 260.099f, -256.226f);
            TF2_Engi_IDRS.FindItemDisplayRuleGroup("Bandolier").rules[0].localScale = new Vector3(0.8584043f, 0.8584043f, 0.8584043f);

            // Berserkers Pauldron
            TF2_Engi_IDRS.FindItemDisplayRuleGroup("WarCryOnMultiKill").rules[0].localPos = new Vector3(-0.432f, 0.159f, -0.052f);
            TF2_Engi_IDRS.FindItemDisplayRuleGroup("WarCryOnMultiKill").rules[0].localAngles = new Vector3(-105.448f, 205.158f, -120.289f);
            TF2_Engi_IDRS.FindItemDisplayRuleGroup("WarCryOnMultiKill").rules[0].localScale = new Vector3(0.9f, 0.9f, 0.9f);

            // Desk Plant AKA The best legendary.
            TF2_Engi_IDRS.FindItemDisplayRuleGroup("Plant").rules[0].localPos = new Vector3(0.0771f, 0.2637f, 0.1765f);
            TF2_Engi_IDRS.FindItemDisplayRuleGroup("Plant").rules[0].localAngles = new Vector3(64.464f, 43.791f, 201.998f);
            TF2_Engi_IDRS.FindItemDisplayRuleGroup("Plant").rules[0].localScale = new Vector3(0.06175016f, 0.06175017f, 0.06175017f);
            TF2_Engi_IDRS.FindItemDisplayRuleGroup("Plant").rules[0].childName = "ThighL";

            // Lucky Clover
            TF2_Engi_IDRS.FindItemDisplayRuleGroup("Clover").rules[0].localPos = new Vector3(0, 0.6163f, 0.089f);
            TF2_Engi_IDRS.FindItemDisplayRuleGroup("Clover").rules[0].localAngles = new Vector3(0, 0, 0);
            TF2_Engi_IDRS.FindItemDisplayRuleGroup("Clover").rules[0].localScale = new Vector3(0.8f, 0.8f, 0.8f);

            // Maskathan
            TF2_Engi_IDRS.FindItemDisplayRuleGroup("GhostOnKill").rules[0].localPos = new Vector3(0, 0.4361f, 0.2255f);
            TF2_Engi_IDRS.FindItemDisplayRuleGroup("GhostOnKill").rules[0].localAngles = new Vector3(11.26f, 0, 0);
            TF2_Engi_IDRS.FindItemDisplayRuleGroup("GhostOnKill").rules[0].localScale = new Vector3(0.5f, 0.5f, 0.5f);
            TF2_Engi_IDRS.FindItemDisplayRuleGroup("GhostOnKill").rules[1] = new ItemDisplayRule();

            // Tesla Coil
            TF2_Engi_IDRS.FindItemDisplayRuleGroup("ShockNearby").rules[0].localPos = new Vector3(0, 0.396f, -0.008f);
            TF2_Engi_IDRS.FindItemDisplayRuleGroup("ShockNearby").rules[0].localAngles = new Vector3(-45.24f, 0, 0);
            TF2_Engi_IDRS.FindItemDisplayRuleGroup("ShockNearby").rules[0].localScale = new Vector3(0.5f, 0.5f, 0.5f);

            // Hammer
            TF2_Engi_IDRS.FindItemDisplayRuleGroup("ArmorReductionOnHit").rules[0].localPos = new Vector3(0.267f, -0.1719f, 0.0881f);
            TF2_Engi_IDRS.FindItemDisplayRuleGroup("ArmorReductionOnHit").rules[0].localAngles = new Vector3(-60.839f, 3.967f, -100.761f);
            TF2_Engi_IDRS.FindItemDisplayRuleGroup("ArmorReductionOnHit").rules[0].localScale = new Vector3(0.1f, 0.1f, 0.1f);

            // Brilliant Behemoth
            TF2_Engi_IDRS.FindItemDisplayRuleGroup("Behemoth").rules[0].localPos = new Vector3(0.416f, -0.153f, -0.343f);
            TF2_Engi_IDRS.FindItemDisplayRuleGroup("Behemoth").rules[0].localAngles = new Vector3(4.388f, 86.051f, 196.102f);
            TF2_Engi_IDRS.FindItemDisplayRuleGroup("Behemoth").rules[0].localScale = new Vector3(0.1f, 0.1f, 0.1f);

            // ATG
            TF2_Engi_IDRS.FindItemDisplayRuleGroup("Missile").rules[0].localPos = new Vector3(-0.074f, 0.559f, -0.362f);

            // Brittle Crown
            TF2_Engi_IDRS.FindItemDisplayRuleGroup("GoldOnHit").rules[0].localPos = new Vector3(0, 0.459f, 0.1336f);
            TF2_Engi_IDRS.FindItemDisplayRuleGroup("GoldOnHit").rules[0].localAngles = new Vector3(0, 0, 0);
            TF2_Engi_IDRS.FindItemDisplayRuleGroup("GoldOnHit").rules[0].localScale = new Vector3(1, 1, 1);

            // Shield Bug
            {
                Vector3 bug1, bug2;
                bug1 = TF2_Engi_IDRS.FindItemDisplayRuleGroup("ShieldOnly").rules[0].localPos;
                bug2 = TF2_Engi_IDRS.FindItemDisplayRuleGroup("ShieldOnly").rules[1].localPos;

                TF2_Engi_IDRS.FindItemDisplayRuleGroup("ShieldOnly").rules[0].localPos = new Vector3(bug1.x, 0.6f, bug1.z);
                TF2_Engi_IDRS.FindItemDisplayRuleGroup("ShieldOnly").rules[1].localPos = new Vector3(bug2.x, 0.6f, bug2.z);
            }

            // Shaped Glass
            TF2_Engi_IDRS.FindItemDisplayRuleGroup("LunarDagger").rules[0].localPos = new Vector3(0, -0.05f, -0.543f);
            TF2_Engi_IDRS.FindItemDisplayRuleGroup("LunarDagger").rules[0].localAngles = new Vector3(-97.196f, 0, -1.525f);
            TF2_Engi_IDRS.FindItemDisplayRuleGroup("LunarDagger").rules[0].localScale = new Vector3(1, 1, 1);

            // Tougher Times
            TF2_Engi_IDRS.FindItemDisplayRuleGroup("Bear").rules[0].localPos = new Vector3(-0.004f, 0.381f, -0.253f);

            // Monster Tooth
            // For some god forsaken reason, monster tooth is 6 item display rules instead of just 1 single rule with a single mesh.
            // As long as it looks close enough no one will notice.

            TF2_Engi_IDRS.FindItemDisplayRuleGroup("Tooth").rules[0].localPos = TF2_Engi_IDRS.FindItemDisplayRuleGroup("Tooth").rules[0].localPos - new Vector3(0, 0.15f, 0);

            TF2_Engi_IDRS.FindItemDisplayRuleGroup("Tooth").rules[1].localPos = TF2_Engi_IDRS.FindItemDisplayRuleGroup("Tooth").rules[1].localPos - new Vector3(0, 0.15f, 0);

            TF2_Engi_IDRS.FindItemDisplayRuleGroup("Tooth").rules[2].localPos = TF2_Engi_IDRS.FindItemDisplayRuleGroup("Tooth").rules[2].localPos - new Vector3(0, 0.15f, 0);

            TF2_Engi_IDRS.FindItemDisplayRuleGroup("Tooth").rules[3].localPos = TF2_Engi_IDRS.FindItemDisplayRuleGroup("Tooth").rules[3].localPos - new Vector3(0, 0.15f, 0);

            TF2_Engi_IDRS.FindItemDisplayRuleGroup("Tooth").rules[4].localPos = TF2_Engi_IDRS.FindItemDisplayRuleGroup("Tooth").rules[4].localPos - new Vector3(0, 0.15f, 0);

            TF2_Engi_IDRS.FindItemDisplayRuleGroup("Tooth").rules[5].localPos = TF2_Engi_IDRS.FindItemDisplayRuleGroup("Tooth").rules[5].localPos - new Vector3(0, 0.15f, 0);
        }
    }
}
