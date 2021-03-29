using R2API;
using R2API.Utils;
using RoR2;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace MoistureUpset.Skins
{
    // If you see this, this hasn't been fully implemented yet. Keep it a secret. - Rune
    // We don't have any assets made for this skin. I'm just doing some prototyping right now.
    // Please don't expect anything just because you see this.
    public static class JotaroCaptain 
    {
        public static readonly string Name = "Jotaro Kujo";
        public static readonly string NameToken = "MOISTURE_UPSET_JOTARO_CAPTAIN_SKIN";

        // Runs on Awake
        public static void Init()
        {
            PopulateAssets();

            Utils.AddSkin("CaptainBody", CaptainSkin);

            AddToPrefab();
            CaptainDisplayFix();
        }

        // Load assets here
        private static void PopulateAssets()
        {
            Utils.LoadAsset("Skins.Jotaro.jotarosubtitle");
            Utils.LoadAsset("Skins.Jotaro.jotaro");
        }

        // Skindef stuff here
        private static SkinDef[] CaptainSkin(GameObject bodyPrefab)
        {
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
                        defaultMaterial = Assets.CreateMaterial("@MoistureUpset_Skins_Jotaro_jotaro:assets/jotaro/jotaro.png"),
                        defaultShadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On,
                        ignoreOverlays = false,
                        renderer = renderers[0]
                    },
                    new CharacterModel.RendererInfo
                    {
                        defaultMaterial = Assets.CreateMaterial("@MoistureUpset_Skins_Jotaro_jotaro:assets/jotaro/jotarohurt.png"),
                        defaultShadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On,
                        ignoreOverlays = false,
                        renderer = renderers[2]
                    }
                },

                MeshReplacements = new SkinDef.MeshReplacement[]
                {
                    new SkinDef.MeshReplacement
                    {
                        mesh = Resources.Load<Mesh>("@MoistureUpset_Skins_Jotaro_jotaro:assets/jotaro/jotaro.mesh"),
                        renderer = renderers[0]
                    },
                    new SkinDef.MeshReplacement
                    {
                        mesh = Resources.Load<Mesh>("@MoistureUpset_na:assets/na1.mesh"),
                        renderer = renderers[1]
                    },
                    new SkinDef.MeshReplacement
                    {
                        mesh = Resources.Load<Mesh>("@MoistureUpset_Skins_Jotaro_jotaro:assets/jotaro/jotaro.mesh"), // We use this skinnedmeshrenderer to be the hurt jotaro.
                        renderer = renderers[2]
                    },
                    new SkinDef.MeshReplacement
                    {
                        mesh = Resources.Load<Mesh>("@MoistureUpset_na:assets/na1.mesh"),
                        renderer = renderers[3]
                    },
                    new SkinDef.MeshReplacement
                    {
                        mesh = Resources.Load<Mesh>("@MoistureUpset_na:assets/na1.mesh"),
                        renderer = renderers[4]
                    },
                    new SkinDef.MeshReplacement
                    {
                        mesh = Resources.Load<Mesh>("@MoistureUpset_na:assets/na1.mesh"),
                        renderer = renderers[5]
                    },
                    new SkinDef.MeshReplacement
                    {
                        mesh = Resources.Load<Mesh>("@MoistureUpset_na:assets/na1.mesh"),
                        renderer = renderers[6]
                    }
                },
                ProjectileGhostReplacements = new SkinDef.ProjectileGhostReplacement[0],
                MinionSkinReplacements = new SkinDef.MinionSkinReplacement[0]
            };

            Array.Resize(ref skinController.skins, skinController.skins.Length + 1);
            skinController.skins[skinController.skins.Length - 1] = LoadoutAPI.CreateNewSkinDef(skin);

            SkinHelper.RegisterSkin(NameToken, "jotaro");

            return skinController.skins;
        }

        
       

        private static void CaptainDisplayFix()
        {
            var fab = Resources.Load<GameObject>("prefabs/characterdisplays/CaptainDisplay");

            fab.AddComponent<Jotaro.JotaroDisplayFix>(); // To fix the models not swapping back on skin change.
        }

        // Add stuff to the character prefab here
        private static void AddToPrefab()
        {
            GameObject captainBody = Resources.Load<GameObject>("prefabs/characterbodies/captainbody"); // load captain prefab
            captainBody.AddComponent<Jotaro.AddSubtitleBar>(); // add a script that will load the subtitle bar
            captainBody.AddComponent<Jotaro.SubtitleController>();
            captainBody.AddComponent<Jotaro.JotaroHurt>();
        }
    }
}
