using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using RoR2;
using System.Collections;

namespace MoistureUpset.Skins.Engi
{
    class EngiKillCam : MonoBehaviour
    {
        public CharacterBody attacker;

        private Camera camera;

        bool doing = false;

        void Start()
        {

        }

        void Update()
        {
            if (doing)
            {
                //GameObject.Find("Main Camera(Clone)").GetComponent<CameraRigController>().target = attacker.gameObject;
                camera.transform.LookAt(attacker.transform);
            }
        }

        public void DoKillCam()
        {
            if (!attacker) //  maybe it was suicide, I'm not sure.
            {
                DebugClass.Log($"No attacker?");
                return;
            }

            camera = GameObject.Find("Main Camera(Clone)").GetComponent<CameraRigController>().sceneCam;

            GameObject.Find("Main Camera(Clone)").GetComponent<CameraRigController>().enabled = false;
            

            doing = true;

            StartCoroutine(FreezeCam());
        }

        IEnumerator FreezeCam()
        {
            yield return new WaitForSeconds(3);
            var tex = ScreenCapture.CaptureScreenshotAsTexture();

            //Camera.main.clearFlags = CameraClearFlags.Nothing;
            //yield return null;
            //Camera.main.cullingMask = 0;

            Graphics.Blit(tex, camera.targetTexture);

            camera.Render();
        }

        public IEnumerator FreezeFrame(int frames)
        {
            //yield return null;
            while (frames > 0)
            {
                Camera.main.clearFlags = CameraClearFlags.Nothing;
                yield return null;
                Camera.main.cullingMask = 0;
                frames--;
            }

            Camera.main.clearFlags = CameraClearFlags.SolidColor;
            Camera.main.cullingMask = ~0;
        }

    }
}
