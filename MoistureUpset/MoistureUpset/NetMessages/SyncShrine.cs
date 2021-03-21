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
    public enum ShrineType
    {
        blood,
        mountain,
        healing,
        order,
        combat,
        count
    }
    public class SyncShrine : INetMessage
    {
        NetworkInstanceId netId;
        Int32 type;
        public SyncShrine()
        {

        }

        public SyncShrine(NetworkInstanceId netId, ShrineType type)
        {
            this.netId = netId;
            this.type = (Int32)type;
        }

        public void Deserialize(NetworkReader reader)
        {
            netId = reader.ReadNetworkId();
            type = reader.ReadInt32();
        }

        public void OnReceived()
        {
            GameObject g = Util.FindNetworkObject(netId);
            if (g)
            {
                if (g.GetComponentInChildren<CharacterBody>() == NetworkUser.readOnlyLocalPlayersList[0].master?.GetBody())
                {
                    BonziBuddy.buddy.GenericShrine((ShrineType)type);
                }
            }
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(netId);
            writer.Write(type);
        }
    }
}
