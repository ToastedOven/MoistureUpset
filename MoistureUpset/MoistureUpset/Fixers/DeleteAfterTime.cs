using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace MoistureUpset.Fixers
{
    class DeleteAfterTime : MonoBehaviour
    {
        public IEnumerator DeleteAfter(float time)
        {
            yield return new WaitForSeconds(time);
            GameObject.Destroy(this.gameObject);
        }
    }
}
