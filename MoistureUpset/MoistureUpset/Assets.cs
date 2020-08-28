using BepInEx;
using RoR2;
using R2API;
using R2API.MiscHelpers;
using R2API.Utils;
using System.Reflection;
using static R2API.SoundAPI;
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

            using (var bankStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("MoistureUpset.Risk2GaySounds.bnk"))
            {
                var bytes = new byte[bankStream.Length];
                bankStream.Read(bytes, 0, bytes.Length);

                SoundBanks.Add(bytes);
            }

            using (var bankStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("MoistureUpset.MusicReplacements.bnk"))
            {
                var bytes = new byte[bankStream.Length];
                bankStream.Read(bytes, 0, bytes.Length);

                SoundBanks.Add(bytes);
            }
        }
    }
}
