using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace MFramework_Unity
{
    public static class UnityEx
    {
        /// <summary>
        /// GetComponentReal
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="go"></param>
        /// <returns></returns>
        public static T GetComponentReal<T>(this GameObject go) where T : Component
        {
            T t = go.GetComponent<T>();
            if (t == null)
                t = go.AddComponent<T>();
            return t;
        }
        /// <summary>
        /// GetComponentReal
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tr"></param>
        /// <returns></returns>
        public static T GetComponentReal<T>(this Transform tr) where T : Component
        {
            T t = tr.GetComponent<T>();
            if (t == null)
                t = tr.gameObject.AddComponent<T>();
            return t;
        }
        /// <summary>
        /// GetComponentReal
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="com"></param>
        /// <returns></returns>
        public static T GetComponentReal<T>(this Component com) where T : Component
        {
            T t = com.GetComponent<T>();

            if (t == null)
                t = com.gameObject.AddComponent<T>();
            return t;
        }

        /// <summary>
        /// FindObjectsOfTypeExpend
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T[] FindObjsOfTyEx<T>() where T : MonoBehaviour
        {
            T[] ts = Resources.FindObjectsOfTypeAll<T>();
            List<T> temp = new List<T>();

            //这里需要注意过滤掉预制体
            for (int i = 0; i < ts.Length; i++)
            {
                T item = ts[i];
                if (item.gameObject.scene.name != null)
                    temp.Add(item);
                else { }
                //Debug.Log("这是一个预设体！");
            }
            return temp.ToArray();
        }
        /// <summary>
        /// FindObjectsOfBehaviourExpend
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T[] FindObjsOfBehavEx<T>() where T : Behaviour
        {
            T[] ts = Resources.FindObjectsOfTypeAll<T>();
            List<T> temp = new List<T>();

            //这里需要注意过滤掉预制体
            for (int i = 0; i < ts.Length; i++)
            {
                T item = ts[i];
                if (item.gameObject.scene.name != null)
                    temp.Add(item);
                else { }
                //Debug.Log("这是一个预设体！");
            }

            return temp.ToArray();
        }
        /// <summary>
        /// FindObjectsOfTypeExpend
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T[] FindObjsOfTyExInScene<T>() where T : MonoBehaviour
        {
            T[] ts = Resources.FindObjectsOfTypeAll<T>();
            List<T> temp = new List<T>();

            //这里需要注意过滤掉预制体
            for (int i = 0; i < ts.Length; i++)
            {
                T item = ts[i];
                if (item.gameObject.scene.name != null && (item.gameObject.scene.name == SceneManager.GetActiveScene().name || item.gameObject.scene.name == "DontDestroyOnLoad"))
                    temp.Add(item);
                else { }
                //Debug.Log("这是一个预设体！");
            }

            return temp.ToArray();
        }
        /// <summary>
        /// FindAllObjectsOfTypeInScene 寻找当前场景中所有的 T 的物体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T[] FindAllObjsOfTyExInScene<T>() where T : Component
        {
            T[] ts = Resources.FindObjectsOfTypeAll<T>();
            List<T> temp = new List<T>();

            //这里需要注意过滤掉预制体
            for (int i = 0; i < ts.Length; i++)
            {
                T item = ts[i];
                if (item.gameObject.scene.name != null && (item.gameObject.scene.name == SceneManager.GetActiveScene().name || item.gameObject.scene.name == "DontDestroyOnLoad"))
                    temp.Add(item);
                else { }
                //Debug.Log("这是一个预设体！");
            }

            return temp.ToArray();
        }
        /// <summary>
        /// FindObjectsOfTypeExpend
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T[] FindObjsOfTyExInSceneUnDestr<T>() where T : MonoBehaviour
        {
            T[] ts = Resources.FindObjectsOfTypeAll<T>();
            List<T> temp = new List<T>();

            //这里需要注意过滤掉预制体
            for (int i = 0; i < ts.Length; i++)
            {
                T item = ts[i];
                if (item.gameObject.scene.name != null && (item.gameObject.scene.name == SceneManager.GetActiveScene().name))
                    temp.Add(item);
                else { }
                //Debug.Log("这是一个预设体！");
            }

            return temp.ToArray();
        }
        /// <summary>
        /// FindObjectsOfTypeExpend
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T[] FindObjsOfTyExInSceneDestr<T>() where T : MonoBehaviour
        {
            T[] ts = Resources.FindObjectsOfTypeAll<T>();
            List<T> temp = new List<T>();

            //这里需要注意过滤掉预制体
            for (int i = 0; i < ts.Length; i++)
            {
                T item = ts[i];
                if (item.gameObject.scene.name != null && (item.gameObject.scene.name == "DontDestroyOnLoad"))
                    temp.Add(item);
                else { }
                //Debug.Log("这是一个预设体！");
            }

            return temp.ToArray();
        }
        /// <summary>
        /// FindObjectsOfTypeExpend
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T[] FindObjsOfTyEx<T>(this Object obj, bool justChildren) where T : MonoBehaviour
        {
            T[] ts = Resources.FindObjectsOfTypeAll<T>();
            List<T> temp = new List<T>();

            //这里需要注意过滤掉预制体
            for (int i = 0; i < ts.Length; i++)
            {
                T item = ts[i];
                if (item.gameObject.scene.name != null)
                    temp.Add(item);
                else { }
                //Debug.Log("这是一个预设体！");
            }

            return temp.ToArray();
        }
        /// <summary>
        /// FindObjectsOfTypeExpend
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="justChildren"></param>
        /// <param name="root"></param>
        /// <returns></returns>
        public static T[] FindObjsOfTyEx<T>(bool justChildren, Transform root) where T : MonoBehaviour
        {
            T[] ts = Resources.FindObjectsOfTypeAll<T>();
            List<T> temp = new List<T>();

            //这里需要注意过滤掉预制体
            for (int i = 0; i < ts.Length; i++)
            {
                T item = ts[i];
                if (item.gameObject.scene.name != null) continue;
                if (justChildren && item.transform.root == root)
                    temp.Add(item);
                else if (!justChildren)
                    temp.Add(item);
                //Debug.Log("这是一个预设体！");
            }

            return temp.ToArray();
        }
        /// <summary>
        /// FindObjectOfTypeExpend
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T FindObjOfTyEx<T>() where T : MonoBehaviour
        {
            T[] ts = Resources.FindObjectsOfTypeAll<T>();

            //这里需要注意过滤掉预制体
            for (int i = 0; i < ts.Length; i++)
            {
                T item = ts[i];
                if (item.gameObject.scene.name != null)
                    return item;
                else { }
                //Debug.Log("这是一个预设体！");
            }
            return default(T);
        }
        /// <summary>
        /// FindObjectOfTypeExpend in root
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T FindObjOfTyEx<T>(Transform root) where T : Behaviour
        {
            T[] ts = Resources.FindObjectsOfTypeAll<T>();

            //这里需要注意过滤掉预制体
            for (int i = 0; i < ts.Length; i++)
            {
                T item = ts[i];
                if (item.gameObject.scene.name != null && item.transform.parent == root)
                    return item;
                else { }
                //Debug.Log("这是一个预设体！");
            }
            return default(T);
        }
        /// <summary>
        /// FindPrefabObjectOfTypeExpend 返回相关类型的预制体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T FindPrefObjOfTyEx<T>() where T : MonoBehaviour
        {
            T[] ts = Resources.FindObjectsOfTypeAll<T>();

            //这里需要注意过滤掉预制体
            for (int i = 0; i < ts.Length; i++)
            {
                T item = ts[i];
                if (item.gameObject.scene.name == null)
                    return item;
                else { }
                //Debug.Log("这是一个预设体！");
            }

            return default(T);
        }
        /// <summary>
        /// FindPrefabObjectOfTypeExpend 返回相关类型的预制体数组
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T[] FindPrefObjsOfTyEx<T>() where T : MonoBehaviour
        {
            T[] ts = Resources.FindObjectsOfTypeAll<T>();
            List<T> temp = new List<T>();

            //这里需要注意过滤掉预制体
            for (int i = 0; i < ts.Length; i++)
            {
                T item = ts[i];
                if (item.gameObject.scene.name != null)
                    temp.Add(item);
                else { }
                //Debug.Log("这是一个预设体！");
            }

            return temp.ToArray();
        }
        /// <summary>
        /// FindPrefabObjectOfTypeExpend
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T FindPrefObjOfBehavEx<T>() where T : Behaviour
        {
            T[] ts = Resources.FindObjectsOfTypeAll<T>();
            List<T> temp = new List<T>();

            //这里需要注意过滤掉预制体
            for (int i = 0; i < ts.Length; i++)
            {
                T item = ts[i];
                if (item.gameObject.scene.name != null)
                    temp.Add(item);
                else { }
                //Debug.Log("这是一个预设体！");
            }

            return temp.ToArray()[0];
        }

        /// <summary>
        /// 实例化对象
        /// </summary>
        /// <param name="original"></param>
        /// <param name="position"></param>
        /// <param name="rotation"></param>
        /// <param name="parent"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        public static Object Instantiate(Object original, Vector3 position, Quaternion rotation, Transform parent, System.Action<bool> callback)
        {
            Object retVal = Object.Instantiate(original, position, rotation, parent);

            if (callback == null) return retVal;
            if (retVal != null) callback(true);
            else callback(false);
            return retVal;
        }
        /// <summary>
        /// 实例化对象
        /// </summary>
        /// <param name="original"></param>
        /// <param name="parent"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        public static Object Instantiate(Object original, Transform parent, System.Action<bool> callback)
        {
            Object retVal = Object.Instantiate(original, parent);

            if (callback == null) return retVal;
            if (retVal != null) callback(true);
            else callback(false);
            return retVal;
        }
        /// <summary>
        /// 实例化对象
        /// </summary>
        /// <param name="original"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        public static Object Instantiate(Object original, System.Action<bool> callback)
        {
            Object retVal = Object.Instantiate(original);

            if (callback == null) return retVal;
            if (retVal != null) callback(true);
            else callback(false);
            return retVal;
        }
        /// <summary>
        /// 实例化对象
        /// </summary>
        /// <param name="original"></param>
        /// <param name="position"></param>
        /// <param name="rotation"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        public static Object Instantiate(Object original, Vector3 position, Quaternion rotation, System.Action<bool> callback)
        {
            Object retVal = Object.Instantiate(original, position, rotation);

            if (callback == null) return retVal;
            if (retVal != null) callback(true);
            else callback(false);
            return retVal;
        }
        /// <summary>
        /// 实例化对象
        /// </summary>
        /// <param name="original"></param>
        /// <param name="parent"></param>
        /// <param name="instantiateInWorldSpace"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        public static Object Instantiate(Object original, Transform parent, bool instantiateInWorldSpace, System.Action<bool> callback)
        {
            Object retVal = Object.Instantiate(original, parent, instantiateInWorldSpace);

            if (callback == null) return retVal;
            if (retVal != null) callback(true);
            else callback(false);
            return retVal;
        }

        /// <summary>
        /// FindObjectsOfTypeExpend
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T[] FindObjectsOfTypeExpend<T>() where T : MonoBehaviour
        {
            T[] ts = Resources.FindObjectsOfTypeAll<T>();

            List<T> temp = new List<T>();

            //这里需要注意过滤掉预制体
            foreach (T item in ts)
            {
                if (item.gameObject.scene.name != null)
                {
                    temp.Add(item);
                }
                else
                {
                    //Debug.Log("这是一个预设体！");
                }
            }

            return temp.ToArray();
        }
        /// <summary>
        /// FindObjectsOfBehaviourExpend
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T[] FindObjectsOfBehaviourExpend<T>() where T : Behaviour
        {
            T[] ts = Resources.FindObjectsOfTypeAll<T>();

            List<T> temp = new List<T>();

            //这里需要注意过滤掉预制体
            foreach (T item in ts)
            {
                if (item.gameObject.scene.name != null)
                {
                    temp.Add(item);
                }
                else
                {
                    //Debug.Log("这是一个预设体！");
                }
            }

            return temp.ToArray();
        }
        /// <summary>
        /// FindObjectsOfTypeExpend
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T[] FindObjectsOfTypeExpendInScene<T>() where T : MonoBehaviour
        {
            T[] ts = Resources.FindObjectsOfTypeAll<T>();

            List<T> temp = new List<T>();

            //这里需要注意过滤掉预制体
            foreach (T item in ts)
            {
                if (item.gameObject.scene.name != null)
                {
                    if (item.gameObject.scene.name == SceneManager.GetActiveScene().name)
                    {
                        temp.Add(item);
                    }

                }
                else
                {
                    //Debug.Log("这是一个预设体！");
                }
            }

            return temp.ToArray();
        }
        /// <summary>
        /// FindObjectsOfTypeExpend
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T[] FindObjectsOfTypeExpend<T>(this Object obj, bool justChildren) where T : MonoBehaviour
        {
            T[] ts = Resources.FindObjectsOfTypeAll<T>();

            List<T> temp = new List<T>();

            //这里需要注意过滤掉预制体
            foreach (T item in ts)
            {
                if (item.gameObject.scene.name != null)
                {
                    temp.Add(item);
                }
                else
                {
                    //Debug.Log("这是一个预设体！");
                }
            }

            return temp.ToArray();
        }
        /// <summary>
        /// FindObjectsOfTypeExpend
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="justChildren"></param>
        /// <param name="root"></param>
        /// <returns></returns>
        public static T[] FindObjectsOfTypeExpend<T>(bool justChildren, Transform root) where T : MonoBehaviour
        {
            T[] ts = Resources.FindObjectsOfTypeAll<T>();

            List<T> temp = new List<T>();

            //这里需要注意过滤掉预制体
            foreach (T item in ts)
            {
                if (justChildren)
                {
                    if (item.gameObject.scene.name != null && item.transform.root == root)
                    {
                        temp.Add(item);
                    }
                }
                else
                {
                    if (item.gameObject.scene.name != null)
                    {
                        temp.Add(item);
                    }
                }
            }

            return temp.ToArray();
        }
        /// <summary>
        /// FindObjectOfTypeExpend
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T FindObjectOfTypeExpend<T>() where T : MonoBehaviour
        {
            T[] ts = Resources.FindObjectsOfTypeAll<T>();

            List<T> temp = new List<T>();

            //这里需要注意过滤掉预制体
            foreach (T item in ts)
            {
                if (item.gameObject.scene.name != null)
                {
                    temp.Add(item);
                }
                else
                {
                    //Debug.Log("这是一个预设体！");
                }
            }

            return temp.ToArray()[0];
        }
        /// <summary>
        /// FindPrefabObjectOfTypeExpend
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T FindPrefabObjectOfTypeExpend<T>() where T : MonoBehaviour
        {
            T[] ts = Resources.FindObjectsOfTypeAll<T>();

            List<T> temp = new List<T>();

            //这里需要注意过滤掉预制体
            foreach (T item in ts)
            {
                if (item.gameObject.scene.name == null)
                {
                    temp.Add(item);
                }
                else
                {
                    //Debug.Log("这不是一个预设体！");
                }
            }

            return temp.ToArray()[0];
        }
        /// <summary>
        /// FindPrefabObjectOfTypeExpend
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T FindPrefabObjectOfBehaviourExpend<T>() where T : Behaviour
        {
            T[] ts = Resources.FindObjectsOfTypeAll<T>();

            List<T> temp = new List<T>();

            //这里需要注意过滤掉预制体
            foreach (T item in ts)
            {
                if (item.gameObject.scene.name == null)
                {
                    temp.Add(item);
                }
                else
                {
                    //Debug.Log("这不是一个预设体！");
                }
            }

            return temp.ToArray()[0];
        }

        /// <summary>
        /// ViewportPointToAnchoredPosition
        /// </summary>
        /// <param name="ori"></param>
        /// <param name="widthhight"></param>
        /// <returns></returns>
        public static Vector2 ViewportPointToAnchoredPosition(Vector2 ori, Vector2 widthhight)
        {
            return new Vector2((ori.x - 0.5f) * widthhight.x, (ori.y - 0.5f) * widthhight.y);
        }

        /// <summary>
        /// 生成缩略图 指定目标的长宽
        /// </summary>
        /// <param name="source"></param>
        /// <param name="targetWidth"></param>
        /// <param name="targetHeight"></param>
        /// <param name="savePath"></param>
        /// <returns></returns>
        public static Texture2D ScaleTexture(Texture2D source, int targetWidth, int targetHeight, string savePath = "")
        {
            Texture2D result = new Texture2D(targetWidth, targetHeight, TextureFormat.ARGB32, false);

            for (int i = 0; i < result.height; ++i)
            {
                for (int j = 0; j < result.width; ++j)
                {
                    Color newColor = source.GetPixelBilinear((float)j / (float)result.width, (float)i / (float)result.height);
                    result.SetPixel(j, i, newColor);
                }
            }
            result.Apply();
            return result;
            //File.WriteAllBytes(savePath, result.EncodeToJPG());
        }
        /// <summary>
        /// 生成缩略图 指定目标的缩放的百分比
        /// </summary>
        /// <param name="source"></param>
        /// <param name="percent"></param>
        /// <param name="savePath"></param>
        /// <returns></returns>
        public static Texture2D ScaleTexture(Texture2D source, double percent, string savePath = "")
        {
            Texture2D result = new Texture2D(int.Parse(Math.Round(source.width * percent).ToString()), int.Parse(Math.Round(source.height * percent).ToString()), TextureFormat.ARGB32, false);
            for (int i = 0; i < result.height; ++i)
            {
                for (int j = 0; j < result.width; ++j)
                {
                    Color newColor = source.GetPixelBilinear((float)j / (float)result.width, (float)i / (float)result.height);
                    result.SetPixel(j, i, newColor);
                }
            }
            result.Apply();
            return result;
            //File.WriteAllBytes(savePath, result.EncodeToJPG());
        }

    }
}