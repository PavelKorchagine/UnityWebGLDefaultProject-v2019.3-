using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BuildSettingEditor
{
    private const string MENU_ROOT = "Tools/BuildSetting/";

    [MenuItem(MENU_ROOT + "TransferWebPlayer")]
    private static void WebPlayerPlatformBuild()
    {
        PlayerSettings.defaultIsFullScreen = false;
        PlayerSettings.defaultScreenWidth = 960; 
        PlayerSettings.defaultWebScreenHeight = 600; 
        PlayerSettings.runInBackground = true;
        PlayerSettings.displayResolutionDialog = ResolutionDialogSetting.Disabled;
        PlayerSettings.visibleInBackground = true;
        PlayerSettings.allowFullscreenSwitch = true;

        //SplashScreen

        //OtherSettings
        PlayerSettings.MTRendering = true;
        PlayerSettings.defaultInterfaceOrientation = UIOrientation.LandscapeLeft;

        //UnityEditor.Android.
        PlayerSettings.companyName = "arhiphop";
        PlayerSettings.productName = "747发动机小游戏";
        PlayerSettings.Android.androidTVCompatibility = false;
        PlayerSettings.Android.androidIsGame = false;
        QualitySettings.vSyncCount = 0;
    }
}
