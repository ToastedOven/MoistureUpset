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
    public class SyncDamage : INetMessage
    {
        NetworkInstanceId netId;
        DamageInfo info;
        GameObject victim;

        public SyncDamage()
        {

        }

        public SyncDamage(NetworkInstanceId netId, DamageInfo attacker, GameObject victim)
        {
            this.netId = netId;
            this.info = attacker;
            this.victim = victim;
        }

        public void Deserialize(NetworkReader reader)
        {
            //DebugClass.Log($"POSITION: {reader.Position}, SIZE: {reader.Length}");

            netId = reader.ReadNetworkId();
            info = reader.ReadDamageInfo();
            victim = reader.ReadGameObject();
        }

        public void OnReceived()
        {

            if (info.attacker && victim)
            {
                Debug.Log($"--------{victim.name} took damage, {victim.GetComponentInChildren<HealthComponent>().health} health remaining");
                //health is new health, aka it can be lessthan or equal to 0
            }
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(netId);
            writer.Write(info);
            writer.Write(victim);
        }
    }
}
