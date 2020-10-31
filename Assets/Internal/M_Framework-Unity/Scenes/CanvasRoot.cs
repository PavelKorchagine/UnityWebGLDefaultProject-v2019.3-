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

/// <summary>
/// CanvasRoot
/// </summary>
public class CanvasRoot : MonoBehaviour
{
    static string m_Tag = "CanvasRoot";

    // Start is called before the first frame update
    private void Start()
    {
        FindObjectOfType<CanvasRoot>();
    }
    private static void RefreshTag()
    {
       
    }

    #region CustomEditor(typeof(CanvasRoot)
#if UNITY_EDITOR

    private bool _showParameter = true;

    [CanEditMultipleObjects]
    [CustomEditor(typeof(CanvasRoot))]
    public class CanvasRootEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            CanvasRoot manager = (CanvasRoot)target;
            GUI.changed = false;
            serializedObject.Update();
            //GUI style 设置
            GUIStyle firstLevelStyle = new GUIStyle(GUI.skin.label);
            firstLevelStyle.alignment = TextAnchor.UpperLeft;
            firstLevelStyle.fontStyle = FontStyle.Normal;
            firstLevelStyle.fontSize = 11;
            firstLevelStyle.wordWrap = true;

            GUIStyle boxStyle = new GUIStyle(GUI.skin.box);
            boxStyle.normal.textColor = GUI.skin.label.normal.textColor;
            boxStyle.fontStyle = FontStyle.Bold;
            boxStyle.alignment = TextAnchor.UpperLeft;

            #region ShowParameter
            GUILayout.BeginVertical("", boxStyle);
            manager._showParameter = EditorGUILayout.BeginToggleGroup(string.Format("ShowParameter"), manager._showParameter);
            EditorGUILayout.EndToggleGroup();
            GUILayout.EndVertical();

            if (manager._showParameter)
            {
                GUILayout.BeginVertical("", boxStyle);
                base.OnInspectorGUI();
                //GUILayout.BeginHorizontal("", boxStyle);
                //GUILayout.EndHorizontal();
                GUILayout.EndVertical();
            }
            #endregion

        }
    }
#endif
    #endregion

}

