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
using System.Collections;

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
            if (CentralNetworkObject != null || _centralNetworkObjectSpawned != null)
            {
                GameObject.Destroy(CentralNetworkObject);
                GameObject.Destroy(_centralNetworkObjectSpawned);
                users = null;
            }

            var tempObject = new GameObject("moistUpsetTemp");

            tempObject.AddComponent<NetworkIdentity>();

            CentralNetworkObject = tempObject.InstantiateClone("MoistUpset Network Sound Manager");

            GameObject.Destroy(tempObject);

            _nsc = CentralNetworkObject.AddComponent<NetworkedSoundComponent>();
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

        public static void playSound(string soundIDString, Vector3 location)
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
                    NetworkedSoundComponent.Invoke(user, soundIDString, location);
                }
            }
        }
    }
}

internal class NetworkedSoundComponent : NetworkBehaviour
{
    private static NetworkedSoundComponent _instance;

    private static NetworkUser[] users;

    internal static GameObject soundPlayer;

    private void Awake()
    {
        _instance = this;
        soundPlayer = new GameObject("Temp Audio Player");
    }

    public static void Invoke(NetworkUser user, string soundIDString, int playerIndex)
    {
        _instance.TargetPlaySound(user.connectionToClient, soundIDString, playerIndex);
    }
    public static void Invoke(NetworkUser user, string soundIDString, NetworkIdentity identity)
    {
        _instance.TargetPlaySoundNetworkIdentity(user.connectionToClient, soundIDString, identity);
    }

    public static void Invoke(NetworkUser user, string soundIDString, Vector3 location)
    {
        _instance.TargetPlaySoundLocation(user.connectionToClient, soundIDString, location);
    }

    [TargetRpc]
    private void TargetPlaySound(NetworkConnection target, string soundIDString, int playerIndex)
    {
        if (users == null)
        {
            users = NetworkUser.readOnlyInstancesList.ToArray();
        }


        try
        {
            AkSoundEngine.PostEvent(soundIDString, users[playerIndex].master.GetBody().gameObject);
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
        
    }

    [TargetRpc]
    private void TargetPlaySoundNetworkIdentity(NetworkConnection target, string soundIDString, NetworkIdentity identity)
    {
        try
        {
            AkSoundEngine.PostEvent(soundIDString, identity.gameObject);
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
        
    }

    [TargetRpc]
    private void TargetPlaySoundLocation(NetworkConnection target, string soundIDString, Vector3 location)
    {
        try
        {
            GameObject tempAudio = Instantiate(soundPlayer, location, Quaternion.identity);

            AkSoundEngine.PostEvent(soundIDString, tempAudio);

            Destroy(tempAudio, 5f);
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }

    }
}
