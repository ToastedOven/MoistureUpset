using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace MoistureUpset
{
    public class BonziBuddy : MonoBehaviour
    {
        AudioSource song;
        string songName;

        void Start()
        {
            song = gameObject.AddComponent<AudioSource>();
        }
        public IEnumerator loadsong()
        {
            yield return new WaitUntil(() => File.Exists("BepInEx\\plugins\\MetrosexualFruitcake-MoistureUpset\\joemama.wav"));
            string url = string.Format("file://{0}", "BepInEx\\plugins\\MetrosexualFruitcake-MoistureUpset\\joemama.wav");
            WWW www = new WWW(url);
            yield return www;


            song.clip = www.GetAudioClip(false, false);
            song.Play();
        }
    }
}
