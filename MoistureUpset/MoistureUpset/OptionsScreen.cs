using System;
using System.Collections.Generic;
using UnityEngine;
using R2API;
using R2API.Utils;
using RoR2;
using System.Text;
using IL.RoR2;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using RoR2.UI;
using UnityEngine.Events;
using RoR2.UI.SkinControllers;

namespace MoistureUpset
{
    public static class OptionsScreen
    {
        private static bool initilized = false;

        private static GameObject modOptionsPanel;

        private static GameObject[] panelControllers;
        public static void Init()
        {
            On.RoR2.UI.SettingsPanelController.Start += SettingsPanelController_Start;

            On.RoR2.UI.SettingsPanelController.Update += SettingsPanelController_Update;
        }

        private static void SettingsPanelController_Update(On.RoR2.UI.SettingsPanelController.orig_Update orig, SettingsPanelController self)
        {
            orig(self);

            if (GameObject.Find("GenericHeaderButton (Mod Options)") == null)
            {
                initilized = false;
            }

            if (!initilized)
            {
                GameObject gameplayButton = GameObject.Find("GenericHeaderButton (Audio)");

                //foreach (var component in gameplayButton.GetComponentsInChildren<Component>())
                //{
                //    Debug.Log(component);
                //}

                GameObject testButton = UnityEngine.Object.Instantiate<GameObject>(gameplayButton, gameplayButton.transform.parent);

                testButton.name = "GenericHeaderButton (Mod Options)";

                UnityEngine.Object.DestroyImmediate(testButton.GetComponent<ButtonSkinController>());

                testButton.GetComponentInChildren<HGTextMeshProUGUI>().SetText("MOD OPTIONS");

                testButton.GetComponentInChildren<HGButton>().onClick.AddListener(loadPanel);

                initilized = true;
            }
        }

        private static void SettingsPanelController_Start(On.RoR2.UI.SettingsPanelController.orig_Start orig, SettingsPanelController self)
        {
            orig(self);

            GameObject audioPanel = null;

            if (modOptionsPanel == null)
            {
                SettingsPanelController[] objects = Resources.FindObjectsOfTypeAll<SettingsPanelController>();

                panelControllers = new GameObject[6];

                for (int i = 0; i < objects.Length / 2; i++)
                {
                    objects[i].gameObject.GetComponentInChildren<HGButton>().onClick.AddListener(unloadPanel);

                    if (objects[i].name == "SettingsSubPanel, Audio")
                    {
                        audioPanel = objects[i].gameObject;
                    }

                    panelControllers[i] = objects[i].gameObject;
                }

                if (audioPanel != null)
                {
                    modOptionsPanel = UnityEngine.Object.Instantiate<GameObject>(audioPanel, audioPanel.transform.parent);

                    GameObject verticalLayout = modOptionsPanel.transform.Find("Scroll View").Find("Viewport").Find("VerticalLayout").gameObject;


                    UnityEngine.Object.DestroyImmediate(verticalLayout.transform.Find("SettingsEntryButton, Slider (Master Volume)").gameObject);
                    UnityEngine.Object.DestroyImmediate(verticalLayout.transform.Find("SettingsEntryButton, Slider (SFX Volume)").gameObject);
                    UnityEngine.Object.DestroyImmediate(verticalLayout.transform.Find("SettingsEntryButton, Slider (MSX Volume)").gameObject);
                    UnityEngine.Object.DestroyImmediate(verticalLayout.transform.Find("SettingsEntryButton, Bool (Audio Focus)").gameObject);

                    modOptionsPanel.SetActive(false);
                }
            }
        }

        public static void loadPanel()
        {
            foreach (var item in panelControllers)
            {
                if (item.activeSelf)
                {
                    item.SetActive(false);
                }
            }

            modOptionsPanel.SetActive(true);
        }

        public static void unloadPanel()
        {
            Debug.Log("disable?");
            //modOptionsPanel.SetActive(false);
        }
    }
}
