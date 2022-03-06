using BepInEx;
using R2API.Utils;
using RoR2;
using R2API;
using R2API.MiscHelpers;
using System.Reflection;
using static R2API.SoundAPI;
using System;
using UnityEngine;
using RoR2.Networking;
using System.IO;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Text;
using RiskOfOptions;
using MoistureUpset.InteractReplacements.SodaBarrel;
using R2API.Networking.Interfaces;
using UnityEngine.Networking;

namespace MoistureUpset.InteractReplacements
{
    public static class Interactables
    {
        public static void Init()
        {
            Chests();
        }
        public static GameObject particles;
        public static void ReloadChests()
        {

            if (BigJank.getOptionValue("Interactables") == 1)
            {

                var blank = Resources.Load<Texture>("@MoistureUpset_na:assets/blank.png");
                var cUm = Resources.Load<GameObject>("prefabs/networkedobjects/chest/Chest1");
                EnemyReplacements.ReplaceModel("prefabs/networkedobjects/chest/Chest1", "@MoistureUpset_moisture_chests:assets/arbitraryfolder/smallchest.mesh", "@MoistureUpset_moisture_chests:assets/arbitraryfolder/smallchest.png");
                cUm.GetComponentInChildren<SkinnedMeshRenderer>().material.SetTexture("_SplatmapTex", Resources.Load<Texture>("@MoistureUpset_moisture_chests:assets/arbitraryfolder/smallchestsplat.png"));
                cUm.AddComponent<NewSplatSystemRemover>();


                EnemyReplacements.ReplaceModel("prefabs/networkedobjects/chest/CategoryChestDamage", "@MoistureUpset_moisture_chests:assets/arbitraryfolder/categorychest.mesh", "@MoistureUpset_moisture_chests:assets/arbitraryfolder/damagechest.png");
                EnemyReplacements.ReplaceMeshFilter("prefabs/networkedobjects/chest/CategoryChestDamage", "@MoistureUpset_na:assets/na1.mesh");
                cUm = Resources.Load<GameObject>("prefabs/networkedobjects/chest/CategoryChestDamage");
                cUm.GetComponentInChildren<SkinnedMeshRenderer>().material.SetTexture("_SplatmapTex", Resources.Load<Texture>("@MoistureUpset_moisture_chests:assets/arbitraryfolder/largechestsplat.png"));
                cUm.AddComponent<NewSplatSystemRemover>();

                EnemyReplacements.ReplaceModel("prefabs/networkedobjects/chest/CategoryChestHealing", "@MoistureUpset_moisture_chests:assets/arbitraryfolder/categorychest.mesh", "@MoistureUpset_moisture_chests:assets/arbitraryfolder/healingchest.png");
                EnemyReplacements.ReplaceMeshFilter("prefabs/networkedobjects/chest/CategoryChestHealing", "@MoistureUpset_na:assets/na1.mesh");
                cUm = Resources.Load<GameObject>("prefabs/networkedobjects/chest/CategoryChestHealing");
                cUm.GetComponentInChildren<SkinnedMeshRenderer>().material.SetTexture("_SplatmapTex", Resources.Load<Texture>("@MoistureUpset_moisture_chests:assets/arbitraryfolder/largechestsplat.png"));
                cUm.AddComponent<NewSplatSystemRemover>();

                EnemyReplacements.ReplaceModel("prefabs/networkedobjects/chest/CategoryChestUtility", "@MoistureUpset_moisture_chests:assets/arbitraryfolder/categorychest.mesh", "@MoistureUpset_moisture_chests:assets/arbitraryfolder/utilitychest.png");
                EnemyReplacements.ReplaceMeshFilter("prefabs/networkedobjects/chest/CategoryChestUtility", "@MoistureUpset_na:assets/na1.mesh");
                cUm = Resources.Load<GameObject>("prefabs/networkedobjects/chest/CategoryChestUtility");
                cUm.GetComponentInChildren<SkinnedMeshRenderer>().material.SetTexture("_SplatmapTex", Resources.Load<Texture>("@MoistureUpset_moisture_chests:assets/arbitraryfolder/largechestsplat.png"));
                cUm.AddComponent<NewSplatSystemRemover>();

                EnemyReplacements.ReplaceModel("prefabs/networkedobjects/chest/EquipmentBarrel", "@MoistureUpset_moisture_chests:assets/arbitraryfolder/shulker.mesh", "@MoistureUpset_moisture_chests:assets/arbitraryfolder/shulker.png");
                cUm = Resources.Load<GameObject>("prefabs/networkedobjects/chest/EquipmentBarrel");
                cUm.GetComponentInChildren<SkinnedMeshRenderer>().material.SetInt("_Cull", 0);
                cUm.AddComponent<equipmentbarrelfixer>();
                cUm.GetComponentInChildren<SfxLocator>().openSound = "EquipmentBarrel";

                EnemyReplacements.ReplaceModel("prefabs/networkedobjects/chest/Chest2", "@MoistureUpset_moisture_chests:assets/arbitraryfolder/largechest.mesh", "@MoistureUpset_moisture_chests:assets/arbitraryfolder/largechest.png");
                EnemyReplacements.ReplaceModel("prefabs/networkedobjects/chest/GoldChest", "@MoistureUpset_moisture_chests:assets/arbitraryfolder/goldchest.mesh", "@MoistureUpset_moisture_chests:assets/arbitraryfolder/goldchest.png");
                cUm = Resources.Load<GameObject>("prefabs/networkedobjects/chest/GoldChest");
                cUm.GetComponentInChildren<SkinnedMeshRenderer>().material.shader = Resources.Load<GameObject>("prefabs/networkedobjects/chest/Chest2").GetComponentInChildren<SkinnedMeshRenderer>().material.shader;
                cUm.GetComponentInChildren<SkinnedMeshRenderer>().material.mainTexture.filterMode = FilterMode.Point;
                cUm.GetComponentInChildren<ParticleSystem>().maxParticles = 0;
                cUm.GetComponentInChildren<SfxLocator>().openSound = "GoldChest";
                cUm.AddComponent<NewSplatSystemRemover>();

                particles = Resources.Load<GameObject>("@MoistureUpset_moisture_chests:assets/arbitraryfolder/particles.prefab");
                particles.transform.SetParent(cUm.transform);
                particles.transform.localPosition = Vector3.zero;

                EnemyReplacements.ReplaceModel("prefabs/networkedobjects/chest/Barrel1", "@MoistureUpset_InteractReplacements_SodaBarrel_sodaspritz:assets/sodafountain/cylinder.mesh");
                cUm = Resources.Load<GameObject>("prefabs/networkedobjects/chest/Barrel1");
                cUm.GetComponentInChildren<SkinnedMeshRenderer>().material.SetTexture("_SplatmapTex", Resources.Load<Texture>("@MoistureUpset_InteractReplacements_SodaBarrel_sodaspritz:assets/sodafountain/sodaSplatmap.png"));
                cUm.AddComponent<RandomizeSoda>();
                {
                    GameObject spritzPrefab = Resources.Load<GameObject>("@MoistureUpset_InteractReplacements_SodaBarrel_sodaspritz:assets/sodafountain/soda spritz.prefab");
                    spritzPrefab.transform.SetParent(cUm.transform);
                    spritzPrefab.transform.localPosition = new Vector3(0, 1.2f, -0.4f);
                }
                cUm.GetComponentInChildren<SfxLocator>().openSound = "Soda";
                cUm.AddComponent<NewSplatSystemRemover>();

                cUm = Resources.Load<GameObject>("prefabs/networkedobjects/chest/TripleShopLarge");
                cUm.GetComponentInChildren<MeshFilter>().mesh = Resources.Load<Mesh>("@MoistureUpset_na:assets/na1.mesh");
                cUm.AddComponent<Fixers.spinnerfixer>().pos = 5;

                cUm = Resources.Load<GameObject>("prefabs/networkedobjects/chest/TripleShop");
                cUm.GetComponentInChildren<MeshFilter>().mesh = Resources.Load<Mesh>("@MoistureUpset_na:assets/na1.mesh");
                cUm.AddComponent<Fixers.spinnerfixer>().pos = 4.5f;

                cUm = Resources.Load<GameObject>("prefabs/networkedobjects/chest/TripleShopEquipment");
                cUm.GetComponentInChildren<MeshFilter>().mesh = Resources.Load<Mesh>("@MoistureUpset_na:assets/na1.mesh");
                cUm.AddComponent<Fixers.spinnerfixer>().pos = 4.5f;



                cUm = Resources.Load<GameObject>("prefabs/networkedobjects/chest/MultiShopTerminal");
                cUm.GetComponentInChildren<SkinnedMeshRenderer>().material = Resources.Load<GameObject>("prefabs/networkedobjects/chest/Barrel1").GetComponentInChildren<SkinnedMeshRenderer>().material;
                EnemyReplacements.ReplaceModel("prefabs/networkedobjects/chest/MultiShopTerminal", "@MoistureUpset_moisture_chests:assets/arbitraryfolder/spinnerpart.mesh", "@MoistureUpset_moisture_chests:assets/arbitraryfolder/spinnerred.png");
                cUm.GetComponentInChildren<SkinnedMeshRenderer>().material.SetTexture("_SplatmapTex", Resources.Load<Texture>("@MoistureUpset_moisture_chests:assets/arbitraryfolder/faheet.png"));
                cUm.AddComponent<NewSplatSystemRemover>();

                cUm = Resources.Load<GameObject>("prefabs/networkedobjects/chest/MultiShopLargeTerminal");
                cUm.GetComponentInChildren<SkinnedMeshRenderer>().material = Resources.Load<GameObject>("prefabs/networkedobjects/chest/Barrel1").GetComponentInChildren<SkinnedMeshRenderer>().material;
                EnemyReplacements.ReplaceModel("prefabs/networkedobjects/chest/MultiShopLargeTerminal", "@MoistureUpset_moisture_chests:assets/arbitraryfolder/spinnerpart.mesh", "@MoistureUpset_moisture_chests:assets/arbitraryfolder/spinnerred.png");
                cUm.GetComponentInChildren<SkinnedMeshRenderer>().material.SetTexture("_SplatmapTex", Resources.Load<Texture>("@MoistureUpset_moisture_chests:assets/arbitraryfolder/faheet.png"));
                cUm.AddComponent<NewSplatSystemRemover>();

                cUm = Resources.Load<GameObject>("prefabs/networkedobjects/chest/MultiShopEquipmentTerminal");
                cUm.GetComponentInChildren<SkinnedMeshRenderer>().material = Resources.Load<GameObject>("prefabs/networkedobjects/chest/Barrel1").GetComponentInChildren<SkinnedMeshRenderer>().material;
                EnemyReplacements.ReplaceModel("prefabs/networkedobjects/chest/MultiShopEquipmentTerminal", "@MoistureUpset_moisture_chests:assets/arbitraryfolder/spinnerpart.mesh", "@MoistureUpset_moisture_chests:assets/arbitraryfolder/spinnerred.png");
                cUm.GetComponentInChildren<SkinnedMeshRenderer>().material.SetTexture("_SplatmapTex", Resources.Load<Texture>("@MoistureUpset_moisture_chests:assets/arbitraryfolder/faheet.png"));
                cUm.AddComponent<NewSplatSystemRemover>();

                if (BigJank.getOptionValue("Currency Changes") == 1)
                {
                    cUm = Resources.Load<GameObject>("prefabs/networkedobjects/NewtStatue");
                    GameObject g = Resources.Load<GameObject>("@MoistureUpset_moisture_newtaltar:assets/testing/atoasteroven.prefab");
                    g.transform.parent = cUm.transform;
                    g.transform.localPosition = Vector3.zero;
                    g.transform.localEulerAngles = Vector3.zero;
                }
            }
            if (BigJank.getOptionValue("Shrine Changes") == 1)
            {
                GameObject gathan = Resources.Load<GameObject>("prefabs/networkedobjects/shrines/ShrineChance");
                foreach (var item in gathan.GetComponentInChildren<MeshRenderer>().material.GetTexturePropertyNames())
                {
                    gathan.GetComponentInChildren<MeshRenderer>().sharedMaterial.SetTexture(item, Resources.Load<Texture>("@MoistureUpset_na:assets/blank.png"));
                }
                EnemyReplacements.ReplaceMeshRenderer("prefabs/networkedobjects/shrines/ShrineChance", "@MoistureUpset_moisture_lego:assets/arbitraryfolder/lego.mesh", "@MoistureUpset_moisture_lego:assets/arbitraryfolder/lego.png", 0);
                EnemyReplacements.ReplaceMeshRenderer("prefabs/networkedobjects/shrines/ShrineChance", "@MoistureUpset_moisture_lego:assets/arbitraryfolder/chancesymbol.png", 1);

            }
        }
        private static void Chests()
        {
            if (BigJank.getOptionValue("Shrine Changes") == 1)
            {
                EnemyReplacements.LoadResource("moisture_lego");
            }
            if (BigJank.getOptionValue("Interactables") == 1)
            {
                EnemyReplacements.LoadBNK("Chest");
                EnemyReplacements.LoadResource("moisture_chests");
                EnemyReplacements.LoadResource("moisture_newtaltar");
                Skins.Utils.LoadAsset("InteractReplacements.SodaBarrel.sodaspritz");
                On.RoR2.BarrelInteraction.OnInteractionBegin += SpraySoda;
                On.RoR2.BarrelInteraction.OnDeserialize += SpraySoda;
                On.RoR2.MultiShopController.OnPurchase += (orig, self, interactor, interaction) =>
                {
                    orig(self, interactor, interaction);
                    List<NetworkInstanceId> ids = new List<NetworkInstanceId>();
                    ids.Add(self.GetComponent<NetworkIdentity>().netId);
                    int available = 0;
                    foreach (var item in self.GetFieldValue<GameObject[]>("terminalGameObjects")) //these have networkidentities, find netID?
                    {
                        ids.Add(item.GetComponent<NetworkIdentity>().netId);
                        if (item.GetComponent<MultiShopController>().Networkavailable)
                        {
                            available += 1;
                        }
                    }
                    if (available == 0 && ids.Count == 4)
                    {
                        new SyncFidget(ids[0], ids[1], ids[2], ids[3]).Send(R2API.Networking.NetworkDestination.Clients);
                    }
                };


                Texture[] textures = new Texture[] { Resources.Load<Texture>("@MoistureUpset_moisture_chests:assets/arbitraryfolder/spinnerred.png"), Resources.Load<Texture>("@MoistureUpset_moisture_chests:assets/arbitraryfolder/spinnerred.png"), Resources.Load<Texture>("@MoistureUpset_moisture_chests:assets/arbitraryfolder/spinneryellow.png"), Resources.Load<Texture>("@MoistureUpset_moisture_chests:assets/arbitraryfolder/spinnerorange.png"), Resources.Load<Texture>("@MoistureUpset_moisture_chests:assets/arbitraryfolder/spinnermagenta.png"), Resources.Load<Texture>("@MoistureUpset_moisture_chests:assets/arbitraryfolder/spinnergreen.png"), Resources.Load<Texture>("@MoistureUpset_moisture_chests:assets/arbitraryfolder/spinnerblue.png"), Resources.Load<Texture>("@MoistureUpset_moisture_chests:assets/arbitraryfolder/spinnercyan.png") };
                On.RoR2.MultiShopController.CreateTerminals += (orig, self) =>
                {
                    orig(self);
                    int num = UnityEngine.Random.Range(0, textures.Length - 1);
                    float r = UnityEngine.Random.Range(0, .71f);
                    foreach (var item in self.GetFieldValue<GameObject[]>("terminalGameObjects"))
                    {
                        item.GetComponentInChildren<SkinnedMeshRenderer>().material.mainTexture = textures[num];
                        item.GetComponentInChildren<RandomizeSplatBias>().maxGreenBias = r;
                        item.GetComponentInChildren<RandomizeSplatBias>().minGreenBias = r;
                        item.GetComponentInChildren<RandomizeSplatBias>().InvokeMethod("Start");
                    }
                };
                if (BigJank.getOptionValue("Currency Changes") == 1)
                {
                    On.RoR2.PurchaseInteraction.OnInteractionBegin += (orig, self, i) =>
                    {
                        if (self.CanBeAffordedByInteractor(i))
                        {
                            try
                            {
                                if (self.gameObject.ToString().Contains("NewtStatue"))
                                {
                                    if (self.gameObject.GetComponent<Fixers.robloxfixer>().a.name == "AToasterOven(Clone)")
                                    {
                                        self.gameObject.GetComponent<Fixers.robloxfixer>().a.Play("Backflip");
                                    }
                                    else if (self.gameObject.GetComponent<Fixers.robloxfixer>().a.name == "RuneMasterGaming580808080808080ADHD(Clone)")
                                    {
                                        int num = UnityEngine.Random.Range(0, 4);
                                        self.gameObject.GetComponent<Fixers.robloxfixer>().a.Play($"r_death{num + 1}");
                                    }
                                    else
                                    {
                                        self.gameObject.GetComponent<Fixers.robloxfixer>().a.CrossFade("Backflip", .4f);
                                    }
                                    self.gameObject.GetComponent<Fixers.robloxfixer>().bought = true;
                                }
                            }
                            catch (Exception)
                            {
                            }
                        }
                        orig(self, i);
                    };
                    On.RoR2.PurchaseInteraction.OnDeserialize += (orig, self, r, i) =>
                    {
                        orig(self, r, i);
                        if (self.gameObject.ToString().Contains("NewtStatue") && !self.available)
                        {
                            if (self.gameObject.GetComponent<Fixers.robloxfixer>().a.name == "AToasterOven(Clone)")
                            {
                                self.gameObject.GetComponent<Fixers.robloxfixer>().a.Play("Backflip");
                            }
                            else if (self.gameObject.GetComponent<Fixers.robloxfixer>().a.name == "RuneMasterGaming580808080808080ADHD(Clone)")
                            {
                                int num = UnityEngine.Random.Range(0, 4);
                                self.gameObject.GetComponent<Fixers.robloxfixer>().a.Play($"r_death{num + 1}");
                            }
                            else
                            {
                                self.gameObject.GetComponent<Fixers.robloxfixer>().a.CrossFade("Backflip", .4f);
                            }
                            self.gameObject.GetComponent<Fixers.robloxfixer>().bought = true;
                        }
                    };
                }
            }



            ReloadChests();
        }

        private static void SpraySoda(On.RoR2.BarrelInteraction.orig_OnDeserialize orig, BarrelInteraction self, NetworkReader reader, bool initialState)
        {
            orig(self, reader, initialState);

            if (self.GetFieldValue<bool>("opened"))
            {
                Color spritz = self.gameObject.GetComponentInChildren<RandomizeSoda>().getSpritzColor();

                var col = self.gameObject.GetComponentInChildren<ParticleSystem>().colorOverLifetime;

                Gradient gradient = col.color.gradient;

                gradient.SetKeys(new GradientColorKey[] { new GradientColorKey(spritz, 0.0f), new GradientColorKey(gradient.colorKeys[1].color, gradient.colorKeys[1].time) }, gradient.alphaKeys);

                col.color = gradient;

                self.gameObject.GetComponentInChildren<ParticleSystem>().Play();
            }
        }

        private static void SpraySoda(On.RoR2.BarrelInteraction.orig_OnInteractionBegin orig, BarrelInteraction self, Interactor activator)
        {
            orig(self, activator);

            Color spritz = self.gameObject.GetComponentInChildren<RandomizeSoda>().getSpritzColor();

            var col = self.gameObject.GetComponentInChildren<ParticleSystem>().colorOverLifetime;

            Gradient gradient = col.color.gradient;

            gradient.SetKeys(new GradientColorKey[] { new GradientColorKey(spritz, 0.0f), new GradientColorKey(gradient.colorKeys[1].color, gradient.colorKeys[1].time) }, gradient.alphaKeys);

            col.color = gradient;

            self.gameObject.GetComponentInChildren<ParticleSystem>().Play();
        }
    }

    public class SyncFidget : INetMessage
    {
        NetworkInstanceId baseterminal;
        NetworkInstanceId terminal1;
        NetworkInstanceId terminal2;
        NetworkInstanceId terminal3;

        public SyncFidget()
        {

        }

        public SyncFidget(NetworkInstanceId netId, NetworkInstanceId t1, NetworkInstanceId t2, NetworkInstanceId t3)
        {
            baseterminal = netId;
            terminal1 = t1;
            terminal2 = t2;
            terminal3 = t3;
        }

        public void Deserialize(NetworkReader reader)
        {
            baseterminal = reader.ReadNetworkId();
            terminal1 = reader.ReadNetworkId();
            terminal2 = reader.ReadNetworkId();
            terminal3 = reader.ReadNetworkId();
        }

        public void OnReceived()
        {
            GameObject bodyObject = Util.FindNetworkObject(baseterminal);
            Util.FindNetworkObject(terminal1).AddComponent<Fixers.terminalfixer>().center = bodyObject.transform.position;
            Util.FindNetworkObject(terminal2).AddComponent<Fixers.terminalfixer>().center = bodyObject.transform.position;
            Util.FindNetworkObject(terminal3).AddComponent<Fixers.terminalfixer>().center = bodyObject.transform.position;
        }

        public void Serialize(NetworkWriter writer)
        {
            writer.Write(baseterminal);
            writer.Write(terminal1);
            writer.Write(terminal2);
            writer.Write(terminal3);
        }
    }
}
