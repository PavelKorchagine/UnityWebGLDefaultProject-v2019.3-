using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPointEnterHandler
{
    void OnPointEnter();
}

public interface IPointExitHandler
{
    void OnPointExit();
}

public interface IPointDownHandler
{
    GameObject OnPointDown(GameObject go);
}

public interface IPointUpHandler
{
    GameObject OnPointUp(GameObject go);
}

public interface IPointClickHandler
{
    void OnPointClick(object par);
}

public interface IPointStayHandler
{
    void OnPointStay(object par);
}

public interface IPointDoubleClickHandler
{
    void OnPointDoubleClick(object par);
}
public interface IPointRightMouseClickHandler
{
    void OnPointRightMouseClick(object par);
}
