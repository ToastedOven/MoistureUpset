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
                    new CharacterModel.RendererInfo
                    {
                        defaultMaterial = Resources.Load<Material>("@MoistureUpset_moisture_puro:assets/puro/puro.mat"),
                        defaultShadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On,
                        ignoreOverlays = false,
                        renderer = renderers[2]
                    },
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

            skin.RendererInfos[0].defaultMaterial = GameObject.Instantiate(Resources.Load<GameObject>("prefabs/characterbodies/CommandoBody")).GetComponentInChildren<SkinnedMeshRenderer>().material;
            skin.RendererInfos[0].defaultMaterial.mainTexture = Resources.Load<Material>("@MoistureUpset_moisture_puro:assets/puro/puro.mat").mainTexture;


            Array.Resize(ref skinController.skins, skinController.skins.Length + 1);
            skinController.skins[skinController.skins.Length - 1] = LoadoutAPI.CreateNewSkinDef(skin);

            var skinsField = Reflection.GetFieldValue<SkinDef[][]>(typeof(BodyCatalog), "skins");
            skinsField[BodyCatalog.FindBodyIndex(bodyPrefab)] = skinController.skins;
            Reflection.SetFieldValue(typeof(BodyCatalog), "skins", skinsField);

            LanguageAPI.Add(NameToken, Name);

            DebugClass.Log($"Adding skin: {Name}");


            BoneAdder.boneList = bodyPrefab.GetComponentInChildren<SkinnedMeshRenderer>().bones;
            BoneAdder.AddToBoneList("@MoistureUpset_moisture_puro:assets/puro/purobones.prefab", "purobones");
            bodyPrefab.GetComponentInChildren<SkinnedMeshRenderer>().bones = BoneAdder.boneList;
            BoneAdder.AppendAtoB("TailC", "pelvis");
            BoneAdder.AppendAtoB("EarC.R", "head");
            BoneAdder.AppendAtoB("EarC.L", "head");
            //BoneAdder.AttachCollider(bodyPrefab, .5f, .5f, DynamicBoneCollider.Direction.Y, DynamicBoneCollider.Bound.Outside);
            //BoneAdder.FinalizeColliders();
            //bodyPrefab.AddComponent<UseAllColliders>();

            BoneAdder.boneList = survivorDef.displayPrefab.GetComponentInChildren<SkinnedMeshRenderer>().bones;
            BoneAdder.AddToBoneList("@MoistureUpset_moisture_puro:assets/puro/purobonesdisplay.prefab", "purobonesdisplay");
            survivorDef.displayPrefab.GetComponentInChildren<SkinnedMeshRenderer>().bones = BoneAdder.boneList;
            BoneAdder.AppendAtoB("TailC", "pelvis");
            BoneAdder.AppendAtoB("EarC.R", "head");
            BoneAdder.AppendAtoB("EarC.L", "head");

            //GameObject animcontroller = Resources.Load<GameObject>("@MoistureUpset_moisture_puro:assets/puro/IGOTITPOGGGGG.prefab");
            //animcontroller.transform.parent = bodyPrefab.GetComponent<ModelLocator>().modelTransform;
            //animcontroller.transform.localPosition = Vector3.zero;
            //animcontroller.transform.localEulerAngles = Vector3.zero;
            //SkinnedMeshRenderer smr1 = animcontroller.GetComponentInChildren<SkinnedMeshRenderer>();
            //SkinnedMeshRenderer smr2 = bodyPrefab.GetComponent<ModelLocator>().modelTransform.GetComponentInChildren<SkinnedMeshRenderer>();
            //var test = animcontroller.AddComponent<BoneMapper>();
            //test.smr1 = smr1;
            //test.smr2 = smr2;
            //test.a1 = bodyPrefab.GetComponent<ModelLocator>().modelTransform.GetComponentInChildren<Animator>();
            //test.a2 = animcontroller.GetComponentInChildren<Animator>();
            //test.h = bodyPrefab.GetComponentInChildren<HealthComponent>();
        }
    }
}
