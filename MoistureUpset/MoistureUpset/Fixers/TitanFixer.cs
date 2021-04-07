using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace MoistureUpset.Fixers
{
    class TitanFixer : MonoBehaviour
    {
        void Start()
        {
            var pre = gameObject;
            var yeet = pre.GetComponentInChildren<ParticleSystemRenderer>();
            yeet.material.mainTexture = Resources.Load<Texture>("@MoistureUpset_roblox:assets/robloxfist.png");
            try
            {
                int num = UnityEngine.Random.Range(0, 100);
                if (num == 99)
                {
                    yeet.mesh = Resources.Load<Mesh>("@MoistureUpset_roblox:assets/jj5x5.mesh");
                    AkSoundEngine.PostEvent("RobloxJJ5X5", gameObject);
                }
                else
                    switch (num % 7)
                    {
                        case 0:
                            yeet.mesh = Resources.Load<Mesh>("@MoistureUpset_roblox:assets/pizza.mesh");
                            AkSoundEngine.PostEvent("RobloxPizza", gameObject);
                            break;
                        case 1:
                            yeet.mesh = Resources.Load<Mesh>("@MoistureUpset_roblox:assets/sword.mesh");
                            AkSoundEngine.PostEvent("RobloxSword", gameObject);
                            break;
                        case 2:
                            yeet.mesh = Resources.Load<Mesh>("@MoistureUpset_roblox:assets/cola.mesh");
                            AkSoundEngine.PostEvent("RobloxCola", gameObject);
                            break;
                        case 3:
                            yeet.mesh = Resources.Load<Mesh>("@MoistureUpset_roblox:assets/cake.mesh");
                            AkSoundEngine.PostEvent("RobloxCake", gameObject);
                            break;
                        case 4:
                            yeet.mesh = Resources.Load<Mesh>("@MoistureUpset_roblox:assets/burger.mesh");
                            AkSoundEngine.PostEvent("RobloxBurger", gameObject);
                            break;
                        case 5:
                            yeet.mesh = Resources.Load<Mesh>("@MoistureUpset_roblox:assets/gravity.mesh");
                            AkSoundEngine.PostEvent("RobloxGravity", gameObject);
                            break;
                        case 6:
                            yeet.mesh = Resources.Load<Mesh>("@MoistureUpset_roblox:assets/robloxtaco.mesh");
                            AkSoundEngine.PostEvent("RobloxTaco", gameObject);
                            break;
                        default:
                            break;
                    }
                yeet.transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
            }
            catch (Exception e)
            {
                //Debug.Log(e);
            }
        }
    }
}
