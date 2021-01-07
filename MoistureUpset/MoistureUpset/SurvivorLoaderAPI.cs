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
    public static class SurvivorLoaderAPI // Why we call this class, is beyond me, we originally wanted this to be some easy functions to call to add skins. and now it's filled with stuff mostly for the Engi skin.
    {
        public static void LoadSurvivors()
        {
            PopulateAssets();
            EngiDisplayFix();
            //AnimeBubble();
        }

        private static void AnimeBubble()
        {
            //On.EntityStates.Engi.EngiBubbleShield.Deployed.OnEnter += (orig, self) =>
            //{
            //    orig(self);
            //    //
            //    //
            //    if (self.outer.gameObject.GetComponentInChildren<RoR2.Deployable>().ownerMaster.GetBody().isSkin("THE_TF2_ENGINEER_SKIN"))
            //    {
            //        AkSoundEngine.PostEvent("PlayBubble", self.outer.gameObject.AddComponent<AnimeBubbleOnDeath>().gameObject);
            //    }
            //};

            //On.EntityStates.Engi.EngiBubbleShield.Deployed.OnExit += (orig, self) =>
            //{
            //    if (self.outer.gameObject.GetComponentInChildren<RoR2.Deployable>().ownerMaster.GetBody().isSkin("THE_TF2_ENGINEER_SKIN"))
            //    {
            //        //AkSoundEngine.PostEvent("PauseBubble", self.outer.gameObject);
            //    }

            //    orig(self);
            //};

            //On.EntityStates.Engi.EngiBubbleShield.Undeployed.OnEnter += (orig, self) =>
            //{
            //    if (self.outer.gameObject.GetComponentInChildren<RoR2.Deployable>().ownerMaster.GetBody().isSkin("THE_TF2_ENGINEER_SKIN"))
            //    {
            //        AkSoundEngine.PostEvent("PauseBubble", self.outer.gameObject);
            //    }

            //    orig(self);
            //};

            //On.EntityStates.EntityState.Destroy += (orig, obj) =>
            //{
            //    Debug.Log(obj.name);
            //    orig(obj);
            //};
        }
        private static void EngiDisplayFix()
        {
            var fab = Resources.Load<GameObject>("prefabs/characterdisplays/EngiDisplay");

            fab.AddComponent<DisplayFix>();
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

            using (var assetStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("MoistureUpset.Models.na"))
            {
                var MainAssetBundle = AssetBundle.LoadFromStream(assetStream);

                ResourcesAPI.AddProvider(new AssetBundleResourcesProvider("@MoistureUpset_NA", MainAssetBundle));
            }

            using (var assetStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("MoistureUpset.sploaderskin"))
            {
                var MainAssetBundle = AssetBundle.LoadFromStream(assetStream);

                ResourcesAPI.AddProvider(new AssetBundleResourcesProvider("@MoistureUpset", MainAssetBundle));
            }

            using (var assetStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("MoistureUpset.engineer"))
            {
                var MainAssetBundle = AssetBundle.LoadFromStream(assetStream);

                ResourcesAPI.AddProvider(new AssetBundleResourcesProvider("@MoistureUpset_engi", MainAssetBundle));
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

            using (var assetStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("MoistureUpset.sentry2"))
            {
                var MainAssetBundle = AssetBundle.LoadFromStream(assetStream);

                ResourcesAPI.AddProvider(new AssetBundleResourcesProvider("@MoistureUpset_engi_sentry2", MainAssetBundle));
            }

            EnemyReplacements.LoadResource("dispener");
            EnemyReplacements.LoadResource("demopill");
            EnemyReplacements.LoadResource("rocket");
            EnemyReplacements.LoadResource("mines");
            EnemyReplacements.LoadResource("oopsideletedtheoldresource");


            On.RoR2.SurvivorCatalog.Init += SurvivorCatalog_Init;

            On.EntityStates.Engi.EngiWeapon.PlaceTurret.OnEnter += PlaceTurret_OnEnter;

            On.RoR2.Projectile.ProjectileController.Start += ProjectileController_Start;

        }

        private static void ProjectileController_Start(On.RoR2.Projectile.ProjectileController.orig_Start orig, ProjectileController self)
        {
            orig(self);
            try
            {
                var cb = self.owner.GetComponentInChildren<CharacterBody>();

                if (cb != null)
                {
                    //Debug.Log("--------------");
                    //Debug.Log(self.owner.name);
                    //Debug.Log(self.ghost.name);
                    if (self.owner.name == "EngiBody(Clone)" && cb.isSkin("THE_TF2_ENGINEER_SKIN"))
                    {
                        if (self.ghost.name == "EngiSeekerGrenadeGhost(Clone)")
                        {
                            var meshes = self.ghost.gameObject.GetComponentsInChildren<MeshFilter>();

                            meshes[0].sharedMesh = Resources.Load<Mesh>("@MoistureUpset_dispener:assets/dispenser.mesh");
                            meshes[1].sharedMesh = Resources.Load<Mesh>("@MoistureUpset_NA:assets/na1.mesh");
                            meshes[2].sharedMesh = Resources.Load<Mesh>("@MoistureUpset_NA:assets/na1.mesh");

                            self.ghost.gameObject.GetComponentInChildren<Renderer>().material.mainTexture = Resources.Load<Texture>("@MoistureUpset_dispener:assets/dispenser.png");
                            self.ghost.gameObject.GetComponentInChildren<Renderer>().material.SetTexture("_EmTex", Resources.Load<Texture>("@MoistureUpset_dispener:assets/dispenser.png"));
                            self.ghost.gameObject.GetComponentInChildren<Renderer>().material.SetTexture("_NormalTex", null);

                            meshes[0].transform.localScale = new Vector3(0.5f, 0.55f, 0.5f);
                            meshes[0].transform.localPosition += new Vector3(0f, 0f, 0.5f);

                            GameObject.DestroyImmediate(self.ghost.GetComponentInChildren<Rewired.ComponentControls.Effects.RotateAroundAxis>());

                            for (int i = 0; i < NetworkUser.readOnlyInstancesList.Count; i++)
                            {
                                if (NetworkUser.readOnlyInstancesList[i].master.GetBody() == cb)
                                {
                                    NetworkAssistant.playSound("EngiBuildsDispenser", i);
                                }
                            }

                            //foreach (var comp in self.ghost.GetComponentsInChildren<Component>())
                            //{
                            //    Debug.Log($"---------------------{}");
                            //}
                        }
                        else if (self.ghost.name == "EngiGrenadeGhost(Clone)")
                        {
                            var meshes = self.ghost.gameObject.GetComponentsInChildren<MeshFilter>();

                            meshes[0].sharedMesh = Resources.Load<Mesh>("@MoistureUpset_demopill:assets/demopill.mesh");

                            self.ghost.gameObject.GetComponentInChildren<Renderer>().material = Resources.Load<Material>("@MoistureUpset_demopill:assets/demopill.mat");

                            meshes[0].transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
                        }
                        else if (self.ghost.name == "EngiHarpoonGhost(Clone)")
                        {
                            var meshes = self.ghost.gameObject.GetComponentsInChildren<MeshFilter>();

                            meshes[0].sharedMesh = Resources.Load<Mesh>("@MoistureUpset_rocket:assets/rocket.mesh");

                            //foreach (var comp in self.ghost.GetComponentsInChildren<Component>())
                            //{
                            //    Debug.Log($"---------------------{comp}");
                            //}

                            self.ghost.gameObject.GetComponentInChildren<MeshRenderer>().material = Resources.Load<Material>("@MoistureUpset_engi:assets/models_player_engineer_engineer_red.mat");

                            meshes[0].transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
                        }
                        else if (self.ghost.name == "SpiderMineGhost(Clone)")
                        {
                            var meshes = self.ghost.gameObject.GetComponentsInChildren<SkinnedMeshRenderer>();

                            meshes[0].sharedMesh = Resources.Load<Mesh>("@MoistureUpset_mines:assets/spidermine.mesh");

                            self.ghost.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material = Resources.Load<Material>("@MoistureUpset_mines:assets/harpeenis.mat");
                        }
                        else if (self.ghost.name == "EngiMineGhost(Clone)")
                        {
                            var meshes = self.ghost.gameObject.GetComponentsInChildren<SkinnedMeshRenderer>();

                            meshes[0].sharedMesh = Resources.Load<Mesh>("@MoistureUpset_mines:assets/harpoon.mesh");

                            self.ghost.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material = Resources.Load<Material>("@MoistureUpset_mines:assets/harpeenis.mat");
                        }
                    }
                    //else if (self.owner.name == "GravekeeperBody(Clone)")
                    //{
                    //    if (self.ghost.name == "GravekeeperHookGhost(Clone)")
                    //    {
                    //        Debug.Log($"---------------------");
                    //        foreach (var comp in self.ghost.GetComponentsInChildren<Component>())
                    //        {
                    //            Debug.Log($"---------------------{comp}");
                    //        }
                    //        Debug.Log($"---------------------");
                    //    }
                    //}
                }
            }
            catch (Exception)
            {
            }
        }


        //This is one of the worst code practices I've ever done. But I have no better ideas so ¯\_(ツ)_/¯

        // It really do be like that sometimes

        // I finally found a better idea check out DisplayFix.cs



        //private static List<NetworkUser> GetSortedNetworkUsersList()
        //{
        //    List<NetworkUser> networkUserList = new List<NetworkUser>(NetworkUser.readOnlyInstancesList.Count);
        //    networkUserList.AddRange((IEnumerable<NetworkUser>)NetworkUser.readOnlyLocalPlayersList);
        //    for (int index = 0; index < NetworkUser.readOnlyInstancesList.Count; ++index)
        //    {
        //        NetworkUser readOnlyInstances = NetworkUser.readOnlyInstancesList[index];
        //        if (!networkUserList.Contains(readOnlyInstances))
        //            networkUserList.Add(readOnlyInstances);
        //    }
        //    return networkUserList;
        //}

        //private static void CharacterSelectController_OnNetworkUserLoadoutChanged(On.RoR2.UI.CharacterSelectController.orig_OnNetworkUserLoadoutChanged orig, RoR2.UI.CharacterSelectController self, NetworkUser networkUser)
        //{
        //    int index = GetSortedNetworkUsersList().IndexOf(networkUser);
        //    if (index == -1)
        //        return;

        //    CharacterPad safe1 = HGArrayUtilities.GetSafe<CharacterPad>(self.characterDisplayPads, index);
        //    if (!(bool)(UnityEngine.Object)safe1.displayInstance)
        //        return;
        //    Loadout dest = new Loadout();
        //    networkUser.networkLoadout.CopyLoadout(dest);
        //    int fromSurvivorIndex = SurvivorCatalog.GetBodyIndexFromSurvivorIndex(safe1.displaySurvivorIndex);
        //    int skinIndex = (int)dest.bodyLoadoutManager.GetSkinIndex(fromSurvivorIndex);

        //    if (fromSurvivorIndex == 35 && skinIndex == 2)
        //    {
        //        var renderers = safe1.displayInstance.GetComponentsInChildren<SkinnedMeshRenderer>();
        //        foreach (var item in renderers)
        //        {
        //            if (item.gameObject.name == "EngiTurretMesh")
        //            {
        //                item.sharedMesh = Resources.Load<Mesh>("@MoistureUpset_engi_turret:assets/normal_sentry.mesh");
        //                item.material.mainTexture = Resources.Load<Texture>("@MoistureUpset_engi_turret2:assets/unified_turret_tex.png");
        //                item.material.SetTexture("_EmTex", Resources.Load<Texture>("@MoistureUpset_engi_turret2:assets/unified_turret_tex.png"));
        //            }
        //            else if (item.gameObject.name == "EngiWalkerTurretMesh")
        //            {
        //                item.sharedMesh = Resources.Load<Mesh>("@MoistureUpset_engi_turret:assets/normal_sentry.mesh");
        //                item.material.mainTexture = Resources.Load<Texture>("@MoistureUpset_engi_turret2:assets/unified_turret_tex.png");
        //                item.material.SetTexture("_EmTex", Resources.Load<Texture>("@MoistureUpset_engi_turret2:assets/unified_turret_tex.png"));
        //            }
        //        }
        //    }

        //    SkinDef safe2 = HGArrayUtilities.GetSafe<SkinDef>(BodyCatalog.GetBodySkins(fromSurvivorIndex), skinIndex);
        //    CharacterModel componentInChildren = safe1.displayInstance.GetComponentInChildren<CharacterModel>();
        //    if (!(bool)(UnityEngine.Object)componentInChildren || safe2 == null)
        //        return;
        //    safe2.Apply(componentInChildren.gameObject);
        //}

        //private static void CharacterSelectController_SelectSurvivor(On.RoR2.UI.CharacterSelectController.orig_SelectSurvivor orig, RoR2.UI.CharacterSelectController self, SurvivorIndex survivor)
        //{
        //    try
        //    {
        //        //Debug.Log("bruh");
        //        //foreach (var item in self.characterDisplayPads)
        //        //{
        //        //    foreach (var displayClone in item.displayInstance.GetComponents<Component>())
        //        //    {
        //        //        Debug.Log($">>>>>>>>>>>{displayClone}");
        //        //        foreach (var displayCloneChildren in displayClone.GetComponentsInChildren<Renderer>())
        //        //        {
        //        //            Debug.Log($"{displayCloneChildren} ---> {displayCloneChildren.gameObject.name}");
        //        //        }
        //        //    }
        //        //}


        //    }
        //    catch (Exception e)
        //    {
        //        Debug.Log(e);
        //    }
        //    finally
        //    {
        //        orig(self, survivor);
        //    }
        //}

        //private static void CharacterSelectController_Update(On.RoR2.UI.CharacterSelectController.orig_Update orig, RoR2.UI.CharacterSelectController self)
        //{
        //    try
        //    {

        //    }
        //    catch (Exception e)
        //    {
        //        Debug.Log(e);
        //    }
        //    finally
        //    {
        //        orig(self);
        //    }
        //}


        // Gotta do this jank mess in order to make the blueprint look like the custom skinned turrets, not my best idea, but it works.
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
                    Debug.LogWarning("is null");
                }

            }

            orig(self);

            if (tempPrefab != null)
            {
                GameObject.Destroy(tempPrefab);
            }
        }

        //private static CharacterMaster MasterSummon_Perform(On.RoR2.MasterSummon.orig_Perform orig, MasterSummon self)
        //{
        //    CharacterMaster cm = orig(self);

        //    try
        //    {
        //        if (cm.minionOwnership.ownerMaster != null)
        //        {
        //            if (cm.minionOwnership.ownerMaster.GetBody().isSkin("THE_TF2_ENGINEER_SKIN") && cm.minionOwnership.ownerMaster.GetBody().name == "EngiBody(Clone)" && cm.GetBody().name.Contains("Engi"))
        //            {
        //                for (int i = 0; i < cm.GetBody().GetComponentInChildren<SkinnedMeshRenderer>().sharedMaterials.Length; i++)
        //                {
        //                    //cm.GetBody().GetComponentInChildren<SkinnedMeshRenderer>().sharedMaterials[i].mainTexture = Resources.Load<Texture>("@MoistureUpset_engi_turret2:assets/unified_turret_tex.png");
        //                    //cm.GetBody().GetComponentInChildren<SkinnedMeshRenderer>().sharedMaterials[i].SetTexture("_EmTex", Resources.Load<Texture>("@MoistureUpset_engi_turret2:assets/unified_turret_tex.png"));
        //                }
        //                Debug.Log($"{cm.name}--------{cm.GetBody().name}");

        //                Debug.Log(cm.GetBody().GetComponentInChildren<ModelSkinController>().skins.Length);

        //                switch (cm.name)
        //                {
        //                    case "EngiWalkerTurretMaster(Clone)":
        //                        //cm.GetBody().GetComponentInChildren<SkinnedMeshRenderer>().sharedMesh = Resources.Load<Mesh>("@MoistureUpset_engi_turret:assets/walker_turret.mesh");
        //                        cm.GetBody().GetComponentInChildren<ModelSkinController>().ApplySkin(2);
        //                        break;
        //                    case "EngiTurretMaster(Clone)":
        //                        //cm.GetBody().GetComponentInChildren<SkinnedMeshRenderer>().sharedMesh = Resources.Load<Mesh>("@MoistureUpset_engi_turret:assets/normal_sentry.mesh");
        //                        cm.GetBody().GetComponentInChildren<ModelSkinController>().ApplySkin(2);
        //                        break;
        //                }


        //                //cm.GetBody().GetComponentInChildren<SkinnedMeshRenderer>().sharedMesh = Resources.Load<Mesh>("@MoistureUpset_engi_turret:assets/normal_sentry.mesh");
        //            }
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Debug.Log(e);
        //    }


        //    return cm;
        //}

        private static void SurvivorCatalog_Init(On.RoR2.SurvivorCatalog.orig_Init orig)
        {
            orig();

            AddStarPlatinumSkinToLoader();
            EngineerStuff("The Engineer", "THE_TF2_ENGINEER_SKIN", "@MoistureUpset_engi:assets/models_player_engineer_engineer_red.mat", "@MoistureUpset_oopsideletedtheoldresource:assets/engi.mesh", RoR2.SurvivorIndex.Engi);
            //EngineerStuff("The Engineer", "THE_ENGINEER_SKIN", "@MoistureUpset_engi:assets/models_player_engineer_engineer_red.mat", "@MoistureUpset_mike:assets/engimesh.mesh", RoR2.SurvivorIndex.Engi);
            EditDropPod();


            foreach (var item in SurvivorCatalog.allSurvivorDefs)
            {
                item.bodyPrefab.AddComponent<SkinReloader>();

                //foreach (var display in item.displayPrefab.GetComponentsInChildren<Component>())
                //{
                //    Debug.Log($"--------------------{display.gameObject.name} ---------------");
                //}
            }
        }
        private static void EditDropPod()
        {
            On.RoR2.UI.LogBook.LogBookController.GetMonsterStatus += (orig, profile, entry) =>
            {
                return RoR2.UI.LogBook.EntryStatus.Available;
            };
        }
        private static void EngineerStuff(string _name, string _nameToken, string _mat1, string _mesh1, RoR2.SurvivorIndex _survivorIndex)
        {
            var survivorDef = SurvivorCatalog.GetSurvivorDef(_survivorIndex);
            var bodyPrefab = survivorDef.bodyPrefab;

            var engiTurretBodyPrefab = BodyCatalog.GetBodyPrefab(36);
            var engiTurretBodyRenderer = engiTurretBodyPrefab.GetComponentsInChildren<Renderer>();

            var engiWalkerTurretBodyPrefab = BodyCatalog.GetBodyPrefab(37);
            var engiWalkerTurretBodyRenderer = engiWalkerTurretBodyPrefab.GetComponentsInChildren<Renderer>();

            var renderers = bodyPrefab.GetComponentsInChildren<Renderer>();
            var skinController = bodyPrefab.GetComponentInChildren<ModelSkinController>();

            var mdl = skinController.gameObject;

            var TurretSkinController = engiTurretBodyPrefab.GetComponentInChildren<ModelSkinController>();
            var WalkerTurretSkinController = engiWalkerTurretBodyPrefab.GetComponentInChildren<ModelSkinController>();


            //On.RoR2.SkinDef.Awake += SkinDef_Awake;

            //var turretSkinDef = ScriptableObject.CreateInstance<RoR2.SkinDef>();

            //turretSkinDef.name = _name;
            //turretSkinDef.nameToken = _nameToken;

            //turretSkinDef.rootObject = TurretSkinController.gameObject;

            //turretSkinDef.rendererInfos = new CharacterModel.RendererInfo[]
            //{
            //    new CharacterModel.RendererInfo
            //    {
            //        defaultMaterial = Resources.Load<Material>("@MoistureUpset_engi_turret:assets/unifiedtex.mat"),
            //        defaultShadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On,
            //        ignoreOverlays = false,
            //        renderer = engiTurretBodyRenderer[0]
            //    }
            //};

            //turretSkinDef.meshReplacements = new SkinDef.MeshReplacement[]
            //{
            //    new SkinDef.MeshReplacement
            //    {
            //        mesh = Resources.Load<Mesh>("@MoistureUpset_engi_turret:assets/normal_sentry.mesh"),
            //        renderer = engiTurretBodyRenderer[0]
            //    }
            //};

            //var walkerTurretSkinDef = ScriptableObject.CreateInstance<RoR2.SkinDef>();

            //walkerTurretSkinDef.name = _name;
            //walkerTurretSkinDef.nameToken = _nameToken;

            //walkerTurretSkinDef.rootObject = WalkerTurretSkinController.gameObject;

            //walkerTurretSkinDef.rendererInfos = new CharacterModel.RendererInfo[]
            //{
            //    new CharacterModel.RendererInfo
            //    {
            //        defaultMaterial = Resources.Load<Material>("@MoistureUpset_engi_turret:assets/unifiedtex.mat"),
            //        defaultShadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On,
            //        ignoreOverlays = false,
            //        renderer = engiWalkerTurretBodyRenderer[0]
            //    }
            //};

            //walkerTurretSkinDef.meshReplacements = new SkinDef.MeshReplacement[]
            //{
            //    new SkinDef.MeshReplacement
            //    {
            //        mesh = Resources.Load<Mesh>("@MoistureUpset_engi_turret:assets/normal_sentry.mesh"),
            //        renderer = engiWalkerTurretBodyRenderer[0]
            //    }
            //};

            //On.RoR2.SkinDef.Awake -= SkinDef_Awake;

            var turretSkinDef = new LoadoutAPI.SkinDefInfo
            {
                Icon = LoadoutAPI.CreateSkinIcon(new Color(.75f, .14f, .37f, 1f), new Color(.003f, .05f, .14f, 1f), new Color(.25f, .04f, .15f, 1f), new Color(.96f, .66f, .45f, 1f)),
                Name = _name,
                NameToken = _nameToken,
                RootObject = TurretSkinController.gameObject,
                BaseSkins = new SkinDef[0],
                UnlockableName = "",
                GameObjectActivations = new SkinDef.GameObjectActivation[0],

                RendererInfos = new CharacterModel.RendererInfo[]
                {
                    new CharacterModel.RendererInfo
                    {
                        defaultMaterial = Resources.Load<Material>("@MoistureUpset_engi_turret:assets/unifiedtex.mat"),
                        defaultShadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On,
                        ignoreOverlays = false,
                        renderer = engiTurretBodyRenderer[0]
                    }

                },
                MeshReplacements = new SkinDef.MeshReplacement[]
                {
                    new SkinDef.MeshReplacement
                    {
                        mesh = Resources.Load<Mesh>("@MoistureUpset_engi_turret:assets/normal_sentry.mesh"),
                        renderer = engiTurretBodyRenderer[0]
                    }
                },
                ProjectileGhostReplacements = new SkinDef.ProjectileGhostReplacement[0],
                MinionSkinReplacements = new SkinDef.MinionSkinReplacement[0]
            };

            var walkerTurretSkinDef = new LoadoutAPI.SkinDefInfo
            {
                Icon = LoadoutAPI.CreateSkinIcon(new Color(.75f, .14f, .37f, 1f), new Color(.003f, .05f, .14f, 1f), new Color(.25f, .04f, .15f, 1f), new Color(.96f, .66f, .45f, 1f)),
                Name = _name,
                NameToken = _nameToken,
                RootObject = WalkerTurretSkinController.gameObject,
                BaseSkins = new SkinDef[0],
                UnlockableName = "",
                GameObjectActivations = new SkinDef.GameObjectActivation[0],

                RendererInfos = new CharacterModel.RendererInfo[]
                {
                    new CharacterModel.RendererInfo
                    {
                        defaultMaterial = Resources.Load<Material>("@MoistureUpset_engi_turret:assets/unifiedtex.mat"),
                        defaultShadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On,
                        ignoreOverlays = false,
                        renderer = engiWalkerTurretBodyRenderer[0]
                    }

                },
                MeshReplacements = new SkinDef.MeshReplacement[]
                {
                    new SkinDef.MeshReplacement
                    {
                        mesh = Resources.Load<Mesh>("@MoistureUpset_engi_turret:assets/walker_turret.mesh"),
                        renderer = engiWalkerTurretBodyRenderer[0]
                    }
                },
                ProjectileGhostReplacements = new SkinDef.ProjectileGhostReplacement[0],
                MinionSkinReplacements = new SkinDef.MinionSkinReplacement[0]
            };

            var bruh = LoadoutAPI.CreateNewSkinDef(turretSkinDef);
            var ligma = LoadoutAPI.CreateNewSkinDef(walkerTurretSkinDef);


            var skin = new LoadoutAPI.SkinDefInfo
            {
                Icon = LoadoutAPI.CreateSkinIcon(new Color(.75f, .14f, .37f, 1f), new Color(.003f, .05f, .14f, 1f), new Color(.25f, .04f, .15f, 1f), new Color(.96f, .66f, .45f, 1f)),
                Name = _name,
                NameToken = _nameToken,
                RootObject = mdl,
                BaseSkins = new SkinDef[0],
                UnlockableName = "",
                GameObjectActivations = new SkinDef.GameObjectActivation[0],

                RendererInfos = new CharacterModel.RendererInfo[]
                {
                    new CharacterModel.RendererInfo
                    {
                        defaultMaterial = Resources.Load<Material>(_mat1),
                        defaultShadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On,
                        ignoreOverlays = false,
                        renderer = renderers[0]
                    }

                },
                MeshReplacements = new SkinDef.MeshReplacement[]
                {
                    new SkinDef.MeshReplacement
                    {
                        mesh = Resources.Load<Mesh>(_mesh1),
                        renderer = renderers[0]
                    }
                },
                ProjectileGhostReplacements = new SkinDef.ProjectileGhostReplacement[0],
                MinionSkinReplacements = new SkinDef.MinionSkinReplacement[]
                {
                    new SkinDef.MinionSkinReplacement
                    {
                        minionBodyPrefab = engiTurretBodyPrefab,
                        minionSkin = bruh
                    },
                    new SkinDef.MinionSkinReplacement
                    {
                        minionBodyPrefab = engiWalkerTurretBodyPrefab,
                        minionSkin = ligma
                    }
                }
            };

            engiTurretBodyPrefab.AddComponent<SkinReloader>();
            engiWalkerTurretBodyPrefab.AddComponent<SkinReloader>();

            Array.Resize(ref skinController.skins, skinController.skins.Length + 1);
            skinController.skins[skinController.skins.Length - 1] = LoadoutAPI.CreateNewSkinDef(skin);

            Array.Resize(ref TurretSkinController.skins, TurretSkinController.skins.Length + 1);
            TurretSkinController.skins[TurretSkinController.skins.Length - 1] = bruh;

            Array.Resize(ref WalkerTurretSkinController.skins, WalkerTurretSkinController.skins.Length + 1);
            WalkerTurretSkinController.skins[WalkerTurretSkinController.skins.Length - 1] = ligma;

            var skinsField = Reflection.GetFieldValue<SkinDef[][]>(typeof(BodyCatalog), "skins");

            skinsField[BodyCatalog.FindBodyIndex(bodyPrefab)] = skinController.skins;
            skinsField[BodyCatalog.FindBodyIndex(engiTurretBodyPrefab)] = TurretSkinController.skins;
            skinsField[BodyCatalog.FindBodyIndex(engiWalkerTurretBodyPrefab)] = WalkerTurretSkinController.skins;

            Reflection.SetFieldValue(typeof(BodyCatalog), "skins", skinsField);

            SkinHelper.RegisterSkin("THE_TF2_ENGINEER_SKIN", "Engi");
        }

        private static void SkinDef_Awake(On.RoR2.SkinDef.orig_Awake orig, RoR2.SkinDef self)
        {

        }

        private static void AddStarPlatinumSkinToLoader()
        {
            var survivorDef = SurvivorCatalog.GetSurvivorDef(SurvivorIndex.Loader);
            var bodyPrefab = survivorDef.bodyPrefab;

            var renderers = bodyPrefab.GetComponentsInChildren<Renderer>();
            var skinController = bodyPrefab.GetComponentInChildren<ModelSkinController>();

            var mdl = skinController.gameObject;

            var skin = new LoadoutAPI.SkinDefInfo
            {
                Icon = LoadoutAPI.CreateSkinIcon(Color.black, Color.white, new Color(0.69F, 0.19F, 0.65F, 1F), Color.yellow),
                Name = "Star Platinum",
                NameToken = "STAR_PLATINUM_SKIN",
                RootObject = mdl,
                BaseSkins = new SkinDef[0],
                UnlockableName = "",
                GameObjectActivations = new SkinDef.GameObjectActivation[0],

                RendererInfos = new CharacterModel.RendererInfo[]
                {
                    new CharacterModel.RendererInfo
                    {
                        defaultMaterial = Resources.Load<Material>("@MoistureUpset:assets/starplatinummat.mat"),
                        defaultShadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On,
                        ignoreOverlays = false,
                        renderer = renderers[0]
                    },
                    new CharacterModel.RendererInfo
                    {
                        defaultMaterial = Resources.Load<Material>("@MoistureUpset:assets/starplatinummat.mat"),
                        defaultShadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On,
                        ignoreOverlays = false,
                        renderer = renderers[1]
                    },
                    new CharacterModel.RendererInfo
                    {
                        defaultMaterial = Resources.Load<Material>("@MoistureUpset:assets/starplatinummat.mat"),
                        defaultShadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On,
                        ignoreOverlays = false,
                        renderer = renderers[2]
                    },
                },

                MeshReplacements = new SkinDef.MeshReplacement[]
                {
                    new SkinDef.MeshReplacement
                    {
                        mesh = Resources.Load<Mesh>("@MoistureUpset:assets/sphist.mesh"),
                        renderer = renderers[0]
                    },
                    new SkinDef.MeshReplacement
                    {
                        mesh = Resources.Load<Mesh>("@MoistureUpset:assets/speniscloth.mesh"),
                        renderer = renderers[1]
                    },
                    new SkinDef.MeshReplacement
                    {
                        mesh = Resources.Load<Mesh>("@MoistureUpset:assets/splatinum.mesh"),
                        renderer = renderers[2]
                    }

                },
                ProjectileGhostReplacements = new SkinDef.ProjectileGhostReplacement[0],
                MinionSkinReplacements = new SkinDef.MinionSkinReplacement[0]
            };

            Array.Resize(ref skinController.skins, skinController.skins.Length + 1);
            skinController.skins[skinController.skins.Length - 1] = LoadoutAPI.CreateNewSkinDef(skin);

            var skinsField = Reflection.GetFieldValue<SkinDef[][]>(typeof(BodyCatalog), "skins");
            skinsField[BodyCatalog.FindBodyIndex(bodyPrefab)] = skinController.skins;
            Reflection.SetFieldValue(typeof(BodyCatalog), "skins", skinsField);

            
        }
    }
}
