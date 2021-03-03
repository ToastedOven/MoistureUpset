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
    public class SyncAnimation : INetMessage
    {
        NetworkInstanceId netId;
        string animation;

        public SyncAnimation()
        {

        }

        public SyncAnimation(NetworkInstanceId netId, string animation)
        {
            this.netId = netId;
            this.animation = animation;
        }

        public void Deserialize(NetworkReader reader)
        {
            DebugClass.Log($"POSITION: {reader.Position}, SIZE: {reader.Length}");

            netId = reader.ReadNetworkId();
            animation = reader.ReadString();
        }

        public void OnReceived()
        {
            GameObject bodyObject = Util.FindNetworkObject(netId);
            if (!bodyObject)
            {
                DebugClass.Log($"Body is null!!!");
            }

            DebugClass.Log($"Recieved message to play {animation} on client. Playing on {bodyObject.GetComponent<ModelLocator>().modelTransform}");
            //DebugClass.Log($"Client Body is {bodyObject.name}");
            bodyObject.GetComponent<ModelLocator>().modelTransform.GetComponentInChildren<BoneMapper>().PlayAnim(animation);
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(netId);
            writer.Write(animation);
        }
    }
}
