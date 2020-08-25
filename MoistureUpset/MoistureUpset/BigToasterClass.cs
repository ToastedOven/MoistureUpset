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

namespace MoistureUpset
{
    public static class BigToasterClass
    {
        public static void RunAll()
        {
            DeathSound();
            Somebody();
            BossMusic();
            DropRewards();
            Death();
        }
        //public static void OnHit()
        //{
        //    On.RoR2.Projectile.ProjectileSingleTargetImpact.OnProjectileImpact += (orig, self, info) =>
        //    {
        //        if (!self.GetPropertyValue<bool>("alive"))
        //        {
        //            return;
        //        }
        //        Collider collider = info.collider;
        //        if (collider)
        //        {
        //            DamageInfo damageInfo = new DamageInfo();
        //            if (self.GetPropertyValue<RoR2.Projectile.ProjectileDamage>("projectileDamage"))
        //            {
        //                damageInfo.damage = self.GetPropertyValue<RoR2.Projectile.ProjectileDamage>("projectileDamage").damage;
        //                damageInfo.crit = self.GetPropertyValue<RoR2.Projectile.ProjectileDamage>("projectileDamage").crit;
        //                damageInfo.attacker = self.GetPropertyValue<RoR2.Projectile.ProjectileController>("projectileController").owner;
        //                damageInfo.inflictor = base.gameObject;
        //                damageInfo.position = info.estimatedPointOfImpact;
        //                damageInfo.force = self.GetPropertyValue<RoR2.Projectile.ProjectileDamage>("projectileDamage").force * base.transform.forward;
        //                damageInfo.procChainMask = self.GetPropertyValue<RoR2.Projectile.ProjectileController>("projectileController").procChainMask;
        //                damageInfo.procCoefficient = self.GetPropertyValue<RoR2.Projectile.ProjectileController>("projectileController").procCoefficient;
        //                damageInfo.damageColorIndex = self.GetPropertyValue<RoR2.Projectile.ProjectileDamage>("projectileDamage").damageColorIndex;
        //                damageInfo.damageType = self.GetPropertyValue<RoR2.Projectile.ProjectileDamage>("projectileDamage").damageType;
        //            }
        //            else
        //            {
        //                Debug.Log("No projectile damage component!");
        //            }
        //            HurtBox component = collider.GetComponent<HurtBox>();
        //            if (component)
        //            {
        //                HealthComponent healthComponent = component.healthComponent;
        //                if (healthComponent)
        //                {
        //                    if (healthComponent.gameObject == self.GetPropertyValue<RoR2.Projectile.ProjectileController>("projectileController").owner)
        //                    {
        //                        return;
        //                    }
        //                    if (FriendlyFireManager.ShouldDirectHitProceed(healthComponent, self.GetPropertyValue<RoR2.Projectile.ProjectileController>("projectileController").teamFilter.teamIndex))
        //                    {
        //                        Util.PlaySound(self.enemyHitSoundString, base.gameObject);
        //                        if (NetworkServer.active)
        //                        {
        //                            damageInfo.ModifyDamageInfo(component.damageModifier);
        //                            healthComponent.TakeDamage(damageInfo);
        //                            GlobalEventManager.instance.OnHitEnemy(damageInfo, component.healthComponent.gameObject);
        //                        }
        //                    }
        //                    self.InvokeMethod<bool>("set_alive", false);
        //                }
        //            }
        //            else if (self.destroyOnWorld)
        //            {
        //                self.InvokeMethod<bool>("set_alive", false);
        //            }
        //            damageInfo.position = base.transform.position;
        //            if (NetworkServer.active)
        //            {
        //                GlobalEventManager.instance.OnHitAll(damageInfo, collider.gameObject);
        //            }
        //        }
        //        if (!self.GetPropertyValue<bool>("alive"))
        //        {
        //            if (NetworkServer.active && self.impactEffect)
        //            {
        //                EffectManager.SimpleImpactEffect(self.impactEffect, info.estimatedPointOfImpact, -base.transform.forward, !self.GetPropertyValue<RoR2.Projectile.ProjectileController>("projectileController").isPrediction);
        //            }
        //            Util.PlaySound(self.hitSoundString, base.gameObject);
        //            if (self.destroyWhenNotAlive)
        //            {
        //                UnityEngine.Object.Destroy(base.gameObject);
        //            }
        //        }
        //    };
        //}
        public static void Death()
        {
            On.RoR2.Chat.PlayerDeathChatMessage.ConstructChatString += (orig, self) =>
            {
                string text = orig(self);
                return $"{text} <style=cDeath>loser alert.</style>";
            };
        }
        public static void DeathSound()
        {
            On.EntityStates.GenericCharacterDeath.PlayDeathSound += (orig, self) =>
            {
                Util.PlaySound("EDeath", self.outer.gameObject);
            };
        }
        public static void DropRewards()
        {
            On.RoR2.MusicController.UpdateTeleporterParameters += (orig, self, t, cT, tB) =>
            {
                try
                {
                    bool flag = true;
                    flag = t.holdoutZoneController.IsBodyInChargingRadius(tB);
                    AkSoundEngine.SetRTPCValue("isInPortalRange", (flag ? 1f : 0f));
                    if (TeleporterInteraction.instance.isCharged)
                    {
                        var mainBody = NetworkUser.readOnlyLocalPlayersList[0].master?.GetBody();
                        AkSoundEngine.PostEvent("EndBossMusic", mainBody.gameObject);
                    }
                }
                catch (Exception)
                {

                }
                orig(self, t, cT, tB);

            };
            On.RoR2.BossGroup.DropRewards += (orig, self) =>
            {
                orig(self);
                try
                {
                    var mainBody = NetworkUser.readOnlyLocalPlayersList[0].master?.GetBody();
                    Util.PlaySound("BossDied", mainBody.gameObject);
                }
                catch (Exception)
                {
                }
            };
            On.RoR2.BossGroup.OnEnable += (orig, self) =>
            {
                orig(self);
                try
                {
                    var mainBody = NetworkUser.readOnlyLocalPlayersList[0].master?.GetBody();
                    AkSoundEngine.PostEvent("PlayBossMusic", mainBody.gameObject);
                }
                catch (Exception)
                {
                }
            };
        }
        public static void Somebody()
        {
            On.EntityStates.SurvivorPod.PreRelease.OnEnter += (orig, self) =>
            {
                orig(self);
                Util.PlaySound("somebody", self.outer.gameObject);
            };
        }
        public static void BossMusic()
        {
            On.RoR2.WwiseUtils.CommonWwiseIds.Init += (orig) =>
            {
                orig();
                //RoR2.WwiseUtils.CommonWwiseIds.bossfight = AkSoundEngine.GetIDFromString("ooflongestloop");
                try
                {
                    RoR2.WwiseUtils.CommonWwiseIds.alive = AkSoundEngine.GetIDFromString("ooflongestloop");
                    RoR2.WwiseUtils.CommonWwiseIds.dead = AkSoundEngine.GetIDFromString("ooflongestloop");
                }
                catch (Exception)
                {
                }
                //RoR2.WwiseUtils.CommonWwiseIds.gameplay = AkSoundEngine.GetIDFromString("ooflongestloop");
                //RoR2.WwiseUtils.CommonWwiseIds.logbook = AkSoundEngine.GetIDFromString("ooflongestloop");
                //RoR2.WwiseUtils.CommonWwiseIds.main = AkSoundEngine.GetIDFromString("ooflongestloop");
                //RoR2.WwiseUtils.CommonWwiseIds.menu = AkSoundEngine.GetIDFromString("ooflongestloop");
                //RoR2.WwiseUtils.CommonWwiseIds.none = AkSoundEngine.GetIDFromString("ooflongestloop");
                //RoR2.WwiseUtils.CommonWwiseIds.secretLevel = AkSoundEngine.GetIDFromString("ooflongestloop");
            };
            //On.RoR2.WwiseUtils.SoundbankLoader.Start += (orig, self) =>
            //{
            //    //orig(self);
            //    for (int i = 0; i < self.soundbankStrings.Length; i++)
            //    {
            //        if (self.soundbankStrings[i] == "Music")
            //        {
            //            AkBankManager.LoadBank(self.soundbankStrings[i], self.decodeBank, self.saveDecodedBank);
            //            //AkBankManager.LoadBank("MusicReplacement", self.decodeBank, self.saveDecodedBank);
            //            //System.Console.WriteLine($"Loading MusicReplacement before {self.soundbankStrings[i]}");
            //        }
            //        else
            //        {
            //            AkBankManager.LoadBank(self.soundbankStrings[i], self.decodeBank, self.saveDecodedBank);
            //        }
            //    }
            //};
        }
    }
}
