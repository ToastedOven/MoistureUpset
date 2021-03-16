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
    public class SyncItems : INetMessage
    {
        NetworkInstanceId netId;
        ItemIndex index;
        Int32 count;

        public SyncItems()
        {

        }

        public SyncItems(NetworkInstanceId netId, ItemIndex index, int count)
        {
            this.netId = netId;
            this.index = index;
            this.count = count;
        }

        public void Deserialize(NetworkReader reader)
        {
            //DebugClass.Log($"POSITION: {reader.Position}, SIZE: {reader.Length}");

            netId = reader.ReadNetworkId();
            index = reader.ReadItemIndex();
            count = reader.ReadInt32();
        }

        public void OnReceived()
        {
            GameObject g = Util.FindNetworkObject(netId);
            if (g)
            {
                if (g.GetComponentInChildren<CharacterBody>() == NetworkUser.readOnlyLocalPlayersList[0].master?.GetBody())
                {
                    Inventory inventory = g.GetComponentInChildren<CharacterBody>().inventory;
                    BonziBuddy.buddy.Items(inventory, index, count, g);
                }
            }
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(netId);
            writer.Write(index);
            writer.Write(count);
        }
    }
}
