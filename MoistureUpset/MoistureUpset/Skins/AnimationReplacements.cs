using BepInEx;
using R2API.Utils;
using RoR2;
using R2API;
using R2API.MiscHelpers;
using System.Reflection;
using static R2API.SoundAPI;
using System;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Text;
using RiskOfOptions;
using TMPro;
using R2API.Networking.Interfaces;
using UnityEngine.Animations;
using UnityEngine.UI;

namespace MoistureUpset.Skins
{
    public static class AnimationReplacements
    {
        public static void RunAll()
        {
            EnemyReplacements.LoadResource("moisture_animationreplacements");
            ChangeAnims();
            On.RoR2.UI.HUD.Awake += (orig, self) =>
            {
                orig(self);
                //self.mainContainer.transform.Find("MainUIArea")



                GameObject g = GameObject.Instantiate(Resources.Load<GameObject>("@MoistureUpset_moisture_animationreplacements:assets/emotewheel/emotewheel.prefab"));
                foreach (var item in g.GetComponentsInChildren<TextMeshProUGUI>())
                {
                    item.font = self.mainContainer.transform.Find("MainUIArea").Find("SpringCanvas").Find("UpperLeftCluster").Find("MoneyRoot").Find("ValueText").GetComponent<TextMeshProUGUI>().font;
                    item.fontMaterial = self.mainContainer.transform.Find("MainUIArea").Find("SpringCanvas").Find("UpperLeftCluster").Find("MoneyRoot").Find("ValueText").GetComponent<TextMeshProUGUI>().fontMaterial;
                    item.fontSharedMaterial = self.mainContainer.transform.Find("MainUIArea").Find("SpringCanvas").Find("UpperLeftCluster").Find("MoneyRoot").Find("ValueText").GetComponent<TextMeshProUGUI>().fontSharedMaterial;
                }
                g.transform.SetParent(self.mainContainer.transform);
                g.transform.localPosition = new Vector3(0, 0, 0);
                //g.transform.localScale = new Vector3(self.GetComponent<CanvasScaler>().referenceResolution.x / Screen.width, self.GetComponent<CanvasScaler>().referenceResolution.y / Screen.height, 1);
                var s = g.AddComponent<mousechecker>();
                foreach (var item in g.GetComponentsInChildren<Transform>())
                {
                    if (item.gameObject.name.StartsWith("Emote"))
                    {
                        s.gameObjects.Add(item.gameObject);
                    }
                    if (item.gameObject.name.StartsWith("MousePos"))
                    {
                        s.text = item.gameObject;
                    }
                }
            };
        }
        public static bool setup = false;
        public static void ChangeAnims(/*SurvivorDef index, string resource*/)
        {
            //DebugClass.DebugBones("prefabs/characterbodies/HereticBody", 3);
            On.RoR2.SurvivorCatalog.Init += (orig) =>
            {
                orig();
                if (!setup)
                {
                    setup = true;
                    ApplyAnimationStuff(RoR2Content.Survivors.Croco, "@MoistureUpset_moisture_animationreplacements:assets/animationreplacements/acrid.prefab");
                    ApplyAnimationStuff(RoR2Content.Survivors.Mage, "@MoistureUpset_moisture_animationreplacements:assets/animationreplacements/artificer.prefab");
                    ApplyAnimationStuff(RoR2Content.Survivors.Captain, "@MoistureUpset_moisture_animationreplacements:assets/animationreplacements/captain.prefab");
                    ApplyAnimationStuff(RoR2Content.Survivors.Engi, "@MoistureUpset_moisture_animationreplacements:assets/animationreplacements/engi.prefab");
                    ApplyAnimationStuff(RoR2Content.Survivors.Loader, "@MoistureUpset_moisture_animationreplacements:assets/animationreplacements/loader.prefab");
                    ApplyAnimationStuff(RoR2Content.Survivors.Merc, "@MoistureUpset_moisture_animationreplacements:assets/animationreplacements/merc.prefab");
                    ApplyAnimationStuff(RoR2Content.Survivors.Toolbot, "@MoistureUpset_moisture_animationreplacements:assets/animationreplacements/mult.prefab");
                    ApplyAnimationStuff(RoR2Content.Survivors.Treebot, "@MoistureUpset_moisture_animationreplacements:assets/animationreplacements/rex.prefab");
                    ApplyAnimationStuff(RoR2Content.Survivors.Commando, "@MoistureUpset_moisture_animationreplacements:assets/animationreplacements/commando.prefab");
                    ApplyAnimationStuff(RoR2Content.Survivors.Huntress, "@MoistureUpset_moisture_animationreplacements:assets/animationreplacements/huntressBetterMaybeFixed.prefab");
                    ApplyAnimationStuff(RoR2Content.Survivors.Bandit2, "@MoistureUpset_moisture_animationreplacements:assets/animationreplacements/bandit.prefab");
                    ApplyAnimationStuff(Resources.Load<GameObject>("prefabs/characterbodies/HereticBody"), "@MoistureUpset_moisture_animationreplacements:assets/animationreplacements/heretic.prefab", 3);
                }

                //bodyPrefab = survivorDef.displayPrefab;
                //animcontroller = Resources.Load<GameObject>(resource);
                //animcontroller.transform.parent = bodyPrefab.GetComponent<ModelLocator>().modelTransform;
                //animcontroller.transform.localPosition = Vector3.zero;
                //animcontroller.transform.localEulerAngles = Vector3.zero;
                //smr1 = animcontroller.GetComponentInChildren<SkinnedMeshRenderer>();
                //smr2 = bodyPrefab.GetComponent<ModelLocator>().modelTransform.GetComponentInChildren<SkinnedMeshRenderer>();
                //test = animcontroller.AddComponent<BoneMapper>();
                //test.smr1 = smr1;
                //test.smr2 = smr2;
                //test.a1 = bodyPrefab.GetComponent<ModelLocator>().modelTransform.GetComponentInChildren<Animator>();
                //test.a2 = animcontroller.GetComponentInChildren<Animator>();
                //test.h = bodyPrefab.GetComponentInChildren<HealthComponent>();
            };
        }
        private static void ApplyAnimationStuff(SurvivorDef index, string resource)
        {
            var survivorDef = index;
            var bodyPrefab = survivorDef.bodyPrefab;
            GameObject animcontroller = Resources.Load<GameObject>(resource);
            animcontroller.transform.parent = bodyPrefab.GetComponent<ModelLocator>().modelTransform;
            animcontroller.transform.localPosition = Vector3.zero;
            animcontroller.transform.localEulerAngles = Vector3.zero;
            animcontroller.transform.localScale = Vector3.one;
            SkinnedMeshRenderer smr1 = animcontroller.GetComponentInChildren<SkinnedMeshRenderer>();
            SkinnedMeshRenderer smr2 = bodyPrefab.GetComponent<ModelLocator>().modelTransform.GetComponentInChildren<SkinnedMeshRenderer>();
            var test = animcontroller.AddComponent<BoneMapper>();
            test.smr1 = smr1;
            test.smr2 = smr2;
            test.a1 = bodyPrefab.GetComponent<ModelLocator>().modelTransform.GetComponentInChildren<Animator>();
            test.a2 = animcontroller.GetComponentInChildren<Animator>();
            test.h = bodyPrefab.GetComponentInChildren<HealthComponent>();
            test.model = bodyPrefab.GetComponent<ModelLocator>().modelTransform.gameObject;
        }
        private static void ApplyAnimationStuff(GameObject bodyPrefab, string resource, int pos = 0)
        {
            GameObject animcontroller = Resources.Load<GameObject>(resource);
            animcontroller.transform.parent = bodyPrefab.GetComponent<ModelLocator>().modelTransform;
            animcontroller.transform.localPosition = Vector3.zero;
            animcontroller.transform.localEulerAngles = Vector3.zero;
            animcontroller.transform.localScale = Vector3.one;
            SkinnedMeshRenderer smr1 = animcontroller.GetComponentsInChildren<SkinnedMeshRenderer>()[pos];
            SkinnedMeshRenderer smr2 = bodyPrefab.GetComponent<ModelLocator>().modelTransform.GetComponentsInChildren<SkinnedMeshRenderer>()[pos];
            var test = animcontroller.AddComponent<BoneMapper>();
            test.smr1 = smr1;
            test.smr2 = smr2;
            for (int i = 0; i < smr1.bones.Length; i++)
            {
                if (smr1.bones[i].name != smr2.bones[i].name)
                {
                    DebugClass.Log($"Fixing heretic bone order for emotes");
                    List<Transform> trans = new List<Transform>();
                    foreach (var item in smr2.bones)
                    {
                        foreach (var item2 in smr1.bones)
                        {
                            if (item.name == item2.name)
                            {
                                trans.Add(item2);
                            }
                        }
                    }
                    smr1.bones = trans.ToArray();
                    DebugClass.Log($"Done");
                    break;
                }
            }
            test.a1 = bodyPrefab.GetComponent<ModelLocator>().modelTransform.GetComponentInChildren<Animator>();
            test.a2 = animcontroller.GetComponentInChildren<Animator>();
            test.h = bodyPrefab.GetComponentInChildren<HealthComponent>();
            test.model = bodyPrefab.GetComponent<ModelLocator>().modelTransform.gameObject;
        }
    }
    public class BoneMapper : MonoBehaviour
    {
        public SkinnedMeshRenderer smr1, smr2;
        public Animator a1, a2;
        public HealthComponent h;
        public List<BonePair> pairs = new List<BonePair>();
        public float timer = 0;
        public static float caramellCount = 0;
        public static float caramellTimer = 0;
        public GameObject model;
        List<string> ignore = new List<string>();
        bool twopart = false;

        public void PlayAnim(string s)
        {
            a2.enabled = true;
            if (s == "Caramelldansen")
            {
                AkSoundEngine.PostEvent("StopEmotes", gameObject);
                AkSoundEngine.PostEvent("PlayCaramell", gameObject);
                if (a2.GetCurrentAnimatorStateInfo(0).IsName("Caramelldansen"))
                {
                    return;
                }
                caramellCount++;
                for (int i = 0; i < smr2.bones.Length; i++)
                {
                    try
                    {
                        if (smr2.bones[i].gameObject.GetComponent<ParentConstraint>())
                        {
                            smr2.bones[i].gameObject.GetComponent<ParentConstraint>().constraintActive = true;
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
                a2.PlayInFixedTime(s, -1, caramellTimer);
                twopart = false;
                return;
            }
            else
            {
                AkSoundEngine.PostEvent("StopCaramell", gameObject);
                AkSoundEngine.PostEvent("StopEmotes", gameObject);
                AkSoundEngine.PostEvent(s.Replace(" ", ""), gameObject);
                if (a2.GetCurrentAnimatorStateInfo(0).IsName("Caramelldansen"))
                {
                    caramellCount--;
                    if (caramellCount == 0)
                    {
                        caramellTimer = 0;
                    }
                }
            }
            for (int i = 0; i < smr2.bones.Length; i++)
            {
                try
                {
                    if (smr2.bones[i].gameObject.GetComponent<ParentConstraint>())
                    {
                        smr2.bones[i].gameObject.GetComponent<ParentConstraint>().constraintActive = true;
                    }
                }
                catch (Exception)
                {
                }
            }
            a2.Play(s, -1, 0f);
            twopart = false;
        }
        void AddIgnore(DynamicBone dynbone, Transform t)
        {
            for (int i = 0; i < t.childCount; i++)
            {
                if (!dynbone.m_Exclusions.Contains(t.GetChild(i)))
                {
                    ignore.Add(t.GetChild(i).name);
                    AddIgnore(dynbone, t.GetChild(i));
                }
            }
        }
        void Start()
        {
            foreach (var item in model.GetComponents<DynamicBone>())
            {
                try
                {
                    if (!item.m_Exclusions.Contains(item.m_Root))
                    {
                        ignore.Add(item.m_Root.name);
                    }
                    AddIgnore(item, item.m_Root);
                }
                catch (Exception)
                {
                }
            }
            if (model.name.StartsWith("mdlLoader"))
            {
                Transform LClav = model.transform, RClav = model.transform;
                foreach (var item in model.GetComponentsInChildren<Transform>())
                {
                    if (item.name == "clavicle.l")
                    {
                        LClav = item;
                        ignore.Add(LClav.name);
                    }
                    if (item.name == "clavicle.r")
                    {
                        RClav = item;
                        ignore.Add(RClav.name);
                    }
                }
                foreach (var item in LClav.GetComponentsInChildren<Transform>())
                {
                    ignore.Add(item.name);
                }
                foreach (var item in RClav.GetComponentsInChildren<Transform>())
                {
                    ignore.Add(item.name);
                }
            }
            for (int i = 0; i < smr2.bones.Length; i++)
            {
                try
                {
                    if (!ignore.Contains(smr2.bones[i].name))
                    {
                        var s = new ConstraintSource();
                        s.sourceTransform = smr1.bones[i];
                        s.weight = 1;
                        smr2.bones[i].gameObject.AddComponent<ParentConstraint>().AddSource(s);
                    }
                }
                catch (Exception)
                {
                }
            }
        }
        void Update()
        {
            if (caramellCount != 0)
            {
                caramellTimer += Time.deltaTime / caramellCount;
            }
            if (Input.GetKeyDown(KeyCode.Y))
            {
                a2.Play("none");
            }
            if (a2.GetCurrentAnimatorStateInfo(0).IsName("none"))
            {
                if (!twopart)
                {
                    twopart = true;
                }
                else
                {
                    if (a2.enabled)
                    {
                        AkSoundEngine.PostEvent("StopEmotes", gameObject);
                        AkSoundEngine.PostEvent("StopCaramell", gameObject);
                        for (int i = 0; i < smr2.bones.Length; i++)
                        {
                            try
                            {
                                if (smr2.bones[i].gameObject.GetComponent<ParentConstraint>())
                                {
                                    smr2.bones[i].gameObject.GetComponent<ParentConstraint>().constraintActive = false;
                                }
                            }
                            catch (Exception)
                            {
                                break;
                            }
                        }
                    }
                    a1.enabled = true;
                    a2.enabled = false;
                }
            }
            else
            {
                a1.enabled = false;
                twopart = false;
            }
            if (h.health <= 0)
            {
                AkSoundEngine.PostEvent("StopEmotes", gameObject);
                AkSoundEngine.PostEvent("StopCaramell", gameObject);
                for (int i = 0; i < smr2.bones.Length; i++)
                {
                    try
                    {
                        if (smr2.bones[i].gameObject.GetComponent<ParentConstraint>())
                        {
                            smr2.bones[i].gameObject.GetComponent<ParentConstraint>().constraintActive = false;
                        }
                    }
                    catch (Exception)
                    {
                        break;
                    }
                }
                GameObject.Destroy(gameObject);
            }
        }
    }
    public class BonePair
    {
        public Transform original, newiginal;
        public BonePair(Transform n, Transform o)
        {
            newiginal = n;
            original = o;
        }

        public void test()
        {

        }
    }

    public static class Pain
    {
        public static Transform FindBone(SkinnedMeshRenderer mr, string name)
        {
            foreach (var item in mr.bones)
            {
                if (item.name == name)
                {
                    return item;
                }
            }
            DebugClass.Log($"couldnt find bone [{name}]");
            return mr.bones[0];
        }

        public static Transform FindBone(List<Transform> bones, string name)
        {
            foreach (var item in bones)
            {
                if (item.name == name)
                {
                    return item;
                }
            }
            DebugClass.Log($"couldnt find bone [{name}]");
            return bones[0];
        }
    }
}
