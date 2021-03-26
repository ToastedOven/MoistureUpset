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
        PickupIndex item;
        Int32 count;
        List<PickupIndex> pickups = new List<PickupIndex>();

        public SyncMountain()
        {

        }

        public SyncMountain(PickupIndex item, int count)
        {
            this.item = item;
            this.count = count;
        }

        public void Deserialize(NetworkReader reader)
        {
            item = reader.ReadPickupIndex();
            count = reader.ReadInt32();
        }

        public void OnReceived()
        {
            pickups.Add(item);
            if (pickups.Count == count)
            {
                BonziBuddy.buddy.Mountain(pickups);
                pickups.Clear();
            }
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(item);
            writer.Write(count);
        }
    }
}
