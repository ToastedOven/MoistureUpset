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
        public AK.Wwise.Event akevent;
        void Start()
        {
            //Debug.Log($"--------{GetComponentInChildren<RoR2.Projectile.ProjectileSteerTowardTarget>().rotationSpeed}");
            GetComponentInChildren<AkEvent>().data = akevent;
        }

        void Update()
        {
        }
    }
}