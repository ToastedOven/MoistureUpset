using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using BepInEx;
using R2API;
using R2API.Utils;
using RoR2;
using System.Linq;
using RewiredConsts;

namespace MoistureUpset
{
    public static class SoundNetworkAssistant
    {
        public static GameObject CentralNetworkObject;
        private static GameObject _centralNetworkObjectSpawned;

        private static NetworkedSoundComponent _nsc;

        private static NetworkUser[] users;

        public static void InitSNA()
        {
            var tempObject = new GameObject("moistUpsetTemp");

            tempObject.AddComponent<NetworkIdentity>();

            CentralNetworkObject = tempObject.InstantiateClone("MoistUpset Network Sound Manager");

            GameObject.Destroy(tempObject);

            _nsc = CentralNetworkObject.AddComponent<NetworkedSoundComponent>();

            CommandHelper.AddToConsoleWhenReady();
        }

        public static void playSound(string soundIDString, int index)
        {
            if (NetworkServer.active)
            {
                if (!_centralNetworkObjectSpawned)
                {
                    _centralNetworkObjectSpawned = UnityEngine.Object.Instantiate(CentralNetworkObject);
                    NetworkServer.Spawn(_centralNetworkObjectSpawned);
                }

                if (users == null)
                {
                    users = NetworkUser.readOnlyInstancesList.ToArray();
                }

                foreach (var user in users)
                {
                    NetworkedSoundComponent.Invoke(user, soundIDString, index);
                }
            }
        }

        public static void playSound(string soundIDString, NetworkIdentity identity)
        {
            if (NetworkServer.active)
            {
                if (!_centralNetworkObjectSpawned)
                {
                    _centralNetworkObjectSpawned = UnityEngine.Object.Instantiate(CentralNetworkObject);
                    NetworkServer.Spawn(_centralNetworkObjectSpawned);
                }

                if (users == null)
                {
                    users = NetworkUser.readOnlyInstancesList.ToArray();
                }

                foreach (var user in users)
                {
                    NetworkedSoundComponent.Invoke(user, soundIDString, identity);
                }
            }
        }
    }
}

internal class NetworkedSoundComponent : NetworkBehaviour
{
    private static NetworkedSoundComponent _instance;

    private static NetworkUser[] users;

    private void Awake()
    {
        _instance = this;
    }

    public static void Invoke(NetworkUser user, string soundIDString, int playerIndex)
    {
        _instance.TargetPlaySound(user.connectionToClient, soundIDString, playerIndex);
    }
    public static void Invoke(NetworkUser user, string soundIDString, NetworkIdentity identity)
    {
        _instance.TargetPlaySoundNetworkIdentity(user.connectionToClient, soundIDString, identity);
    }

    [TargetRpc]
    private void TargetPlaySound(NetworkConnection target, string soundIDString, int playerIndex)
    {
        if (users == null)
        {
            users = NetworkUser.readOnlyInstancesList.ToArray();
        }

        AkSoundEngine.PostEvent(soundIDString, users[playerIndex].master.GetBody().gameObject);
    }

    [TargetRpc]
    private void TargetPlaySoundNetworkIdentity(NetworkConnection target, string soundIDString, NetworkIdentity identity)
    {
        AkSoundEngine.PostEvent(soundIDString, identity.gameObject);
    }
}
