using System;
using System.Collections.Generic;
using System.Text;
using Unity;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Networking;

namespace MoistureUpset
{
    class Helicopter : MonoBehaviour
    {
        GameObject fab = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/LunarWisp/LunarWispBody.prefab").WaitForCompletion();
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
