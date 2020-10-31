using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInterTrigger
{
    void OnInterTriggerEnter(GameObject go);
    void OnInterTriggerExit(GameObject go);
}
