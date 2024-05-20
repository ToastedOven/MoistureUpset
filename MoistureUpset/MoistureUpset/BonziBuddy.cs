using HG;
using MoistureUpset.NetMessages;
using R2API;
using R2API.Networking.Interfaces;
using R2API.Utils;
using RoR2;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using RoR2.Achievements;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.AddressableAssets;

namespace MoistureUpset
{
    //public class BonziUnlocked : ModdedUnlockable
    //{
    //    public override string AchievementIdentifier { get; } = "MOISTURE_BONZIBUDDY_ACHIEVEMENT_ID";
    //    public override string UnlockableIdentifier { get; } = "MOISTURE_BONZIBUDDY_REWARD_ID";
    //    public override string PrerequisiteUnlockableIdentifier { get; } = "MOISTURE_BONZIBUDDY_PREREQ_ID";
    //    public override string AchievementNameToken { get; } = "MOISTURE_BONZIBUDDY_ACHIEVEMENT_NAME";
    //    public override string AchievementDescToken { get; } = "MOISTURE_BONZIBUDDY_ACHIEVEMENT_DESC";
    //    public override string UnlockableNameToken { get; } = "MOISTURE_BONZIBUDDY_UNLOCKABLE_NAME";

    //    public override Sprite Sprite { get; } = Assets.Load<Sprite>("@MoistureUpset_moisture_bonzistatic:assets/bonzibuddy/BonziIcon.png");
    //    public void ClearCheck(On.RoR2.EscapeSequenceController.EscapeSequenceMainState.orig_Update orig, RoR2.EscapeSequenceController.EscapeSequenceMainState self)
    //    {
    //        orig(self);
    //        if (BonziBuddy.buddy.foundMe)
    //        {
    //            base.Grant();
    //        }
    //    }
    //    public override void OnInstall()
    //    {
    //        base.OnInstall();
    //        On.RoR2.EscapeSequenceController.EscapeSequenceMainState.Update += ClearCheck;
    //    }

    //    public override void OnUninstall()
    //    {
    //        base.OnUninstall();
    //        On.RoR2.EscapeSequenceController.EscapeSequenceMainState.Update -= ClearCheck;
    //    }

    //    public override Func<string> GetHowToUnlock { get; } = new Func<string>(() => "lig");
    //    public override Func<string> GetUnlocked { get; } = new Func<string>(() => "ball");
    //}
    public class BonziBuddy : MonoBehaviour
    {
        #region defined positions
        public static Vector2 M1 = new Vector2(0.7779733f, 0.2307445f);
        public static Vector2 MAINMENU = new Vector2(0.6449644f, 0.8017029f);
        public static Vector2 SETTINGS = new Vector2(0.04327124f, 0.216697f);
        public static Vector2 LOGBOOK = new Vector2(0.9575167f, 0.2372429f);
        public static Vector2 MUSICANDMORE = new Vector2(0.2407375f, 0.7695388f);
        public static Vector2 ALTGAMEMODES = new Vector2(0.6959499f, 0.4022391f);
        public static Vector2 ECLIPSE = new Vector2(0.572173f, 0.7421584f);
        public static Vector2 MULTIPLAYERSETUP = new Vector2(0.7007814f, 0.7697079f);
        public static Vector2 CHARSELECT = new Vector2(0.862179f, 0.2109936f);
        public static Vector2 DEATH = new Vector2(0.5394522f, 0.1317119f);
        #endregion
        public static BonziBuddy buddy;

        Animator a;
        GameObject textBox;
        TextMeshPro text;
        public bool foundMe = false;
        bool firstTime = false;
        float prevY = 0, prevX = 0;
        bool moveUp = false, moveDown = false, moveLeft = false, moveRight = false;
        bool debugging = false;
        string currentClip = "";
        public bool atDest = true;
        public Vector2 dest;
        public Vector2 screenPos;
        int idlenum = -1;
        string username = "";
        List<string> lastQuotes = new List<string>();
        List<string> lastQuotesAllyDeath = new List<string>();
        List<int> lastIdle = new List<int>();
        bool idling = false;
        int failCount = 0;
        public bool activeMountainShrine = false;
        int mountainShrineItems = 0, mountainShrineCount = 0;
        float bloodShrineTimer = 0;
        Transform charPosition = null;
        GameObject obj1, obj2, obj3, obj4, obj5, obj6;
        GameObject preloaded = Assets.Load<GameObject>("@MoistureUpset_moisture_bonzistatic:assets/bonzibuddy/bonzistatic.prefab");
        public float dontSpeak = 0;
        public static bool doHooks = true;
        int dioUsed = 0, dioHeld = 0;
        float casinoTimer = 0;
        bool cheated = false;
        bool daniel = false, sapi4 = false;
        bool dank = false;

        bool bonziActive = false;
        void Start()
        {
            username = Facepunch.Steamworks.Client.Instance.Username;
            a = GetComponentInChildren<Animator>();
            prevX = transform.position.x;
            prevY = transform.position.y;
            text = GetComponentInChildren<TextMeshPro>();
            textBox = text.gameObject.transform.parent.gameObject;
            text.gameObject.layer = 5;
            textBox.layer = 5;
            text.gameObject.transform.localPosition = new Vector3(0.06f, 0, -.1f);
            dest = new Vector2(.5f, .5f);
            textBox.SetActive(false);
            preloaded.GetComponent<ParticleSystemRenderer>().material.mainTexture = Assets.Load<Texture>("@MoistureUpset_moisture_bonzistatic:assets/bonzibuddy/frames.png");
            Hooks();
            SetupBalcon();
            string s = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Windows);
            if (File.Exists(s + "\\Downloaded Installations\\{952F792A-172C-4F2F-88F7-C002F916C583}\\NextUp-ScanSoft Daniel British Voice.msi"))
            {
                daniel = true;
            }
            if (File.Exists(s + "\\Speech\\speech.dll") && File.Exists(s + "\\lhsp\\help\\tv_enua.hlp"))
            {
                sapi4 = true;
            }
        }

        internal static void SetupGameObjects()
        {
            GameObject bonzi = Instantiate(Assets.Load<GameObject>("@MoistureUpset_moisture_bonzibuddy:assets/bonzibuddy/bonzibuddy.prefab"));
            DontDestroyOnLoad(bonzi);
            bonzi.GetComponent<RectTransform>().SetParent(RoR2Application.instance.mainCanvas.transform, false);
            bonzi.SetActive(true);
            bonzi.GetComponent<RectTransform>().anchorMin = Vector2.zero;
            bonzi.GetComponent<RectTransform>().anchorMax = Vector2.zero;
            bonzi.layer = 5;
            BonziBuddy.buddy = bonzi.AddComponent<BonziBuddy>();

            GameObject Gamer = Instantiate(new GameObject());
            DontDestroyOnLoad(Gamer);
            Gamer.SetActive(true);
            MLG.MemeMachine = Gamer.AddComponent<MLG>();
            MLG.MemeMachine.tracks.Add(new MLG.AudioTrack
            {
                Stage1 = "HopeWillDieStage1",
                Stage2 = "HopeWillDieStage2",
                Interval = "HopeWillDieInterval",
                Stage1StartDuration = 47.18f,
            });
            MLG.MemeMachine.tracks.Add(new MLG.AudioTrack
            {
                Stage1 = "BangStage1",
                Stage2 = "BangStage2",
                Interval = "BangInterval",
                Stage1StartDuration = 17.584f,
            });
            MLG.MemeMachine.tracks.Add(new MLG.AudioTrack
            {
                Stage1 = "BignisStage1",
                Stage2 = "BignisStage2",
                Interval = "BignisInterval",
                Stage1StartDuration = 33.133f,
            });
            MLG.MemeMachine.tracks.Add(new MLG.AudioTrack
            {
                Stage1 = "TooLoudStage1",
                Stage2 = "TooLoudStage2",
                Interval = "TooLoudInterval",
                Stage1StartDuration = 55.622f,
            });
            MLG.MemeMachine.tracks.Add(new MLG.AudioTrack
            {
                Stage1 = "FireStage1",
                Stage2 = "FireStage2",
                Interval = "FireInterval",
                Stage1StartDuration = 18.345f,
            });
            MLG.MemeMachine.tracks.Add(new MLG.AudioTrack
            {
                Stage1 = "KyotoStage1",
                Stage2 = "KyotoStage2",
                Interval = "KyotoInterval",
                Stage1StartDuration = 46.626f,
            });
            MLG.MemeMachine.ActiveTrack = UnityEngine.Random.Range(0, MLG.MemeMachine.tracks.Count);


            GameObject ScreenStuff = Instantiate(Assets.Load<GameObject>("@MoistureUpset_2014:assets/2014/Sniper/MLGScreenStuff.prefab"));
            DontDestroyOnLoad(ScreenStuff);
            ScreenStuff.GetComponent<RectTransform>().SetParent(RoR2Application.instance.mainCanvas.transform, false);
            ScreenStuff.SetActive(true);
            ScreenStuff.GetComponent<RectTransform>().anchorMin = Vector2.zero;
            ScreenStuff.GetComponent<RectTransform>().anchorMax = Vector2.zero;
            ScreenStuff.layer = 5;
            //ScreenStuff.GetComponent<RectTransform>().localScale = new Vector3(4, 4, 4);
            //ScreenStuff.GetComponent<RectTransform>().localPosition = new Vector3(0, -300, 0);
            MLG.MemeMachine.UIAnimator = ScreenStuff;
        }
        struct ItemsValue
        {
            public float value;
            public int bestItem/* = -1*/;
            public float bestValue;
            public float averageValue;
            public float temp/* = 69420*/;
            public bool daisy/* = false*/;
            public int squidCount/* = false*/;
        }
        private ItemsValue CalcItemValues(string[] nameTokens)
        {
            ItemsValue v = new ItemsValue();
            v.value = 0;
            v.bestItem = -1;
            v.bestValue = 0;
            v.averageValue = 0;
            v.temp = 69420;
            v.daisy = false;
            v.squidCount = 0;
            CharacterBody body = RoR2.NetworkUser.readOnlyLocalPlayersList[0].master?.GetBody();
            Inventory inventory = body.inventory;
            int luck = inventory.GetItemCount(RoR2Content.Items.Clover) - inventory.GetItemCount(RoR2Content.Items.LunarBadLuck);

            for (int i = 0; i < nameTokens.Length; i++)
            {
                switch (nameTokens[i])
                {
                    case "ITEM_CLOVER_NAME":
                        if (luck == -1)
                        {
                            v.temp = .75f;
                        }
                        else if (luck > -1)
                        {
                            v.temp = 1;

                        }
                        else
                        {
                            v.temp = .4f;
                        }
                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//57 Leaf Clover
                    case "ITEM_SYRINGE_NAME":
                        v.temp = .75f;
                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Soldier's Syringe
                    case "ITEM_BEAR_NAME":
                        v.temp = .5f;
                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Tougher Times
                    case "ITEM_BEHEMOTH_NAME":
                        v.temp = .95f;
                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Brilliant Behemoth
                    case "ITEM_MISSILE_NAME":
                        if (luck < 0 && inventory.GetItemCount(RoR2.DLC1Content.Items.MissileVoid) == 0)
                        {
                            v.temp = .2f;
                        }
                        else if (inventory.GetItemCount(RoR2.DLC1Content.Items.MissileVoid) != 0)
                        {
                            v.temp = .7f;
                        }
                        else if (luck > 0 && inventory.GetItemCount(RoR2.DLC1Content.Items.MissileVoid) == 0)
                        {
                            v.temp = .95f;
                        }
                        else
                        {
                            v.temp = .85f;
                        }
                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//AtG Missile Mk. 1
                    case "ITEM_EXPLODEONDEATH_NAME":
                        if (inventory.GetItemCount(RoR2.DLC1Content.Items.ExplodeOnDeathVoid) != 0)
                        {
                            v.temp = .8f;
                        }
                        else
                        {
                            v.temp = .65f;
                        }
                        if (inventory.GetItemCount(RoR2Content.Items.ExplodeOnDeath) > 5)
                        {
                            v.temp += .175f;
                        }
                        if (inventory.GetItemCount(RoR2.DLC1Content.Items.ExplodeOnDeathVoid) > 5)
                        {
                            v.temp += .1f;
                        }
                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Will-o'-the-wisp
                    case "ITEM_DAGGER_NAME":
                        v.temp = .95f;
                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Ceremonial Dagger
                    case "ITEM_TOOTH_NAME":
                        v.temp = .2f;
                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Monster Tooth
                    case "ITEM_CRITGLASSES_NAME":
                        if (body.crit >= 100)
                        {
                            v.temp = .1f;
                        }
                        else
                        {
                            v.temp = .75f;
                        }
                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Lens-Maker's Glasses
                    case "ITEM_HOOF_NAME":
                        if (body.moveSpeed > body.baseMoveSpeed * 3)
                        {
                            v.temp = .2f;
                        }
                        else
                        {
                            v.temp = .5f;
                        }
                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Paul's Goat Hoof
                    case "ITEM_FEATHER_NAME":
                        if (body.maxJumpCount > 5 || body.baseNameToken == "MERC_BODY_NAME" || body.baseNameToken == "CROCO_BODY_NAME")
                        {
                            v.temp = .22f;
                        }
                        else
                        {
                            v.temp = .55f;
                        }
                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Hopoo Feather
                    case "ITEM_CHAINLIGHTNING_NAME":
                        if (luck == -1)
                        {
                            v.temp = .5f;
                        }
                        else if (luck < -1)
                        {
                            v.temp = .1f;
                        }
                        else
                        {
                            v.temp = .85f;
                        }

                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Ukulele
                    case "ITEM_SEED_NAME":
                        if (body.crit > 70 && body.critHeal > 0)
                        {
                            v.temp = .1f;
                        }
                        else
                        {
                            v.temp = .2f;
                        }
                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Leeching Seed
                    case "ITEM_ICICLE_NAME":
                        if (inventory.GetEquipment(0).equipmentDef != RoR2Content.Equipment.DeathProjectile)
                        {
                            v.temp = .6f;
                        }
                        else
                        {
                            v.temp = .8f;
                        }
                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Frost Relic
                    case "ITEM_GHOSTONKILL_NAME":
                        if (luck < 0)
                        {
                            v.temp = 0f;
                        }
                        else
                        {
                            v.temp = .3f;
                        }
                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Happiest Mask
                    case "ITEM_MUSHROOM_NAME":
                        if (inventory.GetItemCount(RoR2.DLC1Content.Items.MushroomVoid) != 0)
                        {
                            v.temp = .65f;
                        }
                        else
                        {
                            v.temp = .3f;
                        }
                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Bustling Fungus
                    case "ITEM_CROWBAR_NAME":
                        v.temp = .7f;
                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Crowbar
                    case "ITEM_ATTACKSPEEDONCRIT_NAME":
                        if (luck < 0 && body.crit < 50)
                        {
                            v.temp = .35f;
                        }
                        else
                        {
                            v.temp = .8f;
                        }
                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Predatory Instincts
                    case "ITEM_BLEEDONHIT_NAME":
                        if (body.bleedChance > 99 || body.baseNameToken == "LOADER_BODY_NAME")
                        {
                            v.temp = .15f;
                        }
                        else
                        {
                            v.temp = .75f;
                        }
                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Tri-Tip Dagger
                    case "ITEM_SPRINTOUTOFCOMBAT_NAME":
                        if (body.moveSpeed > body.moveSpeed * 3)
                        {
                            v.temp = .2f;
                        }
                        else
                        {
                            v.temp = .5f;
                        }
                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Red Whip
                    case "ITEM_FALLBOOTS_NAME":
                        if (inventory.GetItemCount(RoR2Content.Items.FallBoots) != 0 || body.baseNameToken == "LOADER_BODY_NAME")
                        {
                            v.temp = .05f;
                        }
                        else
                        {
                            v.temp = .2f;
                        }
                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//H3AD-5T v2
                    case "ITEM_COOLDOWNONCRIT_NAME":
                        break;//Wicked Ring
                    case "ITEM_WARDONLEVEL_NAME":
                        if (inventory.GetItemCount(RoR2Content.Items.WardOnLevel) != 0)
                        {
                            v.temp = .1f;
                        }
                        else
                        {

                            v.temp = .2f;
                        }
                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Warbanner
                    case "ITEM_WARCRYONMULTIKILL_NAME":
                        v.temp = .1f;
                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Berzerker's Pauldron
                    case "ITEM_PHASING_NAME":
                        v.temp = 0f;
                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Old War Stealthkit
                    case "ITEM_HEALONCRIT_NAME":
                        if (inventory.GetItemCount(RoR2Content.Items.ShieldOnly) != 0)
                        {
                            v.temp = .15f;
                        }
                        else
                        {
                            v.temp = .65f;
                        }
                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Harvester's Scythe
                    case "ITEM_HEALWHILESAFE_NAME":
                        if (RoR2.Run.instance.stageClearCount > 4)
                        {
                            v.temp = .05f;
                        }
                        else
                        {
                            v.temp = .3f;
                        }
                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Cautious Slug
                    case "ITEM_JUMPBOOST_NAME":
                        v.temp = .4f;
                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Wax Quail
                    case "ITEM_PERSONALSHIELD_NAME":
                        if (inventory.GetItemCount(RoR2Content.Items.ShieldOnly) != 0)
                        {
                            v.temp = .5f;
                        }
                        else
                        {
                            v.temp = .15f;
                        }

                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Personal Shield Generator
                    case "ITEM_NOVAONHEAL_NAME":
                        if (inventory.GetItemCount(RoR2Content.Items.ShieldOnly) != 0)
                        {
                            v.temp = .1f;
                        }
                        else
                        {
                            v.temp = .65f;
                        }
                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//N'kuhana's Opinion
                    case "ITEM_MEDKIT_NAME":
                        if (inventory.GetItemCount(RoR2Content.Items.ShieldOnly) != 0)
                        {
                            v.temp = .05f;
                        }
                        else
                        {
                            v.temp = .35f;
                        }
                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Medkit
                    case "ITEM_EQUIPMENTMAGAZINE_NAME":
                        v.temp = .7f;
                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Fuel Cell
                    case "ITEM_INFUSION_NAME":
                        v.temp = .4f;
                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Infusion
                    case "ITEM_SHOCKNEARBY_NAME":
                        v.temp = .9f;
                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Unstable Tesla Coil
                    case "ITEM_IGNITEONKILL_NAME":
                        if (inventory.GetItemCount(RoR2.DLC1Content.Items.StrengthenBurn) != 0)
                        {
                            v.temp = .7f;
                        }
                        else
                        {
                            v.temp = .55f;
                        }

                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Gasoline
                    case "ITEM_BOUNCENEARBY_NAME":
                        if (luck == -1)
                        {
                            v.temp = .5f;
                        }
                        else if (luck < -1)
                        {
                            v.temp = .15f;
                        }
                        else
                        {
                            v.temp = 1f;
                        }
                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Sentient Meat Hook
                    case "ITEM_FIREWORK_NAME":
                        v.temp = .1f;
                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Bundle of Fireworks
                    case "ITEM_BANDOLIER_NAME":
                        if (luck < 0)
                        {
                            v.temp = 0f;
                        }
                        else
                        {
                            v.temp = .1f;
                        }

                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Bandolier
                    case "ITEM_STUNCHANCEONHIT_NAME":
                        if (luck < 0)
                        {
                            v.temp = .05f;
                        }
                        else
                        {
                            v.temp = .25f;
                        }
                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Stun Grenade
                    case "ITEM_LUNARDAGGER_NAME":
                        v.temp = .05f;
                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Shaped Glass
                    case "ITEM_GOLDONHIT_NAME":
                        if (luck < 0)
                        {
                            v.temp = .1f;
                        }
                        else
                        {
                            v.temp = .6f;
                        }
                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Brittle Crown
                              //😂😂😂😂 WHO DID THIS???? 😂😂😂😂
                              //https://cdn.discordapp.com/attachments/404338516336574477/956328549663518810/revengeance_status_12.mp4
                    case "ITEM_SHIELDONLY_NAME":
                        if (inventory.GetItemCount(RoR2Content.Items.ShieldOnly) != 0)
                        {
                            v.temp = .75f;
                        }
                        else
                        {
                            v.temp = .4f;
                        }

                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Transcendence
                    case "ITEM_ALIENHEAD_NAME":
                        v.temp = .85f;
                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Alien Head
                    case "ITEM_TALISMAN_NAME":
                        if (inventory.GetEquipment(0).equipmentDef != RoR2Content.Equipment.DeathProjectile)
                        {
                            v.temp = 1f;
                        }
                        else
                        {
                            v.temp = .75f;
                        }
                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Soulbound Catalyst
                    case "ITEM_KNURL_NAME":
                        v.temp = .5f;
                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Titanic Knurl
                    case "ITEM_BEETLEGLAND_NAME":
                        v.temp = .3f;
                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Queen's Gland
                    case "ITEM_SPRINTBONUS_NAME":
                        v.temp = .45f;
                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Energy Drink
                    case "ITEM_SECONDARYSKILLMAGAZINE_NAME":
                        if (body.baseNameToken == "CAPTAIN_BODY_NAME")
                        {
                            v.temp = .05f;
                        }
                        else
                        {
                            v.temp = .4f;
                        }
                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Backup Magazine
                    case "ITEM_STICKYBOMB_NAME":
                        if (inventory.GetItemCount(RoR2Content.Items.StickyBomb) > 20)
                        {
                            v.temp = .15f;
                        }
                        else
                        {
                            v.temp = .75f;
                        }

                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Sticky Bomb
                    case "ITEM_TREASURECACHE_NAME":
                        if (inventory.GetItemCount(RoR2.DLC1Content.Items.TreasureCacheVoid) != 0)
                        {
                            v.temp = .6f;
                        }
                        else
                        {
                            v.temp = .3f;
                        }

                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Rusted Key
                    case "ITEM_BOSSDAMAGEBONUS_NAME":
                        v.temp = .6f;
                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Armor-Piercing Rounds
                    case "ITEM_SPRINTARMOR_NAME":
                        v.temp = .05f;
                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Rose Buckler
                    case "ITEM_ELEMENTALRINGS_NAME":
                        break;//Kjaro and Runald's Bands
                    case "ITEM_ICERING_NAME":
                        if (inventory.GetItemCount(RoR2.DLC1Content.Items.ElementalRingVoid) != 0)
                        {
                            v.temp = .6f;
                        }
                        else
                        {
                            v.temp = .8f;
                        }
                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Runald's Band
                    case "ITEM_FIRERING_NAME":
                        if (inventory.GetItemCount(RoR2.DLC1Content.Items.ElementalRingVoid) != 0)
                        {
                            v.temp = .6f;
                        }
                        else
                        {
                            v.temp = .7f;
                        }
                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Kjaro's Band
                    case "ITEM_SLOWONHIT_NAME":
                        if (inventory.GetItemCount(RoR2.DLC1Content.Items.SlowOnHitVoid) != 0)
                        {
                            v.temp = .55f;
                        }
                        else
                        {
                            v.temp = .35f;
                        }
                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Chronobauble
                    case "ITEM_EXTRALIFE_NAME":
                        if (body.baseNameToken == "ENGI_BODY_NAME")
                        {
                            v.temp = 1f;
                        }
                        else if (inventory.GetItemCount(RoR2.DLC1Content.Items.ExtraLifeVoid) != 0)
                        {
                            v.temp = .9f;
                        }
                        else
                        {
                            v.temp = .6f;
                        }

                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Dio's Best Friend
                    case "ITEM_EXTRALIFECONSUMED_NAME":
                        break;//Dio's Best Friend (Consumed)
                    case "ITEM_UTILITYSKILLMAGAZINE_NAME":
                        v.temp = .8f;
                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Hardlight Afterburner
                    case "ITEM_HEADHUNTER_NAME":
                        if (inventory.GetItemCount(RoR2Content.Items.ShieldOnly) != 0)
                        {
                            v.temp = .8f;
                        }
                        else
                        {
                            v.temp = .25f;
                        }
                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Wake of Vultures
                    case "ITEM_KILLELITEFRENZY_NAME":
                        v.temp = .7f;
                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Brainstalks
                    case "ITEM_INCREASEHEALING_NAME":
                        v.temp = .7f;
                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Rejuvenation Rack
                    case "ITEM_REPEATHEAL_NAME":
                        v.temp = .0f;
                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Corpsebloom
                    case "ITEM_AUTOCASTEQUIPMENT_NAME":
                        if (inventory.GetItemCount(RoR2Content.Items.AutoCastEquipment) != 0)
                        {
                            v.temp = .7f;
                        }
                        else
                        {
                            v.temp = .85f;
                        }

                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Gesture of the Drowned
                    case "ITEM_EXECUTELOWHEALTHELITE_NAME":
                        v.temp = .65f;
                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Old Guillotine
                    case "ITEM_ENERGIZEDONEQUIPMENTUSE_NAME":
                        if (inventory.GetItemCount(RoR2Content.Items.EnergizedOnEquipmentUse) != 0)
                        {
                            v.temp = .25f;
                        }
                        else
                        {
                            v.temp = .45f;
                        }

                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//War Horn
                    case "ITEM_BARRIERONOVERHEAL_NAME":
                        if (inventory.GetItemCount(RoR2Content.Items.BarrierOnOverHeal) > 1)
                        {
                            v.temp = .4f;
                        }
                        else
                        {
                            v.temp = .8f;
                        }

                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Aegis
                    case "ITEM_TONICAFFLICTION_NAME":
                        break;//Tonic Affliction
                    case "ITEM_TITANGOLDDURINGTP_NAME":
                        v.temp = .5f;
                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Halcyon Seed
                    case "ITEM_SPRINTWISP_NAME":
                        v.temp = .7f;
                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Little Disciple
                    case "ITEM_BARRIERONKILL_NAME":
                        v.temp = .3f;
                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Topaz Brooch
                    case "ITEM_ARMORREDUCTIONONHIT_NAME":
                        if (inventory.GetItemCount(RoR2Content.Items.ArmorReductionOnHit) != 0)
                        {
                            v.temp = .2f;
                        }
                        else
                        {
                            v.temp = .85f;
                        }

                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Shattering Justice
                    case "ITEM_TPHEALINGNOVA_NAME":
                        v.daisy = true;
                        break;//Lepton Daisy
                    case "ITEM_NEARBYDAMAGEBONUS_NAME":
                        if (body.baseNameToken == "MERC_BODY_NAME" || body.baseNameToken == "LOADER_BODY_NAME" || body.baseNameToken == "VOIDSURVIVOR_BODY_NAME")
                        {
                            v.temp = .85f;
                        }
                        else
                        {
                            v.temp = .7f;
                        }

                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Focus Crystal
                    case "ITEM_LUNARSKILLREPLACEMENTS_NAME":
                        break;//Heresy Set
                    case "ITEM_LUNARUTILITYREPLACEMENT_NAME":
                        if (inventory.GetItemCount(RoR2Content.Items.LunarUtilityReplacement) != 0)
                        {
                            v.temp = .05f;
                        }
                        else
                        {
                            v.temp = .15f;
                        }

                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Strides of Heresy
                    case "ITEM_PROTECTIONPOTION_NAME":
                        v.temp = .25f;
                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Tix Flask
                    case "ITEM_THORNS_NAME":
                        if (inventory.GetEquipment(0).equipmentDef != RoR2Content.Equipment.BurnNearby)
                        {
                            v.temp = .6f;
                        }
                        else
                        {
                            v.temp = .85f;
                        }
                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Razorwire
                    case "ITEM_FLATHEALTH_NAME":
                        v.temp = .2f;
                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Bison Steak
                    case "ITEM_PEARL_NAME":
                        v.temp = .6f;
                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Pearl
                    case "ITEM_SHINYPEARL_NAME":
                        v.temp = .9f;
                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Irradiant Pearl
                    case "ITEM_BONUSGOLDPACKONKILL_NAME":
                        if (RoR2.Run.instance.stageClearCount > 5)
                        {
                            v.temp = .05f;
                        }
                        else
                        {
                            v.temp = .35f;
                        }

                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Ghor's Tome
                    case "ITEM_LASERTURBINE_NAME":
                        v.temp = .6f;
                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Resonance Disc
                    case "ITEM_LUNARPRIMARYREPLACEMENT_NAME":
                        v.temp = .1f;
                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Visions of Heresy
                    case "ITEM_NOVAONLOWHEALTH_NAME":
                        if (body.baseNameToken == "ENGI_BODY_NAME")
                        {
                            v.temp = .75f;
                        }
                        else
                        {
                            v.temp = .25f;
                        }

                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Genesis Loop
                    case "ITEM_LUNARTRINKET_NAME":
                        v.temp = .0f;
                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Beads of Fealty
                    case "ITEM_REPULSIONARMORPLATE_NAME":
                        v.temp = .4f;
                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Repulsion Armor Plate
                    case "ITEM_SQUIDTURRET_NAME":
                        v.temp = .05f;
                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        v.squidCount++;
                        break;//Squid Polyp
                    case "ITEM_DEATHMARK_NAME":
                        if (inventory.GetItemCount(RoR2Content.Items.DeathMark) == 0)
                        {
                            v.temp = .25f;
                        }
                        else
                        {
                            v.temp = .0f;
                        }

                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Death Mark
                    case "ITEM_INTERSTELLARDESKPLANT_NAME":
                        v.temp = .5f;
                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Interstellar Desk Plant
                    case "ITEM_ANCESTRALINCUBATOR_NAME":
                        break;//Ancestral Incubator
                    case "ITEM_FOCUSEDCONVERGENCE_NAME":
                        if (inventory.GetItemCount(RoR2Content.Items.FocusConvergence) > 2)
                        {
                            v.temp = .0f;
                        }
                        else
                        {
                            v.temp = .15f;
                        }

                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Focused Convergence
                    case "ITEM_FIREBALLSONHIT_NAME":
                        if (luck == -1)
                        {
                            v.temp = .25f;
                        }
                        else if (luck < -1)
                        {
                            v.temp = .05f;
                        }
                        else
                        {
                            v.temp = .8f;
                        }

                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Molten Perforator
                    case "ITEM_LIGHTNINGSTRIKEONHIT_NAME":
                        if (luck == -1)
                        {
                            v.temp = .3f;
                        }
                        else if (luck < -1)
                        {
                            v.temp = .05f;
                        }
                        else
                        {
                            v.temp = .9f;
                        }
                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Charged Perforator
                    case "ITEM_BLEEDONHITANDEXPLODE_NAME":
                        if (inventory.GetItemCount(RoR2Content.Items.BleedOnHitAndExplode) != 0)
                        {
                            v.temp = .05f;
                        }
                        else
                        {
                            v.temp = .8f;
                        }

                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Shatterspleen
                    case "ITEM_SIPHONONLOWHEALTH_NAME":
                        v.temp = .55f;
                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Mired Urn
                    case "ITEM_MONSTERSONSHRINEUSE_NAME":
                        v.temp = .0f;
                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Defiant Gouge
                    case "ITEM_RANDOMDAMAGEZONE_NAME":
                        v.temp = .15f;
                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Mercurial Rachis
                    case "ITEM_ARTIFACTKEY_NAME":
                        break;//Artifact Key
                    case "ITEM_CAPTAINDEFENSEMATRIX_NAME":
                        v.temp = 1.0f;
                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Defensive Microbots
                    case "ITEM_SCRAPWHITE_NAME":
                        break;//Item Scrap, White
                    case "ITEM_SCRAPGREEN_NAME":
                        break;//Item Scrap, Green
                    case "ITEM_SCRAPRED_NAME":
                        break;//Item Scrap, Red
                    case "ITEM_SCRAPYELLOW_NAME":
                        break;//Item Scrap, Yellow
                    case "ITEM_LUNARBADLUCK_NAME":
                        if (luck < 0)
                        {
                            v.temp = .55f;
                        }
                        else
                        {
                            v.temp = .3f;
                        }
                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Purity
                    case "ITEM_LUNARSECONDARYREPLACEMENT_NAME":
                        v.temp = .15f;
                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Hooks of Heresy
                    case "ITEM_ATTACKSPEEDANDMOVESPEED_NAME":
                        v.temp = .7f;
                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Mocha
                    case "ITEM_PRIMARYSKILLSHURIKEN_NAME":
                        if (body.baseNameToken == "RAILGUNNER_BODY_NAME")
                        {
                            v.temp = .05f;
                        }
                        else
                        {
                            v.temp = .7f;
                        }

                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Shuriken
                    case "ITEM_CRITDAMAGE_NAME":
                        v.temp = 1f;
                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Laser Scope
                    case "ITEM_DRONEWEAPONS_NAME":
                        v.temp = .6f;
                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Spare Drone Parts
                    case "ITEM_MOVESPEEDONKILL_NAME":
                        v.temp = .35f;
                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Hunter's Harpoon
                    case "ITEM_STRENGTHENBURN_NAME":
                        if (body.baseNameToken == "MAGE_BODY_NAME")
                        {
                            v.temp = .85f;
                        }
                        else if (inventory.GetItemCount(RoR2Content.Items.IgniteOnKill) != 0)
                        {
                            v.temp = .8f;
                        }
                        else
                        {
                            v.temp = .7f;
                        }

                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Ignition Tank
                    case "ITEM_HEALINGPOTION_NAME":
                        if (inventory.GetItemCount(RoR2.DLC1Content.Items.FragileDamageBonus) != 0 || body.baseNameToken == "ENGI_BODY_NAME")
                        {
                            v.temp = .65f;
                        }
                        else
                        {
                            v.temp = .25f;
                        }

                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Power Elixir
                    case "ITEM_HEALINGPOTIONCONSUMED_NAME":
                        break;//Empty Bottle
                    case "ITEM_PERMANENTDEBUFFONHIT_NAME":
                        v.temp = .85f;
                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Symbiotic Scorpion
                    case "ITEM_MOREMISSILE_NAME":
                        v.temp = 1.0f;
                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Pocket I.C.B.M.
                    case "ITEM_SKULLCOUNTER_NAME":
                        break;//Skull Token
                    case "ITEM_ROBOBALLBUDDY_NAME":
                        v.temp = .6f;
                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Empathy Cores
                    case "ITEM_PARENTEGG_NAME":
                        if (RoR2.Run.instance.stageClearCount > 5)
                        {
                            v.temp = .35f;
                        }
                        else
                        {
                            v.temp = .6f;
                        }

                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Planula
                    case "ITEM_LUNARSPECIALREPLACEMENT_NAME":
                        v.temp = .15f;
                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Essence of Heresy
                    case "ITEM_HALFSPEEDDOUBLEHEALTH_NAME":
                        if (body.moveSpeed > body.baseMoveSpeed * 2.5f)
                        {
                            v.temp = .65f;
                        }
                        else
                        {
                            v.temp = .4f;
                        }
                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Stone Flux Pauldron
                    case "ITEM_HALFATTACKSPEEDHALFCOOLDOWNS_NAME":
                        if (body.attackSpeed > body.baseAttackSpeed * 5f)
                        {
                            v.temp = .65f;
                        }
                        else if (body.attackSpeed > body.baseAttackSpeed * 2.5f)
                        {
                            v.temp = .45f;
                        }
                        else
                        {
                            v.temp = .35f;
                        }
                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Light Flux Pauldron
                    case "ITEM_LUNARWINGS_NAME":
                        break;//Blessings of Terafirmae
                    case "ITEM_RANDOMLYLUNAR_NAME":
                        v.temp = .0f;
                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Eulogy Zero
                    case "ITEM_IMMUNETODEBUFF_NAME":
                        if (inventory.GetItemCount(RoR2.DLC1Content.Items.ImmuneToDebuff) != 0)
                        {
                            v.temp = .45f;
                        }
                        else
                        {
                            v.temp = 1f;
                        }

                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Ben's Raincoat
                    case "ITEM_REGENERATINGSCRAP_NAME":
                        if (inventory.GetItemCount(RoR2.DLC1Content.Items.RegeneratingScrap) % 5 == 4)
                        {
                            v.temp = .9f;
                        }
                        else
                        {
                            v.temp = .75f;
                        }
                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Regenerating Scrap
                    case "ITEM_REGENERATINGSCRAPCONSUMED_NAME":
                        break;//Regenerating Scrap (Consumed)
                    case "ITEM_CRITGLASSESVOID_NAME":
                        if (luck < 0)
                        {
                            v.temp = .1f;
                        }
                        else if (luck > 0)
                        {
                            v.temp = .7f;
                        }
                        else
                        {
                            v.temp = .55f;
                        }
                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Lost Seer's Lenses
                    case "ITEM_EXPLODEONDEATHVOID_NAME":
                        v.temp = .8f;
                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Voidsent Flame
                    case "ITEM_BLEEDONHITVOID_NAME":
                        if (body.baseNameToken == "LOADER_BODY_NAME" || body.baseNameToken == "MAGE_BODY_NAME")
                        {
                            v.temp = .7f;
                        }
                        else if (inventory.GetItemCount(RoR2.DLC1Content.Items.BleedOnHitVoid) > 9)
                        {
                            v.temp = .1f;
                        }
                        else
                        {
                            v.temp = .35f;
                        }
                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Needletick
                    case "ITEM_TREASURECACHEVOID_NAME":
                        v.temp = .8f;
                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Encrusted Key
                    case "ITEM_BEARVOID_NAME":
                        v.temp = .5f;
                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Safer Spaces
                    case "ITEM_CLOVERVOID_NAME":
                        v.temp = 1.0f;
                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Benthic Bloom
                    case "ITEM_MISSILEVOID_NAME":
                        if (inventory.GetItemCount(RoR2.DLC1Content.Items.MissileVoid) == 0 && luck > 0)
                        {
                            v.temp = .15f;
                        }
                        else
                        {
                            v.temp = .7f;
                        }

                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Plasma Shrimp
                    case "ITEM_MUSHROOMVOID_NAME":
                        if (inventory.GetItemCount(RoR2Content.Items.ShieldOnly) != 0)
                        {
                            v.temp = .2f;
                        }
                        else
                        {
                            v.temp = .65f;
                        }

                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Weeping Fungus
                    case "ITEM_SLOWONHITVOID_NAME":
                        if (luck < 0)
                        {
                            v.temp = .2f;
                        }
                        else
                        {
                            v.temp = .55f;
                        }

                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Tentabauble
                    case "ITEM_CHAINLIGHTNINGVOID_NAME":
                        if (luck < -1)
                        {
                            v.temp = .15f;
                        }
                        else
                        {
                            v.temp = .95f;
                        }

                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Polylute
                    case "ITEM_EQUIPMENTMAGAZINEVOID_NAME":
                        v.temp = .55f;
                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Lysate Cell
                    case "ITEM_EXTRALIFEVOID_NAME":
                        v.temp = .9f;
                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Pluripotent Larva
                    case "ITEM_EXTRALIFEVOIDCONSUMED_NAME":
                        break;//Pluripotent Larva (Consumed)
                    case "ITEM_FRAGILEDAMAGEBONUS_NAME":
                        if (inventory.GetItemCount(RoR2.DLC1Content.Items.HealingPotion) != 0)
                        {
                            v.temp = .7f;
                        }
                        else
                        {
                            v.temp = .5f;
                        }

                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Delicate Watch
                    case "ITEM_FRAGILEDAMAGEBONUSCONSUMED_NAME":
                        break;//Delicate Watch (Broken)
                    case "ITEM_OUTOFCOMBATARMOR_NAME":
                        v.temp = .5f;
                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Oddly-shaped Opal
                    case "ITEM_SCRAPWHITESUPPRESSED_NAME":
                        break;//Strange Scrap, White
                    case "ITEM_SCRAPGREENSUPPRESSED_NAME":
                        break;//Strange Scrap, Green
                    case "ITEM_SCRAPREDSUPPRESSED_NAME":
                        break;//Strange Scrap, Red
                    case "ITEM_GOLDONHURT_NAME":
                        if (RoR2.Run.instance.stageClearCount > 4)
                        {
                            v.temp = .1f;
                        }
                        else if (Facepunch.Steamworks.Client.Instance.Lobby.GetMemberIDs().Length != 1)
                        {
                            v.temp = .6f;
                        }
                        else
                        {
                            v.temp = .4f;
                        }
                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Roll of Pennies
                    case "ITEM_RANDOMEQUIPMENTTRIGGER_NAME":
                        v.temp = .55f;
                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Bottled Chaos
                    case "ITEM_FREECHEST_NAME":
                        v.temp = .75f;
                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Shipping Request Form
                    case "ITEM_ELEMENTALRINGVOID_NAME":
                        v.temp = .6f;
                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Singularity Band
                    case "ITEM_LUNARSUN_NAME":
                        v.temp = .55f;
                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Egocentrism
                    case "ITEM_MINORCONSTRUCTONKILL_NAME":
                        v.temp = .3f;
                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Defense Nucleus
                    case "ITEM_VOIDMEGACRABITEM_NAME":
                        v.temp = .9f;
                        if (v.bestValue < v.temp)
                        {
                            v.bestValue = v.temp;
                            v.bestItem = i;
                        }
                        v.averageValue += v.temp / (float)nameTokens.Length;
                        break;//Newly Hatched Zoea

                    default:
                        break;
                }
            }

            return v;
        }
        private void Hooks()
        {
            if (!doHooks)
                return;
            DebugClass.Log($"Why the fuck does this only work if I leave the debug log in???????");
            doHooks = false;
            //On.RoR2.Interactor.AttemptInteraction += (orig, self, g) =>
            //{
            //    //LunarRecycler
            //    //SeerStation
            //    //CasinoChest
            //    //DebugClass.Log($"----------{g.name}");
            //    //ShouldSpeak(g.name);
            //    orig(self, g);
            //};
            On.RoR2.UI.PauseScreenController.OnEnable += (orig, self) =>
            {
                orig(self);
                if (Facepunch.Steamworks.Client.Instance.Lobby.GetMemberIDs().Length < 2 && RoR2.UI.PauseScreenController.instancesList.Count == 1 && casinoTimer > 0)
                {
                    casinoTimer = 10;
                    cheated = true;
                }
            };
            On.RoR2.PurchaseInteraction.OnInteractionBegin += (orig, self, i) =>
            {
                orig(self, i);
                if (self.CanBeAffordedByInteractor(i))
                {
                    try
                    {
                        if (self.gameObject.ToString().StartsWith("LunarRecycler"))
                        {
                            if (self.cost == 128)
                            {
                                ShouldSpeak("You do realise that you could just play on command right?"
                                    , "Just use command M9");
                            }
                            else if (self.cost == 2048)
                            {
                                ShouldSpeak("I don't think you heard me the first time, so let me reiterate. The command artifact lets you choose your items instead of being stuck rerolling here."
                                    , "What the fuck did I just say to you?");
                            }
                            else if (self.cost == 2097152)
                            {
                                ShouldSpeak("Ok so let me just put this over there, you can choose to use it or not."
                                    , "Bro, just take this item");
                                if (bonziActive)
                                    new SyncLunarReRoll(new Vector3(-94, -25, -47)).Send(R2API.Networking.NetworkDestination.Server);
                            }
                            else if (self.cost == 536870912)
                            {
                                if (Settings.CurrencyChanges.Value)
                                    ShouldSpeak("I'm just going to do this before you overflow your robux into the negatives"
                                    , "I'm just going to do this before you overflow your robux into the negatives");
                                else
                                    ShouldSpeak("I'm just going to do this before you overflow your coins into the negatives"
                                    , "I'm just going to do this before you overflow your coins into the negatives");

                                if (bonziActive)
                                    StartCoroutine(RestOfDroplets());
                            }
                        }
                        else if (self.gameObject.ToString().StartsWith("CasinoChest"))
                        {
                            casinoTimer = 10;
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
            };
            On.EntityStates.Missions.BrotherEncounter.Phase4.OnEnter += (orig, self) =>
            {
                orig(self);
                dontSpeak = 86400; //yeah if you wait a whole day in phase 4 you can break this, eat me.
            };
            On.EntityStates.BrotherMonster.TrueDeathState.OnEnter += (orig, self) =>
            {
                orig(self);
                dontSpeak = 5;
                //Main Camera(Clone)
                //MOISTURE_BONZIBUDDY_ACHIEVEMENT_ID
                if (!!LocalUserManager.readOnlyLocalUsersList[0].userProfile.HasUnlockable("MOISTURE_BONZIBUDDY_UNLOCKABLE_NAME"))//achievement not unlocked
                {
                    On.RoR2.Run.FixedUpdate += Run_FixedUpdate;
                    On.RoR2.HoldoutZoneController.FixedUpdate += HoldoutZoneController_FixedUpdate;
                    Chat.AddMessage($"<style=cWorldEvent>You hear a rumbling coming from the teleporter...</style>");
                    foreach (var item in Camera.allCameras)
                    {
                        var effect = item.gameObject.AddComponent<GlitchEffect>();
                        effect.Shader = Assets.Load<Shader>("@MoistureUpset_moisture_bonzistatic:assets/bonzibuddy/GlitchShader.shader");
                        effect.displacementMap = Assets.Load<Texture2D>("@MoistureUpset_moisture_bonzistatic:assets/bonzibuddy/test.png");
                        effect.intensity = 0;
                        effect.flipIntensity = 0;
                        effect.colorIntensity = 0;
                    }


                    charPosition = RoR2.NetworkUser.readOnlyLocalPlayersList[0].master?.GetBody().gameObject.transform;

                    obj1 = preloaded;
                    obj1 = Instantiate(obj1);
                    obj1.transform.position = new Vector3(448, -140, 525);

                    obj3 = preloaded;
                    obj3 = Instantiate(obj3);
                    obj3.transform.position = new Vector3(554, -140, 632);

                    obj4 = preloaded;
                    obj4 = Instantiate(obj4);
                    obj4.transform.position = new Vector3(675, -140, 757);

                    obj5 = preloaded;
                    obj5 = Instantiate(obj5);
                    obj5.transform.position = new Vector3(810, -140, 893);

                    obj6 = preloaded;
                    obj6 = Instantiate(obj6);
                    obj6.transform.position = new Vector3(974, -244, 1046);

                    obj2 = new GameObject();
                    obj2 = Instantiate(obj2);
                    obj2.transform.position = new Vector3(1105, -283, 1181);
                    AkSoundEngine.PostEvent("BonziGlitches", obj2);
                }
            };
            On.RoR2.UI.MainMenu.MainMenuController.SetDesiredMenuScreen += (orig, self, newscreen) =>
            {
                orig(self, newscreen);
                BonziBuddy.buddy.MainMenuMovement(newscreen.name);
            };
            On.RoR2.Run.OnClientGameOver += (orig, self, report) =>
            {
                orig(self, report);
                dioUsed = 0;
                dioHeld = 0;
                try
                {
                    if (report.gameEnding.endingTextToken == "GAME_RESULT_UNKNOWN")
                    {
                        ShouldSpeak("Kind of a cop-out isn't it?"
                            , "What a bitch");
                    }
                    GoTo(DEATH);
                }
                catch (Exception)
                {
                }
            };
            On.RoR2.SceneCatalog.OnActiveSceneChanged += (orig, oldS, newS) =>
            {
                try
                {
                    feelsGood = true;
                    sceneChange = true;
                    AkSoundEngine.SetRTPCValue("DistanceToBonzi", 800);
                    resetRun = false;
                    oncePerStage = true;
                    activeMountainShrine = false;
                    switch (newS.name)
                    {
                        case "logbook":
                            GoTo(LOGBOOK);
                            break;
                        case "title":
                            if (BigJank.getOptionValue(Settings.BonziBuddyBool) /*&& LocalUserManager.readOnlyLocalUsersList[0].userProfile.HasUnlockable("MOISTURE_BONZIBUDDY_UNLOCKABLE_NAME")*/ && !bonziActive)
                            {
                                Activate();
                            }
                            GoTo(MAINMENU);
                            break;
                        case "lobby":
                            GoTo(CHARSELECT);
                            break;
                        case "eclipseworld":
                            GoTo(ECLIPSE);
                            break;
                        case "outro":
                            buddy.enabled = false; /////solution for this
                            break;
                        case "moon2":
                            //frogge 
                            break;
                        default:
                            GoTo(M1);
                            break;
                    }
                    if (newS.name != "outro")
                    {
                        enabled = true; /////and this
                    }
                    if (newS.name != "moon2")
                    {
                        On.RoR2.HoldoutZoneController.FixedUpdate -= HoldoutZoneController_FixedUpdate;
                        On.RoR2.Run.FixedUpdate -= Run_FixedUpdate;
                    }
                    charPosition = null;
                    AkSoundEngine.ExecuteActionOnEvent(1901251578, AkActionOnEventType.AkActionOnEventType_Stop);
                    SyncBonziApproach.netIds.Clear();
                    SyncBonziApproach.distances.Clear();
                }
                catch (Exception)
                {
                }
                orig(oldS, newS);
            };
            On.RoR2.Inventory.GiveItem_ItemIndex_int += (orig, self, index, count) =>
            {
                try
                {
                    if (self.gameObject && self.gameObject.GetComponentInChildren<RoR2.CharacterMaster>() && self.gameObject.GetComponentInChildren<RoR2.CharacterMaster>().GetBody().gameObject)
                    {
                        new SyncItems(self.gameObject.GetComponentInChildren<RoR2.CharacterMaster>().GetBody().gameObject.GetComponent<NetworkIdentity>().netId, index, count).Send(R2API.Networking.NetworkDestination.Clients);
                    }
                }
                catch (Exception)
                {
                }
                //DebugClass.Log($"----------{ItemCatalog.GetItemDef(index).nameToken}");//this is the nametoken, this works
                orig(self, index, count);
            };
            On.RoR2.UI.ChatBox.SubmitChat += (orig, self) =>
            {
                try
                {
                    if (resetRun && (self.inputField.text.ToUpper() == "YES" || self.inputField.text.ToUpper() == "Y"))
                    {
                        //NetworkUser.readOnlyLocalPlayersList[0].master?.GetBody().healthComponent.Suicide();
                        //NetworkUser.readOnlyLocalPlayersList[0].master?.GetBody().gameObject.GetComponent<NetworkIdentity>()
                        new SyncSuicide((RoR2.NetworkUser.readOnlyLocalPlayersList[0].master?.GetBody().gameObject.GetComponent<NetworkIdentity>()).netId).Send(R2API.Networking.NetworkDestination.Server);
                        resetRun = false;
                    }
                    else if (resetRun && (self.inputField.text.ToUpper() == "NO" || self.inputField.text.ToUpper() == "N"))
                    {
                        ShouldSpeak("Fine, that's your loss"
                            , "Swag");
                        resetRun = false;
                    }
                }
                catch (Exception)
                {
                }
                orig(self);
            };
            On.RoR2.ShrineChanceBehavior.AddShrineStack += (orig, self, activator) =>
            {
                float yes = self.GetFieldValue<int>("successfulPurchaseCount");
                orig(self, activator);


                if (self.GetFieldValue<int>("successfulPurchaseCount") == yes)
                {
                    new SyncChance(activator.gameObject.GetComponentInChildren<RoR2.CharacterBody>().netId, self.GetFieldValue<int>("successfulPurchaseCount") != yes, "ChanceFailure").Send(R2API.Networking.NetworkDestination.Clients);
                }
                else
                {
                    new SyncChance(activator.gameObject.GetComponentInChildren<RoR2.CharacterBody>().netId, self.GetFieldValue<int>("successfulPurchaseCount") != yes, "ChanceSuccess").Send(R2API.Networking.NetworkDestination.Clients);
                }
            };
            On.RoR2.ShrineBloodBehavior.AddShrineStack += (orig, self, activator) =>
            {
                orig(self, activator);
                new SyncShrine(activator.gameObject.GetComponentInChildren<RoR2.CharacterBody>().netId, ShrineType.blood).Send(R2API.Networking.NetworkDestination.Clients);
            };
            On.RoR2.ShrineBossBehavior.AddShrineStack += (orig, self, activator) =>
            {
                orig(self, activator);
                new SyncShrine(activator.gameObject.GetComponentInChildren<RoR2.CharacterBody>().netId, ShrineType.mountain).Send(R2API.Networking.NetworkDestination.Clients);
            };
            On.RoR2.ShrineHealingBehavior.AddShrineStack += (orig, self, activator) =>
            {
                orig(self, activator);
                if (self.purchaseCount == 1)
                {
                    new SyncShrine(activator.gameObject.GetComponentInChildren<RoR2.CharacterBody>().netId, ShrineType.healing).Send(R2API.Networking.NetworkDestination.Clients);
                }
            };
            On.RoR2.ShrineRestackBehavior.AddShrineStack += (orig, self, activator) =>
            {
                new SyncShrine(activator.gameObject.GetComponentInChildren<RoR2.CharacterBody>().netId, ShrineType.order).Send(R2API.Networking.NetworkDestination.Clients);
                orig(self, activator);
            };
            On.RoR2.CharacterMaster.OnBodyDamaged += (orig, self, report) =>
            {
                if (report.victim)
                {
                    new SyncDamage(self.netId, report.damageInfo, report.victim.gameObject.GetComponent<NetworkIdentity>().netId).Send(R2API.Networking.NetworkDestination.Clients);
                }
                orig(self, report);
            };
            On.EntityStates.GenericCharacterDeath.OnEnter += (orig, self) =>
            {
                orig(self);
                try
                {
                    if (self.outer.gameObject.GetComponentInChildren<RoR2.PositionIndicator>() && self.outer.gameObject.GetComponentInChildren<RoR2.PositionIndicator>().name == "PlayerPositionIndicator(Clone)")
                    {
                        if (self.outer.gameObject.GetComponentInChildren<RoR2.CharacterBody>() == RoR2.NetworkUser.readOnlyLocalPlayersList[0].master?.GetBody())
                        {
                            buddy.PlayerDeath(self.outer.gameObject, self.outer.gameObject.GetComponentInChildren<RoR2.HealthComponent>().killingDamageType);
                        }
                        else
                        {
                            buddy.AllyDeath(self.outer.gameObject, self.outer.gameObject.GetComponentInChildren<RoR2.HealthComponent>().killingDamageType);
                        }
                    }
                }
                catch (Exception)
                {
                }
            };
            On.RoR2.BossGroup.DropRewards += (orig, self) =>
            {
                if (activeMountainShrine && self.dropPosition)
                {
                    mountainShrineItems = 1 + self.bonusRewardCount;
                    mountainShrineItems *= RoR2.Run.instance.participatingPlayerCount;
                    mountainShrineCount = mountainShrineItems;
                }
                orig(self);
                mountainShrineItems = 0;
                mountainShrineCount = 0;
                activeMountainShrine = false;
            };
            On.RoR2.PickupDropletController.CreatePickupDroplet_PickupIndex_Vector3_Vector3 += (orig, index, pos, vector) =>
            {
                orig(index, pos, vector);
                if (mountainShrineItems > 0)
                {
                    mountainShrineItems--;
                    new SyncMountain(index, mountainShrineCount).Send(R2API.Networking.NetworkDestination.Clients);
                }
            };
            On.RoR2.CharacterMasterNotificationQueue.PushItemTransformNotification += (orig, master, oldI, newI, type) =>
            {
                orig(master, oldI, newI, type);
                if (type == CharacterMasterNotificationQueue.TransformationType.LunarSun)
                {
                    ItemDef oldDef = ItemCatalog.GetItemDef(oldI);
                    switch (oldDef.nameToken)
                    {
                        case "ITEM_CLOVER_NAME":
                            ShouldSpeak("Oooooh, that sucks", "Are you fucking serious m8?");
                            break;//57 Leaf Clover
                        case "ITEM_SYRINGE_NAME":
                            break;//Soldier's Syringe
                        case "ITEM_BEAR_NAME":
                            if (master.inventory.GetItemCount(oldDef) == 0)
                            {
                                ShouldSpeak("Oof, there goes your block chance", "Block chance is for pussies anyway");
                            }
                            break;//Tougher Times
                        case "ITEM_BEHEMOTH_NAME":
                            if (master.inventory.GetItemCount(oldDef) == 0)
                            {
                                ShouldSpeak("Rocket Launcher doesn't go boom :(", true);
                            }
                            break;//Brilliant Behemoth
                        case "ITEM_MISSILE_NAME":
                            break;//AtG Missile Mk. 1
                        case "ITEM_EXPLODEONDEATH_NAME":
                            break;//Will-o'-the-wisp
                        case "ITEM_DAGGER_NAME":
                            break;//Ceremonial Dagger
                        case "ITEM_TOOTH_NAME":
                            break;//Monster Tooth
                        case "ITEM_CRITGLASSES_NAME":
                            if (master.inventory.GetItemCount(oldDef) == 8)
                            {
                                ShouldSpeak("Oh, no. What will you do without max crit?", true);
                            }
                            break;//Lens-Maker's Glasses
                        case "ITEM_HOOF_NAME":
                            break;//Paul's Goat Hoof
                        case "ITEM_FEATHER_NAME":
                            break;//Hopoo Feather
                        case "ITEM_CHAINLIGHTNING_NAME":
                            break;//Ukulele
                        case "ITEM_SEED_NAME":
                            break;//Leeching Seed
                        case "ITEM_ICICLE_NAME":
                            break;//Frost Relic
                        case "ITEM_GHOSTONKILL_NAME":
                            ShouldSpeak("Hahaha yes, die trash", true);
                            break;//Happiest Mask
                        case "ITEM_MUSHROOM_NAME":
                            if (master.inventory.GetItemCount(oldDef) == 0)
                            {
                                ShouldSpeak("No Bungus?", true);
                            }
                            break;//Bustling Fungus
                        case "ITEM_CROWBAR_NAME":
                            break;//Crowbar
                        case "ITEM_ATTACKSPEEDONCRIT_NAME":
                            break;//Predatory Instincts
                        case "ITEM_BLEEDONHIT_NAME":
                            break;//Tri-Tip Dagger
                        case "ITEM_SPRINTOUTOFCOMBAT_NAME":
                            break;//Red Whip
                        case "ITEM_FALLBOOTS_NAME":
                            if (master.inventory.GetItemCount(oldDef) == 0)
                            {
                                ShouldSpeak($"I sure hope you don't have {RoR2.Language.GetString(RoR2Content.Artifacts.weakAssKneesArtifactDef.nameToken)} enabled", true);
                            }
                            break;//H3AD-5T v2
                        case "ITEM_COOLDOWNONCRIT_NAME":
                            break;//Wicked Ring
                        case "ITEM_WARDONLEVEL_NAME":
                            break;//Warbanner
                        case "ITEM_WARCRYONMULTIKILL_NAME":
                            break;//Berzerker's Pauldron
                        case "ITEM_PHASING_NAME":
                            break;//Old War Stealthkit
                        case "ITEM_HEALONCRIT_NAME":
                            break;//Harvester's Scythe
                        case "ITEM_HEALWHILESAFE_NAME":
                            break;//Cautious Slug
                        case "ITEM_JUMPBOOST_NAME":
                            break;//Wax Quail
                        case "ITEM_PERSONALSHIELD_NAME":
                            break;//Personal Shield Generator
                        case "ITEM_NOVAONHEAL_NAME":
                            if (master.inventory.GetItemCount(oldDef) == 0)
                            {
                                ShouldSpeak("Maybe try using N'kuhana's Fact next time", true);
                            }
                            break;//N'kuhana's Opinion
                        case "ITEM_MEDKIT_NAME":
                            break;//Medkit
                        case "ITEM_EQUIPMENTMAGAZINE_NAME":
                            break;//Fuel Cell
                        case "ITEM_INFUSION_NAME":
                            break;//Infusion
                        case "ITEM_SHOCKNEARBY_NAME":
                            break;//Unstable Tesla Coil
                        case "ITEM_IGNITEONKILL_NAME":
                            break;//Gasoline
                        case "ITEM_BOUNCENEARBY_NAME":
                            break;//Sentient Meat Hook
                        case "ITEM_FIREWORK_NAME":
                            break;//Bundle of Fireworks
                        case "ITEM_BANDOLIER_NAME":
                            break;//Bandolier
                        case "ITEM_STUNCHANCEONHIT_NAME":
                            break;//Stun Grenade
                        case "ITEM_LUNARDAGGER_NAME":
                            ShouldSpeak("You should actually be glad to lose that", true);
                            break;//Shaped Glass
                        case "ITEM_GOLDONHIT_NAME":
                            break;//Brittle Crown
                        case "ITEM_SHIELDONLY_NAME":
                            break;//Transcendence
                        case "ITEM_ALIENHEAD_NAME":
                            break;//Alien Head
                        case "ITEM_TALISMAN_NAME":
                            break;//Soulbound Catalyst
                        case "ITEM_KNURL_NAME":
                            break;//Titanic Knurl
                        case "ITEM_BEETLEGLAND_NAME":
                            if (BigJank.getOptionValue(Settings.Winston))
                            {
                                ShouldSpeak("Winston swapped...", true);
                            }
                            break;//Queen's Gland
                        case "ITEM_SPRINTBONUS_NAME":
                            break;//Energy Drink
                        case "ITEM_SECONDARYSKILLMAGAZINE_NAME":
                            break;//Backup Magazine
                        case "ITEM_STICKYBOMB_NAME":
                            break;//Sticky Bomb
                        case "ITEM_TREASURECACHE_NAME":
                            break;//Rusted Key
                        case "ITEM_BOSSDAMAGEBONUS_NAME":
                            break;//Armor-Piercing Rounds
                        case "ITEM_SPRINTARMOR_NAME":
                            break;//Rose Buckler
                        case "ITEM_ELEMENTALRINGS_NAME":
                            break;//Kjaro and Runald's Bands
                        case "ITEM_ICERING_NAME":
                            break;//Runald's Band
                        case "ITEM_FIRERING_NAME":
                            break;//Kjaro's Band
                        case "ITEM_SLOWONHIT_NAME":
                            break;//Chronobauble
                        case "ITEM_EXTRALIFE_NAME":
                            break;//Dio's Best Friend
                        case "ITEM_EXTRALIFECONSUMED_NAME":
                            break;//Dio's Best Friend (Consumed)
                        case "ITEM_UTILITYSKILLMAGAZINE_NAME":
                            break;//Hardlight Afterburner
                        case "ITEM_HEADHUNTER_NAME":
                            ShouldSpeak("On a normal day it would be sad losing a red. This is not a normal day.", $"{Language.GetString(RoR2Content.Items.HeadHunter.nameToken)} is a shit item anyway");
                            break;//Wake of Vultures
                        case "ITEM_KILLELITEFRENZY_NAME":
                            break;//Brainstalks
                        case "ITEM_INCREASEHEALING_NAME":
                            break;//Rejuvenation Rack
                        case "ITEM_REPEATHEAL_NAME":
                            ShouldSpeak($"You probably didn't mean to grab {Language.GetString(RoR2Content.Items.RepeatHeal.nameToken)} anyway", true);
                            break;//Corpsebloom
                        case "ITEM_AUTOCASTEQUIPMENT_NAME":
                            if (master.inventory.GetItemCount(oldDef) == 0)
                            {
                                ShouldSpeak("*dies*", true);
                            }
                            break;//Gesture of the Drowned
                        case "ITEM_EXECUTELOWHEALTHELITE_NAME":
                            break;//Old Guillotine
                        case "ITEM_ENERGIZEDONEQUIPMENTUSE_NAME":
                            break;//War Horn
                        case "ITEM_BARRIERONOVERHEAL_NAME":
                            break;//Aegis
                        case "ITEM_TONICAFFLICTION_NAME":
                            break;//Tonic Affliction
                        case "ITEM_TITANGOLDDURINGTP_NAME":
                            if (BigJank.getOptionValue(Settings.AlexJones))
                            {
                                if (master.inventory.GetItemCount(oldDef) == 0)
                                {
                                    ShouldSpeak("Finally, inner peace", true);
                                }
                            }
                            break;//Halcyon Seed
                        case "ITEM_SPRINTWISP_NAME":
                            break;//Little Disciple
                        case "ITEM_BARRIERONKILL_NAME":
                            break;//Topaz Brooch
                        case "ITEM_ARMORREDUCTIONONHIT_NAME":
                            break;//Shattering Justice
                        case "ITEM_TPHEALINGNOVA_NAME":
                            break;//Lepton Daisy
                        case "ITEM_NEARBYDAMAGEBONUS_NAME":
                            if (master.inventory.GetItemCount(oldDef) == 0)
                            {
                                ShouldSpeak("All my crystals argon", true);
                            }
                            break;//Focus Crystal
                        case "ITEM_LUNARSKILLREPLACEMENTS_NAME":
                            break;//Heresy Set
                        case "ITEM_LUNARUTILITYREPLACEMENT_NAME":
                            break;//Strides of Heresy
                        case "ITEM_PROTECTIONPOTION_NAME":
                            break;//Tix Flask
                        case "ITEM_THORNS_NAME":
                            break;//Razorwire
                        case "ITEM_FLATHEALTH_NAME":
                            break;//Bison Steak
                        case "ITEM_PEARL_NAME":
                            break;//Pearl
                        case "ITEM_SHINYPEARL_NAME":
                            ShouldSpeak("Nahooooo your stats", true);
                            break;//Irradiant Pearl
                        case "ITEM_BONUSGOLDPACKONKILL_NAME":
                            break;//Ghor's Tome
                        case "ITEM_LASERTURBINE_NAME":
                            break;//Resonance Disc
                        case "ITEM_LUNARPRIMARYREPLACEMENT_NAME":
                            if (master.inventory.GetItemCount(oldDef) == 0 && Language.GetString(oldDef.nameToken).ToUpper().Contains("VISIONS"))
                            {
                                ShouldSpeak("I see, said the blind man", true);
                            }
                            break;//Visions of Heresy
                        case "ITEM_NOVAONLOWHEALTH_NAME":
                            break;//Genesis Loop
                        case "ITEM_LUNARTRINKET_NAME":
                            break;//Beads of Fealty
                        case "ITEM_REPULSIONARMORPLATE_NAME":
                            break;//Repulsion Armor Plate
                        case "ITEM_SQUIDTURRET_NAME":
                            ShouldSpeak("Squid friend :(", true);
                            break;//Squid Polyp
                        case "ITEM_DEATHMARK_NAME":
                            if (master.inventory.GetItemCount(oldDef) != 0)
                            {
                                ShouldSpeak("That's ok, extras didn't really do anything", true);
                            }
                            break;//Death Mark
                        case "ITEM_INTERSTELLARDESKPLANT_NAME":
                            break;//Interstellar Desk Plant
                        case "ITEM_ANCESTRALINCUBATOR_NAME":
                            break;//Ancestral Incubator
                        case "ITEM_FOCUSEDCONVERGENCE_NAME":
                            if (Facepunch.Steamworks.Client.Instance.Lobby.GetMemberIDs().Length == 2)
                            {
                                ShouldSpeak("Your teammate will thank you", true);
                            }
                            else if (Facepunch.Steamworks.Client.Instance.Lobby.GetMemberIDs().Length > 2)
                            {
                                ShouldSpeak("Your teammates will thank you", true);
                            }
                            break;//Focused Convergence
                        case "ITEM_FIREBALLSONHIT_NAME":
                            break;//Molten Perforator
                        case "ITEM_LIGHTNINGSTRIKEONHIT_NAME":
                            break;//Charged Perforator
                        case "ITEM_BLEEDONHITANDEXPLODE_NAME":
                            break;//Shatterspleen
                        case "ITEM_SIPHONONLOWHEALTH_NAME":
                            break;//Mired Urn
                        case "ITEM_MONSTERSONSHRINEUSE_NAME":
                            break;//Defiant Gouge
                        case "ITEM_RANDOMDAMAGEZONE_NAME":
                            break;//Mercurial Rachis
                        case "ITEM_ARTIFACTKEY_NAME":
                            ShouldSpeak("Oh yeah, it's big brain time", true);
                            break;//Artifact Key
                        case "ITEM_CAPTAINDEFENSEMATRIX_NAME":
                            ShouldSpeak("This is so sad, can we hit children?", true);
                            break;//Defensive Microbots
                        case "ITEM_SCRAPWHITE_NAME":
                            break;//Item Scrap, White
                        case "ITEM_SCRAPGREEN_NAME":
                            break;//Item Scrap, Green
                        case "ITEM_SCRAPRED_NAME":
                            break;//Item Scrap, Red
                        case "ITEM_SCRAPYELLOW_NAME":
                            break;//Item Scrap, Yellow
                        case "ITEM_LUNARBADLUCK_NAME":
                            break;//Purity
                        case "ITEM_LUNARSECONDARYREPLACEMENT_NAME":
                            break;//Hooks of Heresy
                        case "ITEM_ATTACKSPEEDANDMOVESPEED_NAME":
                            break;//Mocha
                        case "ITEM_PRIMARYSKILLSHURIKEN_NAME":
                            break;//Shuriken
                        case "ITEM_CRITDAMAGE_NAME":
                            break;//Laser Scope
                        case "ITEM_DRONEWEAPONS_NAME":
                            break;//Spare Drone Parts
                        case "ITEM_MOVESPEEDONKILL_NAME":
                            break;//Hunter's Harpoon
                        case "ITEM_STRENGTHENBURN_NAME":
                            break;//Ignition Tank
                        case "ITEM_HEALINGPOTION_NAME":
                            break;//Power Elixir
                        case "ITEM_HEALINGPOTIONCONSUMED_NAME":
                            break;//Empty Bottle
                        case "ITEM_PERMANENTDEBUFFONHIT_NAME":
                            break;//Symbiotic Scorpion
                        case "ITEM_MOREMISSILE_NAME":
                            if (master.inventory.GetItemCount(oldDef) == 0 && master.inventory.GetItemCount(RoR2Content.Items.Missile) != 0)
                            {
                                ShouldSpeak("Say goodbye to your triple a.t.g. chucklenuts", true);
                            }
                            break;//Pocket I.C.B.M.
                        case "ITEM_SKULLCOUNTER_NAME":
                            break;//Skull Token
                        case "ITEM_ROBOBALLBUDDY_NAME":
                            break;//Empathy Cores
                        case "ITEM_PARENTEGG_NAME":
                            break;//Planula
                        case "ITEM_LUNARSPECIALREPLACEMENT_NAME":
                            break;//Essence of Heresy
                        case "ITEM_HALFSPEEDDOUBLEHEALTH_NAME":
                            ShouldSpeak("Ahhh, it feels like taking shackles off", true);
                            break;//Stone Flux Pauldron
                        case "ITEM_HALFATTACKSPEEDHALFCOOLDOWNS_NAME":
                            break;//Light Flux Pauldron
                        case "ITEM_LUNARWINGS_NAME":
                            break;//Blessings of Terafirmae
                        case "ITEM_RANDOMLYLUNAR_NAME":
                            break;//Eulogy Zero
                        case "ITEM_IMMUNETODEBUFF_NAME":
                            if (master.inventory.GetItemCount(oldDef) == 0)
                            {
                                ShouldSpeak("Now we're both naked :)", true);
                            }
                            break;//Ben's Raincoat
                        case "ITEM_REGENERATINGSCRAP_NAME":
                            ShouldSpeak("It was nice while it lasted", true);
                            break;//Regenerating Scrap
                        case "ITEM_REGENERATINGSCRAPCONSUMED_NAME":
                            break;//Regenerating Scrap (Consumed)
                        case "ITEM_CRITGLASSESVOID_NAME":
                            break;//Lost Seer's Lenses
                        case "ITEM_EXPLODEONDEATHVOID_NAME":
                            break;//Voidsent Flame
                        case "ITEM_BLEEDONHITVOID_NAME":
                            break;//Needletick
                        case "ITEM_TREASURECACHEVOID_NAME":
                            break;//Encrusted Key
                        case "ITEM_BEARVOID_NAME":
                            break;//Safer Spaces
                        case "ITEM_CLOVERVOID_NAME":
                            break;//Benthic Bloom
                        case "ITEM_MISSILEVOID_NAME":
                            break;//Plasma Shrimp
                        case "ITEM_MUSHROOMVOID_NAME":
                            if (master.inventory.GetItemCount(oldDef) == 0)
                            {
                                ShouldSpeak("No Wungus?", true);
                            }
                            break;//Weeping Fungus
                        case "ITEM_SLOWONHITVOID_NAME":
                            if (master.inventory.GetItemCount(oldDef) == 0)
                            {
                                ShouldSpeak($"Ah crap, now you have to use {Language.GetString(RoR2Content.Items.SlowOnHit.nameToken)}s", true);
                            }
                            break;//Tentabauble
                        case "ITEM_CHAINLIGHTNINGVOID_NAME":
                            break;//Polylute
                        case "ITEM_EQUIPMENTMAGAZINEVOID_NAME":
                            break;//Lysate Cell
                        case "ITEM_EXTRALIFEVOID_NAME":
                            break;//Pluripotent Larva
                        case "ITEM_EXTRALIFEVOIDCONSUMED_NAME":
                            break;//Pluripotent Larva (Consumed)
                        case "ITEM_FRAGILEDAMAGEBONUS_NAME":
                            ShouldSpeak("It's better than it breaking", true);
                            break;//Delicate Watch
                        case "ITEM_FRAGILEDAMAGEBONUSCONSUMED_NAME":
                            break;//Delicate Watch (Broken)
                        case "ITEM_OUTOFCOMBATARMOR_NAME":
                            break;//Oddly-shaped Opal
                        case "ITEM_SCRAPWHITESUPPRESSED_NAME":
                            break;//Strange Scrap, White
                        case "ITEM_SCRAPGREENSUPPRESSED_NAME":
                            break;//Strange Scrap, Green
                        case "ITEM_SCRAPREDSUPPRESSED_NAME":
                            break;//Strange Scrap, Red
                        case "ITEM_GOLDONHURT_NAME":
                            break;//Roll of Pennies
                        case "ITEM_RANDOMEQUIPMENTTRIGGER_NAME":
                            break;//Bottled Chaos
                        case "ITEM_FREECHEST_NAME":
                            if (master.inventory.GetItemCount(oldDef) == 0)
                            {
                                ShouldSpeak("Free items? More like... Um... no items. Ha, gottem", true);
                            }
                            break;//Shipping Request Form
                        case "ITEM_ELEMENTALRINGVOID_NAME":
                            break;//Singularity Band
                        case "ITEM_LUNARSUN_NAME":
                            break;//Egocentrism
                        case "ITEM_MINORCONSTRUCTONKILL_NAME":
                            break;//Defense Nucleus
                        case "ITEM_VOIDMEGACRABITEM_NAME":
                            break;//Newly Hatched Zoea

                        default:
                            break;
                    }
                }
            };
            On.RoR2.PickupPickerController.OnDisplayBegin += (orig, self, promptcontroller, localUser, rigcontroller) =>
            {
                orig(self, promptcontroller, localUser, rigcontroller);
                try
                {
                    var options = self.GetFieldValue<PickupPickerController.Option[]>("options");
                    if (options.Length == 3)
                    {
                        List<string> nameTokens = new List<string>();
                        foreach (var item in options)
                        {
                            nameTokens.Add(PickupCatalog.GetPickupDef(item.pickupIndex).nameToken);
                        }
                        ItemsValue items = CalcItemValues(nameTokens.ToArray());
                        List<string> quotes = new List<string>();
                        if (items.bestValue < .15f)
                        {
                            quotes.Add("Well this sucks");
                            quotes.Add("I expected nothing and I'm still dissapointed");
                            quotes.Add("You know, basically anything else looks really good right now");
                        }
                        else if (items.bestValue < .2f)
                        {
                            quotes.Add("At least it technically could have been worse");
                            quotes.Add("I mean it could have been worse, but not really");
                        }
                        else if (items.bestValue < .25f)
                        {
                            if (items.averageValue < .2f)
                            {
                                quotes.Add("Not everything here is total garbage, just mostly garbage");
                            }
                            quotes.Add("Good items, please and thanks");
                            quotes.Add("A crumb of good items please");
                        }
                        else if (items.bestValue < .3f)
                        {
                            if (items.averageValue < .15f)
                            {
                                quotes.Add("Well at least not everything here is absolutely terrible");
                                quotes.Add($"Is {Language.GetString(PickupCatalog.GetPickupDef(options[items.bestItem].pickupIndex).nameToken)} really the best the game has to offer?");
                            }
                            else if (items.averageValue < .25f)
                            {
                                quotes.Add("Mediocre at best");
                            }
                            quotes.Add("I don't know, just pick something I guess");
                        }
                        else if (items.bestValue < .35f)
                        {
                            if (Facepunch.Steamworks.Client.Instance.Lobby.GetMemberIDs().Length == 2)
                            {
                                quotes.Add("Maybe you should trade with your teammate");
                            }
                            else if (Facepunch.Steamworks.Client.Instance.Lobby.GetMemberIDs().Length > 2)
                            {
                                quotes.Add("Maybe trade with one of your teammates");
                            }
                            if (items.averageValue > .275f)
                            {
                                quotes.Add("I present to you, the epitomy of mediocrity");
                            }
                            else
                            {
                                quotes.Add("Well it's not all mediocre, some of it is worse!");
                            }
                            quotes.Add("Well at least some of this is acceptable");
                            quotes.Add("Well, at least it's something I guess");
                        }
                        else if (items.bestValue < .4f)
                        {
                            if (items.averageValue > .3f)
                            {
                                quotes.Add("Well, I think we can work with these at least");
                            }
                            else if (items.averageValue < .25f)
                            {
                                quotes.Add("Well it's not all trash");
                            }
                            quotes.Add("Could've been better, but it could've been worse");
                        }
                        else if (items.bestValue < .45f)
                        {
                            if (items.averageValue < .35f)
                            {
                                quotes.Add("Alright we can work with that");
                            }
                            else
                            {
                                quotes.Add("Alright we can work with these");
                            }
                        }
                        else if (items.bestValue < .5f)
                        {
                            if (items.averageValue == .5f)
                            {
                                quotes.Add("Ahh, perfectly average, just as all things should be");
                            }
                            quotes.Add("It's ok");
                        }
                        else if (items.bestValue < .55f)
                        {
                            if (items.averageValue > .45f)
                            {
                                quotes.Add("Above average? technically");
                            }
                            quotes.Add("Now this... this isn't actually anything special but it's ok I guess");
                        }
                        else if (items.bestValue < .6f)
                        {
                            if (items.averageValue < .3f)
                            {
                                quotes.Add("Well at least you found a good item");
                            }
                            quotes.Add("Good stuff");
                        }
                        else if (items.bestValue < .65f)
                        {
                            if (items.averageValue > .6f)
                            {
                                quotes.Add("An actual hard choice for which item to choose? unbelievable");
                            }
                            quotes.Add("Not bad");
                            quotes.Add("Don't think too hard now");
                        }
                        else if (items.bestValue < .7f)
                        {
                            if (items.averageValue > .6f)
                            {
                                quotes.Add("Thanks game, but like actually tho");
                            }
                            else if (items.averageValue < .3f)
                            {
                                quotes.Add("Well there's just no contest here");
                            }
                            quotes.Add("Pretty good selection you got there");
                        }
                        else if (items.bestValue < .75f)
                        {
                            if (items.averageValue < .65f)
                            {
                                quotes.Add("Not too hard of a choice I hope");
                            }
                            else if (items.averageValue > .65f)
                            {
                                quotes.Add("What a nice selection");
                            }
                            quotes.Add("Looks good to me chief");
                        }
                        else if (items.bestValue < .8f)
                        {
                            if (items.averageValue > .75f)
                            {
                                quotes.Add("There really is no bad option here");
                                quotes.Add($"I'm partial to {Language.GetString(PickupCatalog.GetPickupDef(options[UnityEngine.Random.Range(0, options.Length)].pickupIndex).nameToken)}");
                            }
                            else if (items.averageValue < .5f)
                            {
                                quotes.Add($"{Language.GetString(PickupCatalog.GetPickupDef(options[items.bestItem].pickupIndex).nameToken)} is really carrying this batch");
                            }
                            quotes.Add("Ahhhhh, these will do nicely");
                        }
                        else if (items.bestValue < .85f)
                        {
                            quotes.Add("Can't ask for much better than this");
                        }
                        else if (items.bestValue < .9f)
                        {
                            if (items.averageValue < .4f)
                            {
                                quotes.Add($"This would have been terrible if not for {Language.GetString(PickupCatalog.GetPickupDef(options[items.bestItem].pickupIndex).nameToken)}");
                            }
                            else if (items.averageValue < .6f)
                            {
                                quotes.Add($"They really tried to drag down {Language.GetString(PickupCatalog.GetPickupDef(options[items.bestItem].pickupIndex).nameToken)}");
                            }
                            else if (items.averageValue > .8f)
                            {
                                quotes.Add("So many choices...");
                            }
                            quotes.Add("It's a good day to get items");
                        }
                        else if (items.bestValue < .95f)
                        {
                            if (items.averageValue > .875f)
                            {
                                quotes.Add("Well I think you just used up all your luck for the run");
                            }
                            else if (items.averageValue > .85f)
                            {
                                quotes.Add("If only the game would let you pick two items");
                            }
                            quotes.Add("Now this is what I'm talking about");
                        }
                        else if (items.bestValue < 1.0f)
                        {
                            if (items.averageValue < .7f)
                            {
                                quotes.Add("I mean, I only see one option here");
                            }
                            else if (items.averageValue > .9f)
                            {
                                quotes.Add("Why do you make us chose game? You can be cruel sometimes");
                            }
                            quotes.Add("Poggers!");
                            quotes.Add("Let's gooooooo");
                        }
                        else
                        {
                            if (items.averageValue < .45)
                            {
                                quotes.Add("Wow, trash, trash, and the best item ever");
                            }
                            else if (items.averageValue > .9f)
                            {
                                quotes.Add("I'm almost angry it gave you so many good items when you can only pick one... almost");
                            }
                            else if (items.averageValue > .8f)
                            {
                                quotes.Add($"I mean, yeah, they're all neat, but {Language.GetString(PickupCatalog.GetPickupDef(options[items.bestItem].pickupIndex).nameToken)} takes the cake");
                            }
                            quotes.Add("WOO, YEAH BABY! THAT'S WHAT I'VE BEEN WAITING FOR! THAT'S WHAT IT'S ALL ABOUT!");
                            quotes.Add("Ok ok ok ok, keep calm");
                            quotes.Add("It's that easy");
                        }
                        if (items.daisy)
                        {
                            ShouldSpeak($"Yooooo, {Language.GetString("ITEM_TPHEALINGNOVA_NAME")}", true);
                        }
                        else if (options.Length != 0)
                        {
                            ShouldSpeak(quotes[UnityEngine.Random.Range(0, quotes.Count)], true);
                        }
                    }
                }
                catch (Exception e)
                {
                    DebugClass.Log($"----------{e}");
                }
            };
        }
        public void FreeCommand(Vector3 v)
        {
            GameObject table = GameObject.Find("HOLDER: Store").transform.Find("LunarShop").Find("LunarTable").gameObject;
            foreach (var item in table.GetComponentsInChildren<PurchaseInteraction>())
            {
                if (item.Networkavailable)
                {
                    item.Networkavailable = false;
                    item.available = false;
                    item.gameObject.transform.Find("Display").Find("mdlBazaarBabyFlower").Find("PickupDisplay").gameObject.SetActive(false);
                    StartCoroutine(DropletCoroutine(v));
                    return;
                }
            }
        }
        bool Lock = false;
        bool sceneChange = false;
        IEnumerator RestOfDroplets()
        {
            sceneChange = false;
            yield return new WaitUntil(() => !Lock || sceneChange);
            Lock = true;
            if (!sceneChange)
                new SyncLunarReRoll(new Vector3(-98, -16, -43)).Send(R2API.Networking.NetworkDestination.Server);
            yield return new WaitUntil(() => !Lock || sceneChange);
            Lock = true;
            if (!sceneChange)
                new SyncLunarReRoll(new Vector3(-97, -13, -36)).Send(R2API.Networking.NetworkDestination.Server);
            yield return new WaitUntil(() => !Lock || sceneChange);
            Lock = true;
            if (!sceneChange)
                new SyncLunarReRoll(new Vector3(-93, -10, -31)).Send(R2API.Networking.NetworkDestination.Server);
            yield return new WaitUntil(() => !Lock || sceneChange);
            Lock = true;
            if (!sceneChange)
                new SyncLunarReRoll(new Vector3(-94, -7, -28)).Send(R2API.Networking.NetworkDestination.Server);
            sceneChange = false;
        }
        IEnumerator DropletCoroutine(Vector3 v)
        {
            PickupDropletController.onDropletHitGroundServer += OnDropletHitGroundServer;
            PickupDropletController.CreatePickupDroplet(PickupCatalog.FindPickupIndex(RoR2Content.Items.LunarBadLuck.itemIndex), v, new Vector3(0, 0, 0));
            yield return new WaitForSeconds(1);
            PickupDropletController.onDropletHitGroundServer -= OnDropletHitGroundServer;
            Lock = false;
        }

        private static void OnDropletHitGroundServer(ref GenericPickupController.CreatePickupInfo createPickupInfo, ref bool shouldSpawn)
        {
            PickupIndex pickupIndex = createPickupInfo.pickupIndex;
            PickupDef pickupDef = PickupCatalog.GetPickupDef(pickupIndex);
            if (pickupDef == null || (pickupDef.itemIndex == ItemIndex.None && pickupDef.equipmentIndex == EquipmentIndex.None))
            {
                return;
            }
            GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Command/CommandCube.prefab").WaitForCompletion(), createPickupInfo.position, createPickupInfo.rotation);
            gameObject.GetComponent<PickupIndexNetworker>().NetworkpickupIndex = pickupIndex;
            gameObject.GetComponent<PickupPickerController>().SetOptionsFromPickupForCommandArtifact(pickupIndex);
            NetworkServer.Spawn(gameObject);
            shouldSpawn = false;
        }
        private void HoldoutZoneController_FixedUpdate(On.RoR2.HoldoutZoneController.orig_FixedUpdate orig, HoldoutZoneController self)
        {
            float num = Time.fixedDeltaTime * dist;
            if (NetworkServer.active)
            {
                Time.fixedDeltaTime -= num;
            }
            orig(self);
            if (NetworkServer.active)
            {
                Time.fixedDeltaTime += num;
            }
        }
        private void Run_FixedUpdate(On.RoR2.Run.orig_FixedUpdate orig, Run self)
        {
            orig(self);
            if (charPosition != null)
            {
                float num = Vector3.Distance(charPosition.position, obj2.transform.position) - 75f;
                if (num >= 0)
                {
                    new SyncBonziApproach((int)num, charPosition.gameObject.GetComponentInChildren<NetworkIdentity>().netId).Send(R2API.Networking.NetworkDestination.Clients);
                }
            }
            Run.instance.fixedTime -= Time.unscaledDeltaTime * dist;
        }
        float dist = 0;
        public void BonziApproach(int distance)
        {
            if (distance < 800)
            {
                float distance2;
                distance2 = ((1f - ((float)distance / 800f)) * .6f) + .4f;
                if (distance2 > 1)
                {
                    distance2 = 1;
                }
                dist = distance2;
                return;
            }
            dist = 0f;
        }

        public void Mountain(List<PickupIndex> pickups)
        {
            List<string> nameTokens = new List<string>();
            foreach (var item in pickups)
            {
                nameTokens.Add(PickupCatalog.GetPickupDef(item).nameToken);
            }
            ItemsValue items = CalcItemValues(nameTokens.ToArray());
            if (items.squidCount != 0)
            {
                if (items.squidCount > 1)
                {
                    ShouldSpeak($"You got {items.squidCount} {RoR2.Language.GetString("ITEM_SQUIDTURRET_NAME")}s, nothing else matters"
    , $"You got {items.squidCount} {RoR2.Language.GetString("ITEM_SQUIDTURRET_NAME")}s, nothing else matters");
                }
                else
                {
                    ShouldSpeak($"You got a {RoR2.Language.GetString("ITEM_SQUIDTURRET_NAME")}, nothing else matters"
, $"You got a {RoR2.Language.GetString("ITEM_SQUIDTURRET_NAME")}, nothing else matters");
                }
            }
            else
            {
                List<string> quotes = new List<string>();
                if (daniel && (int)MLG.progress > 0)
                {
                    if (items.averageValue > .95f)
                    {
                        quotes.Add("Holy shit M8");
                        quotes.Add("Damn bro that's some nice loot");
                    }
                    else if (items.averageValue > .75f)
                    {
                        quotes.Add("I could quickscope you with these items");
                        quotes.Add("Not too shabby");
                    }
                    else if (items.averageValue > .55f)
                    {
                        quotes.Add("I'll allow it");
                        quotes.Add("It could have been worse M9");
                    }
                    else if (items.averageValue > .35f)
                    {
                        quotes.Add("Gee... thanks");
                        quotes.Add("Why would anyone do a mountain shrine for this");
                    }
                    else
                    {
                        quotes.Add("What the fuck");
                        quotes.Add("Wow, it's nothing");
                    }
                }
                else
                {
                    if (items.averageValue > .95f)
                    {
                        quotes.Add("It can't get any better than this");
                        quotes.Add("Just like the simulations");
                    }
                    else if (items.averageValue > .75f)
                    {
                        quotes.Add("Hey... that's pretty good");
                        quotes.Add("Not too shabby");
                    }
                    else if (items.averageValue > .55f)
                    {
                        quotes.Add("I'll allow it");
                        quotes.Add("Could have been worse");
                    }
                    else if (items.averageValue > .35f)
                    {
                        quotes.Add("Gee... thanks");
                        quotes.Add("This is why people don't do mountain shrines");
                    }
                    else
                    {
                        quotes.Add("I expected nothing and I'm still dissapointed");
                        quotes.Add("Wow, it's nothing");
                        quotes.Add("My disappointment is immeasurable and my day is ruined");
                        quotes.Add("This has been the worst trade deal in the history of trade deals, maybe ever");
                    }
                }
                ShouldSpeak(quotes[UnityEngine.Random.Range(0, quotes.Count)], quotes[UnityEngine.Random.Range(0, quotes.Count)]);
            }
        }
        string[] ratio = new string[] { "ratio", "wrong", "get a job", "unfunny", "cope", "don't care", "didn't ask", "owned", "ur a toddler", "who asked", "dead game", "seethe", "ur a coward", "stay mad", "ok", "cry about it", "L", "skill issue", "triggered", "ok and?", "cringe", "touch grass", "not based", "you're white", "grammar issue", "go outside", "get good", "reported", "you're gay", "you're straight", "ur mom", "not funny didn't laugh", "fatherless", "ur BF ugly", "you fell off", "no life", "counter ratio", "STFU", "no social credit", "lol", "irrelevant", "hoes mad", "cope harder", "log off", "rejected", "screenshotted", "pound salt", "ur a minor", "k.", "any askers", "copium", "ok boomer", "no bitches", "muted", "go tell reddit", "simp", "get stick bugged", "talk nonsense", "you're a full time discord mod", "get clapped", "ratio again", "cancelled", "freer than air", "screencapped your bio", "NFT owner", "dog water", "you don't know 2 + 2", "try again", "you failed kindergarten", "you have an anime profile picture", "orphan", "go to bed", "furry", "dream stan" };
        List<string> usedWords = new List<string>();
        public void Chance(bool number1VictoryRoyale)
        {
            if (number1VictoryRoyale)
            {
                failCount = 0;
            }
            else
            {
                failCount++;
                switch (failCount)
                {
                    case 1:
                        break;
                    case 2:
                        ShouldSpeak("L", true);
                        break;
                    default:
                        string cum;
                        int counter = 0;
                        while (counter < 20)
                        {
                            counter++;
                            cum = ratio[UnityEngine.Random.Range(0, ratio.Length)];
                            if (!usedWords.Contains(cum))
                            {
                                usedWords.Add(cum);
                                if (usedWords.Count > 20)
                                {
                                    usedWords.RemoveAt(0);
                                }
                                break;
                            }
                        }
                        ShouldSpeak($"+ {usedWords[usedWords.Count - 1]}", true);
                        break;
                }
            }
        }
        public void GenericShrine(ShrineType type)
        {
            List<string> quotes = new List<string>();
            RoR2.Inventory inventory = RoR2.NetworkUser.readOnlyLocalPlayersList[0].master?.GetBody().inventory;
            switch (type)
            {
                case ShrineType.blood:
                    if (inventory.GetItemCount(RoR2Content.Items.Mushroom) == 0 && inventory.GetItemCount(RoR2Content.Items.HealWhileSafe) == 0 && inventory.GetItemCount(RoR2Content.Items.Medkit) == 0 && inventory.GetItemCount(RoR2Content.Items.Tooth) == 0 && inventory.GetItemCount(RoR2Content.Items.PersonalShield) < 5 && inventory.GetItemCount(RoR2Content.Items.HealOnCrit) == 0 && inventory.GetItemCount(RoR2Content.Items.Seed) == 0 && inventory.GetItemCount(RoR2Content.Items.Plant) == 0 && inventory.GetItemCount(RoR2Content.Items.Knurl) == 0 && inventory.GetItemCount(RoR2Content.Items.ShieldOnly) == 0 && inventory.GetItemCount(RoR2Content.Items.LunarDagger) == 0 && inventory.GetItemCount(RoR2.DLC1Content.Items.MushroomVoid) == 0 && inventory.GetItemCount(RoR2.DLC1Content.Items.HealingPotion) == 0 && inventory.GetItemCount(RoR2.DLC1Content.Items.ExtraLifeVoid) == 0 && inventory.GetItemCount(RoR2Content.Items.ParentEgg) == 0)
                    {
                        quotes.Add("I hope you have some plan to heal this off");
                        bloodShrineTimer = 15f;
                    }
                    else
                    {
                        bloodShrineTimer = 7.5f;
                    }
                    break;
                case ShrineType.mountain:
                    quotes.Add("Is there really any reason to not hit these?");
                    quotes.Add($"I can't wait to get some extra {RoR2.Language.GetString(RoR2Content.Items.TPHealingNova.nameToken)}s");
                    quotes.Add("It's free real estate");
                    break;
                case ShrineType.healing:
                    quotes.Add("People actually use these?");
                    quotes.Add("Wow! This is garbage. You actually like this?");
                    break;
                case ShrineType.order:
                    dontSpeak = 2;
                    if (inventory.GetItemCount(RoR2Content.Items.Clover) - inventory.GetItemCount(RoR2Content.Items.LunarBadLuck) > 0 || (inventory.GetItemCount(RoR2Content.Items.Missile) != 0 && inventory.GetItemCount(RoR2Content.Items.Clover) - inventory.GetItemCount(RoR2Content.Items.LunarBadLuck) >= 0))
                    {
                        quotes.Add("This was so worth it");
                    }
                    if (inventory.GetItemCount(RoR2Content.Items.Clover) - inventory.GetItemCount(RoR2Content.Items.LunarBadLuck) < 0)
                    {
                        quotes.Add("I hope you are ready to never launch another ATG missle");
                    }
                    if (inventory.GetItemCount(RoR2Content.Items.LunarTrinket) > 1)
                    {
                        quotes.Add("Here's hoping you find a lunar pool soon");
                    }
                    if (inventory.GetItemCount(RoR2Content.Items.BleedOnHit) > 7)
                    {
                        quotes.Add($"Do you really need this many {RoR2.Language.GetString("ITEM_BLEEDONHIT_NAME")}s?");
                    }
                    if (inventory.GetItemCount(RoR2Content.Items.Hoof) != 0 || inventory.GetItemCount(RoR2Content.Items.SprintBonus) != 0 || inventory.GetItemCount(RoR2Content.Items.SprintOutOfCombat) != 0)
                    {
                        quotes.Add("Speeeeeeeeeeeed");
                    }
                    if (inventory.GetItemCount(RoR2Content.Items.Mushroom) != 0)
                    {
                        quotes.Add("BUNGUS");
                    }
                    if (inventory.GetItemCount(RoR2Content.Items.FlatHealth) != 0)
                    {
                        quotes.Add("Wow, I don't think you could have done worse on your white roll");
                    }
                    if (inventory.GetItemCount(RoR2Content.Items.ScrapWhite) != 0 || inventory.GetItemCount(RoR2Content.Items.ScrapRed) != 0 || inventory.GetItemCount(RoR2Content.Items.ScrapGreen) != 0 || inventory.GetItemCount(RoR2Content.Items.ScrapYellow) != 0)
                    {
                        quotes.Add("At least now you just need a good printer or two");
                    }
                    if (inventory.GetItemCount(RoR2Content.Items.CritGlasses) > 10)
                    {
                        quotes.Add("Too much crit! Too much crit!");
                    }
                    dioHeld = inventory.GetItemCount(RoR2Content.Items.ExtraLife);
                    if (inventory.GetItemCount(RoR2Content.Items.Bear) != 0)
                    {
                        quotes.Add("You can now block attacks almost as hard as I get blocked on twitter");
                    }
                    if (inventory.GetItemCount(RoR2Content.Items.EquipmentMagazine) != 0 || inventory.GetItemCount(RoR2Content.Items.AutoCastEquipment) != 0)
                    {
                        quotes.Add("Equipment Cooldown Status: None");
                    }
                    if (inventory.GetItemCount(RoR2Content.Items.Feather) != 0)
                    {
                        quotes.Add("Where we're going, we don't need the floor");
                    }
                    if (inventory.GetItemCount(RoR2Content.Items.TPHealingNova) != 0)
                    {
                        quotes.Add($"Waow it's a {RoR2.Language.GetString("ITEM_TPHEALINGNOVA_NAME")} build guys!");
                    }
                    if (inventory.GetItemCount(RoR2Content.Items.Phasing) != 0)
                    {
                        quotes.Add("When you get hit you have a chance to become permanently invisible");
                    }
                    try
                    {
                        if (inventory.GetItemCount(RoR2Content.Items.Thorns) != 0 && inventory.currentEquipmentState.equipmentDef.nameToken == "EQUIPMENT_BURNNEARBY_NAME")
                        {
                            quotes.Add("Oh yeah, it's all coming together");
                        }
                    }
                    catch (Exception e)
                    {
                        DebugClass.Log(e);
                    }
                    if (inventory.GetItemCount(RoR2Content.Items.ExplodeOnDeath) != 0)
                    {
                        quotes.Add("Wisp jar go boom");
                    }
                    if (inventory.GetItemCount(RoR2Content.Items.AlienHead) > 1 || inventory.GetItemCount(RoR2Content.Items.KillEliteFrenzy) > 1)
                    {
                        quotes.Add("What is a cooldown?");
                    }
                    if (inventory.GetItemCount(RoR2Content.Items.Behemoth) > 1)
                    {
                        quotes.Add("I don't know if you really needed more than one Behemoth");
                    }
                    if (inventory.GetItemCount(RoR2Content.Items.ExtraLife) + inventory.GetItemCount(RoR2.DLC1Content.Items.ExtraLifeVoid) > 1)
                    {
                        quotes.Add("At least you won't be able to lose for a while");
                    }
                    if (inventory.GetItemCount(RoR2Content.Items.Plant) > 1)
                    {
                        quotes.Add("Deskplant Pog");
                    }
                    if (inventory.GetItemCount(RoR2Content.Items.ShinyPearl) > 1)
                    {
                        quotes.Add($"Holy crap he got the {RoR2.Language.GetString("ITEM_SHINYPEARL_NAME")}s");
                    }
                    if (inventory.GetItemCount(RoR2Content.Items.LunarDagger) > 1)
                    {
                        quotes.Add("Your health is no more");
                    }
                    if (inventory.GetItemCount(RoR2Content.Items.ShieldOnly) > 1)
                    {
                        quotes.Add("Become the shield");
                    }
                    if (inventory.GetItemCount(RoR2.DLC1Content.Items.HealingPotion) > 1)
                    {
                        quotes.Add("This is just discount dios");
                    }
                    if (inventory.GetItemCount(RoR2.DLC1Content.Items.AttackSpeedAndMoveSpeed) > 1)
                    {
                        quotes.Add("Can you overdose on caffine?");
                    }
                    if (inventory.GetItemCount(RoR2.DLC1Content.Items.CloverVoid) > 1)
                    {
                        quotes.Add("So what you are telling me, is that starting next level all your items will be red");
                    }
                    if (inventory.GetItemCount(RoR2.DLC1Content.Items.StrengthenBurn) > 1 && SurvivorCatalog.FindSurvivorDefFromBody(NetworkUser.readOnlyLocalPlayersList[0].master?.GetBody().gameObject) == RoR2Content.Survivors.Mage)
                    {
                        quotes.Add("I hope you brought your burn moves");
                    }
                    if (inventory.GetItemCount(RoR2.DLC1Content.Items.RegeneratingScrap) > 4)
                    {
                        quotes.Add("Did someone say free reds every stage?");
                    }
                    if (inventory.GetItemCount(RoR2.DLC1Content.Items.CritGlassesVoid) > 10)
                    {
                        quotes.Add("Well if it isn't a boss, it might as well be dead");
                    }
                    if (inventory.GetItemCount(RoR2.DLC1Content.Items.ChainLightningVoid) > 4)
                    {
                        quotes.Add("Single target damage status: through the roof");
                    }
                    if (inventory.GetItemCount(RoR2.DLC1Content.Items.FragileDamageBonus) > 1)
                    {
                        quotes.Add("You're really going to regret this once you fall to low health");
                    }
                    if (inventory.GetItemCount(RoR2.DLC1Content.Items.HalfSpeedDoubleHealth) > 1)
                    {
                        quotes.Add("Movement speed? Where we're going we don't need movement speed");
                    }
                    //new items here

                    if (quotes.Count == 0)
                    {
                        quotes.Add("Interesting choice");
                        quotes.Add("It certainly makes the run more interesting");
                    }
                    break;
                default:
                    quotes.Add("");
                    break;
            }
            if (quotes.Count != 0)
                ShouldSpeak(quotes[UnityEngine.Random.Range(0, quotes.Count)], true);
        }
        public void NotEnoughMoney()
        {
            if (idling)
            {
                if (UnityEngine.Random.Range(0, 5) == 0)
                {
                    string[] quotes = { "Woah there buddy, you don't have enough money for that one.", $"Sorry {username}, I can't give credit. Come back when you're a little hmmmmm, richer.", "hmmmmmmmmmm, no" };
                    ShouldSpeak(quotes[UnityEngine.Random.Range(0, quotes.Length)], true);
                }
            }
        }
        public bool resetRun = false;
        public void Items(RoR2.Inventory inventory, ItemIndex index, int count, GameObject g)
        {
            StartCoroutine(Items(inventory, index, count, g, false));
        }
        bool usedGreenRegenScrap = true;
        bool feelsGood = true;
        public IEnumerator Items(RoR2.Inventory inventory, ItemIndex index, int count, GameObject g, bool yeet)
        {
            yield return new WaitForSeconds(.1f);
            bool temp = true;
            try
            {
                if (BigJank.getOptionValue(Settings.ScaleHitMarkerWithCrit))
                {
                    AkSoundEngine.SetRTPCValue("AirhornAudio", 100 - (NetworkUser.readOnlyLocalPlayersList[0].master?.GetBody()).crit);
                }
                else
                {
                    AkSoundEngine.SetRTPCValue("AirhornAudio", 100);
                }
            }
            catch (Exception)
            {
            }
            if (dontSpeak <= 0)
            {
                string itemToken = ItemCatalog.GetItemDef(index).nameToken;
                if (inventory.GetTotalItemCountOfTier(ItemTier.Tier3) == 1 && itemToken == RoR2Content.Items.Plant.nameToken)
                {
                    ShouldSpeak($"I see that you have received {RoR2.Language.GetString(itemToken)} as your first red item, would you like me to end the run now? yes or no."
                            , "Ah I see you received this fucking garbage as your first red item, would you like to end your suffering now? yes or no.");
                    resetRun = true;
                }
                else
                {
                    int luck = -1 * inventory.GetItemCount(RoR2Content.Items.LunarBadLuck);
                    if (inventory.GetItemCount(RoR2.DLC1Content.Items.CloverVoid) == 0)
                    {
                        luck += inventory.GetItemCount(RoR2Content.Items.Clover);
                    }
                    switch (itemToken)
                    {
                        case "ITEM_SYRINGE_NAME":
                            if (inventory.GetItemCount(RoR2Content.Items.Syringe) > 10 && casinoTimer > 0 && cheated)
                            {
                                ShouldSpeak("Did you really need to pause buffer to get more attack speed?"
                            , "I saw you cheating M8");
                            }
                            else if (inventory.GetItemCount(RoR2Content.Items.Syringe) == 11)
                            {
                                ShouldSpeak("Don't you think you have enough attack speed?"
                                    , "Don't you think you have enough attack speed?");
                            }
                            break;
                        case "ITEM_BEAR_NAME":
                            if (inventory.GetItemCount(RoR2Content.Items.Bear) == 50)
                            {
                                ShouldSpeak("I don't know if you really need any more block chance"
                                    , "Still not enough chance to block my quickscopes");
                            }
                            if (inventory.GetItemCount(RoR2Content.Items.Bear) == 101)
                            {
                                ShouldSpeak("You know stacking them further is almost pointless..."
                                    , "No no, keep stacking block chance, I'm sure it's a great use of your time");
                            }
                            break;
                        case "ITEM_BEHEMOTH_NAME":
                            ShouldSpeak("Haha rocket launcher go boom"
                                    , "Tactical nuke incoming!");
                            break;
                        case "ITEM_MISSILE_NAME":
                            //ShouldSpeak("This really pogs my champ");
                            break;
                        case "ITEM_EXPLODEONDEATH_NAME":
                            //fire in a jar thing
                            break;
                        case "ITEM_DAGGER_NAME":
                            //red dagger
                            if (inventory.GetItemCount(RoR2Content.Items.Dagger) == 1)
                            {
                                ShouldSpeak("muda muda muda muda mudamudamudamudamuda MUDAAAAA!"
                                    , "Have jojo references gone too far?!");
                            }
                            break;
                        case "ITEM_TOOTH_NAME":
                            //monster tooth
                            break;
                        case "ITEM_CRITGLASSES_NAME":
                            if (inventory.GetItemCount(RoR2Content.Items.CritGlasses) > 10 && casinoTimer > 0 && cheated)
                            {
                                ShouldSpeak("Hahahaha you idiot"
                                    , "You really suck at this cheating don't you");
                            }
                            break;
                        case "ITEM_HOOF_NAME":
                            break;
                        case "ITEM_FEATHER_NAME":
                            if (inventory.GetItemCount(RoR2Content.Items.Feather) > 8 && casinoTimer > 0 && cheated)
                            {
                                ShouldSpeak("Bro you have too many jumps already, why did you cheese the game to get more?"
                                    , "noclip hax enabled");
                            }
                            break;
                        case "ITEM_AACANNON_NAME":
                            //not used
                            break;
                        case "ITEM_CHAINLIGHTNING_NAME":
                            //uke
                            break;
                        case "ITEM_PLASMACORE_NAME":
                            //not used
                            break;
                        case "ITEM_SEED_NAME":
                            //leeching
                            break;
                        case "ITEM_ICICLE_NAME":
                            ShouldSpeak("At least they buffed it"
                                    , "At least they buffed it");
                            //frost relic
                            break;
                        case "ITEM_GHOSTONKILL_NAME":
                            ShouldSpeak($"At least it's not {RoR2.Language.GetString(RoR2Content.Items.Plant.nameToken)}"
                                    , "Well that fucking sucks");
                            break;
                        case "ITEM_MUSHROOM_NAME":
                            if (inventory.GetItemCount(RoR2Content.Items.Mushroom) == 1 && inventory.GetItemCount(RoR2.DLC1Content.Items.MushroomVoid) == 0)
                            {
                                if (SurvivorCatalog.FindSurvivorDefFromBody(g.GetComponentInChildren<CharacterBody>().gameObject) == RoR2Content.Survivors.Engi)
                                {
                                    ShouldSpeak("Oh yeah, it's all coming together"
                                    , "Oh yeah, it's all coming together");
                                }
                                else
                                {
                                    ShouldSpeak("BUNGUS"
                                    , "BUNGUS");
                                }
                            }
                            //bungus
                            break;
                        case "ITEM_CROWBAR_NAME":
                            break;
                        case "ITEM_LEVELBONUS_NAME":
                            //not used
                            break;
                        case "ITEM_ATTACKSPEEDONCRIT_NAME":
                            break;
                        case "ITEM_BLEEDONHIT_NAME":
                            if (inventory.GetItemCount(RoR2Content.Items.BleedOnHit) > 10 && casinoTimer > 0 && cheated)
                            {
                                ShouldSpeak("Hahahaha you idiot"
                                    , "You fucking dipshit");
                            }
                            if (inventory.GetItemCount(RoR2Content.Items.BleedOnHit) == 10)
                            {
                                ShouldSpeak("Oh yeah, it's gamer time."
                                    , "I'm enabling Windows G");
                            }
                            break;
                        case "ITEM_SPRINTOUTOFCOMBAT_NAME":
                            break;
                        case "ITEM_FALLBOOTS_NAME":
                            //the legendary
                            break;
                        case "ITEM_COOLDOWNONCRIT_NAME":
                            //wicked ring not used
                            break;
                        case "ITEM_WARDONLEVEL_NAME":
                            //warbanner
                            break;
                        case "ITEM_PHASING_NAME":
                            //stealthkit
                            break;
                        case "ITEM_HEALONCRIT_NAME":
                            break;
                        case "ITEM_HEALWHILESAFE_NAME":
                            //slug
                            break;
                        case "ITEM_TEMPESTONKILL_NAME":
                            //not used
                            break;
                        case "ITEM_PERSONALSHIELD_NAME":
                            break;
                        case "ITEM_EQUIPMENTMAGAZINE_NAME":
                            //fuel cell
                            break;
                        case "ITEM_NOVAONHEAL_NAME":
                            //legendary heal damage thing
                            break;
                        case "ITEM_SHOCKNEARBY_NAME":
                            //tesla
                            break;
                        case "ITEM_INFUSION_NAME":
                            break;
                        case "ITEM_WARCRYONCOMBAT_NAME":
                            //not used
                            break;
                        case "ITEM_CLOVER_NAME":
                            if (inventory.GetItemCount(RoR2.DLC1Content.Items.CloverVoid) == 0)
                            {
                                if (casinoTimer > 0 && cheated)
                                {
                                    ShouldSpeak("I would normally congratualte you but you cheated so ehhhh"
                                        , "Cheating is just part of the game");
                                }
                                if (inventory.GetItemCount(RoR2Content.Items.LunarBadLuck) == 0)
                                {
                                    if (inventory.GetItemCount(RoR2Content.Items.Clover) == 1)
                                    {
                                        ShouldSpeak("run = won"
                                        , "Oh fuck yeah M9");
                                    }
                                }
                                else if (inventory.GetItemCount(RoR2Content.Items.LunarBadLuck) == 1)
                                {
                                    ShouldSpeak("I bet you are regretting that purity now aren't ya?"
                                        , "I bet you are regretting that purity now aren't ya?");
                                }
                                else
                                {
                                    ShouldSpeak("I bet you are regretting those purities now aren't ya?"
                                        , "I bet you are regretting those purities now aren't ya?");
                                }
                            }
                            break;
                        case "ITEM_MEDKIT_NAME":
                            break;
                        case "ITEM_BANDOLIER_NAME":
                            if (casinoTimer > 0 && cheated)
                            {
                                ShouldSpeak("You could have pause buffered for litterally any other item"
                                    , "Why the fuck would you cheat just to get this trash");
                            }
                            break;
                        case "ITEM_BOUNCENEARBY_NAME":
                            //meat hook
                            break;
                        case "ITEM_IGNITEONKILL_NAME":
                            //gas
                            break;
                        case "ITEM_PLANTONHIT_NAME":
                            //not used
                            break;
                        case "ITEM_STUNCHANCEONHIT_NAME":
                            break;
                        case "ITEM_FIREWORK_NAME":
                            if (casinoTimer > 0 && cheated)
                            {
                                ShouldSpeak("You know, I actually don't blame you for buffering to get this item"
                                    , "You kinda suck at this cheating thing don't you");
                            }
                            break;
                        case "ITEM_LUNARDAGGER_NAME":
                            if (inventory.GetItemCount(RoR2Content.Items.LunarDagger) == 1)
                            {
                                if (Facepunch.Steamworks.Client.Instance.Lobby.GetMemberIDs().Length == 2)
                                {
                                    ShouldSpeak("At least you have your teammate to pickup the slack when you inevitably die"
                                        , "At least you have your teammate to pickup the slack when you inevitably die");
                                }
                                else if (Facepunch.Steamworks.Client.Instance.Lobby.GetMemberIDs().Length > 2)
                                {
                                    ShouldSpeak("At least you have your teammates to pickup the slack when you inevitably die"
                                        , "At least you have your teammates to pickup the slack when you inevitably die");
                                }
                                else
                                {
                                    ShouldSpeak("Should you really be doing this?"
                                        , "This is absolutely the correct decision");
                                }
                            }
                            else if (inventory.GetItemCount(RoR2Content.Items.LunarDagger) == 2)
                            {
                                ShouldSpeak($"Ah whatever, you already have {inventory.GetItemCount(RoR2Content.Items.LunarDagger) - 1} of them, how much could one more hurt?"
                                    , "Yes, obtain more damage");
                            }
                            break;
                        case "ITEM_GOLDONHIT_NAME":
                            //crown
                            break;
                        case "ITEM_MAGEATTUNEMENT_NAME":
                            //not used
                            break;
                        case "ITEM_WARCRYONMULTIKILL_NAME":
                            //pauldron
                            break;
                        case "ITEM_BOOSTHP_NAME":
                            //not used
                            break;
                        case "ITEM_BOOSTDAMAGE_NAME":
                            //not used
                            break;
                        case "ITEM_SHIELDONLY_NAME":
                            if (inventory.GetItemCount(RoR2Content.Items.ShieldOnly) == 1)
                            {
                                ShouldSpeak($"Ah I see you are a gamer of culture."
                                            , "Health is for bitches anyway");
                            }
                            //trans
                            break;
                        case "ITEM_ALIENHEAD_NAME":
                            break;
                        case "ITEM_TALISMAN_NAME":
                            //catalyst
                            break;
                        case "ITEM_KNURL_NAME":
                            break;
                        case "ITEM_BEETLEGLAND_NAME":
                            if (BigJank.getOptionValue(Settings.Winston))
                            {
                                ShouldSpeak("Winston please switch"
                                    , "Ded game xd");
                            }
                            break;
                        case "ITEM_BURNNEARBY_NAME":
                            //not used
                            break;
                        case "ITEM_CRITHEAL_NAME":
                            //not used
                            break;
                        case "ITEM_CRIPPLEWARDONLEVEL_NAME":
                            //not used
                            break;
                        case "ITEM_SPRINTBONUS_NAME":
                            break;
                        case "ITEM_SECONDARYSKILLMAGAZINE_NAME":
                            break;
                        case "ITEM_STICKYBOMB_NAME":
                            if (inventory.GetItemCount(RoR2Content.Items.StickyBomb) > 20 && casinoTimer > 0 && cheated)
                            {
                                ShouldSpeak("Hahahaha you idiot"
                                    , "Nice going");
                            }
                            break;
                        case "ITEM_TREASURECACHE_NAME":
                            //rusted key
                            break;
                        case "ITEM_BOSSDAMAGEBONUS_NAME":
                            break;
                        case "ITEM_SPRINTARMOR_NAME":
                            break;
                        case "ITEM_ICERING_NAME":
                            break;
                        case "ITEM_FIRERING_NAME":
                            break;
                        case "ITEM_SLOWONHIT_NAME":
                            break;
                        case "ITEM_EXTRALIFE_NAME":
                            if (casinoTimer > 0 && cheated)
                            {
                                ShouldSpeak("I hope you lose it quickly"
                                    , "I hope you lose it quickly");
                            }
                            dioHeld += count;
                            break;
                        case "ITEM_EXTRALIFECONSUMED_NAME":
                            break;
                        case "ITEM_UTILITYSKILLMAGAZINE_NAME":
                            //hardlight
                            break;
                        case "ITEM_HEADHUNTER_NAME":
                            //vultures
                            break;
                        case "ITEM_KILLELITEFRENZY_NAME":
                            //stonks
                            break;
                        case "ITEM_REPEATHEAL_NAME":
                            switch (UnityEngine.Random.Range(0, 2))
                            {
                                case 0:
                                    ShouldSpeak("That better have been an accident"
                                        , "Why the fuck you would take this");
                                    break;
                                case 1:
                                    ShouldSpeak("Get a load of this idiot"
                                        , "Get a load of this idiot");
                                    break;
                                case 2:
                                    ShouldSpeak("You're going to regret this later"
                                        , "I thought you were a real gamer");
                                    break;
                                default:
                                    break;
                            }
                            //corpseblossom
                            break;
                        case "ITEM_GHOST_NAME":
                            //not used
                            break;
                        case "ITEM_HEALTHDECAY_NAME":
                            //not used
                            break;
                        case "ITEM_AUTOCASTEQUIPMENT_NAME":
                            if (inventory.GetItemCount(RoR2Content.Items.AutoCastEquipment) + inventory.GetItemCount(RoR2Content.Items.EquipmentMagazine) < 4
                                && inventory.GetItemCount(RoR2Content.Items.Talisman) == 0
                                //inventory.currentEquipmentState.equipmentDef.nameToken == "EQUIPMENT_BURNNEARBY_NAME"
                                //&& inventory.GetEquipmentIndex() == EquipmentIndex.Tonic
                                && inventory.currentEquipmentState.equipmentDef.nameToken == "EQUIPMENT_TONIC_NAME"
                                && inventory.GetItemCount(RoR2Content.Items.Clover) - inventory.GetItemCount(RoR2Content.Items.LunarBadLuck) <= 0)
                            {
                                ShouldSpeak("I hope you enjoy the tonic afflictions"
                                        , "I hope you enjoy the tonic afflictions");
                            }
                            //gesture
                            break;
                        case "ITEM_INCREASEHEALING_NAME":
                            //rejuv rack
                            break;
                        case "ITEM_JUMPBOOST_NAME":
                            //quail
                            break;
                        case "ITEM_DRIZZLEPLAYERHELPER_NAME":
                            //not used
                            break;
                        case "ITEM_EXECUTELOWHEALTHELITE_NAME":
                            if (RoR2.Run.instance.stageClearCount + 1 > 5 && inventory.GetItemCount(RoR2Content.Items.ExecuteLowHealthElite) == 1)
                            {
                                ShouldSpeak("Finally"
                                    , "Fucking finally");
                            }
                            break;
                        case "ITEM_ENERGIZEDONEQUIPMENTUSE_NAME":
                            //war horn
                            break;
                        case "ITEM_BARRIERONOVERHEAL_NAME":
                            break;
                        case "ITEM_TONICAFFLICTION_NAME":
                            if (inventory.GetItemCount(RoR2Content.Items.Clover) - inventory.GetItemCount(RoR2Content.Items.LunarBadLuck) < 0)
                            {
                                ShouldSpeak("What are you even thinking!?"
                                    , "What the fuck are were you thinking?!");
                            }
                            else if (inventory.GetItemCount(RoR2Content.Items.AutoCastEquipment) + inventory.GetItemCount(RoR2Content.Items.EquipmentMagazine) < 4
                                && inventory.GetItemCount(RoR2Content.Items.Talisman) == 0
                                && inventory.currentEquipmentState.equipmentDef.nameToken == "EQUIPMENT_TONIC_NAME"
                                && inventory.GetItemCount(RoR2Content.Items.Clover) - inventory.GetItemCount(RoR2Content.Items.LunarBadLuck) <= 0
                                && inventory.GetItemCount(RoR2Content.Items.AutoCastEquipment) > 0)
                            {
                                ShouldSpeak("You deserve that one"
                                    , "You deserve that on");
                            }
                            else
                            {
                                ShouldSpeak("Maybe the tonic life shouldn't be for you?"
                                    , "Maybe the tonic life shouldn't be for you?");
                            }
                            break;
                        case "ITEM_TITANGOLDDURINGTP_NAME":
                            //halcyon seed
                            break;
                        case "ITEM_SPRINTWISP_NAME":
                            //disciple
                            break;
                        case "ITEM_BARRIERONKILL_NAME":
                            break;
                        case "ITEM_ARMORREDUCTIONONHIT_NAME":
                            break;
                        case "ITEM_TPHEALINGNOVA_NAME":
                            if (casinoTimer > 0 && cheated)
                            {
                                ShouldSpeak("Hahaha I bet you missed a great item to get this instead"
                                    , "Smoke weed every day");
                            }
                            //shiton daisy
                            break;
                        case "ITEM_NEARBYDAMAGEBONUS_NAME":
                            //focus crystal
                            break;
                        case "ITEM_LUNARUTILITYREPLACEMENT_NAME":
                            //strides
                            break;
                        case "ITEM_MONSOONPLAYERHELPER_NAME":
                            //not used
                            break;
                        case "ITEM_THORNS_NAME":
                            //razorwire
                            break;
                        case "ITEM_REGENONKILL_NAME":
                            //meat
                            break;
                        case "ITEM_PEARL_NAME":
                            break;
                        case "ITEM_SHINYPEARL_NAME":
                            if (inventory.GetItemCount(RoR2Content.Items.ShinyPearl) == 1)
                            {
                                ShouldSpeak("bruh"
                                        , "Ez Clap");
                            }
                            break;
                        case "ITEM_BONUSGOLDPACKONKILL_NAME":
                            //ghor
                            break;
                        case "ITEM_LASERTURBINE_NAME":
                            //res disc
                            break;
                        case "ITEM_LUNARPRIMARYREPLACEMENT_NAME":
                            //visions
                            break;
                        case "ITEM_NOVAONLOWHEALTH_NAME":
                            break;
                        case "ITEM_LUNARTRINKET_NAME":
                            if (inventory.GetItemCount(RoR2Content.Items.LunarTrinket) == 1)
                            {
                                ShouldSpeak("Ten bucks you only grabbed these to dump them into a pool later"
                                    , "Ten bucks you only grabbed these to dump them into a pool later");
                            }
                            else if (inventory.GetItemCount(RoR2Content.Items.LunarTrinket) == 2)
                            {
                                ShouldSpeak("I knew it"
                                    , "I knew it");
                            }
                            //beads
                            break;
                        case "ITEM_INVADINGDOPPELGANGER_NAME":
                            //not used
                            break;
                        case "ITEM_CUTHP_NAME":
                            //not used
                            break;
                        case "ITEM_ARTIFACTKEY_NAME":
                            break;
                        case "ITEM_ARMORPLATE_NAME":
                            break;
                        case "ITEM_SQUID_NAME":
                            break;
                        case "ITEM_DEATHMARK_NAME":
                            if (inventory.GetItemCount(RoR2Content.Items.DeathMark) == 2)
                            {
                                ShouldSpeak("You do realise stacking these does basically nothing right?"
                                    , "Why would you ever grab more than one?");
                            }
                            break;
                        case "ITEM_PLANT_NAME":
                            //idp
                            if (inventory.GetItemCount(RoR2Content.Items.Plant) == 1)
                            {
                                ShouldSpeak("Well well well, if it isn't the best item in the game"
                                    , "Holy shit M8, the run has been won");

                            }
                            break;
                        case "ITEM_INCUBATOR_NAME":
                            //not used
                            break;
                        case "ITEM_FOCUSCONVERGENCE_NAME":
                            break;
                        case "ITEM_BOOSTATTACKSPEED_NAME":
                            //not used
                            break;
                        case "ITEM_ADAPTIVEARMOR_NAME":
                            //not used
                            break;
                        case "ITEM_CAPTAINDEFENSEMATRIX_NAME":
                            break;
                        case "ITEM_FIREBALLSONHIT_NAME":
                            //perforator
                            break;
                        case "ITEM_BLEEDONHITANDEXPLODE_NAME":
                            //spleeeeeen
                            break;
                        case "ITEM_SIPHONONLOWHEALTH_NAME":
                            //urn
                            break;
                        case "ITEM_MONSTERSONSHRINEUSE_NAME":
                            break;
                        case "ITEM_RANDOMDAMAGEZONE_NAME":
                            break;
                        case "ITEM_SCRAPWHITE_NAME":
                            break;
                        case "ITEM_SCRAPGREEN_NAME":
                            break;
                        case "ITEM_SCRAPRED_NAME":
                            if (inventory.GetItemCount(RoR2Content.Items.ScrapRed) == 1)
                            {
                                ShouldSpeak("Good luck ever finding a printer for this"
                                    , "Good luck ever finding a printer for this");

                            }
                            break;
                        case "ITEM_SCRAPYELLOW_NAME":
                            break;
                        case "ITEM_LUNARBADLUCK_NAME":
                            if (inventory.GetItemCount(RoR2Content.Items.LunarBadLuck) == 1)
                            {
                                if (inventory.GetItemCount(RoR2Content.Items.Clover) == 1)
                                {
                                    ShouldSpeak("There goes your luck Sadge"
                                        , "Hippity Hoppity your luck is no longer your property");
                                }
                                else if (inventory.GetItemCount(RoR2Content.Items.Clover) > 1)
                                {
                                    ShouldSpeak("It's ok, you have some luck to spare"
                                        , "At least you have some excess luck");
                                }
                                else
                                {
                                    ShouldSpeak("This is almost definitely a bad idea."
                                        , "I have never seen a better idea");
                                }
                            }
                            break;
                        case "ITEM_BOOSTEQUIPMENTRECHARGE_NAME":
                            //not used
                            break;
                        case "ITEM_COUNT_NAME":
                            break;
                        case "ITEM_ATTACKSPEEDANDMOVESPEED_NAME":
                            break;//Mocha
                        case "ITEM_PRIMARYSKILLSHURIKEN_NAME":
                            if (inventory.GetItemCount(RoR2.DLC1Content.Items.PrimarySkillShuriken) == 1)
                            {
                                ShouldSpeak($"Imagine if they made {RoR2.Language.GetString(RoR2Content.Items.LunarPrimaryReplacement.nameToken)} into a good item... oh wait.", true);
                            }
                            break;//Shuriken
                        case "ITEM_CRITDAMAGE_NAME":
                            break;//Laser Scope
                        case "ITEM_DRONEWEAPONS_NAME":
                            break;//Spare Drone Parts
                        case "ITEM_MOVESPEEDONKILL_NAME":
                            break;//Hunter's Harpoon
                        case "ITEM_STRENGTHENBURN_NAME":
                            break;//Ignition Tank
                        case "ITEM_HEALINGPOTION_NAME":
                            break;//Power Elixir
                        case "ITEM_HEALINGPOTIONCONSUMED_NAME":
                            ShouldSpeak("mmm tasty health potion", true);
                            break;//Empty Bottle
                        case "ITEM_PERMANENTDEBUFFONHIT_NAME":
                            break;//Symbiotic Scorpion
                        case "ITEM_MOREMISSILE_NAME":
                            break;//Pocket I.C.B.M.
                        case "ITEM_SKULLCOUNTER_NAME":
                            break;//Skull Token
                        case "ITEM_ROBOBALLBUDDY_NAME":
                            if (BigJank.getOptionValue(Settings.ObamaPrism))
                            {
                                ShouldSpeak("MR OBAMA GET DOWN", true);
                            }
                            break;//Empathy Cores
                        case "ITEM_PARENTEGG_NAME":
                            break;//Planula
                        case "ITEM_LUNARSPECIALREPLACEMENT_NAME":
                            break;//Essence of Heresy
                        case "ITEM_HALFSPEEDDOUBLEHEALTH_NAME":
                            break;//Stone Flux Pauldron
                        case "ITEM_HALFATTACKSPEEDHALFCOOLDOWNS_NAME":
                            break;//Light Flux Pauldron
                        case "ITEM_LUNARWINGS_NAME":
                            break;//Blessings of Terafirmae
                        case "ITEM_RANDOMLYLUNAR_NAME":
                            break;//Eulogy Zero
                        case "ITEM_IMMUNETODEBUFF_NAME":
                            ShouldSpeak("I wish I had clothes :(", "");
                            break;//Ben's Raincoat
                        case "ITEM_REGENERATINGSCRAP_NAME":
                            if (usedGreenRegenScrap)
                            {
                                ShouldSpeak("I used the scrap to create the scrap", true);
                            }
                            else
                            {
                                ShouldSpeak("Oonga Boonga free items", true);
                            }
                            break;//Regenerating Scrap
                        case "ITEM_REGENERATINGSCRAPCONSUMED_NAME":
                            if (inventory.GetItemCount(RoR2.DLC1Content.Items.RegeneratingScrapConsumed) == 5 && feelsGood)
                            {
                                ShouldSpeak("Oh that feels good", true);
                                feelsGood = false;
                            }
                            if (inventory.GetItemCount(RoR2.DLC1Content.Items.RegeneratingScrapConsumed) == 1 || usedGreenRegenScrap)
                            {
                                usedGreenRegenScrap = true;
                                temp = false;
                            }
                            break;//Regenerating Scrap (Consumed)
                        case "ITEM_CRITGLASSESVOID_NAME":
                            if (inventory.GetItemCount(RoR2.DLC1Content.Items.CritGlassesVoid) == 200)
                            {
                                ShouldSpeak("What the fuck", true);
                            }
                            break;//Lost Seer's Lenses
                        case "ITEM_EXPLODEONDEATHVOID_NAME":
                            break;//Voidsent Flame
                        case "ITEM_BLEEDONHITVOID_NAME":
                            break;//Needletick
                        case "ITEM_TREASURECACHEVOID_NAME":
                            break;//Encrusted Key
                        case "ITEM_BEARVOID_NAME":
                            if (inventory.GetEquipment(0).equipmentDef != RoR2Content.Equipment.Cleanse && inventory.GetItemCount(RoR2.DLC1Content.Items.BearVoid) == 1)
                            {
                                ShouldSpeak($"Now just get blast shower", true);
                            }
                            break;//Safer Spaces
                        case "ITEM_CLOVERVOID_NAME":
                            if (inventory.GetItemCount(RoR2.DLC1Content.Items.CloverVoid) == 1)
                            {
                                ShouldSpeak("Ooooooooh boy, here we go", true);
                            }
                            break;//Benthic Bloom
                        case "ITEM_MISSILEVOID_NAME":
                            if (inventory.GetItemCount(RoR2.DLC1Content.Items.MissileVoid) == 1)
                            {
                                if (luck == 1)
                                {
                                    ShouldSpeak("Questionable move", true);
                                }
                                else if (luck == 2)
                                {
                                    ShouldSpeak("You do realize you have extremely high luck right?", true);
                                }
                                else if (luck > 2)
                                {
                                    ShouldSpeak("Why would you ever take that when your luck is so high?", "You fucking idiot");
                                }
                            }
                            break;//Plasma Shrimp
                        case "ITEM_MUSHROOMVOID_NAME":
                            if (inventory.GetItemCount(RoR2.DLC1Content.Items.MushroomVoid) == 1)
                            {
                                ShouldSpeak("WUNGUS", true);
                            }
                            break;//Weeping Fungus
                        case "ITEM_SLOWONHITVOID_NAME":
                            if (inventory.GetItemCount(RoR2.DLC1Content.Items.SlowOnHitVoid) == 1)
                            {
                                ShouldSpeak($"{RoR2.Language.GetString("BROTHER_BODY_NAME")} is trembling", $"{RoR2.Language.GetString("BROTHER_BODY_NAME")} is fucked lmao xd");
                            }
                            break;//Tentabauble
                        case "ITEM_CHAINLIGHTNINGVOID_NAME":
                            if (inventory.GetItemCount(RoR2.DLC1Content.Items.ChainLightningVoid) == 1)
                            {
                                ShouldSpeak("It's the best purple item, you can't change my mind", true);
                            }
                            break;//Polylute
                        case "ITEM_EQUIPMENTMAGAZINEVOID_NAME":
                            if (inventory.GetItemCount(RoR2.DLC1Content.Items.EquipmentMagazineVoid) == 1 && SurvivorCatalog.FindSurvivorDefFromBody(g.GetComponentInChildren<CharacterBody>().gameObject) == RoR2Content.Survivors.Engi)
                            {
                                ShouldSpeak("WOO, YEAH BABY! THAT'S WHAT I'VE BEEN WAITING FOR! THAT'S WHAT IT'S ALL ABOUT!", true);
                            }
                            break;//Lysate Cell
                        case "ITEM_EXTRALIFEVOID_NAME":
                            dioHeld++;
                            break;//Pluripotent Larva
                        case "ITEM_EXTRALIFEVOIDCONSUMED_NAME":
                            break;//Pluripotent Larva (Consumed)
                        case "ITEM_FRAGILEDAMAGEBONUS_NAME":
                            break;//Delicate Watch
                        case "ITEM_FRAGILEDAMAGEBONUSCONSUMED_NAME":
                            if (inventory.GetItemCount(RoR2.DLC1Content.Items.FragileDamageBonusConsumed) == 1)
                            {
                                ShouldSpeak("Yeaaaaaah... coulda seen that one coming", true);
                            }
                            break;//Delicate Watch (Broken)
                        case "ITEM_OUTOFCOMBATARMOR_NAME":
                            break;//Oddly-shaped Opal
                        case "ITEM_SCRAPWHITESUPPRESSED_NAME":
                            break;//Strange Scrap, White
                        case "ITEM_SCRAPGREENSUPPRESSED_NAME":
                            break;//Strange Scrap, Green
                        case "ITEM_SCRAPREDSUPPRESSED_NAME":
                            break;//Strange Scrap, Red
                        case "ITEM_GOLDONHURT_NAME":
                            break;//Roll of Pennies
                        case "ITEM_RANDOMEQUIPMENTTRIGGER_NAME":
                            break;//Bottled Chaos
                        case "ITEM_FREECHEST_NAME":
                            if (usedGreenRegenScrap)
                            {
                                ShouldSpeak("I used the free items to create the free items", true);
                            }
                            else
                            {
                                ShouldSpeak("Oonga Boonga free items", true);
                            }
                            break;//Shipping Request Form
                        case "ITEM_ELEMENTALRINGVOID_NAME":
                            break;//Singularity Band
                        case "ITEM_LUNARSUN_NAME":
                            break;//Egocentrism
                        case "ITEM_MINORCONSTRUCTONKILL_NAME":
                            break;//Defense Nucleus
                        case "ITEM_VOIDMEGACRABITEM_NAME":
                            break;//Newly Hatched Zoea
                        default:
                            break;
                    }
                }
                if (temp)
                {
                    usedGreenRegenScrap = false;
                }
            }
        }
        public void PlayerDeath(GameObject g, DamageType damageType)
        {
            try
            {
                if (bonziActive)
                {
                    a.SetInteger("idle", -1);
                    a.Play("idle");
                    List<string> deathQuotes = new List<string> { "That really was your fault.", "If you think about it, you just suck.", "That's unfortunate." };
                    if (UnityEngine.Random.Range(0, 1000) == 0)
                    {
                        deathQuotes[2] = "That's unfortnite";
                    }
                    if (Facepunch.Steamworks.Client.Instance.Lobby.GetMemberIDs().Length > 1)
                    {
                        deathQuotes.Add("You should really start carrying your own weight.");
                        deathQuotes.Add("Just blame your teammates 4Head");
                    }
                    RoR2.Inventory inventory = g.GetComponentInChildren<RoR2.CharacterBody>().inventory;
                    if (dioHeld != 0)
                    {
                        deathQuotes.Clear();
                        if (dioUsed == 0)
                        {
                            deathQuotes.Add("Wait don't leave yet!");
                        }
                        else if (dioUsed == 1)
                        {
                            deathQuotes.Add("You know, just because you have them, doesn't mean you have to use them...");
                        }
                        else if (dioUsed == 2)
                        {
                            ShouldSpeak("T t t triple kill"
                                , "Oh baby a triple!");
                            return;
                        }
                        else if (dioUsed == 3)
                        {
                            deathQuotes.Add("Really just chugging these down at this point yeah?");
                        }
                        else if (dioUsed == 4)
                        {
                            deathQuotes.Add("That's 5 deaths now, how are you this bad at the game?");
                        }
                        else if (dioUsed == 5)
                        {
                            if (inventory.GetItemCount(RoR2.DLC1Content.Items.ExtraLifeVoid) != 0)
                            {
                                deathQuotes.Add($"You know, I was thinking to myself earlier and you know what I thought? We need to use more {RoR2.Language.GetString(RoR2.DLC1Content.Items.ExtraLifeVoid.nameToken)}s. So thank you, for using them for me so I don't have to.");
                            }
                            else
                            {
                                deathQuotes.Add($"You know, I was thinking to myself earlier and you know what I thought? We need to use more {RoR2.Language.GetString(RoR2Content.Items.ExtraLife.nameToken)}s. So thank you, for using them for me so I don't have to.");
                            }
                        }
                        else if (dioUsed == 6)
                        {
                            deathQuotes.Add("So that was a bit of a hyperbole earlier. I don't actually think we should consume more of them, so if you could just stop that would be great.");
                        }
                        else if (dioUsed == 7)
                        {
                            deathQuotes.Add("You know what? I give up, I hope you lose this run.");
                        }
                        else if (dioUsed == 68)
                        {
                            deathQuotes.Add("nice.");
                        }
                        else if (dioUsed == 419)
                        {
                            deathQuotes.Add("Blaze it");
                        }
                        else if (dioUsed > 7)
                        {
                            deathQuotes.Add($"{dioUsed + 1}");
                        }
                        ShouldSpeak(deathQuotes[UnityEngine.Random.Range(0, deathQuotes.Count)], true);
                        dioHeld -= 1;
                        dioUsed += 1;
                        return;
                    }
                    if (inventory.GetItemCount(RoR2Content.Items.ExtraLifeConsumed) + inventory.GetItemCount(RoR2.DLC1Content.Items.ExtraLifeVoid) > 7)
                    {
                        ShouldSpeak("good"
                            , "F");
                        return;
                    }
                    if (bloodShrineTimer > 0)
                    {
                        deathQuotes.Add("Maybe next time bring some more healing before you use a blood shrine");
                        deathQuotes.Add("Maybe next time bring some more healing before you use a blood shrine");
                        deathQuotes.Add("Maybe next time bring some more healing before you use a blood shrine");
                    }
                    if (damageType == DamageType.FallDamage)
                    {
                        deathQuotes.Add($"Have you considered playing {RoR2.Language.GetString("LOADER_BODY_NAME")}?");
                        deathQuotes.Add($"Maybe don't jump so far next time.");
                        deathQuotes.Add($"Fall damage is fatal by the way");
                    }
                    if (inventory.GetItemCount(RoR2Content.Items.GhostOnKill) > 0)
                    {
                        deathQuotes.Add($"{RoR2.Language.GetString(RoR2Content.Items.GhostOnKill.nameToken)} really shouldn't be a red item.");
                    }
                    if (inventory.GetItemCount(RoR2Content.Items.Plant) > 0)
                    {
                        deathQuotes.Add($"{RoR2.Language.GetString(RoR2Content.Items.Plant.nameToken)} really shouldn't be a red item.");
                    }
                    if (inventory.GetItemCount(RoR2Content.Items.Clover) == 1)
                    {
                        deathQuotes.Add($"Wow you died with a {RoR2.Language.GetString(RoR2Content.Items.Clover.nameToken)}? You really do suck at this game.");
                    }
                    else if (inventory.GetItemCount(RoR2Content.Items.Clover) > 1)
                    {
                        deathQuotes.Add($"Wow you died with {inventory.GetItemCount(RoR2Content.Items.Clover)} {RoR2.Language.GetString(RoR2Content.Items.Clover.nameToken)}s? You really do suck at this game.");
                    }
                    if (inventory.GetItemCount(RoR2Content.Items.LunarDagger) != 0)
                    {
                        if (Facepunch.Steamworks.Client.Instance.Lobby.GetMemberIDs().Length > 1)
                        {
                            deathQuotes.Add($"You know, when you pickup {RoR2.Language.GetString(RoR2Content.Items.LunarDagger.nameToken)} you are just holding your teamates back.");
                        }
                        deathQuotes.Add($"You probably would have gotten further without {RoR2.Language.GetString(RoR2Content.Items.LunarDagger.nameToken)}.");
                        deathQuotes.Add($"{RoR2.Language.GetString(RoR2Content.Items.LunarDagger.nameToken)} probably wasn't the move there chief...");
                    }
                    //Debug.Log($"--------{inventory.GetItemCount(RoR2Content.Items.LunarBadLuck)}");
                    string theQuote;
                    int num = 0;
                    do
                    {
                        theQuote = deathQuotes[UnityEngine.Random.Range(0, deathQuotes.Count)];
                        num++;
                        if (num == 7)
                        {
                            break;
                        }
                    } while (lastQuotes.Contains(theQuote));
                    lastQuotes.Add(theQuote);
                    if (lastQuotes.Count > 5)
                    {
                        lastQuotes.RemoveAt(0);
                    }
                    ShouldSpeak(theQuote, true);
                }
            }
            catch (Exception e)
            {
                DebugClass.Log(e);
            }
        }
        public void AllyDeath(GameObject g, DamageType damageType)
        {
            string allyName = g.GetComponent<RoR2.CharacterBody>().GetUserName();
            List<string> deathQuotes = new List<string> { $"That really was {allyName}'s fault.", $"{allyName} wants you to know that it's your fault", "Yikes" };
            if (UnityEngine.Random.Range(0, 50) == 0)
            {
                deathQuotes.Add($"It's so sad that {allyName} died of ligma");
            }
            RoR2.Inventory inventory = g.GetComponentInChildren<RoR2.CharacterBody>().inventory;
            if (g.name.StartsWith("Commando"))
            {
                deathQuotes.Add($"Why is {allyName} even using {RoR2.Language.GetString(RoR2Content.Survivors.Commando.displayNameToken)}???");
            }
            else if (g.name.StartsWith("Engi"))
            {
                if (inventory.GetItemCount(RoR2Content.Items.ExtraLife) + inventory.GetItemCount(RoR2.DLC1Content.Items.ExtraLifeVoid) != 0)
                {
                    deathQuotes.Add($"What a waste of a respawn");
                }
                if (RoR2.NetworkUser.readOnlyLocalPlayersList[0].master?.GetBody().inventory.GetItemCount(RoR2Content.Items.Mushroom) != 0 && inventory.GetItemCount(RoR2Content.Items.Mushroom) == 0 && !(RoR2.NetworkUser.readOnlyLocalPlayersList[0].master?.GetBody()).gameObject.name.StartsWith("Engi"))
                {
                    deathQuotes.Add($"{allyName} died because you stole their bungus");
                }
            }
            if (damageType == DamageType.FallDamage)
            {
                deathQuotes.Add($"Maybe {allyName} should play {RoR2.Language.GetString("LOADER_BODY_NAME")} instead.");
                if ((RoR2.NetworkUser.readOnlyLocalPlayersList[0].master?.GetBody()).gameObject.name.StartsWith("Loader") && RoR2.NetworkUser.readOnlyLocalPlayersList[0].master?.GetBody().inventory.GetItemCount(RoR2Content.Items.FallBoots) != 0)
                {
                    deathQuotes.Add($"Why do you even have a {RoR2.Language.GetString(RoR2Content.Items.FallBoots.nameToken)}? {allyName} really could have used it.");
                }
                else if (RoR2.NetworkUser.readOnlyLocalPlayersList[0].master?.GetBody().inventory.GetItemCount(RoR2Content.Items.FallBoots) > 1 && inventory.GetItemCount(RoR2Content.Items.FallBoots) == 0)
                {
                    deathQuotes.Add($"Do you really need {inventory.GetItemCount(RoR2Content.Items.FallBoots)} {RoR2.Language.GetString(RoR2Content.Items.FallBoots.nameToken)}s? {allyName} really could have used one of em.");
                }
            }
            if (inventory.GetItemCount(RoR2Content.Items.Clover) == 1)
            {
                deathQuotes.Add($"Wow, dying with a {RoR2.Language.GetString(RoR2Content.Items.Clover.nameToken)}? You should flame them");
            }
            else if (inventory.GetItemCount(RoR2Content.Items.Clover) > 1)
            {
                deathQuotes.Add($"Wow, dying with {inventory.GetItemCount(RoR2Content.Items.Clover)} {RoR2.Language.GetString(RoR2Content.Items.Clover.nameToken)}s? You should flame them");
            }
            if (inventory.GetItemCount(RoR2Content.Items.Plant) != 0)
            {
                deathQuotes.Add($"{allyName} probably died because of {RoR2.Language.GetString(RoR2Content.Items.Plant.nameToken)}");
            }
            if (inventory.GetItemCount(RoR2Content.Items.LunarDagger) != 0)
            {
                deathQuotes.Add($"Why would you even take {RoR2.Language.GetString(RoR2Content.Items.LunarDagger.nameToken)}");
            }


            string theQuote;
            int num = 0;
            do
            {
                theQuote = deathQuotes[UnityEngine.Random.Range(0, deathQuotes.Count)];
                num++;
                if (num == 7)
                {
                    break;
                }
            } while (lastQuotesAllyDeath.Contains(theQuote));
            lastQuotesAllyDeath.Add(theQuote);
            if (lastQuotesAllyDeath.Count > 5)
            {
                lastQuotesAllyDeath.RemoveAt(0);
            }
            ShouldSpeak(theQuote, true);
            //if (inventory.GetItemCount(ItemIndex.ExtraLife) != 0)
            //{
            //    deathQuotes.Clear();
            //    if (inventory.GetItemCount(ItemIndex.ExtraLifeConsumed) == 0)
            //    {
            //        deathQuotes.Add("Wait don't leave yet!");
            //    }
            //    else if (inventory.GetItemCount(ItemIndex.ExtraLifeConsumed) == 1)
            //    {
            //        deathQuotes.Add("You know, just because you have them, doesn't mean you have to use them...");
            //    }
            //    else if (inventory.GetItemCount(ItemIndex.ExtraLifeConsumed) == 2)
            //    {
            //        ShouldSpeak("T t t triple kill");
            //        return;
            //    }
            //    else if (inventory.GetItemCount(ItemIndex.ExtraLifeConsumed) == 3)
            //    {
            //        deathQuotes.Add("Really just chugging these down at this point yeah?");
            //    }
            //    else if (inventory.GetItemCount(ItemIndex.ExtraLifeConsumed) == 4)
            //    {
            //        deathQuotes.Add("That's 5 deaths now, how are you this bad at the game?");
            //    }
            //    else if (inventory.GetItemCount(ItemIndex.ExtraLifeConsumed) == 5)
            //    {
            //        deathQuotes.Add($"You know, I was thinking to myself earlier and you know what I thought? We need to use more {Language.GetString(ItemCatalog.GetItemDef(ItemIndex.ExtraLife).nameToken)}s. So thank you, for using them for me so I don't have to.");
            //    }
            //    else if (inventory.GetItemCount(ItemIndex.ExtraLifeConsumed) == 6)
            //    {
            //        deathQuotes.Add("So that was a bit of a hyperbole earlier. I don't actually think we should consume more of them, so if you could just stop that would be great.");
            //    }
            //    else if (inventory.GetItemCount(ItemIndex.ExtraLifeConsumed) == 7)
            //    {
            //        deathQuotes.Add("You know what? I give up, I hope you lose this run.");
            //    }
            //    else if (inventory.GetItemCount(ItemIndex.ExtraLifeConsumed) == 68)
            //    {
            //        deathQuotes.Add("nice.");
            //    }
            //    else if (inventory.GetItemCount(ItemIndex.ExtraLifeConsumed) == 419)
            //    {
            //        deathQuotes.Add("Blaze it");
            //    }
            //    else if (inventory.GetItemCount(ItemIndex.ExtraLifeConsumed) > 7)
            //    {
            //        deathQuotes.Add($"{inventory.GetItemCount(ItemIndex.ExtraLifeConsumed) + 1}");
            //    }
            //    ShouldSpeak(deathQuotes[UnityEngine.Random.Range(0, deathQuotes.Count)]);
            //    return;
            //}
            //if (inventory.GetItemCount(ItemIndex.ExtraLifeConsumed) > 7)
            //{
            //    ShouldSpeak("good");
            //    return;
            //}
            //if (inventory.GetItemCount(ItemIndex.GhostOnKill) > 0)
            //{
            //    deathQuotes.Add($"{Language.GetString(ItemCatalog.GetItemDef(ItemIndex.GhostOnKill).nameToken)} really shouldn't be a red item.");
            //}
            //if (inventory.GetItemCount(ItemIndex.Plant) > 0)
            //{
            //    deathQuotes.Add($"{Language.GetString(ItemCatalog.GetItemDef(ItemIndex.Plant).nameToken)} really shouldn't be a red item.");
            //}
            //if (inventory.GetItemCount(ItemIndex.Clover) == 1)
            //{
            //    deathQuotes.Add($"Wow you died with a {Language.GetString(ItemCatalog.GetItemDef(ItemIndex.Clover).nameToken)}? You really do suck at this game.");
            //}
            //else if (inventory.GetItemCount(ItemIndex.Clover) > 1)
            //{
            //    deathQuotes.Add($"Wow you died with {inventory.GetItemCount(ItemIndex.Clover)} {Language.GetString(ItemCatalog.GetItemDef(ItemIndex.Clover).nameToken)}s? You really do suck at this game.");
            //}
            //if (inventory.GetItemCount(ItemIndex.LunarDagger) != 0)
            //{
            //    if (Facepunch.Steamworks.Client.Instance.Lobby.GetMemberIDs().Length > 1)
            //    {
            //        deathQuotes.Add($"You know, when you pickup {Language.GetString(ItemCatalog.GetItemDef(ItemIndex.LunarDagger).nameToken)} you are just holding your teamates back.");
            //    }
            //    deathQuotes.Add($"You probably would have gotten further without {Language.GetString(ItemCatalog.GetItemDef(ItemIndex.LunarDagger).nameToken)}.");
            //    deathQuotes.Add($"{Language.GetString(ItemCatalog.GetItemDef(ItemIndex.LunarDagger).nameToken)} probably wasn't the move there chief...");
            //}
            //Debug.Log($"--------{inventory.GetItemCount(ItemIndex.LunarBadLuck)}");
            //string theQuote;
            //int num = 0;
            //do
            //{
            //    num++;
            //    theQuote = deathQuotes[UnityEngine.Random.Range(0, deathQuotes.Count)];
            //} while (lastQuotes.Contains(theQuote) || num == 7);
            //lastQuotes.Add(theQuote);
            //if (lastQuotes.Count > 5)
            //{
            //    lastQuotes.RemoveAt(0);
            //}
        }
        bool AlmostEqual(float a, float b, float threshold)
        {
            return Math.Abs(a - b) <= threshold;
        }
        const int speed = 2;
        void Update()
        {
            emergencyTimer += Time.unscaledDeltaTime;
            if (casinoTimer > 0)
            {
                casinoTimer -= Time.unscaledDeltaTime;
                if (!(casinoTimer > 0))
                {
                    cheated = false;
                }
            }
            if (dontSpeak > 0)
            {
                dontSpeak -= Time.unscaledDeltaTime;
            }
            if (charPosition != null)
            {
                if (Vector3.Distance(charPosition.position, new Vector3(1105, -283, 1181)) < 35f)
                {
                    if (!Directory.Exists($"{documents}\\My Games"))
                    {
                        DebugClass.Log($"How do you not even have a \"My Games\" folder???? What happened");
                        Directory.CreateDirectory($"{documents}\\My Games");
                    }
                    if (!Directory.Exists($"{documents}\\My Games\\Moisture Upset"))
                    {
                        DebugClass.Log($"Creating Folder");
                        Directory.CreateDirectory($"{documents}\\My Games\\Moisture Upset");
                    }
                    if (!Directory.Exists($"{documents}\\My Games\\Moisture Upset\\data"))
                    {
                        DebugClass.Log($"Creating Folder");
                        Directory.CreateDirectory($"{documents}\\My Games\\Moisture Upset\\data");
                    }
                    if (!Directory.Exists($"{documents}\\My Games\\Moisture Upset\\data\\BonziUnlocked"))
                    {
                        Directory.CreateDirectory($"{documents}\\My Games\\Moisture Upset\\data\\BonziUnlocked");
                    }
                    LocalUserManager.readOnlyLocalUsersList[0].userProfile.GrantUnlockable(UnlockableCatalog.GetUnlockableDef("MOISTURE_BONZIBUDDY_UNLOCKABLE_NAME"));
                    Activate();
                    new SyncBonziApproach(9999, charPosition.gameObject.GetComponentInChildren<NetworkIdentity>().netId).Send(R2API.Networking.NetworkDestination.Clients);
                    charPosition = null;
                }
                if (obj2 && obj2.transform.position == new Vector3(1105, -283, 1181))
                {
                    if (Vector3.Distance(charPosition.position, obj2.transform.position) < 75f)
                    {
                        MoistureUpsetMod.musicController.GetPropertyValue<MusicTrackDef>("currentTrack").Stop();
                        AkSoundEngine.PostEvent("BonziError", obj2);
                        AkSoundEngine.ExecuteActionOnEvent(3605238264, AkActionOnEventType.AkActionOnEventType_Stop);
                        AkSoundEngine.ExecuteActionOnEvent(1901251578, AkActionOnEventType.AkActionOnEventType_Stop);
                        Destroy(obj1);
                        Destroy(obj3);
                        Destroy(obj4);
                        Destroy(obj5);
                        Destroy(obj6);
                        obj2.transform.position = charPosition.position;
                        foreach (var item in Camera.allCameras)
                        {
                            Destroy(item.GetComponent<GlitchEffect>());
                        }
                    }
                    else
                    {
                        float num = Vector3.Distance(charPosition.position, obj2.transform.position) - 75f;
                        if (num > 800)
                        {
                            num = 800;
                        }
                        AkSoundEngine.SetRTPCValue("DistanceToBonzi", num);
                        foreach (var item in Camera.allCameras)
                        {
                            var glitch = item.GetComponent<GlitchEffect>();
                            glitch.intensity = 1 - (num / 800f);
                            if (glitch.intensity > .75f)
                            {
                                glitch.intensity = .75f;
                            }
                            glitch.flipIntensity = 1 - (num / 800f);
                            if (glitch.flipIntensity > .5f)
                            {
                                glitch.flipIntensity = .5f;
                            }
                            glitch.colorIntensity = 1 - (num / 800f);
                        }
                    }
                }//this is poorly organized




                if (obj1 && obj1.transform.position == new Vector3(1811, 351, 719))
                {
                    if (Vector3.Distance(charPosition.position, obj1.transform.position) < 250f)
                    {
                        AkSoundEngine.PostEvent("BonziStartup", obj1);
                        obj1.transform.position = new Vector3(1811, 350, 719);
                    }
                }
            }
            idling = !textBox.activeSelf && !speaking;
            if (firstTime && idling && bonziActive)
            {
                if (bloodShrineTimer > 0)
                {
                    bloodShrineTimer -= Time.unscaledDeltaTime;
                }
                Vector2 temp = RectTransformUtility.WorldToScreenPoint(Camera.current, transform.position);
                screenPos = new Vector2(temp.x / (float)Screen.width, temp.y / (float)Screen.height);
                if (a.GetCurrentAnimatorClipInfo(0).Length != 0)
                {
                    currentClip = a.GetCurrentAnimatorClipInfo(0)[0].clip.name;
                }
                bool equalX = AlmostEqual(dest.x, screenPos.x, .004f);
                bool equalY = AlmostEqual(dest.y, screenPos.y, .004f);
                atDest = equalX && equalY;
                moveDown = moveUp = moveLeft = moveRight = false;
                if (!atDest && currentClip != "entrance" && currentClip != "leave" && !debugging)
                {
                    if (dest.x > screenPos.x && !equalX)
                    {
                        moveRight = true;
                        if (currentClip == "flyright")
                        {
                            transform.position += new Vector3(speed * Time.unscaledDeltaTime * (Screen.width / 1920.0f), 0, 0);
                        }
                    }
                    else if (dest.x < screenPos.x && !equalX)
                    {
                        moveLeft = true;
                        if (currentClip == "flyleft")
                        {
                            transform.position -= new Vector3(speed * Time.unscaledDeltaTime * (Screen.width / 1920.0f), 0, 0);

                        }
                    }
                    else if (dest.y > screenPos.y && !equalY)
                    {
                        moveUp = true;
                        if (currentClip == "flyup")
                        {
                            transform.position += new Vector3(0, speed * Time.unscaledDeltaTime * (Screen.height / 1080.0f), 0);

                        }
                    }
                    else if (dest.y < screenPos.y && !equalY)
                    {
                        moveDown = true;
                        if (currentClip == "flydown")
                        {
                            transform.position -= new Vector3(0, speed * Time.unscaledDeltaTime * (Screen.height / 1080.0f), 0);

                        }
                    }
                }
                //DebugMovement();
                IdleAnimation();

                MovingAnimations();
            }
        }
        public void MainMenuMovement(string location)
        {
            switch (location)
            {
                case "MultiplayerMenu2":
                    GoTo(MULTIPLAYERSETUP);
                    break;
                case "ExtraGameModeMenu":
                    GoTo(ALTGAMEMODES);
                    break;
                case "TitleMenu":
                    GoTo(MAINMENU);
                    break;
                case "MoreMenu":
                    GoTo(MUSICANDMORE);
                    break;
                case "MainSettings":
                    GoTo(SETTINGS);
                    break;
                default:
                    break;
            }
        }
        public bool oncePerStage = true;
        public void Damage(GameObject victim, RoR2.DamageInfo info)
        {
            var playerBody = RoR2.NetworkUser.readOnlyLocalPlayersList[0].master?.GetBody();
            var attackerBody = info.attacker.GetComponentInChildren<RoR2.CharacterBody>();
            var victimBody = victim.GetComponentInChildren<RoR2.CharacterBody>();
            if (victim.GetComponentInChildren<RoR2.HealthComponent>().health <= 0)
            {
                if (attackerBody == playerBody)
                {
                    if (victimBody.isBoss)
                    {
                        if (oncePerStage)
                        {
                            switch (victimBody.baseNameToken)
                            {
                                case "TITAN_BODY_NAME":
                                    if (BigJank.getOptionValue(Settings.RobloxTitan))
                                    {
                                        ShouldSpeak($"ooooooooof", true);
                                    }
                                    break;
                                case "GRAVEKEEPER_BODY_NAME":
                                    if (BigJank.getOptionValue(Settings.Twitch))
                                    {
                                        ShouldSpeak($"Poggers", true);
                                    }
                                    break;
                                default:
                                    //ShouldSpeak("");
                                    break;
                            }
                            oncePerStage = false;
                        }
                    }
                    else if (victimBody.isElite)
                    {
                        if (UnityEngine.Random.Range(0, 75) == 0)
                        {
                            switch (victimBody.baseNameToken)
                            {
                                case "WISP_BODY_NAME":
                                    //ShouldSpeak($"{victimBody.GetDisplayName()}");
                                    break;
                                case "JELLYFISH_BODY_NAME":
                                    if (BigJank.getOptionValue(Settings.Comedy))
                                    {
                                        ShouldSpeak($"I'm something of a comedian myself.", true);
                                    }
                                    break;
                                case "BEETLE_BODY_NAME":
                                    //ShouldSpeak($"{victimBody.GetDisplayName()}");
                                    break;
                                case "LEMURIAN_BODY_NAME":
                                    //ShouldSpeak($"{victimBody.GetDisplayName()}");
                                    break;
                                case "HERMIT_CRAB_BODY_NAME":
                                    //ShouldSpeak($"{victimBody.GetDisplayName()}");
                                    break;
                                case "CLAYBRUISER_BODY_NAME":
                                    //ShouldSpeak($"{victimBody.GetDisplayName()}");
                                    break;
                                case "IMP_BODY_NAME":
                                    //ShouldSpeak($"{victimBody.GetDisplayName()}");
                                    break;
                                case "BELL_BODY_NAME":
                                    if (BigJank.getOptionValue(Settings.TacoBell))
                                    {
                                        ShouldSpeak($"Now I'm feeling kind of hungry.", true);
                                    }
                                    break;
                                case "GOLEM_BODY_NAME":
                                    if (BigJank.getOptionValue(Settings.Robloxian))
                                    {
                                        ShouldSpeak($"oof", true);
                                    }
                                    //ShouldSpeak($"{victimBody.GetDisplayName()}");
                                    break;
                                case "BEETLEGUARD_BODY_NAME":
                                    //ShouldSpeak($"{victimBody.GetDisplayName()}");
                                    break;
                                case "BISON_BODY_NAME":
                                    //ShouldSpeak($"{victimBody.GetDisplayName()}");
                                    break;
                                case "GREATERWISP_BODY_NAME":
                                    //ShouldSpeak($"{victimBody.GetDisplayName()}");
                                    break;
                                case "LEMURIANBRUISER_BODY_NAME":
                                    //ShouldSpeak($"{victimBody.GetDisplayName()}");
                                    break;
                                case "BEETLEQUEEN_BODY_NAME":
                                    //ShouldSpeak($"{victimBody.GetDisplayName()}");
                                    break;
                                case "CLAYBOSS_BODY_NAME":
                                    //ShouldSpeak($"{victimBody.GetDisplayName()}");
                                    break;
                                case "TITAN_BODY_NAME":
                                    if (BigJank.getOptionValue(Settings.RobloxTitan))
                                    {
                                        ShouldSpeak($"ooooooooof", true);
                                    }
                                    break;
                                case "GRAVEKEEPER_BODY_NAME":
                                    if (BigJank.getOptionValue(Settings.Twitch))
                                    {
                                        ShouldSpeak($"Poggers", true);
                                    }
                                    break;
                                case "BROTHER_BODY_NAME":
                                    //ShouldSpeak($"{victimBody.GetDisplayName()}");
                                    break;
                                case "TITANGOLD_BODY_NAME":
                                    //ShouldSpeak($"{victimBody.GetDisplayName()}");
                                    break;
                                case "VAGRANT_BODY_NAME":
                                    //ShouldSpeak($"{victimBody.GetDisplayName()}");
                                    break;
                                case "MAGMAWORM_BODY_NAME":
                                    //ShouldSpeak($"{victimBody.GetDisplayName()}");
                                    break;
                                case "IMPBOSS_BODY_NAME":
                                    //ShouldSpeak($"{victimBody.GetDisplayName()}");
                                    break;
                                case "ELECTRICWORM_BODY_NAME":
                                    //ShouldSpeak($"{victimBody.GetDisplayName()}");
                                    break;
                                case "SUPERROBOBALLBOSS_BODY_NAME":
                                    //ShouldSpeak($"{victimBody.GetDisplayName()}");
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    else if (victimBody.baseNameToken == "SHOPKEEPER_BODY_NAME")
                    {
                        ShouldSpeak("I see we are at that point in the game now", true);
                    }
                }
            }
        }
        public void FallDamage(GameObject victim, RoR2.DamageInfo info)
        {
            if (victim.GetComponentInChildren<RoR2.HealthComponent>().health <= 0)
            {

            }
        }
        float timer = 20f;
        private void IdleAnimation()
        {
            if (currentClip == "idle")
            {
                timer -= Time.unscaledDeltaTime;
                if (timer < 0)
                {
                    timer = UnityEngine.Random.Range(20, 41);
                    //timer = 5;
                    do
                    {
                        idlenum = UnityEngine.Random.Range(0, 15);
                    } while (lastIdle.Contains(idlenum));
                    lastIdle.Add(idlenum);
                    if (lastIdle.Count == 4)
                    {
                        lastIdle.RemoveAt(0);
                    }
                    if (!Settings.AccurateTTS.Value)
                    {
                        if (UnityEngine.Random.Range(0, 150) == 0)
                        {
                            ShouldSpeak("Did you know that in Settings, Mod Settings, Moisture Upset, you can change my tts voice to be the authentic Bonzi Buddy voice!", true);
                            idlenum = -1;
                        }
                    }
                    a.SetInteger("idle", idlenum);
                    switch (idlenum)
                    {
                        //case 11:
                        //    if (!Directory.Exists($"{documents}\\My Games\\Moisture Upset\\data\\BonziUnlocked"))
                        //        ShouldSpeak("You know it isn't that hard to unlock me properly right?"
                        //            ,"Imagine cheating in my achievement");
                        //    else
                        //        ShouldSpeak("Did you know? Me neither...", true);
                        //    break;
                        //case 12:
                        //    if (!Directory.Exists($"{documents}\\My Games\\Moisture Upset\\data\\BonziUnlocked"))
                        //        ShouldSpeak("You know it isn't that hard to unlock me properly right?"
                        //            , "Imagine cheating in my achievement");
                        //    else
                        //        ShouldSpeak("We live in a society", true);
                        //    break;
                        //case 13:
                        //    if (!Directory.Exists($"{documents}\\My Games\\Moisture Upset\\data\\BonziUnlocked"))
                        //        ShouldSpeak("You know it isn't that hard to unlock me properly right?"
                        //            , "Imagine cheating in my achievement");
                        //    else
                        //        ShouldSpeak("Bottom Text", true);
                        //    break;
                        //case 14:
                        //    if (!Directory.Exists($"{documents}\\My Games\\Moisture Upset\\data\\BonziUnlocked"))
                        //        ShouldSpeak("You know it isn't that hard to unlock me properly right?"
                        //            , "Imagine cheating in my achievement");
                        //    else
                        //        ShouldSpeak("Can I ask?........... Thanks that's all.", true);
                        //    break;
                        //default:
                        //    break;
                        case 11:
                            ShouldSpeak("Did you know? Me neither...", true);
                            break;
                        case 12:
                            ShouldSpeak("We live in a society", true);
                            break;
                        case 13:
                            ShouldSpeak("Bottom Text", true);
                            break;
                        case 14:
                            if (UnityEngine.Random.Range(0, 3000) == 69)
                            {
                                ShouldSpeak($"Wake up {Environment.UserName}", true);
                            }
                            else
                            {
                                ShouldSpeak("Can I ask?........... Thanks that's all.", true);
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            else
            {
                a.SetInteger("idle", -1);
            }
        }
        private void MovingAnimations()
        {
            if (currentClip != "entrance" && currentClip != "leave")
            {
                if (prevY > transform.position.y || moveDown)
                {
                    //down
                    if (currentClip != "flydown" && currentClip != "flydownstart")
                    {
                        a.Play("flydownstart");
                    }
                    a.SetBool("moving", true);
                }
                else if (prevY < transform.position.y || moveUp)
                {
                    //up
                    if (currentClip != "flyup" && currentClip != "flyupstart")
                    {
                        a.Play("flyupstart");
                    }
                    a.SetBool("moving", true);
                }
                else if (prevX > transform.position.x || moveLeft)
                {
                    //left
                    if (currentClip != "flyleft" && currentClip != "flyleftstart")
                    {
                        a.Play("flyleftstart");
                    }
                    a.SetBool("moving", true);
                }
                else if (prevX < transform.position.x || moveRight)
                {
                    //right
                    if (currentClip != "flyright" && currentClip != "flyrightstart")
                    {
                        a.Play("flyrightstart");
                    }
                    a.SetBool("moving", true);
                }
                else
                {
                    a.SetBool("moving", false);
                }
                prevX = transform.position.x;
                prevY = transform.position.y;
            }
        }
        private void DebugMovement()
        {
            if (Input.GetKey(KeyCode.I))
            {
                moveUp = true;
                //if (currentClip == "flyup")
                transform.position += new Vector3(0, 1 * Time.unscaledDeltaTime * (Screen.height / 1080.0f), 0);
            }
            if (Input.GetKey(KeyCode.J))
            {
                moveLeft = true;
                //if (currentClip == "flyleft")
                transform.position -= new Vector3(1 * Time.unscaledDeltaTime * (Screen.width / 1920.0f), 0, 0);
            }
            if (Input.GetKey(KeyCode.K))
            {
                moveDown = true;
                //if (currentClip == "flydown")
                transform.position -= new Vector3(0, 1 * Time.unscaledDeltaTime * (Screen.height / 1080.0f), 0);
            }
            if (Input.GetKey(KeyCode.L))
            {
                moveRight = true;
                //if (currentClip == "flyright")
                transform.position += new Vector3(1 * Time.unscaledDeltaTime * (Screen.width / 1920.0f), 0, 0);
            }
            if (Input.GetKeyDown(KeyCode.O))
            {
                DebugClass.Log($"public static Vector2 nameathanNamestar = new Vector2({screenPos.x}f, {screenPos.y}f);");
            }
            if (Input.GetKeyDown(KeyCode.U))
            {
                debugging = !debugging;
            }
            if (Input.GetKeyDown(KeyCode.M))
            {
                ShouldSpeak("This is a test to see where my textbox will be.", true);
            }
        }
        public static void GoTo(float x, float y)
        {
            GoTo(new Vector2(x, y));
        }
        public static void GoTo(Vector2 pos)
        {
            buddy.dest = pos;
        }
        public static void SetActive(bool yeet)
        {
            buddy.StartCoroutine(SetActive(yeet, 0));
        }
        public static IEnumerator SetActive(bool yeet, int yes)
        {
            yield return new WaitUntil(() => buddy.idling || buddy.bonziActive == false);
            if (/*LocalUserManager.readOnlyLocalUsersList[0].userProfile.HasUnlockable("MOISTURE_BONZIBUDDY_UNLOCKABLE_NAME")*/true)
            {
                if (yeet && !buddy.bonziActive)
                {
                    buddy.Activate();
                }
                else if (!yeet && buddy.bonziActive)
                {
                    buddy.Deactivate();
                }
            }
        }
        public void Activate()
        {
            foundMe = true;
            firstTime = false;
            bonziActive = true;
            Setup();
        }
        public void Deactivate()
        {
            bonziActive = false;
            a.Play("leave");
        }
        public void Setup()
        {
            if (foundMe && !firstTime && bonziActive)
            {
                StartCoroutine(StartAnimation());
            }
        }
        public IEnumerator StartAnimation()
        {
            a.Play("entrance");
            //yield return new WaitUntil(() => true);
            yield return new WaitUntil(() => a.GetCurrentAnimatorClipInfo(0).Length != 0);
            yield return new WaitUntil(() => a.GetCurrentAnimatorClipInfo(0)[0].clip.name == "entrance");
            GetComponent<RectTransform>().SetAsLastSibling();
            yield return new WaitUntil(() => a.GetCurrentAnimatorClipInfo(0)[0].clip.name == "idle");
            GoTo(transform.position);
            if (SceneManager.GetActiveScene().name != "moon2")
            {
                switch (UnityEngine.Random.Range(0, 3))
                {
                    case 0:
                        StartCoroutine(Speak($"Hey {username}, good to see you again!"));
                        break;
                    case 1:
                        if (UnityEngine.Random.Range(0, 3000) == 0)
                        {
                            StartCoroutine(Speak($"Welcome back {Environment.UserName}!"));
                        }
                        else
                        {
                            StartCoroutine(Speak($"Welcome back {username}!"));
                        }
                        break;
                    case 2:
                        StartCoroutine(Speak($"The weather is nice out today, isn't it {username}."));
                        break;
                    default:
                        break;
                }
            }
            else
            {
                if (UnityEngine.Random.Range(0, 3000) == 0)
                {
                    StartCoroutine(Speak($"Hey {Environment.UserName}, I'm bonzi buddy! I'll be your personal assistant from now on."));
                }
                else
                {
                    StartCoroutine(Speak($"Hey {username}, I'm bonzi buddy! I'll be your personal assistant from now on."));
                }
            }
            firstTime = true;
            yield return new WaitUntil(() => a.GetCurrentAnimatorClipInfo(0)[0].clip.name == "speaking");
            yield return new WaitUntil(() => a.GetCurrentAnimatorClipInfo(0)[0].clip.name == "idle");
            if (SceneManager.GetActiveScene().name == "title")
            {
                GoTo(MAINMENU);
            }
            else
            {
                GoTo(M1);
            }
        }
        bool twostep = true;
        //public static void ForceRestart(bool ISuck)
        //{
        //    Destroy(buddy.gameObject);

        //    GameObject bonzi = Instantiate(Assets.Load<GameObject>("@MoistureUpset_moisture_bonzibuddy:assets/bonzibuddy/bonzibuddy.prefab"));
        //    DontDestroyOnLoad(bonzi);
        //    bonzi.GetComponent<RectTransform>().SetParent(RoR2Application.instance.mainCanvas.transform, false);
        //    bonzi.SetActive(true);
        //    bonzi.GetComponent<RectTransform>().anchorMin = Vector2.zero;
        //    bonzi.GetComponent<RectTransform>().anchorMax = Vector2.zero;
        //    bonzi.layer = 5;
        //    buddy = bonzi.AddComponent<BonziBuddy>();

        //    buddy.Activate();
        //}
        List<string> words = new List<string>();
        bool sayingWords = false;
        public void ShouldSpeak(string whatToSay, string whatToMLG)
        {
            if (daniel && (int)MLG.progress > 0 && whatToMLG != "")
            {
                words.Add(whatToMLG);
            }
            else if (whatToSay != "")
            {
                words.Add(whatToSay);
            }
            if (!sayingWords)
            {
                sayingWords = true;
                StartCoroutine(ShouldSpeak());
            }
        }
        public void ShouldSpeak(string whatToSay, bool same)
        {
            if (whatToSay != "")
            {

                words.Add(whatToSay);
                if (!sayingWords)
                {
                    sayingWords = true;
                    StartCoroutine(ShouldSpeak());
                }
            }
        }
        public IEnumerator ShouldSpeak()
        {
            if (bonziActive)
            {
                while (words.Count > 0)
                {
                    if (!textBox.activeSelf && !speaking)
                    {
                        a.Play("idle");
                    }
                    yield return new WaitUntil(() => currentClip == "idle" && !textBox.activeSelf && !speaking);
                    StartCoroutine(Speak(words[0]));
                    words.RemoveAt(0);
                }
                sayingWords = false;
            }
        }
        public IEnumerator Speak(string whatToSay)
        {
            textBox.SetActive(true);
            text.text = whatToSay;
            yield return new WaitForSeconds(.1f);
            //this is a really long test 1this is a really long test2this is a really long test3this is a really long test4this is a really long test5this is a really long test6this is a really long test7this is a really long test8 this is a really long test9this is a really long test10
            int num = text.firstOverflowCharacterIndex;
            if (text.isTextOverflowing)
            {
                text.text = whatToSay.Remove(num);
                whatToSay = whatToSay.Remove(0, num);
                twostep = true;
                StartCoroutine(loadsong(text.text));
                yield return new WaitUntil(() => !speaking && !twostep);
                textBox.SetActive(false);
                StartCoroutine(Speak(whatToSay));
            }
            else
            {
                text.text = whatToSay;
                twostep = true;
                StartCoroutine(loadsong(text.text));
                yield return new WaitUntil(() => !speaking && !twostep);
                textBox.SetActive(false);
            }
        }
        public bool isLocked(FileInfo file)
        {
            FileStream fileStream = null;
            try
            {
                fileStream = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            }
            catch (IOException)
            {
                return true;
            }
            finally
            {
                bool flag = fileStream != null;
                if (flag)
                {
                    fileStream.Close();
                }
            }
            return false;
        }
        private static string documents = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
        float emergencyTimer = 0;
        public IEnumerator loadsong(string text)
        {
            if (!speaking)
            {
                string balconPath = $"\"{documents}\\My Games\\Moisture Upset\\data\\balcon.exe\"";
                string joemamaPath = $"\"{documents}\\My Games\\Moisture Upset\\data\\joemama.wav\"";
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo startInfo;
                try
                {
                    if (File.Exists($"{documents}\\My Games\\Moisture Upset\\data\\joemama.wav"))
                    {
                        process = new System.Diagnostics.Process();
                        startInfo = new System.Diagnostics.ProcessStartInfo();
                        startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                        startInfo.FileName = "cmd.exe";
                        startInfo.Arguments = $"/C del {joemamaPath}";
                        process.StartInfo = startInfo;
                        process.Start();
                    }
                }
                catch (Exception e)
                {
                    DebugClass.Log($"Bonzi Buddy failed to speak for reason: {e}");
                    yield break;
                }
                emergencyTimer = 0;
                yield return new WaitUntil(() => !File.Exists($"{documents}\\My Games\\Moisture Upset\\data\\joemama.wav") || emergencyTimer > 10f);
                if (emergencyTimer > 10f)
                {
                    yield break;
                }
                try
                {
                    process = new System.Diagnostics.Process();
                    startInfo = new System.Diagnostics.ProcessStartInfo();
                    startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                    startInfo.FileName = balconPath;
                    string s = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Windows);
                    if (File.Exists(s + "\\Downloaded Installations\\{952F792A-172C-4F2F-88F7-C002F916C583}\\NextUp-ScanSoft Daniel British Voice.msi"))
                    {
                        daniel = true;
                    }
                    if (File.Exists(s + "\\Speech\\speech.dll") && File.Exists(s + "\\lhsp\\help\\tv_enua.hlp"))
                    {
                        sapi4 = true;
                    }
                    if (daniel && (int)MLG.progress > 0 && Settings.AccurateTTS.Value)
                    {
                        startInfo.Arguments = $"-n \"ScanSoft Daniel_Full_22kHz\" -t \"{text}\" -w {joemamaPath}";
                    }
                    else if (sapi4 && Settings.AccurateTTS.Value)
                    {
                        startInfo.Arguments = $"-n Sidney -t \"{text}\" -p 60 -s 140 -w {joemamaPath}";
                    }
                    else
                    {
                        startInfo.Arguments = $"-n \"Microsoft David Desktop\" -t \"{text}\" -p 10 -s \"-2\" -w {joemamaPath}";
                    }
                    process.StartInfo = startInfo;
                    process.Start();
                }
                catch (Exception e)
                {
                    DebugClass.Log($"Bonzi Buddy failed to speak for reason: {e}");
                    yield break;
                }
                emergencyTimer = 0;
                yield return new WaitUntil(() => process.HasExited || emergencyTimer > 10f);
                if (emergencyTimer > 10f)
                {
                    yield break;
                }
                emergencyTimer = 0;
                yield return new WaitUntil(() => File.Exists($"{documents}\\My Games\\Moisture Upset\\data\\joemama.wav") || emergencyTimer > 10f);
                if (emergencyTimer > 10f)
                {
                    yield break;
                }
                FileInfo file = new FileInfo($"{documents}\\My Games\\Moisture Upset\\data\\joemama.wav");
                emergencyTimer = 0;
                yield return new WaitUntil(() => !isLocked(file) || emergencyTimer > 10f);
                if (emergencyTimer > 10f)
                {
                    yield break;
                }
                try
                {
                    if (text == "stop")
                    {
                        speaking = false;
                        a.SetBool("speaking", false);
                        twostep = false;
                        AkSoundEngine.ExecuteActionOnEvent(3183910552, AkActionOnEventType.AkActionOnEventType_Stop);

                    }
                    else
                    {
                        a.Play("speaking");
                        a.SetBool("speaking", true);
                        speaking = true;

                        AkAudioInputManager.PostAudioInputEvent("ttsInput", MoistureUpsetMod.musicController.gameObject, WavBufferToWwise, BeforePlayingAudio);
                    }
                }
                catch (Exception e)
                {
                    DebugClass.Log($"Bonzi Buddy failed to speak for reason: {e}");
                    yield break;
                }
            }
        }
        private static bool speaking = false;
        private uint length = 0;

        float[] left, right;
        public void SetupBalcon()
        {
            if (!Directory.Exists($"{documents}\\My Games"))
            {
                DebugClass.Log($"How do you not even have a \"My Games\" folder???? What happened");
                Directory.CreateDirectory($"{documents}\\My Games");
            }
            if (!Directory.Exists($"{documents}\\My Games\\Moisture Upset"))
            {
                DebugClass.Log($"Creating Folder");
                Directory.CreateDirectory($"{documents}\\My Games\\Moisture Upset");
            }
            if (!Directory.Exists($"{documents}\\My Games\\Moisture Upset\\data"))
            {
                DebugClass.Log($"Creating Folder");
                Directory.CreateDirectory($"{documents}\\My Games\\Moisture Upset\\data");
            }



            if (!File.Exists($"{documents}\\My Games\\Moisture Upset\\data\\balcon.exe"))
            {
                using (var resource = Assembly.GetExecutingAssembly().GetManifestResourceStream("MoistureUpset.Resources.balcon.exe"))
                {
                    using (var file = new FileStream($"{documents}\\My Games\\Moisture Upset\\data\\balcon.exe", FileMode.Create, FileAccess.Write))
                    {
                        resource.CopyTo(file);
                    }
                }
            }
            if (!File.Exists($"{documents}\\My Games\\Moisture Upset\\data\\spchapi.exe"))
            {
                using (var resource = Assembly.GetExecutingAssembly().GetManifestResourceStream("MoistureUpset.Resources.spchapi.exe"))
                {
                    using (var file = new FileStream($"{documents}\\My Games\\Moisture Upset\\data\\spchapi.exe", FileMode.Create, FileAccess.Write))
                    {
                        resource.CopyTo(file);
                    }
                }
            }
            if (!File.Exists($"{documents}\\My Games\\Moisture Upset\\data\\tv_enua.exe"))
            {
                using (var resource = Assembly.GetExecutingAssembly().GetManifestResourceStream("MoistureUpset.Resources.tv_enua.exe"))
                {
                    using (var file = new FileStream($"{documents}\\My Games\\Moisture Upset\\data\\tv_enua.exe", FileMode.Create, FileAccess.Write))
                    {
                        resource.CopyTo(file);
                    }
                }
            }
            if (!File.Exists($"{documents}\\My Games\\Moisture Upset\\data\\Daniel 22Khz MLG voice.exe"))
            {
                using (var resource = Assembly.GetExecutingAssembly().GetManifestResourceStream("MoistureUpset.Resources.Daniel 22Khz MLG voice.exe"))
                {
                    using (var file = new FileStream($"{documents}\\My Games\\Moisture Upset\\data\\Daniel 22Khz MLG voice.exe", FileMode.Create, FileAccess.Write))
                    {
                        resource.CopyTo(file);
                    }
                }
            }
            if (!File.Exists($"{documents}\\My Games\\Moisture Upset\\readme.txt"))
            {
                using (var resource = Assembly.GetExecutingAssembly().GetManifestResourceStream("MoistureUpset.Resources.readme.txt"))
                {
                    using (var file = new FileStream($"{documents}\\My Games\\Moisture Upset\\readme.txt", FileMode.Create, FileAccess.Write))
                    {
                        resource.CopyTo(file);
                    }
                }
            }
            if (!File.Exists($"{documents}\\My Games\\Moisture Upset\\Delet this.bat"))
            {
                using (var resource = Assembly.GetExecutingAssembly().GetManifestResourceStream("MoistureUpset.Resources.Delet this.bat"))
                {
                    using (var file = new FileStream($"{documents}\\My Games\\Moisture Upset\\Delet this.bat", FileMode.Create, FileAccess.Write))
                    {
                        resource.CopyTo(file);
                    }
                }
            }
        }
        public static void FixTTS(bool yeet)
        {
            if (yeet)
            {
                string s = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Windows);
                if ((!File.Exists(s + "\\Speech\\speech.dll") || !File.Exists(s + "\\lhsp\\help\\tv_enua.hlp")) && File.Exists($"{documents}\\My Games\\Moisture Upset\\data\\SAPI4_Installed"))
                {
                    File.Delete($"{documents}\\My Games\\Moisture Upset\\data\\SAPI4_Installed");
                }
                //string s = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Windows);
                if (!File.Exists($"{documents}\\My Games\\Moisture Upset\\data\\SAPI4_Installed"))
                {
                    File.Create($"{documents}\\My Games\\Moisture Upset\\data\\SAPI4_Installed");
                    System.Diagnostics.Process.Start($"{documents}\\My Games\\Moisture Upset\\data\\spchapi.exe");
                    System.Diagnostics.Process.Start($"{documents}\\My Games\\Moisture Upset\\data\\tv_enua.exe");
                }

                if (!File.Exists(s + "\\Downloaded Installations\\{952F792A-172C-4F2F-88F7-C002F916C583}\\NextUp-ScanSoft Daniel British Voice.msi") && File.Exists($"{documents}\\My Games\\Moisture Upset\\data\\Daniel_Installed"))
                {
                    File.Delete($"{documents}\\My Games\\Moisture Upset\\data\\Daniel_Installed");
                }
                //string s = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Windows);
                if (!File.Exists($"{documents}\\My Games\\Moisture Upset\\data\\Daniel_Installed"))
                {
                    File.Create($"{documents}\\My Games\\Moisture Upset\\data\\Daniel_Installed");
                    System.Diagnostics.Process.Start($"{documents}\\My Games\\Moisture Upset\\data\\Daniel 22Khz MLG voice.exe");
                }
            }
        }
        private bool WavBufferToWwise(uint playingID, uint channelIndex, float[] samples)
        {
            if (left.Length <= 0)
            {
                DebugClass.Log("There was an error playing the audio file, The audio buffer is empty!");
            }


            // probably redundant now tbh.
            if (length >= (uint)left.Length)
            {
                length = (uint)left.Length;
            }

            // DebugClass.Log($"Samples: {samples.Length}, Left: {left.Length}, Current: {length}");

            try
            {
                uint i = 0;

                for (i = 0; i < samples.Length; ++i)
                {
                    if (i + length >= left.Length)
                    {
                        speaking = false;
                        a.SetBool("speaking", false);
                        twostep = false;
                        AkSoundEngine.ExecuteActionOnEvent(3183910552, AkActionOnEventType.AkActionOnEventType_Stop);
                        length = 0;
                        left = right = new float[0];
                        break;
                    }
                    samples[i] = left[i + length];
                }
                length += i;
            }
            catch (Exception)
            {
                Debug.Log($"--------end of audio???");
                throw;
            }
            if (length >= (uint)left.Length)
            {
                length = (uint)left.Length;
                speaking = false;
                a.SetBool("speaking", false);
                twostep = false;
            }

            //DebugClass.Log($"id:{playingID}, samples: {samples}, channlIndex: {channelIndex}");

            return speaking;
        }
        private void BeforePlayingAudio(uint playingID, AkAudioFormat format)
        {
            uint samplerate, channels;

            left = right = new float[0];

            readWav($"{documents}\\My Games\\Moisture Upset\\data\\joemama.wav", out left, out right, out samplerate, out channels);

            format.channelConfig.uNumChannels = channels;
            format.uSampleRate = samplerate;
        }


        // Brought to you by StackOverflow, modified by brain damage.
        static bool readWav(string filename, out float[] L, out float[] R, out uint samplerate, out uint channels)
        {
            L = R = null;

            samplerate = 0;
            channels = 0;

            try
            {
                using (FileStream fs = File.Open(filename, FileMode.Open))
                {
                    BinaryReader reader = new BinaryReader(fs);

                    // chunk 0
                    int chunkID = reader.ReadInt32();
                    int fileSize = reader.ReadInt32();
                    int riffType = reader.ReadInt32();


                    // chunk 1
                    int fmtID = reader.ReadInt32();
                    int fmtSize = reader.ReadInt32(); // bytes for this chunk (expect 16 or 18)

                    // 16 bytes coming...
                    int fmtCode = reader.ReadInt16();
                    int Channels = reader.ReadInt16();
                    int sampleRate = reader.ReadInt32();
                    int byteRate = reader.ReadInt32();
                    int fmtBlockAlign = reader.ReadInt16();
                    int bitDepth = reader.ReadInt16();

                    samplerate = (uint)sampleRate;
                    channels = (uint)Channels;

                    if (fmtSize == 18)
                    {
                        // Read any extra values
                        int fmtExtraSize = reader.ReadInt16();
                        reader.ReadBytes(fmtExtraSize);
                    }

                    // chunk 2
                    int dataID = reader.ReadInt32();
                    int bytes = reader.ReadInt32();

                    // DATA!
                    byte[] byteArray = reader.ReadBytes(bytes);

                    int bytesForSamp = bitDepth / 8;
                    int nValues = bytes / bytesForSamp;


                    float[] asFloat = null;
                    switch (bitDepth)
                    {
                        case 64:
                            double[]
                                asDouble = new double[nValues];
                            Buffer.BlockCopy(byteArray, 0, asDouble, 0, bytes);
                            asFloat = Array.ConvertAll(asDouble, e => (float)e);
                            break;
                        case 32:
                            asFloat = new float[nValues];
                            Buffer.BlockCopy(byteArray, 0, asFloat, 0, bytes);
                            break;
                        case 16:
                            Int16[]
                                asInt16 = new Int16[nValues];
                            Buffer.BlockCopy(byteArray, 0, asInt16, 0, bytes);
                            asFloat = Array.ConvertAll(asInt16, e => e / (float)(Int16.MaxValue + 1));
                            break;
                        default:
                            return false;
                    }

                    switch (channels)
                    {
                        case 1:
                            L = asFloat;
                            R = null;
                            return true;
                        case 2:
                            // de-interleave
                            int nSamps = nValues / 2;
                            L = new float[nSamps];
                            R = new float[nSamps];
                            for (int s = 0, v = 0; s < nSamps; s++)
                            {
                                L[s] = asFloat[v++];
                                R[s] = asFloat[v++];
                            }
                            return true;
                        default:
                            return false;
                    }
                }
            }
            catch
            {
                Debug.Log("...Failed to load: " + filename);
                return false;
            }

            return false;
        }
    }
}
