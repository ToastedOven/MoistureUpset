using RoR2;

namespace MoistureUpset
{
    public static class SoundAssets
    {
        public static void RegisterSoundEvents()
        {
            On.RoR2.Stats.StatManager.OnDamageDealt += (On.RoR2.Stats.StatManager.orig_OnDamageDealt orig, DamageReport damageReport) =>
            {
                orig(damageReport);

                var mainBody = NetworkUser.readOnlyLocalPlayersList[0].master?.GetBody();

                if (damageReport.victim.body == mainBody)
                {
                    AkSoundEngine.PostEvent("MinecraftHurt", mainBody.gameObject);

                    if (damageReport.isFallDamage)
                    {
                        AkSoundEngine.PostEvent("MinecraftCrunch", mainBody.gameObject);
                    }
                }
            };
        }
    }
}
