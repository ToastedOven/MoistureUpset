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
    public class SyncFanFare : INetMessage
    {
        internal static string[] songs = { "fanfare1", "fanfare2", "fanfare3", "fanfare4", "fanfare5", "fanfare6", "fanfare7", "fanfare8", "fanfare9", "fanfare10", "fanfare11", "fanfare12" };

        Int32 position;

        public SyncFanFare()
        {

        }

        public SyncFanFare(int pos)
        {
            position = pos;
        }

        public void Deserialize(NetworkReader reader)
        {
            position = reader.ReadInt32();
        }

        public void OnReceived()
        {
            var thing = AkSoundEngine.PostEvent(songs[position], NetworkUser.readOnlyLocalPlayersList[0].master?.GetBody().gameObject);
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(position);
        }
    }
}
