using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#region using UnityEditor;
#if UNITY_EDITOR
using UnityEditor;
#endif
#endregion

namespace MFramework_Unity
{
    public class MonoDriver : MonoBehaviour
    {
        #region Awake
        ////protected virtual void Awake()
        ////{
        ////}
        ////protected virtual void OnDestroy()
        ////{
        ////}
        //protected virtual void OnEnable()
        //{
        //    //driver.Register(this);
        //}
        //protected virtual void OnDisable()
        //{
        //    //driver.Remove(this);
        //} 
        #endregion

        private List<BaseMonoAbstract> baseMonos = new List<BaseMonoAbstract>();

        public List<BaseMonoAbstract> CloneBaseMonoColl()
        {
            List<BaseMonoAbstract> temp = new List<BaseMonoAbstract>();
            temp.AddRange(baseMonos);
            return temp;
        }
        public bool Register(BaseMonoAbstract baseMono)
        {
            if (!baseMonos.Contains(baseMono))
            {
                baseMonos.Add(baseMono);
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool Remove(BaseMonoAbstract baseMono)
        {
            if (baseMonos.Contains(baseMono))
            {
                baseMonos.Remove(baseMono);
                return true;
            }
            else
            {
                return false;
            }
        }
        public void Clear()
        {
            baseMonos.Clear();
        }
        public BaseMonoAbstract GetLastItem()
        {
            return baseMonos[baseMonos.Count - 2];
        }

        public void Update()
        {
            int count = baseMonos.Count;
            for (int i = 0; i < count; i++)
            {
                if (i > baseMonos.Count - 1) continue;
                if (baseMonos[i] == null) baseMonos.Remove(baseMonos[i]);
                else baseMonos[i].OnUpdateAbstract();
            }
        }
        public void FixedUpdate()
        {
            int count = baseMonos.Count;
            for (int i = 0; i < count; i++)
            {
                if (i > baseMonos.Count - 1) continue;
                if (baseMonos[i] == null) baseMonos.Remove(baseMonos[i]);
                else baseMonos[i].OnFixUpdateAbstract();
            }
        }
        public void LateUpdate()
        {
            int count = baseMonos.Count;
            for (int i = 0; i < count; i++)
            {
                if (i > baseMonos.Count - 1) continue;
                if (baseMonos[i] == null) baseMonos.Remove(baseMonos[i]);
                else baseMonos[i].OnLateUpdateAbstract();
            }
        }
        #region UNITY_EDITOR
#if UNITY_EDITOR
        [CustomEditor(typeof(MonoDriver))]
        public class MonoDriverEditor : Editor
        {
            public override void OnInspectorGUI()
            {
                base.OnInspectorGUI();
                MonoDriver manager = target as MonoDriver;

                if (manager == null)
                {
                    return;
                }
                if (!EditorApplication.isPlaying)
                {
                    return;
                }
                EditorGUILayout.LabelField("注册的 BaseMono：" + manager.baseMonos.Count.ToString());
                foreach (var item in manager.baseMonos)
                {
                    EditorGUILayout.LabelField("(" + manager.baseMonos.IndexOf(item).ToString() + ") " + item);
                }
                this.Repaint();
            }
        }
#endif
        #endregion
    }
}