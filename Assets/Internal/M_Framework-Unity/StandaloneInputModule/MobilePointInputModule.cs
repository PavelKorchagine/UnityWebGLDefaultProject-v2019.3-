using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;
#region using UNITY_EDITOR
#if UNITY_EDITOR
using UnityEditor;
#endif
#endregion

/* 
**************************************************************************************************************
Copyright/版权 (C) 2020 The arHop Studio. All Rights Reserved. 保留所有权利。
File Name/文件名:           MobilePointInputModule.cs
Discription/描述:     		Be fully careful of  Code modification!
Author/作者:             	Korchagin
CreateTime/创建时间:        2020/5/12 12:17:58
Motto/座右铭:
Where there is ambition, everything will be done, and the boat will sink. 
The Pass of the 12th Qin Dynasty belongs to the Chu Dynasty. A painstaking man, who will live up to his fate, 
will live up to his courage, and three thousand Yuejia will swallow up Wu.
																	———— Pu Songling
有志者，事竟成，破釜沉舟，百二秦关终属楚；苦心人，天不负，卧薪尝胆，三千越甲可吞吴。
																	————  蒲松龄
**************************************************************************************************************
*/

namespace MFramework_Unity.InputSystem
{
    /// <summary>
    /// MobilePointInputModule
    /// </summary>
    public class MobilePointInputModule : MonoBehaviour
    {
        public float rayDistance = 5;
        internal GameObject targetObj;
        public GameObject objRayCast;
        internal _Ray.RayCast rayCast;
        protected _Ray.RayCast virtualRayCast;
        [SerializeField] protected Camera mainCamera;
        [SerializeField] protected MouseInputStick inputStick;
        protected Transform _originPivot;
        protected GameObject selectObject;
        protected Ray _ray;
        protected RaycastHit rH;
        protected GameObject _realPointDownObject;
        protected float _rotation;
        protected WaitForSeconds waitseconds = new WaitForSeconds(0.5f);
        protected bool Show;
        protected bool isShowOnGUI = false;
        protected IPointDownHandler _pointDownHandler;
        public bool isUIOcclusion = true;
        /*
      * 在代码中使用时如何开启某个Layers？
      * LayerMask mask = 1 << 你需要开启的Layers层。
      * LayerMask mask = 0 << 你需要关闭的Layers层。
      * 举几个个栗子：
      * LayerMask mask = 1 << 2; 表示开启Layer2。
      * LayerMask mask = 0 << 5;表示关闭Layer5。
      * LayerMask mask = 1<<2|1<<8;表示开启Layer2和Layer8。
      * LayerMask mask = 0<<3|0<<7;表示关闭Layer3和Layer7。
      * 上面也可以写成：
      * LayerMask mask = ~（1<<3|1<<7）;表示关闭Layer3和Layer7。
      * LayerMask mask = 1<<2|0<<4;表示开启Layer2并且同时关闭Layer4.
      */
        [SerializeField] protected LayerMask realRayPointLayerMask = 1 << 0 | 1 << 5;//1 : 2的0次方
        protected Vector3 virtualPoint;
        protected LayerMask virtualPointLayerMask = 1 << 9;//512 : 2的9次方

        protected virtual void ExecuteHandler(GameObject go)
        {
            // 检测是否点击到UI层上，用于鼠标穿透
            if (isUIOcclusion && IsClickUI())
            {
                if (selectObject != null)
                {
                    IPointExitHandler hander = selectObject.GetComponent<IPointExitHandler>();
                    if (hander != null)
                    {
                        hander.OnPointExit();
                    }
                    selectObject = null;

                }

                return;
            }

            if (selectObject != null && selectObject != go)
            {
                IPointExitHandler hander = selectObject.GetComponent<IPointExitHandler>();
                if (hander != null)
                {
                    hander.OnPointExit();
                }
                selectObject = null;
            }

            if (go != null)
            {
                if (selectObject != go)
                {
                    IPointEnterHandler hander = go.GetComponent<IPointEnterHandler>();
                    if (hander != null)
                    {
                        hander.OnPointEnter();
                    }
                    selectObject = go;

                }
                if (UnityEngine.Input.GetMouseButtonDown(0) && go != null)
                {
                    _pointDownHandler = go.GetComponent<IPointDownHandler>();
                    if (_pointDownHandler != null)
                    {
                        _realPointDownObject = _pointDownHandler.OnPointDown(selectObject);
                    }
                    //_movedObject = selectObject.GetComponent<IMovableObject>();

                    //BaseExMonoInter baseMonoInter = go.GetComponent<BaseExMonoInter>();
                    //if (baseMonoInter != null)
                    //{
                    //    baseMonoInter.OnPickup(inputStick.GetCursor().gameObject);
                    //    selectObject = baseMonoInter.gameObject;
                    //    //baseMonoInter.GetComponent<Rigidbody>().isKinematic = true;
                    //}

                }
                else if (UnityEngine.Input.GetMouseButtonUp(0) && selectObject != null)
                {
                    IPointUpHandler hander = go.GetComponent<IPointUpHandler>();
                    GameObject upObj = null;
                    if (hander != null)
                    {
                        upObj = hander.OnPointUp(selectObject);
                        if (upObj == _realPointDownObject)
                        {
                            IPointClickHandler handerClick = go.GetComponent<IPointClickHandler>();
                            if (handerClick != null)
                            {
                                handerClick.OnPointClick(rayCast);
                            }
                        }
                    }

                    //BaseMonoInter baseMonoInter = selectObject.GetComponent<BaseMonoInter>();
                    //if (baseMonoInter != null)
                    //{
                    //    baseMonoInter.OnUnPickup(inputStick.GetCursor().gameObject);
                    //}
                    selectObject = null;
                }
            }
        }

        /// <summary>
        /// 检测是否点击在UI上
        /// </summary>
        /// <returns></returns>
        protected virtual bool IsClickUI()
        {
            if (EventSystem.current != null)
            {
                PointerEventData eventData = new PointerEventData(EventSystem.current);
                eventData.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                List<RaycastResult> results = new List<RaycastResult>();
                EventSystem.current.RaycastAll(eventData, results);
                return results.Count > 0;
            }
            return false;
        }

        /// <summary>
        /// IsPointerOverGameObject
        /// </summary>
        /// <returns></returns>
        public bool IsPointerOverGameObject()
        {
            //if (Input.touchCount > 0) {

            //    int id = Input.GetTouch(0).fingerId;
            //    return UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject(id);//安卓机上不行
            //}
            //else {
            //return UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject();
            PointerEventData eventData = new PointerEventData(UnityEngine.EventSystems.EventSystem.current);
            eventData.pressPosition = Input.mousePosition;
            eventData.position = Input.mousePosition;

            List<RaycastResult> list = new List<RaycastResult>();
            UnityEngine.EventSystems.EventSystem.current.RaycastAll(eventData, list);
            //Debug.Log(list.Count);
            return list.Count > 0;
            // }
        }

        /// <summary>
        /// IsPointerOverGameObject 方法二 通过UI事件发射射线 -- 是 2D UI 的位置，非 3D 位置.
        /// </summary>
        /// <param name="screenPosition"></param>
        /// <returns></returns>
        public bool IsPointerOverGameObject(Vector2 screenPosition)
        {
            //实例化点击事件
            PointerEventData eventDataCurrentPosition = new PointerEventData(UnityEngine.EventSystems.EventSystem.current);
            //将点击位置的屏幕坐标赋值给点击事件
            eventDataCurrentPosition.position = new Vector2(screenPosition.x, screenPosition.y);

            List<RaycastResult> results = new List<RaycastResult>();
            //向点击处发射射线
            EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

            return results.Count > 0;
        }

        /// <summary>
        /// 方法三 通过画布上的 GraphicRaycaster 组件发射射线
        /// </summary>
        /// <param name="canvas"></param>
        /// <param name="screenPosition"></param>
        /// <returns></returns>
        public bool IsPointerOverGameObject(Canvas canvas, Vector2 screenPosition)
        {
            //实例化点击事件
            PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
            //将点击位置的屏幕坐标赋值给点击事件
            eventDataCurrentPosition.position = screenPosition;
            //获取画布上的 GraphicRaycaster 组件
            GraphicRaycaster uiRaycaster = canvas.gameObject.GetComponent<GraphicRaycaster>();

            List<RaycastResult> results = new List<RaycastResult>();
            // GraphicRaycaster 发射射线
            uiRaycaster.Raycast(eventDataCurrentPosition, results);

            return results.Count > 0;
        }

        public virtual void OnInit()
        {

        }
        private void Awake()
        {
            if (mainCamera == null) mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        }

        private void Update()
        {
            rayCast = _Ray.GetMouseRayCast(mainCamera, rayDistance, realRayPointLayerMask);
            if (rayCast.RealRay.direction != Vector3.zero) inputStick.transform.forward = rayCast.RealRay.direction;
        }

        #region CustomEditor(typeof(MobilePointInputModule)
#if UNITY_EDITOR

        private bool _showParameter = true;

        [CanEditMultipleObjects]
        [CustomEditor(typeof(MobilePointInputModule))]
        public class MobilePointInputModuleEditor : Editor
        {
            public override void OnInspectorGUI()
            {
                MobilePointInputModule manager = (MobilePointInputModule)target;
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

}