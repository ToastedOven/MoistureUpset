using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace MoistureUpset.Fixers
{
    class NyanFixer : MonoBehaviour
    {
        void OnDestroy()
        {
            GameObject g = new GameObject();
            g = GameObject.Instantiate(g);
            g.transform.position = gameObject.transform.position;
            AkSoundEngine.PostEvent("nyan", g);
        }
    }
}
