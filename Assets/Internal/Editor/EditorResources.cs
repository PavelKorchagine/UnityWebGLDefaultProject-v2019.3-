using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;

/* 
*********************************************************************
Copyright (C) 2019 The Company Name
File Name:           EditorResources.cs
Author:              Korchagin
CreateTime:          2019
********************************************************************* 
*/

public class EditorResources
{
    public static T Load<T>(string assetName) where T : UnityEngine.Object
    {
        T t = default(T);
        t = EditorGUIUtility.Load(assetName) as T;
        return t;
    }


}
