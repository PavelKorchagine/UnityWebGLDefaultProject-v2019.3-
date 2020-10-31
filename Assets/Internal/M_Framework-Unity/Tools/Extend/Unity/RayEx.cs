using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MFramework_Unity.Tools
{
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

    /// <summary>
    /// 自定义的射线
    /// </summary>
    public struct RayEx
    {
        public struct RayExCast
        {
            public bool IsRayCast;
            public GameObject GameObjRayCast;
            public string GameObjRayCastName;
            public Vector3 Vector3RayCastPoint;
            public float Distance;
            public Ray RealRay;
        }
        public static RayExCast GetMouseRayCast(Camera mainCamera)
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            return PhysicsRayCast(ray);
        }

        private static RayExCast PhysicsRayCast(Ray ray)
        {
            RaycastHit rH;

            RayExCast _rayCast = new RayExCast()
            {
                IsRayCast = false,
                GameObjRayCast = null,
                GameObjRayCastName = string.Empty,
                Vector3RayCastPoint = Vector3.zero,
                Distance = 0f,
                RealRay = ray
            };

            if (Physics.Raycast(ray, out rH))
            {
                _rayCast.IsRayCast = true;
                _rayCast.GameObjRayCast = rH.collider.gameObject;
                _rayCast.GameObjRayCastName = rH.collider.name;
                _rayCast.Vector3RayCastPoint = rH.point;
                _rayCast.Distance = rH.distance;

#if UNITY_EDITOR
                Debug.DrawLine(ray.origin, rH.point, Color.red); //画轨迹
#endif
            }

            return _rayCast;
        }

        public static RayExCast GetCast(Ray ray)
        {
            return PhysicsRayCast(ray);
        }

        public static RayExCast GetCast(Ray ray, float maxDistance = 1000, int myLayerMask = -1)
        {
            return PhysicsRayCast(ray, maxDistance, myLayerMask);
        }

        public static RayExCast GetMouseRayCast(Camera mainCamera, float maxDistance = 1000, int myLayerMask = -1)
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            return PhysicsRayCast(ray, maxDistance, myLayerMask);
        }

        private static RayExCast PhysicsRayCast(Ray ray, float maxDistance = 1000, int myLayerMask = -1)
        {
            RaycastHit rH;

            RayExCast _rayCast = new RayExCast()
            {
                IsRayCast = false,
                GameObjRayCast = null,
                GameObjRayCastName = string.Empty,
                Vector3RayCastPoint = Vector3.zero,
                Distance = 0f,
                RealRay = ray
            };

            if (Physics.Raycast(ray, out rH, maxDistance, myLayerMask))
            {
                _rayCast.IsRayCast = true;
                _rayCast.GameObjRayCast = rH.collider.gameObject;
                _rayCast.GameObjRayCastName = rH.collider.name;
                _rayCast.Vector3RayCastPoint = rH.point;
                _rayCast.Distance = rH.distance;

#if UNITY_EDITOR
                if (myLayerMask == 1)
                    Debug.DrawLine(ray.origin, rH.point, Color.red); //画轨迹
#endif
            }

            return _rayCast;
        }

        public static RayExCast GetRayCast(Transform tran, float maxDistance = 1000, int myLayerMask = -1)
        {
            return GetRayCast(tran.position, tran.forward, maxDistance, myLayerMask);
        }

        public static RayExCast GetRayCast(Vector3 pos, Vector3 forward, float maxDistance = 1000, int myLayerMask = -1)
        {
            Ray ray = new Ray(pos, forward);
            return PhysicsRayCast(ray, maxDistance, myLayerMask);
        }
    }

}