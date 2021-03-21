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
    public class SyncMountain : INetMessage
    {
        string items;

        public SyncMountain()
        {

        }

        public SyncMountain(string items)
        {
            this.items = items;
        }

        public void Deserialize(NetworkReader reader)
        {
            items = reader.ReadString();
        }

        public void OnReceived()
        {
            BonziBuddy.buddy.Mountain(items.Split(' '));
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(items);
        }
    }
}
