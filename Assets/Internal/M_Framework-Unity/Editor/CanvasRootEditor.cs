using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
#region using UNITY_EDITOR
#if UNITY_EDITOR
using UnityEditor;
#endif
#endregion

/* 
**************************************************************************************************************
Copyright/版权 (C) 2020 The arHop Studio. All Rights Reserved. 保留所有权利。
File Name/文件名:           CanvasRoot.cs
Discription/描述:     		Be fully careful of  Code modification!
Author/作者:             	Korchagin
CreateTime/创建时间:        2020/5/6 16:51:29
Motto/座右铭:
Where there is ambition, everything will be done, and the boat will sink. 
The Pass of the 12th Qin Dynasty belongs to the Chu Dynasty. A painstaking man, who will live up to his fate, 
will live up to his courage, and three thousand Yuejia will swallow up Wu.
																	———— Pu Songling
有志者，事竟成，破釜沉舟，百二秦关终属楚；苦心人，天不负，卧薪尝胆，三千越甲可吞吴。
																	————  蒲松龄
**************************************************************************************************************
*/

[InitializeOnLoad]
/// <summary>
/// CanvasRoot
/// </summary>
public class CanvasRootEditor
{
#if UNITY_EDITOR
    static string m_Tag = "CanvasRoot";

    [InitializeOnLoadMethod]
    // Start is called before the first frame update
    static void Start()
    {

        try
        {
            RefreshTag();
        }
        catch (Exception)
        {

        }
    }

#endif

    private static void RefreshTag()
    {
#if UNITY_EDITOR

        var tags = UnityEditorInternal.InternalEditorUtility.tags.ToList();
        if (!tags.Contains(m_Tag))
        {
            string msg = string.Format("工程：{0}， Tags列表中不包含Tag:{1}, 将添加！", Application.identifier, m_Tag);
            Debug.LogWarningFormat(msg);
            EditorUtility.DisplayDialog("Warning", msg, "OK");

            UnityEditorInternal.InternalEditorUtility.AddTag(m_Tag);
        }

        #region UnityEditorInternal layers
        var layers = UnityEditorInternal.InternalEditorUtility.layers.ToList();

        foreach (var item in layers)
        {
        }
        if (!layers.Contains("Envir")) // 环境
        {
            //UnityEditorInternal.InternalEditorUtility.la

        }
        if (!layers.Contains("Ground")) // 地面
        {

        }
        if (!layers.Contains("Interact")) // 交互
        {

        }
        #endregion

#endif

        string currentActiveScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        GameObject canvasRoot = GameObject.FindGameObjectWithTag(m_Tag);
        GameObject canvasRootPrefab = null;
        if (canvasRoot == null)
        {
            canvasRoot = GameObject.Find(m_Tag);
        }

        if (canvasRoot == null)
        {
            var crs = Resources.FindObjectsOfTypeAll<CanvasRoot>();
            foreach (var item in crs)
            {
                item.tag = m_Tag;
            }
            var crsL = new List<CanvasRoot>();
            foreach (var item in crs)
            {
                if (string.IsNullOrEmpty(item.gameObject.scene.name))
                {
                    canvasRootPrefab = item.gameObject;
                    break;
                }
            }

            foreach (var item in crs)
            {
                if (item.gameObject.scene.name == currentActiveScene)
                {
                    canvasRoot = item.gameObject;
                    break;
                }
            }
        }


        if (canvasRoot == null && canvasRootPrefab != null)
        {
            canvasRoot = GameObject.Instantiate(canvasRootPrefab);
            canvasRoot.transform.position = canvasRoot.transform.eulerAngles = Vector3.zero;
            canvasRoot.transform.localScale = Vector3.one;
        }

        if (canvasRoot != null)
        {
            //Debug.Log("成功找到CanvasRoot物体：" + canvasRoot);
        }

        if (canvasRoot != null)
        {
            if (canvasRoot.gameObject.tag != m_Tag)
            {
                canvasRoot.gameObject.tag = m_Tag;
                Debug.Log("成功设置CanvasRoot物体的标签：" + m_Tag);

            }
        }

    }

}

