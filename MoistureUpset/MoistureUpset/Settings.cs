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
            EnemyReplacements.RunAll();
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
            ModSettingsManager.AddCheckBox("Only Survivor Skins", "Only survivor skins are enabled. Restart required!", false, "Misc");

            ModSettingsManager.AddSlider("HitMarker Volume", "This sound is also tied to SFX, but has a separate slider if you want it to be less noisy", 100, "Audio", survivorsSkinsOnlySlider);
            ModSettingsManager.AddListener(new UnityEngine.Events.UnityAction<float>(BigToasterClass.HitMarker), "HitMarker Volume", "Audio");
            ModSettingsManager.AddSlider("Modded Music Volume", "The default music slider also work for modded music, but this effects modded music only. In case you want a different audio balance", 50, "Audio");
            ModSettingsManager.AddListener(new UnityEngine.Events.UnityAction<float>(BigToasterClass.Modded_MSX), "Modded Music Volume", "Audio");
            ModSettingsManager.AddSlider("Modded SFX Volume", "The default sound slider also work for modded SFX, but this effects modded sfx only. In case you want a different audio balance", 50, "Audio");
            ModSettingsManager.AddListener(new UnityEngine.Events.UnityAction<float>(BigToasterClass.Modded_SFX), "Modded SFX Volume", "Audio");
        }
        private static void EnemyOptions()
        {
            //ModSettingsManager.AddOption("", "", true, "");
            ModSettingsManager.AddCheckBox("Dogplane", "Replaces wisps with a dogplanes", true, "Enemy Skins", survivorSkinsOnlyCheckBox);
            ModSettingsManager.AddCheckBox("Comedy", "Replaces jellyfish with an astounding amount of comedy", true, "Enemy Skins", survivorSkinsOnlyCheckBox);
            ModSettingsManager.AddCheckBox("Froggy Chair", "Replaces beetles with froggy chairs", true, "Enemy Skins", survivorSkinsOnlyCheckBox);
            ModSettingsManager.AddCheckBox("Mike Wazowski", "Replaces lemurians with mike wazowskis", true, "Enemy Skins", survivorSkinsOnlyCheckBox);
            ModSettingsManager.AddCheckBox("Skeleton Crab", "Replaces hermit crabs with spider jockies", true, "Enemy Skins", survivorSkinsOnlyCheckBox);
            ModSettingsManager.AddCheckBox("Trumpet Skeleton", "Replaces imps with trumpet skeletons", true, "Enemy Skins", survivorSkinsOnlyCheckBox);
            ModSettingsManager.AddCheckBox("Lemme Smash", "please", true, "Enemy Skins", survivorSkinsOnlyCheckBox);
            ModSettingsManager.AddCheckBox("Obama Prism", "Replaces solus units with obamium units", true, "Enemy Skins", survivorSkinsOnlyCheckBox);
            ModSettingsManager.AddCheckBox("Toad", "Shoutouts to SimpleFlips", true, "Enemy Skins", survivorSkinsOnlyCheckBox);
            ModSettingsManager.AddCheckBox("Taco Bell", "Replaces brass contraptions with midroll ads", true, "Enemy Skins", survivorSkinsOnlyCheckBox);
            ModSettingsManager.AddCheckBox("Winston", "Replaces beetle guards with enemy team winstons", true, "Enemy Skins", survivorSkinsOnlyCheckBox);
            ModSettingsManager.AddCheckBox("Thomas", "Replaces bighorn bisons with thomas the tank engine", true, "Enemy Skins", survivorSkinsOnlyCheckBox);
            ModSettingsManager.AddCheckBox("Robloxian", "Replaces stone golems with robloxians", true, "Enemy Skins", survivorSkinsOnlyCheckBox);
            ModSettingsManager.AddCheckBox("Heavy", "Replaces clay templars with heavy's", true, "Enemy Skins", survivorSkinsOnlyCheckBox);
            ModSettingsManager.AddCheckBox("Ghast", "Replaces greater wisps with ghasts", true, "Enemy Skins", survivorSkinsOnlyCheckBox);
            ModSettingsManager.AddCheckBox("Roflcopter", "Replaces flying lunar chimeras with Roflcopters", true, "Enemy Skins", survivorSkinsOnlyCheckBox);
            ModSettingsManager.AddCheckBox("Bowser", "Replaces elder lemurians with slightly furry bowsers", true, "Enemy Skins", survivorSkinsOnlyCheckBox);
            ModSettingsManager.AddCheckBox("Hagrid", "Replaces parents with hagrid", true, "Enemy Skins", survivorSkinsOnlyCheckBox);
            ModSettingsManager.AddCheckBox("Thanos", "Replaces mithrix with thanos", true, "Enemy Skins", survivorSkinsOnlyCheckBox);
            ModSettingsManager.AddCheckBox("Rob", "Replaces grounded lunar chimeras with Rob", true, "Enemy Skins", survivorSkinsOnlyCheckBox);
            ModSettingsManager.AddCheckBox("Crab Rave", "Replaces void reavers with crabs", true, "Enemy Skins", survivorSkinsOnlyCheckBox);
            ModSettingsManager.AddCheckBox("Nyan Cat", "Replaces beetle queens with nyan cats", true, "Enemy Skins", survivorSkinsOnlyCheckBox);
            ModSettingsManager.AddCheckBox("Giga Puddi", "PUDDI PUDDI", true, "Enemy Skins", survivorSkinsOnlyCheckBox);
            ModSettingsManager.AddCheckBox("Roblox Titan", "Replaces Stone Titan with a buff robloxian", true, "Enemy Skins", survivorSkinsOnlyCheckBox);
            ModSettingsManager.AddCheckBox("Alex Jones", "Replaces Aurelionite with alex jones", true, "Enemy Skins", survivorSkinsOnlyCheckBox);
            ModSettingsManager.AddCheckBox("WanderingAtEveryone", "Replaces wandering vagrants with some @Someone", true, "Enemy Skins", survivorSkinsOnlyCheckBox);
            ModSettingsManager.AddCheckBox("Pool Noodle", "Replaces magma worms with pool noodles", true, "Enemy Skins", survivorSkinsOnlyCheckBox);
            ModSettingsManager.AddCheckBox("Twitch", "Replaces grovetenders with twitch chat", true, "Enemy Skins", survivorSkinsOnlyCheckBox);
            ModSettingsManager.AddCheckBox("Sans", "Replaces imp overlords with sans", true, "Enemy Skins", survivorSkinsOnlyCheckBox);
            ModSettingsManager.AddCheckBox("Imposter", "Replaces scavengers with crewmates", true, "Enemy Skins", survivorSkinsOnlyCheckBox);
            ModSettingsManager.AddCheckBox("Squirmles", "Replaces overloading worms with Squirmles", true, "Enemy Skins", survivorSkinsOnlyCheckBox);
            ModSettingsManager.AddCheckBox("Merchant", "Replaces shop keeper with beedle", true, "Enemy Skins", survivorSkinsOnlyCheckBox);
            ModSettingsManager.AddCheckBox("Cereal", "EAT EM UP EAT EM UP EAT EM UP!", true, "Enemy Skins", survivorSkinsOnlyCheckBox);
            ModSettingsManager.AddCheckBox("Interactables", "Replaces chests and barrels with minecraft items", true, "Interactables", survivorSkinsOnlyCheckBox);
            ModSettingsManager.AddCheckBox("Currency Changes", "Replaces currency types with robux and tix", true, "UI Changes", survivorSkinsOnlyCheckBox);
            ModSettingsManager.AddCheckBox("Top Secret Setting", "You'll probably know it when you see it", true, "Misc", survivorSkinsOnlyCheckBox);
            ModSettingsManager.AddListener(new UnityEngine.Events.UnityAction<bool>(BonziBuddy.SetActive), "Top Secret Setting", "Misc");
            //ModSettingsManager.addListener(ModSettingsManager.getOption("Top Secret Setting"), new UnityEngine.Events.UnityAction<bool>(BonziBuddy.SetActive));
        }
        private static void CollabOptions()
        {
            ModSettingsManager.AddCheckBox("DireSeeker", "Replaces Direseeker with Giga Bowser (Requires Direseeker mod)", true, "Enemy Skins", survivorSkinsOnlyCheckBox);
        }

        private static void SoundOptions()
        {
            ModSettingsManager.AddCheckBox("Minecraft Oof Sounds", "Adds Minecraft oof sounds whenever you get hurt.", true, "Misc", survivorSkinsOnlyCheckBox);

            // Yeah I know this looks jank, but it sort of works.
            ModSettingsManager.AddListener(new UnityEngine.Events.UnityAction<bool>(delegate (bool temp) { if (float.Parse(ModSettingsManager.getOptionValue("Only Survivor Skins")) != 1) SyncAudio.doMinecraftOofSound = temp; }), "Minecraft Oof Sounds", "Misc");
            ModSettingsManager.AddListener(new UnityEngine.Events.UnityAction<bool>(delegate (bool temp) { if (float.Parse(ModSettingsManager.getOptionValue("Only Survivor Skins")) != 1) SyncAudio.doShrineSound = temp; }), "Shrine Changes", "Interactables");
        }
        private static void Misc()
        {
            ModSettingsManager.AddCheckBox("Original REDACTED TTS", "Gives REDACTED REDACTED's original TTS voice. For 99% of users, the first time you turn this on it will require an install of SAPI4 and tv_enua(this is where REDACTED's voice is). If you do not feel safe doing this you can either leave this unchecked or manually download and install Speakonia from cfs-technologies on the web.", false, "Misc", survivorSkinsOnlyCheckBox);
            ModSettingsManager.AddListener(new UnityEngine.Events.UnityAction<bool>(BonziBuddy.FixTTS), "Original REDACTED TTS", "Misc");
            ModSettingsManager.AddCheckBox("NSFW", "Toggles 'NSFW' content. Not actually NSFW like boobies, just some questionable words if you aren't into that kinda thing", false, "Misc", survivorSkinsOnlyCheckBox);
            ModSettingsManager.AddCheckBox("Fanfare", "Adds fanfare to the end of the teleporter event", true, "Audio", survivorSkinsOnlyCheckBox);
            ModSettingsManager.AddCheckBox("Pizza Roll", "Replaces that diamond UI element with a pizza roll", true, "UI Changes", survivorSkinsOnlyCheckBox);
            ModSettingsManager.AddCheckBox("Roblox Cursor", "Replaces the cursor with a roblox cursor", true, "UI Changes", survivorSkinsOnlyCheckBox);
            ModSettingsManager.AddCheckBox("Logo", "Replaces the logo with moisture upset", true, "UI Changes", survivorSkinsOnlyCheckBox);
            ModSettingsManager.AddCheckBox("Generic boss music", "Replaces generic boss music (horde of basic enemies) with custom music", true, "Audio", survivorSkinsOnlyCheckBox);
            ModSettingsManager.AddCheckBox("Awp UI", "Replaces clicks on the UI with awp shots and reloads", true, "Audio", survivorSkinsOnlyCheckBox);
            ModSettingsManager.AddCheckBox("Chest noises", "Replaces chest noises", true, "Interactables", survivorSkinsOnlyCheckBox);
            ModSettingsManager.AddCheckBox("Player death sound", "Replaces player death sound", true, "Audio", survivorSkinsOnlyCheckBox);
            ModSettingsManager.AddCheckBox("Player death chat", "Complains about the game in chat so you don't have to", true, "Misc", survivorSkinsOnlyCheckBox);
            ModSettingsManager.AddCheckBox("Difficulty Icons", "Replaces difficulty icons with much more accurate images", true, "UI Changes", survivorSkinsOnlyCheckBox);
            ModSettingsManager.AddCheckBox("Difficulty Names", "Replaces difficulty names with uhh... humor", true, "UI Changes", survivorSkinsOnlyCheckBox);
            ModSettingsManager.AddCheckBox("In-Run Difficulty Names", "AND THEY DON'T STOP COMING", true, "UI Changes", survivorSkinsOnlyCheckBox);
            ModSettingsManager.AddCheckBox("Main menu music", "WHATS GOING ON", true, "Audio", survivorSkinsOnlyCheckBox);
            ModSettingsManager.AddCheckBox("Shreks outhouse", "SOMEBODY", true, "Misc", survivorSkinsOnlyCheckBox);
            ModSettingsManager.AddCheckBox("Shrine Changes", "Very important updates for shrines", true, "Interactables", survivorSkinsOnlyCheckBox);
            ModSettingsManager.AddCheckBox("Misc", "Random text changes, might fix later", true, "Misc", survivorSkinsOnlyCheckBox);
            ModSettingsManager.AddCheckBox("Creative Void Zone", "Adds some entertainment value to the Void Zone", true, "Audio", survivorSkinsOnlyCheckBox);
            ModSettingsManager.AddCheckBox("End of game music", "Defeat theme", true, "Audio", survivorSkinsOnlyCheckBox);
            ModSettingsManager.AddCheckBox("Respawn SFX", "Yeah", true, "Audio", survivorSkinsOnlyCheckBox);
            ModSettingsManager.AddCheckBox("Replace Intro Scene", "Replaces the default intro cutscene with one that UnsavedTrash made", true, "UI Changes", survivorSkinsOnlyCheckBox);

            ModSettingsManager.AddKeyBind("Emote Wheel", "Displays the emote wheel.", KeyCode.C, "Controls");

            ModSettingsManager.AddListener(new UnityAction<KeyCode>(delegate (KeyCode keyCode) {mousechecker.emoteButton = keyCode;}), "Emote Wheel", "Controls");
            //ModSettingsManager.AddCheckBox("Shreks outhouse", "SOMEBODY", "1"));
        }
    }
}
