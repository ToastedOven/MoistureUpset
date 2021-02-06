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

public class mousechecker : MonoBehaviour
{
    public GameObject text;

    public List<GameObject> gameObjects = new List<GameObject>();

    public RoR2.UI.MPInput input = GameObject.Find("MPEventSystem Player0").GetComponent<RoR2.UI.MPInput>();
    public RoR2.UI.MPEventSystem events;
    GameObject selected;
    void Start()
    {
        selected = gameObjects[0];
        events = input.GetFieldValue<RoR2.UI.MPEventSystem>("eventSystem");
    }
    void Update()
    {
        Vector3 v = new Vector3(-10, 0, 0);
        if (transform.localPosition == v)
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
                    DebugClass.Log($"playing {selected.GetComponent<TextMeshProUGUI>().text}");
                    var bonemapper = (NetworkUser.readOnlyLocalPlayersList[0].master?.GetBody().modelLocator.modelTransform.GetComponentInChildren<BoneMapper>());
                    bonemapper.a2.Play(selected.GetComponent<TextMeshProUGUI>().text, -1, 0f);
                    bonemapper.a1.enabled = true;
                    bonemapper.a2.enabled = true;
                }
                catch (Exception e)
                {
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
