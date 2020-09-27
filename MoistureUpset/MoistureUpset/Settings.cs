using System;
using System.Collections.Generic;
using System.Text;
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
        }
        public static void PingAll()
        {
            BigToasterClass.HitMarker(float.Parse(ModSettingsManager.getOptionValue("HitMarker Volume")));
            EnemyReplacements.RunAll();
            BigToasterClass.RunAll();
        }
        private static void Setup()
        {
            ModSettingsManager.setPanelDescription("Dude I'm so moist right now.");
            ModSettingsManager.setPanelTitle("Moisture Upset");
        }
        private static void HitMarker()
        {
            ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Slider, "HitMarker Volume", "This sound is also tied to SFX, but has a seperate slider if you want it to be less noisy", "100"));
            ModSettingsManager.addListener(ModSettingsManager.getOption("HitMarker Volume"), new UnityEngine.Events.UnityAction<float>(BigToasterClass.HitMarker));
        }
        private static void EnemyOptions()
        {
            ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Dogplane", "Replaces wisps with a dogplanes", "1"));
            ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Comedy", "Replaces jellyfish with an astounding amount of comedy", "1"));
            ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Froggy Chair", "Replaces beetles with froggy chairs", "1"));
            ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Mike Wazowski", "Replaces lemurians with mike wazowskis", "1"));
            ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Hermit Crab", "Replaces hermit crabs with _", "1"));
            ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Trumpet Skeleton", "Replaces imps with trumpet skeletons", "1"));
            ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Alloy Vulture", "Replaces alloy vultures with _", "1"));
            ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Obama Prism", "Replaces solus units with obamium units", "1"));
            ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Toad", "Shoutouts to SimpleFlips", "1"));
            ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Taco Bell", "Replaces brass contraptions with midroll ads", "1"));
            ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Winston", "Replaces beetle guards with enemy team winstons", "1"));
            ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Thomas", "Replaces bighorn bisons with thomas the tank engine", "1"));
            ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Robloxian", "Replaces stone golems with robloxians", "1"));
            ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Heavy", "Replaces clay templars with heavy's", "1"));
            ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Ghast", "Replaces greater wisps with ghasts", "1"));
            ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Lunar Chimera Flying", "Replaces flying lunar chimeras with _", "1"));
            ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Bowser", "Replaces elder lemurians with bowsers", "1"));
            ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Parent", "Replaces parents with _", "1"));
            ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Mithrix", "Replaces mithrix with _", "1"));
            ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Lunar Chimera Grounded", "Replaces grounded lunar chimeras with _", "1"));
            ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Void Reaver", "Replaces void reavers with _", "1"));
            ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Beetle Queen", "Replaces beetle queens with _", "1"));
            ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Clay Dunestrider", "Replaces clay dunestriders with _", "1"));
            ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Stone Titan", "Replaces stone titans with _", "1"));
            ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Aurelionite", "Replaces aurelionite with _", "1"));
            ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Wandering Vagrant", "Replaces wandering vagrants with _", "1"));
            ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Magma Worm", "Replaces magma worms with _", "1"));
            //ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Obama Sphere", "Replaces solus control units with obama spheres", "1"));
            //ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Obamium Worship Unit", "Replaces alloy worship units with obama spheres", "1"));
            ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Grovetender", "Replaces grovetenders with _", "1"));
            ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Sans", "Replaces imp overlords with sans", "1"));
            ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Scavenger", "Replaces scavengers with _", "1"));
            ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Overloading Worm", "Replaces overloading worms with _", "1"));
            ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Merchant", "Replaces shop keeper worms with beetle", "1"));
            ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Alex Jones", "Replaces Aurelionite worms with alex jones", "1"));
            ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Roblox Titan", "Replaces Stone Titan worms with a buff robloxian", "1"));
        }
        private static void Misc()
        {
            ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "NSFW", "Turn this off to disable stuff that is obviously (in my eyes) NSFW, such as Jizzle replacing Drizzle. Defaults to being turned off just in case", "0"));
            ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Fanfare", "Adds fanfare to the end of the teleporter event", "1"));
            ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Pizza Roll", "Replaces that diamond UI element with a pizza roll", "1"));
            ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Roblox Cursor", "Replaces the cursor with a roblox cursor", "1"));
            ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Logo", "Replaces the logo with moisture upset", "1"));
            ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Generic boss music", "Replaces generic boss music (horde of basic enemies) with custom music", "1"));
            ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Awp UI", "Replaces clicks on the UI with awp shots and reloads", "1"));
            ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Chest noises", "Replaces various chest noises", "1"));
            ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Player death sound", "Replaces player death sound", "1"));
            ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Player death chat", "Complains about the game in chat so you don't have to", "1"));
            ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Difficulty Icons", "Replaces difficulty icons with much more accurate images", "1"));
            ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Difficulty Names", "Replaces difficulty names with uhh... humor", "1"));
            ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "In-Run Difficulty Names", "AND THEY DON'T STOP COMING", "1"));
            ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Main menu music", "WHATS GOING ON", "1"));
            ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Shreks outhouse", "SOMEBODY", "1"));
            //ModSettingsManager.addOption(new ModOption(ModOption.OptionType.Bool, "Shreks outhouse", "SOMEBODY", "1"));
        }
    }
}
