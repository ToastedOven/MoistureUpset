using RoR2;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace MoistureUpset.InteractReplacements.SodaBarrel
{
    class RandomizeSoda : MonoBehaviour
    {
        private Texture[] textures = { Resources.Load<Texture>("@MoistureUpset_InteractReplacements_SodaBarrel_sodaspritz:assets/sodafountain/cokacoon.png"), Resources.Load<Texture>("@MoistureUpset_InteractReplacements_SodaBarrel_sodaspritz:assets/sodafountain/drcoon.png"), Resources.Load<Texture>("@MoistureUpset_InteractReplacements_SodaBarrel_sodaspritz:assets/sodafountain/mtncoon.png"), Resources.Load<Texture>("@MoistureUpset_InteractReplacements_SodaBarrel_sodaspritz:assets/sodafountain/pepcoon.png"), Resources.Load<Texture>("@MoistureUpset_InteractReplacements_SodaBarrel_sodaspritz:assets/sodafountain/hicoon.png"), Resources.Load<Texture>("@MoistureUpset_InteractReplacements_SodaBarrel_sodaspritz:assets/sodafountain/spritecoon.png"), Resources.Load<Texture>("@MoistureUpset_InteractReplacements_SodaBarrel_sodaspritz:assets/sodafountain/fantacoon.png"), Resources.Load<Texture>("@MoistureUpset_InteractReplacements_SodaBarrel_sodaspritz:assets/sodafountain/watercoon.png"), Resources.Load<Texture>("@MoistureUpset_InteractReplacements_SodaBarrel_sodaspritz:assets/sodafountain/lioncoon.png") };
        private int currentTexture = 0;
        void Start()
        {
            currentTexture = UnityEngine.Random.Range(0, textures.Length);
            GetComponentInChildren<ModelLocator>().modelTransform.GetComponentInChildren<SkinnedMeshRenderer>().material.mainTexture = textures[currentTexture];
        }

        public Color getSpritzColor()
        {
            // UnsavedTrash didn't put the pixel color in the second texture, and I don't feel like loading up gimp. so it just copies the coca cola color.
            if (currentTexture == 1)
            {
                currentTexture = 0;
            }

            Texture2D texture2D = (Texture2D)textures[currentTexture];

            return texture2D.GetPixel(0, texture2D.height);
        }
    }
}
