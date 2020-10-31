// /****************************************************************************
//  * Copyright (c) 2018 ZhongShan KPP Technology Co
//  * Date: 2018-03-14 20:39
//  ****************************************************************************/

using System;

namespace UnityEngine
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
    public class ButtonAttribute : PropertyAttribute
    {
        public enum ShowIfRunTime
        {
            All,
            Playing,
            Editing,
        }

        public readonly float height = 16;
        public readonly bool isMulti;
        public readonly string funcName = null;
        public readonly string[] funcNames = null;
        public readonly ShowIfRunTime showIfRunTime = ShowIfRunTime.All;

        public ButtonAttribute(string funcName)
        {
            this.funcName = funcName;
        }

        public ButtonAttribute(ShowIfRunTime showIfRunTime, float height = 16, params string[] funcNames)
        {
            isMulti = true;
            this.height = height;
            this.showIfRunTime = showIfRunTime;
            this.funcNames = funcNames;
        }

        public ButtonAttribute(params string[] funcNames)
        {
            isMulti = true;
            this.funcNames = funcNames;
        }

        public ButtonAttribute(string funcName, ShowIfRunTime showIfRunTime = ShowIfRunTime.All, float height = 16)
        {
            this.height = height;
            this.showIfRunTime = showIfRunTime;
            this.funcName = funcName;
        }
    }
}