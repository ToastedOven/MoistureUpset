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
    public class RequestSoda : INetMessage
    {
        NetworkInstanceId netId;

        public RequestSoda()
        {

        }

        public RequestSoda(NetworkInstanceId netId)
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
            var randomizer = g.GetComponent<RandomizeSoda>();
            new SendSoda(netId, randomizer.currentTexture).Send(R2API.Networking.NetworkDestination.Clients);
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(netId);
        }
    }
}
