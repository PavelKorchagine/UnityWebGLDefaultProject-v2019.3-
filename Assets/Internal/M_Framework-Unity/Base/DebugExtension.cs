using System.Collections;
using System.Collections.Generic;
using System.Text;

/* 
**************************************************************************************************************
Copyright/版权 (C) 2020 The arHop Studio. All Rights Reserved. 保留所有权利。
File Name/文件名:           DebugExtension.cs
Discription/描述:     		Be fully careful of  Code modification!
Author/作者:             	Korchagin
CreateTime/创建时间:        2020/6/4 9:41:5
Motto/座右铭:
Where there is ambition, everything will be done, and the boat will sink. 
The Pass of the 12th Qin Dynasty belongs to the Chu Dynasty. A painstaking man, who will live up to his fate, 
will live up to his courage, and three thousand Yuejia will swallow up Wu.
																	———— Pu Songling
有志者，事竟成，破釜沉舟，百二秦关终属楚；苦心人，天不负，卧薪尝胆，三千越甲可吞吴。
																	————  蒲松龄
**************************************************************************************************************
*/

namespace UnityEngine
{
    public class DebugX
    {
        public static string IntervalStr = ", ";
        public static void Log(params object[] args)
        {
            StringBuilder sb = new StringBuilder();
            int le = args.Length;
            for (int i = 0; i < le; i++)
            {
                sb.Append(args[i]);
                if (i < le - 1) sb.Append(IntervalStr);
            }
            Debug.Log(sb.ToString());
        }

    }

    public class M_Debug
    {
        protected string intervalStr = ", ";
        public M_Debug()
        {
        }
        public M_Debug(string intervalStr)
        {
            this.intervalStr = intervalStr;
        }
        public void Log(params object[] args)
        {
            StringBuilder sb = new StringBuilder();
            int le = args.Length;
            for (int i = 0; i < le; i++)
            {
                sb.Append(args[i]);
                if (i < le - 1) sb.Append(intervalStr);
            }
            Debug.Log(sb.ToString());
        }
    }

}