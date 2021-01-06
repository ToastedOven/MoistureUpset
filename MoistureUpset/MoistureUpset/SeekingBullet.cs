using System;
using System.Collections.Generic;
using System.Text;
using Unity;
using UnityEngine;
using UnityEngine.Networking;

namespace MoistureUpset
{
    class SeekingBullet : MonoBehaviour
    {
        void Start()
        {
        }

        void Update()
        {
        }

        void OnDestroy()
        {
            RoR2.Audio.PointSoundManager.EmitSoundLocal(AkSoundEngine.GetIDFromString("PlayLightning"), transform.position);
        }
    }
}