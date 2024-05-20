using MoistureUpset.Skins;
using R2API.Networking.Interfaces;
using RoR2;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace MoistureUpset.NetMessages
{
    class SyncAudioWithJotaroSubtitles : INetMessage
    {
        NetworkInstanceId netId;
        string soundId;

        public SyncAudioWithJotaroSubtitles()
        {

        }

        public SyncAudioWithJotaroSubtitles(NetworkInstanceId netId, string soundId)
        {
            this.netId = netId;
            this.soundId = soundId;
        }

        //public SyncAudio(Vector3)

        public void Deserialize(NetworkReader reader)
        {
            netId = reader.ReadNetworkId();
            soundId = reader.ReadString();
        }

        public void OnReceived()
        {
            GameObject bodyObject = Util.FindNetworkObject(netId);
            if (!bodyObject)
            {
                DebugClass.Log($"Body is null!!!");
            }

            AkSoundEngine.PostEvent(soundId, bodyObject, (uint)AkCallbackType.AK_Marker, EventCallback, bodyObject);
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(netId);
            writer.Write(soundId);
        }

        private void EventCallback(object in_cookie, AkCallbackType in_type, AkCallbackInfo in_info)
        {
            if (in_type != AkCallbackType.AK_Marker)
                return;
            AkMarkerCallbackInfo akMarker = (AkMarkerCallbackInfo)in_info;

            GameObject body = (GameObject)in_cookie;

            DebugClass.Log(akMarker.strLabel);
            DebugClass.Log(body.name);

            body.GetComponentInChildren<Skins.Jotaro.SubtitleController>().SetSubtitle(akMarker.strLabel, 4f);



            //SetSubtitle(voiceLines[UnityEngine.Random.Range(0, voiceLines.Length)], 4f);
            //SetSubtitle(akMarker.strLabel, 4f);
        }
    }
}
