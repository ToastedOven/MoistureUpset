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
    public class SyncChance : INetMessage
    {
        NetworkInstanceId netId;
        bool succ;
        string sound;

        public SyncChance()
        {

        }

        public SyncChance(NetworkInstanceId netId, bool succ, string sound)
        {
            this.netId = netId;
            this.succ = succ;
            this.sound = sound;
        }

        public void Deserialize(NetworkReader reader)
        {
            netId = reader.ReadNetworkId();
            succ = reader.ReadBoolean();
            sound = reader.ReadString();
        }

        public void OnReceived()
        {
            GameObject g = Util.FindNetworkObject(netId);
            if (g)
            {
                if (g.GetComponentInChildren<CharacterBody>() == NetworkUser.readOnlyLocalPlayersList[0].master?.GetBody())
                {
                    BonziBuddy.buddy.Chance(succ);
                }
                if (BigJank.getOptionValue("Shrine Changes", "Interactables"))
                {
                    AkSoundEngine.PostEvent(sound, g);
                }
            }
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(netId);
            writer.Write(succ);
            writer.Write(sound);
        }
    }
}
