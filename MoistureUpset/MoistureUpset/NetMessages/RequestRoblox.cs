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
    public class RequestRoblox : INetMessage
    {
        NetworkInstanceId netId;

        public RequestRoblox()
        {

        }

        public RequestRoblox(NetworkInstanceId netId)
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
            var randomizer = g.GetComponent<GolemRandomizer>();
            new SendRoblox(netId, randomizer.num).Send(R2API.Networking.NetworkDestination.Clients);
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(netId);
        }
    }
}
