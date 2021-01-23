using BepInEx;
using R2API.Utils;
using RoR2;
using R2API;
using R2API.MiscHelpers;
using System.Reflection;
using static R2API.SoundAPI;
using System;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Text;
using RiskOfOptions;

namespace MoistureUpset
{
    public static class HudChanges
    {
        public static void RunAll()
        {
            DEBUG();
            Currency();
        }

        public static void DEBUG()
        {
        }
        private static void Currency()
        {
            EnemyReplacements.LoadResource("moisture_pungas");
            if (float.Parse(ModSettingsManager.getOptionValue("Currency Changes")) == 1)
            {
                var newfont = Resources.Load<Font>("@MoistureUpset_moisture_pungas:assets/pungas/bombardierwithtix.ttf");

                var tex = Resources.Load<Texture>("@MoistureUpset_moisture_pungas:assets/pungas/testatlas.png");
                var mat = Resources.Load<Material>("tmpfonts/bombardier/tmpbombdropshadow");
                mat.mainTexture = newfont.material.mainTexture;
                mat.mainTexture = tex;

                mat = Resources.Load<Material>("tmpfonts/bombardier/tmpBombDropshadowHologram");
                mat.mainTexture = newfont.material.mainTexture;
                mat.mainTexture = tex;

                var setting = Resources.Load<TMPro.TMP_Settings>("TMP Settings");
                setting.SetFieldValue("m_defaultSpriteAsset", Resources.Load<TMPro.TMP_SpriteAsset>("@MoistureUpset_moisture_pungas:assets/pungas/texInlineSprites.asset"));

                EnemyReplacements.ReplaceMeshFilter("prefabs/pickupmodels/PickupLunarCoin", "@MoistureUpset_moisture_pungas:assets/pungas/coin.mesh", "@MoistureUpset_moisture_pungas:assets/pungas/goldrobux_16.png");

                On.RoR2.Language.SetStringByToken += (orig, self, token, st) =>
                {
                    if (token == "COST_MONEY_FORMAT")
                    {
                        st = "<sprite index=6>{0}";
                    }
                    if (token == "LUNAR_COIN_PICKUP_CONTEXT")
                    {
                        st = "Pick up Robux";
                    }
                    if (token == "PICKUP_LUNAR_COIN")
                    {
                        st = "Robux";
                    }
                    if (token == "NEWT_STATUE_CONTEXT")
                    {
                        st = "Get VIP Pass";
                    }
                    if (st.ToUpper().Contains("GOLD") && token != "PORTAL_GOLDSHORES_NAME" && token != "PORTAL_GOLDSHORES_CONTEXT" && token != "PORTAL_GOLDSHORES_WILL_OPEN" && token != "PORTAL_GOLDSHORES_OPEN")
                    {
                        st = st.Replace("Gold", "Tix");
                        st = st.Replace("gold", "tix");
                    }
                    orig(self, token, st);
                };

                On.RoR2.UI.HUD.Awake += (orig, self) =>
                {
                    orig(self);
                    var image = self.moneyText.gameObject.AddComponent<UnityEngine.UI.Image>();
                    image.sprite = Resources.Load<Sprite>("@MoistureUpset_moisture_pungas:assets/pungas/tixSprite.png");
                    image.preserveAspect = true;
                    foreach (var item in self.moneyText.gameObject.GetComponentsInChildren<RoR2.UI.HGTextMeshProUGUI>())
                    {
                        if (item.name == "DollarSign")
                        {
                            item.text = "";
                        }
                    }
                };

                var fab = Resources.Load<GameObject>("prefabs/effects/CoinEmitter");
                var b = fab.GetComponentInChildren<CoinBehavior>();
                b.coinTiers[0].particleSystem.gameObject.GetComponentInChildren<ParticleSystemRenderer>().mesh = Resources.Load<Mesh>("@MoistureUpset_moisture_pungas:assets/pungas/buildersclub.mesh");
                b.coinTiers[0].particleSystem.gameObject.GetComponentInChildren<ParticleSystemRenderer>().material = Resources.Load<Material>("@MoistureUpset_moisture_pungas:assets/pungas/obc.mat");
                b.coinTiers[1].particleSystem.gameObject.GetComponentInChildren<ParticleSystemRenderer>().mesh = Resources.Load<Mesh>("@MoistureUpset_moisture_pungas:assets/pungas/buildersclub.mesh");
                b.coinTiers[1].particleSystem.gameObject.GetComponentInChildren<ParticleSystemRenderer>().material = Resources.Load<Material>("@MoistureUpset_moisture_pungas:assets/pungas/tbc.mat");
                b.coinTiers[2].particleSystem.gameObject.GetComponentInChildren<ParticleSystemRenderer>().mesh = Resources.Load<Mesh>("@MoistureUpset_moisture_pungas:assets/pungas/buildersclub.mesh");
                b.coinTiers[2].particleSystem.gameObject.GetComponentInChildren<ParticleSystemRenderer>().material = Resources.Load<Material>("@MoistureUpset_moisture_pungas:assets/pungas/bc.mat");
                b.coinTiers[4].particleSystem.gameObject.GetComponentInChildren<ParticleSystemRenderer>().sharedMaterial.mainTexture = Resources.Load<Texture>("@MoistureUpset_moisture_pungas:assets/pungas/tix.png");
                foreach (var item in b.coinTiers[3].particleSystem.gameObject.GetComponentsInChildren<ParticleSystemRenderer>())
                {
                    item.mesh = Resources.Load<Mesh>("@MoistureUpset_na:assets/na1.mesh");
                }
                //0 is the moneybag
            }
        }
    }
}
