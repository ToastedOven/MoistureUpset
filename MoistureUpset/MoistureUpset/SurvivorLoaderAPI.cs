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

namespace MoistureUpset
{
    public static class SurvivorLoaderAPI
    {
        public static SkinDef turretSkinDef;

        public static void LoadSurvivors()
        {
            PopulateAssets();
        }

        private static void PopulateAssets()
        {
            using (var assetStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("MoistureUpset.noob"))
            {
                var MainAssetBundle = AssetBundle.LoadFromStream(assetStream);

                ResourcesAPI.AddProvider(new AssetBundleResourcesProvider("@MoistureUpset_noob", MainAssetBundle));
            }

            using (var assetStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("MoistureUpset.mike"))
            {
                var MainAssetBundle = AssetBundle.LoadFromStream(assetStream);

                ResourcesAPI.AddProvider(new AssetBundleResourcesProvider("@MoistureUpset_mike", MainAssetBundle));
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

            using (var assetStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("MoistureUpset.outhousebetter"))
            {
                var MainAssetBundle = AssetBundle.LoadFromStream(assetStream);

                ResourcesAPI.AddProvider(new AssetBundleResourcesProvider("@MoistureUpset_droppod", MainAssetBundle));
            }
            On.RoR2.SurvivorCatalog.Init += SurvivorCatalog_Init;

            On.RoR2.MasterSummon.Perform += MasterSummon_Perform;

            On.EntityStates.Engi.EngiWeapon.PlaceTurret.OnEnter += PlaceTurret_OnEnter;

            On.RoR2.UI.CharacterSelectController.Update += CharacterSelectController_Update;
            On.RoR2.UI.CharacterSelectController.SelectSurvivor += CharacterSelectController_SelectSurvivor;
            On.RoR2.UI.CharacterSelectController.OnNetworkUserLoadoutChanged += CharacterSelectController_OnNetworkUserLoadoutChanged;

            On.RoR2.CharacterMaster.OnBodyStart += CharacterMaster_OnBodyStart;
        }

        private static void CharacterMaster_OnBodyStart(On.RoR2.CharacterMaster.orig_OnBodyStart orig, CharacterMaster self, CharacterBody body)
        {
            CharacterMaster cm = body.master;

            try
            {
                if (cm.minionOwnership.ownerMaster != null)
                {
                    if (cm.minionOwnership.ownerMaster.GetBody().skinIndex == 2 && cm.minionOwnership.ownerMaster.GetBody().name == "EngiBody(Clone)" && cm.GetBody().name.Contains("Engi"))
                    {
                        for (int i = 0; i < cm.GetBody().GetComponentInChildren<SkinnedMeshRenderer>().sharedMaterials.Length; i++)
                        {
                            cm.GetBody().GetComponentInChildren<SkinnedMeshRenderer>().sharedMaterials[i].mainTexture = Resources.Load<Texture>("@MoistureUpset_engi_turret2:assets/unified_turret_tex.png");
                            cm.GetBody().GetComponentInChildren<SkinnedMeshRenderer>().sharedMaterials[i].SetTexture("_EmTex", Resources.Load<Texture>("@MoistureUpset_engi_turret2:assets/unified_turret_tex.png"));
                        }
                        Debug.Log($"{cm.name}--------{cm.GetBody().name}");
                        switch (cm.name)
                        {
                            case "EngiWalkerTurretMaster(Clone)":
                                cm.GetBody().GetComponentInChildren<SkinnedMeshRenderer>().sharedMesh = Resources.Load<Mesh>("@MoistureUpset_engi_turret:assets/walker_turret.mesh");
                                break;
                            case "EngiTurretMaster(Clone)":
                                cm.GetBody().GetComponentInChildren<SkinnedMeshRenderer>().sharedMesh = Resources.Load<Mesh>("@MoistureUpset_engi_turret:assets/normal_sentry.mesh");
                                break;
                        }

                        //cm.GetBody().GetComponentInChildren<SkinnedMeshRenderer>().sharedMesh = Resources.Load<Mesh>("@MoistureUpset_engi_turret:assets/normal_sentry.mesh");
                    }
                }
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }
            finally
            {
                orig(self, body);
            }
        }


        //This is one of the worst code practices I've ever done. But I have no better ideas so ¯\_(ツ)_/¯

        private static List<NetworkUser> GetSortedNetworkUsersList()
        {
            List<NetworkUser> networkUserList = new List<NetworkUser>(NetworkUser.readOnlyInstancesList.Count);
            networkUserList.AddRange((IEnumerable<NetworkUser>)NetworkUser.readOnlyLocalPlayersList);
            for (int index = 0; index < NetworkUser.readOnlyInstancesList.Count; ++index)
            {
                NetworkUser readOnlyInstances = NetworkUser.readOnlyInstancesList[index];
                if (!networkUserList.Contains(readOnlyInstances))
                    networkUserList.Add(readOnlyInstances);
            }
            return networkUserList;
        }

        private static void CharacterSelectController_OnNetworkUserLoadoutChanged(On.RoR2.UI.CharacterSelectController.orig_OnNetworkUserLoadoutChanged orig, RoR2.UI.CharacterSelectController self, NetworkUser networkUser)
        {
            int index = GetSortedNetworkUsersList().IndexOf(networkUser);
            if (index == -1)
                return;

            CharacterPad safe1 = HGArrayUtilities.GetSafe<CharacterPad>(self.characterDisplayPads, index);
            if (!(bool)(UnityEngine.Object)safe1.displayInstance)
                return;
            Loadout dest = new Loadout();
            networkUser.networkLoadout.CopyLoadout(dest);
            int fromSurvivorIndex = SurvivorCatalog.GetBodyIndexFromSurvivorIndex(safe1.displaySurvivorIndex);
            int skinIndex = (int)dest.bodyLoadoutManager.GetSkinIndex(fromSurvivorIndex);

            if (fromSurvivorIndex == 35 && skinIndex == 2)
            {
                var renderers = safe1.displayInstance.GetComponentsInChildren<SkinnedMeshRenderer>();
                foreach (var item in renderers)
                {
                    if (item.gameObject.name == "EngiTurretMesh")
                    {
                        item.sharedMesh = Resources.Load<Mesh>("@MoistureUpset_engi_turret:assets/normal_sentry.mesh");
                        item.material.mainTexture = Resources.Load<Texture>("@MoistureUpset_engi_turret2:assets/unified_turret_tex.png");
                        item.material.SetTexture("_EmTex", Resources.Load<Texture>("@MoistureUpset_engi_turret2:assets/unified_turret_tex.png"));
                    }
                    else if (item.gameObject.name == "EngiWalkerTurretMesh")
                    {
                        item.sharedMesh = Resources.Load<Mesh>("@MoistureUpset_engi_turret:assets/normal_sentry.mesh");
                        item.material.mainTexture = Resources.Load<Texture>("@MoistureUpset_engi_turret2:assets/unified_turret_tex.png");
                        item.material.SetTexture("_EmTex", Resources.Load<Texture>("@MoistureUpset_engi_turret2:assets/unified_turret_tex.png"));
                    }
                }
            }

            SkinDef safe2 = HGArrayUtilities.GetSafe<SkinDef>(BodyCatalog.GetBodySkins(fromSurvivorIndex), skinIndex);
            CharacterModel componentInChildren = safe1.displayInstance.GetComponentInChildren<CharacterModel>();
            if (!(bool)(UnityEngine.Object)componentInChildren || safe2 == null)
                return;
            safe2.Apply(componentInChildren.gameObject);
        }

        private static void CharacterSelectController_SelectSurvivor(On.RoR2.UI.CharacterSelectController.orig_SelectSurvivor orig, RoR2.UI.CharacterSelectController self, SurvivorIndex survivor)
        {
            try
            {
                //Debug.Log("bruh");
                //foreach (var item in self.characterDisplayPads)
                //{
                //    foreach (var displayClone in item.displayInstance.GetComponents<Component>())
                //    {
                //        Debug.Log($">>>>>>>>>>>{displayClone}");
                //        foreach (var displayCloneChildren in displayClone.GetComponentsInChildren<Renderer>())
                //        {
                //            Debug.Log($"{displayCloneChildren} ---> {displayCloneChildren.gameObject.name}");
                //        }
                //    }
                //}

                
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }
            finally
            {
                orig(self, survivor);
            }
        }

        private static void CharacterSelectController_Update(On.RoR2.UI.CharacterSelectController.orig_Update orig, RoR2.UI.CharacterSelectController self)
        {
            try
            {
                
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }
            finally
            {
                orig(self);
            }
        }

        private static void PlaceTurret_OnEnter(On.EntityStates.Engi.EngiWeapon.PlaceTurret.orig_OnEnter orig, EntityStates.Engi.EngiWeapon.PlaceTurret self)
        {
            var cb = self.outer.GetComponentInChildren<CharacterBody>();

            GameObject tempPrefab = (GameObject)null;

            if (cb.skinIndex == 2)
            {
                if (self.blueprintPrefab != null)
                {
                    tempPrefab = GameObject.Instantiate<GameObject>(self.blueprintPrefab);
                    tempPrefab.GetComponentInChildren<SkinnedMeshRenderer>().sharedMesh = Resources.Load<Mesh>("@MoistureUpset_engi_turret:assets/normal_sentry.mesh");
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

        private static CharacterMaster MasterSummon_Perform(On.RoR2.MasterSummon.orig_Perform orig, MasterSummon self)
        {
            CharacterMaster cm = orig(self);

            try
            {
                if (cm.minionOwnership.ownerMaster != null)
                {
                    if (cm.minionOwnership.ownerMaster.GetBody().skinIndex == 2 && cm.minionOwnership.ownerMaster.GetBody().name == "EngiBody(Clone)" && cm.GetBody().name.Contains("Engi"))
                    {
                        for (int i = 0; i < cm.GetBody().GetComponentInChildren<SkinnedMeshRenderer>().sharedMaterials.Length; i++)
                        {
                            cm.GetBody().GetComponentInChildren<SkinnedMeshRenderer>().sharedMaterials[i].mainTexture = Resources.Load<Texture>("@MoistureUpset_engi_turret2:assets/unified_turret_tex.png");
                            cm.GetBody().GetComponentInChildren<SkinnedMeshRenderer>().sharedMaterials[i].SetTexture("_EmTex", Resources.Load<Texture>("@MoistureUpset_engi_turret2:assets/unified_turret_tex.png"));
                        }
                        Debug.Log($"{cm.name}--------{cm.GetBody().name}");
                        switch (cm.name)
                        {
                            case "EngiWalkerTurretMaster(Clone)":
                                cm.GetBody().GetComponentInChildren<SkinnedMeshRenderer>().sharedMesh = Resources.Load<Mesh>("@MoistureUpset_engi_turret:assets/walker_turret.mesh");
                                break;
                            case "EngiTurretMaster(Clone)":
                                cm.GetBody().GetComponentInChildren<SkinnedMeshRenderer>().sharedMesh = Resources.Load<Mesh>("@MoistureUpset_engi_turret:assets/normal_sentry.mesh");
                                break;
                        }

                        cm.GetBody().GetComponentInChildren<SkinnedMeshRenderer>().sharedMesh = Resources.Load<Mesh>("@MoistureUpset_engi_turret:assets/normal_sentry.mesh");
                    }
                }
            }
            catch (Exception e)
            {
                Debug.Log(e);
            }


            return cm;
        }

        private static void SurvivorCatalog_Init(On.RoR2.SurvivorCatalog.orig_Init orig)
        {
            orig();

            AddStarPlatinumSkinToLoader();
            EngineerStuff("The Engineer", "THE_ENGINEER_SKIN", "@MoistureUpset_engi:assets/models_player_engineer_engineer_red.mat", "@MoistureUpset_engi:assets/engi.mesh", RoR2.SurvivorIndex.Engi);
            //EngineerStuff("The Engineer", "THE_ENGINEER_SKIN", "@MoistureUpset_engi:assets/models_player_engineer_engineer_red.mat", "@MoistureUpset_mike:assets/engimesh.mesh", RoR2.SurvivorIndex.Engi);
            EditDropPod();
        }
        private static void EditDropPod()
        {
            On.RoR2.UI.LogBook.LogBookController.GetMonsterStatus += (orig, profile, entry) =>
            {
                return RoR2.UI.LogBook.EntryStatus.Available;
            };
            //var fab = Resources.Load<GameObject>("prefabs/networkedobjects/SurvivorPod");
            //var renderers = fab.GetComponentsInChildren<Renderer>();
            //var meshes = fab.GetComponentsInChildren<MeshFilter>();
            //renderers[0].material = Resources.Load<Material>("@MoistureUpset_droppod:assets/shrekpodmat.mat");
            //meshes[0].mesh = Resources.Load<Mesh>("@MoistureUpset_droppod:assets/outhouse.mesh");
            //renderers[1].material = Resources.Load<Material>("@MoistureUpset_droppod:assets/shrekpodmat.mat");
            //meshes[1].mesh = Resources.Load<Mesh>("@MoistureUpset_droppod:assets/door.mesh");



            //var fab2 = Resources.Load<GameObject>("prefabs/characterbodies/EngiBody");
            //var components2 = fab2.GetComponentsInChildren<Component>();
            //Debug.Log($"-----engi-------------");
            //foreach (var item in components2)
            //{
            //    Debug.Log($"-----component-----{item}");
            //}

            //fab = Resources.Load<GameObject>("prefabs/characterbodies/EngiTurretBody");
            //renderers = fab.GetComponentsInChildren<Renderer>();
            //meshes = fab.GetComponentsInChildren<MeshFilter>();
            //renderers[0].material = Resources.Load<Material>("@MoistureUpset_engi_sentry2:assets/coolturretmat.mat");
            //meshes[0].mesh = Resources.Load<Mesh>("@MoistureUpset_engi_sentry2:assets/sentry2_optimized_reference.mesh");
            //fab = Resources.Load<GameObject>("prefabs/characterbodies/Turret1Body");
            //foreach (var item in fab.GetComponentsInChildren<Component>())
            //{
            //    Debug.Log($"------------------------------name: {item.name} type: {item.GetType()}");
            //}
        }
        private static void EngineerStuff(string _name, string _nameToken, string _mat1, string _mesh1, RoR2.SurvivorIndex _survivorIndex)
        {
            var survivorDef = SurvivorCatalog.GetSurvivorDef(_survivorIndex);
            var bodyPrefab = survivorDef.bodyPrefab;

            //for (int i = 0; i < BodyCatalog.allBodyPrefabs.ToArray().Length ; i++)
            //{
            //    Debug.Log($">>>>>>>>>>>>{i}<<<<<<>>>>>>>{BodyCatalog.allBodyPrefabs.ToArray()[i]}");
            //}


            var fuckthisbodyprefab = BodyCatalog.GetBodyPrefab(36);
            var fuckthisrenderer = fuckthisbodyprefab.GetComponentsInChildren<Renderer>();


            var renderers = bodyPrefab.GetComponentsInChildren<Renderer>();
            var skinController = bodyPrefab.GetComponentInChildren<ModelSkinController>();

            var normalTurretBodyPrefab = Resources.Load<GameObject>("prefabs/characterbodies/EngiTurretBody");

            var mdl = skinController.gameObject;

            On.RoR2.SkinDef.Awake += SkinDef_Awake;

            turretSkinDef = ScriptableObject.CreateInstance<RoR2.SkinDef>();

            turretSkinDef.rendererInfos = new CharacterModel.RendererInfo[]
            {
                new CharacterModel.RendererInfo
                {
                    defaultMaterial = Resources.Load<Material>("@MoistureUpset_engi_turret:assets/unifiedtex.mat"),
                    defaultShadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On,
                    ignoreOverlays = false,
                    renderer = fuckthisrenderer[0]
                }
            };

            turretSkinDef.meshReplacements = new SkinDef.MeshReplacement[]
            {
                new SkinDef.MeshReplacement
                {
                    mesh = Resources.Load<Mesh>("@MoistureUpset_engi_turret:assets/normal_sentry.mesh"),
                    renderer = fuckthisrenderer[0]
                }
            };

            On.RoR2.SkinDef.Awake -= SkinDef_Awake;


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
                        minionBodyPrefab = fuckthisbodyprefab,
                        minionSkin = turretSkinDef
                    }
                }
            };


            Array.Resize(ref skinController.skins, skinController.skins.Length + 1);
            skinController.skins[skinController.skins.Length - 1] = LoadoutAPI.CreateNewSkinDef(skin);

            var skinsField = Reflection.GetFieldValue<SkinDef[][]>(typeof(BodyCatalog), "skins");
            skinsField[BodyCatalog.FindBodyIndex(bodyPrefab)] = skinController.skins;
            Reflection.SetFieldValue(typeof(BodyCatalog), "skins", skinsField);
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
