using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace MoistureUpset.Fixers
{
    class GoldTitanFixer : MonoBehaviour
    {
        void Start()
        {
            AkSoundEngine.PostEvent("OHSHITWADDUP", gameObject);
        }
    }
}
