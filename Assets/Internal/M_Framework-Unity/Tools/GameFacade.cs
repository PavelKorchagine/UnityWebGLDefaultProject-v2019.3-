using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace MFramework_Unity
{
    public partial class GameFacade : MonoBehaviour
    {
        private void Init()
        {

        }

        private void Start()
        {
            string usrInfo = string.Empty;

#if UNITY_EDITOR

#elif UNITY_WEBGL && !UNITY_EDITOR
            usrInfo = "编辑器开发平台";
#endif
        }
    }

}