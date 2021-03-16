using BepInEx;
using R2API.Utils;
using RoR2;
using R2API;
using R2API.MiscHelpers;
using System.Reflection;
using static R2API.SoundAPI;
using System;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Text;
using RiskOfOptions;
using TMPro;
using MoistureUpset.Skins;
using MoistureUpset;
using R2API.Networking.Interfaces;
using MoistureUpset.NetMessages;

public class mousechecker : MonoBehaviour
{
    public GameObject text;

    public List<GameObject> gameObjects = new List<GameObject>();

    public RoR2.UI.MPInput input = GameObject.Find("MPEventSystem Player0").GetComponent<RoR2.UI.MPInput>();
    public RoR2.UI.MPEventSystem events;
    GameObject selected;
    float XScale = 1, YScale = 1;
    void Start()
    {
        selected = gameObjects[0];
        events = input.GetFieldValue<RoR2.UI.MPEventSystem>("eventSystem");
    }
    void Update()
    {
        Vector3 v = new Vector3(0, 0, 0);
        if (transform.localPosition == v)
        {
            XScale = Screen.width / 1980f;
            YScale = Screen.height / 1080f;
            if (!(Math.Abs(Input.mousePosition.x - (Screen.width / 2.0f)) < 30f * XScale && Math.Abs(Input.mousePosition.y - (Screen.height / 2.0f)) < 30f * YScale))
            {
                float dist = 99999;
                foreach (var item in gameObjects)
                {
                    if (dist > Vector2.Distance(new Vector2(item.GetComponent<RectTransform>().localPosition.x + (Screen.width / 2), item.GetComponent<RectTransform>().localPosition.y + (Screen.height / 2)), (Vector2)Input.mousePosition))
                    {
                        dist = Vector2.Distance(new Vector2(item.GetComponent<RectTransform>().localPosition.x + (Screen.width / 2), item.GetComponent<RectTransform>().localPosition.y + (Screen.height / 2)), (Vector2)Input.mousePosition);
                        selected = item;
                    }
                    item.GetComponent<RectTransform>().localScale = new Vector3(0.6771638f, 0.6771638f, 0.6771638f);
                }
                selected.GetComponent<RectTransform>().localScale = new Vector3(0.9771638f, 0.9771638f, 0.9771638f);
            }
            else
            {
                selected.GetComponent<RectTransform>().localScale = new Vector3(0.6771638f, 0.6771638f, 0.6771638f);
            }
        }
        if (Input.GetKey(KeyCode.C))
        {
            if (transform.localPosition != v)
            {
                events.cursorOpenerForGamepadCount += 1;
                events.cursorOpenerCount += 1;
            }
            transform.localPosition = v; 
        }
        else
        {
            if (transform.localPosition == v)
            {
                try
                {
                    XScale = Screen.width / 1980f;
                    YScale = Screen.height / 1080f;
                    if (Math.Abs(Input.mousePosition.x - (Screen.width / 2.0f)) < 30f * XScale && Math.Abs(Input.mousePosition.y - (Screen.height / 2.0f)) < 30f * YScale)
                    {
                        var identity = NetworkUser.readOnlyLocalPlayersList[0].master?.GetBody().gameObject.GetComponent<NetworkIdentity>();

                        if (!NetworkServer.active)
                        {
                            new SyncAnimationToServer(identity.netId, "none").Send(R2API.Networking.NetworkDestination.Server);
                        }
                        else
                        {
                            new SyncAnimationToClients(identity.netId, "none").Send(R2API.Networking.NetworkDestination.Clients);

                            GameObject bodyObject = Util.FindNetworkObject(identity.netId);
                            bodyObject.GetComponent<ModelLocator>().modelTransform.GetComponentInChildren<BoneMapper>().PlayAnim("none");
                        }
                        
                    }
                    else
                    {
                        var identity = NetworkUser.readOnlyLocalPlayersList[0].master?.GetBody().gameObject.GetComponent<NetworkIdentity>();

                        if (!NetworkServer.active)
                        {
                            new SyncAnimationToServer(identity.netId, selected.GetComponentInChildren<TextMeshProUGUI>().text).Send(R2API.Networking.NetworkDestination.Server);
                        }
                        else
                        {
                            new SyncAnimationToClients(identity.netId, selected.GetComponentInChildren<TextMeshProUGUI>().text).Send(R2API.Networking.NetworkDestination.Clients);

                            GameObject bodyObject = Util.FindNetworkObject(identity.netId);
                            bodyObject.GetComponent<ModelLocator>().modelTransform.GetComponentInChildren<BoneMapper>().PlayAnim(selected.GetComponentInChildren<TextMeshProUGUI>().text);
                        }
                        
                    }
                }
                catch (Exception e)
                {
                    DebugClass.Log(e);
                }
                if (events.cursorOpenerForGamepadCount != 0)
                {
                    events.cursorOpenerForGamepadCount -= 1;
                    events.cursorOpenerCount -= 1;
                }
            }
            transform.localPosition = new Vector3(0, 2000, 0);
        }
    }
}
