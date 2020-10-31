using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
#region using UNITY_EDITOR
#if UNITY_EDITOR
using UnityEditor;
#endif
#endregion

/* 
**************************************************************************************************************
Copyright/版权 (C) 2020 The arHop Studio. All Rights Reserved. 保留所有权利。
File Name/文件名:           ObjectPoolManager.cs
Discription/描述:     		Be fully careful of  Code modification!
Author/作者:             	Korchagin
CreateTime/创建时间:        2020/8/24 11:21:16
**************************************************************************************************************
*/

/// <summary>
/// ObjectPoolManager
/// </summary>
public class ObjectPoolManager : MonoBehaviour
{
    #region ObjectPoolManager Instance

    private static bool initialized;

    private static ObjectPoolManager instance;
    public static ObjectPoolManager Instance
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
            GameObject go = new GameObject("ObjectPoolManager");
            instance = go.AddComponent<ObjectPoolManager>();
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

    private Dictionary<Type, IEnumerable> objectPoolDict = new Dictionary<Type, IEnumerable>();

    public Transform ObjectPoolsRoot
    {
        get {
            if (_objectPoolsRoot == null)
            {
                _objectPoolsRoot = new GameObject("ObjectPoolsRoot").transform;
                _objectPoolsRoot.localPosition = _objectPoolsRoot.localEulerAngles = Vector3.zero;
                _objectPoolsRoot.localScale = Vector3.one;
            }
            return _objectPoolsRoot;
        }
    }

    private Transform _objectPoolsRoot;

    public Transform GetBornPointTran<T>() where T : Component
    {
        Transform tar = ObjectPoolsRoot.Find(typeof(T).Name);
        if (tar == null)
        {
            tar = new GameObject(typeof(T).Name).transform;
        }
        tar.parent = ObjectPoolsRoot;
        tar.localPosition = tar.localEulerAngles = Vector3.zero;
        tar.localScale = Vector3.one;
        return tar;
    }

    public ObjectPool<T> GetObjectPool<T>(T it, int ori = 5) where T : Component
    {
        try
        {
            if (!objectPoolDict.ContainsKey(typeof(T)))
            {
                Queue<ObjectPool<T>> objectPoolQueue = new Queue<ObjectPool<T>>();
                ObjectPool<T> objectPool = new ObjectPool<T>(it, ori);
                objectPoolQueue.Enqueue(objectPool);
                objectPoolDict.Add(typeof(T), objectPoolQueue);
            }
            else if (objectPoolDict.ContainsKey(typeof(T)) && objectPoolDict[typeof(T)] as Queue<ObjectPool<T>> != null)
            {
                Queue<ObjectPool<T>> objectPoolQueue = objectPoolDict[typeof(T)] as Queue<ObjectPool<T>>;
                if (objectPoolQueue.Count <= 0)
                {
                    ObjectPool<T> objectPool = new ObjectPool<T>(it, ori);
                    objectPoolQueue.Enqueue(objectPool);
                }
            }

            return (objectPoolDict[typeof(T)] as Queue<ObjectPool<T>>).Dequeue();
        }
        catch (Exception)
        {
            return null;
        }
    }

    public void ResumeObjectPool<T>(ObjectPool<T> objectPool) where T : Component
    {
        try
        {
            if (!objectPoolDict.ContainsKey(typeof(T)))
            {
                Queue<ObjectPool<T>> objectPoolQueue = new Queue<ObjectPool<T>>();
                objectPoolDict.Add(typeof(T), objectPoolQueue);
            }

            Queue<ObjectPool<T>> que = objectPoolDict[typeof(T)] as Queue<ObjectPool<T>>;
            que.Enqueue(objectPool);

        }
        catch (Exception)
        {

        }
    }


	#region CustomEditor(typeof(ObjectPoolManager)
#if UNITY_EDITOR

    private bool _showParameter = true;
	
	[CanEditMultipleObjects]
    [CustomEditor(typeof(ObjectPoolManager))]
    public class ObjectPoolManagerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            ObjectPoolManager manager = (ObjectPoolManager)target;
            GUI.changed = false;
            serializedObject.Update();
            //GUI style 设置
            GUIStyle firstLevelStyle = new GUIStyle(GUI.skin.label);
            firstLevelStyle.alignment = TextAnchor.UpperLeft;
            firstLevelStyle.fontStyle = FontStyle.Normal;
            firstLevelStyle.fontSize = 11;
            firstLevelStyle.wordWrap = true;

            GUIStyle boxStyle = new GUIStyle(GUI.skin.box);
            boxStyle.normal.textColor = GUI.skin.label.normal.textColor;
            boxStyle.fontStyle = FontStyle.Bold;
            boxStyle.alignment = TextAnchor.UpperLeft;

            #region ShowParameter
            GUILayout.BeginVertical("", boxStyle);
            manager._showParameter = EditorGUILayout.BeginToggleGroup(string.Format("ShowParameter"), manager._showParameter);
            EditorGUILayout.EndToggleGroup();
            GUILayout.EndVertical();

            if (manager._showParameter)
            {
                GUILayout.BeginVertical("", boxStyle);
                base.OnInspectorGUI();

                if (Application.isPlaying)
                {
                    foreach (var item in manager.objectPoolDict.Keys)
                    {
                        GUILayout.Label($"Type: {item}");
                        foreach (var iteme in manager.objectPoolDict[item])
                        {
                            GUILayout.Label($"ObjectPool<T>: {iteme}");
                        }
                    }
                }
                //GUILayout.BeginHorizontal("", boxStyle);
                //GUILayout.EndHorizontal();
                GUILayout.EndVertical();
            }
            #endregion

            this.Repaint();
        }
    }
#endif
    #endregion
	
}

