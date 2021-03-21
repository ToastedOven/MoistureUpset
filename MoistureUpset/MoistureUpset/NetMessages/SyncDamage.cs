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
        NetworkInstanceId victim;

        public SyncDamage()
        {

        }

        public SyncDamage(NetworkInstanceId netId, DamageInfo attacker, NetworkInstanceId victim)
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
            victim = reader.ReadNetworkId();
        }

        public void OnReceived()
        {
            GameObject v = Util.FindNetworkObject(victim);
            if (info.attacker && v)
            {
                //Debug.Log($"--------{victim.name} took damage, {victim.GetComponentInChildren<HealthComponent>().health} health remaining");
                //health is new health, aka it can be lessthan or equal to 0
                var body = NetworkUser.readOnlyLocalPlayersList[0].master?.GetBody();
                if (v.GetComponentInChildren<CharacterBody>() == body || info.attacker.GetComponentInChildren<CharacterBody>() == body)
                {
                    BonziBuddy.buddy.Damage(v, info);
                }
            }
            else if (info.damageType == DamageType.FallDamage && v)
            {
                var body = NetworkUser.readOnlyLocalPlayersList[0].master?.GetBody();
                if (v.GetComponentInChildren<CharacterBody>() == body)
                {
                    BonziBuddy.buddy.FallDamage(v, info);
                }
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
