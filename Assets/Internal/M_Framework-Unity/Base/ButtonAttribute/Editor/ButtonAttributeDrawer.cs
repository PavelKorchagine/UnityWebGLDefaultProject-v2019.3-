// /****************************************************************************
//  * Copyright (c) 2018 ZhongShan KPP Technology Co
//  * Date: 2018-03-13 22:36
//  ****************************************************************************/

using System.Reflection;
using UnityEngine;

namespace UnityEditor
{
    [CustomPropertyDrawer(typeof(ButtonAttribute), true)]
    public class ButtonAttributeDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            ButtonAttribute attribute = (ButtonAttribute)this.attribute;
            if (attribute.showIfRunTime != ButtonAttribute.ShowIfRunTime.All)
            {
                if (attribute.showIfRunTime == ButtonAttribute.ShowIfRunTime.Playing != Application.isPlaying)
                {
                    return -2;
                }
            }

            return attribute.height;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            ButtonAttribute attribute = (ButtonAttribute)this.attribute;

            //看看应该显示按钮的时机
            if (attribute.showIfRunTime != ButtonAttribute.ShowIfRunTime.All)
            {
                if (attribute.showIfRunTime == ButtonAttribute.ShowIfRunTime.Playing != Application.isPlaying)
                {
                    return;
                }
            }

            position.height = attribute.height;
            if (attribute.isMulti)
            {
                if (attribute.funcNames == null || attribute.funcNames.Length == 0)
                {
                    position = EditorGUI.IndentedRect(position);
                    EditorGUI.HelpBox(position, "[Button] funcNames is Not Set!", MessageType.Warning);
                    return;
                }

                float width = position.width/attribute.funcNames.Length;
                position.width = width;
                for (int i = 0; i < attribute.funcNames.Length; i++)
                {
                    string funcName = attribute.funcNames[i];
                    if (GUI.Button(position, funcName))
                    {
                        CalledFunc(property.serializedObject.targetObject, funcName);
                    }

                    position.x += width;
                }
            }
            else
            {
                if (attribute.funcName == null)
                {
                    EditorGUI.HelpBox(position, "[Button] funcName is Not Set!", MessageType.Warning);
                    return;
                }

                if (GUI.Button(position, attribute.funcName))
                {
                    CalledFunc(property.serializedObject.targetObject, attribute.funcName);
                }
            }
        }

        public static void CalledFunc(Object target, string strFuncName)
        {
            //找脚本上的FR函数，编辑器调用
            MethodInfo methodInfo = target.GetType()
                                          .GetMethod(strFuncName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            if (methodInfo == null)
            {
                return;
            }

            //Debug.Log(target + ".<b>" + strFuncName + "()</b> Invoke");
            methodInfo.Invoke(target, null);
        }
    }
}