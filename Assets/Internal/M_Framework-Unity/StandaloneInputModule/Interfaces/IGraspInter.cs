using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/* 
*********************************************************************
Copyright (C) 2019 The Company Name
File Name:           IGraspInter.cs
Author:              Korchagin
CreateTime:          2019
********************************************************************* 
*/

public interface IGraspInter // 握住
{
    GameObject OnGrasp(GameObject go);
}
