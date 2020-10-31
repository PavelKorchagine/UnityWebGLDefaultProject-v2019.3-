using System;
using UnityEngine;
#region using UnityEditor;
#if UNITY_EDITOR
using UnityEditor;
#endif
#endregion

namespace MFramework_Unity
{
    public abstract class BaseMonoAbstract : MonoBehaviour
    {
        protected MonoDriver driver;

        protected virtual void Awake()
        {
            if (transform.root == null)
                driver = transform.GetComponentReal<MonoDriver>();
            else
                driver = transform.root.GetComponentReal<MonoDriver>();
            OnAwake();
        }
        protected virtual void OnDestroy()
        {
            driver = null;
            OnGODestroy();
        }
        protected virtual void OnEnable()
        {
            driver.Register(this);
        }
        protected virtual void OnDisable()
        {
            driver.Remove(this);
        }
        protected virtual void Start()
        {
            OnStart();
        }
        internal void OnUpdateAbstract()
        {
            OnUpdate();
        }
        internal void OnFixUpdateAbstract()
        {
            OnFixUpdate();
        }
        internal void OnLateUpdateAbstract()
        {
            OnLateUpdate();
        }

        public abstract void OnInit();
        public abstract void OnInit(params object[] args);
        public abstract void OnGet();
        public abstract void OnDie();
        public abstract void OnReset();
        protected abstract void OnAwake();
        protected abstract void OnStart();
        protected abstract void OnUpdate();
        protected abstract void OnFixUpdate();
        protected abstract void OnLateUpdate();
        protected abstract void OnGODestroy();

        #region UNITY_EDITOR
#if UNITY_EDITOR
        [CustomEditor(typeof(BaseMonoAbstract))]
        public class BaseMonoAbstractEditor : Editor
        {
            public override void OnInspectorGUI()
            {
                base.OnInspectorGUI();
                BaseMonoAbstract manager = target as BaseMonoAbstract;

                if (manager == null)
                {
                    return;
                }
                if (!EditorApplication.isPlaying)
                {
                    return;
                }
                if (GUILayout.Button("Do Init"))
                {
                    manager.OnInit();

                }
                this.Repaint();
            }
        }
#endif
        #endregion
    }
}