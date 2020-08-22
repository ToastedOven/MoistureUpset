using BepInEx;
using RoR2;
using System.Reflection;
using static R2API.SoundAPI;

namespace Nunchuck
{
    [BepInDependency("com.bepis.r2api")]
    //Change these
    [BepInPlugin("com.WetBoys.WetGamers", "We are really wet.", "0.6.9")]
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
            On.RoR2.DamageNumberManager.SpawnDamageNumber += (orig, self, amount, position, crit, teamIndex, DamageColorIndex) =>
            {
                var mainBody = NetworkUser.readOnlyLocalPlayersList[0].master?.GetBody();
                orig(self, amount, position, crit, teamIndex, DamageColorIndex);
                Util.PlaySound("YourMother", mainBody.gameObject);
                //Util.PlaySound("maybejapanese", mainBody.gameObject);
                //AkSoundEngine.PostEvent(EntityStates.Commando.CommandoWeapon.FireBarrage.fireBarrageSoundString, mainBody.gameObject);
                Util.PlaySound(EntityStates.Huntress.HuntressWeapon.ThrowGlaive.attackSoundString, mainBody.gameObject);
                Chat.AddMessage($"played {EntityStates.Huntress.HuntressWeapon.ThrowGlaive.attackSoundString}");
            };
        }
    }
}