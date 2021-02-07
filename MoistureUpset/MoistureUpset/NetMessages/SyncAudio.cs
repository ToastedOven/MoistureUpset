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
    class SyncAudio : INetMessage
    {
        public static bool doMinecraftOofSound = false;

        NetworkInstanceId netId;
        string soundId;

        public SyncAudio()
        {

        }

        public SyncAudio(NetworkInstanceId netId, string soundId)
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
            if (!doMinecraftOofSound && soundId == "MinecraftHurt")
                return;

            GameObject bodyObject = Util.FindNetworkObject(netId);
            if (!bodyObject)
            {
                DebugClass.Log($"Body is null!!!");
            }

            AkSoundEngine.PostEvent(soundId, bodyObject);
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(netId);
            writer.Write(soundId);
        }
    }
}
