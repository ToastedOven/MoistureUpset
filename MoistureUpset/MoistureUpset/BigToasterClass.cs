using BepInEx;
using R2API.Utils;
using RoR2;
using R2API;
using R2API.MiscHelpers;
using System.Reflection;
using static R2API.SoundAPI;

namespace MoistureUpset
{
    public static class BigToasterClass
    {
        public static void DeathSound()
        {
            On.EntityStates.GenericCharacterDeath.PlayDeathSound += (orig, self) =>
            {
                Util.PlaySound("EDeath", self.outer.gameObject);
            };
        }
        public static void DropRewards()
        {
            On.RoR2.BossGroup.DropRewards += (orig, self) =>
            {

            };
        }
    }
}
