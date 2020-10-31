using System;
using System.Collections.Generic;
using UnityEngine;
#region using UnityEditor;
#if UNITY_EDITOR
using UnityEditor;
#endif
#endregion

public class ObserveManager : MonoBehaviour
{
    public Dictionary<Type, List<IObserver>> pairs = new Dictionary<Type, List<IObserver>>();

    #region pairsBools
#if UNITY_EDITOR
    public Dictionary<Type, bool> pairsBools = new Dictionary<Type, bool>();
#endif
    #endregion

    //字段定义
    public bool ShowParameter = false;
    private List<DelayedNodifyMessage> current = new List<DelayedNodifyMessage>();
    private Queue<DelayedNodifyMessage> pool = new Queue<DelayedNodifyMessage>();

    public void SayHello()
    {
        Debug.LogWarningFormat("[{0}] say: Hello，World !! {1}", this, System.DateTime.Now);
    }

    #region Instance

    private static bool initialized;

    private static ObserveManager instance;
    public static ObserveManager Instance
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
            GameObject go = new GameObject("ObserveManager");
            instance = go.AddComponent<ObserveManager>();
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


    //API

    // Update is called once per frame
    protected virtual void OnUpdate()
    {
        HandleDelayedNodify();
    }

    //Func
    private void HandleDelayedNodify()
    {
        if (pool.Count == 0) return;
        current.Clear();
        lock (pool)
        {
            while (pool.Count > 0)
            {
                current.Add(pool.Dequeue());
            }
        }
        try
        {
            for (int i = 0; i < current.Count; i++)
            {
                for (int j = 0; j < pairs[current[i].observeType].Count; j++)
                {
                    pairs[current[i].observeType][j].ListenMethod(current[i].observeType, current[i].code, current[i].msg);
                }
            }
        }
        catch (Exception)
        {
            //Debug.LogFormat("<i><color=yellow> - HandleDelayedNodify Exception - over </color></i>");
        }
    }

    public void AddListener<T>(IObserver observer)
    {
        if (!pairs.ContainsKey(typeof(T)))
        {
            pairs.Add(typeof(T), new List<IObserver>());

            #region pairsBools.Add(typeof(T), false);
#if UNITY_EDITOR
            pairsBools.Add(typeof(T), false);
#endif
            #endregion

        }
        if (!pairs[typeof(T)].Contains(observer))
        {
            pairs[typeof(T)].Add(observer);
        }
    }
    public void AddListener<T>(T t, IObserver observer)
    {
        if (!pairs.ContainsKey(t.GetType()))
        {
            pairs.Add(t.GetType(), new List<IObserver>());

            #region pairsBools.Add(typeof(T), false);
#if UNITY_EDITOR
            pairsBools.Add(typeof(T), false);
#endif
            #endregion

        }
        if (!pairs[t.GetType()].Contains(observer))
        {
            pairs[t.GetType()].Add(observer);
        }
    }
    public void RemoveListener<T>(IObserver observer)
    {
        if (!pairs.ContainsKey(typeof(T))) return;
        if (!pairs[typeof(T)].Contains(observer)) return;
        pairs[typeof(T)].Remove(observer);
    }
    public void RemoveListener<T>(T t, IObserver observer)
    {
        if (!pairs.ContainsKey(t.GetType())) return;
        if (!pairs[t.GetType()].Contains(observer)) return;
        pairs[t.GetType()].Remove(observer);
    }
    [Obsolete("少用")]
    public void RemoveListener(IObserver observer)
    {
        foreach (var item in pairs.Values)
        {
            if (item.Contains(observer))
            {
                item.Remove(observer);
            }
        }
    }
    public void Notify<T>(long code, object msg)
    {
        if (!pairs.ContainsKey(typeof(T))) return;
        for (int i = 0; i < pairs[typeof(T)].Count; i++)
        {
            pairs[typeof(T)][i].ListenMethod(typeof(T), code, msg);
        }
    }
    public void Notify<T>(T t, long code, object msg)
    {
        if (!pairs.ContainsKey(t.GetType())) return;
        for (int i = 0; i < pairs[t.GetType()].Count; i++)
        {
            pairs[t.GetType()][i].ListenMethod(t.GetType(), code, msg);
        }
    }
    public void DelayedNodify<T>(long code, object msg)
    {
        if (!pairs.ContainsKey(typeof(T))) return;
        lock (pool)
        {
            pool.Enqueue(new DelayedNodifyMessage(typeof(T), code, msg));
        }
    }
    public void DelayedNodify<T>(T t, long code, object msg)
    {
        if (!pairs.ContainsKey(t.GetType())) return;
        lock (pool)
        {
            pool.Enqueue(new DelayedNodifyMessage(typeof(T), code, msg));
        }
    }


    #region UNITY_EDITOR ObserveManagerEditor

#if UNITY_EDITOR
    [ExecuteInEditMode]
    [CanEditMultipleObjects] //sure why not
    [CustomEditor(typeof(ObserveManager))]
    public class ObserveManagerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            ObserveManager manager = target as ObserveManager;
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
            manager.ShowParameter = EditorGUILayout.BeginToggleGroup(string.Format("ShowParameter"), manager.ShowParameter);
            EditorGUILayout.EndToggleGroup();
            GUILayout.EndVertical();

            if (manager.ShowParameter)
            {
                foreach (var item in manager.pairs.Keys)
                {
                    //GUILayout.Label(string.Format("<b> item - {0} - over </b>", item), firstLevelStyle);
                    GUILayout.BeginVertical("", boxStyle);
                    manager.pairsBools[item] = EditorGUILayout.BeginToggleGroup(string.Format("<b> item - {0} - over </b>", item), manager.pairsBools[item]);
                    // showItem Control
                    if (manager.pairsBools[item])
                    {
                        foreach (var iteme in manager.pairs[item])
                        {
                            GUILayout.Label("- " + iteme + " - over", firstLevelStyle);
                        }
                    }
                    EditorGUILayout.EndToggleGroup();
                    GUILayout.EndVertical();
                    // showItem End
                    this.Repaint();
                }
            }
            #endregion
            if (GUI.changed)
            {
                EditorUtility.SetDirty(manager);
            }
            serializedObject.ApplyModifiedProperties();
        }
    }

#endif

    #endregion


}

