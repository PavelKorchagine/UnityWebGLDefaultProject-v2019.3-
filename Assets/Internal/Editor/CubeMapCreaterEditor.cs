﻿using UnityEngine;
using UnityEditor;
using System.Collections;

public class CubeMapCreaterEditor : ScriptableWizard
{

    public Transform renderPosition;
    public Cubemap cubemap;

    void OnWizardUpdate()
    {
        helpString = "Select transform to render" + " from and cubemap to render into";
        if (renderPosition != null && cubemap != null)
        {
            isValid = true;
        }
        else
        {
            isValid = false;
        }
    }

    void OnWizardCreate()
    {
        GameObject go = new GameObject("CubeCam", typeof(Camera));

        go.transform.position = renderPosition.position;
        go.transform.rotation = Quaternion.identity;

        go.GetComponent<Camera>().RenderToCubemap(cubemap);

        DestroyImmediate(go);
    }

    [MenuItem("Tools/CubeMapCreater/RenderCubeMap")]
    static void RenderCubemap()
    {
        ScriptableWizard.DisplayWizard("RenderCubeMap", typeof(CubeMapCreaterEditor), "Render!");
    }
}
