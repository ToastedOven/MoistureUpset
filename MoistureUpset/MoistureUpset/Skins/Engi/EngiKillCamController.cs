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

        private CameraRigController _cameraRig;
        private Camera _camera;
        private EngiKillCam _killCam;
        private Texture2D _screenTex;
        private Material _screenMat;
        private bool _lookAt;

        private void Start()
        {
            _screenTex = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
            _screenMat = new Material(Addressables.LoadAssetAsync<Shader>($"RoR2/Base/Shaders/HGStandard.shader").WaitForCompletion());
        }

        private void Update()
        {
            if (_lookAt)
            {
                //GameObject.Find("Main Camera(Clone)").GetComponent<CameraRigController>().target = attacker.gameObject;
                _camera.transform.LookAt(attacker.transform);
            }
        }

        private void OnPostRenderCallback(Camera cam)
        {
            if (cam != _camera)
                return;
            
            _screenTex.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0, false);
            _screenTex.Apply();
            _screenMat.mainTexture = _screenTex;

            Camera.onPostRender -= OnPostRenderCallback;

            _killCam.freezeFrameMat = _screenMat;
            _killCam.showFreezeFrame = true;
        }

        public void DoKillCam()
        {
            if (!attacker) //  maybe it was suicide, I'm not sure.
            {
                DebugClass.Log($"No attacker?");
                return;
            }
            
            _cameraRig = GameObject.Find("Main Camera(Clone)").GetComponent<CameraRigController>();
            _camera = _cameraRig.sceneCam;
            _killCam = _camera.gameObject.AddComponent<EngiKillCam>();

            _cameraRig.enabled = false;

            _lookAt = true;

            StartCoroutine(FreezeFrame());
        }

        private IEnumerator FreezeFrame()
        {
            yield return new WaitForSeconds(2);

            Camera.onPostRender += OnPostRenderCallback;

            yield return new WaitForSeconds(3);
            _killCam.showFreezeFrame = false;
        }
    }
}
