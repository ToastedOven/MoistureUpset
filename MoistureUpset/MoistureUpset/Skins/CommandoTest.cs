using R2API;
using R2API.Utils;
using RoR2;
using RoR2.Projectile;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace MoistureUpset.Skins
{
    public static class CommandoTest
    {
        private static readonly string Name = "Floppy Pancake";
        private static readonly string NameToken = "MOISTURE_UPSET_FLOPPY_PANCAKE";

        // Runs on Awake
        public static void Init()
        {
            PopulateAssets();
            On.RoR2.SurvivorCatalog.Init += RegisterSkin;
        }

        // Load assets here
        private static void PopulateAssets()
        {
            EnemyReplacements.LoadResource("moisture_puro");


            //GameObject fab = Resources.Load<GameObject>("prefabs/characterdisplays/CommandoDisplay");
            //fab.GetComponentInChildren<SkinnedMeshRenderer>().sharedMesh = Resources.Load<Mesh>("@MoistureUpset_test:assets/testing/commandomesh.mesh");
            //var boner = fab.AddComponent<DynamicBone>();
            //Transform b = fab.transform;
            //GameObject fab2 = Resources.Load<GameObject>("@MoistureUpset_test:assets/testing/bone.prefab");
            //Transform t = fab2.GetComponent<Transform>();
            //t.parent = b;
            //boner.m_Root = t;
            //foreach (var item in fab.GetComponentsInChildren<Component>())
            //{
            //    Debug.Log($"--------{item}");
            //}
        }

        // Skindef stuff here
        private static void RegisterSkin(On.RoR2.SurvivorCatalog.orig_Init orig)
        {
            orig();

            var survivorDef = SurvivorCatalog.GetSurvivorDef(SurvivorIndex.Commando);
            var bodyPrefab = survivorDef.bodyPrefab;

            var renderers = bodyPrefab.GetComponentsInChildren<Renderer>();
            var skinController = bodyPrefab.GetComponentInChildren<ModelSkinController>();

            var mdl = skinController.gameObject;

            var skin = new LoadoutAPI.SkinDefInfo
            {
                Icon = LoadoutAPI.CreateSkinIcon(Color.black, Color.white, new Color(0.69F, 0.19F, 0.65F, 1F), Color.yellow),
                Name = Name,
                NameToken = NameToken,
                RootObject = mdl,
                BaseSkins = new SkinDef[0],
                UnlockableName = "",
                GameObjectActivations = new SkinDef.GameObjectActivation[0],

                RendererInfos = new CharacterModel.RendererInfo[]
                {
                },

                MeshReplacements = new SkinDef.MeshReplacement[]
                {
                    new SkinDef.MeshReplacement
                    {
                        mesh = Resources.Load<Mesh>("@MoistureUpset_moisture_puro:assets/puro/puro.mesh"),
                        renderer = renderers[2]
                    },
                },
                ProjectileGhostReplacements = new SkinDef.ProjectileGhostReplacement[0],
                MinionSkinReplacements = new SkinDef.MinionSkinReplacement[0]
            };

            Array.Resize(ref skinController.skins, skinController.skins.Length + 1);
            skinController.skins[skinController.skins.Length - 1] = LoadoutAPI.CreateNewSkinDef(skin);

            var skinsField = Reflection.GetFieldValue<SkinDef[][]>(typeof(BodyCatalog), "skins");
            skinsField[BodyCatalog.FindBodyIndex(bodyPrefab)] = skinController.skins;
            Reflection.SetFieldValue(typeof(BodyCatalog), "skins", skinsField);

            LanguageAPI.Add(NameToken, Name);

            DebugClass.Log($"Adding skin: {Name}");



            List<Transform> t = new List<Transform>();
            foreach (var item in bodyPrefab.GetComponentsInChildren<SkinnedMeshRenderer>())
            {
                foreach (var bone in item.bones)
                {
                    t.Add(bone);
                }
            }
            var tail = Resources.Load<GameObject>("@MoistureUpset_moisture_puro:assets/puro/tailc.prefab");
            foreach (var boner in tail.GetComponentsInChildren<Transform>())
            {
                if (boner.name.Contains("_end"))
                {
                    continue;
                }
                t.Add(boner);
            }
            var earL = Resources.Load<GameObject>("@MoistureUpset_moisture_puro:assets/puro/earc.l.prefab");
            foreach (var boner in earL.GetComponentsInChildren<Transform>())
            {
                if (boner.name.Contains("_end"))
                {
                    continue;
                }
                t.Add(boner);
            }
            var earR = Resources.Load<GameObject>("@MoistureUpset_moisture_puro:assets/puro/earc.r.prefab");
            foreach (var boner in earR.GetComponentsInChildren<Transform>())
            {
                if (boner.name.Contains("_end"))
                {
                    continue;
                }
                t.Add(boner);
            }
            bodyPrefab.GetComponentInChildren<SkinnedMeshRenderer>().bones = t.ToArray();

            DynamicBone dynbone;
            var i = 0;
            Transform pelvis = bodyPrefab.GetComponentInChildren<SkinnedMeshRenderer>().bones[0];
            Transform head = bodyPrefab.GetComponentInChildren<SkinnedMeshRenderer>().bones[0];
            foreach (var item in bodyPrefab.GetComponentInChildren<SkinnedMeshRenderer>().bones)
            {
                if (item.name == "head")
                {
                    head = item;
                }
                if (item.name == "pelvis")
                {
                    pelvis = item;
                }
                if (item.name == "TailC")
                {
                    item.SetParent(pelvis);
                    item.transform.position = pelvis.position;
                    item.transform.rotation = Quaternion.identity;
                    item.transform.localRotation = new Quaternion(-.7f, .4f, .3f, -.5f);
                    item.transform.localPosition = new Vector3(.1f, .2f, 0);
                    //bodyPrefab.AddComponent<RotationTest>().t = item;
                    dynbone = bodyPrefab.AddComponent<DynamicBone>();
                    dynbone.m_Root = item.GetChild(0);
                }
                if (item.name == "EarC.R")
                {
                    item.SetParent(head);
                    item.transform.position = head.position;
                    item.transform.rotation = Quaternion.identity;
                    item.transform.localRotation = new Quaternion(-.1f, -.1f, .2f, 1);
                    dynbone = bodyPrefab.AddComponent<DynamicBone>();
                    dynbone.m_Root = item.GetChild(0);
                }
                if (item.name == "EarC.L")
                {
                    item.SetParent(head);
                    item.transform.position = head.position;
                    item.transform.rotation = Quaternion.identity;
                    item.transform.localRotation = new Quaternion(-.1f, -.1f, -.2f, 1);
                    dynbone = bodyPrefab.AddComponent<DynamicBone>();
                    dynbone.m_Root = item.GetChild(0);
                }
                //Debug.Log($"{i}--------{item}-----parent:{item.parent}");
                i++;
            }
        }
    }


    public class RotationTest : MonoBehaviour
    {
        public Transform t;
        void Update()
        {
            //transform.rotation is Identity 
            //on Space.Self aka local rotation
            //(0,0,1) == https://cdn.discordapp.com/attachments/483371638340059156/801122731763564564/unknown.png
            //(0,1,0) == https://cdn.discordapp.com/attachments/483371638340059156/801123449736265788/unknown.png
            //(1,0,0) == https://cdn.discordapp.com/attachments/483371638340059156/801123975492927528/unknown.png
            if (Input.GetKey(KeyCode.U))
            {
                t.Rotate(new Vector3(10 * Time.deltaTime, 0, 0), Space.Self);
            }
            if (Input.GetKey(KeyCode.J))
            {
                t.Rotate(new Vector3(-10 * Time.deltaTime, 0, 0), Space.Self);
            }
            if (Input.GetKey(KeyCode.I))
            {
                t.Rotate(new Vector3(0, 10 * Time.deltaTime, 0), Space.Self);
            }
            if (Input.GetKey(KeyCode.K))
            {
                t.Rotate(new Vector3(0, -10 * Time.deltaTime, 0), Space.Self);
            }
            if (Input.GetKey(KeyCode.O))
            {
                t.Rotate(new Vector3(0, 0, 10 * Time.deltaTime), Space.Self);
            }
            if (Input.GetKey(KeyCode.L))
            {
                t.Rotate(new Vector3(0, 0, -10 * Time.deltaTime), Space.Self);
            }

            if (Input.GetKey(KeyCode.R))
            {
                t.Translate(new Vector3(0, 1 * Time.deltaTime, 0), Space.World);
            }
            if (Input.GetKey(KeyCode.Y))
            {
                t.Translate(new Vector3(0, -1 * Time.deltaTime, 0), Space.World);
            }
            if (Input.GetKey(KeyCode.F))
            {
                t.Translate(new Vector3(0, 0, 1 * Time.deltaTime), Space.World);
            }
            if (Input.GetKey(KeyCode.H))
            {
                t.Translate(new Vector3(0, 0, -1 * Time.deltaTime), Space.World);
            }
            if (Input.GetKey(KeyCode.T))
            {
                t.Translate(new Vector3(1 * Time.deltaTime, 0, 0), Space.World);
            }
            if (Input.GetKey(KeyCode.G))
            {
                t.Translate(new Vector3(-1 * Time.deltaTime, 0, 0), Space.World);
            }


            if (Input.GetKey(KeyCode.Z))
            {
                Debug.Log($"--------{t.localRotation}");
            }
            if (Input.GetKey(KeyCode.X))
            {
                Debug.Log($"--------{t.localPosition}");
            }
        }
    }
}
