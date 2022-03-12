using RoR2;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace MoistureUpset.Fixers
{
    class GhastFixer : MonoBehaviour
    {
        Texture normal = Assets.Load<Texture>("@MoistureUpset_ghast:assets/ghastfire.png");
        GameObject explosion;
        void Start()
        {
            explosion = GameObject.Instantiate(Assets.Load<GameObject>("@MoistureUpset_ghast:assets/arbitraryfolder/explosion.prefab"));
            explosion.transform.position = this.transform.position;
            explosion.transform.localScale = new Vector3(2, 2, 2);
            var timer = explosion.AddComponent<DeleteAfterTime>();
            StartCoroutine(timer.DeleteAfter(1.9f));
        }
    }
    class GhastFixerButTheGhastNotTheFireballs : MonoBehaviour
    {
        static Material normal = UnityEngine.Object.Instantiate<Material>(Addressables.LoadAssetAsync<GameObject>("RoR2/Base/GreaterWisp/GreaterWispBody.prefab").WaitForCompletion().GetComponentInChildren<SkinnedMeshRenderer>().material);
        static Material shoot = UnityEngine.Object.Instantiate<Material>(Addressables.LoadAssetAsync<GameObject>("RoR2/Base/GreaterWisp/GreaterWispBody.prefab").WaitForCompletion().GetComponentInChildren<SkinnedMeshRenderer>().material);
        void Start()
        {
            normal.mainTexture = Assets.Load<Texture>("@MoistureUpset_ghast:assets/ghast.png");
            shoot.mainTexture = Assets.Load<Texture>("@MoistureUpset_ghast:assets/ghastfire.png");
        }
        public void Shot()
        {
            gameObject.GetComponentInChildren<ModelLocator>().modelTransform.gameObject.GetComponentInChildren<CharacterModel>().baseRendererInfos[0].defaultMaterial = shoot;
            StartCoroutine(yeet());
        }
        IEnumerator yeet()
        {
            yield return new WaitForSeconds(1f);
            gameObject.GetComponentInChildren<ModelLocator>().modelTransform.gameObject.GetComponentInChildren<CharacterModel>().baseRendererInfos[0].defaultMaterial = normal;
        }
    }
}
