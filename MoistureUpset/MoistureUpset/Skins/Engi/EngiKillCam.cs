using System;
using System.Collections;
using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace MoistureUpset.Skins.Engi
{
    [RequireComponent(typeof(Camera))]
    public class EngiKillCam : MonoBehaviour
    {
        public float transitionTime = 2f;
        public float zoomTime = 0.2f;
        
        public CameraRigController rigController;
        public CharacterBody attacker;
        public Texture2D screenTexture;
        public Material freezeFrameMaterial;
        public AnimationCurve zoomCurve;
        public AnimationCurve transitionCurve;
        
        private bool _showFreezeFrame;
        private Camera _camera;

        public void Start()
        {
            zoomCurve = new AnimationCurve();
            zoomCurve.AddKey(0, 0);
            zoomCurve.AddKey(zoomTime, 1);

            //transitionCurve = AnimationCurve.Linear(0, 0, transitionTime, 1);
            transitionCurve = AnimationCurve.EaseInOut(0, 0, transitionTime, 1);

            _camera = GetComponent<Camera>();

            StartCoroutine(DoKillCam());
        }

        public void OnRenderImage(RenderTexture src, RenderTexture dest)
        {
            if (!_showFreezeFrame)
            {
                Graphics.Blit(src, dest);
                return;
            }

            Graphics.Blit(src, freezeFrameMaterial);
        }
        
        private void OnPostRenderCallback(Camera cam)
        {
            if (cam != _camera)
                return;
            
            screenTexture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0, false);
            screenTexture.Apply();
            freezeFrameMaterial.mainTexture = screenTexture;

            Camera.onPostRender -= OnPostRenderCallback;
            
            _showFreezeFrame = true;
            AkSoundEngine.PostEvent("DeathFreezeFrame", gameObject);
        }
        
        private IEnumerator DoKillCam()
        {
            var camTransform = _camera.transform;
            
            var startPos = camTransform.position;
            var startLookAt = startPos + camTransform.forward * 5f;
            
            // Move camera and rotate it towards attacker/
            float curveDeltaTime = 0;
            float percent;
            do
            {
                var direction = (startPos - AttackerPosition).normalized;
                var endPos = startPos + (direction * 3f + Vector3.up * 2f);

                var lookCenter = (startLookAt + AttackerPosition) / 2f;
                lookCenter -= Vector3.left;

                var relStartLook = startLookAt - lookCenter;
                var relEndLook = AttackerPosition - lookCenter;

                curveDeltaTime += Time.deltaTime;
                percent = transitionCurve.Evaluate(curveDeltaTime);

                camTransform.position = Vector3.Lerp(startPos, endPos, percent);

                camTransform.LookAt(Vector3.Slerp(relStartLook, relEndLook, percent) + camTransform.position);

                yield return new WaitForEndOfFrame();
            } while (percent != 1 && curveDeltaTime < transitionTime + 4f);

            // Zoom camera to attacker.
            startPos = camTransform.position;
            curveDeltaTime = 0;
            do
            {
                var direction = (startPos - AttackerPosition).normalized;
                var endPos = AttackerPosition + direction * 7f;
                
                curveDeltaTime += Time.deltaTime;
                percent = zoomCurve.Evaluate(curveDeltaTime);
                
                camTransform.position = Vector3.Lerp(startPos, endPos, percent);

                yield return new WaitForEndOfFrame();
            } while (percent != 1);

            // Do a freeze frame and wait 5 seconds.
            Camera.onPostRender += OnPostRenderCallback;
            yield return new WaitForSeconds(5);
            
            // Return everything back to normal.
            _showFreezeFrame = false;
            rigController.enabled = true;
        }

        private Vector3 _lastKnownAttacker = Vector3.zero;
        private Vector3 AttackerPosition
        {
            get
            {
                if (attacker)
                    _lastKnownAttacker = attacker.transform.position;
                
                return _lastKnownAttacker;
            }
        }
    }
}