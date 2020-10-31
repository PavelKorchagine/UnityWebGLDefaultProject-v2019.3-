using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/* 
*********************************************************************
Copyright (C) 2019 The Company Name
File Name:           IPickupInter.cs
Author:              Korchagin
CreateTime:          2019
********************************************************************* 
*/

public interface IPickupInter // 拿起
{
    GameObject OnPickup(GameObject go);
    GameObject OnUnPickup(GameObject go);
}
