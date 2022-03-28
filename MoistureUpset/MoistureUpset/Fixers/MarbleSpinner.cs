using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace MoistureUpset.Fixers
{
    class MarbleSpinner : MonoBehaviour
    {
        int x, y;
        Vector3 prevVel = Vector3.one;
        Vector3 vel = Vector3.zero;
        Vector3 prevPos = Vector3.zero;
        Vector3 mag;

        void Start()
        {
            x = UnityEngine.Random.Range(500, 800);
            y = UnityEngine.Random.Range(500, 800);
        }
        void Update()
        {
            transform.Rotate(new Vector3(x * Time.deltaTime, y * Time.deltaTime, 0));
            vel = transform.position - prevPos;
            vel.Normalize();
            mag = vel - prevVel;
            if (mag.magnitude > .1f)
            {
                AkSoundEngine.PostEvent("marbleBounce", gameObject);
            }
            prevPos = transform.position;
            prevVel = vel;
        }
    }
}
