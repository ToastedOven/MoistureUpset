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
    public class SendSoda : INetMessage
    {
        NetworkInstanceId netId;
        Int32 position;

        public SendSoda()
        {

        }

        public SendSoda(NetworkInstanceId netId, int pos)
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
            var soda = g.GetComponent<RandomizeSoda>();
            soda.currentTexture = position;
            g.GetComponentInChildren<ModelLocator>().modelTransform.GetComponentInChildren<SkinnedMeshRenderer>().material.mainTexture = RandomizeSoda.textures[soda.currentTexture];
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(netId);
            writer.Write(position);
        }
    }
}
