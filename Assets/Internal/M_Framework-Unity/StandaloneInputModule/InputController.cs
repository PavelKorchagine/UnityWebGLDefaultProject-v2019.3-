using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public static InputController singleton;
    public static InputController Instance
    {
        get
        {
            if (singleton == null)
            {
                GameObject go = new GameObject("InputController");
                singleton = go.AddComponent<InputController>();
            }
            return singleton;
        }
    }

    private void Awake()
    {
        singleton = this;
    }

    private void Update()
    {
        if (count > 0)
        {
            count--;
            if (count <= 0)
            {

            }

        }
        
    }

    private bool inputInterval = true;
    private int Count = 30;
    private int count;

    public bool CheckInputInterval()
    {
        if (count == -1) inputInterval = false;
        return inputInterval;
    }
}
