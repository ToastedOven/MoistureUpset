using BepInEx;
using R2API.Utils;
using RoR2;
using System.Reflection;
using static R2API.SoundAPI;

namespace Nunchuck
{
    [BepInDependency("com.bepis.r2api")]
    //Change these
    [BepInPlugin("com.WetBoys.WetGamers", "We are really wet.", "0.6.9")]
    [R2APISubmoduleDependency("SoundAPI")]
    public class BigTest : BaseUnityPlugin
    {
        public void Awake()
        {
            using (var bankStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("MoistureUpset.ImMoist.bnk"))
            {
                var bytes = new byte[bankStream.Length];
                bankStream.Read(bytes, 0, bytes.Length);             
                SoundBanks.Add(bytes);
            }
            On.EntityStates.GenericCharacterDeath.PlayDeathSound += (orig, self) =>
            {
                Util.PlaySound("EDeath", base.gameObject);
            };
        }
    }
}