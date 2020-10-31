using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public class ObjectPool<T> where T : Component
{
    // Start is called before the first frame update
    public T t;
    public Queue<T> pool = new Queue<T>();
    /// <summary>
    /// 物体出生点
    /// </summary>
    public Transform bornPointTran;
    
    public ObjectPool(T t, int volume = 5, Transform bornPointTran = null)
    {
        this.t = t;
        this.bornPointTran = bornPointTran;

        for (int i = 0; i < volume; i++)
        {
            pool.Enqueue(GetOne());
        }
    }

    private T GetOne()
    {
        if (bornPointTran == null)
        {
            bornPointTran = ObjectPoolManager.Instance.GetBornPointTran<T>();
        }
        T t = UnityEngine.Object.Instantiate(this.t, bornPointTran);
        t.gameObject.SetActive(false);
        return t;
    }

    /// <summary>
    /// 获得物体
    /// </summary>
    /// <returns></returns>
    public T GetObject()
    {
        if (pool.Count <= 0)
        {
            pool.Enqueue(GetOne());
        }
        return pool.Dequeue();
    }
    /// <summary>
    /// 回收物体
    /// </summary>
    /// <param name="t"></param>
    public void ResumeObject(T t)
    {
        t.gameObject.SetActive(false);
        if (bornPointTran == null)
            bornPointTran = ObjectPoolManager.Instance.GetBornPointTran<T>();
        Transform tar = t.transform;
        tar.SetParent(bornPointTran);
        tar.localPosition = tar.localEulerAngles = Vector3.zero;
        tar.localScale = Vector3.one;
        pool.Enqueue(t);
    }

}
