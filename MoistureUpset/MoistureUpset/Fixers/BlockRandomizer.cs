using MoistureUpset.NetMessages;
using R2API.Networking.Interfaces;
using R2API.Utils;
using RoR2;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace MoistureUpset.Fixers
{
    class BlockRandomizer : MonoBehaviour
    {
        internal static List<Material> materials = new List<Material>();
        internal static List<Mesh> meshes = new List<Mesh>();
        internal int num;
        internal int matNum;
        SkinnedMeshRenderer mesh;
        bool check = true;
        bool needToSetup = true;
        void Start()
        {
            if (NetworkServer.active)
            {
                num = UnityEngine.Random.Range(0, meshes.Count);
                matNum = UnityEngine.Random.Range(0, materials.Count);
                Setup();
            }
            else
            {
                new RequestBlock(gameObject.GetComponent<NetworkIdentity>().netId).Send(R2API.Networking.NetworkDestination.Server);
            }
        }
        internal void Setup()
        {
            if (needToSetup)
            {
                needToSetup = false;
                GetComponentInChildren<ModelLocator>().modelTransform.gameObject.GetComponentInChildren<CharacterModel>().baseRendererInfos[0].defaultMaterial = materials[matNum];
                GetComponentInChildren<ModelLocator>().modelTransform.gameObject.GetComponentInChildren<CharacterModel>().baseRendererInfos[1].defaultMaterial = materials[matNum];
                mesh = gameObject.GetComponent<ModelLocator>().modelTransform.gameObject.GetComponentInChildren<SkinnedMeshRenderer>();
                mesh.sharedMesh = meshes[num];
            }
        }
        void Update()
        {
            if (check)
            {
                if (GetComponentInChildren<ModelLocator>().modelTransform.gameObject.GetComponentInChildren<CharacterModel>().baseRendererInfos[0].defaultMaterial != materials[matNum])
                {
                    GetComponentInChildren<ModelLocator>().modelTransform.gameObject.GetComponentInChildren<CharacterModel>().baseRendererInfos[0].defaultMaterial = materials[matNum];
                    GetComponentInChildren<ModelLocator>().modelTransform.gameObject.GetComponentInChildren<CharacterModel>().baseRendererInfos[1].defaultMaterial = materials[matNum];
                    check = false;
                }
            }
        }
    }
}