using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using MoistureUpset.NetMessages;
using RiskOfOptions;
using UnityEngine;
using UnityEngine.Events;

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
            ModSettingsManager.AddSlider("HitMarker Volume", "This sound is also tied to SFX, but has a seperate slider if you want it to be less noisy", 100, "Audio");
            ModSettingsManager.AddListener(new UnityEngine.Events.UnityAction<float>(BigToasterClass.HitMarker), "HitMarker Volume", "Audio");
            ModSettingsManager.AddSlider("Modded Music Volume", "The default music slider also work for modded music, but this effects modded music only. Incase you want a different audio balance", 50, "Audio");
            ModSettingsManager.AddListener(new UnityEngine.Events.UnityAction<float>(BigToasterClass.Modded_MSX), "Modded Music Volume", "Audio");
            ModSettingsManager.AddSlider("Modded SFX Volume", "The default sound slider also work for modded SFX, but this effects modded sfx only. Incase you want a different audio balance", 50, "Audio");
            ModSettingsManager.AddListener(new UnityEngine.Events.UnityAction<float>(BigToasterClass.Modded_SFX), "Modded SFX Volume", "Audio");
        }
        private static void EnemyOptions()
        {
            //ModSettingsManager.AddOption("", "", true, "");
            ModSettingsManager.AddCheckBox("Dogplane", "Replaces wisps with a dogplanes", true, "Enemy Skins");
            ModSettingsManager.AddCheckBox("Comedy", "Replaces jellyfish with an astounding amount of comedy", true, "Enemy Skins");
            ModSettingsManager.AddCheckBox("Froggy Chair", "Replaces beetles with froggy chairs", true, "Enemy Skins");
            ModSettingsManager.AddCheckBox("Mike Wazowski", "Replaces lemurians with mike wazowskis", true, "Enemy Skins");
            ModSettingsManager.AddCheckBox("Skeleton Crab", "Replaces hermit crabs with spider jockies", true, "Enemy Skins");
            ModSettingsManager.AddCheckBox("Trumpet Skeleton", "Replaces imps with trumpet skeletons", true, "Enemy Skins");
            ModSettingsManager.AddCheckBox("Lemme Smash", "please", true, "Enemy Skins");
            ModSettingsManager.AddCheckBox("Obama Prism", "Replaces solus units with obamium units", true, "Enemy Skins");
            ModSettingsManager.AddCheckBox("Toad", "Shoutouts to SimpleFlips", true, "Enemy Skins");
            ModSettingsManager.AddCheckBox("Taco Bell", "Replaces brass contraptions with midroll ads", true, "Enemy Skins");
            ModSettingsManager.AddCheckBox("Winston", "Replaces beetle guards with enemy team winstons", true, "Enemy Skins");
            ModSettingsManager.AddCheckBox("Thomas", "Replaces bighorn bisons with thomas the tank engine", true, "Enemy Skins");
            ModSettingsManager.AddCheckBox("Robloxian", "Replaces stone golems with robloxians", true, "Enemy Skins");
            ModSettingsManager.AddCheckBox("Heavy", "Replaces clay templars with heavy's", true, "Enemy Skins");
            ModSettingsManager.AddCheckBox("Ghast", "Replaces greater wisps with ghasts", true, "Enemy Skins");
            ModSettingsManager.AddCheckBox("Roflcopter", "Replaces flying lunar chimeras with Roflcopters", true, "Enemy Skins");
            ModSettingsManager.AddCheckBox("Bowser", "Replaces elder lemurians with slightly furry bowsers", true, "Enemy Skins");
            ModSettingsManager.AddCheckBox("Hagrid", "Replaces parents with hagrid", true, "Enemy Skins");
            ModSettingsManager.AddCheckBox("Thanos", "Replaces mithrix with thanos", true, "Enemy Skins");
            ModSettingsManager.AddCheckBox("Rob", "Replaces grounded lunar chimeras with Rob", true, "Enemy Skins");
            ModSettingsManager.AddCheckBox("Crab Rave", "Replaces void reavers with crabs", true, "Enemy Skins");
            ModSettingsManager.AddCheckBox("Nyan Cat", "Replaces beetle queens with nyan cats", true, "Enemy Skins");
            ModSettingsManager.AddCheckBox("Giga Puddi", "PUDDI PUDDI", true, "Enemy Skins");
            ModSettingsManager.AddCheckBox("Roblox Titan", "Replaces Stone Titan with a buff robloxian", true, "Enemy Skins");
            ModSettingsManager.AddCheckBox("Alex Jones", "Replaces Aurelionite with alex jones", true, "Enemy Skins");
            ModSettingsManager.AddCheckBox("WanderingAtEveryone", "Replaces wandering vagrants with some @Someone", true, "Enemy Skins");
            ModSettingsManager.AddCheckBox("Pool Noodle", "Replaces magma worms with pool noodles", true, "Enemy Skins");
            ModSettingsManager.AddCheckBox("Twitch", "Replaces grovetenders with twitch chat", true, "Enemy Skins");
            ModSettingsManager.AddCheckBox("Sans", "Replaces imp overlords with sans", true, "Enemy Skins");
            ModSettingsManager.AddCheckBox("Imposter", "Replaces scavengers with crewmates", true, "Enemy Skins");
            ModSettingsManager.AddCheckBox("Squirmles", "Replaces overloading worms with Squirmles", true, "Enemy Skins");
            ModSettingsManager.AddCheckBox("Merchant", "Replaces shop keeper with beedle", true, "Enemy Skins");
            ModSettingsManager.AddCheckBox("Cereal", "EAT EM UP EAT EM UP EAT EM UP!", true, "Enemy Skins");
            ModSettingsManager.AddCheckBox("Interactables", "Replaces chests and barrels with minecraft items", true, "Interactables");
            ModSettingsManager.AddCheckBox("Currency Changes", "Replaces currency types with robux and tix", true, "UI Changes");
            ModSettingsManager.AddCheckBox("Top Secret Setting", "You'll probably know it when you see it", true, "Misc");
            ModSettingsManager.AddListener(new UnityEngine.Events.UnityAction<bool>(BonziBuddy.SetActive), "Top Secret Setting", "Misc");
            //ModSettingsManager.addListener(ModSettingsManager.getOption("Top Secret Setting"), new UnityEngine.Events.UnityAction<bool>(BonziBuddy.SetActive));
        }
        private static void CollabOptions()
        {
            ModSettingsManager.AddCheckBox("DireSeeker", "Replaces Direseeker with Giga Bowser (Requires Direseeker mod)", true, "Enemy Skins");
        }

        private static void SoundOptions()
        {
            ModSettingsManager.AddCheckBox("Minecraft Oof Sounds", "Adds Minecraft oof sounds whenever you get hurt.", true, "Misc");

            // Yeah I know this looks jank, but it sort of works.
            ModSettingsManager.AddListener(new UnityEngine.Events.UnityAction<bool>(delegate (bool temp) { if (float.Parse(ModSettingsManager.getOptionValue("Only Survivor Skins")) != 1) SyncAudio.doMinecraftOofSound = temp; }), "Minecraft Oof Sounds", "Misc");
            ModSettingsManager.AddListener(new UnityEngine.Events.UnityAction<bool>(delegate (bool temp) { if (float.Parse(ModSettingsManager.getOptionValue("Only Survivor Skins")) != 1) SyncAudio.doShrineSound = temp; }), "Shrine Changes", "Interactables");
        }
        private static void Misc()
        {
            ModSettingsManager.AddCheckBox("Original REDACTED TTS", "Gives REDACTED REDACTED's original TTS voice. For 99% of users, the first time you turn this on it will require an install of SAPI4 and tv_enua(this is where REDACTED's voice is). If you do not feel safe doing this you can either leave this unchecked or manually download and install Speakonia from cfs-technologies on the web.", false, "Misc");
            ModSettingsManager.AddListener(new UnityEngine.Events.UnityAction<bool>(BonziBuddy.FixTTS), "Original REDACTED TTS", "Misc");
            ModSettingsManager.AddCheckBox("NSFW", "Toggles 'NSFW' content. Not actually NSFW like boobies, just some questionable words if you aren't into that kinda thing", false, "Misc");
            ModSettingsManager.AddCheckBox("Fanfare", "Adds fanfare to the end of the teleporter event", true, "Audio");
            ModSettingsManager.AddCheckBox("Pizza Roll", "Replaces that diamond UI element with a pizza roll", true, "UI Changes");
            ModSettingsManager.AddCheckBox("Roblox Cursor", "Replaces the cursor with a roblox cursor", true, "UI Changes");
            ModSettingsManager.AddCheckBox("Logo", "Replaces the logo with moisture upset", true, "UI Changes");
            ModSettingsManager.AddCheckBox("Generic boss music", "Replaces generic boss music (horde of basic enemies) with custom music", true, "Audio");
            ModSettingsManager.AddCheckBox("Awp UI", "Replaces clicks on the UI with awp shots and reloads", true, "Audio");
            ModSettingsManager.AddCheckBox("Chest noises", "Replaces chest noises", true, "Interactables");
            ModSettingsManager.AddCheckBox("Player death sound", "Replaces player death sound", true, "Audio");
            ModSettingsManager.AddCheckBox("Player death chat", "Complains about the game in chat so you don't have to", true, "Misc");
            ModSettingsManager.AddCheckBox("Difficulty Icons", "Replaces difficulty icons with much more accurate images", true, "UI Changes");
            ModSettingsManager.AddCheckBox("Difficulty Names", "Replaces difficulty names with uhh... humor", true, "UI Changes");
            ModSettingsManager.AddCheckBox("In-Run Difficulty Names", "AND THEY DON'T STOP COMING", true, "UI Changes");
            ModSettingsManager.AddCheckBox("Main menu music", "WHATS GOING ON", true, "Audio");
            ModSettingsManager.AddCheckBox("Shreks outhouse", "SOMEBODY", true, "Misc");
            ModSettingsManager.AddCheckBox("Shrine Changes", "Very important updates for shrines", true, "Interactables");
            ModSettingsManager.AddCheckBox("Misc", "Random text changes, might fix later", true, "Misc");
            ModSettingsManager.AddCheckBox("Creative Void Zone", "Adds some entertainment value to the Void Zone", true, "Audio");
            ModSettingsManager.AddCheckBox("End of game music", "Defeat theme", true, "Audio");
            ModSettingsManager.AddCheckBox("Respawn SFX", "Yeah", true, "Audio");
            ModSettingsManager.AddCheckBox("Replace Intro Scene", "Replaces the default intro cutscene with one that UnsavedTrash made", true, "UI Changes");

            ModSettingsManager.AddKeyBind("Emote Wheel", "Displays the emote wheel.", KeyCode.C, "Controls");

            ModSettingsManager.AddListener(new UnityAction<KeyCode>(delegate (KeyCode keyCode) {mousechecker.emoteButton = keyCode;}), "Emote Wheel", "Controls");
            //ModSettingsManager.AddCheckBox("Shreks outhouse", "SOMEBODY", "1"));
        }
    }
}
