using MoistureUpset.Fixers;
using MoistureUpset.InteractReplacements.SodaBarrel;
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
    public class SendRoblox : INetMessage
    {
        NetworkInstanceId netId;
        Int32 position;

        public SendRoblox()
        {

        }

        public SendRoblox(NetworkInstanceId netId, int pos)
        {
            this.netId = netId;
            position = pos;
        }

        public void Deserialize(NetworkReader reader)
        {
            //DebugClass.Log($"POSITION: {reader.Position}, SIZE: {reader.Length}");

            netId = reader.ReadNetworkId();
            position = reader.ReadInt32();
        }

        public void OnReceived()
        {
            GameObject g = Util.FindNetworkObject(netId);
            var randomizer = g.GetComponent<GolemRandomizer>();
            randomizer.num = position;
            randomizer.Setup();
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(netId);
            writer.Write(position);
        }
    }
}
