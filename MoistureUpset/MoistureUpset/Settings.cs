using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using BepInEx.Configuration;
using MoistureUpset.NetMessages;
using RiskOfOptions;
using RiskOfOptions.OptionConfigs;
using RiskOfOptions.Options;
using RoR2;
using UnityEngine;

namespace MoistureUpset
{
    public static class Settings
    {
        public static ConfigEntry<bool> OnlySurvivorSkins;
        public static ConfigEntry<float> HitMarkerVolume;
        public static ConfigEntry<float> ModdedMusicVolume;
        public static ConfigEntry<float> ModdedSFXVolume;

        public static ConfigEntry<bool> Dogplane;
        public static ConfigEntry<bool> Comedy;
        public static ConfigEntry<bool> FroggyChair;
        public static ConfigEntry<bool> MikeWazowski;
        public static ConfigEntry<bool> SkeletonCrab;
        public static ConfigEntry<bool> TrumpetSkeleton;
        public static ConfigEntry<bool> LemmeSmash;
        public static ConfigEntry<bool> ObamaPrism;
        public static ConfigEntry<bool> Toad;
        public static ConfigEntry<bool> TacoBell;
        public static ConfigEntry<bool> Winston;
        public static ConfigEntry<bool> Thomas;
        public static ConfigEntry<bool> Robloxian;
        public static ConfigEntry<bool> Heavy;
        public static ConfigEntry<bool> Ghast;
        public static ConfigEntry<bool> Roflcopter;
        public static ConfigEntry<bool> Bowser;
        public static ConfigEntry<bool> Hagrid;
        public static ConfigEntry<bool> Thanos;
        public static ConfigEntry<bool> Rob;
        public static ConfigEntry<bool> CrabRave;
        public static ConfigEntry<bool> NyanCat;
        public static ConfigEntry<bool> GigaPuddi;
        public static ConfigEntry<bool> RobloxTitan;
        public static ConfigEntry<bool> AlexJones;
        public static ConfigEntry<bool> WanderingAtEveryone;
        public static ConfigEntry<bool> PoolNoodle;
        public static ConfigEntry<bool> Twitch;
        public static ConfigEntry<bool> Sans;
        public static ConfigEntry<bool> Imposter;
        public static ConfigEntry<bool> Squirmles;
        public static ConfigEntry<bool> Merchant;
        public static ConfigEntry<bool> Cereal;
        public static ConfigEntry<bool> DQ;
        public static ConfigEntry<bool> Gnome;
        public static ConfigEntry<bool> ChildrenBlocks;
        public static ConfigEntry<bool> AdultBlocks;

        public static ConfigEntry<bool> Interactables;
        public static ConfigEntry<bool> CurrencyChanges;

        public static ConfigEntry<bool> DireSeeker;

        public static ConfigEntry<bool> MinecraftOofSounds;

        public static ConfigEntry<bool> NSFW;
        public static ConfigEntry<bool> Fanfare;
        public static ConfigEntry<bool> PizzaRoll;
        public static ConfigEntry<bool> RobloxCursor;
        public static ConfigEntry<bool> Logo;
        public static ConfigEntry<bool> GenericBossMusic;
        public static ConfigEntry<bool> AwpUI;
        public static ConfigEntry<bool> ChestNoises;
        public static ConfigEntry<bool> PlayerDeathSound;
        public static ConfigEntry<bool> PlayerDeathChat;
        public static ConfigEntry<bool> DifficultyIcons;
        public static ConfigEntry<bool> DifficultyNames;
        public static ConfigEntry<bool> InRunDifficultyNames;
        public static ConfigEntry<bool> MainMenuMusic;
        public static ConfigEntry<bool> ShreksOuthouse;
        public static ConfigEntry<bool> ShrineChanges;
        public static ConfigEntry<bool> MiscOptions;
        public static ConfigEntry<bool> CreativeVoidZone;
        public static ConfigEntry<bool> EndOfGameMusic;
        public static ConfigEntry<bool> RespawnSFX;
        public static ConfigEntry<bool> ReplaceIntroScene;
        public static ConfigEntry<bool> ScaleHitMarkerWithCrit;
        public static ConfigEntry<bool> BonziBuddyBool;
        public static ConfigEntry<bool> AccurateTTS;
        public static ConfigEntry<bool> MLGMode;
        public static ConfigEntry<bool> noDMCA;

        public static ConfigEntry<bool> ReplaceItems;
        public static void RunAll()
        {
            SetupConfig();
            SetupROO();
            PingAll();
            Setup();
            //HitMarker();
            //ModSettingsManager.addStartupListener(new UnityEngine.Events.UnityAction(PingAll));
            //Misc();
            //EnemyOptions();
            //CollabOptions();
            //SoundOptions();
        }
        //Audio
        //Enemy Skins
        //UI Changes
        //Interactables
        //Controls
        //Misc
        internal static void SetupConfig()
        {
            OnlySurvivorSkins = MoistureUpsetMod.Instance.Config.Bind<bool>("Misc", "Only Survivor Skins", false, "Disables everything except survivor skins.");
            HitMarkerVolume = MoistureUpsetMod.Instance.Config.Bind<float>("Audio", "HitMarker Volume", 100.0f, "This sound is also tied to SFX, but has a seperate slider if you want it to be less noisy");
            ModdedMusicVolume = MoistureUpsetMod.Instance.Config.Bind<float>("Audio", "Modded Music Volume", 50.0f, "The default music slider also works for modded music, but this affects modded music only. Incase you want a different audio balance");
            ModdedSFXVolume = MoistureUpsetMod.Instance.Config.Bind<float>("Audio", "Modded SFX Volume", 50.0f, "The default sound slider also works for modded SFX, but this affects modded sfx only. Incase you want a different audio balance");

            ScaleHitMarkerWithCrit = MoistureUpsetMod.Instance.Config.Bind<bool>("Audio", "Scale Hit Marker With Crit", false, "Lowers crit sfx volume as crit chance goes up");

            HitMarkerVolume.Value = Mathf.Clamp(HitMarkerVolume.Value, 0, 100);
            ModdedMusicVolume.Value = Mathf.Clamp(ModdedMusicVolume.Value, 0, 100);
            ModdedSFXVolume.Value = Mathf.Clamp(ModdedSFXVolume.Value, 0, 100);

            HitMarkerVolume.SettingChanged += HitMarkerVolume_SettingChanged;
            ModdedMusicVolume.SettingChanged += ModdedMusicVolume_SettingChanged;
            ModdedSFXVolume.SettingChanged += ModdedSFXVolume_SettingChanged;
            ScaleHitMarkerWithCrit.SettingChanged += ScaleHitMarkerWithCrit_SettingChanged;

            Dogplane = MoistureUpsetMod.Instance.Config.Bind<bool>("Enemy Skins", "Dogplane", true, "Replaces wisps with a dogplanes");
            Comedy = MoistureUpsetMod.Instance.Config.Bind<bool>("Enemy Skins", "Comedy", true, "Replaces jellyfish with an astounding amount of comedy");
            FroggyChair = MoistureUpsetMod.Instance.Config.Bind<bool>("Enemy Skins", "Froggy Chair", true, "Replaces beetles with froggy chairs");
            MikeWazowski = MoistureUpsetMod.Instance.Config.Bind<bool>("Enemy Skins", "Mike Wazowski", true, "Replaces lemurians with mike wazowskis");
            SkeletonCrab = MoistureUpsetMod.Instance.Config.Bind<bool>("Enemy Skins", "Spider Jockey", true, "Replaces hermit crabs with spider jockies");
            TrumpetSkeleton = MoistureUpsetMod.Instance.Config.Bind<bool>("Enemy Skins", "Trumpet Skeleton", true, "Replaces imps with trumpet skeletons");
            LemmeSmash = MoistureUpsetMod.Instance.Config.Bind<bool>("Enemy Skins", "Lemme Smash", true, "please");
            ObamaPrism = MoistureUpsetMod.Instance.Config.Bind<bool>("Enemy Skins", "Obama Prism", true, "Replaces solus units with obamium units");
            Toad = MoistureUpsetMod.Instance.Config.Bind<bool>("Enemy Skins", "Toad", true, "Shoutouts to SimpleFlips");
            TacoBell = MoistureUpsetMod.Instance.Config.Bind<bool>("Enemy Skins", "Taco Bell", true, "Replaces brass contraptions with midroll ads");
            Winston = MoistureUpsetMod.Instance.Config.Bind<bool>("Enemy Skins", "Winston", true, "Replaces beetle guards with enemy team winstons");
            Thomas = MoistureUpsetMod.Instance.Config.Bind<bool>("Enemy Skins", "Thomas", true, "Replaces bighorn bisons with thomas the tank engine");
            Robloxian = MoistureUpsetMod.Instance.Config.Bind<bool>("Enemy Skins", "Robloxian", true, "Replaces stone golems with robloxians");
            Heavy = MoistureUpsetMod.Instance.Config.Bind<bool>("Enemy Skins", "Heavy", true, "Replaces clay templars with tf2heavyweaponsguy");
            Ghast = MoistureUpsetMod.Instance.Config.Bind<bool>("Enemy Skins", "Ghast", true, "Replaces greater wisps with ghasts");
            Roflcopter = MoistureUpsetMod.Instance.Config.Bind<bool>("Enemy Skins", "Roflcopter", true, "Replaces flying lunar chimeras with Roflcopters");
            Bowser = MoistureUpsetMod.Instance.Config.Bind<bool>("Enemy Skins", "Bowser", true, "Replaces elder lemurians with slightly furry bowsers");
            Hagrid = MoistureUpsetMod.Instance.Config.Bind<bool>("Enemy Skins", "Hagrid", true, "Replaces parents with hagrid");
            Thanos = MoistureUpsetMod.Instance.Config.Bind<bool>("Enemy Skins", "Thanos", true, "Replaces mithrix with thanos");
            Rob = MoistureUpsetMod.Instance.Config.Bind<bool>("Enemy Skins", "Rob", true, "Replaces grounded lunar chimeras with Rob");
            CrabRave = MoistureUpsetMod.Instance.Config.Bind<bool>("Enemy Skins", "Crab Rave", true, "The void is raving");
            NyanCat = MoistureUpsetMod.Instance.Config.Bind<bool>("Enemy Skins", "Nyan Cat", true, "Replaces beetle queens with nyan cats");
            GigaPuddi = MoistureUpsetMod.Instance.Config.Bind<bool>("Enemy Skins", "Giga Puddi", true, "PUDDI PUDDI");
            RobloxTitan = MoistureUpsetMod.Instance.Config.Bind<bool>("Enemy Skins", "Roblox Titan", true, "Replaces Stone Titan with a buff robloxian");
            AlexJones = MoistureUpsetMod.Instance.Config.Bind<bool>("Enemy Skins", "Alex Jones", true, "Replaces Aurelionite with Alex Jones");
            WanderingAtEveryone = MoistureUpsetMod.Instance.Config.Bind<bool>("Enemy Skins", "Wandering@Everyone", true, "Replaces wandering vagrants with some @Someone");
            PoolNoodle = MoistureUpsetMod.Instance.Config.Bind<bool>("Enemy Skins", "Pool Noodle", true, "Replaces magma worms with pool noodles");
            Twitch = MoistureUpsetMod.Instance.Config.Bind<bool>("Enemy Skins", "Twitch", true, "Replaces grovetenders with twitch chat");
            Sans = MoistureUpsetMod.Instance.Config.Bind<bool>("Enemy Skins", "Sans", true, "Replaces imp overlords with sans");
            Imposter = MoistureUpsetMod.Instance.Config.Bind<bool>("Enemy Skins", "Imposter", true, "WHEN THE IMPOSTER IS SUS");
            Squirmles = MoistureUpsetMod.Instance.Config.Bind<bool>("Enemy Skins", "Squirmles", true, "Replaces overloading worms with Squirmles");
            Merchant = MoistureUpsetMod.Instance.Config.Bind<bool>("Enemy Skins", "Merchant", true, "Replaces shop keeper with beedle");
            Cereal = MoistureUpsetMod.Instance.Config.Bind<bool>("Enemy Skins", "Cereal", true, "EAT EM UP EAT EM UP EAT EM UP!");
            DQ = MoistureUpsetMod.Instance.Config.Bind<bool>("Enemy Skins", "DQ Lips", true, "Incase you didn't get enough marketing from the brass contraptions");
            Gnome = MoistureUpsetMod.Instance.Config.Bind<bool>("Enemy Skins", "Gnome", true, "I'm gnot a gvoid-infestor, I'm a gnome");
			ChildrenBlocks = MoistureUpsetMod.Instance.Config.Bind<bool>("Enemy Skins", "Children Blocks", true, "Replaces Alpha Constructs with wooden blocks");
            AdultBlocks = MoistureUpsetMod.Instance.Config.Bind<bool>("Enemy Skins", "Adult Blocks", true, "Replaces Xi Constructs with wooden blocks");

            Interactables = MoistureUpsetMod.Instance.Config.Bind<bool>("Interactables", "Interactables", true, "Replaces chests and barrels with minecraft items");
            CurrencyChanges = MoistureUpsetMod.Instance.Config.Bind<bool>("UI Changes", "Currency Changes", true, "Replaces currency types with robux and tix");
            DireSeeker = MoistureUpsetMod.Instance.Config.Bind<bool>("Enemy Skins", "DireSeeker", true, "Replaces Direseeker with Giga Bowser (Requires Direseeker mod)");

            MinecraftOofSounds = MoistureUpsetMod.Instance.Config.Bind<bool>("Audio", "Minecraft Oof Sounds", true, "Adds Minecraft oof sounds whenever you get hurt");
            NSFW = MoistureUpsetMod.Instance.Config.Bind<bool>("Misc", "NSFW", false, "Toggles 'NSFW' content. Not actually NSFW like boobies, just some questionable words if you aren't into that kinda thing");
            Fanfare = MoistureUpsetMod.Instance.Config.Bind<bool>("Audio", "Fanfare", true, "Adds fanfare to the end of the teleporter event");
            PizzaRoll = MoistureUpsetMod.Instance.Config.Bind<bool>("UI Changes", "Pizza Roll", true, "Replaces that diamond UI element with a pizza roll");
            RobloxCursor = MoistureUpsetMod.Instance.Config.Bind<bool>("UI Changes", "Roblox Cursor", true, "Replaces the cursor with a roblox cursor");
            Logo = MoistureUpsetMod.Instance.Config.Bind<bool>("UI Changes", "Logo", true, "Replaces the logo with moisture upset");
            GenericBossMusic = MoistureUpsetMod.Instance.Config.Bind<bool>("Audio", "Generic boss music", true, "Replaces generic boss music (horde of basic enemies) with custom music");
            AwpUI = MoistureUpsetMod.Instance.Config.Bind<bool>("Audio", "Awp UI", true, "Replaces clicks on the UI with awp shots and reloads");
            ChestNoises = MoistureUpsetMod.Instance.Config.Bind<bool>("Audio", "Chest noises", true, "Replaces chest noises");
            PlayerDeathSound = MoistureUpsetMod.Instance.Config.Bind<bool>("Audio", "Player death sound", true, "Replaces player death sound");
            PlayerDeathChat = MoistureUpsetMod.Instance.Config.Bind<bool>("Misc", "Player death chat", true, "Complains about the game in chat so you don't have to");
            DifficultyIcons = MoistureUpsetMod.Instance.Config.Bind<bool>("UI Changes", "Difficulty Icons", true, "Replaces difficulty icons with much more accurate images");
            DifficultyNames = MoistureUpsetMod.Instance.Config.Bind<bool>("UI Changes", "DifficultyNames", true, "Replaces difficulty names with uhh... humor");
            InRunDifficultyNames = MoistureUpsetMod.Instance.Config.Bind<bool>("UI Changes", "In-Run Difficulty Names", true, "AND THEY DON'T STOP COMING");
            MainMenuMusic = MoistureUpsetMod.Instance.Config.Bind<bool>("Audio", "Main menu music", true, "WHATS GOING ON");
            ShreksOuthouse = MoistureUpsetMod.Instance.Config.Bind<bool>("Interactables", "Shreks outhouse", true, "SOMEBODY");
            ShrineChanges = MoistureUpsetMod.Instance.Config.Bind<bool>("Interactables", "Shrine Changes", true, "Very important updates for shrines");
            MiscOptions = MoistureUpsetMod.Instance.Config.Bind<bool>("Misc", "Misc", true, "Random text changes, might fix later");
            CreativeVoidZone = MoistureUpsetMod.Instance.Config.Bind<bool>("Audio", "Creative Void Zone", true, "Adds some entertainment value to the Void Zone");
            EndOfGameMusic = MoistureUpsetMod.Instance.Config.Bind<bool>("Audio", "End of game music", true, "Defeat theme");
            RespawnSFX = MoistureUpsetMod.Instance.Config.Bind<bool>("Audio", "Respawn SFX", true, "Yeah");
            noDMCA = MoistureUpsetMod.Instance.Config.Bind<bool>("Audio", "No DMCA audio", false, "We took out everything we think you can get DMCA'd for (fuck you btw Sony, 009 sound system used to be fine) MLG mode is unaffected");
            ReplaceIntroScene = MoistureUpsetMod.Instance.Config.Bind<bool>("Misc", "Replace Intro Scene", true, "Replaces the default intro cutscene with one that UnsavedTrash made");


            BonziBuddyBool = MoistureUpsetMod.Instance.Config.Bind<bool>("Misc", "Top Secret Setting", true, "You'll probably know it when you see it");
            AccurateTTS = MoistureUpsetMod.Instance.Config.Bind<bool>("Misc", "Accurate REDACTED TTS", false, "Gives REDACTED REDACTED's original TTS voice. For 99% of users, the first time you turn this on it will require an install of SAPI4 and tv_enua(this is where REDACTED's voice is). If you do not feel safe doing this you can either leave this unchecked or manually download and install Speakonia from cfs-technologies on the web.");
            MLGMode = MoistureUpsetMod.Instance.Config.Bind<bool>("Misc", "MLG Mode", false, "What year is it");
            MLGMode.SettingChanged += MLGMode_SettingChanged;

            BonziBuddyBool.SettingChanged += BonziBuddyBool_SettingChanged; ;
            AccurateTTS.SettingChanged += AccurateTTS_SettingChanged; ;

            ReplaceItems = MoistureUpsetMod.Instance.Config.Bind<bool>("Misc", "Replace Items", true, "Replace all items with memes");
        }

        private static void MLGMode_SettingChanged(object sender, EventArgs e)
        {
            if (!MLGMode.Value)
                MLG.TurnOff();
        }

        private static void AccurateTTS_SettingChanged(object sender, EventArgs e)
        {
            BonziBuddy.FixTTS(AccurateTTS.Value);
        }

        private static void BonziBuddyBool_SettingChanged(object sender, EventArgs e)
        {
            BonziBuddy.SetActive(BonziBuddyBool.Value);
        }

        private static void ScaleHitMarkerWithCrit_SettingChanged(object sender, EventArgs e)
        {
            var body = NetworkUser.readOnlyLocalPlayersList[0].master?.GetBody();
            if (body)
            {
                if (BigJank.getOptionValue(Settings.ScaleHitMarkerWithCrit))
                {
                    AkSoundEngine.SetRTPCValue("AirhornAudio", 100 - body.crit);
                }
                else
                {
                    AkSoundEngine.SetRTPCValue("AirhornAudio", 100);
                }
            }
        }

        private static void ModdedSFXVolume_SettingChanged(object sender, EventArgs e)
        {
            AkSoundEngine.SetRTPCValue("Modded_SFX", ModdedSFXVolume.Value);
        }

        private static void ModdedMusicVolume_SettingChanged(object sender, EventArgs e)
        {
            AkSoundEngine.SetRTPCValue("Modded_MSX", ModdedMusicVolume.Value);
        }

        private static void HitMarkerVolume_SettingChanged(object sender, EventArgs e)
        {
            AkSoundEngine.SetRTPCValue("RuneBadNoise", HitMarkerVolume.Value);
        }

        internal static void SetupROO()
        {
            ModSettingsManager.AddOption(new CheckBoxOption(Settings.OnlySurvivorSkins, new CheckBoxConfig() { restartRequired = true }));
            ModSettingsManager.AddOption(new SliderOption(Settings.HitMarkerVolume, new SliderConfig() { checkIfDisabled = CheckOnlySurvivorSkins }));
            ModSettingsManager.AddOption(new SliderOption(Settings.ModdedMusicVolume, new SliderConfig() { checkIfDisabled = CheckOnlySurvivorSkins }));
            ModSettingsManager.AddOption(new SliderOption(Settings.ModdedSFXVolume, new SliderConfig() { checkIfDisabled = CheckOnlySurvivorSkins }));
            ModSettingsManager.AddOption(new CheckBoxOption(Settings.ScaleHitMarkerWithCrit, new CheckBoxConfig() { checkIfDisabled = CheckCritMarkerScaler }));
            ModSettingsManager.AddOption(new CheckBoxOption(Settings.noDMCA, new CheckBoxConfig() { checkIfDisabled = CheckOnlySurvivorSkins, restartRequired = true }));

            ModSettingsManager.AddOption(new CheckBoxOption(Settings.Dogplane, new CheckBoxConfig() { checkIfDisabled = CheckOnlySurvivorSkins, restartRequired = true }));
            ModSettingsManager.AddOption(new CheckBoxOption(Settings.Comedy, new CheckBoxConfig() { checkIfDisabled = CheckOnlySurvivorSkins, restartRequired = true }));
            ModSettingsManager.AddOption(new CheckBoxOption(Settings.FroggyChair, new CheckBoxConfig() { checkIfDisabled = CheckOnlySurvivorSkins, restartRequired = true }));
            ModSettingsManager.AddOption(new CheckBoxOption(Settings.MikeWazowski, new CheckBoxConfig() { checkIfDisabled = CheckOnlySurvivorSkins, restartRequired = true }));
            ModSettingsManager.AddOption(new CheckBoxOption(Settings.SkeletonCrab, new CheckBoxConfig() { checkIfDisabled = CheckOnlySurvivorSkins, restartRequired = true }));
            ModSettingsManager.AddOption(new CheckBoxOption(Settings.TrumpetSkeleton, new CheckBoxConfig() { checkIfDisabled = CheckOnlySurvivorSkins, restartRequired = true }));
            ModSettingsManager.AddOption(new CheckBoxOption(Settings.LemmeSmash, new CheckBoxConfig() { checkIfDisabled = CheckOnlySurvivorSkins, restartRequired = true }));
            ModSettingsManager.AddOption(new CheckBoxOption(Settings.ObamaPrism, new CheckBoxConfig() { checkIfDisabled = CheckOnlySurvivorSkins, restartRequired = true }));
            ModSettingsManager.AddOption(new CheckBoxOption(Settings.Toad, new CheckBoxConfig() { checkIfDisabled = CheckOnlySurvivorSkins, restartRequired = true }));
            ModSettingsManager.AddOption(new CheckBoxOption(Settings.TacoBell, new CheckBoxConfig() { checkIfDisabled = CheckOnlySurvivorSkins, restartRequired = true }));
            ModSettingsManager.AddOption(new CheckBoxOption(Settings.Winston, new CheckBoxConfig() { checkIfDisabled = CheckOnlySurvivorSkins, restartRequired = true }));
            ModSettingsManager.AddOption(new CheckBoxOption(Settings.Thomas, new CheckBoxConfig() { checkIfDisabled = CheckOnlySurvivorSkins, restartRequired = true }));
            ModSettingsManager.AddOption(new CheckBoxOption(Settings.Robloxian, new CheckBoxConfig() { checkIfDisabled = CheckOnlySurvivorSkins, restartRequired = true }));
            ModSettingsManager.AddOption(new CheckBoxOption(Settings.Heavy, new CheckBoxConfig() { checkIfDisabled = CheckOnlySurvivorSkins, restartRequired = true }));
            ModSettingsManager.AddOption(new CheckBoxOption(Settings.Ghast, new CheckBoxConfig() { checkIfDisabled = CheckOnlySurvivorSkins, restartRequired = true }));
            ModSettingsManager.AddOption(new CheckBoxOption(Settings.Roflcopter, new CheckBoxConfig() { checkIfDisabled = CheckOnlySurvivorSkins, restartRequired = true }));
            ModSettingsManager.AddOption(new CheckBoxOption(Settings.Bowser, new CheckBoxConfig() { checkIfDisabled = CheckOnlySurvivorSkins, restartRequired = true }));
            ModSettingsManager.AddOption(new CheckBoxOption(Settings.Hagrid, new CheckBoxConfig() { checkIfDisabled = CheckOnlySurvivorSkins, restartRequired = true }));
            ModSettingsManager.AddOption(new CheckBoxOption(Settings.Thanos, new CheckBoxConfig() { checkIfDisabled = CheckOnlySurvivorSkins, restartRequired = true }));
            ModSettingsManager.AddOption(new CheckBoxOption(Settings.Rob, new CheckBoxConfig() { checkIfDisabled = CheckOnlySurvivorSkins, restartRequired = true }));
            ModSettingsManager.AddOption(new CheckBoxOption(Settings.CrabRave, new CheckBoxConfig() { checkIfDisabled = CheckOnlySurvivorSkins, restartRequired = true }));
            ModSettingsManager.AddOption(new CheckBoxOption(Settings.NyanCat, new CheckBoxConfig() { checkIfDisabled = CheckOnlySurvivorSkins, restartRequired = true }));
            ModSettingsManager.AddOption(new CheckBoxOption(Settings.GigaPuddi, new CheckBoxConfig() { checkIfDisabled = CheckOnlySurvivorSkins, restartRequired = true }));
            ModSettingsManager.AddOption(new CheckBoxOption(Settings.RobloxTitan, new CheckBoxConfig() { checkIfDisabled = CheckOnlySurvivorSkins, restartRequired = true }));
            ModSettingsManager.AddOption(new CheckBoxOption(Settings.AlexJones, new CheckBoxConfig() { checkIfDisabled = CheckOnlySurvivorSkins, restartRequired = true }));
            ModSettingsManager.AddOption(new CheckBoxOption(Settings.WanderingAtEveryone, new CheckBoxConfig() { checkIfDisabled = CheckOnlySurvivorSkins, restartRequired = true }));
            ModSettingsManager.AddOption(new CheckBoxOption(Settings.PoolNoodle, new CheckBoxConfig() { checkIfDisabled = CheckOnlySurvivorSkins, restartRequired = true }));
            ModSettingsManager.AddOption(new CheckBoxOption(Settings.Twitch, new CheckBoxConfig() { checkIfDisabled = CheckOnlySurvivorSkins, restartRequired = true }));
            ModSettingsManager.AddOption(new CheckBoxOption(Settings.Sans, new CheckBoxConfig() { checkIfDisabled = CheckOnlySurvivorSkins, restartRequired = true }));
            ModSettingsManager.AddOption(new CheckBoxOption(Settings.Imposter, new CheckBoxConfig() { checkIfDisabled = CheckOnlySurvivorSkins, restartRequired = true }));
            ModSettingsManager.AddOption(new CheckBoxOption(Settings.Squirmles, new CheckBoxConfig() { checkIfDisabled = CheckOnlySurvivorSkins, restartRequired = true }));
            ModSettingsManager.AddOption(new CheckBoxOption(Settings.Merchant, new CheckBoxConfig() { checkIfDisabled = CheckOnlySurvivorSkins, restartRequired = true }));
            ModSettingsManager.AddOption(new CheckBoxOption(Settings.Cereal, new CheckBoxConfig() { checkIfDisabled = CheckOnlySurvivorSkins, restartRequired = true }));
            ModSettingsManager.AddOption(new CheckBoxOption(Settings.DQ, new CheckBoxConfig() { checkIfDisabled = CheckOnlySurvivorSkins, restartRequired = true }));
            ModSettingsManager.AddOption(new CheckBoxOption(Settings.Gnome, new CheckBoxConfig() { checkIfDisabled = CheckOnlySurvivorSkins, restartRequired = true }));
            ModSettingsManager.AddOption(new CheckBoxOption(Settings.ChildrenBlocks, new CheckBoxConfig() { checkIfDisabled = CheckOnlySurvivorSkins, restartRequired = true }));
            ModSettingsManager.AddOption(new CheckBoxOption(Settings.AdultBlocks, new CheckBoxConfig() { checkIfDisabled = CheckOnlySurvivorSkins, restartRequired = true }));

            ModSettingsManager.AddOption(new CheckBoxOption(Settings.Interactables, new CheckBoxConfig() { checkIfDisabled = CheckOnlySurvivorSkins, restartRequired = true }));
            ModSettingsManager.AddOption(new CheckBoxOption(Settings.CurrencyChanges, new CheckBoxConfig() { checkIfDisabled = CheckOnlySurvivorSkins, restartRequired = true }));

            ModSettingsManager.AddOption(new CheckBoxOption(Settings.DireSeeker, new CheckBoxConfig() { checkIfDisabled = CheckOnlySurvivorSkins, restartRequired = true }));

            ModSettingsManager.AddOption(new CheckBoxOption(Settings.MinecraftOofSounds, new CheckBoxConfig() { checkIfDisabled = CheckOnlySurvivorSkins, restartRequired = true }));

            ModSettingsManager.AddOption(new CheckBoxOption(Settings.NSFW, new CheckBoxConfig() { checkIfDisabled = CheckOnlySurvivorSkins, restartRequired = true }));
            ModSettingsManager.AddOption(new CheckBoxOption(Settings.Fanfare, new CheckBoxConfig() { checkIfDisabled = CheckOnlySurvivorSkins, restartRequired = true }));
            ModSettingsManager.AddOption(new CheckBoxOption(Settings.PizzaRoll, new CheckBoxConfig() { checkIfDisabled = CheckOnlySurvivorSkins, restartRequired = true }));
            ModSettingsManager.AddOption(new CheckBoxOption(Settings.RobloxCursor, new CheckBoxConfig() { checkIfDisabled = CheckOnlySurvivorSkins, restartRequired = true }));
            ModSettingsManager.AddOption(new CheckBoxOption(Settings.Logo, new CheckBoxConfig() { checkIfDisabled = CheckOnlySurvivorSkins, restartRequired = true }));
            ModSettingsManager.AddOption(new CheckBoxOption(Settings.GenericBossMusic, new CheckBoxConfig() { checkIfDisabled = CheckOnlySurvivorSkins, restartRequired = true }));
            ModSettingsManager.AddOption(new CheckBoxOption(Settings.AwpUI, new CheckBoxConfig() { checkIfDisabled = CheckOnlySurvivorSkins, restartRequired = true }));
            ModSettingsManager.AddOption(new CheckBoxOption(Settings.ChestNoises, new CheckBoxConfig() { checkIfDisabled = CheckOnlySurvivorSkins, restartRequired = true }));
            ModSettingsManager.AddOption(new CheckBoxOption(Settings.PlayerDeathSound, new CheckBoxConfig() { checkIfDisabled = CheckOnlySurvivorSkins, restartRequired = true }));
            ModSettingsManager.AddOption(new CheckBoxOption(Settings.PlayerDeathChat, new CheckBoxConfig() { checkIfDisabled = CheckOnlySurvivorSkins, restartRequired = true }));
            ModSettingsManager.AddOption(new CheckBoxOption(Settings.DifficultyIcons, new CheckBoxConfig() { checkIfDisabled = CheckOnlySurvivorSkins, restartRequired = true }));
            ModSettingsManager.AddOption(new CheckBoxOption(Settings.DifficultyNames, new CheckBoxConfig() { checkIfDisabled = CheckOnlySurvivorSkins, restartRequired = true }));
            ModSettingsManager.AddOption(new CheckBoxOption(Settings.InRunDifficultyNames, new CheckBoxConfig() { checkIfDisabled = CheckOnlySurvivorSkins, restartRequired = true }));
            ModSettingsManager.AddOption(new CheckBoxOption(Settings.MainMenuMusic, new CheckBoxConfig() { checkIfDisabled = CheckOnlySurvivorSkins, restartRequired = true }));
            ModSettingsManager.AddOption(new CheckBoxOption(Settings.ShreksOuthouse, new CheckBoxConfig() { checkIfDisabled = CheckOnlySurvivorSkins, restartRequired = true }));
            ModSettingsManager.AddOption(new CheckBoxOption(Settings.ShrineChanges, new CheckBoxConfig() { checkIfDisabled = CheckOnlySurvivorSkins, restartRequired = true }));
            ModSettingsManager.AddOption(new CheckBoxOption(Settings.MiscOptions, new CheckBoxConfig() { checkIfDisabled = CheckOnlySurvivorSkins, restartRequired = true }));
            ModSettingsManager.AddOption(new CheckBoxOption(Settings.CreativeVoidZone, new CheckBoxConfig() { checkIfDisabled = CheckOnlySurvivorSkins, restartRequired = true }));
            ModSettingsManager.AddOption(new CheckBoxOption(Settings.EndOfGameMusic, new CheckBoxConfig() { checkIfDisabled = CheckOnlySurvivorSkins, restartRequired = false }));
            ModSettingsManager.AddOption(new CheckBoxOption(Settings.RespawnSFX, new CheckBoxConfig() { checkIfDisabled = CheckOnlySurvivorSkins, restartRequired = true }));
            ModSettingsManager.AddOption(new CheckBoxOption(Settings.ReplaceIntroScene, new CheckBoxConfig() { checkIfDisabled = CheckOnlySurvivorSkins, restartRequired = true }));

            ModSettingsManager.AddOption(new CheckBoxOption(Settings.BonziBuddyBool, new CheckBoxConfig() { checkIfDisabled = CheckOnlySurvivorSkins, restartRequired = false }));
            ModSettingsManager.AddOption(new CheckBoxOption(Settings.AccurateTTS, new CheckBoxConfig() { checkIfDisabled = CheckOnlySurvivorSkins, restartRequired = false }));
            ModSettingsManager.AddOption(new CheckBoxOption(Settings.MLGMode, new CheckBoxConfig() { checkIfDisabled = CheckOnlySurvivorSkins, restartRequired = false }));

            ModSettingsManager.AddOption(new CheckBoxOption(Settings.ReplaceItems, new CheckBoxConfig() { checkIfDisabled = CheckOnlySurvivorSkins, restartRequired = true }));
        }
        private static bool CheckOnlySurvivorSkins()
        {
            return OnlySurvivorSkins.Value;
        }
        private static bool CheckCritMarkerScaler()
        {
            return OnlySurvivorSkins.Value || HitMarkerVolume.Value <= 0;
        }
        public static void PingAll()
        {
            Assets.AddBundle("Models.na");
            InteractReplacements.Interactables.Init();
            EnemyReplacements.RunAll();
            ItemReplacements.RunAll();
            HudChanges.RunAll();
            BigToasterClass.RunAll();
        }
        private static void Setup()
        {
            ModSettingsManager.SetModDescription($"Made by Rune#0001 Metrosexual Fruitcake#6969 & Unsaved Trash#0001\n\nVersion {PluginInfo.PLUGIN_VERSION}");
            EnemyReplacements.LoadResource("moisture_defaults");
            ModSettingsManager.SetModIcon(Assets.Load<Sprite>("@MoistureUpset_moisture_defaults:assets/newlogo.png"));
            //ModSettingsManager.setPanelTitle("Moisture Upset");
        }
    }
}
