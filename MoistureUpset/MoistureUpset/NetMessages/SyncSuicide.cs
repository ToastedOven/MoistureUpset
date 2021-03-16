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
    public class SyncSuicide : INetMessage
    {
        NetworkInstanceId netId;

        public SyncSuicide()
        {

        }

        public SyncSuicide(NetworkInstanceId netId)
        {
            this.netId = netId;
        }

        public void Deserialize(NetworkReader reader)
        {
            //DebugClass.Log($"POSITION: {reader.Position}, SIZE: {reader.Length}");

            netId = reader.ReadNetworkId();
        }

        public void OnReceived()
        {
            GameObject g = Util.FindNetworkObject(netId);
            if (g)
            {
                g.GetComponentInChildren<CharacterBody>().healthComponent.Suicide();
            }
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(netId);
        }
    }
}
