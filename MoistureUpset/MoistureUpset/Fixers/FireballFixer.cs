using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace MoistureUpset.Fixers
{
    class FireballFixer : MonoBehaviour
    {
        private void Start()
        {
            var charge = GameObject.Instantiate(Assets.Load<GameObject>("@MoistureUpset_ghast:assets/firecharge.prefab"));
            charge.transform.parent = this.transform;
            charge.transform.localPosition = Vector3.zero;
        }
    }
}
