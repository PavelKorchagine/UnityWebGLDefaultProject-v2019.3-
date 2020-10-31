using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;
using System.IO;

/* 
*********************************************************************
Copyright (C) 2019 The Company Name
File Name:           NewBehaviourScript.cs
Author:              Korchagin
CreateTime:          2019
********************************************************************* 
*/

public static class AssetsBoundleEx
{
    [MenuItem("Assets/Build AssetBudles of WebGL")]
    [MenuItem("AssetsBoundle/Build AssetBudles of WebGL")]
    public static void BuildAllAssetBundles()
    {
        string strDir = "AssetBudles of WebGL";
        if (Directory.Exists(strDir) == false)
        {
            Directory.CreateDirectory(strDir);
        }
        BuildPipeline.BuildAssetBundles(strDir, BuildAssetBundleOptions.ChunkBasedCompression, BuildTarget.WebGL);
    }

    
}