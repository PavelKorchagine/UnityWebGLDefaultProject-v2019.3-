using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#region using UNITY_EDITOR
#if UNITY_EDITOR
using UnityEditor;
#endif
#endregion

/* 
**************************************************************************************************************
Copyright/版权 (C) 2020 The arHop Studio. All Rights Reserved. 保留所有权利。
File Name/文件名:           TexturePreprocessor.cs
Discription/描述:     		Be fully careful of  Code modification!
Author/作者:             	Korchagin
CreateTime/创建时间:        2020/6/28 9:40:56
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
/// TexturePreprocessor
/// </summary>
public class TexturePreprocessor : AssetPostprocessor
{
    void OnPostProcessTexture()
    {
        TextureImporter textureImporter = assetImporter as TextureImporter;
        textureImporter.mipmapEnabled = false;
        string path = textureImporter.assetPath;
        Object asset = AssetDatabase.LoadAssetAtPath(path, typeof(Texture2D));
        Texture2D texture = asset as Texture2D;

        if (texture != null)
        {
            Debug.Log("Texture path: " + path);
            texture.wrapMode = TextureWrapMode.Clamp;
            EditorUtility.SetDirty(asset);
        }
        else
        {
            Debug.Log("error " + path);
        }
    }

}

