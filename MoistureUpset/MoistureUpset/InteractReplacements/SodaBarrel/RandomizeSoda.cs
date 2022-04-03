using MoistureUpset.NetMessages;
using R2API.Networking.Interfaces;
using RoR2;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace MoistureUpset.InteractReplacements.SodaBarrel
{
    class RandomizeSoda : MonoBehaviour
    {
        internal static Texture[] textures;
        internal int currentTexture = 0;
        void Start()
        {
            if (NetworkServer.active)
            {
                currentTexture = UnityEngine.Random.Range(0, textures.Length);
                GetComponentInChildren<ModelLocator>().modelTransform.GetComponentInChildren<SkinnedMeshRenderer>().material.mainTexture = textures[currentTexture];
            }
            else
            {
                new RequestSoda(gameObject.GetComponent<NetworkIdentity>().netId).Send(R2API.Networking.NetworkDestination.Server);
            }
        }
        internal void Setup()
        {

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
