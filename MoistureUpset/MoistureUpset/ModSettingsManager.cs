using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using BepInEx;
using R2API.Utils;
using RoR2.ConVar;
using RoR2.UI;
using RoR2.UI.SkinControllers;
using UnityEngine;

namespace MoistureUpset
{
    public static class ModSettingsManager
    {
        private static List<ModOption> modOptions = new List<ModOption>();

        private static bool initilized = false;
        private static bool isOptionsRegistered = false;

        private static GameObject modOptionsPanel;
        private static GameObject modOptionsButton;
        private static GameObject descriptionPanel;

        private static GameObject[] panelControllers;
        private static GameObject[] headerButtons;

        private static GameObject verticalLayout;

        private static GameObject SliderPrefab;
        private static GameObject BoolPrefab;
        private static GameObject KeyBindingPrefab;

        private static readonly string StartingText = "risk_of_options";

        private static Dictionary<string, string> modLocals = new Dictionary<string, string>();


        public static void Init()
        {
            On.RoR2.UI.SettingsPanelController.Start += SettingsPanelController_Start;

            On.RoR2.UI.SettingsPanelController.Update += SettingsPanelController_Update;

            On.RoR2.Console.Awake += Console_Awake;

            On.RoR2.Language.GetLocalizedStringByToken += Language_GetLocalizedStringByToken;

            InitOptions();
        }

        private static string Language_GetLocalizedStringByToken(On.RoR2.Language.orig_GetLocalizedStringByToken orig, RoR2.Language self, string token)
        {
            string result = orig(self, token);

            if (result == token)
            {
                if (result != "" || result != null)
                {
                    if (modLocals.ContainsKey(token))
                    {
                        result = modLocals[token];
                        Debug.Log($"-------{result}---{token}");
                    }
                }
            }

            return result;
        }

        private static void Console_Awake(On.RoR2.Console.orig_Awake orig, RoR2.Console self)
        {
            orig(self);

            Debug.LogWarning("BRUH ALERT");

            foreach (var item in modOptions)
            {
                RoR2.Console.instance.InvokeMethod("RegisterConVarInternal", new object[] { item.conVar });
                Debug.Log($"[Risk of Options]: {item.conVar.name} ConVar registered.");
            }
        }

        public static void eatdick(float bruh)
        {
            AkSoundEngine.SetRTPCValue("RuneBadNoise", bruh);
        }

        private static void SettingsPanelController_Update(On.RoR2.UI.SettingsPanelController.orig_Update orig, SettingsPanelController self)
        {
            orig(self);

            if (GameObject.Find("GenericHeaderButton (Mod Options)") == null)
            {
                initilized = false;
                isOptionsRegistered = false;
            }

            if (!isOptionsRegistered)
            {
                InstOptions();

                ModOption hitmarker = ModSettingsManager.getOption("Hitmarker Volume");
                hitmarker.gameObject.GetComponentInChildren<SettingsSlider>().slider.onValueChanged.AddListener(new UnityEngine.Events.UnityAction<float>(eatdick));

                isOptionsRegistered = true;
            }

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

            if (GameObject.Find("SettingsSubPanel, Mod Options") != null)
            {
                descriptionPanel.GetComponent<LanguageTextMeshController>().token = "";
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
                //Debug.Log("-------------------------");
                //

                //Debug.Log(descriptionPanel.GetComponent<HGTextMeshProUGUI>().text);

                //Debug.Log("-------------------------");
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

            foreach (var item in modOptions)
            {
                if (item.gameObject != null)
                {

                }
                if (!item.gameObject.activeSelf)
                {
                    item.gameObject.SetActive(true);
                }
            }

            if (modOptionsButton == null)
            {
                modOptionsButton = GameObject.Find("GenericHeaderButton (Mod Options)");
            }

            foreach (var key in modLocals.Keys)
            {
                Debug.Log($"----------{key}: {modLocals[key]}");
            }

            GameObject highlightedButton = GameObject.Find("GenericHeaderHighlight");

            modOptionsButton.GetComponent<HGButton>().interactable = false;

            highlightedButton.transform.position = modOptionsButton.transform.position;

            modOptionsPanel.SetActive(true);

        }

        public static void unloadPanel()
        {

            if (modOptionsPanel.activeSelf)
            {
                modOptionsPanel.SetActive(false);
                modOptionsButton.GetComponent<HGButton>().interactable = true;
            }
        }

        private static void InitPanel()
        {
            GameObject audioPanel = null;
            GameObject keybindingPanel = null;

            if (modOptionsPanel == null)
            {
                SettingsPanelController[] objects = Resources.FindObjectsOfTypeAll<SettingsPanelController>();

                panelControllers = new GameObject[6];
                headerButtons = new GameObject[] { GameObject.Find("GenericHeaderButton (Gameplay)"), GameObject.Find("GenericHeaderButton (KB & M)"), GameObject.Find("GenericHeaderButton (Controller)"),
                                                   GameObject.Find("GenericHeaderButton (Audio)"), GameObject.Find("GenericHeaderButton (Video)"), GameObject.Find("GenericHeaderButton (Graphics)")};

                for (int i = 0; i < objects.Length / 2; i++)
                {
                    if (objects[i].name == "SettingsSubPanel, Audio")
                    {
                        audioPanel = objects[i].gameObject;
                    }
                    else if (objects[i].name == "SettingsSubPanel, Controls (M&KB)")
                    {
                        keybindingPanel = objects[i].gameObject;
                    }

                    panelControllers[i] = objects[i].gameObject;
                }

                if (audioPanel != null)
                {
                    modOptionsPanel = UnityEngine.Object.Instantiate<GameObject>(audioPanel, audioPanel.transform.parent);

                    modOptionsPanel.name = "SettingsSubPanel, Mod Options";

                    verticalLayout = modOptionsPanel.transform.Find("Scroll View").Find("Viewport").Find("VerticalLayout").gameObject;

                    var masterVolume = verticalLayout.transform.Find("SettingsEntryButton, Slider (Master Volume)").gameObject;

                    SliderPrefab = UnityEngine.Object.Instantiate<GameObject>(masterVolume, masterVolume.transform.parent);
                    SliderPrefab.SetActive(false);

                    var audioFocus = verticalLayout.transform.Find("SettingsEntryButton, Bool (Audio Focus)").gameObject;

                    BoolPrefab = UnityEngine.Object.Instantiate<GameObject>(audioFocus, audioFocus.transform.parent);
                    BoolPrefab.SetActive(false);

                    UnityEngine.Object.DestroyImmediate(verticalLayout.transform.Find("SettingsEntryButton, Slider (Master Volume)").gameObject);
                    UnityEngine.Object.DestroyImmediate(verticalLayout.transform.Find("SettingsEntryButton, Slider (SFX Volume)").gameObject);
                    UnityEngine.Object.DestroyImmediate(verticalLayout.transform.Find("SettingsEntryButton, Slider (MSX Volume)").gameObject);
                    UnityEngine.Object.DestroyImmediate(verticalLayout.transform.Find("SettingsEntryButton, Bool (Audio Focus)").gameObject);

                    var keybindButton = keybindingPanel.transform.Find("Scroll View").Find("Viewport").Find("VerticalLayout").Find("SettingsEntryButton, Binding (Jump)").gameObject;

                    KeyBindingPrefab = UnityEngine.Object.Instantiate<GameObject>(keybindButton, keybindButton.transform.parent);
                    keybindingPanel.SetActive(false);

                    modOptionsPanel.SetActive(false);
                }

            }
        }

        public static void InstOptions()
        {
            foreach (var mo in modOptions)
            {
                GameObject newOption = null;

                if (mo.optionType == ModOption.OptionType.Slider)
                {
                    newOption = UnityEngine.Object.Instantiate<GameObject>(SliderPrefab, SliderPrefab.transform.parent);

                    newOption.GetComponentInChildren<SettingsSlider>().settingName = mo.longName;
                    newOption.GetComponentInChildren<SettingsSlider>().nameToken = mo.longName.ToUpper().Replace(" ", "_");
                    newOption.GetComponentInChildren<SettingsSlider>().nameLabel.token = mo.longName.ToUpper().Replace(" ", "_");

                    modLocals[mo.longName.ToUpper().Replace(" ", "_")] = mo.name;
                }
                else if (mo.optionType == ModOption.OptionType.Bool)
                {
                    newOption = UnityEngine.Object.Instantiate<GameObject>(BoolPrefab, BoolPrefab.transform.parent);

                    newOption.GetComponentInChildren<CarouselController>().settingName = mo.longName;
                    newOption.GetComponentInChildren<CarouselController>().nameToken = mo.longName.ToUpper().Replace(" ", "_");
                    newOption.GetComponentInChildren<CarouselController>().nameLabel.token = mo.longName.ToUpper().Replace(" ", "_");

                    modLocals[mo.longName.ToUpper().Replace(" ", "_")] = mo.name;
                }
                else if (mo.optionType == ModOption.OptionType.Keybinding)
                {

                }

                newOption.GetComponentInChildren<HGButton>().hoverToken = $"{mo.name.ToUpper().Replace(" ", "_")}_DESCRIPTION";
                modLocals[$"{mo.longName.ToUpper().Replace(" ", "_")}_DESCRIPTION"] = mo.description;

                //UnityEngine.Object.DestroyImmediate(newOption.GetComponentInChildren<ButtonSkinController>());
                //UnityEngine.Object.DestroyImmediate(newOption.GetComponentInChildren<HGButton>());

                newOption.name = $"ModOptions, Slider ({mo.longName})";

                mo.gameObject = newOption;

                Debug.Log(mo.gameObject.name);
            }
        }

        public static void RegisterOption(ModOption mo)
        {
            if (mo.optionType == ModOption.OptionType.Slider)
            {
                mo.conVar = new FloatConVar(mo.longName, RoR2.ConVarFlags.Archive, null, mo.description);
            }
            else if (mo.optionType == ModOption.OptionType.Bool)
            {
                mo.conVar = new BoolConVar(mo.longName, RoR2.ConVarFlags.Archive, null, mo.description);
            }
            else if (mo.optionType == ModOption.OptionType.Keybinding)
            {
                throw new NotImplementedException("Not yet supported.");
            }
            

            modOptions.Add(mo);
        }

        public static void addOption(ModOption mo)
        {
            mo.longName = $"{StartingText}.{mo.owner.ToLower()}.{mo.name.ToLower().Replace(" ", "_")}";
            RegisterOption(mo);
        }

        public static ModOption getOption(string name)
        {
            var classes = Assembly.GetCallingAssembly().GetExportedTypes();

            string id = "";

            foreach (var item in classes)
            {
                BepInPlugin bepInPlugin = item.GetCustomAttribute<BepInPlugin>();

                if (bepInPlugin != null)
                {
                    id = bepInPlugin.GUID;
                    break;
                }
            }

            foreach (var item in modOptions)
            {
                if (item.longName == $"{StartingText}.{id.ToLower()}.{name.ToLower().Replace(" ", "_")}")
                {
                    return item;
                }
            }

            Debug.LogError($"ModOption {name} not found!");
            return null;
        }

        public static string getOptionValue(string name)
        {
            BaseConVar conVar = null;

            var classes = Assembly.GetCallingAssembly().GetExportedTypes();

            string id = "";

            foreach (var item in classes)
            {
                BepInPlugin bepInPlugin = item.GetCustomAttribute<BepInPlugin>();

                if (bepInPlugin != null)
                {
                    id = bepInPlugin.GUID;
                    break;
                }
            }

            if (id == "")
            {
                Debug.LogError("GUID of calling mod not found!");
                return "";
            }

            conVar = RoR2.Console.instance.FindConVar($"{StartingText}.{id.ToLower()}.{name.ToLower().Replace(" ", "_")}");

            if (conVar == null)
            {
                Debug.LogError($"Convar {name} not found in convars.");
            }

            return conVar.GetString();
        }

        private static void InitOptions()
        {
            addOption(new ModOption(ModOption.OptionType.Slider, "Hitmarker Volume", "Volume of both the hitmarker and the crit sound effects."));

            addOption(new ModOption(ModOption.OptionType.Bool, "Enable Turret Skins", "EXPERIMENTAL: Enable the Engi turret skin replacments."));
        }


        public static bool ContainsModOption(this List<ModOption> modOptions, ModOption modOption)
        {
            foreach (var item in modOptions)
            {
                if (item == modOption)
                {
                    return true;
                }
            }
            return false;
        }

    }
}
