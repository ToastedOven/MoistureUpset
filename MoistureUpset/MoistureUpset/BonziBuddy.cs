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
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

namespace MoistureUpset
{
    public class BonziUnlocked : ModdedUnlockableAndAchievement<VanillaSpriteProvider>
    {
        public override string AchievementIdentifier { get; } = "MOISTURE_BONZIBUDDY_ACHIEVEMENT_ID";
        public override string UnlockableIdentifier { get; } = "MOISTURE_BONZIBUDDY_REWARD_ID";
        public override string PrerequisiteUnlockableIdentifier { get; } = "MOISTURE_BONZIBUDDY_PREREQ_ID";
        public override string AchievementNameToken { get; } = "MOISTURE_BONZIBUDDY_ACHIEVEMENT_NAME";
        public override string AchievementDescToken { get; } = "MOISTURE_BONZIBUDDY_ACHIEVEMENT_DESC";
        public override string UnlockableNameToken { get; } = "MOISTURE_BONZIBUDDY_UNLOCKABLE_NAME";
        protected override VanillaSpriteProvider SpriteProvider { get; } = new VanillaSpriteProvider("@MoistureUpset_moisture_bonzistatic:assets/bonzibuddy/gifs/idle/frame_000_delay-0.03s.gif");
        public void ClearCheck(On.RoR2.EscapeSequenceController.EscapeSequenceMainState.orig_Update orig, RoR2.EscapeSequenceController.EscapeSequenceMainState self)
        {
            orig(self);
            Debug.Log($"--------checking it");
            if (BonziBuddy.buddy.foundMe)
            {
                Debug.Log($"--------===============------yeah");
                base.Grant();
            }
        }
        public override void OnInstall()
        {
            base.OnInstall();
            On.RoR2.EscapeSequenceController.EscapeSequenceMainState.Update += ClearCheck;
        }

        public override void OnUninstall()
        {
            base.OnUninstall();
            On.RoR2.EscapeSequenceController.EscapeSequenceMainState.Update -= ClearCheck;
        }
    }
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
        bool activeMountainShrine = false;
        int mountainShrineItems = 0;
        StringBuilder mountainItems = new StringBuilder();
        float bloodShrineTimer = 0;
        Transform charPosition = null;
        GameObject obj1, obj2, obj3, obj4, obj5, obj6;
        GameObject preloaded = Resources.Load<GameObject>("@MoistureUpset_moisture_bonzistatic:assets/bonzibuddy/bonzistatic.prefab");

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
            Hooks();
        }
        private void Hooks()
        {
            On.EntityStates.BrotherMonster.TrueDeathState.OnEnter += (orig, self) =>
            {
                orig(self);
                charPosition = RoR2.NetworkUser.readOnlyLocalPlayersList[0].master?.GetBody().gameObject.transform;

                obj1 = preloaded;
                obj1 = Instantiate(obj1);
                obj1.transform.position = new Vector3(1811, 351, 719);

                obj3 = preloaded;
                obj3 = Instantiate(obj3);
                obj3.transform.position = new Vector3(1958, 340, 722);

                obj4 = preloaded;
                obj4 = Instantiate(obj4);
                obj4.transform.position = new Vector3(2170, 336, 728);

                obj5 = preloaded;
                obj5 = Instantiate(obj5);
                obj5.transform.position = new Vector3(2317, 337, 726);

                obj6 = preloaded;
                obj6 = Instantiate(obj6);
                obj6.transform.position = new Vector3(2432, 248, 724);

                obj2 = new GameObject();
                obj2 = Instantiate(obj2);
                obj2.transform.position = new Vector3(2656, 205, 722);
                AkSoundEngine.PostEvent("BonziGlitches", obj2);
            };
            On.RoR2.UI.MainMenu.MainMenuController.SetDesiredMenuScreen += (orig, self, newscreen) =>
            {
                orig(self, newscreen);
                BonziBuddy.buddy.MainMenuMovement(newscreen.name);
            };
            On.RoR2.Run.OnClientGameOver += (orig, self, report) =>
            {
                orig(self, report);
                try
                {
                    if (report.gameEnding.endingTextToken == "GAME_RESULT_UNKNOWN")
                    {
                        ShouldSpeak("Kind of a cop-out isn't it?");
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
                    resetRun = false;
                    oncePerStage = true;
                    activeMountainShrine = false;
                    switch (newS.name)
                    {
                        case "logbook":
                            GoTo(LOGBOOK);
                            break;
                        case "title":
                            buddy.Setup();
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
                        case "moon":
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
                    charPosition = null;
                    AkSoundEngine.ExecuteActionOnEvent(1901251578, AkActionOnEventType.AkActionOnEventType_Stop);
                }
                catch (Exception)
                {
                }
                orig(oldS, newS);
            };
            On.RoR2.PurchaseInteraction.OnInteractionBegin += (orig, self, i) =>
            {
                if (!self.CanBeAffordedByInteractor(i))
                {
                    new SyncBroke(i.gameObject.GetComponentInChildren<RoR2.CharacterBody>().netId).Send(R2API.Networking.NetworkDestination.Clients);
                }
                orig(self, i);
            };
            On.RoR2.Inventory.GiveItem += (orig, self, index, count) =>
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
                        ShouldSpeak("Fine, that's your loss");
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
                orig(self, activator);
                new SyncShrine(activator.gameObject.GetComponentInChildren<RoR2.CharacterBody>().netId, ShrineType.order).Send(R2API.Networking.NetworkDestination.Clients);
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
                }
                orig(self);
                mountainShrineItems = 0;
                activeMountainShrine = false;
            };
            On.RoR2.PickupDropletController.CreatePickupDroplet += (orig, index, pos, vector) =>
            {
                orig(index, pos, vector);
                if (mountainShrineItems > 0)
                {
                    mountainItems.Append($"{(int)(index.itemIndex)} ");
                    mountainShrineItems--;
                    if (mountainShrineItems == 0)
                    {
                        mountainItems.Remove(mountainItems.Length - 1, 1);
                        new SyncMountain(mountainItems.ToString()).Send(R2API.Networking.NetworkDestination.Clients);
                        mountainItems.Clear();
                    }
                }
            };
        }
        public void Mountain(string[] stringItems)
        {
            int squidCount = 0;
            ItemIndex[] items = new ItemIndex[stringItems.Length];
            for (int i = 0; i < items.Length; i++)
            {
                items[i] = (ItemIndex)(int.Parse(stringItems[i]));
            }
            float goodPercent = 0;
            foreach (var item in items)
            {
                switch (item)
                {
                    case ItemIndex.Missile:
                        goodPercent += 1.0f / (float)items.Length;
                        break;
                    case ItemIndex.ExplodeOnDeath:
                        goodPercent += 0.7f / (float)items.Length;
                        break;
                    case ItemIndex.Feather:
                        goodPercent += 0.7f / (float)items.Length;
                        break;
                    case ItemIndex.ChainLightning:
                        goodPercent += 0.9f / (float)items.Length;
                        break;
                    case ItemIndex.Seed:
                        goodPercent += 0.6f / (float)items.Length;
                        break;
                    case ItemIndex.AttackSpeedOnCrit:
                        goodPercent += 0.85f / (float)items.Length;
                        break;
                    case ItemIndex.SprintOutOfCombat:
                        goodPercent += 0.5f / (float)items.Length;
                        break;
                    case ItemIndex.Phasing:
                        goodPercent += 0.35f / (float)items.Length;
                        break;
                    case ItemIndex.HealOnCrit:
                        goodPercent += 0.85f / (float)items.Length;
                        break;
                    case ItemIndex.EquipmentMagazine:
                        goodPercent += 0.9f / (float)items.Length;
                        break;
                    case ItemIndex.Infusion:
                        goodPercent += 0.3f / (float)items.Length;
                        break;
                    case ItemIndex.Bandolier:
                        goodPercent += 0.1f / (float)items.Length;
                        break;
                    case ItemIndex.WarCryOnMultiKill:
                        goodPercent += 0.5f / (float)items.Length;
                        break;
                    case ItemIndex.Knurl:
                        goodPercent += 0.8f / (float)items.Length;
                        break;
                    case ItemIndex.BeetleGland:
                        goodPercent += 0.5f / (float)items.Length;
                        break;
                    case ItemIndex.SprintArmor:
                        goodPercent += 0.25f / (float)items.Length;
                        break;
                    case ItemIndex.IceRing:
                        goodPercent += 0.85f / (float)items.Length;
                        break;
                    case ItemIndex.FireRing:
                        goodPercent += 0.8f / (float)items.Length;
                        break;
                    case ItemIndex.SlowOnHit:
                        goodPercent += 0.35f / (float)items.Length;
                        break;
                    case ItemIndex.JumpBoost:
                        goodPercent += 0.5f / (float)items.Length;
                        break;
                    case ItemIndex.ExecuteLowHealthElite:
                        goodPercent += 0.95f / (float)items.Length;
                        break;
                    case ItemIndex.EnergizedOnEquipmentUse:
                        goodPercent += 0.45f / (float)items.Length;
                        break;
                    case ItemIndex.SprintWisp:
                        goodPercent += 0.75f / (float)items.Length;
                        break;
                    case ItemIndex.TPHealingNova:
                        //no
                        break;
                    case ItemIndex.Thorns:
                        goodPercent += 0.65f / (float)items.Length;
                        break;
                    case ItemIndex.BonusGoldPackOnKill:
                        goodPercent += 0.4f / (float)items.Length;
                        break;
                    case ItemIndex.NovaOnLowHealth:
                        goodPercent += 0.4f / (float)items.Length;
                        break;
                    case ItemIndex.Squid:
                        squidCount++;
                        break;
                    case ItemIndex.DeathMark:
                        goodPercent += 0.4f / (float)items.Length;
                        break;
                    case ItemIndex.Incubator:
                        break;
                    case ItemIndex.FireballsOnHit:
                        goodPercent += 0.85f / (float)items.Length;
                        break;
                    case ItemIndex.BleedOnHitAndExplode:
                        goodPercent += 1.0f / (float)items.Length;
                        break;
                    case ItemIndex.SiphonOnLowHealth:
                        goodPercent += 0.45f / (float)items.Length;
                        break;
                    default:
                        goodPercent += 0.8f / (float)items.Length;
                        break;
                }
            }
            if (squidCount != 0)
            {
                ShouldSpeak($"You got {squidCount} {RoR2.Language.GetString(RoR2.ItemCatalog.GetItemDef(ItemIndex.Squid).nameToken)}s, nothing else matters");
            }
            else
            {
                List<string> quotes = new List<string>();
                if (goodPercent > .95f)
                {
                    quotes.Add("It can't get any better than this");
                    quotes.Add("Just like the simulations");
                }
                else if (goodPercent > .75f)
                {
                    quotes.Add("Hey... that's pretty good");
                    quotes.Add("Not too shabby");
                }
                else if (goodPercent > .55f)
                {
                    quotes.Add("I'll allow it");
                    quotes.Add("Could have been worse");
                }
                else if (goodPercent > .35f)
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
                ShouldSpeak(quotes[UnityEngine.Random.Range(0, quotes.Count)]);
            }
        }
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
                        ShouldSpeak("Wow");
                        break;
                    case 3:
                        ShouldSpeak("Really?");
                        break;
                    case 4:
                        ShouldSpeak("This has to be rigged... right?");
                        break;
                    case 5:
                        ShouldSpeak("Yeah it's rigged");
                        break;
                    case 6:
                        ShouldSpeak("What did you do to deserve this?");
                        break;
                    case 7:
                        ShouldSpeak("I didn't really think that you would ever make it this far so I kinda ran out of things to say");
                        break;
                    case 8:
                        ShouldSpeak("Maybe I'll just start counting how many times you fail in a row");
                        break;
                    default:
                        ShouldSpeak($"That's {failCount}");
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
                    if (inventory.GetItemCount(ItemIndex.RegenOnKill) == 0 && inventory.GetItemCount(ItemIndex.Mushroom) == 0 && inventory.GetItemCount(ItemIndex.HealWhileSafe) == 0 && inventory.GetItemCount(ItemIndex.Medkit) == 0 && inventory.GetItemCount(ItemIndex.Tooth) == 0 && inventory.GetItemCount(ItemIndex.PersonalShield) < 5 && inventory.GetItemCount(ItemIndex.HealOnCrit) == 0 && inventory.GetItemCount(ItemIndex.Seed) == 0 && inventory.GetItemCount(ItemIndex.Plant) == 0 && inventory.GetItemCount(ItemIndex.Knurl) == 0 && inventory.GetItemCount(ItemIndex.ShieldOnly) == 0 && inventory.GetItemCount(ItemIndex.LunarDagger) == 0)
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
                    activeMountainShrine = true;
                    quotes.Add("Is there really any reason to not hit these?");
                    quotes.Add($"I can't wait to get some extra {RoR2.Language.GetString(RoR2.ItemCatalog.GetItemDef(ItemIndex.NovaOnHeal).nameToken)}s");
                    quotes.Add("It's free real estate");
                    break;
                case ShrineType.healing:
                    quotes.Add("People actually use these?");
                    quotes.Add("Wow! This is garbage. You actually like this?");
                    break;
                case ShrineType.order:
                    if (inventory.GetItemCount(ItemIndex.Clover) - inventory.GetItemCount(ItemIndex.LunarBadLuck) > 0 || (inventory.GetItemCount(ItemIndex.Missile) != 0 && inventory.GetItemCount(ItemIndex.Clover) - inventory.GetItemCount(ItemIndex.LunarBadLuck) >= 0))
                    {
                        quotes.Add("This was so worth it");
                    }
                    if (inventory.GetItemCount(ItemIndex.Clover) - inventory.GetItemCount(ItemIndex.LunarBadLuck) < 0)
                    {
                        quotes.Add("I hope you are ready to never launch another ATG missle");
                    }
                    if (inventory.GetItemCount(ItemIndex.LunarTrinket) > 1)
                    {
                        quotes.Add("Here's hoping you find a lunar pool soon");
                    }
                    if (inventory.GetItemCount(ItemIndex.BleedOnHit) > 7)
                    {
                        quotes.Add($"Do you really need this many {RoR2.Language.GetString("ITEM_BLEEDONHIT_NAME")}s?");
                    }
                    if (inventory.GetItemCount(ItemIndex.Hoof) != 0 || inventory.GetItemCount(ItemIndex.SprintBonus) != 0 || inventory.GetItemCount(ItemIndex.SprintOutOfCombat) != 0)
                    {
                        quotes.Add("Speeeeeeeeeeeed");
                    }
                    if (inventory.GetItemCount(ItemIndex.Mushroom) != 0)
                    {
                        quotes.Add("BUNGUS");
                    }
                    if (inventory.GetItemCount(ItemIndex.RegenOnKill) != 0)
                    {
                        quotes.Add("Wow, I don't think you could have done worse on your white roll");
                    }
                    if (inventory.GetItemCount(ItemIndex.ScrapWhite) != 0 || inventory.GetItemCount(ItemIndex.ScrapRed) != 0 || inventory.GetItemCount(ItemIndex.ScrapGreen) != 0 || inventory.GetItemCount(ItemIndex.ScrapYellow) != 0)
                    {
                        quotes.Add("At least now you just need a good printer or two");
                    }
                    if (inventory.GetItemCount(ItemIndex.CritGlasses) > 10)
                    {
                        quotes.Add("Too much crit! Too much crit!");
                    }
                    if (inventory.GetItemCount(ItemIndex.Bear) != 0)
                    {
                        quotes.Add("You can now block attacks almost as hard as I get blocked on twitter");
                    }
                    if (inventory.GetItemCount(ItemIndex.EquipmentMagazine) != 0 || inventory.GetItemCount(ItemIndex.AutoCastEquipment) != 0)
                    {
                        quotes.Add("Equipment Cooldown Status: None");
                    }
                    if (inventory.GetItemCount(ItemIndex.Feather) != 0)
                    {
                        quotes.Add("Where we're going, we don't need the floor");
                    }
                    if (inventory.GetItemCount(ItemIndex.TPHealingNova) != 0)
                    {
                        quotes.Add($"Waow it's a {RoR2.Language.GetString("ITEM_TPHEALINGNOVA_NAME")} build guys!");
                    }
                    if (inventory.GetItemCount(ItemIndex.Phasing) != 0)
                    {
                        quotes.Add("When you get hit you have a chance to become permanently invisible");
                    }
                    if (inventory.GetItemCount(ItemIndex.Thorns) != 0 && inventory.GetEquipmentIndex() == EquipmentIndex.BurnNearby)
                    {
                        quotes.Add("Oh yeah, it's all coming together");
                    }
                    if (inventory.GetItemCount(ItemIndex.ExplodeOnDeath) != 0)
                    {
                        quotes.Add("Wisp jar go boom");
                    }
                    if (inventory.GetItemCount(ItemIndex.AlienHead) > 1 || inventory.GetItemCount(ItemIndex.KillEliteFrenzy) > 1)
                    {
                        quotes.Add("What is a cooldown?");
                    }
                    if (inventory.GetItemCount(ItemIndex.Behemoth) > 1)
                    {
                        quotes.Add("I don't know if you really needed more than one Behemoth");
                    }
                    if (inventory.GetItemCount(ItemIndex.ExtraLife) > 1)
                    {
                        quotes.Add("At least you won't be able to lose for a while");
                    }
                    if (inventory.GetItemCount(ItemIndex.Plant) > 1)
                    {
                        quotes.Add("Deskplant Pog");
                    }
                    if (inventory.GetItemCount(ItemIndex.ShinyPearl) > 1)
                    {
                        quotes.Add($"Holy crap he got the {RoR2.Language.GetString("ITEM_SHINYPEARL_NAME")}s");
                    }
                    if (inventory.GetItemCount(ItemIndex.LunarDagger) > 1)
                    {
                        quotes.Add("Your health is no more");
                    }
                    if (inventory.GetItemCount(ItemIndex.ShieldOnly) > 1)
                    {
                        quotes.Add("Become the shield");
                    }


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
                ShouldSpeak(quotes[UnityEngine.Random.Range(0, quotes.Count)]);
        }
        public void NotEnoughMoney()
        {
            if (idling)
            {
                if (UnityEngine.Random.Range(0, 5) == 0)
                {
                    string[] quotes = { "Woah there buddy, you don't have enough money for that one.", $"Sorry {username}, I can't give credit. Come back when you're a little hmmmmm, richer.", "hmmmmmmmmmm, no" };
                    ShouldSpeak(quotes[UnityEngine.Random.Range(0, quotes.Length)]);
                }
            }
        }
        public bool resetRun = false;
        public void Items(RoR2.Inventory inventory, ItemIndex index, int count, GameObject g)
        {
            if (inventory.GetTotalItemCountOfTier(ItemTier.Tier3) == 0 && index == ItemIndex.Plant)
            {
                ShouldSpeak($"I see that you have received {RoR2.Language.GetString(RoR2.ItemCatalog.GetItemDef(ItemIndex.Plant).nameToken)} as your first red item, would you like me to end the run now? yes or no?");
                resetRun = true;
            }
            else
            {
                switch (index)
                {
                    case ItemIndex.None:
                        break;
                    case ItemIndex.Syringe:
                        if (inventory.GetItemCount(ItemIndex.Syringe) == 10)
                        {
                            ShouldSpeak("Don't you think you have enough attack speed?");
                        }
                        break;
                    case ItemIndex.Bear:
                        if (inventory.GetItemCount(ItemIndex.Syringe) == 100)
                        {
                            ShouldSpeak("You know stacking them further is almost pointless...");
                        }
                        break;
                    case ItemIndex.Behemoth:
                        ShouldSpeak("Haha rocket launcher go boom");
                        break;
                    case ItemIndex.Missile:
                        //ShouldSpeak("This really pogs my champ");
                        break;
                    case ItemIndex.ExplodeOnDeath:
                        //fire in a jar thing
                        break;
                    case ItemIndex.Dagger:
                        //red dagger

                        ShouldSpeak("muda muda muda muda mudamudamudamudamuda MUDAAAAA!");
                        break;
                    case ItemIndex.Tooth:
                        //monster tooth
                        break;
                    case ItemIndex.CritGlasses:
                        break;
                    case ItemIndex.Hoof:
                        break;
                    case ItemIndex.Feather:
                        break;
                    case ItemIndex.AACannon:
                        //not used
                        break;
                    case ItemIndex.ChainLightning:
                        //uke
                        break;
                    case ItemIndex.PlasmaCore:
                        //not used
                        break;
                    case ItemIndex.Seed:
                        //leeching
                        break;
                    case ItemIndex.Icicle:
                        ShouldSpeak("Wow gee thanks");
                        //frost relic
                        break;
                    case ItemIndex.GhostOnKill:
                        ShouldSpeak($"At least it's not {RoR2.Language.GetString(RoR2.ItemCatalog.GetItemDef(ItemIndex.Plant).nameToken)}");
                        break;
                    case ItemIndex.Mushroom:
                        ShouldSpeak("BUNGUS");
                        //bungus
                        break;
                    case ItemIndex.Crowbar:
                        break;
                    case ItemIndex.LevelBonus:
                        //not used
                        break;
                    case ItemIndex.AttackSpeedOnCrit:
                        break;
                    case ItemIndex.BleedOnHit:
                        if (inventory.GetItemCount(ItemIndex.BleedOnHit) == 6)
                        {
                            ShouldSpeak("Oh yeah, it's gamer time.");
                        }
                        break;
                    case ItemIndex.SprintOutOfCombat:
                        break;
                    case ItemIndex.FallBoots:
                        //the legendary
                        break;
                    case ItemIndex.CooldownOnCrit:
                        //wicked ring not used
                        break;
                    case ItemIndex.WardOnLevel:
                        //warbanner
                        break;
                    case ItemIndex.Phasing:
                        //stealthkit
                        break;
                    case ItemIndex.HealOnCrit:
                        break;
                    case ItemIndex.HealWhileSafe:
                        //slug
                        break;
                    case ItemIndex.TempestOnKill:
                        //not used
                        break;
                    case ItemIndex.PersonalShield:
                        break;
                    case ItemIndex.EquipmentMagazine:
                        //fuel cell
                        break;
                    case ItemIndex.NovaOnHeal:
                        //legendary heal damage thing
                        break;
                    case ItemIndex.ShockNearby:
                        //tesla
                        break;
                    case ItemIndex.Infusion:
                        break;
                    case ItemIndex.WarCryOnCombat:
                        //not used
                        break;
                    case ItemIndex.Clover:
                        if (inventory.GetItemCount(ItemIndex.LunarBadLuck) == 0)
                        {
                            ShouldSpeak("run = won");
                        }
                        else if (inventory.GetItemCount(ItemIndex.LunarBadLuck) == 1)
                        {
                            ShouldSpeak("I bet you are regretting that purity now aren't ya?");
                        }
                        else
                        {
                            ShouldSpeak("I bet you are regretting those purities now aren't ya?");
                        }
                        break;
                    case ItemIndex.Medkit:
                        break;
                    case ItemIndex.Bandolier:
                        break;
                    case ItemIndex.BounceNearby:
                        //meat hook
                        break;
                    case ItemIndex.IgniteOnKill:
                        //gas
                        break;
                    case ItemIndex.PlantOnHit:
                        //not used
                        break;
                    case ItemIndex.StunChanceOnHit:
                        break;
                    case ItemIndex.Firework:
                        break;
                    case ItemIndex.LunarDagger:
                        if (inventory.GetItemCount(ItemIndex.LunarDagger) == 0)
                        {
                            ShouldSpeak("Should you really be doing this?");
                        }
                        else
                        {
                            ShouldSpeak($"Ah whatever, you already have {inventory.GetItemCount(ItemIndex.LunarDagger)}, how much could one more hurt?");
                        }
                        break;
                    case ItemIndex.GoldOnHit:
                        //crown
                        break;
                    case ItemIndex.MageAttunement:
                        //not used
                        break;
                    case ItemIndex.WarCryOnMultiKill:
                        //pauldron
                        break;
                    case ItemIndex.BoostHp:
                        //not used
                        break;
                    case ItemIndex.BoostDamage:
                        //not used
                        break;
                    case ItemIndex.ShieldOnly:
                        ShouldSpeak($"Ah I see you are a gamer of culture.");
                        //trans
                        break;
                    case ItemIndex.AlienHead:
                        break;
                    case ItemIndex.Talisman:
                        //catalyst
                        break;
                    case ItemIndex.Knurl:
                        break;
                    case ItemIndex.BeetleGland:
                        if (BigJank.getOptionValue("Winston") == 1)
                        {
                            ShouldSpeak("Winston please switch");
                        }
                        break;
                    case ItemIndex.BurnNearby:
                        //not used
                        break;
                    case ItemIndex.CritHeal:
                        //not used
                        break;
                    case ItemIndex.CrippleWardOnLevel:
                        //not used
                        break;
                    case ItemIndex.SprintBonus:
                        break;
                    case ItemIndex.SecondarySkillMagazine:
                        break;
                    case ItemIndex.StickyBomb:
                        break;
                    case ItemIndex.TreasureCache:
                        //rusted key
                        break;
                    case ItemIndex.BossDamageBonus:
                        break;
                    case ItemIndex.SprintArmor:
                        break;
                    case ItemIndex.IceRing:
                        break;
                    case ItemIndex.FireRing:
                        break;
                    case ItemIndex.SlowOnHit:
                        break;
                    case ItemIndex.ExtraLife:
                        break;
                    case ItemIndex.ExtraLifeConsumed:
                        break;
                    case ItemIndex.UtilitySkillMagazine:
                        //hardlight
                        break;
                    case ItemIndex.HeadHunter:
                        //vultures
                        break;
                    case ItemIndex.KillEliteFrenzy:
                        //stonks
                        break;
                    case ItemIndex.RepeatHeal:
                        ShouldSpeak("You're going to regret this later");
                        //corpseblossom
                        break;
                    case ItemIndex.Ghost:
                        //not used
                        break;
                    case ItemIndex.HealthDecay:
                        //not used
                        break;
                    case ItemIndex.AutoCastEquipment:
                        if (inventory.GetItemCount(ItemIndex.AutoCastEquipment) + inventory.GetItemCount(ItemIndex.EquipmentMagazine) < 3
                            && inventory.GetItemCount(ItemIndex.Talisman) == 0
                            && inventory.GetEquipmentIndex() == EquipmentIndex.Tonic
                            && inventory.GetItemCount(ItemIndex.Clover) - inventory.GetItemCount(ItemIndex.LunarBadLuck) <= 0)
                        {
                            ShouldSpeak("I hope you enjoy the tonic afflictions");
                        }
                        //gesture
                        break;
                    case ItemIndex.IncreaseHealing:
                        //rejuv rack
                        break;
                    case ItemIndex.JumpBoost:
                        //quail
                        break;
                    case ItemIndex.DrizzlePlayerHelper:
                        //not used
                        break;
                    case ItemIndex.ExecuteLowHealthElite:
                        if (RoR2.Run.instance.stageClearCount + 1 > 5 && inventory.GetItemCount(ItemIndex.ExecuteLowHealthElite) == 0)
                        {
                            ShouldSpeak("Finally");
                        }
                        break;
                    case ItemIndex.EnergizedOnEquipmentUse:
                        //war horn
                        break;
                    case ItemIndex.BarrierOnOverHeal:
                        break;
                    case ItemIndex.TonicAffliction:
                        if (inventory.GetItemCount(ItemIndex.Clover) - inventory.GetItemCount(ItemIndex.LunarBadLuck) < 0)
                        {
                            ShouldSpeak("What are you even thinking!?");
                        }
                        else if (inventory.GetItemCount(ItemIndex.AutoCastEquipment) + inventory.GetItemCount(ItemIndex.EquipmentMagazine) < 4
                            && inventory.GetItemCount(ItemIndex.Talisman) == 0
                            && inventory.GetEquipmentIndex() == EquipmentIndex.Tonic
                            && inventory.GetItemCount(ItemIndex.Clover) - inventory.GetItemCount(ItemIndex.LunarBadLuck) <= 0
                            && inventory.GetItemCount(ItemIndex.AutoCastEquipment) > 0)
                        {
                            ShouldSpeak("You deserve that one");
                        }
                        else
                        {
                            ShouldSpeak("Maybe the tonic life shouldn't be for you?");
                        }
                        break;
                    case ItemIndex.TitanGoldDuringTP:
                        //halcyon seed
                        break;
                    case ItemIndex.SprintWisp:
                        //disciple
                        break;
                    case ItemIndex.BarrierOnKill:
                        break;
                    case ItemIndex.ArmorReductionOnHit:
                        break;
                    case ItemIndex.TPHealingNova:
                        //shiton daisy
                        break;
                    case ItemIndex.NearbyDamageBonus:
                        //focus crystal
                        break;
                    case ItemIndex.LunarUtilityReplacement:
                        //strides
                        break;
                    case ItemIndex.MonsoonPlayerHelper:
                        //not used
                        break;
                    case ItemIndex.Thorns:
                        //razorwire
                        break;
                    case ItemIndex.RegenOnKill:
                        //meat
                        break;
                    case ItemIndex.Pearl:
                        break;
                    case ItemIndex.ShinyPearl:
                        ShouldSpeak("bruh");
                        break;
                    case ItemIndex.BonusGoldPackOnKill:
                        //ghor
                        break;
                    case ItemIndex.LaserTurbine:
                        //res disc
                        break;
                    case ItemIndex.LunarPrimaryReplacement:
                        //visions
                        break;
                    case ItemIndex.NovaOnLowHealth:
                        break;
                    case ItemIndex.LunarTrinket:
                        if (inventory.GetItemCount(ItemIndex.LunarTrinket) == 0)
                        {
                            ShouldSpeak("Ten bucks you only grabbed these to dump them into a pool later");
                        }
                        else
                        {
                            ShouldSpeak("I knew it");
                        }
                        //beads
                        break;
                    case ItemIndex.InvadingDoppelganger:
                        //not used
                        break;
                    case ItemIndex.CutHp:
                        //not used
                        break;
                    case ItemIndex.ArtifactKey:
                        break;
                    case ItemIndex.ArmorPlate:
                        break;
                    case ItemIndex.Squid:
                        break;
                    case ItemIndex.DeathMark:
                        if (inventory.GetItemCount(ItemIndex.DeathMark) == 1)
                        {
                            ShouldSpeak("You do realise stacking these does basically nothing right?");
                        }
                        break;
                    case ItemIndex.Plant:
                        //idp
                        ShouldSpeak("Well well well, if it isn't the best item in the game");
                        break;
                    case ItemIndex.Incubator:
                        //not used
                        break;
                    case ItemIndex.FocusConvergence:
                        break;
                    case ItemIndex.BoostAttackSpeed:
                        //not used
                        break;
                    case ItemIndex.AdaptiveArmor:
                        //not used
                        break;
                    case ItemIndex.CaptainDefenseMatrix:
                        break;
                    case ItemIndex.FireballsOnHit:
                        //perforator
                        break;
                    case ItemIndex.BleedOnHitAndExplode:
                        //spleeeeeen
                        break;
                    case ItemIndex.SiphonOnLowHealth:
                        //urn
                        break;
                    case ItemIndex.MonstersOnShrineUse:
                        break;
                    case ItemIndex.RandomDamageZone:
                        break;
                    case ItemIndex.ScrapWhite:
                        break;
                    case ItemIndex.ScrapGreen:
                        break;
                    case ItemIndex.ScrapRed:
                        ShouldSpeak("Good luck ever finding a printer for this");
                        break;
                    case ItemIndex.ScrapYellow:
                        break;
                    case ItemIndex.LunarBadLuck:
                        if (inventory.GetItemCount(ItemIndex.LunarBadLuck) == 0)
                        {
                            ShouldSpeak("This is almost definitely a bad idea.");
                        }
                        break;
                    case ItemIndex.BoostEquipmentRecharge:
                        //not used
                        break;
                    case ItemIndex.Count:
                        break;
                    default:
                        break;
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
                    if (inventory.GetItemCount(ItemIndex.ExtraLife) != 0)
                    {
                        deathQuotes.Clear();
                        if (inventory.GetItemCount(ItemIndex.ExtraLifeConsumed) == 0)
                        {
                            deathQuotes.Add("Wait don't leave yet!");
                        }
                        else if (inventory.GetItemCount(ItemIndex.ExtraLifeConsumed) == 1)
                        {
                            deathQuotes.Add("You know, just because you have them, doesn't mean you have to use them...");
                        }
                        else if (inventory.GetItemCount(ItemIndex.ExtraLifeConsumed) == 2)
                        {
                            ShouldSpeak("T t t triple kill");
                            return;
                        }
                        else if (inventory.GetItemCount(ItemIndex.ExtraLifeConsumed) == 3)
                        {
                            deathQuotes.Add("Really just chugging these down at this point yeah?");
                        }
                        else if (inventory.GetItemCount(ItemIndex.ExtraLifeConsumed) == 4)
                        {
                            deathQuotes.Add("That's 5 deaths now, how are you this bad at the game?");
                        }
                        else if (inventory.GetItemCount(ItemIndex.ExtraLifeConsumed) == 5)
                        {
                            deathQuotes.Add($"You know, I was thinking to myself earlier and you know what I thought? We need to use more {RoR2.Language.GetString(RoR2.ItemCatalog.GetItemDef(ItemIndex.ExtraLife).nameToken)}s. So thank you, for using them for me so I don't have to.");
                        }
                        else if (inventory.GetItemCount(ItemIndex.ExtraLifeConsumed) == 6)
                        {
                            deathQuotes.Add("So that was a bit of a hyperbole earlier. I don't actually think we should consume more of them, so if you could just stop that would be great.");
                        }
                        else if (inventory.GetItemCount(ItemIndex.ExtraLifeConsumed) == 7)
                        {
                            deathQuotes.Add("You know what? I give up, I hope you lose this run.");
                        }
                        else if (inventory.GetItemCount(ItemIndex.ExtraLifeConsumed) == 68)
                        {
                            deathQuotes.Add("nice.");
                        }
                        else if (inventory.GetItemCount(ItemIndex.ExtraLifeConsumed) == 419)
                        {
                            deathQuotes.Add("Blaze it");
                        }
                        else if (inventory.GetItemCount(ItemIndex.ExtraLifeConsumed) > 7)
                        {
                            deathQuotes.Add($"{inventory.GetItemCount(ItemIndex.ExtraLifeConsumed) + 1}");
                        }
                        ShouldSpeak(deathQuotes[UnityEngine.Random.Range(0, deathQuotes.Count)]);
                        return;
                    }
                    if (inventory.GetItemCount(ItemIndex.ExtraLifeConsumed) > 7)
                    {
                        ShouldSpeak("good");
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
                    if (inventory.GetItemCount(ItemIndex.GhostOnKill) > 0)
                    {
                        deathQuotes.Add($"{RoR2.Language.GetString(RoR2.ItemCatalog.GetItemDef(ItemIndex.GhostOnKill).nameToken)} really shouldn't be a red item.");
                    }
                    if (inventory.GetItemCount(ItemIndex.Plant) > 0)
                    {
                        deathQuotes.Add($"{RoR2.Language.GetString(RoR2.ItemCatalog.GetItemDef(ItemIndex.Plant).nameToken)} really shouldn't be a red item.");
                    }
                    if (inventory.GetItemCount(ItemIndex.Clover) == 1)
                    {
                        deathQuotes.Add($"Wow you died with a {RoR2.Language.GetString(RoR2.ItemCatalog.GetItemDef(ItemIndex.Clover).nameToken)}? You really do suck at this game.");
                    }
                    else if (inventory.GetItemCount(ItemIndex.Clover) > 1)
                    {
                        deathQuotes.Add($"Wow you died with {inventory.GetItemCount(ItemIndex.Clover)} {RoR2.Language.GetString(RoR2.ItemCatalog.GetItemDef(ItemIndex.Clover).nameToken)}s? You really do suck at this game.");
                    }
                    if (inventory.GetItemCount(ItemIndex.LunarDagger) != 0)
                    {
                        if (Facepunch.Steamworks.Client.Instance.Lobby.GetMemberIDs().Length > 1)
                        {
                            deathQuotes.Add($"You know, when you pickup {RoR2.Language.GetString(RoR2.ItemCatalog.GetItemDef(ItemIndex.LunarDagger).nameToken)} you are just holding your teamates back.");
                        }
                        deathQuotes.Add($"You probably would have gotten further without {RoR2.Language.GetString(RoR2.ItemCatalog.GetItemDef(ItemIndex.LunarDagger).nameToken)}.");
                        deathQuotes.Add($"{RoR2.Language.GetString(RoR2.ItemCatalog.GetItemDef(ItemIndex.LunarDagger).nameToken)} probably wasn't the move there chief...");
                    }
                    //Debug.Log($"--------{inventory.GetItemCount(ItemIndex.LunarBadLuck)}");
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
                    ShouldSpeak(theQuote);
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
            List<string> deathQuotes = new List<string> { $"That really was {allyName}'s fault.", $"{allyName} wants you to know that it's your fault" };
            if (UnityEngine.Random.Range(0, 100) == 0)
            {
                deathQuotes.Add($"It's so sad that {allyName} died of ligma");
            }
            RoR2.Inventory inventory = g.GetComponentInChildren<RoR2.CharacterBody>().inventory;
            if (g.name.StartsWith("Commando"))
            {
                deathQuotes.Add($"Why is {allyName} even using {RoR2.Language.GetString(RoR2.SurvivorCatalog.GetSurvivorDef(SurvivorIndex.Commando).displayNameToken)}???");
            }
            else if (g.name.StartsWith("Engi"))
            {
                if (inventory.GetItemCount(ItemIndex.ExtraLife) != 0)
                {
                    deathQuotes.Add($"What a waste of a respawn");
                }
                if (RoR2.NetworkUser.readOnlyLocalPlayersList[0].master?.GetBody().inventory.GetItemCount(ItemIndex.Plant) != 0 && inventory.GetItemCount(ItemIndex.Plant) == 0 && !(RoR2.NetworkUser.readOnlyLocalPlayersList[0].master?.GetBody()).gameObject.name.StartsWith("Engi"))
                {
                    deathQuotes.Add($"{allyName} died because you stole their bungus");
                }
            }
            if (damageType == DamageType.FallDamage)
            {
                deathQuotes.Add($"Maybe {allyName} should play {RoR2.Language.GetString("LOADER_BODY_NAME")} instead.");
                if ((RoR2.NetworkUser.readOnlyLocalPlayersList[0].master?.GetBody()).gameObject.name.StartsWith("Loader") && RoR2.NetworkUser.readOnlyLocalPlayersList[0].master?.GetBody().inventory.GetItemCount(ItemIndex.FallBoots) != 0)
                {
                    deathQuotes.Add($"Why do you even have a {RoR2.Language.GetString(RoR2.ItemCatalog.GetItemDef(ItemIndex.FallBoots).nameToken)}? {allyName} really could have used it.");
                }
                else if (RoR2.NetworkUser.readOnlyLocalPlayersList[0].master?.GetBody().inventory.GetItemCount(ItemIndex.FallBoots) > 1 && inventory.GetItemCount(ItemIndex.FallBoots) == 0)
                {
                    deathQuotes.Add($"Do you really need {inventory.GetItemCount(ItemIndex.FallBoots)} {RoR2.Language.GetString(RoR2.ItemCatalog.GetItemDef(ItemIndex.FallBoots).nameToken)}s? {allyName} really could have used one of em.");
                }
            }
            if (inventory.GetItemCount(ItemIndex.Clover) == 1)
            {
                deathQuotes.Add($"Wow, dying with a {RoR2.Language.GetString(RoR2.ItemCatalog.GetItemDef(ItemIndex.Clover).nameToken)}? You should flame them");
            }
            else if (inventory.GetItemCount(ItemIndex.Clover) > 1)
            {
                deathQuotes.Add($"Wow, dying with {inventory.GetItemCount(ItemIndex.Clover)} {RoR2.Language.GetString(RoR2.ItemCatalog.GetItemDef(ItemIndex.Clover).nameToken)}s? You should flame them");
            }
            if (inventory.GetItemCount(ItemIndex.Plant) != 0)
            {
                deathQuotes.Add($"{allyName} probably died because of {RoR2.Language.GetString(RoR2.ItemCatalog.GetItemDef(ItemIndex.Plant).nameToken)}");
            }
            if (inventory.GetItemCount(ItemIndex.LunarDagger) != 0)
            {
                deathQuotes.Add($"Why would you even take {RoR2.Language.GetString(RoR2.ItemCatalog.GetItemDef(ItemIndex.LunarDagger).nameToken)}");
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
            ShouldSpeak(theQuote);
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
        void Update()
        {
            if (charPosition != null)
            {
                if (Vector3.Distance(charPosition.position, new Vector3(2656,205,722)) < 35f)
                {
                    Activate();
                    charPosition = null;
                }
                if (obj2 && obj2.transform.position == new Vector3(2656, 205, 722))
                {
                    if (Vector3.Distance(charPosition.position, obj2.transform.position) < 75f)
                    {
                        var c = GameObject.FindObjectOfType<MusicController>();
                        c.GetPropertyValue<MusicTrackDef>("currentTrack").Stop();
                        AkSoundEngine.PostEvent("BonziError", obj2);
                        AkSoundEngine.ExecuteActionOnEvent(4159841934, AkActionOnEventType.AkActionOnEventType_Stop);
                        AkSoundEngine.ExecuteActionOnEvent(1901251578, AkActionOnEventType.AkActionOnEventType_Stop);
                        Destroy(obj1);
                        Destroy(obj3);
                        Destroy(obj4);
                        Destroy(obj5);
                        Destroy(obj6);

                        obj2.transform.position = charPosition.position;
                    }
                    else
                    {
                        float num = Vector3.Distance(charPosition.position, obj2.transform.position) - 75f;
                        if (num > 800)
                        {
                            num = 800;
                        }
                        Debug.Log($"--------{num}");
                        AkSoundEngine.SetRTPCValue("DistanceToBonzi", num);
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
                    bloodShrineTimer -= Time.deltaTime;
                }
                Vector2 temp = RectTransformUtility.WorldToScreenPoint(Camera.current, transform.position);
                screenPos = new Vector2(temp.x / (float)Screen.width, temp.y / (float)Screen.height);
                if (a.GetCurrentAnimatorClipInfo(0).Length != 0)
                {
                    currentClip = a.GetCurrentAnimatorClipInfo(0)[0].clip.name;
                }

                bool equalX = AlmostEqual(dest.x, screenPos.x, .0019f);
                bool equalY = AlmostEqual(dest.y, screenPos.y, .0019f);
                atDest = equalX && equalY;
                moveDown = moveUp = moveLeft = moveRight = false;
                if (!atDest && currentClip != "entrance" && currentClip != "leave" && !debugging)
                {
                    if (dest.x > screenPos.x && !equalX)
                    {
                        moveRight = true;
                        if (currentClip == "flyright")
                            transform.position += new Vector3(2 * Time.deltaTime * (Screen.width / 1920.0f), 0, 0);
                    }
                    else if (dest.x < screenPos.x && !equalX)
                    {
                        moveLeft = true;
                        if (currentClip == "flyleft")
                            transform.position -= new Vector3(2 * Time.deltaTime * (Screen.width / 1920.0f), 0, 0);
                    }
                    else if (dest.y > screenPos.y && !equalY)
                    {
                        moveUp = true;
                        if (currentClip == "flyup")
                            transform.position += new Vector3(0, 2 * Time.deltaTime * (Screen.height / 1080.0f), 0);
                    }
                    else if (dest.y < screenPos.y && !equalY)
                    {
                        moveDown = true;
                        if (currentClip == "flydown")
                            transform.position -= new Vector3(0, 2 * Time.deltaTime * (Screen.height / 1080.0f), 0);
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
                                    if (BigJank.getOptionValue("Roblox Titan") == 1)
                                    {
                                        ShouldSpeak($"ooooooooof");
                                    }
                                    break;
                                case "GRAVEKEEPER_BODY_NAME":
                                    if (BigJank.getOptionValue("Twitch") == 1)
                                    {
                                        ShouldSpeak($"Poggers");
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
                                    if (BigJank.getOptionValue("Comedy") == 1)
                                    {
                                        ShouldSpeak($"I'm something of a comedian myself.");
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
                                    if (BigJank.getOptionValue("Taco Bell") == 1)
                                    {
                                        ShouldSpeak($"Now I'm feeling kind of hungry.");
                                    }
                                    break;
                                case "GOLEM_BODY_NAME":
                                    if (BigJank.getOptionValue("Robloxian") == 1)
                                    {
                                        ShouldSpeak($"oof");
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
                                    if (BigJank.getOptionValue("Roblox Titan") == 1)
                                    {
                                        ShouldSpeak($"ooooooooof");
                                    }
                                    break;
                                case "GRAVEKEEPER_BODY_NAME":
                                    if (BigJank.getOptionValue("Twitch") == 1)
                                    {
                                        ShouldSpeak($"Poggers");
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
                        ShouldSpeak("I see we are at that point in the game now");
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
                timer -= Time.deltaTime;
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
                    if (BigJank.getOptionValue("Original Bonzi Buddy TTS") != 1)
                    {
                        if (UnityEngine.Random.Range(0, 150) == 0)
                        {
                            ShouldSpeak("Did you know that in Settings, Mod Settings, Moisture Upset, you can change my tts voice to be the authentic Bonzi Buddy voice!");
                            idlenum = -1;
                        }
                    }
                    a.SetInteger("idle", idlenum);
                    switch (idlenum)
                    {
                        case 11:
                            ShouldSpeak("Did you know? Me neither...");
                            break;
                        case 12:
                            ShouldSpeak("We live in a society");
                            break;
                        case 13:
                            ShouldSpeak("Bottom Text");
                            break;
                        case 14:
                            ShouldSpeak("Can I ask?........... Thanks that's all.");
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
                transform.position += new Vector3(0, 1 * Time.deltaTime * (Screen.height / 1080.0f), 0);
            }
            if (Input.GetKey(KeyCode.J))
            {
                moveLeft = true;
                //if (currentClip == "flyleft")
                transform.position -= new Vector3(1 * Time.deltaTime * (Screen.width / 1920.0f), 0, 0);
            }
            if (Input.GetKey(KeyCode.K))
            {
                moveDown = true;
                //if (currentClip == "flydown")
                transform.position -= new Vector3(0, 1 * Time.deltaTime * (Screen.height / 1080.0f), 0);
            }
            if (Input.GetKey(KeyCode.L))
            {
                moveRight = true;
                //if (currentClip == "flyright")
                transform.position += new Vector3(1 * Time.deltaTime * (Screen.width / 1920.0f), 0, 0);
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
                ShouldSpeak("This is a test to see where my textbox will be.");
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
            switch (UnityEngine.Random.Range(0, 3))
            {
                case 0:
                    ShouldSpeak($"Hey {username}, good to see you again!");
                    break;
                case 1:
                    if (UnityEngine.Random.Range(0, 10000) == 0)
                    {
                        ShouldSpeak($"Welcome back {Environment.UserName}!");
                    }
                    else
                    {
                        ShouldSpeak($"Welcome back {username}!");
                    }
                    break;
                case 2:
                    ShouldSpeak($"The weather is nice out today, isn't it {username}.");
                    break;
                default:
                    break;
            }
            firstTime = true;
            if (SceneManager.GetActiveScene().name == "title")
            {
                GoTo(MAINMENU);
            }
        }
        bool twostep = true;
        public void ShouldSpeak(string whatToSay)
        {
            StartCoroutine(ShouldSpeak(whatToSay, false));
        }
        public IEnumerator ShouldSpeak(string whatToSay, bool bigma)
        {
            if (bonziActive)
            {
                yield return new WaitUntil(() => currentClip == "idle" && !textBox.activeSelf && !speaking);
                StartCoroutine(Speak(whatToSay));
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
        public IEnumerator loadsong(string text)
        {
            if (!speaking)
            {
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                startInfo.FileName = "cmd.exe";
                startInfo.Arguments = $"/C del BepInEx\\plugins\\MetrosexualFruitcake-MoistureUpset\\joemama.wav";
                process.StartInfo = startInfo;
                process.Start();

                yield return new WaitUntil(() => !File.Exists("BepInEx\\plugins\\MetrosexualFruitcake-MoistureUpset\\joemama.wav"));

                process = new System.Diagnostics.Process();
                startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                startInfo.FileName = "cmd.exe";

                if (BigJank.getOptionValue("Original Bonzi Buddy TTS") == 1)
                {
                    startInfo.Arguments = $"/C BepInEx\\plugins\\MetrosexualFruitcake-MoistureUpset\\balcon.exe -n Sidney -t \"{text}\" -p 60 -s 140 -w BepInEx\\plugins\\MetrosexualFruitcake-MoistureUpset\\joemama.wav";
                }
                else
                {
                    startInfo.Arguments = $"/C BepInEx\\plugins\\MetrosexualFruitcake-MoistureUpset\\balcon.exe -n \"Microsoft David Desktop\" -t \"{text}\" -p 10 -s \"-2\" -w BepInEx\\plugins\\MetrosexualFruitcake-MoistureUpset\\joemama.wav";
                }
                process.StartInfo = startInfo;
                process.Start();

                yield return new WaitUntil(() => File.Exists("BepInEx\\plugins\\MetrosexualFruitcake-MoistureUpset\\joemama.wav"));
                FileInfo file = new FileInfo("BepInEx\\plugins\\MetrosexualFruitcake-MoistureUpset\\joemama.wav");
                yield return new WaitUntil(() => !isLocked(file));

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

                    //AkExternalSourceInfo source = new AkExternalSourceInfo();
                    //source.iExternalSrcCookie = AkSoundEngine.GetIDFromString("TestTTSAudio");
                    //source.szFile = "joemama.wav";
                    //source.idCodec = AkSoundEngine.AKCODECID_PCM;

                    //AkSoundEngine.PostEvent("TestTTSAudio", GameObject.FindObjectOfType<GameObject>(), 0, null, null, 1, source);
                    //Debug.Log($"--------postaudioevent");

                    AkAudioInputManager.PostAudioInputEvent("ttsInput", GameObject.FindObjectOfType<GameObject>(), WavBufferToWwise, BeforePlayingAudio);
                }
            }
        }
        private static bool speaking = false;
        private uint length = 0;

        float[] left, right;

        public static void FixTTS(bool yeet)
        {
            if (!yeet)
            {
                string s = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Windows);
                string path = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                if ((!File.Exists(s + "\\Speech\\speech.dll") || !File.Exists(s + "\\lhsp\\help\\tv_enua.hlp")) && File.Exists("SAPI4_Installed"))
                {
                    File.Delete("SAPI4_Installed");
                }
                //string s = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Windows);
                if (!File.Exists("SAPI4_Installed"))
                {
                    File.Create("SAPI4_Installed");
                    System.Diagnostics.Process.Start($"{path}\\MetrosexualFruitcake-MoistureUpset\\spchapi.exe");
                    System.Diagnostics.Process.Start($"{path}\\MetrosexualFruitcake-MoistureUpset\\tv_enua.exe");
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

            readWav("BepInEx\\plugins\\MetrosexualFruitcake-MoistureUpset\\joemama.wav", out left, out right, out samplerate, out channels);

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
