using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.InteropServices;
#region UNITY_EDITOR
#if UNITY_EDITOR
using UnityEditor;
#endif
#endregion

/* 
*********************************************************************
Copyright (C) 2019 The Company Name
File Name:           StandaloneWebGLAPI.cs
Author:              Korchagin
CreateTime:          2019
********************************************************************* 
*/

public class StandaloneWebGLAPI
{
    [DllImport("__Internal")]
    private static extern void Hello();
    [DllImport("__Internal")]
    private static extern void Submit(int type, int success);
    [DllImport("__Internal")]
    private static extern string GetLoginInfo();
    [DllImport("__Internal")]
    private static extern void ToLinkTargetUrl(string str); 

    /// <summary>
    /// 打开新的窗口
    /// </summary>
    /// <param name="url"></param>
    public static void ExternalCallLinkApp(string str)
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        Hello();
        ToLinkTargetUrl(str);
        Debug.Log("UNITY_WEBGL:" + "ExternalCallLinkApp," + str);
#else
        Debug.Log("UNITY_EDITOR:" + "ExternalCallLinkApp," + str);
#endif
    }
    /// <summary>
    /// 提交成绩
    /// </summary>
    /// <param name="type"></param>
    /// <param name="success"></param>
    public static void ExternalCallSubmit(int type, int success)
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        Hello();
        Debug.LogWarning("[系统] ExternalCallSubmit - " + type + " - " + success);
        Submit(type, success);
#else
        Debug.LogWarning("[系统] ExternalCallSubmit - " + type + " - " + success);
#endif

    }

    /// <summary>
    /// 获取用户信息
    /// </summary>
    /// <returns></returns>
    public static string ExternalCallGetUserInfo()
    {
        string info = string.Empty;

#if UNITY_WEBGL && !UNITY_EDITOR
        Hello();
        info = GetLoginInfo();
        Debug.LogWarning("[系统] ExternalCallGetUserInfo - " + info);
#else
        //info = "编辑器开发平台";
        //Debug.LogWarning("[系统] ExternalCallGetUserInfo - " + info);

#endif
        return info;

    }

    [Obsolete]
    public static void ExternalCall(string fun, int type, int success)
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        Application.ExternalCall(fun, type, success);
#endif

    }

}
