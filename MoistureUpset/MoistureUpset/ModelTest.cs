using BepInEx;
using R2API;
using R2API.Utils;
using RoR2;
using System;
using System.Reflection;
using UnityEngine;

namespace SkinTest
{

    public static class SkinTest
    {

        public static void AddLumberJackSkin()
        {
            On.RoR2.SurvivorCatalog.Init += (orig) =>
            {
                orig();
                //Getting character's prefab
                var survivorDef = SurvivorCatalog.GetSurvivorDef(SurvivorIndex.Engi);
                var bodyPrefab = survivorDef.bodyPrefab;

                //Getting necessary components
                var renderers = bodyPrefab.GetComponentsInChildren<Renderer>();
                var skinController = bodyPrefab.GetComponentInChildren<ModelSkinController>();
                var mdl = skinController.gameObject;

                var skin = new LoadoutAPI.SkinDefInfo
                {
                    Icon = LoadoutAPI.CreateSkinIcon(Color.black, Color.white, new Color(0.5F, 0.3F, 0), Color.white),
                    Name = "Sex",
                    NameToken = "Sex2",
                    RootObject = mdl,
                    //Defining skins that will be applyed before our.
                    //Because we will replace only Commando mesh, but not his pistols we have to use base skin.
                    BaseSkins = new SkinDef[] { skinController.skins[0] },
                    UnlockableName = "",
                    GameObjectActivations = new SkinDef.GameObjectActivation[0],
                    //Here we define that Commando mesh will use our material, also other materials can be replaced.
                    RendererInfos = new CharacterModel.RendererInfo[]
                    {
                    new CharacterModel.RendererInfo
                    {
                        defaultMaterial = Resources.Load<Material>("@MoistureUpset:Assets/engineer/models_player_engineer_engineer_red.mat"),
                        defaultShadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On,
                        ignoreOverlays = false,
                        renderer = renderers[0]
                    }
                    },
                    //Here we define that Commando mesh will be replaced by our mesh, also other meshes can be replaced.
                    MeshReplacements = new SkinDef.MeshReplacement[]
                    {
                    new SkinDef.MeshReplacement
                    {
                        mesh = Resources.Load<Mesh>("@MoistureUpset:Assets/engineer/mesh0.mesh"),
                        renderer = renderers[0]
                    }
                    },
                    ProjectileGhostReplacements = new SkinDef.ProjectileGhostReplacement[0],
                    MinionSkinReplacements = new SkinDef.MinionSkinReplacement[0]
                };

                //Adding new skin to a character's skin controller
                Array.Resize(ref skinController.skins, skinController.skins.Length + 1);
                skinController.skins[skinController.skins.Length - 1] = LoadoutAPI.CreateNewSkinDef(skin);

                //Adding new skin into BodyCatalog
                var skinsField = Reflection.GetFieldValue<SkinDef[][]>(typeof(BodyCatalog), "skins");
                skinsField[BodyCatalog.FindBodyIndex(bodyPrefab)] = skinController.skins;
                Reflection.SetFieldValue(typeof(BodyCatalog), "skins", skinsField);
            };
        }
    }
}