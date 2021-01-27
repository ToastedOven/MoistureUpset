using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace MoistureUpset.Fixers
{
    class spinnerfixer : MonoBehaviour
    {
        public float scale = 1;
        void Start()
        {
            transform.localScale = new Vector3(scale, scale, scale);
            //transform.eulerAngles = Vector3.zero;
            GetComponentInChildren<RoR2.Hologram.HologramProjector>().hologramPivot.localPosition = new Vector3(0, 3, 0);
        }
        void Update()
        {
            if (!GetComponentInChildren<RoR2.MultiShopController>().Networkavailable)
            {
                transform.Rotate(new Vector3(0, 1000 * Time.deltaTime, 0));
            }
        }
    }

    class terminalfixer : MonoBehaviour
    {
        public Vector3 center = Vector3.zero;
        void Update()
        {
            transform.RotateAround(center, new Vector3(0, 1, 0), 1000 * Time.deltaTime);
        }
    }

}
