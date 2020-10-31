using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StandaloneInputSystem
{
    public static class ManualSetLaymask
    {
        private static void SetLaymask(this GameObject gameObject, int layerIndex, bool isContainChild = false)
        {
            gameObject.layer = layerIndex;
            if (!isContainChild) return;
            
            Transform[] childs = gameObject.transform.GetComponentsInChildren<Transform>(true);
            int length = childs.Length;
            for (int i = 0; i < length; i++)
            {
                childs[i].gameObject.layer = layerIndex;
            }
        }

        public static void SetLaymask(this GameObject gameObject, LayerMask mask, bool isContainChild = false)
        {
            //gameObject.SetLaymask(mask.value, isContainChild);
            SetLaymask(gameObject, mask.value, isContainChild);
        }

    }
}