using R2API.Utils;
using RoR2;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace MoistureUpset.Fixers
{
    class GolemRandomizer : MonoBehaviour
    {
        internal static List<Material> materials = new List<Material>();
        internal static List<Mesh> meshes = new List<Mesh>();
        int num;
        SkinnedMeshRenderer mesh;
        GameObject shoop;
        bool check = true;
        void Start()
        {
            mesh = gameObject.GetComponentInChildren<SkinnedMeshRenderer>();
            num = UnityEngine.Random.Range(0, meshes.Count);
            GetComponentInChildren<ModelLocator>().modelTransform.gameObject.GetComponentInChildren<CharacterModel>().baseRendererInfos[0].defaultMaterial = materials[num];
            mesh.sharedMesh = meshes[num];

            shoop = GameObject.Instantiate(Assets.Load<GameObject>("@MoistureUpset_noob:assets/robloxcharacters/shoop.prefab"));
            shoop.transform.parent = GetComponentInChildren<ModelLocator>().modelTransform.gameObject.GetComponentInChildren<CharacterModel>().transform.Find("GolemArmature").Find("ROOT").Find("base").Find("stomach").Find("chest").Find("head");
            shoop.transform.localScale = Vector3.one;
            if (num == 5)
            {
                shoop.transform.localPosition = new Vector3(0, .39f, .51f);
            }
            else
            {
                shoop.transform.localPosition = new Vector3(0, .39f, .11f);
            }
            shoop.transform.localEulerAngles = new Vector3(0, 270, 0);
            shoop.SetActive(false);
        }
        void Update()
        {
            if (check)
            {
                if (GetComponentInChildren<ModelLocator>().modelTransform.gameObject.GetComponentInChildren<CharacterModel>().baseRendererInfos[0].defaultMaterial != materials[num])
                {
                    GetComponentInChildren<ModelLocator>().modelTransform.gameObject.GetComponentInChildren<CharacterModel>().baseRendererInfos[0].defaultMaterial = materials[num];
                    check = false;
                    //StartCoroutine(removeShaderTing());
                }
            }
        }
        IEnumerator removeShaderTing()
        {
            yield return new WaitForSeconds(4f);
            Transform transform = GetComponentInChildren<ModelLocator>().modelTransform;
            if (transform)
            {
                transform.GetComponent<PrintController>().InvokeMethod("SetPrintThreshold", 12);
            }
            //string[] s = { "DITHER" };
            //GetComponentInChildren<ModelLocator>().modelTransform.gameObject.GetComponentInChildren<CharacterModel>().baseRendererInfos[0].defaultMaterial.shaderKeywords = s;
        }
        internal void Charge()
        {
            shoop.SetActive(true);
        }
        internal void Shoot()
        {
            shoop.SetActive(false);
        }
    }
}