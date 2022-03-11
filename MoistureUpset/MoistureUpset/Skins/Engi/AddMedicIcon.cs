using RoR2;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

namespace MoistureUpset.Skins.Engi
{
    class AddMedicIcon : MonoBehaviour
    {
        public GameObject MedicIcon;

        void Start()
        {
            GameObject MedicIconPrefab = Assets.Load<GameObject>("@MoistureUpset_Resources_medic:assets/misc/medic.prefab");

            MedicIcon = Instantiate<GameObject>(MedicIconPrefab, GetComponent<ModelLocator>().modelTransform.GetComponent<ChildLocator>().FindChild("Base"));

            MedicIcon.AddComponent<Jotaro.SubtitlesFaceCamera>();

            MedicIcon.SetActive(false);
        }
    }
}
