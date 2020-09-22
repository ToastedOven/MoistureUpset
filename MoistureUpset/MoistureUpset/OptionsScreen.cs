using RoR2.UI;
using UnityEngine;

namespace MoistureUpset
{
    public static class OptionsScreen
    {
        private static bool initilized = false;
        private static bool ispaneldone = false;

        private static GameObject modOptionsPanel;
        private static GameObject modOptionsButton;
        private static GameObject descriptionPanel;

        private static GameObject[] panelControllers;
        private static GameObject[] headerButtons;

        
        public static void Init()
        {
            //On.RoR2.UI.SettingsPanelController.Start += SettingsPanelController_Start;

            //On.RoR2.UI.SettingsPanelController.Update += SettingsPanelController_Update;
        }

        private static void SettingsPanelController_Update(On.RoR2.UI.SettingsPanelController.orig_Update orig, SettingsPanelController self)
        {
            orig(self);

            if (GameObject.Find("GenericHeaderButton (Mod Options)") == null)
            {
                initilized = false;
            }

            //Debug.Log(GameObject.Find("Not Mod Options"));

            if (GameObject.FindObjectsOfType<SettingsPanelController>().Length > 1)
            {
                unloadPanel();
            }

            if (modOptionsButton != null)
            {
                if (modOptionsButton.GetComponentInChildren<HGTextMeshProUGUI>().color != Color.white)
                {
                    modOptionsButton.GetComponentInChildren<HGTextMeshProUGUI>().color = Color.white;
                }
            }

            if (!initilized)
            {
                GameObject gameplayButton = GameObject.Find("GenericHeaderButton (Audio)");

                GameObject testButton = UnityEngine.Object.Instantiate<GameObject>(gameplayButton, gameplayButton.transform.parent);

                testButton.name = "GenericHeaderButton (Mod Options)";

                testButton.GetComponentInChildren<HGTextMeshProUGUI>().SetText("MOD OPTIONS");

                testButton.GetComponentInChildren<HGButton>().onClick.AddListener(loadPanel);

                descriptionPanel = GameObject.Find("DescriptionText");

                initilized = true;
            }

            if (initilized)
            {
                Debug.Log("-------------------------");

                //foreach (var item in modOptionsPanel.GetComponentsInChildren<TextMeshProUGUI>())
                //{
                //    Debug.Log(item.text);
                //}

                Debug.Log(descriptionPanel.GetComponent<HGTextMeshProUGUI>().text);

                Debug.Log("-------------------------");

                
            }

            if (GameObject.Find("SettingsSubPanel, Mod Options") == null)
            {
                ispaneldone = false;
            }

            if (!ispaneldone && GameObject.Find("SettingsSubPanel, Mod Options") != null)
            {
                GameObject GaySlider = GameObject.Find("SettingsEntryButton, Slider (Gayness)");

                foreach (var item in GaySlider.GetComponentsInChildren<HGTextMeshProUGUI>())
                {
                    if (item.text == "")
                    {
                        item.SetText("Gayness");
                    }
                }

                ispaneldone = true;
            }
        }

        private static void SettingsPanelController_Start(On.RoR2.UI.SettingsPanelController.orig_Start orig, SettingsPanelController self)
        {
            orig(self);

            InitPanel();
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

            foreach (var item in headerButtons)
            {
                item.GetComponent<HGButton>().interactable = true;
            }

            if (modOptionsButton == null)
            {
                modOptionsButton = GameObject.Find("GenericHeaderButton (Mod Options)");
            }
            
            GameObject highlightedButton = GameObject.Find("GenericHeaderHighlight");

            modOptionsButton.GetComponent<HGButton>().interactable = false;

            highlightedButton.transform.position = modOptionsButton.transform.position;

            modOptionsPanel.SetActive(true);


            //

            
        }

        public static void unloadPanel()
        {

            if (modOptionsPanel.activeSelf)
            {
                Debug.Log("disable?");
                modOptionsPanel.SetActive(false);
                modOptionsButton.GetComponent<HGButton>().interactable = true;
            }
        }

        private static void InitPanel()
        {
            GameObject audioPanel = null;

            if (modOptionsPanel == null)
            {
                SettingsPanelController[] objects = Resources.FindObjectsOfTypeAll<SettingsPanelController>();

                panelControllers = new GameObject[6];
                headerButtons = new GameObject[] { GameObject.Find("GenericHeaderButton (Gameplay)"), GameObject.Find("GenericHeaderButton (KB & M)"), GameObject.Find("GenericHeaderButton (Controller)"),
                                                   GameObject.Find("GenericHeaderButton (Audio)"), GameObject.Find("GenericHeaderButton (Video)"), GameObject.Find("GenericHeaderButton (Graphics)")};

                GameObject tempfuckingshit = new GameObject("Not Mod Options");

                for (int i = 0; i < objects.Length / 2; i++)
                {
                    if (objects[i].name == "SettingsSubPanel, Audio")
                    {
                        audioPanel = objects[i].gameObject;
                    }

                    panelControllers[i] = objects[i].gameObject;
                }

                if (audioPanel != null)
                {
                    modOptionsPanel = UnityEngine.Object.Instantiate<GameObject>(audioPanel, audioPanel.transform.parent);

                    modOptionsPanel.name = "SettingsSubPanel, Mod Options";

                    GameObject verticalLayout = modOptionsPanel.transform.Find("Scroll View").Find("Viewport").Find("VerticalLayout").gameObject;

                    var temp = verticalLayout.transform.Find("SettingsEntryButton, Slider (Master Volume)").gameObject;

                    var newTemp = UnityEngine.Object.Instantiate<GameObject>(temp, temp.transform.parent);

                    newTemp.name = "SettingsEntryButton, Slider (Gayness)";

                    UnityEngine.Object.DestroyImmediate(verticalLayout.transform.Find("SettingsEntryButton, Slider (Master Volume)").gameObject);

                    UnityEngine.Object.DestroyImmediate(newTemp.GetComponentInChildren<SettingsSlider>());

                    //Debug.Log(newTemp.GetComponentInChildren<HGButton>().onSelect);

                    //newTemp.GetComponentInChildren<HGButton>().onSelect.RemoveAllListeners();

                    //newTemp.GetComponentInChildren<HGButton>().onSelect.AddListener(loadDescription);

                    UnityEngine.Object.DestroyImmediate(newTemp.GetComponentInChildren<HGButton>());

                    UnityEngine.Object.DestroyImmediate(verticalLayout.transform.Find("SettingsEntryButton, Slider (SFX Volume)").gameObject);
                    UnityEngine.Object.DestroyImmediate(verticalLayout.transform.Find("SettingsEntryButton, Slider (MSX Volume)").gameObject);
                    UnityEngine.Object.DestroyImmediate(verticalLayout.transform.Find("SettingsEntryButton, Bool (Audio Focus)").gameObject);

                    modOptionsPanel.SetActive(false);
                }

            }
        }

        public static void loadDescription()
        {
            descriptionPanel.GetComponent<HGTextMeshProUGUI>().SetText("Eat penis");
        }
    }
}
