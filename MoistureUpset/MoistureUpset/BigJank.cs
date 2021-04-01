using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using RiskOfOptions;

namespace MoistureUpset
{
    public static class BigJank
    {
        //public static int getOptionValue(string option)
        //{
        //    if (float.Parse(ModSettingsManager.getOptionValue("Only Survivor Skins")) == 1)
        //    {
        //        return 0;
        //    }
        //    return (int)float.Parse(ModSettingsManager.getOptionValue(option), CultureInfo.InvariantCulture);
        //}
        public static bool getOptionValue(string option, string category)
        {
            if (ModSettingsManager.GetOption("Only Survivor Skins", "Misc").GetBool())
            {
                return false;
            }
            return ModSettingsManager.GetOption(option, category).GetBool();
        }
        public static float getFloatValue(string option, string category)
        {
            if (ModSettingsManager.GetOption("Only Survivor Skins", "Misc").GetBool())
            {
                return 0;
            }
            return ModSettingsManager.GetOption(option, category).GetFloat();
        }
    }
}
