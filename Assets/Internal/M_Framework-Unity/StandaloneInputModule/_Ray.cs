using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MFramework_Unity.InputSystem
{
    /// <summary>
    /// 自定义的射线
    /// </summary>
    public struct _Ray
    {
        public struct RayCast
        {
            public bool IsRayCast;
            public GameObject GameObjRayCast;
            public ModuleShowGUIInfo ModuleInfo;
            public string GameObjRayCastName;
            public Vector3 Vector3RayCastPoint;
            public float Distance;
            public Ray RealRay;
            public RaycastHit raycastHit;
        }
        public static RayCast GetMouseRayCast(Camera mainCamera)
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            return PhysicsRayCast(ray);
        }

        private static RayCast PhysicsRayCast(Ray ray)
        {
            RaycastHit rH;

            RayCast _rayCast = new RayCast()
            {
                IsRayCast = false,
                GameObjRayCast = null,
                GameObjRayCastName = string.Empty,
                Vector3RayCastPoint = Vector3.zero,
                Distance = 0f,
                RealRay = ray,
                raycastHit = new RaycastHit()
            };

            if (Physics.Raycast(ray, out rH))
            {
                _rayCast.IsRayCast = true;
                _rayCast.GameObjRayCast = rH.collider.gameObject;
                _rayCast.GameObjRayCastName = rH.collider.name;
                _rayCast.ModuleInfo = _rayCast.GameObjRayCast.GetComponent<ModuleShowGUIInfo>();
                _rayCast.Vector3RayCastPoint = rH.point;
                _rayCast.Distance = rH.distance;
                _rayCast.raycastHit = rH;
#if UNITY_EDITOR
                Debug.DrawLine(ray.origin, rH.point, Color.red); //画轨迹
#endif
            }

            return _rayCast;
        }

        public static RayCast GetCast(Ray ray)
        {
            return PhysicsRayCast(ray);
        }

        public static RayCast GetCast(Ray ray, float maxDistance = 1000, int myLayerMask = -1)
        {
            return PhysicsRayCast(ray, maxDistance, myLayerMask);
        }

        public static RayCast GetMouseRayCast(Camera mainCamera, float maxDistance = 1000, int myLayerMask = -1)
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            return PhysicsRayCast(ray, maxDistance, myLayerMask);
        }

        public static RayCast GetMouseVirtualRayCast(Camera mainCamera, float maxDistance = 1000, int myLayerMask = -1)
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            return PhysicsRayCast(ray, maxDistance, myLayerMask, false);
        }

        private static RayCast PhysicsRayCast(Ray ray, float maxDistance = 1000, int myLayerMask = -1, bool isdebugLine = true)
        {
            RaycastHit rH;

            RayCast _rayCast = new RayCast()
            {
                IsRayCast = false,
                GameObjRayCast = null,
                GameObjRayCastName = string.Empty,
                Vector3RayCastPoint = Vector3.zero,
                Distance = 0f,
                RealRay = ray,
                raycastHit = new RaycastHit()
            };
#if UNITY_EDITOR
            if (!Physics.Raycast(ray, out rH))
            {
            }
#endif
            if (Physics.Raycast(ray, out rH, maxDistance, myLayerMask))
            {
                _rayCast.IsRayCast = true;
                _rayCast.GameObjRayCast = rH.collider.gameObject;
                _rayCast.GameObjRayCastName = rH.collider.name;
                _rayCast.ModuleInfo = _rayCast.GameObjRayCast.GetComponent<ModuleShowGUIInfo>();
                _rayCast.Vector3RayCastPoint = rH.point;
                _rayCast.Distance = rH.distance;
                _rayCast.raycastHit = rH;

#if UNITY_EDITOR
                if (isdebugLine)
                {
                    // 0: 代表default层
                    if (_rayCast.ModuleInfo == null || _rayCast.ModuleInfo.GetGameObjLayer() != 0)
                    {
                        Debug.DrawLine(ray.origin, rH.point, Color.gray); //画轨迹

                    }
                    else if (_rayCast.ModuleInfo.GetGameObjLayer() == 0)
                    {
                        Debug.DrawLine(ray.origin, rH.point, Color.red); //画轨迹
                    }
                }
#endif
            }
            else
            {
#if UNITY_EDITOR
                if (isdebugLine)
                {
                    Debug.DrawLine(ray.origin, ray.origin + ray.direction * maxDistance, Color.white); //画轨迹

                }
#endif
            }
            return _rayCast;
        }

        public static RayCast GetRayCast(Transform tran, float maxDistance = 1000, int myLayerMask = -1)
        {
            return GetRayCast(tran.position, tran.forward, maxDistance, myLayerMask);
        }

        public static RayCast GetRayCast(Vector3 pos, Vector3 forward, float maxDistance = 1000, int myLayerMask = -1)
        {
            Ray ray = new Ray(pos, forward);
            return PhysicsRayCast(ray, maxDistance, myLayerMask); 
        }
    }
}