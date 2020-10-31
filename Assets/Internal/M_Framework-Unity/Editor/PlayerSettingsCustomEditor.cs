using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
public class PlayerSettingsCustomEditor
{
    static ApiCompatibilityLevel apiLevel = ApiCompatibilityLevel.NET_4_6;

    [InitializeOnLoadMethod]
    // Start is called before the first frame update
    static void Start()
    {
        var apiLevel_ori = PlayerSettings.GetApiCompatibilityLevel(EditorUserBuildSettings.selectedBuildTargetGroup);
        //apiLevel = ApiCompatibilityLevel.NET_4_6;
        if (apiLevel_ori != ApiCompatibilityLevel.NET_4_6)
        {
            Debug.LogWarning("PlayerSettings.SetApiCompatibilityLevel:" + apiLevel);
            PlayerSettings.SetApiCompatibilityLevel(EditorUserBuildSettings.selectedBuildTargetGroup, apiLevel);
        }
    }

}
