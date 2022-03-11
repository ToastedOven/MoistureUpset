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
using UnityEngine.UIElements;
using RoR2.UI;
using UnityEngine.AddressableAssets;

namespace MoistureUpset
{
    public static class HudChanges
    {
        public static void RunAll()
        {
            //DEBUG();
            Currency();
        }

        public static void DEBUG()
        {
            Assets.AddBundle("Resources.xpbar");
            On.RoR2.UI.HUD.Awake += HUD_Awake;
        }

        private static void HUD_Awake(On.RoR2.UI.HUD.orig_Awake orig, RoR2.UI.HUD self)
        {
            orig(self);

            GameObject EXPBar = self.mainContainer.transform.Find("MainUIArea").Find("SpringCanvas").Find("BottomLeftCluster").Find("BarRoots").Find("LevelDisplayCluster").Find("ExpBarRoot").Find("ShrunkenRoot").gameObject;

            var XPTest = Assets.Load<GameObject>("@MoistureUpset_Resources_xpbar:assets/minecraft_xp/xpbarholder.prefab");

            XPTest.transform.SetParent(self.mainContainer.transform);

            XPTest.transform.SetAsFirstSibling();

            DebugClass.Log(XPTest.GetComponent<RectTransform>().localPosition);

            XPTest.GetComponent<RectTransform>().localPosition = Vector3.zero;

            DebugClass.Log(XPTest.GetComponent<RectTransform>().localPosition);

            //var empty = XPTest.transform.Find("empty").gameObject;
            //var fillbar = XPTest.transform.Find("fillbarmask").gameObject;

            //var fillpanel = EXPBar.transform.Find("FillPanel").gameObject;

            //var newbar = GameObject.Instantiate(fillpanel, fillpanel.transform.parent);

            //fillpanel.GetComponent<UnityEngine.UI.Image>().sprite = empty.GetComponentInChildren<UnityEngine.UI.Image>().sprite;

            //newbar.GetComponent<UnityEngine.UI.Image>().color = new Color(0.70f, 0.96f, 0.494f, 0.20f);

            //self.mainContainer.transform.Find("MainUIArea").Find("BottomLeftCluster").Find("BarRoots").Find("LevelDisplayCluster").Find("ExpBarRoot").GetComponent<ExpBar>().fillRectTransform = newbar.GetComponent<RectTransform>();

            //empty.transform.SetParent(EXPBar.transform.Find("FillPanel").parent);

            //empty.transform.localPosition = Vector3.zero;

            //fillbar.transform.SetParent(EXPBar.transform.Find("FillPanel").parent);

            //fillbar.transform.localPosition = Vector3.zero;

            //EXPBar.GetComponent<UnityEngine.UI.Image>().sprite = Assets.Load<Sprite>("@MoistureUpset_Resources_xpbar:assets/minecraft_xp/xpbarfull.png");
            //EXPBar.GetComponent<UnityEngine.UI.Image>().color = Color.white;
        }
        private static void Currency()
        {
            EnemyReplacements.LoadResource("moisture_pungas");
            if (BigJank.getOptionValue(Settings.CurrencyChanges))
            {
                var setting = Addressables.LoadAssetAsync<TMPro.TMP_Settings>("TextMesh Pro/TMP Settings.asset").WaitForCompletion();
                setting.SetFieldValue("m_defaultSpriteAsset", Assets.Load<TMPro.TMP_SpriteAsset>("@MoistureUpset_moisture_pungas:assets/pungas/texInlineSprites.asset"));

                EnemyReplacements.ReplaceMeshFilter("RoR2/Base/LunarCoin/PickupLunarCoin.prefab", "@MoistureUpset_moisture_pungas:assets/pungas/coin.mesh", "@MoistureUpset_moisture_pungas:assets/pungas/goldrobux_16.png");

                On.RoR2.Language.SetStringByToken += (orig, self, token, st) =>
                {
                    if (!token.Contains("LORE") && st.Contains("$"))
                    {
                        st = st.Replace("$", "<sprite index=6>");
                    }
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
                        st = "Purchase Bazaar Pass";
                    }
                    if (st.ToUpper().Contains("GOLD") && token != "PORTAL_GOLDSHORES_NAME" && token != "PORTAL_GOLDSHORES_CONTEXT" && token != "PORTAL_GOLDSHORES_WILL_OPEN" && token != "PORTAL_GOLDSHORES_OPEN")
                    {
                        st = st.Replace("Gold", "Tix");
                        st = st.Replace("gold", "tix");
                    }
                    orig(self, token, st);
                };
                On.RoR2.Chat.PlayerDeathChatMessage.ConstructChatString += (orig, self) =>
                {
                    string text = orig(self);
                    StringBuilder sb = new StringBuilder(text);
                    sb.Remove(0, "<style=cDeath>".Length);
                    sb.Remove(sb.Length - 8, 8);
                    sb.Insert(29, "<style=cDeath>");
                    sb.Insert(sb.Length-29, "</style>");
                    return sb.ToString();
                };
                GameObject g = new GameObject();
                GameObject context = new GameObject();
                On.RoR2.UI.HUD.Awake += (orig, self) =>
                {
                    orig(self);
                    var image = self.moneyText.gameObject.AddComponent<UnityEngine.UI.Image>();
                    image.sprite = Assets.Load<Sprite>("@MoistureUpset_moisture_pungas:assets/pungas/tixSprite.png");
                    image.preserveAspect = true;
                    foreach (var item in self.moneyText.gameObject.GetComponentsInChildren<RoR2.UI.HGTextMeshProUGUI>())
                    {
                        if (item.name == "DollarSign")
                        {
                            item.text = "";
                        }
                    }
                    g = new GameObject();
                    //g.tag = "gamepass";
                    g.transform.parent = self.mainContainer.transform;
                    g.transform.localPosition = new Vector3(0, 1000, 0);
                    image = g.AddComponent<UnityEngine.UI.Image>();
                    image.sprite = Assets.Load<Sprite>("@MoistureUpset_moisture_pungas:assets/pungas/gamepass.png");
                    image.preserveAspect = true;
                    g.transform.localScale = new Vector3(3, 3, 3);

                    context = self.mainContainer.transform.Find("MainUIArea").Find("SpringCanvas").Find("RightCluster").Find("ContextNotification").Find("ContextDisplay").gameObject;
                };

                On.RoR2.PurchaseInteraction.GetContextString += (orig, self, i) =>
                {
                    try
                    {
                        if (Language.GetString(self.contextToken) == "Purchase Bazaar Pass")
                        {
                            g.transform.localPosition = Vector3.Lerp(g.transform.localPosition, Vector3.zero, Time.deltaTime * 6);
                        }
                    }
                    catch (Exception)
                    {
                    }
                    return orig(self, i);
                };

                On.RoR2.UI.HUD.Update += (orig, self) => //optimize? but probably fine
                {
                    orig(self);
                    if (!context.activeSelf)
                    {
                        try
                        {
                            g.transform.localPosition = new Vector3(0, 1000, 0);
                        }
                        catch (Exception)
                        {
                        }
                    }
                };

                var fab = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Common/VFX/CoinEmitter.prefab").WaitForCompletion();
                var b = fab.GetComponentInChildren<CoinBehavior>();
                b.coinTiers[0].particleSystem.gameObject.GetComponentInChildren<ParticleSystemRenderer>().mesh = Assets.Load<Mesh>("@MoistureUpset_moisture_pungas:assets/pungas/buildersclub.mesh");
                b.coinTiers[0].particleSystem.gameObject.GetComponentInChildren<ParticleSystemRenderer>().material = Assets.Load<Material>("@MoistureUpset_moisture_pungas:assets/pungas/obc.mat");
                b.coinTiers[1].particleSystem.gameObject.GetComponentInChildren<ParticleSystemRenderer>().mesh = Assets.Load<Mesh>("@MoistureUpset_moisture_pungas:assets/pungas/buildersclub.mesh");
                b.coinTiers[1].particleSystem.gameObject.GetComponentInChildren<ParticleSystemRenderer>().material = Assets.Load<Material>("@MoistureUpset_moisture_pungas:assets/pungas/tbc.mat");
                b.coinTiers[2].particleSystem.gameObject.GetComponentInChildren<ParticleSystemRenderer>().mesh = Assets.Load<Mesh>("@MoistureUpset_moisture_pungas:assets/pungas/buildersclub.mesh");
                b.coinTiers[2].particleSystem.gameObject.GetComponentInChildren<ParticleSystemRenderer>().material = Assets.Load<Material>("@MoistureUpset_moisture_pungas:assets/pungas/bc.mat");
                b.coinTiers[4].particleSystem.gameObject.GetComponentInChildren<ParticleSystemRenderer>().sharedMaterial.mainTexture = Assets.Load<Texture>("@MoistureUpset_moisture_pungas:assets/pungas/tix.png");
                foreach (var item in b.coinTiers[3].particleSystem.gameObject.GetComponentsInChildren<ParticleSystemRenderer>())
                {
                    item.mesh = Assets.Load<Mesh>("@MoistureUpset_na:assets/na1.mesh");
                }
            }
        }
    }
}
