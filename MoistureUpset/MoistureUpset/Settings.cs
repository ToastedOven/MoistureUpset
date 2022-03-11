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
            OnlySurvivorSkins = Moisture_Upset.instance.Config.Bind<bool>("Misc", "Only Survivor Skins", false, "Disables everything except survivor skins.");
            HitMarkerVolume = Moisture_Upset.instance.Config.Bind<float>("Audio", "HitMarker Volume", 100.0f, "This sound is also tied to SFX, but has a seperate slider if you want it to be less noisy");
            ModdedMusicVolume = Moisture_Upset.instance.Config.Bind<float>("Audio", "Modded Music Volume", 50.0f, "The default music slider also works for modded music, but this affects modded music only. Incase you want a different audio balance");
            ModdedSFXVolume = Moisture_Upset.instance.Config.Bind<float>("Audio", "Modded SFX Volume", 50.0f, "The default sound slider also works for modded SFX, but this affects modded sfx only. Incase you want a different audio balance");

            ScaleHitMarkerWithCrit = Moisture_Upset.instance.Config.Bind<bool>("Audio", "Scale Hit Marker With Crit", false, "Lowers crit sfx volume as crit chance goes up");

            HitMarkerVolume.Value = Mathf.Clamp(HitMarkerVolume.Value, 0, 100);
            ModdedMusicVolume.Value = Mathf.Clamp(ModdedMusicVolume.Value, 0, 100);
            ModdedSFXVolume.Value = Mathf.Clamp(ModdedSFXVolume.Value, 0, 100);

            HitMarkerVolume.SettingChanged += HitMarkerVolume_SettingChanged;
            ModdedMusicVolume.SettingChanged += ModdedMusicVolume_SettingChanged;
            ModdedSFXVolume.SettingChanged += ModdedSFXVolume_SettingChanged;
            ScaleHitMarkerWithCrit.SettingChanged += ScaleHitMarkerWithCrit_SettingChanged;

            Dogplane = Moisture_Upset.instance.Config.Bind<bool>("Enemy Skins", "Dogplane", true, "Replaces wisps with a dogplanes");
            Comedy = Moisture_Upset.instance.Config.Bind<bool>("Enemy Skins", "Comedy", true, "Replaces jellyfish with an astounding amount of comedy");
            FroggyChair = Moisture_Upset.instance.Config.Bind<bool>("Enemy Skins", "Froggy Chair", true, "Replaces beetles with froggy chairs");
            MikeWazowski = Moisture_Upset.instance.Config.Bind<bool>("Enemy Skins", "Mike Wazowski", true, "Replaces lemurians with mike wazowskis");
            SkeletonCrab = Moisture_Upset.instance.Config.Bind<bool>("Enemy Skins", "Spider Jockey", true, "Replaces hermit crabs with spider jockies");
            TrumpetSkeleton = Moisture_Upset.instance.Config.Bind<bool>("Enemy Skins", "Trumpet Skeleton", true, "Replaces imps with trumpet skeletons");
            LemmeSmash = Moisture_Upset.instance.Config.Bind<bool>("Enemy Skins", "Lemme Smash", true, "please");
            ObamaPrism = Moisture_Upset.instance.Config.Bind<bool>("Enemy Skins", "Obama Prism", true, "Replaces solus units with obamium units");
            Toad = Moisture_Upset.instance.Config.Bind<bool>("Enemy Skins", "Toad", true, "Shoutouts to SimpleFlips");
            TacoBell = Moisture_Upset.instance.Config.Bind<bool>("Enemy Skins", "Taco Bell", true, "Replaces brass contraptions with midroll ads");
            Winston = Moisture_Upset.instance.Config.Bind<bool>("Enemy Skins", "Winston", true, "Replaces beetle guards with enemy team winstons");
            Thomas = Moisture_Upset.instance.Config.Bind<bool>("Enemy Skins", "Thomas", true, "Replaces bighorn bisons with thomas the tank engine");
            Robloxian = Moisture_Upset.instance.Config.Bind<bool>("Enemy Skins", "Robloxian", true, "Replaces stone golems with robloxians");
            Heavy = Moisture_Upset.instance.Config.Bind<bool>("Enemy Skins", "Heavy", true, "Replaces clay templars with tf2heavyweaponsguy");
            Ghast = Moisture_Upset.instance.Config.Bind<bool>("Enemy Skins", "Ghast", true, "Replaces greater wisps with ghasts");
            Roflcopter = Moisture_Upset.instance.Config.Bind<bool>("Enemy Skins", "Roflcopter", true, "Replaces flying lunar chimeras with Roflcopters");
            Bowser = Moisture_Upset.instance.Config.Bind<bool>("Enemy Skins", "Bowser", true, "Replaces elder lemurians with slightly furry bowsers");
            Hagrid = Moisture_Upset.instance.Config.Bind<bool>("Enemy Skins", "Hagrid", true, "Replaces parents with hagrid");
            Thanos = Moisture_Upset.instance.Config.Bind<bool>("Enemy Skins", "Thanos", true, "Replaces mithrix with thanos");
            Rob = Moisture_Upset.instance.Config.Bind<bool>("Enemy Skins", "Rob", true, "Replaces grounded lunar chimeras with Rob");
            CrabRave = Moisture_Upset.instance.Config.Bind<bool>("Enemy Skins", "Crab Rave", true, "The void is raving");
            NyanCat = Moisture_Upset.instance.Config.Bind<bool>("Enemy Skins", "Nyan Cat", true, "Replaces beetle queens with nyan cats");
            GigaPuddi = Moisture_Upset.instance.Config.Bind<bool>("Enemy Skins", "Giga Puddi", true, "PUDDI PUDDI");
            RobloxTitan = Moisture_Upset.instance.Config.Bind<bool>("Enemy Skins", "Roblox Titan", true, "Replaces Stone Titan with a buff robloxian");
            AlexJones = Moisture_Upset.instance.Config.Bind<bool>("Enemy Skins", "Alex Jones", true, "Replaces Aurelionite with Alex Jones");
            WanderingAtEveryone = Moisture_Upset.instance.Config.Bind<bool>("Enemy Skins", "Wandering@Everyone", true, "Replaces wandering vagrants with some @Someone");
            PoolNoodle = Moisture_Upset.instance.Config.Bind<bool>("Enemy Skins", "Pool Noodle", true, "Replaces magma worms with pool noodles");
            Twitch = Moisture_Upset.instance.Config.Bind<bool>("Enemy Skins", "Twitch", true, "Replaces grovetenders with twitch chat");
            Sans = Moisture_Upset.instance.Config.Bind<bool>("Enemy Skins", "Sans", true, "Replaces imp overlords with sans");
            Imposter = Moisture_Upset.instance.Config.Bind<bool>("Enemy Skins", "Imposter", true, "WHEN THE IMPOSTER IS SUS");
            Squirmles = Moisture_Upset.instance.Config.Bind<bool>("Enemy Skins", "Squirmles", true, "Replaces overloading worms with Squirmles");
            Merchant = Moisture_Upset.instance.Config.Bind<bool>("Enemy Skins", "Merchant", true, "Replaces shop keeper with beedle");
            Cereal = Moisture_Upset.instance.Config.Bind<bool>("Enemy Skins", "Cereal", true, "EAT EM UP EAT EM UP EAT EM UP!");


            Interactables = Moisture_Upset.instance.Config.Bind<bool>("Interactables", "Interactables", true, "Replaces chests and barrels with minecraft items");
            CurrencyChanges = Moisture_Upset.instance.Config.Bind<bool>("UI Changes", "Currency Changes", true, "Replaces currency types with robux and tix");
            DireSeeker = Moisture_Upset.instance.Config.Bind<bool>("Enemy Skins", "DireSeeker", true, "Replaces Direseeker with Giga Bowser (Requires Direseeker mod)");

            MinecraftOofSounds = Moisture_Upset.instance.Config.Bind<bool>("Audio", "Minecraft Oof Sounds", true, "Adds Minecraft oof sounds whenever you get hurt");
            NSFW = Moisture_Upset.instance.Config.Bind<bool>("Misc", "NSFW", false, "Toggles 'NSFW' content. Not actually NSFW like boobies, just some questionable words if you aren't into that kinda thing");
            Fanfare = Moisture_Upset.instance.Config.Bind<bool>("Audio", "Fanfare", true, "Adds fanfare to the end of the teleporter event");
            PizzaRoll = Moisture_Upset.instance.Config.Bind<bool>("UI Changes", "Pizza Roll", true, "Replaces that diamond UI element with a pizza roll");
            RobloxCursor = Moisture_Upset.instance.Config.Bind<bool>("UI Changes", "Roblox Cursor", true, "Replaces the cursor with a roblox cursor");
            Logo = Moisture_Upset.instance.Config.Bind<bool>("UI Changes", "Logo", true, "Replaces the logo with moisture upset");
            GenericBossMusic = Moisture_Upset.instance.Config.Bind<bool>("Audio", "Generic boss music", true, "Replaces generic boss music (horde of basic enemies) with custom music");
            AwpUI = Moisture_Upset.instance.Config.Bind<bool>("Audio", "Awp UI", true, "Replaces clicks on the UI with awp shots and reloads");
            ChestNoises = Moisture_Upset.instance.Config.Bind<bool>("Audio", "Chest noises", true, "Replaces chest noises");
            PlayerDeathSound = Moisture_Upset.instance.Config.Bind<bool>("Audio", "Player death sound", true, "Replaces player death sound");
            PlayerDeathChat = Moisture_Upset.instance.Config.Bind<bool>("Misc", "Player death chat", true, "Complains about the game in chat so you don't have to");
            DifficultyIcons = Moisture_Upset.instance.Config.Bind<bool>("UI Changes", "Difficulty Icons", true, "Replaces difficulty icons with much more accurate images");
            DifficultyNames = Moisture_Upset.instance.Config.Bind<bool>("UI Changes", "DifficultyNames", true, "Replaces difficulty names with uhh... humor");
            InRunDifficultyNames = Moisture_Upset.instance.Config.Bind<bool>("UI Changes", "In-Run Difficulty Names", true, "AND THEY DON'T STOP COMING");
            MainMenuMusic = Moisture_Upset.instance.Config.Bind<bool>("Audio", "Main menu music", true, "WHATS GOING ON");
            ShreksOuthouse = Moisture_Upset.instance.Config.Bind<bool>("Interactables", "Shreks outhouse", true, "SOMEBODY");
            ShrineChanges = Moisture_Upset.instance.Config.Bind<bool>("Interactables", "Shrine Changes", true, "Very important updates for shrines");
            MiscOptions = Moisture_Upset.instance.Config.Bind<bool>("Misc", "Misc", true, "Random text changes, might fix later");
            CreativeVoidZone = Moisture_Upset.instance.Config.Bind<bool>("Audio", "Creative Void Zone", true, "Adds some entertainment value to the Void Zone");
            EndOfGameMusic = Moisture_Upset.instance.Config.Bind<bool>("Audio", "End of game music", true, "Defeat theme");
            RespawnSFX = Moisture_Upset.instance.Config.Bind<bool>("Audio", "Respawn SFX", true, "Yeah");
            ReplaceIntroScene = Moisture_Upset.instance.Config.Bind<bool>("Misc", "Replace Intro Scene", true, "Replaces the default intro cutscene with one that UnsavedTrash made");

            BonziBuddyBool = Moisture_Upset.instance.Config.Bind<bool>("Misc", "Top Secret Setting", true, "You'll probably know it when you see it");
            AccurateTTS = Moisture_Upset.instance.Config.Bind<bool>("Misc", "Accurate REDACTED TTS", false, "Gives REDACTED REDACTED's original TTS voice. For 99% of users, the first time you turn this on it will require an install of SAPI4 and tv_enua(this is where REDACTED's voice is). If you do not feel safe doing this you can either leave this unchecked or manually download and install Speakonia from cfs-technologies on the web.");
            MLGMode = Moisture_Upset.instance.Config.Bind<bool>("Misc", "MLG Mode", false, "What year is it");

            BonziBuddyBool.SettingChanged += BonziBuddyBool_SettingChanged; ;
            AccurateTTS.SettingChanged += AccurateTTS_SettingChanged; ;

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
            ModSettingsManager.AddOption(new CheckBoxOption(Settings.EndOfGameMusic, new CheckBoxConfig() { checkIfDisabled = CheckOnlySurvivorSkins, restartRequired = true }));
            ModSettingsManager.AddOption(new CheckBoxOption(Settings.RespawnSFX, new CheckBoxConfig() { checkIfDisabled = CheckOnlySurvivorSkins, restartRequired = true }));
            ModSettingsManager.AddOption(new CheckBoxOption(Settings.ReplaceIntroScene, new CheckBoxConfig() { checkIfDisabled = CheckOnlySurvivorSkins, restartRequired = true }));

            ModSettingsManager.AddOption(new CheckBoxOption(Settings.BonziBuddyBool, new CheckBoxConfig() { checkIfDisabled = CheckOnlySurvivorSkins, restartRequired = false }));
            ModSettingsManager.AddOption(new CheckBoxOption(Settings.AccurateTTS, new CheckBoxConfig() { checkIfDisabled = CheckOnlySurvivorSkins, restartRequired = false }));
            ModSettingsManager.AddOption(new CheckBoxOption(Settings.MLGMode, new CheckBoxConfig() { checkIfDisabled = CheckOnlySurvivorSkins, restartRequired = true }));

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
            HudChanges.RunAll();
            BigToasterClass.RunAll();
        }
        private static void Setup()
        {
            ModSettingsManager.SetModDescription($"Made by Rune#0001 Metrosexual Fruitcake#6969 & Unsaved Trash#0001\n\nVersion {Moisture_Upset.Version}");
            EnemyReplacements.LoadResource("moisture_defaults");
            ModSettingsManager.SetModIcon(Assets.Load<Sprite>("@MoistureUpset_moisture_defaults:assets/newlogo.png"));
            //ModSettingsManager.setPanelTitle("Moisture Upset");
        }
        private static void HitMarker()
        {
			
        }
        
        private static void CollabOptions()
        {
            //ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "DireSeeker", "Replaces Direseeker with Giga Bowser (Requires Direseeker mod)", "1"));
        }
        private static void EnemyOptions()
        {
			//AddCheckBox("Force Restart Bonzi Buddy", "Bonzi Buddy isn't a perfect creation. If something goes horribly wrong this might fix him right up.", true, "Misc", survivorSkinsOnlyCheckBox);
            //ModSettingsManager.AddListener(new UnityEngine.Events.UnityAction<bool>(BonziBuddy.ForceRestart), "Force Restart Bonzi Buddy", "Misc");
            //ModSettingsManager.addListener(ModSettingsManager.getOption("Top Secret Setting"), new UnityEngine.Events.UnityAction<bool>(BonziBuddy.SetActive));
        }

        private static void SoundOptions()
        {
            //ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Minecraft Oof Sounds", "Adds Minecraft oof sounds whenever you get hurt.", "1"));

            //// Yeah I know this looks jank, but it sort of works.
            //ModSettingsManager.addListener(ModSettingsManager.getOption("Minecraft Oof Sounds"), new UnityEngine.Events.UnityAction<bool>(delegate(bool temp) { if (float.Parse(ModSettingsManager.getOptionValue("Only Survivor Skins")) != 1) SyncAudio.doMinecraftOofSound = temp; }));
            //ModSettingsManager.addListener(ModSettingsManager.getOption("Shrine Changes"), new UnityEngine.Events.UnityAction<bool>(delegate (bool temp) { if (float.Parse(ModSettingsManager.getOptionValue("Only Survivor Skins")) != 1) SyncAudio.doShrineSound = temp; }));
        }
        private static void Misc()
        {
            ////ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Original REDACTED TTS", "Gives REDACTED REDACTED's original TTS voice. For 99% of users, the first time you turn this on it will require an install of SAPI4 and tv_enua(this is where REDACTED's voice is). If you do not feel safe doing this you can either leave this unchecked or manually download and install Speakonia from cfs-technologies on the web.", "0"));
            ////ModSettingsManager.addListener(ModSettingsManager.getOption("Original REDACTED TTS"), new UnityEngine.Events.UnityAction<bool>(BonziBuddy.FixTTS));
            //ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "NSFW", "Toggles 'NSFW' content. Not actually NSFW like boobies, just some questionable words if you aren't into that kinda thing", "0"));
            //ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Fanfare", "Adds fanfare to the end of the teleporter event", "1"));
            //ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Pizza Roll", "Replaces that diamond UI element with a pizza roll", "1"));
            //ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Roblox Cursor", "Replaces the cursor with a roblox cursor", "1"));
            //ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Logo", "Replaces the logo with moisture upset", "1"));
            //ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Generic boss music", "Replaces generic boss music (horde of basic enemies) with custom music", "1"));
            //ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Awp UI", "Replaces clicks on the UI with awp shots and reloads", "1"));
            //ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Chest noises", "Replaces chest noises", "1"));
            //ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Player death sound", "Replaces player death sound", "1"));
            //ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Player death chat", "Complains about the game in chat so you don't have to", "1"));
            //ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Difficulty Icons", "Replaces difficulty icons with much more accurate images", "1"));
            //ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Difficulty Names", "Replaces difficulty names with uhh... humor", "1"));
            //ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "In-Run Difficulty Names", "AND THEY DON'T STOP COMING", "1"));
            //ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Main menu music", "WHATS GOING ON", "1"));
            //ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Shreks outhouse", "SOMEBODY", "1"));
            //ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Shrine Changes", "Very important updates for shrines", "1"));
            //ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Misc", "Random text changes, might fix later", "1"));
            //ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Creative Void Zone", "Adds some entertainment value to the Void Zone", "1"));
            //ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "End of game music", "Defeat theme", "1"));
            //ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Respawn SFX", "Yeah", "1"));
            //ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Replace Intro Scene", "Replaces the default intro cutscene with one that UnsavedTrash made", "1"));
            ////ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Shreks outhouse", "SOMEBODY", "1"));
        }
    }
}
