using UnityEngine;
using System.Collections;
using System.IO;
using UnityEditor;

/* 
*********************************************************************
Copyright (C) 2019 The arHop Studio. All Rights Reserved.
 
File Name:           #SCRIPTNAME#.cs
Discription:         Be fully careful of  Code modification!
Author:              Korchagin
CreateTime:          2020/9/10 9:50:24
********************************************************************* 
*/

namespace MFramework
{
    public class FormatScriptAsset : UnityEditor.AssetModificationProcessor
    {
        private static void OnWillCreateAsset(string path)
        {
            path = path.Replace(".meta", "");
            if (path.EndsWith(".cs"))
            {
                string allText = File.ReadAllText(path);

                string Year = System.DateTime.Now.Year.ToString();
                string CompanyName = "arHop Studio";
                string AuthorName = "Korchagin";
                string CreateTime = System.DateTime.Now.Year + "/"
                    + System.DateTime.Now.Month + "/"
                    + System.DateTime.Now.Day + " "
                    + System.DateTime.Now.Hour + ":"
                    + System.DateTime.Now.Minute + ":"
                    + System.DateTime.Now.Second;
                string Discription = "Be fully careful of  Code modification!";

                allText = allText
                    .Replace("arHop Studio", CompanyName)
                    .Replace("2020", Year)
                    .Replace("Be fully careful of  Code modification!", Discription)
                    .Replace("Korchagin", AuthorName)
                    .Replace("2020/9/10 9:50:24", CreateTime);

                File.WriteAllText(path, allText);
            }
            AssetDatabase.Refresh();
        }
    }
}