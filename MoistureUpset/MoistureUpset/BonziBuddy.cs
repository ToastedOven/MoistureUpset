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
        public bool isLocked(FileInfo file)
        {
            FileStream fileStream = null;
            try
            {
                fileStream = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            }
            catch (IOException)
            {
                return true;
            }
            finally
            {
                bool flag = fileStream != null;
                if (flag)
                {
                    fileStream.Close();
                }
            }
            return false;
        }
        public IEnumerator loadsong()
        {
            yield return new WaitUntil(() => File.Exists("BepInEx\\plugins\\MetrosexualFruitcake-MoistureUpset\\joemama.wav"));
            FileInfo file = new FileInfo("BepInEx\\plugins\\MetrosexualFruitcake-MoistureUpset\\joemama.wav");
            yield return new WaitUntil(() => !isLocked(file));
            yield return new WaitUntil(() => File.Exists("BepInEx\\plugins\\MetrosexualFruitcake-MoistureUpset\\joemama.wav"));
            FileInfo fileNew = new FileInfo("BepInEx\\plugins\\MetrosexualFruitcake-MoistureUpset\\joemama.wav");
            //string url = string.Format("file://{0}", "BepInEx\\plugins\\MetrosexualFruitcake-MoistureUpset\\joemama.wav");
            WWW www = new WWW("file:///" + "BepInEx\\plugins\\MetrosexualFruitcake-MoistureUpset\\joemama.wav");
            yield return www;

            AudioClip clip = www.GetAudioClip(false, true, AudioType.WAV);
            while (!clip.isReadyToPlay)
            {
                yield return www;
            }

            clip.name = Path.GetFileNameWithoutExtension("BepInEx\\plugins\\MetrosexualFruitcake-MoistureUpset\\joemama.wav");
            song.clip = clip;
            //Debug.Log($"--------{song.minDistance}     {song.maxDistance}");
            song.Play();
        }
    }
}
