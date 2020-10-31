using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using MFramework_Unity;
using DG.Tweening;
#region using UNITY_EDITOR
#if UNITY_EDITOR
using UnityEditor;
#endif
#endregion

/* 
**************************************************************************************************************
Copyright/版权 (C) 2020 The arHop Studio. All Rights Reserved. 保留所有权利。
File Name/文件名:           EventTriggerButton.cs
Discription/描述:     		Be fully careful of  Code modification!
Author/作者:             	Korchagin
CreateTime/创建时间:        2020/4/17 19:56:35
Motto/座右铭:
Where there is ambition, everything will be done, and the boat will sink. 
The Pass of the 12th Qin Dynasty belongs to the Chu Dynasty. A painstaking man, who will live up to his fate, 
will live up to his courage, and three thousand Yuejia will swallow up Wu.
																	———— Pu Songling
有志者，事竟成，破釜沉舟，百二秦关终属楚；苦心人，天不负，卧薪尝胆，三千越甲可吞吴。
																	————  蒲松龄
**************************************************************************************************************
*/

/*
 *      PointerEnter = 0,
        PointerExit = 1,
        PointerDown = 2,
        PointerUp = 3,
        PointerClick = 4,
        Drag = 5,
        Drop = 6,
        Scroll = 7,
        UpdateSelected = 8,
        Select = 9,
        Deselect = 10,
        Move = 11,
        InitializePotentialDrag = 12,
        BeginDrag = 13,
        EndDrag = 14,
        Submit = 15,
        Cancel = 16
*/

public delegate void OnTriggerTouchDown(BaseEventData eventData);
public delegate void OnTriggerTouch(BaseEventData eventData);
public delegate void OnTriggerTouchUp(BaseEventData eventData);
public delegate void OnTriggerTouchClick(BaseEventData eventData);
public delegate void OnTriggerTouchEnter(BaseEventData eventData);
public delegate void OnTriggerTouchExit(BaseEventData eventData);

public class TriggerTouchClickedEvent : UnityEvent
{
    public TriggerTouchClickedEvent()
    {

    }
}
public class TriggerTouchEnterEvent : UnityEvent
{
    public TriggerTouchEnterEvent()
    {

    }
}
public class TriggerTouchExitEvent : UnityEvent
{
    public TriggerTouchExitEvent()
    {

    }
}
public class TriggerTouchDownEvent : UnityEvent
{
    public TriggerTouchDownEvent()
    {

    }
}
public class TriggerTouchUpEvent : UnityEvent
{
    public TriggerTouchUpEvent()
    {

    }
}
public class TriggerTouchEvent : UnityEvent
{
    public TriggerTouchEvent()
    {

    }
}

public class TriggerTouchDoubleClickedEvent : UnityEvent
{
    public TriggerTouchDoubleClickedEvent()
    {

    }
}
public class TriggerTouchThreeClickedEvent : UnityEvent
{
    public TriggerTouchThreeClickedEvent()
    {

    }
}
public class TriggerBeginDragEvent : UnityEvent<BaseEventData>
{
    public TriggerBeginDragEvent()
    {

    }
}

public class TriggerEndDragEvent : UnityEvent<BaseEventData>
{
    public TriggerEndDragEvent()
    {

    }
}

public class TriggerOnDragEvent : UnityEvent<BaseEventData>
{
    public TriggerOnDragEvent()
    {

    }
}


/// <summary>
/// EventTriggerButton
/// </summary>
[RequireComponent(typeof(EventTrigger))]
public class EventTriggerButton : UIBase
{
    protected EventTrigger EventTrigger
    {
        get
        {
            if (m_EventTrigger == null)
            {
                m_EventTrigger = gameObject.GetComponentReal<EventTrigger>();
            }
            return m_EventTrigger;
        }
    }
    protected EventTrigger m_EventTrigger;
    protected bool m_AllowTouchTriggerring = false;
    protected bool b_Triggerring = false;
    public OnTriggerTouchDown onTriggerTouchDown;
    public OnTriggerTouch onTriggerTouch;
    public OnTriggerTouchUp onTriggerTouchUp;
    public OnTriggerTouchClick onTriggerTouchClick;
    public OnTriggerTouchEnter onTriggerTouchEnter;
    public OnTriggerTouchExit onTriggerTouchExit;

    protected BaseEventData eventData;
    protected Vector3 defaultScale = Vector3.one;
    protected float rate = 1.05f;
    protected float dur = 0.5f;

    public TriggerTouchClickedEvent onClick { get; set; } = new TriggerTouchClickedEvent();
    public TriggerTouchDownEvent onTouchDown { get; set; } = new TriggerTouchDownEvent();
    public TriggerTouchUpEvent onTouchUp { get; set; } = new TriggerTouchUpEvent();
    public TriggerTouchEnterEvent onTouchEnter { get; set; } = new TriggerTouchEnterEvent();//鼠标载入
    public TriggerTouchExitEvent onTouchExit { get; set; } = new TriggerTouchExitEvent();
    public TriggerTouchEvent onTouch { get; set; } = new TriggerTouchEvent();
    public TriggerTouchDoubleClickedEvent onTouchDoubleClicked { get; set; } = new TriggerTouchDoubleClickedEvent();
    public TriggerBeginDragEvent onTriggerBeginDrag { get; set; } = new TriggerBeginDragEvent();
    public TriggerEndDragEvent onTriggerEndDrag { get; set; } = new TriggerEndDragEvent();
    public TriggerOnDragEvent onTriggerDrag { get; set; } = new TriggerOnDragEvent();

    public EventTrigger GetEventTrigger()
    {
        return EventTrigger;
    }

    public void SetDefaultTouchState(float rate = 1.05f, float dur = 0.5f)
    {
        this.rate = rate;
        this.dur = dur;
        onTriggerTouchEnter += OnTriggerTouchEnterDefault;
        onTriggerTouchExit += OnTriggerTouchExitDefault;
    }

    private void OnTriggerTouchEnterDefault(BaseEventData eventData)
    {
        transform.DOKill();
        transform.DOScale(defaultScale * rate, dur);
    }

    private void OnTriggerTouchExitDefault(BaseEventData eventData)
    {
        transform.DOKill();
        transform.DOScale(defaultScale, dur);
    }

    public void RemoveAllTouchStateListeners()
    {
        onTriggerTouchEnter = null;
        onTriggerTouchExit = null;
    }

    // Start is called before the first frame update
    protected override void Awake()
    {
        defaultScale = transform.localScale;

        // 注册按下
        RegisterEventTrigger(EventTrigger, EventTriggerType.PointerDown, (bae) => {
            if (onTriggerTouchDown != null)
            {
                onTriggerTouchDown(bae);
            }
            this.eventData = bae;
            b_Triggerring = true;

            if (onTouchDown != null)
            {
                onTouchDown.Invoke();
            }
        });
        // 注册抬起
        RegisterEventTrigger(EventTrigger, EventTriggerType.PointerUp, (bae) => {
            if (onTriggerTouchUp != null)
            {
                onTriggerTouchUp(bae);
            }
            this.eventData = null;
            b_Triggerring = false;

            if (onTouchUp != null)
            {
                onTouchUp.Invoke();
            }
        });
        // 注册点击
        RegisterEventTrigger(EventTrigger, EventTriggerType.PointerClick, (bae) => {
            if (onTriggerTouchClick != null)
            {
                onTriggerTouchClick(bae);
            }

            if (onClick != null)
            {
                onClick.Invoke();
            }
        });
        // 注册进入
        RegisterEventTrigger(EventTrigger, EventTriggerType.PointerEnter, (bae) => {
            if (onTriggerTouchEnter != null)
            {
                onTriggerTouchEnter(bae);
            }

            if (onTouchEnter != null)
            {
                onTouchEnter.Invoke();
            }
        });   
        // 注册退出
        RegisterEventTrigger(EventTrigger, EventTriggerType.PointerExit, (bae) => {
            if (onTriggerTouchExit != null)
            {
                onTriggerTouchExit(bae);
            }

            if (onTouchExit != null)
            {
                onTouchExit.Invoke();
            }
        });
        // 注册按住
        m_AllowTouchTriggerring = true;
        //RemoveEventTrigger(eventTrigger);

        // 注册开始拖拽
        RegisterEventTrigger(EventTrigger, EventTriggerType.BeginDrag, (bae) => {
            if (onTriggerBeginDrag != null)
            {
                onTriggerBeginDrag.Invoke(bae);
            }
        });

        // 注册结束拖拽
        RegisterEventTrigger(EventTrigger, EventTriggerType.EndDrag, (bae) => {
            if (onTriggerEndDrag != null)
            {
                onTriggerEndDrag.Invoke(bae);
            }
        });

        // 注册拖拽中
        RegisterEventTrigger(EventTrigger, EventTriggerType.Drag, (bae) => {
            if (onTriggerDrag != null)
            {
                onTriggerDrag.Invoke(bae);
            }
        });

    }

    // Update is called once per frame
    protected override void Update()
    {
        if (m_AllowTouchTriggerring == false)
        {
            return;
        }
        if (b_Triggerring)
        {
            if (onTriggerTouch != null)
            {
                onTriggerTouch(eventData);
            }

            if (onTouch != null)
            {
                onTouch.Invoke();
            }

        }
    }

    protected void RegisterEventTrigger(EventTrigger trigger, EventTriggerType eventTriggerType, UnityAction<BaseEventData> callback)
    {
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = eventTriggerType;
        entry.callback.AddListener(callback);
        trigger.triggers.Add(entry);

    }

    protected void RemoveEventTrigger(EventTrigger trigger, EventTriggerType eventTriggerType)
    {
        EventTrigger.Entry entry = null;

        for (int i = 0; i < trigger.triggers.Count; i++)
        {
            if (trigger.triggers[i].eventID == eventTriggerType)
            {
                entry = trigger.triggers[i];

                if (trigger.triggers.Contains(entry))
                    trigger.triggers.Remove(entry);
            }
        }
    }

    /// <summary>
    /// RemoveEventTrigger 功能未实现
    /// </summary>
    /// <param name="trigger"></param>
    /// <param name="eventTriggerType"></param>
    /// <param name="callback"></param>
    protected void RemoveEventTrigger(EventTrigger trigger, EventTriggerType eventTriggerType, UnityAction<BaseEventData> callback)
    {
        EventTrigger.Entry entry = null;

        for (int i = 0; i < trigger.triggers.Count; i++)
        {
            // trigger.triggers[i].callback == callback 语法错误
            if (trigger.triggers[i].eventID == eventTriggerType /*&& trigger.triggers[i].callback == callback*/)
            {
                entry = trigger.triggers[i];

                if (trigger.triggers.Contains(entry))
                    trigger.triggers.Remove(entry);
            }
        }
    }

    protected void RemoveEventTrigger(EventTrigger trigger)
    {
        trigger.triggers.Clear();
    }

    #region CustomEditor(typeof(EventTriggerButton)
#if UNITY_EDITOR

    private bool _showParameter = true;
	
	[CanEditMultipleObjects]
    [CustomEditor(typeof(EventTriggerButton))]
    public class EventTriggerButtonEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            EventTriggerButton manager = (EventTriggerButton)target;
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
                //GUILayout.BeginHorizontal("", boxStyle);
                //GUILayout.EndHorizontal();
                GUILayout.EndVertical();
            }
            #endregion

        }
    }
#endif
    #endregion
	
}
