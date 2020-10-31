#region using ReferenceLibrary(.dll)
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using MFramework_Unity.Tools;
#region using UNITY_EDITOR
#if UNITY_EDITOR
using UnityEditor;
using System.Linq;
#endif
#endregion
#endregion

/* 
**************************************************************************************************************
Copyright/版权 (C) 2020 The arHop Studio. All Rights Reserved. 保留所有权利。
File Name/文件名:           NewtonJsonModule.cs
Discription/描述:     		Be fully careful of  Code modification!
Author/作者:             	Korchagin
CreateTime/创建时间:        2020/8/19 16:45:41
**************************************************************************************************************
*/

/// <summary>
/// NewtonJsonModule
/// </summary>
public class NewtonJsonModule
{
    public static string SerializeObject(object tar, bool isFormat = false)
    {
        if (isFormat)
        {
            return (JsonConvert.SerializeObject(tar)).ToFormatJsonString();
        }
        else
        {
            return JsonConvert.SerializeObject(tar);
        }
    }

    public static T DeserializeObject<T>(string data)
    {
        return JsonConvert.DeserializeObject<T>(data);
    }
  
	
}

/* 
**************************************************************************************************************
Motto/座右铭:
Where there is ambition, everything will be done, and the boat will sink. 
The Pass of the 12th Qin Dynasty belongs to the Chu Dynasty. A painstaking man, who will live up to his fate, 
will live up to his courage, and three thousand Yuejia will swallow up Wu.
																	———— Pu Songling
有志者，事竟成，破釜沉舟，百二秦关终属楚；苦心人，天不负，卧薪尝胆，三千越甲可吞吴。
																	————  蒲松龄
**************************************************************************************************************
*/
