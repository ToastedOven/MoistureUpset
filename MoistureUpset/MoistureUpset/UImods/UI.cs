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
    public static class UImods
    {
        public static void ReplaceUIObject(string objectname, string path)
        {
            try
            {
                GameObject logo = GameObject.Find(objectname);
                if (logo)
                {
                    byte[] bytes = ByteReader.readbytes(path);
                    Texture2D tex = new Texture2D(512, 512);
                    tex.LoadImage(bytes);
                    logo.GetComponent<UnityEngine.UI.Image>().sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(.5f, .5f), 100);
                    logo.name = $"{logo.name}REPLACED";
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
