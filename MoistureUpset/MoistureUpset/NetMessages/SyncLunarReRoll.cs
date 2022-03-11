using MoistureUpset.Skins;
using R2API.Networking.Interfaces;
using RoR2;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace MoistureUpset.NetMessages
{
    public class SyncLunarReRoll : INetMessage
    {
        Vector3 pos;
        public SyncLunarReRoll()
        {

        }

        public SyncLunarReRoll(Vector3 pos)
        {
            this.pos = pos;
        }

        public void Deserialize(NetworkReader reader)
        {
            pos = reader.ReadVector3();
        }

        public void OnReceived()
        {
            if (NetworkServer.active)
            {
                BonziBuddy.buddy.FreeCommand(pos);
            }
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(pos);
        }
    }
}
