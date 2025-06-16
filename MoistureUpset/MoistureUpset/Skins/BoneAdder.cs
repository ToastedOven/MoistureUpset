using MoistureUpset;
using System.Collections.Generic;
using UnityEngine;

public static class BoneAdder //if you manage to find this class, feel free to use it for adding bones to models, I haven't released this cause I don't want to step on other people's toes. I just feel this works better
{
    public static Transform[] boneList;

    public static List<DynamicBone> dynbones = new List<DynamicBone>();

    public static List<DynamicBoneCollider> colliders = new List<DynamicBoneCollider>();

    public static void DebugDefaultBones()
    {
        foreach (var item in boneList)
        {
            Debug.Log($"bone: [{item}] parent: [{item.parent}] childcount: [{item.childCount}]");
        }
    }

    public static Transform[] AddToBoneList(string resource, string badBone = "", bool trimEnds = true) //just a helper function incase you have a resource that is just the bones
    {
        var bones = MoistureUpset.Assets.Load<GameObject>(resource);
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
                    dynbones.Add(dynbone);
                    return dynbone;
                }
                return null;
            }
        }
        Debug.Log($"[{a}] not found");
        return null;
    }



    public static DynamicBoneCollider AttachCollider(Transform t, float radius, float height, DynamicBoneCollider.Direction direction, DynamicBoneCollider.Bound bound) //default values for Vector3 seem to not work, so I just seperate it into 2 functions
    {
        return AttachCollider(t.gameObject, radius, height, direction, bound, Vector3.zero); //default to Vector3.zero if unstated. This is most likely what you want so I let you type less
    }
    public static DynamicBoneCollider AttachCollider(Transform t, float radius, float height, DynamicBoneCollider.Direction direction, DynamicBoneCollider.Bound bound, Vector3 center)
    {
        return AttachCollider(t.gameObject, radius, height, direction, bound, center);
    }
    public static DynamicBoneCollider AttachCollider(GameObject g, float radius, float height, DynamicBoneCollider.Direction direction, DynamicBoneCollider.Bound bound)
    {
        return AttachCollider(g, radius, height, direction, bound, Vector3.zero);
    }
    public static DynamicBoneCollider AttachCollider(GameObject g, float radius, float height, DynamicBoneCollider.Direction direction, DynamicBoneCollider.Bound bound, Vector3 center)
    {
        var collider = g.AddComponent<DynamicBoneCollider>();
        collider.m_Radius = radius;
        collider.m_Height = height;
        collider.m_Direction = direction;
        collider.m_Bound = bound;
        collider.m_Center = center;
        colliders.Add(collider);
        return collider;
    }
    public static DynamicBoneCollider AttachCollider(Transform target, GameObject source, int pos = 0)
    {
        return AttachCollider(target.gameObject, source, pos);
    }
    public static DynamicBoneCollider AttachCollider(GameObject target, GameObject source, int pos = 0)
    {
        var collider = target.AddComponent<DynamicBoneCollider>();
        var ogCollider = source.GetComponentsInChildren<DynamicBoneCollider>()[pos];
        collider.m_Radius = ogCollider.m_Radius;
        collider.m_Height = ogCollider.m_Height;
        collider.m_Direction = ogCollider.m_Direction;
        collider.m_Bound = ogCollider.m_Bound;
        collider.m_Center = ogCollider.m_Center;
        colliders.Add(collider);
        return collider;
    }

    public static List<DynamicBone> FinalizeColliders() //run me after creating all of your dynbones and colliders
    {
        foreach (var dynbone in dynbones)
        {
            foreach (var collider in colliders)
            {
                dynbone.m_Colliders.Add(collider);
            }
        }
        return dynbones;
    }

    public static bool ClearLists()
    {
        if (ClearColliders() == 0 && ClearDynbones() == 0)
        {
            return true;
        }
        return false;
    }
    public static int ClearDynbones()
    {
        dynbones.Clear();
        return dynbones.Count;
    }
    public static int ClearColliders()
    {
        colliders.Clear();
        return colliders.Count;
    }
}

public class UseAllColliders : MonoBehaviour //add me to the root gameobject if you want to work with all colliders in the scene
{
    float timer = 1;
    bool run = false;
    void Update()
    {
        if (!run)
        {
            timer -= Time.deltaTime;
            if (timer < 0) //waiting a second before grabbing all colliders cause idk how exactly Hopoo loads the scene and I don't want to miss anything
            {
                run = true;
                var dynbones = GetComponentsInChildren<DynamicBone>();
                DynamicBoneCollider[] colliders = GameObject.FindObjectsOfType<DynamicBoneCollider>();
                foreach (var dynbone in dynbones)
                {
                    dynbone.m_Colliders.Clear();
                    foreach (var collider in colliders)
                    {
                        dynbone.m_Colliders.Add(collider);
                    }
                }
            }
        }
    }
}
