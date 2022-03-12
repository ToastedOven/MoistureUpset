using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace MoistureUpset.Fixers
{
    class GhastFixer : MonoBehaviour
    {
        Texture normal = Assets.Load<Texture>("@MoistureUpset_ghast:assets/ghastfire.png");
        GameObject explosion;
        void Start()
        {
            explosion = GameObject.Instantiate(Assets.Load<GameObject>("@MoistureUpset_ghast:assets/arbitraryfolder/explosion.prefab"));
            explosion.transform.position = this.transform.position;
            explosion.transform.localScale = new Vector3(2, 2, 2);
            var timer = explosion.AddComponent<DeleteAfterTime>();
            StartCoroutine(timer.DeleteAfter(1.95f));
        }
    }
}
