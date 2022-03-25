using System;
using System.Collections.Generic;
using System.Text;


namespace MoistureUpset
{
    class ItemReplacements
    {
        public static void RunAll()
        {
            Debug();
            if (!BigJank.getOptionValue(Settings.ReplaceItems))
            {
                return;
            }
            CritGoggles();
        }

        private static void CritGoggles()
        {
            EnemyReplacements.ReplaceModel("RoR2/Base/CritGlasses/DisplayGlasses.prefab", "@MoistureUpset_moisture_dealwithit:assets/blends/dealwithit.mesh", "@MoistureUpset_moisture_dealwithit:assets/blends/dealwithit.png");
            EnemyReplacements.ReplaceMeshFilter("RoR2/Base/CritGlasses/PickupGlasses.prefab", "@MoistureUpset_moisture_dealwithit:assets/blends/dealwithit_pickup.mesh", "@MoistureUpset_moisture_dealwithit:assets/blends/dealwithit.png");
            
        }

        private static void Debug()
        {
            CritGoggles();
        }
    }
}
