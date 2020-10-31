using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using System.Text;
#region using UNITY_EDITOR
#if UNITY_EDITOR
using UnityEditor;
#endif
#endregion

/* 
**************************************************************************************************************
Copyright/版权 (C) 2020 The arHop Studio. All Rights Reserved. 保留所有权利。
File Name/文件名:           M_TagManagerEditor.cs
Discription/描述:     		Be fully careful of  Code modification!
Author/作者:             	Korchagin
CreateTime/创建时间:        2020/5/8 9:45:7
Motto/座右铭:
Where there is ambition, everything will be done, and the boat will sink. 
The Pass of the 12th Qin Dynasty belongs to the Chu Dynasty. A painstaking man, who will live up to his fate, 
will live up to his courage, and three thousand Yuejia will swallow up Wu.
																	———— Pu Songling
有志者，事竟成，破釜沉舟，百二秦关终属楚；苦心人，天不负，卧薪尝胆，三千越甲可吞吴。
																	————  蒲松龄
**************************************************************************************************************
*/

/// <summary>
/// M_TagManagerEditor
/// </summary>
public class M_AssetPostprocessor : AssetPostprocessor
{

    private static string[] _testLayerArr = { "LayerOne", "LayerTwo", "LayerThree" };
    private static string[] _testTagArr = { "TagOne", "TagTwo", "TagThree" };

    private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
        foreach (string s in importedAssets)
        {
            if (s.Equals("Assets/Test/LayerTest/ExportLayer.cs"))
            {
                foreach (string tag in _testTagArr)
                {
                    AddTag(tag);
                }
                foreach (string layer in _testLayerArr)
                {
                    AddLayer(layer);

                }
                return;
            }
        }
    }

    [MenuItem("Tools/AddDefaultTagLayer")]
    static void AddTagLayer()
    {
        _testTagArr = new string[] {
            "CanvasRoot",
            "DynamicSky",
            "Car",
        };

        Dictionary<int, string> LayerDic = new Dictionary<int, string>() {
            { 0, "Default"}, { 1, "TransparentFX"}, { 2, "Ignore Raycast"}
            , { 4, "Water"}, { 5, "UI"}, { 8, "NULL"}, { 9, "Envir"}
            , { 10, "Ground"}, { 11, "Interact"}, { 12, "RealRayCast"}, { 13, "VirtualRayCast"}
            , { 14, "DynamicSky"}, { 15, "Highlighting"}
        };

        _testLayerArr = new string[] {
            "Default", //0
            "TransparentFX", //1
            "Ignore Raycast", // 2
            "", // 3
            "Water", // 4
            "UI", // 5
            "", //6
            "",// 7
            "NULL", // 8
            "Envir", // 9
            "Ground", // 10
            "Interact", // 11
            "RealRayCast", // 12 真实射线交互层
            "VirtualRayCast", // 13 虚拟射线交互层

        };

        _testLayerArr = new string[32];
        for (int i = 0; i < _testLayerArr.Length; i++)
        {
            _testLayerArr[i] = "";
        }
        foreach (var index in LayerDic.Keys)
        {
            _testLayerArr[index] = LayerDic[index];
        }

        var OriTags = UnityEditorInternal.InternalEditorUtility.tags.ToList();
        foreach (string tag in _testTagArr)
        {
            //AddTag(tag);
            if (!OriTags.Contains(tag))
            {
                //EditorUtility.DisplayDialog("Warning", msg, "OK");
                UnityEditorInternal.InternalEditorUtility.AddTag(tag);
            }
        }
        foreach (string layer in _testLayerArr)
        {
            AddLayer(layer);

        }

        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("工程中层级：");
        foreach (var item in LayerDic)
        {
            stringBuilder.Append(item.Key.ToString());
            stringBuilder.Append(":");
            stringBuilder.Append(item.Value);
            stringBuilder.Append(", ");
        }

        string sbStr = stringBuilder.ToString();

        Debug.Log(sbStr);
        EditorUtility.DisplayDialog("提示：", sbStr, "确认");

    }

    static bool IsHasLayer(string layer)
    {
        SerializedObject tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/Tagmanager.asset"));
        SerializedProperty it = tagManager.GetIterator();
        while (it.NextVisible(true))
        {
            if (it.name == "layers")
            {
                for (int i = 0; i < it.arraySize; i++)
                {
                    SerializedProperty sp = it.GetArrayElementAtIndex(i);
                    if (!string.IsNullOrEmpty(sp.stringValue))
                    {
                        if (sp.stringValue.Equals(layer))
                        {
                            sp.stringValue = string.Empty;
                            tagManager.ApplyModifiedProperties();
                        }
                    }
                }
            }
        }
        for (int i = 0; i < UnityEditorInternal.InternalEditorUtility.layers.Length; i++)
        {
            if (UnityEditorInternal.InternalEditorUtility.layers[i].Contains(layer))
                return true;
        }
        return false;
    }

    static bool IsHasTag(string tag)
    {
        for (int i = 0; i < UnityEditorInternal.InternalEditorUtility.tags.Length; i++)
        {
            if (UnityEditorInternal.InternalEditorUtility.tags[i].Contains(tag))
                return true;
        }
        return false;
    }

    /// <summary>
    /// 添加layer
    /// </summary>
    /// <param name="layer"></param>
    private static void AddLayer(string layer)
    {
        if (!IsHasLayer(layer))
        {
            SerializedObject tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
            SerializedProperty it = tagManager.GetIterator();
            while (it.NextVisible(true))
            {
                if (it.name == "layers")
                {
                    for (int i = 0; i < it.arraySize; i++)
                    {
                        if (i == 3 || i == 6 || i == 7)
                        {
                            continue;
                        }
                        SerializedProperty dataPoint = it.GetArrayElementAtIndex(i);
                        if (string.IsNullOrEmpty(dataPoint.stringValue))
                        {
                            dataPoint.stringValue = layer;
                            tagManager.ApplyModifiedProperties();
                            return;
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// 添加tag
    /// </summary>
    /// <param name="tag"></param>
    private static void AddTag(string tag)
    {
        if (!IsHasTag(tag))
        {
            SerializedObject tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
            SerializedProperty it = tagManager.GetIterator();
            while (it.NextVisible(true))
            {
                if (it.name == "tags")
                {
                    for (int i = 0; i < it.arraySize; i++)
                    {
                        SerializedProperty dataPoint = it.GetArrayElementAtIndex(i);
                        if (string.IsNullOrEmpty(dataPoint.stringValue))
                        {
                            dataPoint.stringValue = tag;
                            tagManager.ApplyModifiedProperties();
                            return;
                        }
                    }
                }
            }
        }
    }
}

