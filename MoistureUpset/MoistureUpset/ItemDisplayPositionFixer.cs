using RoR2;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace MoistureUpset
{
    public static class ItemDisplayPositionFixer
    {
        private static List<string> displays = new List<string>();
        public static void Init()
        {
            On.RoR2.ItemDisplay.SetVisibilityLevel += ItemDisplay_SetVisibilityLevel;
        }

        private static void ItemDisplay_SetVisibilityLevel(On.RoR2.ItemDisplay.orig_SetVisibilityLevel orig, ItemDisplay self, VisibilityLevel newVisibilityLevel)
        {
            orig(self, newVisibilityLevel);

            try
            {
                Transform parent = GetParent(self.gameObject.transform);

                if (parent != null)
                {
                    if (!displays.Contains($"{self.gameObject.name}, Path: {GetPathToParent(self.gameObject.transform)}, Skin Index: {parent.GetComponent<ModelSkinController>().currentSkinIndex}, position: {self.gameObject.transform.localPosition}, scale: {self.gameObject.transform.localScale}"))
                    {
                        displays.Add($"{self.gameObject.name}, Path: {GetPathToParent(self.gameObject.transform)}, Skin Index: {parent.GetComponent<ModelSkinController>().currentSkinIndex}, position: {self.gameObject.transform.localPosition}, scale: {self.gameObject.transform.localScale}");
                        Debug.Log($"{self.gameObject.name}, Path: {GetPathToParent(self.gameObject.transform)}, Skin Index: {parent.GetComponent<ModelSkinController>().currentSkinIndex}, position: {self.gameObject.transform.localPosition}, scale: {self.gameObject.transform.localScale}");
                    }
                }

                if (parent.GetComponent<ModelSkinController>().currentSkinIndex == 2 && parent.name == "mdlEngi")
                {
                    if (self.name == "DisplayWolfPelt(Clone)")
                    {
                        self.gameObject.transform.localPosition = new Vector3(0, 0.45f, 0.25f);
                        self.gameObject.transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);
                    }
                    else if (self.name == "DisplayMissileLauncher(Clone)")
                    {
                        self.gameObject.transform.localPosition = new Vector3(-0.074f, 0.559f, -0.362f);
                    }
                    else if (self.name == "DisplayGlasses(Clone)")
                    {
                        self.gameObject.transform.localPosition = new Vector3(-0.0019f, 0.4301f, 0.2229f);
                        self.gameObject.transform.localEulerAngles = new Vector3(-349.51f, 0, 0);
                        self.gameObject.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                    }
                    else if (self.name == "DisplaySteakCurved(Clone)")
                    {
                        self.gameObject.transform.localPosition = new Vector3(-0.158f, -0.251f, -0.341f);
                        self.gameObject.transform.localEulerAngles = new Vector3(47.971f, -109.439f, -25.023f);
                        self.gameObject.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                    }
                    else if (self.name == "DisplayWarbanner(Clone)")
                    {
                        self.gameObject.transform.localPosition = new Vector3(0.046f, 0.213f, -0.133f);
                        self.gameObject.transform.localEulerAngles = new Vector3(-90f, 0, 90f);
                        self.gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                    }
                    else if (self.name == "DisplayBrooch(Clone)")
                    {
                        self.gameObject.transform.localPosition = new Vector3(-0.0998f, 0.0842f, 0.176f);
                        self.gameObject.transform.localEulerAngles = new Vector3(74.32f, -19.18f, 0);
                        self.gameObject.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
                    }
                    else if (self.name == "DisplayShieldGenerator(Clone)")
                    {
                        self.gameObject.transform.localPosition = new Vector3(0, 0.2565f, 0.1201f);
                        self.gameObject.transform.localEulerAngles = new Vector3(-66.67f, 0, 0);
                        self.gameObject.transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);
                    }
                    else if (self.name == "DisplayHoof(Clone)")
                    {
                        self.gameObject.transform.localPosition = new Vector3(-0.059f, 0.155f, -0.175f);
                        self.gameObject.transform.localEulerAngles = new Vector3(180f, 0, 0);
                        self.gameObject.transform.localScale = new Vector3(0.09f, 0.09f, 0.09f);
                    }
                    else if (self.name == "DisplayScythe(Clone)")
                    {
                        self.gameObject.transform.localPosition = new Vector3(0.095f, 0.231f, -0.52f);
                        self.gameObject.transform.localEulerAngles = new Vector3(-53.836f, 76.72601f, -72.995f);
                        self.gameObject.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
                    }
                    else if (self.name == "DisplayUkulele(Clone)")
                    {
                        self.gameObject.transform.localPosition = new Vector3(-0.286f, -0.246f, -0.355f);
                        self.gameObject.transform.localEulerAngles = new Vector3(-8.4f, -86.51f, -12.2f);
                        self.gameObject.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
                    }
                    else if (self.name == "DisplayStealthkit(Clone)")
                    {
                        self.gameObject.transform.localPosition = new Vector3(0, -0.3532f, 0.2324f);
                        self.gameObject.transform.localEulerAngles = new Vector3(-79.43f, 0, 0);
                        self.gameObject.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
                    }
                    else if (self.name == "DisplayBandolier(Clone)")
                    {
                        self.gameObject.transform.localPosition = new Vector3(-0.006f, 0.015f, 0.004f);
                        self.gameObject.transform.localEulerAngles = new Vector3(-35.005f, 260.099f, -256.226f);
                        self.gameObject.transform.localScale = new Vector3(0.8584043f, 0.8584043f, 0.8584043f);
                    }
                    else if (self.name == "DisplayPauldron(Clone)")
                    {
                        self.gameObject.transform.localPosition = new Vector3(-0.432f, 0.159f, -0.052f);
                        self.gameObject.transform.localEulerAngles = new Vector3(-105.448f, 205.158f, -120.289f);
                        self.gameObject.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
                    }
                    else if (self.name == "DisplayInterstellarDeskPlant(Clone)")
                    {
                        self.gameObject.transform.localPosition = new Vector3(0.2452f, 0.15f, -0.154f);
                        self.gameObject.transform.localEulerAngles = new Vector3(90, 0, 0);
                        self.gameObject.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
                    }
                    else if (self.name == "DisplayClover(Clone)")
                    {
                        self.gameObject.transform.localPosition = new Vector3(0, 0.6163f, 0.089f);
                        self.gameObject.transform.localEulerAngles = new Vector3(0, 0, 0);
                        self.gameObject.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
                    }
                    else if (self.name == "DisplayMask(Clone)")
                    {
                        self.gameObject.transform.localPosition = new Vector3(0, 0.4361f, 0.2255f);
                        self.gameObject.transform.localEulerAngles = new Vector3(11.26f, 0, 0);
                        self.gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                    }
                    else if (self.name == "DisplayTeslaCoil(Clone)")
                    {
                        self.gameObject.transform.localPosition = new Vector3(0, 0.396f, -0.008f);
                        self.gameObject.transform.localEulerAngles = new Vector3(-45.24f, 0, 0);
                        self.gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                    }
                    else if (self.name == "DisplayWarhammer(Clone)")
                    {
                        self.gameObject.transform.localPosition = new Vector3(0.267f, -0.1719f, 0.0881f);
                        self.gameObject.transform.localEulerAngles = new Vector3(-60.839f, 3.967f, -100.761f);
                        self.gameObject.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                    }
                    else if (self.name == "DisplayBehemoth(Clone)")
                    {
                        self.gameObject.transform.localPosition = new Vector3(0.416f, -0.153f, -0.343f);
                        self.gameObject.transform.localEulerAngles = new Vector3(4.388f, 86.051f, 196.102f);
                        self.gameObject.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                    }
                    else if (self.name == "DisplayBoneCrown(Clone)")
                    {
                        self.gameObject.transform.localPosition = new Vector3(0, 0.459f, 0.1336f);
                        self.gameObject.transform.localEulerAngles = new Vector3(0, 0, 0);
                        self.gameObject.transform.localScale = new Vector3(1, 1, 1);
                    }
                    else if (self.name == "DisplayShieldBug(Clone)")
                    {
                        //self.gameObject.transform.localPosition = new Vector3(self.gameObject.transform.localPosition.x, self.gameObject.transform.localPosition.y - 0.1f, self.gameObject.transform.localPosition.z);
                    }
                    else if (self.name == "DisplayLunarDagger(Clone)")
                    {
                        self.gameObject.transform.localPosition = new Vector3(0, -0.05f, -0.543f);
                        self.gameObject.transform.localEulerAngles = new Vector3(-97.196f, 0, -1.525f);
                        self.gameObject.transform.localScale = new Vector3(1, 1, 1);
                    }
                }
            }
            catch (Exception e)
            {
                //self.gameObject.transform.localPosition = new Vector3();
                //self.gameObject.transform.localEulerAngles = new Vector3();
                //self.gameObject.transform.localScale = new Vector3();
            }
        }

        private static string GetPathToParent(Transform transform)
        {
            string path = "";

            Transform parent = transform.parent;

            path += $"{transform.name}";

            while (parent != null)
            {
                path += $"/{parent.name}";

                parent = parent.parent;
            }

            return path;
        }

        private static Transform GetParent(Transform transform)
        {
            Transform parent = transform.parent;

            Transform lastParent = null;

            while (parent != null)
            {
                lastParent = parent;
                parent = parent.parent;
            }

            return lastParent;
        }
    }
}
