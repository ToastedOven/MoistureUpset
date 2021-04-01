using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using MoistureUpset.NetMessages;
using RiskOfOptions;

namespace MoistureUpset
{
    public static class Settings
    {
        public static void RunAll()
        {
            Setup();
            HitMarker();
            ModSettingsManager.addStartupListener(new UnityEngine.Events.UnityAction(PingAll));
            Misc();
            EnemyOptions();
            CollabOptions();
            SoundOptions();
        }
        public static void PingAll()
        {
            EnemyReplacements.LoadResource("na");

            BigToasterClass.HitMarker(BigJank.getOptionValue("HitMarker Volume"));
            BigToasterClass.Modded_MSX(BigJank.getOptionValue("Modded Music Volume"));
            BigToasterClass.Modded_SFX(BigJank.getOptionValue("Modded SFX Volume"));
            InteractReplacements.Interactables.Init();
            EnemyReplacements.RunAll();
            HudChanges.RunAll();
            BigToasterClass.RunAll();
        }
        private static void Setup()
        {
            ModSettingsManager.setPanelDescription("Made by Rune#0001 Metrosexual Fruitcake#6969 & Unsaved Trash#0001\n\nVersion 1.3.1");
            ModSettingsManager.setPanelTitle("Moisture Upset");
        }
        private static void HitMarker()
        {
            ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Only Survivor Skins", "Only survivor skins are enabled. Restart required!", "0"));
            ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Slider, "HitMarker Volume", "This sound is also tied to SFX, but has a seperate slider if you want it to be less noisy", "100"));
            ModSettingsManager.addListener(ModSettingsManager.getOption("HitMarker Volume"), new UnityEngine.Events.UnityAction<float>(BigToasterClass.HitMarker));
            ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Slider, "Modded Music Volume", "The default music slider also work for modded music, but this effects modded music only. Incase you want a different audio balance", "50"));
            ModSettingsManager.addListener(ModSettingsManager.getOption("Modded Music Volume"), new UnityEngine.Events.UnityAction<float>(BigToasterClass.Modded_MSX));
            ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Slider, "Modded SFX Volume", "The default sound slider also work for modded SFX, but this effects modded sfx only. Incase you want a different audio balance", "50"));
            ModSettingsManager.addListener(ModSettingsManager.getOption("Modded SFX Volume"), new UnityEngine.Events.UnityAction<float>(BigToasterClass.Modded_SFX));
        }
        private static void EnemyOptions()
        {
            ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Dogplane", "Replaces wisps with a dogplanes", "1"));
            ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Comedy", "Replaces jellyfish with an astounding amount of comedy", "1"));
            ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Froggy Chair", "Replaces beetles with froggy chairs", "1"));
            ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Mike Wazowski", "Replaces lemurians with mike wazowskis", "1"));
            ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Skeleton Crab", "Replaces hermit crabs with spider jockies", "1"));
            ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Trumpet Skeleton", "Replaces imps with trumpet skeletons", "1"));
            ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Lemme Smash", "please", "1"));
            ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Obama Prism", "Replaces solus units with obamium units", "1"));
            ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Toad", "Shoutouts to SimpleFlips", "1"));
            ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Taco Bell", "Replaces brass contraptions with midroll ads", "1"));
            ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Winston", "Replaces beetle guards with enemy team winstons", "1"));
            ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Thomas", "Replaces bighorn bisons with thomas the tank engine", "1"));
            ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Robloxian", "Replaces stone golems with robloxians", "1"));
            ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Heavy", "Replaces clay templars with heavy's", "1"));
            ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Ghast", "Replaces greater wisps with ghasts", "1"));
            ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Roflcopter", "Replaces flying lunar chimeras with Roflcopters", "1"));
            ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Bowser", "Replaces elder lemurians with slightly furry bowsers", "1"));
            ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Hagrid", "Replaces parents with hagrid", "1"));
            ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Thanos", "Replaces mithrix with thanos", "1"));
            ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Rob", "Replaces grounded lunar chimeras with Rob", "1"));
            ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Crab Rave", "Replaces void reavers with crabs", "1"));
            ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Nyan Cat", "Replaces beetle queens with nyan cats", "1"));
            ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Giga Puddi", "PUDDI PUDDI", "1"));
            ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Roblox Titan", "Replaces Stone Titan with a buff robloxian", "1"));
            ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Alex Jones", "Replaces Aurelionite with alex jones", "1"));
            ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "WanderingAtEveryone", "Replaces wandering vagrants with some @Someone", "1"));
            ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Pool Noodle", "Replaces magma worms with pool noodles", "1"));
            //ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Obama Sphere", "Replaces solus control units with obama spheres", "1"));
            //ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Obamium Worship Unit", "Replaces alloy worship units with obama spheres", "1"));
            ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Twitch", "Replaces grovetenders with twitch chat", "1"));
            ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Sans", "Replaces imp overlords with sans", "1"));
            ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Imposter", "Replaces scavengers with crewmates", "1"));
            ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Squirmles", "Replaces overloading worms with Squirmles", "1"));
            ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Merchant", "Replaces shop keeper with beedle", "1"));
            ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Cereal", "EAT EM UP EAT EM UP EAT EM UP!", "1"));
            ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Interactables", "Replaces chests and barrels with minecraft items", "1"));
            ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Currency Changes", "Replaces currency types with robux and tix", "1"));
            //ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Top Secret Setting", "You'll probably know it when you see it", "1"));
            //ModSettingsManager.addListener(ModSettingsManager.getOption("Top Secret Setting"), new UnityEngine.Events.UnityAction<bool>(BonziBuddy.SetActive));
        }
        private static void CollabOptions()
        {
            ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "DireSeeker", "Replaces Direseeker with Giga Bowser (Requires Direseeker mod)", "1"));
        }

        private static void SoundOptions()
        {
            ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Minecraft Oof Sounds", "Adds Minecraft oof sounds whenever you get hurt.", "1"));

            // Yeah I know this looks jank, but it sort of works.
            ModSettingsManager.addListener(ModSettingsManager.getOption("Minecraft Oof Sounds"), new UnityEngine.Events.UnityAction<bool>(delegate(bool temp) { if (float.Parse(ModSettingsManager.getOptionValue("Only Survivor Skins")) != 1) SyncAudio.doMinecraftOofSound = temp; }));
            ModSettingsManager.addListener(ModSettingsManager.getOption("Shrine Changes"), new UnityEngine.Events.UnityAction<bool>(delegate (bool temp) { if (float.Parse(ModSettingsManager.getOptionValue("Only Survivor Skins")) != 1) SyncAudio.doShrineSound = temp; }));
        }
        private static void Misc()
        {
            //ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Original REDACTED TTS", "Gives REDACTED REDACTED's original TTS voice. For 99% of users, the first time you turn this on it will require an install of SAPI4 and tv_enua(this is where REDACTED's voice is). If you do not feel safe doing this you can either leave this unchecked or manually download and install Speakonia from cfs-technologies on the web.", "0"));
            //ModSettingsManager.addListener(ModSettingsManager.getOption("Original REDACTED TTS"), new UnityEngine.Events.UnityAction<bool>(BonziBuddy.FixTTS));
            ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "NSFW", "Toggles 'NSFW' content. Not actually NSFW like boobies, just some questionable words if you aren't into that kinda thing", "0"));
            ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Fanfare", "Adds fanfare to the end of the teleporter event", "1"));
            ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Pizza Roll", "Replaces that diamond UI element with a pizza roll", "1"));
            ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Roblox Cursor", "Replaces the cursor with a roblox cursor", "1"));
            ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Logo", "Replaces the logo with moisture upset", "1"));
            ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Generic boss music", "Replaces generic boss music (horde of basic enemies) with custom music", "1"));
            ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Awp UI", "Replaces clicks on the UI with awp shots and reloads", "1"));
            ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Chest noises", "Replaces chest noises", "1"));
            ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Player death sound", "Replaces player death sound", "1"));
            ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Player death chat", "Complains about the game in chat so you don't have to", "1"));
            ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Difficulty Icons", "Replaces difficulty icons with much more accurate images", "1"));
            ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Difficulty Names", "Replaces difficulty names with uhh... humor", "1"));
            ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "In-Run Difficulty Names", "AND THEY DON'T STOP COMING", "1"));
            ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Main menu music", "WHATS GOING ON", "1"));
            ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Shreks outhouse", "SOMEBODY", "1"));
            ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Shrine Changes", "Very important updates for shrines", "1"));
            ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Misc", "Random text changes, might fix later", "1"));
            ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Creative Void Zone", "Adds some entertainment value to the Void Zone", "1"));
            ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "End of game music", "Defeat theme", "1"));
            ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Respawn SFX", "Yeah", "1"));
            ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Replace Intro Scene", "Replaces the default intro cutscene with one that UnsavedTrash made", "1"));
            //ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Shreks outhouse", "SOMEBODY", "1"));
        }
    }
}
