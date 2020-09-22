using BepInEx;
using RoR2.ConVar;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace MoistureUpset
{
    public class ModOption
    {
        public OptionType optionType;
        public string longName;
        public string owner { get; private set; }
        public string name;
        public string description;

        public BaseConVar conVar;

        public GameObject gameObject;

        public enum OptionType
        {
            Bool,
            Slider,
            Keybinding
        }

        public ModOption(OptionType _optionType, string _name, string _description)
        {
            optionType = _optionType;
            name = _name;
            description = _description;

            var classes = Assembly.GetCallingAssembly().GetExportedTypes();

            foreach (var item in classes)
            {
                BepInPlugin bepInPlugin = item.GetCustomAttribute<BepInPlugin>();

                if (bepInPlugin != null)
                {
                    owner = bepInPlugin.GUID;
                }
            }
        }

        public static bool operator ==(ModOption a, ModOption b)
        {
            if (a.longName == b.longName)
            {
                return true;
            }
            return false;
        }

        public static bool operator !=(ModOption a, ModOption b)
        {
            if (a.longName != b.longName)
            {
                return true;
            }
            return false;
        }


    }
}
