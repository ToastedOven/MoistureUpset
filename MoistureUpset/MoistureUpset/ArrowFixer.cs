using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace MoistureUpset
{
    class ArrowFixer : MonoBehaviour
    {
        Vector3 prevPos = Vector3.zero;
        void Update()
        {
            transform.LookAt(transform.position + transform.position - prevPos);
            prevPos = transform.position;
        }
    }
    class ArrowTest : MonoBehaviour
    {
        void FixedUpdate()
        {
            //Debug.Log($"-----------{transform.rotation}");
            //Debug.Log($"-----{transform.localEulerAngles}");
        }
    }
}
