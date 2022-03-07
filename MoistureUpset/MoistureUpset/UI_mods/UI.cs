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
                    Texture2D tex = new Texture2D(256, 256);
                    tex.filterMode = FilterMode.Trilinear;
                    tex = logo.GetComponent<UnityEngine.UI.Image>().sprite.texture;
                    tex.LoadImage(bytes);
                    logo.GetComponent<UnityEngine.UI.Image>().sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(.5f, .5f), 100);
                    logo.name = $"{logo.name}REPLACED";
                }
            }
            catch (Exception)
            {
            }
        }
        public static void ReplaceUIBetter(string path, string png)
        {
            try
            {
                var fab = Resources.Load<Sprite>(path);
                byte[] bytes = ByteReader.readbytes(png);
                fab.texture.LoadImage(bytes);
            }
            catch (Exception e)
            {
                //Debug.Log($"Couldn't replace sprite: {e}");
            }
        }
        public static void ReplaceTexture2D(string path, string png)
        {
            try
            {
                var fab = Assets.Load<Texture2D>(path);
                byte[] bytes = ByteReader.readbytes(png);
                fab.LoadImage(bytes);
            }
            catch (Exception e)
            {
                //Debug.Log($"Couldn't replace sprite: {e}");
            }
        }
    }
    //Choice (Difficulty.Easy)
    //Choice (Difficulty.Normal)
    //Choice (Difficulty.Hard)
}
