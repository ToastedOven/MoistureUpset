using RoR2;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace MoistureUpset.InteractReplacements.SodaBarrel
{
    class RandomizeSoda : MonoBehaviour
    {
        private Texture[] textures = { Resources.Load<Texture>("@MoistureUpset_InteractReplacements_SodaBarrel_sodaspritz:assets/sodafountain/cokacoon.png") };
        private int currentTexture = 0;
        void Start()
        {
            currentTexture = UnityEngine.Random.Range(0, textures.Length);
            GetComponentInChildren<ModelLocator>().modelTransform.GetComponentInChildren<SkinnedMeshRenderer>().material.mainTexture = textures[currentTexture];
        }

        public Color getSpritzColor()
        {
            Texture2D texture2D = (Texture2D)textures[currentTexture];

            return texture2D.GetPixel(0, texture2D.height);
        }
    }
}
