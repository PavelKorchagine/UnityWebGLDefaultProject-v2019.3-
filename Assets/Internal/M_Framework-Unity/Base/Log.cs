using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Object = UnityEngine.Object;

/* 
*********************************************************************
Copyright (C) 2019 The Company Name
File Name:           Log.cs
Author:              Korchagin
CreateTime:          2019
********************************************************************* 
*/

public class Log 
{
    public static bool Enable = true;

#if FRAMEWORK_DISABLE_LOGGING
    [System.Diagnostics.Conditional("ALWAYS_FALSE")]
#endif
    public static void LogInfo(string message, Object context = null)
    {
        if (context == null)
        {
            Debug.Log("[FRAMEWORK] " + message);
        }
        else
        {
            Debug.Log("[FRAMEWORK] " + message, context);
        }
    }
#if FRAMEWORK_DISABLE_LOGGING
    [System.Diagnostics.Conditional("ALWAYS_FALSE")]
#endif
    public static void LogInfo(object message, Object context = null)
    {
        if (context == null)
        {
            Debug.Log("[FRAMEWORK] " + message);
        }
        else
        {
            Debug.Log("[FRAMEWORK] " + message, context);
        }
    }
    public static void Print(LogType logType, object data)
    {
        if (Enable == false) return;
        switch (logType)
        {
            case LogType.None:
                break;
            case LogType.L:
                UnityEngine.Debug.Log(data);
                break;
            case LogType.W:
                UnityEngine.Debug.LogWarning(data);
                break;
            case LogType.E:
                UnityEngine.Debug.LogError(data);
                break;
            default:
                break;
        }
    }

    public static void Print(LogType logType, string data, params object[] args)
    {
        if (Enable == false) return;
        switch (logType)
        {
            case LogType.None:
                break;
            case LogType.L:
                UnityEngine.Debug.LogFormat(data, args);
                break;
            case LogType.W:
                UnityEngine.Debug.LogWarningFormat(data, args);
                break;
            case LogType.E:
                UnityEngine.Debug.LogErrorFormat(data, args);
                break;
            default:
                break;
        }
    }

#if NORMAL_DISABLE_LOGGING
    [System.Diagnostics.Conditional("ALWAYS_FALSE")]
#endif
    public static void Default(object data)
    {
        if (Enable == false) return;
        UnityEngine.Debug.Log(data);
    }
    public static void Warning(object data)
    {
        if (Enable == false) return;
        UnityEngine.Debug.LogWarning(data);
    }
    public static void Error(object data)
    {
        if (Enable == false) return;
        UnityEngine.Debug.LogError(data);
    }
#if NORMAL_DISABLE_LOGGING
    [System.Diagnostics.Conditional("ALWAYS_FALSE")]
#endif
    public static void Default(string data, params object[] args)
    {
        if (Enable == false) return;
        UnityEngine.Debug.LogFormat(data, args);
    }
    public static void Warning(string data, params object[] args)
    {
        if (Enable == false) return;
        UnityEngine.Debug.LogWarningFormat(data, args);
    }
    public static void Error(string data, params object[] args)
    {
        if (Enable == false) return;
        UnityEngine.Debug.LogErrorFormat(data, args);
    }

    [System.Diagnostics.Conditional("UNITY_EDITOR")]
    public static void Assert(bool condition)
    {
        UnityEngine.Debug.AssertFormat(condition, string.Empty, false);
    }

    /// <summary>
    /// 运行时间精准测量
    /// </summary>
    /// <returns></returns>
    public static long GetWatch(Action call)
    {
        //1.运行时间精准测量
        System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
        sw.Start();
        //
        // 监控用时的中间代码...
        call();
        //
        sw.Stop();
        long len = sw.ElapsedMilliseconds;
        return len;
    }
    //2.设置断言，打印输出
    //如：重载窗口函数
    //protected override void OnResize(EventArgs e)
    //{
    //    base.OnResize(e);
    //    System.Diagnostics.Debug.Assert(this.Width > 200, "width should be larger than 200.");
    //    System.Diagnostics.Debug.WriteLine(this.Size.ToString());
    //}
}

public enum LogType
{
    None, 
    L,
    W,
    E
}