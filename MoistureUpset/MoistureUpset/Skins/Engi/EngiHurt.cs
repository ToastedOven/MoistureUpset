using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using RoR2;

namespace MoistureUpset.Skins.Engi
{
    class EngiHurt : MonoBehaviour
    {
        private HealthComponent hc;
        private GameObject medicIcon;

        private bool Hurt = false;

        void Start()
        {
            hc = GetComponentInChildren<HealthComponent>();
        }

        void FixedUpdate()
        {
            if (!medicIcon)
            {
                medicIcon = GetComponentInChildren<AddMedicIcon>().MedicIcon;
                return;
            }

            if (hc.combinedHealthFraction < 0.3f && !Hurt)
            {
                medicIcon.gameObject.SetActive(true);
                Hurt = true;
            }
            else if (hc.combinedHealthFraction > 0.50f && Hurt)
            {
                medicIcon.gameObject.SetActive(false);
                Hurt = false;
            }
        }
    }
}
