using System;
using System.Collections.Generic;
using UnityEngine;
using R2API;
using R2API.Utils;
using RoR2;
using System.Text;
using IL.RoR2;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using RoR2.UI;
using UnityEngine.Events;

namespace MoistureUpset
{
    public static class UnReady
    {
        private static Transform HUDroot = null;

        private static GameObject UIGameObject;

        private static GameObject originalReadyButton;

        private static GameObject originalLockedInButton;

        private static bool changed = false;

        private static bool ready = false;

        public static void Init()
        {
            On.RoR2.UI.CharacterSelectController.Awake += CharacterSelectController_Awake;
            On.RoR2.UI.CharacterSelectController.Start += CharacterSelectController_Start;
            On.RoR2.UI.CharacterSelectController.IsClientReady += CharacterSelectController_IsClientReady;
            On.RoR2.UI.CharacterSelectController.Update += CharacterSelectController_Update;
            On.RoR2.UI.CharacterSelectController.ClientSetReady += CharacterSelectController_ClientSetReady;
        }

        private static void CharacterSelectController_Start(On.RoR2.UI.CharacterSelectController.orig_Start orig, CharacterSelectController self)
        {
            orig(self);
        }

        private static void CharacterSelectController_ClientSetReady(On.RoR2.UI.CharacterSelectController.orig_ClientSetReady orig, CharacterSelectController self)
        {
            orig(self);

            
        }

        private static void CharacterSelectController_Update(On.RoR2.UI.CharacterSelectController.orig_Update orig, CharacterSelectController self)
        {
            orig(self);

            originalLockedInButton = GameObject.Find("UnreadyButton");

            if (originalLockedInButton != null)
            {
                originalLockedInButton.GetComponentInChildren<HGTextMeshProUGUI>().SetText("Unready");

                if (!changed)
                {
                    //UnityEngine.Object.DestroyImmediate(originalLockedInButton.GetComponent<MPButton>());

                    MPButton unreadybutton = originalLockedInButton.GetComponent<MPButton>();

                    //Debug.Log(unreadybutton.allowAllEventSystems);

                    unreadybutton.allowAllEventSystems = true;
                    unreadybutton.disablePointerClick = false;

                    unreadybutton.enabled = true;

                    //Debug.Log(unreadybutton.allowAllEventSystems);

                    unreadybutton.onClick.AddListener(new UnityAction(bruh_moment));

                    foreach (var item in originalLockedInButton.GetComponentsInChildren<Component>())
                    {
                        //Debug.Log(item);
                    }

                    changed = true;
                }
                
            }
        }

        public static void bruh_moment()
        {
            //originalLockedInButton.gameObject.SetActive(false);
            originalReadyButton.gameObject.SetActive(true);

            changed = false;

            foreach (var item in RoR2.NetworkUser.readOnlyLocalPlayersList)
            {
                item.CallCmdSubmitVote(RoR2.PreGameController.instance.gameObject, -1);
            }
        }

        private static void CharacterSelectController_Awake(On.RoR2.UI.CharacterSelectController.orig_Awake orig, RoR2.UI.CharacterSelectController self)
        {
            orig(self);

            //HUDroot = self.transform.root;

            //GameObject localunready = Resources.Load<GameObject>("Prefabs/UI/DefaultMPButton");
            //UIGameObject = GameObject.Instantiate(localunready);
            //UIGameObject.transform.SetParent(HUDroot);
            //UIGameObject.GetComponent<RectTransform>().anchorMin = Vector2.zero;
            //UIGameObject.GetComponent<RectTransform>().anchorMax = Vector2.one; UIGameObject.GetComponent<RectTransform>().sizeDelta = Vector2.zero;

            //UIGameObject.SetActive(true);

            originalReadyButton = GameObject.Find("ReadyButton");


            changed = false;
        }

        private static bool CharacterSelectController_IsClientReady(On.RoR2.UI.CharacterSelectController.orig_IsClientReady orig, RoR2.UI.CharacterSelectController self)
        {

            bool iCR = orig(self);


            //if (iCR)
            //{
            //    self.readyButton.gameObject.SetActive(true);
            //    self.unreadyButton.gameObject.SetActive(false);
            //}

            return iCR;


        }
    }
}
