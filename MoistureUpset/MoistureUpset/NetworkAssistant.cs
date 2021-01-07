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
using RiskOfOptions;

namespace MoistureUpset
{
    public static class NetworkAssistant
    {
        public static GameObject CentralNetworkObject;
        public static GameObject _centralNetworkObjectSpawned;

        private static MoistureUpsetNetworkedComponent _nsc;

        public static void InitSNA()
        {
            var tempObject = new GameObject("moistUpsetTemp");

            tempObject.AddComponent<NetworkIdentity>();

            CentralNetworkObject = tempObject.InstantiateClone("MoistUpset Network Sound Manager");

            GameObject.Destroy(tempObject);

            _nsc = CentralNetworkObject.AddComponent<MoistureUpsetNetworkedComponent>();
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

                foreach (var user in NetworkUser.readOnlyInstancesList)
                {
                    MoistureUpsetNetworkedComponent.Invoke(user, soundIDString, index);
                }
            }
        }

        public static void playSound(string soundIDString, NetworkInstanceId identity)
        {
            if (NetworkServer.active)
            {
                if (!_centralNetworkObjectSpawned)
                {
                    _centralNetworkObjectSpawned = UnityEngine.Object.Instantiate(CentralNetworkObject);
                    NetworkServer.Spawn(_centralNetworkObjectSpawned);
                }

                foreach (var user in NetworkUser.readOnlyInstancesList)
                {
                    MoistureUpsetNetworkedComponent.Invoke(user, soundIDString, identity);
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

                foreach (var user in NetworkUser.readOnlyInstancesList)
                {
                    MoistureUpsetNetworkedComponent.Invoke(user, soundIDString, location);
                }
            }
        }

        public static void fuckMe(Vector3 location)
        {
            if (NetworkServer.active)
            {
                if (!_centralNetworkObjectSpawned)
                {
                    _centralNetworkObjectSpawned = UnityEngine.Object.Instantiate(CentralNetworkObject);
                    NetworkServer.Spawn(_centralNetworkObjectSpawned);
                }

                foreach (var user in NetworkUser.readOnlyInstancesList)
                {
                    MoistureUpsetNetworkedComponent.Invoke(user, location);
                }
            }
        }
    }
}

public class MoistureUpsetNetworkedComponent : NetworkBehaviour
{
    internal static MoistureUpsetNetworkedComponent _instance;

    internal static GameObject soundPlayer;

    public static bool MineCraftHurt = true;

    private void Awake()
    {
        _instance = this;
        soundPlayer = new GameObject("Temp Audio Player");

        string temp = ModSettingsManager.getOptionValue("Minecraft Oof Sounds");

        if (temp == "1")
        {
            MineCraftHurt = true;
        }
        else
        {
            MineCraftHurt = false;
        }
    }

    public static void Invoke(NetworkUser user, string soundIDString, int playerIndex)
    {
        _instance.TargetPlaySound(user.connectionToClient, soundIDString, playerIndex);
    }
    public static void Invoke(NetworkUser user, string soundIDString, NetworkInstanceId identity)
    {
        _instance.TargetPlaySoundNetworkIdentity(user.connectionToClient, soundIDString, identity);
    }

    public static void Invoke(NetworkUser user, string soundIDString, Vector3 location)
    {
        _instance.TargetPlaySoundLocation(user.connectionToClient, soundIDString, location);
    }

    public static void Invoke(NetworkUser user, Vector3 location)
    {
        _instance.TargetGetPlayerLocation(user.connectionToClient, location);
    }

    [TargetRpc]
    private void TargetPlaySound(NetworkConnection target, string soundIDString, int playerIndex)
    {
        //MinecraftHurt <<< rune u gay?
        //Debug.Log($"--------{soundIDString}");

        if (soundIDString == "MinecraftHurt" && !MineCraftHurt)
        {
            return;
        }

        try
        {
            AkSoundEngine.PostEvent(soundIDString, NetworkUser.readOnlyInstancesList[playerIndex].master.GetBody().gameObject);
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
        
    }

    [TargetRpc]
    private void TargetPlaySoundNetworkIdentity(NetworkConnection target, string soundIDString, NetworkInstanceId identity)
    {
        try
        {
            AkSoundEngine.PostEvent(soundIDString, NetworkServer.FindLocalObject(identity));
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
        
    }

    [TargetRpc]
    private void TargetPlaySoundLocation(NetworkConnection target, string soundIDString, Vector3 location)
    {
        if ((soundIDString == "ChanceFailure" || soundIDString == "ChanceSuccess") && float.Parse(ModSettingsManager.getOptionValue("Shrine Changes")) != 1)
        {
            return;
        }
        if (soundIDString == "NoodleSplash" && float.Parse(ModSettingsManager.getOptionValue("Pool Noodle")) != 1)
        {
            return;
        }
        if (soundIDString == "PlayerDeath" && float.Parse(ModSettingsManager.getOptionValue("Player death sound")) != 1)
        {
            return;
        }
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

    [TargetRpc]
    private void TargetGetPlayerLocation(NetworkConnection target, Vector3 location)
    {
        foreach (var item in NetworkUser.readOnlyInstancesList)
        {
            if (item.master.GetBody().transform.position == location)
            {
                Debug.Log($"Player found at {location}");
            }
            else
            {
                Debug.Log($"Player not");
            }
        }
    }
}
