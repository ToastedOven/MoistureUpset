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
using UnityEngine.Experimental.UIElements;
using RoR2.UI;

namespace MoistureUpset
{
    public static class HudChanges
    {
        public static void RunAll()
        {
            //DEBUG();
        }

        public static void DEBUG()
        {
            Skins.Utils.LoadAsset("Resources.xpbar");
            On.RoR2.UI.HUD.Awake += HUD_Awake;
        }

        private static void HUD_Awake(On.RoR2.UI.HUD.orig_Awake orig, RoR2.UI.HUD self)
        {
            orig(self);

            GameObject EXPBar = self.mainContainer.transform.Find("MainUIArea").Find("BottomLeftCluster").Find("BarRoots").Find("LevelDisplayCluster").Find("ExpBarRoot").Find("ShrunkenRoot").gameObject;

            var XPTest = Resources.Load<GameObject>("@MoistureUpset_Resources_xpbar:assets/minecraft_xp/xpbarholder.prefab");

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

            //EXPBar.GetComponent<UnityEngine.UI.Image>().sprite = Resources.Load<Sprite>("@MoistureUpset_Resources_xpbar:assets/minecraft_xp/xpbarfull.png");
            //EXPBar.GetComponent<UnityEngine.UI.Image>().color = Color.white;
        }
    }
}
