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
using System.IO;
using UnityEngine.SceneManagement;
using System.Text;
using BepInEx.Logging;

namespace MoistureUpset
{
    public static class DebugClass
    {
        private static ManualLogSource Logger;

        public static void SetLogger(ManualLogSource logSource)
        {
            Logger = logSource;
        }
        public static void Log(object message)
        {
            Logger.Log(LogLevel.Info, $"{message}");
        }
        public static void GetHeirarchy(string name)
        {
            GameObject g = GameObject.Find(name);
            try
            {
                while (g.transform.parent.gameObject)
                {
                    g = g.transform.parent.gameObject;
                }
            }
            catch (Exception)
            {
            }
            Log("nutting");
            Heirarchy(g, "");
        }
        public static void Heirarchy(GameObject g, string depth)
        {
            depth += "--"; 
            Log($"{depth}{g.name}");
            foreach (Transform item in g.transform)
            {
                Heirarchy(item.gameObject, depth);
            }
        }
        public static bool GetParent(string name)
        {
            GameObject g = GameObject.Find(name);
            try
            {
                Log($"-------------{g.transform.parent.gameObject}");
                return true;
            }
            catch (Exception)
            {
                Log($"-------------no parent");
                return false;
            }
        }
        public static void UIdebug()
        {
            UnityEngine.UI.Image[] objects = GameObject.FindObjectsOfType<UnityEngine.UI.Image>();
            Log($"--------------uicomponents----------------");
            foreach (var item in objects)
            {
                Log(item);
            }
            Log($"------------------------------------------");
        }
        public static void UIdebugReplace()
        {
            UnityEngine.UI.Image[] objects = GameObject.FindObjectsOfType<UnityEngine.UI.Image>();
            byte[] bytes = ByteReader.readbytes("MoistureUpset.Resources.roblox.png");
            Texture2D tex = new Texture2D(512, 512);
            tex.LoadImage(bytes);
            for (int i = 0; i < objects.Length; i++)
            {
                objects[i].sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(.5f, .5f), 100);
            }
        }
        public static void TextureDebugReplace()
        {
            Texture t = Resources.Load<Texture>("@MoistureUpset_noob:assets/Noob1TexLaser.png");
            SkinnedMeshRenderer[] objects = GameObject.FindObjectsOfType<SkinnedMeshRenderer>();
            for (int i = 0; i < objects.Length; i++)
            {
                Log($"------{objects[i].name}");
                if (objects[i].name == "golem")
                {
                    foreach (var item in objects[i].sharedMaterials)
                    {
                        item.mainTexture = t;
                        item.SetTexture("_EmTex", t);
                    }
                }
                //objects[i] = t;
            }
            Log($"------end of list------");
        }
        public static void ListComponents(string name)
        {
            GameObject g = GameObject.Find(name);
            if (!g)
            {
                Log($"----------------components----------------");
                Log($"GameObject not found");
                Log($"------------------------------------------");
                return;
            }
            Log($"----------------components----------------");
            foreach (var item in g.GetComponents<Component>())
            {
                Log(item);
            }
            Log($"------------------------------------------");
        }
        public static void ListComponents(GameObject g)
        {
            if (!g)
            {
                Log($"----------------components----------------");
                Log($"GameObject not found");
                Log($"------------------------------------------");
                return;
            }
            Log($"----------------components----------------");
            foreach (var item in g.GetComponentsInChildren<Component>())
            {
                Log(item);
            }
            Log($"------------------------------------------");
        }
        public static void DebugBones(string resource)
        {
            var fab = Resources.Load<GameObject>(resource);
            var meshes = fab.GetComponentsInChildren<SkinnedMeshRenderer>();
            StringBuilder sb = new StringBuilder();
            sb.Append($"rendererererer: {meshes[0]}\n");
            sb.Append($"bone count: {meshes[0].bones.Length}\n");
            sb.Append($"mesh count: {meshes.Length}\n");
            sb.Append($"root bone: {meshes[0].rootBone.name}\n");
            sb.Append($"{resource}:\n");
            if (meshes[0].bones.Length == 0)
            {
                sb.Append("No bones");
            }
            else
            {
                sb.Append("[");
                foreach (var bone in meshes[0].bones)
                {
                    sb.Append($"'{bone.name}', ");
                }
                sb.Remove(sb.Length - 2, 2);
                sb.Append("]");
            }
            sb.Append("\n\n");
            Log(sb.ToString());
        }
        public static void DebugBones(GameObject fab)
        {
            var meshes = fab.GetComponentsInChildren<SkinnedMeshRenderer>();
            StringBuilder sb = new StringBuilder();
            sb.Append($"mesh count = {meshes.Length}\n");
            sb.Append($"{fab}:\n");
            sb.Append("[");
            foreach (var bone in meshes[0].bones)
            {
                sb.Append($"'{bone.name}', ");
            }
            sb.Remove(sb.Length - 2, 2);
            sb.Append("]");
            Log(sb.ToString());
        }
        public static void GetAllGameObjects()
        {
            GameObject[] objects = GameObject.FindObjectsOfType<GameObject>();
            foreach (var item in objects)
            {
                ////Log($"-------sex----{item.name}");
                //if (item.name == "Mesh")
                //{
                //    try
                //    {
                //        DebugBones(item);
                //    }
                //    catch (Exception)
                //    {
                //    }
                //}
                Log($"-------------{item}");
            }
        }

        public static void GetAllTransforms()
        {
            Transform[] objects = GameObject.FindObjectsOfType<Transform>();
            foreach (var item in objects)
            {
                ////Log($"-------sex----{item.name}");
                //if (item.name == "Mesh")
                //{
                //    try
                //    {
                //        DebugBones(item);
                //    }
                //    catch (Exception)
                //    {
                //    }
                //}
                Log($"---{item}------parent:----{item.parent}");
            }
        }
        public static void GetAllGameObjects(GameObject g)
        {
            foreach (var item in g.GetComponents<GameObject>())
            {
                Log($"-------sex----{item}");
            }
        }
    }
}
