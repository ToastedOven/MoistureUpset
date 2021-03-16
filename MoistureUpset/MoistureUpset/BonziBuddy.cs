using MoistureUpset.NetMessages;
using R2API.Networking.Interfaces;
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
    public class BonziBuddy : MonoBehaviour
    {
        #region defined positions
        public static Vector2 M1 = new Vector2(0.7779733f, 0.2007445f);
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
        bool foundMe = true;
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
            On.RoR2.Inventory.GiveItem += (orig, self, index, count) =>
            {
                try
                {
                    if (self.gameObject && self.gameObject.GetComponentInChildren<CharacterMaster>() && self.gameObject.GetComponentInChildren<CharacterMaster>().GetBody().gameObject)
                    {
                        new SyncItems(self.gameObject.GetComponentInChildren<CharacterMaster>().GetBody().gameObject.GetComponent<NetworkIdentity>().netId, index, count).Send(R2API.Networking.NetworkDestination.Clients);
                    }
                }
                catch (Exception)
                {
                }
                orig(self, index, count);
            };
            On.RoR2.UI.ChatBox.SubmitChat += (orig, self) =>
            {
                if (resetRun && (self.inputField.text.ToUpper() == "YES" || self.inputField.text.ToUpper() == "Y"))
                {
                    //NetworkUser.readOnlyLocalPlayersList[0].master?.GetBody().healthComponent.Suicide();
                    //NetworkUser.readOnlyLocalPlayersList[0].master?.GetBody().gameObject.GetComponent<NetworkIdentity>()
                    new SyncSuicide((NetworkUser.readOnlyLocalPlayersList[0].master?.GetBody().gameObject.GetComponent<NetworkIdentity>()).netId).Send(R2API.Networking.NetworkDestination.Server);
                    resetRun = false;
                }
                else if (resetRun && (self.inputField.text.ToUpper() == "NO" || self.inputField.text.ToUpper() == "N"))
                {
                    ShouldSpeak("Fine, that's your loss");
                    resetRun = false;
                }
                orig(self);
            };
        }
        public bool resetRun = false;
        public void Items(Inventory inventory, ItemIndex index, int count, GameObject g)
        {
            if (inventory.GetTotalItemCountOfTier(ItemTier.Tier3) == 0 && index == ItemIndex.Plant)
            {
                ShouldSpeak($"I see that you have received {Language.GetString(ItemCatalog.GetItemDef(ItemIndex.Plant).nameToken)} as your first red item, would you like me to end the run now? yes or no?");
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
                        ShouldSpeak($"At least it's not {Language.GetString(ItemCatalog.GetItemDef(ItemIndex.Plant).nameToken)}");
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
                        if (Run.instance.stageClearCount + 1 > 5 && inventory.GetItemCount(ItemIndex.ExecuteLowHealthElite) == 0)
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
        public void PlayerDeath(GameObject g)
        {
            try
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
                Inventory inventory = g.GetComponentInChildren<CharacterBody>().inventory;
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
                        deathQuotes.Add($"You know, I was thinking to myself earlier and you know what I thought? We need to use more {Language.GetString(ItemCatalog.GetItemDef(ItemIndex.ExtraLife).nameToken)}s. So thank you, for using them for me so I don't have to.");
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
                if (inventory.GetItemCount(ItemIndex.GhostOnKill) > 0)
                {
                    deathQuotes.Add($"{Language.GetString(ItemCatalog.GetItemDef(ItemIndex.GhostOnKill).nameToken)} really shouldn't be a red item.");
                }
                if (inventory.GetItemCount(ItemIndex.Plant) > 0)
                {
                    deathQuotes.Add($"{Language.GetString(ItemCatalog.GetItemDef(ItemIndex.Plant).nameToken)} really shouldn't be a red item.");
                }
                if (inventory.GetItemCount(ItemIndex.Clover) == 1)
                {
                    deathQuotes.Add($"Wow you died with a {Language.GetString(ItemCatalog.GetItemDef(ItemIndex.Clover).nameToken)}? You really do suck at this game.");
                }
                else if (inventory.GetItemCount(ItemIndex.Clover) > 1)
                {
                    deathQuotes.Add($"Wow you died with {inventory.GetItemCount(ItemIndex.Clover)} {Language.GetString(ItemCatalog.GetItemDef(ItemIndex.Clover).nameToken)}s? You really do suck at this game.");
                }
                if (inventory.GetItemCount(ItemIndex.LunarDagger) != 0)
                {
                    if (Facepunch.Steamworks.Client.Instance.Lobby.GetMemberIDs().Length > 1)
                    {
                        deathQuotes.Add($"You know, when you pickup {Language.GetString(ItemCatalog.GetItemDef(ItemIndex.LunarDagger).nameToken)} you are just holding your teamates back.");
                    }
                    deathQuotes.Add($"You probably would have gotten further without {Language.GetString(ItemCatalog.GetItemDef(ItemIndex.LunarDagger).nameToken)}.");
                    deathQuotes.Add($"{Language.GetString(ItemCatalog.GetItemDef(ItemIndex.LunarDagger).nameToken)} probably wasn't the move there chief...");
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
            catch (Exception e)
            {
                DebugClass.Log(e);
            }
        }
        public void AllyDeath(GameObject g)
        {
            string allyName = g.GetComponent<CharacterBody>().GetUserName();
            List<string> deathQuotes = new List<string> { $"That really was {allyName}'s fault.", $"{allyName} wants you to know that it's your fault" };
            Inventory inventory = g.GetComponentInChildren<CharacterBody>().inventory;
            if (g.name.StartsWith("Commando"))
            {
                deathQuotes.Add($"Why is {allyName} even using {Language.GetString(SurvivorCatalog.GetSurvivorDef(SurvivorIndex.Commando).displayNameToken)}???");
            }
            else if (g.name.StartsWith("Engi"))
            {
                if (inventory.GetItemCount(ItemIndex.ExtraLife) != 0)
                {
                    deathQuotes.Add($"What a waste of a respawn");
                }
                if (NetworkUser.readOnlyLocalPlayersList[0].master?.GetBody().inventory.GetItemCount(ItemIndex.Plant) != 0 && inventory.GetItemCount(ItemIndex.Plant) == 0 && !(NetworkUser.readOnlyLocalPlayersList[0].master?.GetBody()).gameObject.name.StartsWith("Engi"))
                {
                    deathQuotes.Add($"{allyName} died because you stole their bungus");
                }
            }
            if (inventory.GetItemCount(ItemIndex.Clover) == 1)
            {
                deathQuotes.Add($"Wow, dying with a {Language.GetString(ItemCatalog.GetItemDef(ItemIndex.Clover).nameToken)}? You should flame them");
            }
            else if (inventory.GetItemCount(ItemIndex.Clover) > 1)
            {
                deathQuotes.Add($"Wow, dying with {inventory.GetItemCount(ItemIndex.Clover)} {Language.GetString(ItemCatalog.GetItemDef(ItemIndex.Clover).nameToken)}s? You should flame them");
            }
            if (inventory.GetItemCount(ItemIndex.Plant))
            {

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
            idling = !textBox.activeSelf && !speaking;
            if (firstTime && idling)
            {
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
        public void Damage(GameObject victim, DamageInfo info)
        {
            var playerBody = NetworkUser.readOnlyLocalPlayersList[0].master?.GetBody();
            var attackerBody = info.attacker.GetComponentInChildren<CharacterBody>();
            var victimBody = victim.GetComponentInChildren<CharacterBody>();
            if (attackerBody == playerBody)
            {
                if (victim.GetComponentInChildren<HealthComponent>().health <= 0)
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
                        if (UnityEngine.Random.Range(0, 100) == 0)
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
                        idlenum = UnityEngine.Random.Range(0, 14);
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
                            StartCoroutine(Speak("Did you know that in Settings, Mod Settings, Moisture Upset, you can change my tts voice to be the authentic Bonzi Buddy voice!"));
                            idlenum = -1;
                        }
                    }
                    a.SetInteger("idle", idlenum);
                    switch (idlenum)
                    {
                        case 11:
                            StartCoroutine(Speak("Did you know? Me neither..."));
                            break;
                        case 12:
                            StartCoroutine(Speak("We live in a society"));
                            break;
                        case 13:
                            StartCoroutine(Speak("Bottom Text"));
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
                StartCoroutine(Speak("This is a test to see where my textbox will be."));
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
        public void Setup()
        {
            if (foundMe && !firstTime)
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
                    StartCoroutine(Speak($"Hey {username}, good to see you again!"));
                    break;
                case 1:
                    if (UnityEngine.Random.Range(0, 10000) == 0)
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
            yield return new WaitUntil(() => currentClip == "idle" && !textBox.activeSelf && !speaking);
            StartCoroutine(Speak(whatToSay));
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
