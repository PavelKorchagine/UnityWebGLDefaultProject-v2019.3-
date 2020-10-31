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
File Name/文件名:           BaseInterateMono.cs
Discription/描述:     		Be fully careful of  Code modification!
Author/作者:             	Korchagin
CreateTime/创建时间:        2020/5/11 14:52:26
Motto/座右铭:
Where there is ambition, everything will be done, and the boat will sink. 
The Pass of the 12th Qin Dynasty belongs to the Chu Dynasty. A painstaking man, who will live up to his fate, 
will live up to his courage, and three thousand Yuejia will swallow up Wu.
																	———— Pu Songling
有志者，事竟成，破釜沉舟，百二秦关终属楚；苦心人，天不负，卧薪尝胆，三千越甲可吞吴。
																	————  蒲松龄
**************************************************************************************************************
*/

namespace MFramework_Unity
{
    [RequireComponent(typeof(Collider))]
    /// <summary>
    /// BaseInterateMono
    /// </summary>
    public class BaseInterateMono : BaseMono, IPointEnterHandler
        , IPointExitHandler, IPointDownHandler, IPointUpHandler, IPointClickHandler
    {
        protected Collider m_Collider;

        public virtual Collider GetCollider()
        {
            return m_Collider;
        }

        public override void OnInit(params object[] args)
        {
            base.OnInit(args);
            m_Collider = gameObject.GetComponentReal<Collider>();
        }

        public override void OnInit()
        {
            base.OnInit();
        }

        public virtual void OnPointEnter()
        {
        }

        public virtual void OnPointExit()
        {
        }

        public virtual GameObject OnPointDown(GameObject go)
        {
            return this.gameObject;
        }

        public virtual GameObject OnPointUp(GameObject go)
        {
            return this.gameObject;
        }

        public virtual void OnPointClick(object para)
        {
        }
    }

}