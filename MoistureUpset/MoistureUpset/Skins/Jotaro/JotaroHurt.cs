using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using RoR2;

namespace MoistureUpset.Skins.Jotaro
{
    class JotaroHurt : MonoBehaviour
    {
        private HealthComponent hc;
        private SkinnedMeshRenderer smr;
        private SkinnedMeshRenderer hurtsmr;

        private bool Hurt = false;

        void Start()
        {
            hc = GetComponentInChildren<HealthComponent>();
        }

        void FixedUpdate()
        {
            if (smr == null)
            {
                foreach (var item in GetComponentInChildren<ModelLocator>().modelTransform.GetComponentsInChildren<SkinnedMeshRenderer>())
                {
                    //DebugClass.Log($"{item.sharedMaterial.name}, {SkinHelper.skinNametoskinMeshName[JotaroCaptain.NameToken]}");
                    if (item.sharedMaterial.mainTexture.name.ToLower() == SkinHelper.skinNametoskinMeshName[JotaroCaptain.NameToken].ToLower())
                    {
                        smr = item;
                    }
                    else if (item.sharedMaterial.mainTexture.name.ToLower() == $"{SkinHelper.skinNametoskinMeshName[JotaroCaptain.NameToken]}hurt".ToLower())
                    {
                        hurtsmr = item;
                    }
                }
                if (hurtsmr != null)
                {
                    hurtsmr.gameObject.SetActive(false);
                }
            }

            if (hc.combinedHealthFraction < 0.3f && !Hurt)
            {
                smr.gameObject.SetActive(false);
                hurtsmr.gameObject.SetActive(true);
                AkSoundEngine.PostEvent("JotaroHurt", gameObject);
                Hurt = true;
            }
            else if (hc.combinedHealthFraction > 0.50f && Hurt)
            {
                hurtsmr.gameObject.SetActive(false);
                smr.gameObject.SetActive(true);
                Hurt = false;
            }
        }
    }
}
