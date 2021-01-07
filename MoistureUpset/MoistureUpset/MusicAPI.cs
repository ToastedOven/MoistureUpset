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
using System.Collections;

namespace MoistureUpset
{
    public static class MusicAPI
    {
        //disclaimer, this might be a braindamaged way of doing it but I say if it works it's not braindamaged.
        //INSTRUCTIONS:
        //In wwise, create a master mixer container and assign it's rtpc to this: https://i.imgur.com/PHsrIx4.png
        //create a music mixer container and then assign it's rtpc to this: https://i.imgur.com/aPNgGU5.png
        //these rtpc values can be made like this https://i.imgur.com/V1JldBM.png
        //create a container in your music mixer, include your music files
        //if desired, set songs to loop
        //setup the audio to play at max volume regardless of distance https://i.imgur.com/KTLaGcx.png
        //create an event which will start your song
        //create an event which will stop your song

        //when creating your bnk file, it will create a txt file with it, in here you can find your event names
        //in order to stop the music player you will need to find the cachedName of said song
        //you can find these by overriding the RoR2.MusicController.UpdateState 
        //run the original command but also debug log "self.GetPropertyValue<MusicTrackDef>("currentTrack").cachedName"
        //ReplaceSong() should work good even if called a lot, I call it in RoR2.MusicController.UpdateState
        //You are responsible for stopping your songs when relevent, a common place would probably be RoR2.BossGroup.DefeatBossObjectiveTracker.ctor, which essentially indicates the start of the teleporter fight

        //under normal circumstances, you should be able to GameObject.FindObjectOfType the musiccontroller
        //for example, I do this on the DefeatBossObjectiveTracker
        //var con = GameObject.FindObjectOfType<MusicController>();
        //MusicAPI.StopCustomSong(ref con, "StopAirHorn");
        public static bool ReplaceSong(ref MusicController controller, string original, string replacement)
        {
            try
            {
                string song = controller.GetPropertyValue<MusicTrackDef>("currentTrack").cachedName;
                if (song.ToUpper() == original.ToUpper())
                {
                    if (MusicAPI.IsPlaying(controller, replacement))
                    {
                        var s = controller.GetPropertyValue<MusicTrackDef>("currentTrack");
                        AK.Wwise.State[] ar = s.states;
                        for (int i = 0; i < ar.Length; i++)
                        {
                            uint num;
                            AkSoundEngine.GetState(ar[i].GroupId, out num);
                            if (num == 0)
                            {
                                return false;
                            }
                        }
                    }
                    controller.GetPropertyValue<MusicTrackDef>("currentTrack").Stop();
                    AkSoundEngine.PostEvent(replacement, controller.gameObject);
                    return true;
                }
            }
            catch (Exception)
            {
            }
            return false;
        }
        public static void StopSong(ref MusicController controller, string original)
        {
            try
            {
                string song = controller.GetPropertyValue<MusicTrackDef>("currentTrack").cachedName;
                if (song.ToUpper() == original.ToUpper())
                {
                    controller.GetPropertyValue<MusicTrackDef>("currentTrack").Stop();
                }
            }
            catch (Exception)
            {
            }
        }
        public static void GetCurrentSong(ref MusicController controller)
        {
            try
            {
                string song = controller.GetPropertyValue<MusicTrackDef>("currentTrack").cachedName;
                //Debug.Log($"--currently playing song------{song}");
            }
            catch (Exception)
            {
            }
        }
        public static void StopCustomSong(ref MusicController controller, string stopevent)
        {
            try
            {
                AkSoundEngine.PostEvent(stopevent, controller.gameObject);
            }
            catch (Exception)
            {
            }
        }
        public static void StartCustomSong(ref MusicController controller, string startevent)
        {
            try
            {
                AkSoundEngine.PostEvent(startevent, controller.gameObject);
            }
            catch (Exception)
            {
            }
        }
        public static bool IsPlaying(MusicController c, string e)
        {///////////////////////////////////////////////use this for minecraft, dont loop
            uint id = AkSoundEngine.GetIDFromString(e);
            uint[] ids = new uint[50];
            uint count = 50;
            AKRESULT result = AkSoundEngine.GetPlayingIDsFromGameObject(c.gameObject, ref count, ids);
            for (int i = 0; i < 50; i++)
            {
                uint playingID = ids[i];
                uint eventID = AkSoundEngine.GetEventIDFromPlayingID(playingID);
                if (eventID == id)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
