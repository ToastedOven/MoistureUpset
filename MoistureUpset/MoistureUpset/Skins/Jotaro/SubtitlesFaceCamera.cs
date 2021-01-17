using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MoistureUpset.Skins.Jotaro
{
    class SubtitlesFaceCamera : MonoBehaviour
    {
        private Camera camera;

        void Update()
        {
            if (camera == null)
            {
                camera = Camera.allCameras[0];
            }
            else
            {
                transform.LookAt(camera.transform);
                transform.Rotate(new Vector3(0, 180, 0), Space.Self);
                //DebugClass.Log($"p: {transform.position}, r: {transform.rotation}");  
            }
        }
    }
}
