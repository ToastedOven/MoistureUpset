using System;
using System.Collections.Generic;
using UnityEngine;
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

        private static MPButton localunreadyButton;

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

            UIGameObject = new GameObject("Moist Upset UI GameObject");

            UIGameObject.transform.SetParent(HUDroot);

            localunreadyButton = GameObject.Instantiate<MPButton>(self.readyButton);
        }

        private static bool CharacterSelectController_IsClientReady(On.RoR2.UI.CharacterSelectController.orig_IsClientReady orig, RoR2.UI.CharacterSelectController self)
        {
            bool iCR = orig(self);


            if (iCR)
            {
                self.readyButton.gameObject.SetActive(true);
                self.unreadyButton.gameObject.SetActive(false);
            }

            return false;
        }
    }
}
