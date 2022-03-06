using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using MoistureUpset.NetMessages;
using RiskOfOptions;
using RiskOfOptions.OptionOverrides;
using UnityEngine;
using UnityEngine.Events;

namespace MoistureUpset
{
    public static class Settings
    {
        private static CheckBoxOverride survivorSkinsOnlyCheckBox;
        private static SliderOverride survivorsSkinsOnlySlider;
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
            BigToasterClass.HitMarker(BigJank.getFloatValue("HitMarker Volume", "Audio"));
            BigToasterClass.Modded_MSX(BigJank.getFloatValue("Modded Music Volume", "Audio"));
            BigToasterClass.Modded_SFX(BigJank.getFloatValue("Modded SFX Volume", "Audio"));
            InteractReplacements.Interactables.Init();
            //EnemyReplacements.RunAll();
            HudChanges.RunAll();
            BigToasterClass.RunAll();
        }
        private static void Setup()
        {
            ModSettingsManager.setPanelDescription($"Made by Rune#0001 Metrosexual Fruitcake#6969 & Unsaved Trash#0001\n\nVersion {Moisture_Upset.VERSION}");

            survivorSkinsOnlyCheckBox = new CheckBoxOverride()
            {
                Name = "Only Survivor Skins",
                CategoryName = "Misc",
                OverrideOnTrue = true,
                ValueToReturnWhenOverriden = false
            };

            survivorsSkinsOnlySlider = new SliderOverride()
            {
                Name = "Only Survivor Skins",
                CategoryName = "Misc",
                OverrideOnTrue = true,
                ValueToReturnWhenOverriden = 0f
            };

            ModSettingsManager.setPanelTitle("Moisture Upset");
            ModSettingsManager.CreateCategory("Audio");
            ModSettingsManager.CreateCategory("Enemy Skins");
            ModSettingsManager.CreateCategory("UI Changes");
            ModSettingsManager.CreateCategory("Interactables");
            ModSettingsManager.CreateCategory("Controls");
            ModSettingsManager.CreateCategory("Misc");
        }
        private static void HitMarker()
        {
            AddCheckBox("Only Survivor Skins", "Only survivor skins are enabled. Restart required!", false, "Misc");

            AddSlider("HitMarker Volume", "This sound is also tied to SFX, but has a separate slider if you want it to be less noisy", 100, 0, 100, "Audio", survivorsSkinsOnlySlider);
            ModSettingsManager.AddListener(new UnityEngine.Events.UnityAction<float>(BigToasterClass.HitMarker), "HitMarker Volume", "Audio");
            AddCheckBox("Scale HitMarker with Crit", "This will scale down the crit sound effect as your crit goes up", true, "Audio", survivorSkinsOnlyCheckBox);
            AddSlider("Modded Music Volume", "The default music slider also work for modded music, but this effects modded music only. In case you want a different audio balance", 50, 0, 100, "Audio");
            ModSettingsManager.AddListener(new UnityEngine.Events.UnityAction<float>(BigToasterClass.Modded_MSX), "Modded Music Volume", "Audio");
            AddSlider("Modded SFX Volume", "The default sound slider also work for modded SFX, but this effects modded sfx only. In case you want a different audio balance", 50, 0, 100, "Audio");
            ModSettingsManager.AddListener(new UnityEngine.Events.UnityAction<float>(BigToasterClass.Modded_SFX), "Modded SFX Volume", "Audio");
        }
        private static void EnemyOptions()
        {
            //ModSettingsManager.AddOption("", "", true, "");
            AddCheckBox("Dogplane", "Replaces wisps with a dogplanes", true, "Enemy Skins", survivorSkinsOnlyCheckBox);
            AddCheckBox("Comedy", "Replaces jellyfish with an astounding amount of comedy", true, "Enemy Skins", survivorSkinsOnlyCheckBox);
            AddCheckBox("Froggy Chair", "Replaces beetles with froggy chairs", true, "Enemy Skins", survivorSkinsOnlyCheckBox);
            AddCheckBox("Mike Wazowski", "Replaces lemurians with mike wazowskis", true, "Enemy Skins", survivorSkinsOnlyCheckBox);
            AddCheckBox("Skeleton Crab", "Replaces hermit crabs with spider jockies", true, "Enemy Skins", survivorSkinsOnlyCheckBox);
            AddCheckBox("Trumpet Skeleton", "Replaces imps with trumpet skeletons", true, "Enemy Skins", survivorSkinsOnlyCheckBox);
            AddCheckBox("Lemme Smash", "please", true, "Enemy Skins", survivorSkinsOnlyCheckBox);
            AddCheckBox("Obama Prism", "Replaces solus units with obamium units", true, "Enemy Skins", survivorSkinsOnlyCheckBox);
            AddCheckBox("Toad", "Shoutouts to SimpleFlips", true, "Enemy Skins", survivorSkinsOnlyCheckBox);
            AddCheckBox("Taco Bell", "Replaces brass contraptions with midroll ads", true, "Enemy Skins", survivorSkinsOnlyCheckBox);
            AddCheckBox("Winston", "Replaces beetle guards with enemy team winstons", true, "Enemy Skins", survivorSkinsOnlyCheckBox);
            AddCheckBox("Thomas", "Replaces bighorn bisons with thomas the tank engine", true, "Enemy Skins", survivorSkinsOnlyCheckBox);
            AddCheckBox("Robloxian", "Replaces stone golems with robloxians", true, "Enemy Skins", survivorSkinsOnlyCheckBox);
            AddCheckBox("Heavy", "Replaces clay templars with heavy's", true, "Enemy Skins", survivorSkinsOnlyCheckBox);
            AddCheckBox("Ghast", "Replaces greater wisps with ghasts", true, "Enemy Skins", survivorSkinsOnlyCheckBox);
            AddCheckBox("Roflcopter", "Replaces flying lunar chimeras with Roflcopters", true, "Enemy Skins", survivorSkinsOnlyCheckBox);
            AddCheckBox("Bowser", "Replaces elder lemurians with slightly furry bowsers", true, "Enemy Skins", survivorSkinsOnlyCheckBox);
            AddCheckBox("Hagrid", "Replaces parents with hagrid", true, "Enemy Skins", survivorSkinsOnlyCheckBox);
            AddCheckBox("Thanos", "Replaces mithrix with thanos", true, "Enemy Skins", survivorSkinsOnlyCheckBox);
            AddCheckBox("Rob", "Replaces grounded lunar chimeras with Rob", true, "Enemy Skins", survivorSkinsOnlyCheckBox);
            AddCheckBox("Crab Rave", "Replaces void reavers with crabs", true, "Enemy Skins", survivorSkinsOnlyCheckBox);
            AddCheckBox("Nyan Cat", "Replaces beetle queens with nyan cats", true, "Enemy Skins", survivorSkinsOnlyCheckBox);
            AddCheckBox("Giga Puddi", "PUDDI PUDDI", true, "Enemy Skins", survivorSkinsOnlyCheckBox);
            AddCheckBox("Roblox Titan", "Replaces Stone Titan with a buff robloxian", true, "Enemy Skins", survivorSkinsOnlyCheckBox);
            AddCheckBox("Alex Jones", "Replaces Aurelionite with alex jones", true, "Enemy Skins", survivorSkinsOnlyCheckBox);
            AddCheckBox("WanderingAtEveryone", "Replaces wandering vagrants with some @Someone", true, "Enemy Skins", survivorSkinsOnlyCheckBox);
            AddCheckBox("Pool Noodle", "Replaces magma worms with pool noodles", true, "Enemy Skins", survivorSkinsOnlyCheckBox);
            AddCheckBox("Twitch", "Replaces grovetenders with twitch chat", true, "Enemy Skins", survivorSkinsOnlyCheckBox);
            AddCheckBox("Sans", "Replaces imp overlords with sans", true, "Enemy Skins", survivorSkinsOnlyCheckBox);
            AddCheckBox("Imposter", "Replaces scavengers with crewmates", true, "Enemy Skins", survivorSkinsOnlyCheckBox);
            AddCheckBox("Squirmles", "Replaces overloading worms with Squirmles", true, "Enemy Skins", survivorSkinsOnlyCheckBox);
            AddCheckBox("Merchant", "Replaces shop keeper with beedle", true, "Enemy Skins", survivorSkinsOnlyCheckBox);
            AddCheckBox("Cereal", "EAT EM UP EAT EM UP EAT EM UP!", true, "Enemy Skins", survivorSkinsOnlyCheckBox);
            AddCheckBox("Interactables", "Replaces chests and barrels with minecraft items", true, "Interactables", survivorSkinsOnlyCheckBox);
            AddCheckBox("Currency Changes", "Replaces currency types with robux and tix", true, "UI Changes", survivorSkinsOnlyCheckBox);
            AddCheckBox("Top Secret Setting", "You'll probably know it when you see it", true, "Misc", survivorSkinsOnlyCheckBox);
            ModSettingsManager.AddListener(new UnityEngine.Events.UnityAction<bool>(BonziBuddy.SetActive), "Top Secret Setting", "Misc");
            AddCheckBox("MLG Mode", "What year is it", true, "Misc", survivorSkinsOnlyCheckBox);
            //AddCheckBox("Force Restart Bonzi Buddy", "Bonzi Buddy isn't a perfect creation. If something goes horribly wrong this might fix him right up.", true, "Misc", survivorSkinsOnlyCheckBox);
            //ModSettingsManager.AddListener(new UnityEngine.Events.UnityAction<bool>(BonziBuddy.ForceRestart), "Force Restart Bonzi Buddy", "Misc");
            //ModSettingsManager.addListener(ModSettingsManager.getOption("Top Secret Setting"), new UnityEngine.Events.UnityAction<bool>(BonziBuddy.SetActive));
        }
        private static void CollabOptions()
        {
            AddCheckBox("DireSeeker", "Replaces Direseeker with Giga Bowser (Requires Direseeker mod)", true, "Enemy Skins", survivorSkinsOnlyCheckBox);
        }

        private static void SoundOptions()
        {
            AddCheckBox("Minecraft Oof Sounds", "Adds Minecraft oof sounds whenever you get hurt.", true, "Misc", survivorSkinsOnlyCheckBox);

            // Yeah I know this looks jank, but it sort of works.
            ModSettingsManager.AddListener(new UnityEngine.Events.UnityAction<bool>(delegate (bool temp) { if (float.Parse(ModSettingsManager.getOptionValue("Only Survivor Skins")) != 1) SyncAudio.doMinecraftOofSound = temp; }), "Minecraft Oof Sounds", "Misc");
            ModSettingsManager.AddListener(new UnityEngine.Events.UnityAction<bool>(delegate (bool temp) { if (float.Parse(ModSettingsManager.getOptionValue("Only Survivor Skins")) != 1) SyncAudio.doShrineSound = temp; }), "Shrine Changes", "Interactables");
        }
        private static void Misc()
        {
            AddCheckBox("Accurate REDACTED TTS", "Gives REDACTED REDACTED's original TTS voice. For 99% of users, the first time you turn this on it will require an install of SAPI4 and tv_enua(this is where REDACTED's voice is). If you do not feel safe doing this you can either leave this unchecked or manually download and install Speakonia from cfs-technologies on the web.", false, "Misc", survivorSkinsOnlyCheckBox);
            ModSettingsManager.AddListener(new UnityEngine.Events.UnityAction<bool>(BonziBuddy.FixTTS), "Accurate REDACTED TTS", "Misc");
            AddCheckBox("NSFW", "Toggles 'NSFW' content. Not actually NSFW like boobies, just some questionable words if you aren't into that kinda thing", false, "Misc", survivorSkinsOnlyCheckBox);
            AddCheckBox("Fanfare", "Adds fanfare to the end of the teleporter event", true, "Audio", survivorSkinsOnlyCheckBox);
            AddCheckBox("Pizza Roll", "Replaces that diamond UI element with a pizza roll", true, "UI Changes", survivorSkinsOnlyCheckBox);
            AddCheckBox("Roblox Cursor", "Replaces the cursor with a roblox cursor", true, "UI Changes", survivorSkinsOnlyCheckBox);
            AddCheckBox("Logo", "Replaces the logo with moisture upset", true, "UI Changes", survivorSkinsOnlyCheckBox);
            AddCheckBox("Generic boss music", "Replaces generic boss music (horde of basic enemies) with custom music", true, "Audio", survivorSkinsOnlyCheckBox);
            AddCheckBox("Awp UI", "Replaces clicks on the UI with awp shots and reloads", true, "Audio", survivorSkinsOnlyCheckBox);
            AddCheckBox("Chest noises", "Replaces chest noises", true, "Interactables", survivorSkinsOnlyCheckBox);
            AddCheckBox("Player death sound", "Replaces player death sound", true, "Audio", survivorSkinsOnlyCheckBox);
            AddCheckBox("Player death chat", "Complains about the game in chat so you don't have to", true, "Misc", survivorSkinsOnlyCheckBox);
            AddCheckBox("Difficulty Icons", "Replaces difficulty icons with much more accurate images", true, "UI Changes", survivorSkinsOnlyCheckBox);
            AddCheckBox("Difficulty Names", "Replaces difficulty names with uhh... humor", true, "UI Changes", survivorSkinsOnlyCheckBox);
            AddCheckBox("In-Run Difficulty Names", "AND THEY DON'T STOP COMING", true, "UI Changes", survivorSkinsOnlyCheckBox);
            AddCheckBox("Main menu music", "WHATS GOING ON", true, "Audio", survivorSkinsOnlyCheckBox);
            AddCheckBox("Shreks outhouse", "SOMEBODY", true, "Misc", survivorSkinsOnlyCheckBox);
            AddCheckBox("Shrine Changes", "Very important updates for shrines", true, "Interactables", survivorSkinsOnlyCheckBox);
            AddCheckBox("Misc", "Random text changes, might fix later", true, "Misc", survivorSkinsOnlyCheckBox);
            AddCheckBox("Creative Void Zone", "Adds some entertainment value to the Void Zone", true, "Audio", survivorSkinsOnlyCheckBox);
            AddCheckBox("End of game music", "Defeat theme", true, "Audio", survivorSkinsOnlyCheckBox);
            AddCheckBox("Respawn SFX", "Yeah", true, "Audio", survivorSkinsOnlyCheckBox);
            AddCheckBox("Replace Intro Scene", "Replaces the default intro cutscene with one that UnsavedTrash made", true, "UI Changes", survivorSkinsOnlyCheckBox);
            //ModSettingsManager.AddCheckBox("Shreks outhouse", "SOMEBODY", "1"));
        }


        private static void AddCheckBox(string name, string desc, bool restart, string category, CheckBoxOverride over = null)
        {
            var thing = new RiskOfOptions.OptionConstructors.CheckBox() { Name = name, Description = desc, RestartRequired = restart, CategoryName = category };
            if (over != null)
            {
                thing.Override = over;
            }
            ModSettingsManager.AddOption(thing);
        }
        private static void AddSlider(string name, string desc, int starting, int min, int max, string category, SliderOverride over = null)
        {
            //ModSettingsManager.AddSlider("HitMarker Volume", "This sound is also tied to SFX, but has a separate slider if you want it to be less noisy", 100, 0, 100, "Audio", survivorsSkinsOnlySlider);
            var thing = new RiskOfOptions.OptionConstructors.Slider() { Name = name, Description = desc, CategoryName = category, DefaultValue = starting, Min = min, Max = max };
            if (over != null)
            {
                thing.Override = over;
            }
            ModSettingsManager.AddOption(thing);
        }
    }
}
