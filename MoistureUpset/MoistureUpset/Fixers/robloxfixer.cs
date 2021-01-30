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
        public bool waved = false, bought = false;
        void Update()
        {
            if (g.GetComponentsInChildren<Component>().Length != 1 && !waved && !bought)
            {
                waved = true;
                a.Play("Waving");
            }
            else if (g.GetComponentsInChildren<Component>().Length == 1)
            {
                waved = false;
            }
        }
    }
}
