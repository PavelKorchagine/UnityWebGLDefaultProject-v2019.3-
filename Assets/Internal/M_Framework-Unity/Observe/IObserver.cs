using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public interface IObserver
{
    //void ListenMethod(ObserveEvent observeEvent, long code, object msg);
    void ListenMethod(Type observeType, long code, object msg);
}
