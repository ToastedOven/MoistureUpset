using System;
using UnityEngine;

namespace MoistureUpset.Skins.Engi
{
    [RequireComponent(typeof(Camera))]
    public class EngiKillCam : MonoBehaviour
    {
        public bool showFreezeFrame;
        public Material freezeFrameMat;
        public void OnRenderImage(RenderTexture src, RenderTexture dest)
        {
            if (!showFreezeFrame)
            {
                Graphics.Blit(src, dest);
                return;
            }

            Graphics.Blit(src, freezeFrameMat);
        }
    }
}