using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using RiskOfOptions;

namespace MoistureUpset
{
    public static class BigJank
    {
        public static int getOptionValue(string option)
        {
            if (float.Parse(ModSettingsManager.getOptionValue("Only Survivor Skins")) == 1)
            {
                return 0;
            }
            return (int)float.Parse(ModSettingsManager.getOptionValue(option), CultureInfo.InvariantCulture);
        }
    }
}
