using System;
using System.Collections.Generic;
using System.Text;
using RiskOfOptions;

namespace MoistureUpset
{
    public static class Settings
    {
        public static void RunAll()
        {
            Setup();
            HitMarker();
        }
        private static void Setup()
        {
            ModSettingsManager.setPanelDescription("Dude I'm so moist right now.");
            ModSettingsManager.setPanelTitle("Moisture Upset");
        }
        private static void HitMarker()
        {
            ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Slider, "HitMarker Volume", "This sound is also tied to SFX, but has a seperate slider if you want it to be less noisy"));
            ModSettingsManager.addListener(ModSettingsManager.getOption("HitMarker Volume"), new UnityEngine.Events.UnityAction<float>(BigToasterClass.HitMarker));
        }
    }
}
