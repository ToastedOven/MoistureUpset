using BepInEx;
using R2API.Utils;
using RoR2;
using R2API;
using R2API.MiscHelpers;
using System.Reflection;
using static R2API.SoundAPI;
using System;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;
using UnityEngine.SceneManagement;

namespace MoistureUpset
{
    public static class DebugClass
    {
        public static void UIdebug()
        {
            UnityEngine.UI.Image[] objects = GameObject.FindObjectsOfType<UnityEngine.UI.Image>();
            Debug.Log($"--------------uicomponents----------------");
            foreach (var item in objects)
            {
                Debug.Log(item);
            }
            Debug.Log($"------------------------------------------");
        }
        public static void UIdebugReplace()
        {
            UnityEngine.UI.Image[] objects = GameObject.FindObjectsOfType<UnityEngine.UI.Image>();
            byte[] bytes = ByteReader.readbytes("MoistureUpset.Resources.roblox.png");
            Texture2D tex = new Texture2D(512, 512);
            tex.LoadImage(bytes);
            for (int i = 0; i < objects.Length; i++)
            {
                objects[i].sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(.5f, .5f), 100);
            }
        }
        public static void ListComponents(string name)
        {
            GameObject g = GameObject.Find(name);
            if (!g)
            {
                Debug.Log($"----------------components----------------");
                Debug.Log($"GameObject not found");
                Debug.Log($"------------------------------------------");
                return;
            }
            Debug.Log($"----------------components----------------");
            foreach (var item in g.GetComponents<Component>())
            {
                Debug.Log(item);
            }
            Debug.Log($"------------------------------------------");
        }
    }
}
