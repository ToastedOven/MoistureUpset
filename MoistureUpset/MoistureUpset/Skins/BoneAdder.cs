using System.Collections.Generic;
using UnityEngine;

public static class BoneAdder
{
    public static Transform[] boneList;

    public static void DebugDefaultBones()
    {
        foreach (var item in boneList)
        {
            Debug.Log($"bone: [{item}] parent: [{item.parent}] childcount: [{item.childCount}]");
        }
    }

    public static Transform[] AddToBoneList(string resource, string badBone = "", bool trimEnds = true) //just a helper function incase you have a resource that is just the bones
    {
        var bones = Resources.Load<GameObject>(resource);
        return AddToBoneList(bones.GetComponentsInChildren<Transform>(), badBone, trimEnds);
    }

    public static Transform[] AddToBoneList(Transform[] bones, string badBone = "", bool trimEnds = true)
    {
        List<Transform> t = new List<Transform>(boneList);
        foreach (var boner in bones)
        {
            if (boner.name.Contains("(Clone)"))
            {
                boner.name = boner.name.Remove(boner.name.Length - 7);
            }
            if (boner.name.Contains("_end") && trimEnds) //you probably want to leave this on, _end bones shouldn't be here
            {
                continue;
            }
            if (boner.name.Equals(badBone)) //incase you have multiple hierarchies you are importing, they might all be under a single transform which you don't want
            {
                continue;
            }
            t.Add(boner);
        }
        boneList = t.ToArray();
        return boneList; //As an fyi, when doing multiple calls of this function, aka multiple bone family additions, you might have to change the order you load them in to make it work in game
    }

    public static DynamicBone AppendAtoB(string a, string b, GameObject bodyPrefab = null, int root = 0) //yes the function is inefficient when importing more than 1 bone family, but it's more convienient this way and it's only on startup so idc
    {
        Transform bBone = boneList[0];
        foreach (var item in boneList)
        {
            if (item.name == b)
            {
                bBone = item;
                break;
            }
        }
        if (bBone == boneList[0] && boneList[0].name != b)
        {
            Debug.Log($"[{b}] not found");
            return null;
        }
        foreach (var item in boneList) //yes this is excessive because a is almost always after b, but this process is relatively cheap so I'm making it "foolproof"
        {
            if (item.name == a)
            {
                item.SetParent(bBone);
                item.transform.localEulerAngles = Vector3.zero;
                item.transform.localPosition = Vector3.zero;
                if (bodyPrefab) //You can add a dynamic bone here, but I personally recommend doing it in Unity instead
                {
                    var dynbone = bodyPrefab.AddComponent<DynamicBone>();
                    dynbone.m_Root = item.GetChild(root);
                    return dynbone;
                }
                return null;
            }
        }
        Debug.Log($"[{a}] not found");
        return null;
    }
}
