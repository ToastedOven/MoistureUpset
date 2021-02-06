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
using R2API.Networking.Interfaces;

namespace MoistureUpset.Skins
{
    public static class AnimationReplacements
    {
        public static void RunAll()
        {
            EnemyReplacements.LoadResource("moisture_animationreplacements");
            ChangeAnims(SurvivorIndex.Croco, "@MoistureUpset_moisture_animationreplacements:assets/animationreplacements/acrid.prefab");
            ChangeAnims(SurvivorIndex.Mage, "@MoistureUpset_moisture_animationreplacements:assets/animationreplacements/artificer.prefab");
            ChangeAnims(SurvivorIndex.Captain, "@MoistureUpset_moisture_animationreplacements:assets/animationreplacements/captain.prefab");
            ChangeAnims(SurvivorIndex.Engi, "@MoistureUpset_moisture_animationreplacements:assets/animationreplacements/engi.prefab");
            ChangeAnims(SurvivorIndex.Loader, "@MoistureUpset_moisture_animationreplacements:assets/animationreplacements/loader.prefab");
            ChangeAnims(SurvivorIndex.Merc, "@MoistureUpset_moisture_animationreplacements:assets/animationreplacements/merc.prefab");
            ChangeAnims(SurvivorIndex.Toolbot, "@MoistureUpset_moisture_animationreplacements:assets/animationreplacements/mult.prefab");
            ChangeAnims(SurvivorIndex.Treebot, "@MoistureUpset_moisture_animationreplacements:assets/animationreplacements/rex.prefab");
            ChangeAnims(SurvivorIndex.Commando, "@MoistureUpset_moisture_animationreplacements:assets/animationreplacements/commando.prefab");
            On.RoR2.UI.HUD.Awake += (orig, self) =>
            {
                orig(self);
                //self.mainContainer.transform.Find("MainUIArea")



                GameObject g = GameObject.Instantiate(Resources.Load<GameObject>("@MoistureUpset_moisture_animationreplacements:assets/emotewheel/emotewheel.prefab"));
                g.transform.SetParent(self.mainContainer.transform);
                g.transform.localPosition = new Vector3(0, 0, 0);
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
        public static void ChangeAnims(SurvivorIndex index, string resource)
        {
            On.RoR2.SurvivorCatalog.Init += (orig) =>
            {
                orig();
                var survivorDef = SurvivorCatalog.GetSurvivorDef(index);
                var bodyPrefab = survivorDef.bodyPrefab;
                GameObject animcontroller = Resources.Load<GameObject>(resource);
                animcontroller.transform.parent = bodyPrefab.GetComponent<ModelLocator>().modelTransform;
                animcontroller.transform.localPosition = Vector3.zero;
                animcontroller.transform.localEulerAngles = Vector3.zero;
                animcontroller.transform.localScale = Vector3.one
                ;
                SkinnedMeshRenderer smr1 = animcontroller.GetComponentInChildren<SkinnedMeshRenderer>();
                SkinnedMeshRenderer smr2 = bodyPrefab.GetComponent<ModelLocator>().modelTransform.GetComponentInChildren<SkinnedMeshRenderer>();
                var test = animcontroller.AddComponent<BoneMapper>();
                test.smr1 = smr1;
                test.smr2 = smr2;
                test.a1 = bodyPrefab.GetComponent<ModelLocator>().modelTransform.GetComponentInChildren<Animator>();
                test.a2 = animcontroller.GetComponentInChildren<Animator>();
                test.h = bodyPrefab.GetComponentInChildren<HealthComponent>();

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
    }
    public class BoneMapper : MonoBehaviour
    {
        public SkinnedMeshRenderer smr1, smr2;
        public Animator a1, a2;
        public HealthComponent h;
        public List<BonePair> pairs = new List<BonePair>();
        public List<Vector3> oldpos = new List<Vector3>();
        public List<Quaternion> oldrot = new List<Quaternion>();
        public List<Vector3> oldscale = new List<Vector3>();
        public float timer = 0;

        public void PlayAnim(string s)
        {
            a2.Play(s, -1, 0f);

            a1.enabled = true;
            a2.enabled = true;
        }
        void Start()
        {
        }
        void Update()
        {
            if (pairs.Count == 0 && a2.enabled)
            {
                if (a1.enabled)
                {
                    foreach (var item in smr2.bones)
                    {
                        oldpos.Add(item.position);
                        oldrot.Add(item.rotation);
                        oldscale.Add(item.localScale);
                    }
                }
                else if (oldpos.Count != 0)
                {
                    oldpos.Clear();
                }
                for (int i = 0; i < smr1.bones.Length; i++)
                {
                    try
                    {
                        smr2.bones[i].position = smr1.bones[i].position;
                        smr2.bones[i].rotation = smr1.bones[i].rotation;
                        smr2.bones[i].localScale = smr1.bones[i].localScale;
                    }
                    catch (Exception)
                    {
                        break;
                    }
                }
            }
            else
            {
                foreach (var item in pairs)
                {
                    item.original.position = item.newiginal.position;
                    item.original.rotation = item.newiginal.rotation;
                    item.original.localScale = item.newiginal.localScale;
                }
            }
            if (Input.GetKeyDown(KeyCode.F))
            {
                //a1.enabled = !a1.enabled;
                //if (!a1.enabled)
                //{
                //    a2.Play("dance", -1, 0f);
                //}

            }
            if (a2.GetCurrentAnimatorStateInfo(0).IsName("none"))
            {
                if (timer > 0)
                {
                    timer -= Time.deltaTime;
                    for (int i = 0; i < oldpos.Count; i++)
                    {
                        smr2.bones[i].position = Vector3.Lerp(smr2.bones[i].position, oldpos[i], Time.deltaTime);
                        smr2.bones[i].rotation = Quaternion.Lerp(smr2.bones[i].rotation, oldrot[i], Time.deltaTime);
                        smr2.bones[i].localScale = Vector3.Lerp(smr2.bones[i].localScale, oldscale[i], Time.deltaTime);
                    }
                    a2.enabled = false;
                }
                if (timer <= 0)
                {
                }
                    a1.enabled = true;
            }
            else
            {
                a1.enabled = false;
                timer = 1f;
            }
            if (h.health <= 0)
            {
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

    public class SyncAnimation : INetMessage
    {
        NetworkInstanceId netId;
        string animation;

        public SyncAnimation()
        {

        }

        public SyncAnimation(NetworkInstanceId netId, string animation)
        {
            this.netId = netId;
            this.animation = animation;
        }

        public void Deserialize(NetworkReader reader)
        {
            netId = reader.ReadNetworkId();
            animation = reader.ReadString();
        }

        public void OnReceived()
        {
            GameObject bodyObject = Util.FindNetworkObject(netId);
            if (!bodyObject)
            {
                DebugClass.Log($"Body is null!!!");
            }

            //DebugClass.Log($"Recieved message to play {animation} on client.");
            //DebugClass.Log($"Client Body is {bodyObject.name}");

            bodyObject.GetComponent<ModelLocator>().modelTransform.GetComponentInChildren<BoneMapper>().PlayAnim(animation);
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(netId);
            writer.Write(animation);
        }
    }
}
