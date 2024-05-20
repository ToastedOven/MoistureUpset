using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using RoR2;
using SimpleJSON;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace MoistureUpset.Skins.ItemDisplayRules
{
    internal static class ItemDisplayRuleOverrides
    {
        private static readonly Dictionary<string, string> BodyNameSkinNameMap = new Dictionary<string, string>();
        private static readonly Dictionary<string, DisplayRuleOverride[]> DisplayRuleOverrideQueue = new Dictionary<string, DisplayRuleOverride[]>();
        private static readonly Dictionary<string, ItemDisplayRuleSet> DisplayRuleSets = new Dictionary<string, ItemDisplayRuleSet>();

        internal static void RegisterDisplayRuleOverride(string bodyName, string skinName, string overrideName)
        {
            BodyNameSkinNameMap.Add(bodyName, skinName);
            DisplayRuleOverrideQueue.Add(bodyName, LoadDisplayRuleGroup(overrideName));
        }

        internal static void GenerateDisplayRuleOverride(string bodyName, GameObject bodyPrefab)
        {
            if (!DisplayRuleOverrideQueue.TryGetValue(bodyName, out var displayRuleOverrides))
                return;

            var originalRuleGroups = bodyPrefab.GetComponentInChildren<CharacterModel>().itemDisplayRuleSet.keyAssetRuleGroups;
            var modifiedRuleGroups = new ItemDisplayRuleSet.KeyAssetRuleGroup[originalRuleGroups.Length];

            for (var i = 0; i < originalRuleGroups.Length; i++)
            {
                modifiedRuleGroups[i] = originalRuleGroups[i].Clone();

                string keyAssetName = modifiedRuleGroups[i].keyAsset.name;

                if (displayRuleOverrides.All(ruleOverride => ruleOverride.keyAssetName != keyAssetName))
                    continue;
                
                DisplayRuleOverride ruleOverride = displayRuleOverrides.First(ruleOverride => ruleOverride.keyAssetName == keyAssetName);

                DisplayRuleGroup ruleGroup = modifiedRuleGroups[i].displayRuleGroup;

                for (var j = 0; j < ruleGroup.rules.Length; j++)
                {
                    if (ruleGroup.rules[j].ruleType != ItemDisplayRuleType.ParentedPrefab)
                        continue;

                    ruleGroup.rules[j].childName = ruleOverride.childName;
                    ruleGroup.rules[j].followerPrefab = Addressables.LoadAssetAsync<GameObject>(ruleOverride.prefabPath).WaitForCompletion();
                    ruleGroup.rules[j].localPos = ruleOverride.position;
                    ruleGroup.rules[j].localAngles = ruleOverride.rotation;
                    ruleGroup.rules[j].localScale = ruleOverride.scale;
                }
            }

            var instance = ScriptableObject.CreateInstance<ItemDisplayRuleSet>();

            instance.keyAssetRuleGroups = modifiedRuleGroups;

            instance.name = BodyNameSkinNameMap[bodyName];

            DisplayRuleSets.Add(BodyNameSkinNameMap[bodyName], instance);
        }

        internal static ItemDisplayRuleSet GetItemDisplayRuleSet(string skinName)
        {
            return DisplayRuleSets[skinName];
        }
        
        internal static void ExportRuleSet(GameObject bodyPrefab)
        {
            var model = bodyPrefab.GetComponentInChildren<CharacterModel>();

            var rootJson = new JSONObject();

            foreach (var keyAsset in model.itemDisplayRuleSet.keyAssetRuleGroups)
            {
                foreach (var rule in keyAsset.displayRuleGroup.rules)
                {
                    if (rule.ruleType == ItemDisplayRuleType.ParentedPrefab)
                    {
                        var jsonObject = new JSONObject
                        {
                            ["prefabName"] = $"{rule.followerPrefab.name}.prefab",
                            ["childName"] = rule.childName,
                            ["pos"] = rule.localPos.Serialize(),
                            ["rotation"] = rule.localAngles.Serialize(),
                            ["scale"] = rule.localScale.Serialize()
                        };

                        rootJson[keyAsset.keyAsset.name] = jsonObject;
                    }
                    else if (rule.ruleType == ItemDisplayRuleType.LimbMask)
                    {
                        rootJson[keyAsset.keyAsset.name]["mask"] = (int)rule.limbMask;
                    }
                }
            }
            
            File.WriteAllText($"{Directory.GetCurrentDirectory()}\\{bodyPrefab.name}.json", rootJson.ToString());
        }

        private static JSONNode Serialize(this Vector3 vector3)
        {
            var jsonVector = new JSONObject
            {
                ["x"] = vector3.x,
                ["y"] = vector3.y,
                ["z"] = vector3.z
            };

            return jsonVector;
        }

        private static DisplayRuleOverride[] LoadDisplayRuleGroup(string overrideName)
        {
            int index = 0;

            List<DisplayRuleOverride> overrides = new List<DisplayRuleOverride>();

            using var binaryStream = Assets.LoadDisplayRuleSetOverride(overrideName);

            while (index < binaryStream.Length)
            {
                int nameSize = binaryStream.ReadByte();
                index++;

                byte[] nameBytes = new byte[nameSize];
                binaryStream.Read(nameBytes, 0, nameSize);
                index += nameSize;

                int childNameSize = binaryStream.ReadByte();
                index++;

                byte[] childNameBytes = new byte[childNameSize];
                binaryStream.Read(childNameBytes, 0, childNameSize);
                index += childNameSize;

                int prefabPathSize = binaryStream.ReadByte();
                index++;

                byte[] prefabPathBytes = new byte[prefabPathSize];
                binaryStream.Read(prefabPathBytes, 0, prefabPathSize);
                index += prefabPathSize;

                DisplayRuleOverride display = new DisplayRuleOverride
                {
                    keyAssetName = Encoding.UTF8.GetString(nameBytes),
                    childName = Encoding.UTF8.GetString(childNameBytes),
                    prefabPath = Encoding.UTF8.GetString(prefabPathBytes),
                    position = binaryStream.ReadVector3(),
                    rotation = binaryStream.ReadVector3(),
                    scale = binaryStream.ReadVector3()
                };
                
                index += 36;
                
                overrides.Add(display);
            }

            return overrides.ToArray();
        }

        private static Vector3 ReadVector3(this Stream stream)
        {
            byte[] buffer = new byte[4];
            
            stream.Read(buffer, 0, 4);
            float x = BitConverter.ToSingle(buffer, 0);

            stream.Read(buffer, 0, 4);
            float y = BitConverter.ToSingle(buffer, 0);

            stream.Read(buffer, 0, 4);
            float z = BitConverter.ToSingle(buffer, 0);

            return new Vector3(x, y, z);
        }

        private static ItemDisplayRuleSet.KeyAssetRuleGroup Clone(this ItemDisplayRuleSet.KeyAssetRuleGroup original)
        {
            return new ItemDisplayRuleSet.KeyAssetRuleGroup
            {
                keyAsset = original.keyAsset,
                displayRuleGroup = original.displayRuleGroup.Clone()
            };
        }

        private static DisplayRuleGroup Clone(this DisplayRuleGroup original)
        {
            DisplayRuleGroup clone = new DisplayRuleGroup
            {
                rules = new ItemDisplayRule[original.rules.Length]
            };

            for (int i = 0; i < original.rules.Length; i++)
            {
                clone.rules[i] = new ItemDisplayRule();
                switch (original.rules[i].ruleType)
                {
                    case ItemDisplayRuleType.ParentedPrefab:
                        clone.rules[i].ruleType = ItemDisplayRuleType.ParentedPrefab;

                        clone.rules[i].childName = original.rules[i].childName;
                        clone.rules[i].followerPrefab = original.rules[i].followerPrefab;
                        clone.rules[i].localPos = original.rules[i].localPos;
                        clone.rules[i].localAngles = original.rules[i].localAngles;
                        clone.rules[i].localScale = original.rules[i].localScale;
                        break;
                    case ItemDisplayRuleType.LimbMask:
                        clone.rules[i].ruleType = ItemDisplayRuleType.LimbMask;

                        clone.rules[i].limbMask = original.rules[i].limbMask;
                        break;
                }
            }

            return clone;
        }
    }
}