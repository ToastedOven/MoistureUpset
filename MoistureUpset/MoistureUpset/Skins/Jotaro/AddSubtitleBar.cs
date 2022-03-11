using RoR2;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

namespace MoistureUpset.Skins.Jotaro
{
    class AddSubtitleBar : MonoBehaviour
    {
        public GameObject subtitleBar;
        void Start()
        {
            GameObject subtitleBarPrefab = Assets.Load<GameObject>("@MoistureUpset_Skins_Jotaro_jotarosubtitle:assets/jotaro/SubtitleHolder.prefab");

            subtitleBar = Instantiate<GameObject>(subtitleBarPrefab, GetComponent<ModelLocator>().modelTransform.GetComponent<ChildLocator>().FindChild("Base"));

            subtitleBar.AddComponent<SubtitlesFaceCamera>();
        }
    }
}
