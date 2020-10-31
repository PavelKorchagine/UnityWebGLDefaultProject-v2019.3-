using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace MFramework_Unity.Tools
{
    public static class TranformEx
    {
        private static List<Transform> tempChildren = new List<Transform>();

        /// <summary>
        /// 静态方法
        /// </summary>
        /// <param name="target"></param>
        public static Transform FindInChr(this Transform tran, string target, bool unActi = true)
        {
            Transform forreturn = null;
            Transform[] trans = tran.GetComponentsInChildren<Transform>(unActi);

            for (int i = 0; i < trans.Length; i++)
            {
                if (trans[i].name == target)
                {
                    //Debug.Log("得到最终子物体的名字是：" + t.name);
                    forreturn = trans[i];
                    break;
                }
            }
            return forreturn;
        }
        /// <summary>
        /// 静态方法
        /// </summary>
        /// <param name="target"></param>
        public static T FindInChr<T>(this Transform tran, bool unActi = true) where T : Component
        {
            T forreturn = null;
            T[] trans = tran.GetComponentsInChildren<T>(unActi);
            if (trans != null && trans.Length > 0) forreturn = trans[0];
            return forreturn;
        }
        /// <summary>
        /// 静态方法
        /// </summary>
        /// <param name="target"></param>
        public static T FindInChr<T>(this Transform tran, string target, bool unActi = true) where T : Component
        {
            T forreturn = null;
            T[] trans = tran.GetComponentsInChildren<T>(unActi);

            for (int i = 0; i < trans.Length; i++)
            {
                if (trans[i].name == target)
                {
                    //Debug.Log("得到最终子物体的名字是：" + t.name);
                    forreturn = trans[i];
                    break;
                }
            }
            return forreturn;
        }
        /// <summary>
        /// 静态方法
        /// </summary>
        /// <param name="target"></param>
        public static Transform FindInChrUnActi(this Transform tran, string target)
        {
            Transform forreturn = null;
            Transform[] trans = tran.GetComponentsInChildren<Transform>(true);

            for (int i = 0; i < trans.Length; i++)
            {
                if (trans[i].name == target)
                {
                    //Debug.Log("得到最终子物体的名字是：" + t.name);
                    forreturn = trans[i];
                    break;
                }
            }
            return forreturn;
        }
        /// <summary>
        /// ToClone
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToClone(this string str)
        {
            return str + "(Clone)";
        }
        /// <summary>
        /// ClearChildren
        /// </summary>
        /// <param name="tra"></param>
        /// <returns></returns>
        public static Transform ClearChildren(this Transform tra)
        {
            // 方法一 有问题
            //int childCount = tra.childCount;
            //for (int i = 0; i < childCount; i++)
            //{
            //    Object.Destroy(tra.GetChild(0).gameObject);
            //    Debug.Log(10);
            //}

            // 方法二 通过
            //foreach (Transform item in tra)
            //{
            //    Object.Destroy(tra.GetChild(0).gameObject);
            //}
            // 方法三 通过
            for (int i = tra.childCount - 1; i >= 0; i--)
            {
                Object.Destroy(tra.GetChild(i).gameObject);
            }
            // 方法四 通过
            //List<Transform> te = tra.GetComponentsInChildren<Transform>(true).ToList();
            //te.Remove(tra);
            //for (int i = 0; i < te.Count; i++)
            //{
            //    Object.Destroy(te[i].gameObject);
            //}
            //te.Clear();

            return tra;
        }
        /// <summary>
        /// FindOrClone
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Transform FindOrClone(this Transform tran, string str)
        {
            Transform temp = tran.Find(str);
            if (temp == null) temp = tran.Find(str.ToClone());
            return temp;
        }

        /// <summary>
        /// FindOrCreate
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Transform FindOrCreate(this Transform tran, string str)
        {
            if (tran == null) return null;

            Transform temp = tran.Find(str);
            if (temp == null)
            {
                temp = new GameObject(str).transform;
                temp.transform.SetParent(tran);
                temp.transform.localPosition = temp.transform.localEulerAngles = Vector3.zero;
                temp.transform.localScale = Vector3.one;
            }
            return temp;
        }

        /// <summary>
        /// 静态方法 获取目标子物体（包括隐藏物体），返回第一个找到的物体
        /// </summary>
        /// <param name="target">目标物体的名字</param>
        public static Transform FindInChildrenExpend(this Transform tran, string target)
        {
            Transform forreturn = null;

            List<Transform> temp = GetChildren(tran);
            //Debug.Log("得到所有子物体 ：" + temp.Count);

            foreach (Transform t in temp)
            {
                //Debug.Log("得到最终子物体的名字是：" + t.name);
                if (t.name == target)
                {
                    //Debug.Log("得到最终子物体的名字是：" + t.name);
                    forreturn = t;
                    return t;
                }
            }
            return forreturn;
        }

        /// <summary>
        /// 静态方法 获取目标子物体（包括隐藏物体），返回第一个找到的物体的泛型型脚本
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tran"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static T FindInChildrenExpend<T>(this Transform tran, string target)
        {
            List<Transform> temp = GetChildren(tran);
            //Debug.Log("得到所有子物体 ：" + temp.Count);

            foreach (Transform t in temp)
            {
                //Debug.Log("得到最终子物体的名字是：" + t.name);
                if (t.name == target)
                {
                    //Debug.Log("得到最终子物体的名字是：" + t.name);
                    return t.GetComponent<T>();
                }
            }

            return default(T);
        }

        /// <summary>
        /// 静态方法 获取目标子物体（包括隐藏物体），返回所有的物体的泛型型脚本
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tran"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static List<T> FindInChildrenExpend<T>(this Transform tran)
        {
            List<Transform> temp = GetChildren(tran);

            List<T> tempT = new List<T>();

            foreach (Transform t in temp)
            {
                T tt = t.GetComponent<T>();
                if (tt != null)
                {
                    tempT.Add(tt);
                }
            }

            return tempT;
        }

        /// <summary>
        /// 静态方法 获取目标子物体（包括隐藏物体），返回第一个找到的物体
        /// </summary>
        /// <param name="target">目标物体的名字</param>
        public static Transform FindInChildrenWithTagExpend(this Transform tran, string target)
        {
            Transform forreturn = null;

            List<Transform> temp = GetChildren(tran);
            //Debug.Log("得到所有子物体 ：" + temp.Count);

            foreach (Transform t in temp)
            {
                //Debug.Log("得到最终子物体的名字是：" + t.name);
                if (t.tag == target)
                {
                    //Debug.Log("得到最终子物体的名字是：" + t.name);
                    forreturn = t;
                    return t;
                }
            }
            return forreturn;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tra"></param>
        /// <returns></returns>
        public static List<Transform> GetChildren(Transform tra)
        {
            if (tempChildren == null)
                tempChildren = new List<Transform>();
            else
                tempChildren.Clear();

            _GetChildren(tra);

            return tempChildren;
        }

        /// <summary>
        /// GetChildren
        /// </summary>
        /// <param name="tra"></param>
        /// <returns></returns>
        private static List<Transform> _GetChildren(Transform tra)
        {
            foreach (Transform item in tra)
            {
                tempChildren.Add(item);
                if (item.childCount > 0)
                {
                    _GetChildren(item);
                }
            }
            return tempChildren;
        }
        /// <summary>
        /// 静态方法
        /// </summary>
        /// <param name="target"></param>
        public static RectTransform FindInChildren(this RectTransform tran, string target)
        {
            RectTransform forreturn = null;
            foreach (RectTransform t in tran.GetComponentsInChildren<RectTransform>())
            {
                if (t.name == target)
                {
                    //Debug.Log("得到最终子物体的名字是：" + t.name);
                    forreturn = t;
                    return t;
                }
            }
            return forreturn;
        }
        /// <summary>
        /// 设置Tranform属性
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="config"></param>
        public static void SetTransformConfig(this Transform tran, TransformConfig config)
        {
            tran.SetParent(config.parent);

            tran.localPosition = config.localPosition;
            tran.localEulerAngles = config.localEulerAngles;
            tran.localScale = config.localScale;
        }
        /// <summary>
        /// 获取Tranform属性
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="config"></param>
        public static TransformConfig GetTransformConfig(this Transform tran)
        {
            TransformConfig config = new TransformConfig();
            config.parent = tran.parent;

            config.localPosition = tran.localPosition;
            config.localEulerAngles = tran.localEulerAngles;
            config.localScale = tran.localScale;

            return config;
        }
    }
    public static class GameObjectExpend
    {
        /// <summary>
        /// 静态方法 获取目标子物体（包括隐藏物体），返回第一个找到的物体
        /// </summary>
        /// <param name="target">目标物体的名字</param>
        public static GameObject FindInChildrenExpend(this GameObject obj, string target)
        {
            Transform tra = obj.transform.FindInChildrenExpend(target);
            if (tra == null)
                return null;
            else
                return tra.gameObject;
        }
        /// <summary>
        /// 静态方法 获取目标子物体（包括隐藏物体），返回第一个找到的物体
        /// </summary>
        /// <param name="target">目标物体的标签</param>
        public static GameObject FindInChildrenWithTagExpend(this GameObject obj, string tag)
        {
            Transform tra = obj.transform.FindInChildrenWithTagExpend(tag);
            if (tra == null)
                return null;
            else
                return tra.gameObject;
        }
    }


    /// <summary>
    /// TransformConfig Transform 配置信息结构体
    /// </summary>
    [Serializable]
    public struct TransformConfig
    {
        public Transform parent;
        public Vector3 localPosition;
        public Vector3 localEulerAngles;
        public Vector3 localScale;

        /// <summary>
        /// Transform 配置信息结构体 构造函数
        /// </summary>
        /// <param name="tran"></param>
        public TransformConfig(Transform tran = null)
        {
            if (tran != null)
            {
                parent = tran.parent;
                localPosition = tran.localPosition;
                localEulerAngles = tran.localEulerAngles;
                localScale = tran.localScale;
            }
            else
            {
                parent = null;
                localPosition = Vector3.zero;
                localEulerAngles = Vector3.zero;
                localScale = Vector3.one;
            }

        }

        /// <summary>
        /// Transform 配置信息结构体 构造函数
        /// </summary>
        /// <param name="localpos"></param>
        /// <param name="localeular"></param>
        /// <param name="localscale"></param>
        /// <param name="parent"></param>
        public TransformConfig(Vector3 localpos, Vector3 localeular, Vector3 localscale, Transform parent = null)
        {
            this.parent = parent;
            this.localPosition = localpos;
            this.localEulerAngles = localeular;
            this.localScale = localscale;
        }
    }
}