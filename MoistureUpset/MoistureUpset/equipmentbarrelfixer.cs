using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
namespace MoistureUpset
{
    class equipmentbarrelfixer : MonoBehaviour
    {
        Transform t;
        Vector3 prevPos;
        void Start()
        {
            t = GetComponentsInChildren<Transform>()[6];
            prevPos = t.localPosition;
        }

        void Update()
        {
            if (prevPos != t.localPosition)
            {
                prevPos = t.localPosition;
                t.Rotate(new Vector3(0, -280 * Time.deltaTime, 0));
            }
        }
    }
}
