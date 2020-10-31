using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
#region using UnityEditor;
#if UNITY_EDITOR
using UnityEditor;
#endif
#endregion
using System;

namespace MFramework_Unity.Tools
{
    public class DOTweenTimerManager : MonoBehaviour
    {
        #region Instance

        private static bool initialized;

        private static DOTweenTimerManager instance;
        public static DOTweenTimerManager Instance
        {
            get
            {
                Initialize();
                return instance;
            }
        }
        private static void Initialize()
        {
            if (!initialized)
            {
                if (!Application.isPlaying)
                    return;
                initialized = true;
                GameObject go = new GameObject("DOTweenTimerManager");
                instance = go.AddComponent<DOTweenTimerManager>();
            }
        }
        private void Awake()
        {
            instance = this;
            if (gameObject.scene.name != "DontDestroyOnLoad")
            {
                DontDestroyOnLoad(this.gameObject);
            }
            initialized = true;
        }
        #endregion

        public void SayHello()
        {
            Debug.LogWarningFormat("[{0}] say: Hello，World !! {1}", this, System.DateTime.Now);
        }
        private bool isDefaultInited;
        private Queue<Transform> delayTrans = new Queue<Transform>();
        private Dictionary<TweenCallback, Transform> delayActionDict = new Dictionary<TweenCallback, Transform>();

        private void AddDelayCallback(TweenCallback callback, float delayDur, bool isFocusAdd = false)
        {
            if (!isDefaultInited)
            {
                DefaultInit();
                isDefaultInited = true;
            }
            if (isFocusAdd)
            {
                RemoveDelayCallback(callback);
            }
            if (delayTrans.Count <= 0)
            {
                delayTrans.Enqueue(GetTran());
            }
            if (delayActionDict.ContainsKey(callback)) return;

            delayActionDict.Add(callback, delayTrans.Dequeue());
            delayActionDict[callback].DOLocalMove(Vector3.zero, delayDur).OnComplete(() =>
            {
                callback();
                delayTrans.Enqueue(delayActionDict[callback]);
                delayActionDict.Remove(callback);
            });
        }

        public void AddDelayCallback(Action callbackAction, float delayDur, bool isFocusAdd = false)
        {
            TweenCallback callback = () => callbackAction();
            AddDelayCallback(callback, delayDur, isFocusAdd);
         
        }

        public void AddDelayCallback(Action callbackAction)
        {
            TweenCallback callback = () => callbackAction();
            AddDelayCallback(callback, 0.5f, false);

        }

        public void AddDelayCallbackA(Action callbackAction, float delayDur)
        {
            TweenCallback callback = () => callbackAction();
            AddDelayCallback(callback, delayDur, false);

        }

        public void AddTween(float f, float to, Action<float> callback, Action onComplete, float dur = 0.5f, Ease ease = Ease.Linear)
        {
            if (delayTrans.Count <= 0)
            {
                delayTrans.Enqueue(GetTran());
            }
            Transform tra = delayTrans.Dequeue();
            tra.localPosition = Vector3.up * f;
            tra.DOLocalMove(Vector3.up * to, dur).SetEase(ease).OnUpdate(()=> {
                if (callback != null) callback(tra.localPosition.y);
            }).OnComplete(()=> {
                if (onComplete != null) onComplete();
                tra.localPosition = Vector3.zero;
                delayTrans.Enqueue(tra);
            });

        }

        public void RemoveDelayCallback(TweenCallback callback)
        {
            if (!delayActionDict.ContainsKey(callback)) return;

            delayActionDict.Remove(callback);
        }

        private void DefaultInit()
        {
            for (int i = 0; i < 5; i++)
            {
                delayTrans.Enqueue(GetTran());

            }
        }

        private Transform GetTran()
        {
            Transform tr = new GameObject("DelayTran").transform;
            tr.parent = transform;
            tr.localPosition = Vector3.zero;
            return tr;
        }

        private void Update()
        {
            //if (UnityEngine.Input.GetKeyUp("1"))
            //{
            //    TweenCallback call = Test;
            //    AddDelayCallback(call, 3);
            //}
        }

        private void Test()
        {
            Debug.Log(111);
        }

        #region UNITY_EDITOR
#if UNITY_EDITOR
        [CustomEditor(typeof(DOTweenTimerManager))]
        public class DOTweenTimerManagerEditor : Editor
        {
            public override void OnInspectorGUI()
            {
                base.OnInspectorGUI();
                DOTweenTimerManager manager = target as DOTweenTimerManager;

                if (manager == null)
                {
                    return;
                }
                if (!EditorApplication.isPlaying)
                {
                    return;
                }
                EditorGUILayout.LabelField("注册的 delayActionDict：" + manager.delayActionDict.Count.ToString());
                foreach (var item in manager.delayActionDict)
                {
                    EditorGUILayout.LabelField(item.Key.Target.ToString() + " :: " + item.Key.Method.Name + " :: " + item.Value.ToString());
                }
                EditorGUILayout.LabelField("注册的 delayTrans：" + manager.delayTrans.Count.ToString());
                foreach (var item in manager.delayTrans)
                {
                    EditorGUILayout.LabelField(item.ToString());
                }
                this.Repaint();
            }
        }
#endif
        #endregion

    }
}