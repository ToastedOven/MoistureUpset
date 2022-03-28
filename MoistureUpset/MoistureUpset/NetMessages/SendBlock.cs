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
    public class SendBlock : INetMessage
    {
        NetworkInstanceId netId;
        Int32 position;
        Int32 matPosition;

        public SendBlock()
        {

        }

        public SendBlock(NetworkInstanceId netId, int pos, int matPos)
        {
            this.netId = netId;
            position = pos;
            matPosition = matPos;
        }

        public void Deserialize(NetworkReader reader)
        {
            //DebugClass.Log($"POSITION: {reader.Position}, SIZE: {reader.Length}");

            netId = reader.ReadNetworkId();
            position = reader.ReadInt32();
            matPosition = reader.ReadInt32();
        }

        public void OnReceived()
        {
            GameObject g = Util.FindNetworkObject(netId);
            var randomizer = g.GetComponent<BlockRandomizer>();
            randomizer.num = position;
            randomizer.matNum = matPosition;
            randomizer.Setup();
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(netId);
            writer.Write(position);
            writer.Write(matPosition);
        }
    }
}
