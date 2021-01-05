using System;
using System.Collections.Generic;
using System.Text;
using Unity;
using UnityEngine;
using UnityEngine.Networking;

namespace MoistureUpset
{
    class Helicopter : MonoBehaviour
    {
        GameObject fab = Resources.Load<GameObject>("prefabs/characterbodies/LunarWispBody");
        Transform head;
        void Start()
        {
            var transforms = GetComponentsInChildren<Transform>();
            head = transforms[35];
        }

        void Update()
        {
            head.Rotate(new Vector3(0, 1500 * Time.deltaTime, 0));
        }
    }
}
