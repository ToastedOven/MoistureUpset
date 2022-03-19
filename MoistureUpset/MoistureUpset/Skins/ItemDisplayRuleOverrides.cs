using System.IO;
using RoR2;
using SimpleJSON;
using UnityEngine;
using Path = System.IO.Path;

namespace MoistureUpset.Skins
{
    internal static class ItemDisplayRuleOverrides
    {
        public static void ExportRuleSet(GameObject bodyPrefab)
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
    }
}