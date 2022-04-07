using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using RoR2;
using System.Collections;
using UnityEngine.AddressableAssets;

namespace MoistureUpset.Skins.Engi
{
    internal class EngiKillCamController : MonoBehaviour
    {
        public CharacterBody attacker;

        private Texture2D _screenTex;
        private Material _screenMat;

        public void Start()
        {
            _screenTex = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
            _screenMat = new Material(Addressables.LoadAssetAsync<Shader>($"RoR2/Base/Shaders/HGStandard.shader").WaitForCompletion());
        }

        public void DoKillCam()
        {
            if (!attacker) //  maybe it was suicide, I'm not sure.
            {
                DebugClass.Log($"No attacker?");
                return;
            }
            
            var cameraRig = GameObject.Find("Main Camera(Clone)").GetComponent<CameraRigController>();
            var camera = cameraRig.sceneCam;
            
            var killCam = camera.gameObject.AddComponent<EngiKillCam>();
            killCam.rigController = cameraRig;
            killCam.attacker = attacker;
            killCam.screenTexture = _screenTex;
            killCam.freezeFrameMaterial = _screenMat;

            cameraRig.enabled = false;
        }
    }
}
