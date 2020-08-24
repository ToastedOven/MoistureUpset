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

namespace MoistureUpset
{
    public static class UnReady
    {
        private static Transform HUDroot = null;

        private static GameObject UIGameObject;

        private static bool ready = false;

        public static void Init()
        {
            On.RoR2.UI.CharacterSelectController.Awake += CharacterSelectController_Awake;
            On.RoR2.UI.CharacterSelectController.IsClientReady += CharacterSelectController_IsClientReady;
            On.RoR2.UI.CharacterSelectController.Update += CharacterSelectController_Update;
            On.RoR2.UI.CharacterSelectController.ClientSetReady += CharacterSelectController_ClientSetReady;
        }

        private static void CharacterSelectController_ClientSetReady(On.RoR2.UI.CharacterSelectController.orig_ClientSetReady orig, CharacterSelectController self)
        {
            orig(self);

            ready = true;
        }

        private static void CharacterSelectController_Update(On.RoR2.UI.CharacterSelectController.orig_Update orig, CharacterSelectController self)
        {
            orig(self);

        }

        private static void CharacterSelectController_Awake(On.RoR2.UI.CharacterSelectController.orig_Awake orig, RoR2.UI.CharacterSelectController self)
        {
            orig(self);

            HUDroot = self.transform.root;

            GameObject localunready = Resources.Load<GameObject>("Prefabs/UI/DefaultMPButton");
            UIGameObject = GameObject.Instantiate(localunready);
            UIGameObject.transform.SetParent(HUDroot);
            UIGameObject.GetComponent<RectTransform>().anchorMin = Vector2.zero;
            UIGameObject.GetComponent<RectTransform>().anchorMax = Vector2.one; UIGameObject.GetComponent<RectTransform>().sizeDelta = Vector2.zero;

            UIGameObject.SetActive(true);
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
