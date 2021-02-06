using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace MoistureUpset.Fixers
{
    class robloxfixer : MonoBehaviour
    {
        public GameObject g;
        public Animator a;
        public bool waved = false, bought = false, standing = false;
        void Update()
        {
            if (g.GetComponentsInChildren<Component>().Length != 1 && !waved && !bought)
            {
                standing = false;
                waved = true;
                a.CrossFade("Waving", .1f);
            }
            else if (g.GetComponentsInChildren<Component>().Length == 1 && !standing && !bought)
            {
                a.CrossFade("Breathing Idle", .4f);
                standing = true;
                waved = false;
            }
        }
    }
}
