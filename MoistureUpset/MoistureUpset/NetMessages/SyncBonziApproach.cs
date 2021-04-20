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
    public class SyncBonziApproach : INetMessage
    {
        public static List<NetworkInstanceId> netIds = new List<NetworkInstanceId>();
        public static List<Int32> distances = new List<Int32>();
        NetworkInstanceId netId;
        Int32 distance;

        public SyncBonziApproach()
        {

        }

        public SyncBonziApproach(int distance, NetworkInstanceId netId)
        {
            this.distance = distance;
            this.netId = netId;
        }

        public void Deserialize(NetworkReader reader)
        {
            distance = reader.ReadInt32();
            netId = reader.ReadNetworkId();
        }

        public void OnReceived()
        {
            bool no = true;
            for (int i = 0; i < netIds.Count; i++)
            {
                if (netIds[i] == netId)
                {
                    distances[i] = distance;
                    no = false;
                    break;
                }
            }
            if (no)
            {
                netIds.Add(netId);
                distances.Add(distance);
            }
            int minDistance = 9999;
            foreach (var item in distances)
            {
                if (item < minDistance)
                {
                    minDistance = item;
                }
            }
            BonziBuddy.buddy.BonziApproach(minDistance);
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(distance);
            writer.Write(netId);
        }
    }
}
