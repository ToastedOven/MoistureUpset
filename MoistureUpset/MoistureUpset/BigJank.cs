using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using BepInEx.Configuration;
using RiskOfOptions;

namespace MoistureUpset
{
    public static class BigJank
    {
        public static bool getOptionValue(ConfigEntry<bool> configEntry)
        {
            if (Settings.OnlySurvivorSkins.Value)
            {
                return false;
            }
            return configEntry.Value;
        }
        public static float getOptionValue(ConfigEntry<float> configEntry)
        {
            if (Settings.OnlySurvivorSkins.Value)
            {
                return 0;
            }
            return configEntry.Value;
        }
    }
} 
