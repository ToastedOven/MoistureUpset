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

        [ConCommand(commandName = "debuglog_on_all", flags = ConVarFlags.ExecuteOnServer, helpText = "Logs a network message to all connected people")]
        private static void CCNetworkLog(ConCommandArgs args)
        {
            if (NetworkServer.active)
            {
                if (!_centralNetworkObjectSpawned)
                {
                    _centralNetworkObjectSpawned = UnityEngine.Object.Instantiate(CentralNetworkObject);
                    NetworkServer.Spawn(_centralNetworkObjectSpawned);
                }

                foreach (NetworkUser user in NetworkUser.readOnlyInstancesList)
                {
                    NetworkedSoundComponent.Invoke(user, string.Join(" ", args.userArgs));
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

    public static void Invoke(NetworkUser user, string msg)
    {
        _instance.TargetLog(user.connectionToClient, msg);
    }

    public static void Invoke(NetworkUser user, string soundIDString, int playerIndex)
    {
        _instance.TargetPlaySound(user.connectionToClient, soundIDString, playerIndex);
    }

    [TargetRpc]
    private void TargetLog(NetworkConnection target, string msg)
    {
        Debug.Log(msg);
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
}
