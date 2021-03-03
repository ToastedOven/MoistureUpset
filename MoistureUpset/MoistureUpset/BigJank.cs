using System;
using System.Collections.Generic;
using System.Text;
using RiskOfOptions;

namespace MoistureUpset
{
    public static class BigJank
    {
        public static bool getOptionValue(string option)
        {
            if (float.Parse(ModSettingsManager.getOptionValue("Only Survivor Skins")) == 1)
            {
                return false;
            }
            return float.Parse(ModSettingsManager.getOptionValue(option)) == 1;
        }
    }
}
