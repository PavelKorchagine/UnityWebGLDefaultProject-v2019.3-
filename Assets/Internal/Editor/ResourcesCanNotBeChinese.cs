/* 
*********************************************************************
Copyright (C) 2019 The Company Name
File Name:           ResourcesCanNotBeChinese.cs
Author:              Korchagin
CreateTime:          2019
********************************************************************* 
*/

using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Text.RegularExpressions;
using System.IO;
using System.Collections.Generic;

public class ResourcesCanNotBeChinese : AssetPostprocessor
{
    public enum CheckState { success, failure }

    /// <summary>
    /// 该方法是由AssetPostprocessor的回调方法
    /// </summary>
    static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets,
    string[] movedAssets, string[] movedFromAssetPaths)
    {
        foreach (var importedAsset in importedAssets)
        {
            Debug.LogWarning("importedAssets:" + importedAsset);
            if (ChekFileIsResourceAndNotChianese(importedAsset) == CheckState.failure)
            {
                //删除文件
                AssetDatabase.DeleteAsset(importedAsset);
                EditorUtility.DisplayDialog("error", "Resource下的文件均不能为中文", "ok");
            }
        }

        for (int i = 0; i < movedAssets.Length; i++)
        {
            if (ChekFileIsResourceAndNotChianese(movedAssets[i]) == CheckState.failure)
            {
                //删除文件
                // AssetDatabase.DeleteAsset(str);
                EditorUtility.DisplayDialog("error", "Resource下的文件均不能为中文", "ok");
                AssetDatabase.MoveAsset(movedAssets[i], movedFromAssetPaths[i]);
            }
        }

        foreach (var str in movedFromAssetPaths)
            Debug.LogWarning("movedFromAssetPaths:" + str);
    }

    /// <summary>
    /// 检查文件是否属于Resources目录，并且文件为中文
    /// </summary>
    public static CheckState ChekFileIsResourceAndNotChianese(string path)
    {

        string fileName = Path.GetFileName(path);
        string filePath = Path.GetDirectoryName(path);
        filePath = Path.GetFileName(filePath);
        if (filePath == "Resources")
        {
            if (Regex.Match(fileName, "^[a-zA-Z\0-9]+$").Success)
            {
                return CheckState.success;
            }
            else
            {
                return CheckState.failure;
            }
        }
        return CheckState.success;
    }
}

