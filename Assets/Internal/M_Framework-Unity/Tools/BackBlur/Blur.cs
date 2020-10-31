using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
#region using UNITY_EDITOR
#if UNITY_EDITOR
using UnityEditor;
#endif
#endregion

/* 
**************************************************************************************************************
Copyright/版权 (C) 2020 The arHop Studio. All Rights Reserved. 保留所有权利。
File Name/文件名:           Blur.cs
Discription/描述:     		Be fully careful of  Code modification!
Author/作者:             	Korchagin
CreateTime/创建时间:        2020/7/14 11:18:33
**************************************************************************************************************
*/

/// <summary>
/// Blur
/// </summary>
[ExecuteInEditMode]
public class Blur : MonoBehaviour
{
    public Material blurMaterial;
    [Range(0.0f, 2.0f)]
    public float blurValue = 0.5f;

    private void OnEnable()
    {
        blurMaterial = GetComponent<UnityEngine.UI.Image>().material;
    }
    private void Update()
    {
        if (blurMaterial == null) return;

        blurMaterial.SetFloat("_Size", blurValue);
    }

    private void OnDestroy()
    {
        blurValue = 0;
    }
}

