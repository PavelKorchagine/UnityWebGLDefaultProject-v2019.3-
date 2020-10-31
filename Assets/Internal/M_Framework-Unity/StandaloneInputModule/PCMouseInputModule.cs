///////////////////////////////////////////////////////////////////////////////
// Copyright 2017-2019  Vrtist Technology Co., Ltd. All Rights Reserved.
// File:  
// Author: 
// Date:  
// Discription:  
/////////////////////////////////////////////////////////////////////////////// 

#define HTCVIVE
#define PC
#define OCULUS_QUEST

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
#region UNITY_EDITOR
#if UNITY_EDITOR
using UnityEditor;
#endif
#endregion

namespace MFramework_Unity.InputSystem
{
    /// <summary>
    /// abstract
    /// </summary>
    public class PCMouseInputModule : MonoBehaviour
    {
        /************************************    属性字段  *************************************/

        public bool isUIOcclusion = false;
        public float rayDistance = 5;
        internal GameObject targetObj;
        public GameObject objRayCast;
        internal _Ray.RayCast rayCast = new _Ray.RayCast();
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
        protected IPointStayHandler _pointStayHandler;
        private bool IsOverGameObject = false;
        public bool isHitOnUI = false;
        [SerializeField] private bool enableDC = false;
        public float doubleClickInterval = 0.3f;
        [SerializeField] private float _doubleClick = 0;
        protected IPointClickHandler lastClickHandler;
        private bool Ctrldown;

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
        public GUIStyle style1;

        public virtual void OnInit(params object[] paras)
        {

        }

        public virtual void OnInit()
        {

        }

        public virtual void UpdateEventMainCamera(Camera mainCamera, Transform inputStickTra = null)
        {
            if (this.mainCamera == mainCamera)
            {
                if (this.inputStick == null && inputStickTra != null)
                {
                    this.inputStick = inputStickTra.GetComponent<MouseInputStick>();
                }
            }
            if (this.mainCamera != mainCamera)
            {
                this.mainCamera = mainCamera;
                if (inputStickTra != null)
                {
                    this.inputStick = inputStickTra.GetComponent<MouseInputStick>();
                }
            }
        }

        /************************************ 私有方法  *********************************/
        protected virtual void ExecuteHandler(GameObject go)
        {
            if (enableDC)
            {
                _doubleClick -= Time.deltaTime;

                if (_doubleClick <= 0)
                {
                    enableDC = false;
                    lastClickHandler = null;

                }
            }

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
                _pointStayHandler = null;
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
                    _pointStayHandler = selectObject.GetComponent<IPointStayHandler>();
                }

                if (_pointStayHandler != null)
                {
                    _pointStayHandler.OnPointStay(rayCast);
                }

#if HTCVIVE
#endif
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

                                if (lastClickHandler != null && handerClick == lastClickHandler)
                                {
                                    IPointDoubleClickHandler handerDClick = go.GetComponent<IPointDoubleClickHandler>();
                                    if (handerDClick != null)
                                    {
                                        handerDClick.OnPointDoubleClick(rayCast);
                                    }

                                }

                                lastClickHandler = handerClick;
                                _doubleClick = doubleClickInterval;
                                enableDC = true;
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
                if (UnityEngine.Input.GetKeyDown(KeyCode.LeftControl))///////////////////////////
                {
                    Ctrldown = true;
              
                }
                if (UnityEngine.Input.GetKeyUp(KeyCode.LeftControl))
                {
                    Ctrldown = false;

                }
                if (Ctrldown)
                {
                    if (UnityEngine.Input.GetMouseButtonUp(0))
                    {
                        Debug.Log("quanxuan");

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

                                    if (lastClickHandler != null && handerClick == lastClickHandler)
                                    {
                                        IPointDoubleClickHandler handerDClick = go.GetComponent<IPointDoubleClickHandler>();
                                        if (handerDClick != null)
                                        {
                                            handerDClick.OnPointDoubleClick(rayCast);
                                        }

                                    }

                                    lastClickHandler = handerClick;
                                    _doubleClick = doubleClickInterval;
                                    enableDC = true;
                                }
                            }
                        }

                        
                        selectObject = null;
                    }
                }///////////////////////////////////////////////
            }

        }

        //检查是否满足条件
        protected virtual bool CheckCondition()
        {
            bool isConditioned = false;
            //#if !UNITY_STANDALONE_WIN || UNITY_EDITOR || UNITY_STANDALONE_OSX
            isConditioned = true;
            return isConditioned;
        }

        /// <summary>
        /// 获取射线 和 摄取物体
        /// </summary>
        protected virtual GameObject GetRayCast()
        {
            return null;
        }

        private Vector3 MouseToWorldPosition()
        {
#if UNITY_EDITOR || UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX
            //screenSpace = Camera.main.WorldToScreenPoint(_movedObject.GetObject().transform.position);
            //return mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z));
            return Vector3.zero;
#else
            return Vector3.zero;
#endif

        }

        /*************************************  API ****************************************/

        protected virtual void Start()
        {
            //if (mainCamera == null) mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        }


        protected virtual void Update()
        {
            if (mainCamera == null)
            {
                return;
            }

            isHitOnUI = IsClickUI();

#if UNITY_EDITOR || UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX || UNITY_WEBGL || UNITY_ANDROID

            // 真实物体交互
            rayCast = _Ray.GetMouseRayCast(mainCamera, rayDistance, realRayPointLayerMask);
            if (rayCast.RealRay.direction != Vector3.zero && inputStick) inputStick.transform.forward = rayCast.RealRay.direction;

            //inputStick.transform.forward = rayCast.RealRay.direction;
            ExecuteHandler(rayCast.GameObjRayCast);
            objRayCast = rayCast.GameObjRayCast;
            // 获取虚拟坐标
            virtualRayCast = _Ray.GetMouseVirtualRayCast(mainCamera, 100, virtualPointLayerMask);
            if (virtualRayCast.IsRayCast)
            {
                virtualPoint = virtualRayCast.Vector3RayCastPoint;
                if (inputStick) inputStick.SetCursorPostion(virtualPoint);
            }

#endif

#if UNITY_EDITOR
            if (UnityEngine.Input.GetMouseButtonDown(0))
            {
                //Debug.Log("EventSystem.current.IsPointerOverGameObject(): " + EventSystem.current.IsPointerOverGameObject());
                //Debug.Log("EventSystem.current.IsPointerOverGameObject(-1): " + EventSystem.current.IsPointerOverGameObject(-1));
                //Debug.Log("EventSystem.current.IsPointerOverGameObject(0): " + EventSystem.current.IsPointerOverGameObject(0));
                //Debug.Log("EventSystem.current.IsPointerOverGameObject(10000): " + EventSystem.current.IsPointerOverGameObject(10000));
                //生成子弹

            }

            if (Input.GetMouseButtonDown(0))
            {
                IsOverGameObject = EventSystem.current.IsPointerOverGameObject();
                if (IsOverGameObject)
                {
                    //Debug.Log("Click the UI GetMouseButtonDown");
                    return;
                }
            }

            if (Input.GetMouseButton(1))
            {

                /* Debug.Log("点到了");
                IsOverGameObject = EventSystem.current.IsPointerOverGameObject();
                IsOverGameObject = EventSystem.current.IsPointerOverGameObject() || IsOverGameObject;
                if (IsOverGameObject)
                {
                    //Debug.Log("Click the UI GetMouseButtonUp");
                    return;
                }

                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray.origin, ray.direction, out RaycastHit hit, float.MaxValue))
                {
                    if (null != hit.collider)
                    {
                        //Debug.LogError(hit.collider.name);
                    }
                }*/
            }
#endif
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

        protected virtual void FixedUpdate()
        {
            //if (rayCast.RealRay.direction != Vector3.zero)
            //    inputStick.transform.forward = rayCast.RealRay.direction;
        }

        protected virtual void OnGUI()
        {
            if (rayCast.IsRayCast)
            {
                //style1 = new GUIStyle();
                style1.fontSize = 15;
                style1.normal.textColor = Color.white;
                if (rayCast.ModuleInfo != null)
                {
                    if (rayCast.ModuleInfo.GetGameObjLayer() == 0)
                    {
                        style1.normal.textColor = Color.red;
                    }
                    else
                    {
                        style1.normal.textColor = Color.gray;
                    }
                    GUI.Label(new Rect(Input.mousePosition.x - 20, Screen.height - Input.mousePosition.y - 30, 400, 50), rayCast.ModuleInfo.GetGuiShowInfo(), style1);
                }
                //else
                //    GUI.Label(new Rect(Input.mousePosition.x - 20, Screen.height - Input.mousePosition.y - 30, 400, 50), rayCast.GameObjRayCastName, style1);
            }


        }


        #region CustomEditor(typeof(PCMouseInputModule)
#if UNITY_EDITOR

        private bool _showParameter = true;

        [CanEditMultipleObjects]
        [CustomEditor(typeof(PCMouseInputModule))]
        public class PCMouseInputModuleEditor : Editor
        {
            public override void OnInspectorGUI()
            {
                PCMouseInputModule manager = (PCMouseInputModule)target;
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