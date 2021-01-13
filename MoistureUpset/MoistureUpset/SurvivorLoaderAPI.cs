using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using RoR2;
using R2API;
using R2API.Utils;
using R2API.AssetPlus;
using UnityEngine;
using System.Linq;
using static RoR2.UI.CharacterSelectController;
using EntityStates.BrotherMonster;
using EntityStates.Engi.EngiBubbleShield;
using RoR2.Projectile;

namespace MoistureUpset
{
    // Definently need to clean this up later.
    // Moved all Skin related stuff into a more origanized format. everything else here needs to be moved somewhere else
    public static class SurvivorLoaderAPI // Why we call this class "SurvivorLoaderAPI" is beyond me, we originally wanted this to be some easy functions to call to add skins. and now it's filled with stuff mostly for the Engi skin and stuff that isn't even skin related.
    {
        public static void LoadSurvivors()
        {
            PopulateAssets();
        }

        private static void PopulateAssets()
        {

            using (var assetStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("MoistureUpset.robloxfont"))
            {
                var MainAssetBundle = AssetBundle.LoadFromStream(assetStream);

                ResourcesAPI.AddProvider(new AssetBundleResourcesProvider("@MoistureUpset_robloxfont", MainAssetBundle));
            }

            using (var assetStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("MoistureUpset.fortnite"))
            {
                var MainAssetBundle = AssetBundle.LoadFromStream(assetStream);

                ResourcesAPI.AddProvider(new AssetBundleResourcesProvider("@MoistureUpset_fortnite", MainAssetBundle));
            }

            using (var assetStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("MoistureUpset.Models.chips"))
            {
                var MainAssetBundle = AssetBundle.LoadFromStream(assetStream);

                ResourcesAPI.AddProvider(new AssetBundleResourcesProvider("@MoistureUpset_chips", MainAssetBundle));
            }
            using (var assetStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("MoistureUpset.unifiedturret"))
            {
                var MainAssetBundle = AssetBundle.LoadFromStream(assetStream);

                ResourcesAPI.AddProvider(new AssetBundleResourcesProvider("@MoistureUpset_engi_turret", MainAssetBundle));
            }

            using (var assetStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("MoistureUpset.mobile"))
            {
                var MainAssetBundle = AssetBundle.LoadFromStream(assetStream);

                ResourcesAPI.AddProvider(new AssetBundleResourcesProvider("@MoistureUpset_engi_turret2", MainAssetBundle));
            }

            //using (var assetStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("MoistureUpset.sentry2"))
            //{
            //    var MainAssetBundle = AssetBundle.LoadFromStream(assetStream);

            //    ResourcesAPI.AddProvider(new AssetBundleResourcesProvider("@MoistureUpset_engi_sentry2", MainAssetBundle));
            //}

            


            On.RoR2.SurvivorCatalog.Init += SurvivorCatalog_Init;

            On.EntityStates.Engi.EngiWeapon.PlaceTurret.OnEnter += PlaceTurret_OnEnter;

            

        }

        



        // Gotta do this jank mess in order to make the blueprint look like the custom skinned turrets, but it works.
        private static void PlaceTurret_OnEnter(On.EntityStates.Engi.EngiWeapon.PlaceTurret.orig_OnEnter orig, EntityStates.Engi.EngiWeapon.PlaceTurret self)
        {
            var cb = self.outer.GetComponentInChildren<CharacterBody>();

            GameObject tempPrefab = (GameObject)null;

            if (cb.isSkin("THE_TF2_ENGINEER_SKIN"))
            {
                if (self.blueprintPrefab != null)
                {
                    tempPrefab = GameObject.Instantiate<GameObject>(self.blueprintPrefab);

                    switch (self.blueprintPrefab.name)
                    {
                        case "EngiTurretBlueprints":
                            tempPrefab.GetComponentInChildren<SkinnedMeshRenderer>().sharedMesh = Resources.Load<Mesh>("@MoistureUpset_engi_turret:assets/normal_sentry.mesh");
                            break;
                        case "EngiWalkerTurretBlueprints":
                            tempPrefab.GetComponentInChildren<SkinnedMeshRenderer>().sharedMesh = Resources.Load<Mesh>("@MoistureUpset_engi_turret:assets/walker_turret.mesh");
                            break;
                    }

                    self.blueprintPrefab = tempPrefab;
                }
                else
                {
                    //Debug.LogWarning("is null");
                }

            }

            orig(self);

            if (tempPrefab != null)
            {
                GameObject.Destroy(tempPrefab);
            }
        }

        private static void SurvivorCatalog_Init(On.RoR2.SurvivorCatalog.orig_Init orig)
        {
            orig();

            //EngineerStuff("The Engineer", "THE_TF2_ENGINEER_SKIN", "@MoistureUpset_engi:assets/models_player_engineer_engineer_red.mat", "@MoistureUpset_oopsideletedtheoldresource:assets/engi.mesh", RoR2.SurvivorIndex.Engi);
            EditDropPod();


            foreach (var item in SurvivorCatalog.allSurvivorDefs)
            {
                item.bodyPrefab.AddComponent<SkinReloader>();
            }
        }
        private static void EditDropPod()
        {
            //On.RoR2.UI.LogBook.LogBookController.GetMonsterStatus += (orig, profile, entry) =>
            //{
            //    return RoR2.UI.LogBook.EntryStatus.Available;
            //};
        }
        //private static void EngineerStuff(string _name, string _nameToken, string _mat1, string _mesh1, RoR2.SurvivorIndex _survivorIndex)
        //{
        //    var survivorDef = SurvivorCatalog.GetSurvivorDef(_survivorIndex);
        //    var bodyPrefab = survivorDef.bodyPrefab;

        //    var engiTurretBodyPrefab = BodyCatalog.GetBodyPrefab(36);
        //    var engiTurretBodyRenderer = engiTurretBodyPrefab.GetComponentsInChildren<Renderer>();

        //    var engiWalkerTurretBodyPrefab = BodyCatalog.GetBodyPrefab(37);
        //    var engiWalkerTurretBodyRenderer = engiWalkerTurretBodyPrefab.GetComponentsInChildren<Renderer>();

        //    var renderers = bodyPrefab.GetComponentsInChildren<Renderer>();
        //    var skinController = bodyPrefab.GetComponentInChildren<ModelSkinController>();

        //    var mdl = skinController.gameObject;

        //    var TurretSkinController = engiTurretBodyPrefab.GetComponentInChildren<ModelSkinController>();
        //    var WalkerTurretSkinController = engiWalkerTurretBodyPrefab.GetComponentInChildren<ModelSkinController>();

        //    var turretSkinDef = new LoadoutAPI.SkinDefInfo
        //    {
        //        Icon = LoadoutAPI.CreateSkinIcon(new Color(.75f, .14f, .37f, 1f), new Color(.003f, .05f, .14f, 1f), new Color(.25f, .04f, .15f, 1f), new Color(.96f, .66f, .45f, 1f)),
        //        Name = "Level 2 Sentry",
        //        NameToken = "EngiTurretBody",
        //        RootObject = TurretSkinController.gameObject,
        //        BaseSkins = new SkinDef[0],
        //        UnlockableName = "",
        //        GameObjectActivations = new SkinDef.GameObjectActivation[0],

        //        RendererInfos = new CharacterModel.RendererInfo[]
        //        {
        //            new CharacterModel.RendererInfo
        //            {
        //                defaultMaterial = Resources.Load<Material>("@MoistureUpset_engi_turret:assets/unifiedtex.mat"),
        //                defaultShadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On,
        //                ignoreOverlays = false,
        //                renderer = engiTurretBodyRenderer[0]
        //            }

        //        },
        //        MeshReplacements = new SkinDef.MeshReplacement[]
        //        {
        //            new SkinDef.MeshReplacement
        //            {
        //                mesh = Resources.Load<Mesh>("@MoistureUpset_engi_turret:assets/normal_sentry.mesh"),
        //                renderer = engiTurretBodyRenderer[0]
        //            }
        //        },
        //        ProjectileGhostReplacements = new SkinDef.ProjectileGhostReplacement[0],
        //        MinionSkinReplacements = new SkinDef.MinionSkinReplacement[0]
        //    };

        //    var walkerTurretSkinDef = new LoadoutAPI.SkinDefInfo
        //    {
        //        Icon = LoadoutAPI.CreateSkinIcon(new Color(.75f, .14f, .37f, 1f), new Color(.003f, .05f, .14f, 1f), new Color(.25f, .04f, .15f, 1f), new Color(.96f, .66f, .45f, 1f)),
        //        Name = "Level 1 Sentry",
        //        NameToken = "EngiWalkerTurretBody",
        //        RootObject = WalkerTurretSkinController.gameObject,
        //        BaseSkins = new SkinDef[0],
        //        UnlockableName = "",
        //        GameObjectActivations = new SkinDef.GameObjectActivation[0],

        //        RendererInfos = new CharacterModel.RendererInfo[]
        //        {
        //            new CharacterModel.RendererInfo
        //            {
        //                defaultMaterial = Resources.Load<Material>("@MoistureUpset_engi_turret:assets/unifiedtex.mat"),
        //                defaultShadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On,
        //                ignoreOverlays = false,
        //                renderer = engiWalkerTurretBodyRenderer[0]
        //            }

        //        },
        //        MeshReplacements = new SkinDef.MeshReplacement[]
        //        {
        //            new SkinDef.MeshReplacement
        //            {
        //                mesh = Resources.Load<Mesh>("@MoistureUpset_engi_turret:assets/walker_turret.mesh"),
        //                renderer = engiWalkerTurretBodyRenderer[0]
        //            }
        //        },
        //        ProjectileGhostReplacements = new SkinDef.ProjectileGhostReplacement[0],
        //        MinionSkinReplacements = new SkinDef.MinionSkinReplacement[0]
        //    };

        //    var bruh = LoadoutAPI.CreateNewSkinDef(turretSkinDef);
        //    var ligma = LoadoutAPI.CreateNewSkinDef(walkerTurretSkinDef);


        //    var skin = new LoadoutAPI.SkinDefInfo
        //    {
        //        //Icon = LoadoutAPI.CreateSkinIcon(new Color(.75f, .14f, .37f, 1f), new Color(.003f, .05f, .14f, 1f), new Color(.25f, .04f, .15f, 1f), new Color(.96f, .66f, .45f, 1f)),
        //        Icon = Resources.Load<Sprite>("@MoistureUpset_engi_icon:assets/tf2_engineer_icon.png"),
        //        Name = _name,  // Doesn't seem to work? nvm I'm just an idiot.
        //        NameToken = _nameToken,
        //        RootObject = mdl,
        //        BaseSkins = new SkinDef[0],
        //        UnlockableName = "",
        //        GameObjectActivations = new SkinDef.GameObjectActivation[0],

        //        RendererInfos = new CharacterModel.RendererInfo[]
        //        {
        //            new CharacterModel.RendererInfo
        //            {
        //                defaultMaterial = Resources.Load<Material>(_mat1),
        //                defaultShadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On,
        //                ignoreOverlays = false,
        //                renderer = renderers[0]
        //            }

        //        },
        //        MeshReplacements = new SkinDef.MeshReplacement[]
        //        {
        //            new SkinDef.MeshReplacement
        //            {
        //                mesh = Resources.Load<Mesh>(_mesh1),
        //                renderer = renderers[0]
        //            }
        //        },
        //        ProjectileGhostReplacements = new SkinDef.ProjectileGhostReplacement[0],
        //        MinionSkinReplacements = new SkinDef.MinionSkinReplacement[]
        //        {
        //            new SkinDef.MinionSkinReplacement
        //            {
        //                minionBodyPrefab = engiTurretBodyPrefab,
        //                minionSkin = bruh
        //            },
        //            new SkinDef.MinionSkinReplacement
        //            {
        //                minionBodyPrefab = engiWalkerTurretBodyPrefab,
        //                minionSkin = ligma
        //            }
        //        }
        //    };

        //    engiTurretBodyPrefab.AddComponent<SkinReloader>();
        //    engiWalkerTurretBodyPrefab.AddComponent<SkinReloader>();

        //    Array.Resize(ref skinController.skins, skinController.skins.Length + 1);
        //    skinController.skins[skinController.skins.Length - 1] = LoadoutAPI.CreateNewSkinDef(skin);

        //    Array.Resize(ref TurretSkinController.skins, TurretSkinController.skins.Length + 1);
        //    TurretSkinController.skins[TurretSkinController.skins.Length - 1] = bruh;

        //    Array.Resize(ref WalkerTurretSkinController.skins, WalkerTurretSkinController.skins.Length + 1);
        //    WalkerTurretSkinController.skins[WalkerTurretSkinController.skins.Length - 1] = ligma;

        //    var skinsField = Reflection.GetFieldValue<SkinDef[][]>(typeof(BodyCatalog), "skins");

        //    skinsField[BodyCatalog.FindBodyIndex(bodyPrefab)] = skinController.skins;
        //    skinsField[BodyCatalog.FindBodyIndex(engiTurretBodyPrefab)] = TurretSkinController.skins;
        //    skinsField[BodyCatalog.FindBodyIndex(engiWalkerTurretBodyPrefab)] = WalkerTurretSkinController.skins;

        //    Reflection.SetFieldValue(typeof(BodyCatalog), "skins", skinsField);

        //    LanguageAPI.Add(_nameToken, _name);

        //    SkinHelper.RegisterSkin("THE_TF2_ENGINEER_SKIN", "Engi");
        //}

        
    }
}
