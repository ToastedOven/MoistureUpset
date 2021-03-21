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
    public class SyncBroke : INetMessage
    {
        NetworkInstanceId netId;
        public SyncBroke()
        {

        }

        public SyncBroke(NetworkInstanceId netId)
        {
            this.netId = netId;
        }

        public void Deserialize(NetworkReader reader)
        {
            netId = reader.ReadNetworkId();
        }

        public void OnReceived()
        {
            GameObject g = Util.FindNetworkObject(netId);
            if (g)
            {
                if (g.GetComponentInChildren<CharacterBody>() == NetworkUser.readOnlyLocalPlayersList[0].master?.GetBody())
                {
                    BonziBuddy.buddy.NotEnoughMoney();
                }
            }
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(netId);
        }
    }
}
