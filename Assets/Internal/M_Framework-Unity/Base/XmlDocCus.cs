using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Xml;
using System.IO;

/* 
*********************************************************************
Copyright (C) 2019 The Company Name
File Name:           XmlDocCus.cs
Author:              Korchagin
CreateTime:          2019
********************************************************************* 
*/

public class XmlDocCus
{
    private XmlElement element;

    public XmlDocCus(string xmlPath)
    {
        if (!File.Exists(xmlPath))
        {
            Debug.LogError(xmlPath + " 不存在");
            return;
        }

        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.Load(xmlPath);
        element = xmlDoc.DocumentElement;
    }
    public XmlDocCus(XmlElement xmle)
    {
        element = xmle;
    }
    public XmlDocCus(XmlNode xmle)
    {
        element = xmle as XmlElement;
    }
    public XmlDocCus(XmlDocument xml)
    {
        element = xml.DocumentElement;
    }
    public XmlData GetValue(string str)
    {
        XmlData xmlData = new XmlData(false, -99, null);

        if (element == null) return xmlData;

        XmlNode node = element.SelectSingleNode(str);
        if (node != null && !string.IsNullOrEmpty(node.InnerText))
        {
            bool.TryParse(node.InnerText, out xmlData.Bool);//bool.Parse 会报异常 "true/false"
            int.TryParse(node.InnerText, out xmlData.Int);
            xmlData.Str = node.InnerText;
        }
        return xmlData;
    }
    public string Get(string str)
    {
        if (element == null) return "";
        XmlNode node = element.SelectSingleNode(str);
        if (node != null && !string.IsNullOrEmpty(node.InnerText))
        {
            return node.InnerText;
        }
        return "";
    }
    public T GetValue<T>(string str)
    {
        Type tp = typeof(T);
        if (element == null) return default(T);

        XmlNode node = element.SelectSingleNode(str);
        if (node != null && !string.IsNullOrEmpty(node.InnerText))
        {
            try
            {
                if (tp == typeof(bool))
                {
                    return (T)(bool.Parse(node.InnerText) as object);//bool.Parse 会报异常 "true/false"
                }
                else if (tp == typeof(int))
                {
                    return (T)(int.Parse(node.InnerText) as object);//bool.Parse 会报异常 "true/false"
                }
                else if (tp == typeof(float))
                {
                    return (T)(float.Parse(node.InnerText) as object);//bool.Parse 会报异常 "true/false"
                }
                else if (tp == typeof(Enum) || tp.IsEnum)
                {
                    return (T)Enum.Parse(typeof(T), node.InnerText);
                }
                else if (tp == typeof(string))
                {
                    return (T)(node.InnerText as object);
                }
            }
            catch (System.Exception e)
            {
                Debug.LogWarningFormat(" - 在 Xml 信息转换状态时发生错误 - {0} - over ", e.ToString());
            }
        }
        return default(T);
    }
    public T GetValue<T>(T t, string str)
    {
        Type tp = typeof(T);
        if (element == null) return default(T);

        XmlNode node = element.SelectSingleNode(str);
        if (node != null && !string.IsNullOrEmpty(node.InnerText))
        {
            try
            {
                if (tp == typeof(bool))
                {
                    return (T)(bool.Parse(node.InnerText) as object);//bool.Parse 会报异常 "true/false"
                }
                else if (tp == typeof(int))
                {
                    return (T)(int.Parse(node.InnerText) as object);//bool.Parse 会报异常 "true/false"
                }
                else if (tp == typeof(Enum) || tp.IsEnum)
                {
                    return (T)Enum.Parse(typeof(T), node.InnerText);
                }
                else if (tp == typeof(string))
                {
                    return (T)(node.InnerText as object);
                }
            }
            catch (System.Exception e)
            {
                Debug.LogWarningFormat(" - 在 Xml 信息转换状态时发生错误 - {0} - over ", e.ToString());
            }
        }
        return default(T);
    }

    //public GlobalDisPlayMode Get(GlobalDisPlayMode t, string str)
    //{
    //    GlobalDisPlayMode global = GlobalDisPlayMode.FollewPlayer;

    //    if (element == null) return global;

    //    XmlNode node = element.SelectSingleNode(str);
    //    if (node != null && !string.IsNullOrEmpty(node.InnerText))
    //    {
    //        try
    //        {
    //            global = (GlobalDisPlayMode)Enum.Parse(typeof(GlobalDisPlayMode), str);
    //            return global;
    //        }
    //        catch (System.Exception e)
    //        {
    //            Debug.LogWarning(e);
    //        }
    //    }
    //    return global;
    //}

    public T Get<T>(string str)
    {
        T global = default(T);

        if (element == null) return global;

        XmlNode node = element.SelectSingleNode(str);
        if (node != null && !string.IsNullOrEmpty(node.InnerText))
        {
            try
            {
                global = (T)Enum.Parse(typeof(T), node.InnerText);
                return global;
            }
            catch (System.Exception e)
            {
                Debug.LogWarningFormat(" - 在 Xml 信息转换枚举时发生错误 - {0} - over ", e.ToString());
            }
        }
        return global;
    }
}
public struct XmlData
{
    public bool Bool;
    public int Int;
    public string Str;

    public XmlData(bool Bool, int Int, string Str)
    {
        this.Bool = Bool;
        this.Int = Int;
        this.Str = Str;
    }
}
