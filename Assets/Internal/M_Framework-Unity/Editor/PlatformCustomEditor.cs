using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEditor.Callbacks;
using System;
using System.Linq;
using System.Text;

public class PlatformCustomEditor
{
    static string serverSym = "SERVER";
    static string clientSym = "CLIENT";

    [MenuItem("Tools/Net/ChagePlatformToServer")]
    public static void ChagePlatformScriptingDefineSymbolsToServer()
    {
        ChagePlatformScriptingDefineSymbols(clientSym, serverSym);
    }

    [MenuItem("Tools/Net/ChagePlatformToClient")]
    public static void ChagePlatformScriptingDefineSymbolsToClient()
    {
        ChagePlatformScriptingDefineSymbols(serverSym, clientSym);
    }

    [MenuItem("Tools/Net/ClearScriptingDefineSymbols")]
    public static void ClearScriptingDefineSymbols()
    {
        PlayerSettings.SetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup, "");
    }

    static void ChagePlatformScriptingDefineSymbols(string re, string ne)
    {
        string symbol = PlayerSettings.GetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup);
        List<string> syms = symbol.Split(';').ToList();

        if (!syms.Contains(ne))
        {
            syms.Add(ne);
        }
        if (syms.Contains(re))
        {
            syms.Remove(re);
        }

        symbol = ConcatByChar(syms.ToArray(), ';');

        PlayerSettings.SetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup, symbol);
    }

    static string ConcatByChar(string[] target, char ch)
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < target.Length; i++)
        {
            sb.Append(target[i]);
            if (i != target.Length - 1)
            {
                sb.Append(ch);
            }
        }
        return sb.ToString();
    }

    /// <summary>
    /// 获取当前打包平台
    /// </summary>
    /// <returns></returns>
    static BuildTarget GetSelectedBuildTarget()
    {
        switch (EditorUserBuildSettings.selectedBuildTargetGroup)
        {
#if UNITY_4 || UNITY_5_0 || UNITY_5_1 || UNITY_5_2 || UNITY_5_3
             case BuildTargetGroup.WebPlayer:
                if (EditorUserBuildSettings.webPlayerStreamed)
                    return BuildTarget.WebPlayerStreamed;
                return BuildTarget.WebPlayer;
#endif
            case BuildTargetGroup.Standalone:
                return EditorUserBuildSettings.selectedStandaloneTarget;

            case BuildTargetGroup.iOS:
                return BuildTarget.iOS;

            case BuildTargetGroup.Android:
                return BuildTarget.Android;

            case BuildTargetGroup.tvOS:
                return BuildTarget.tvOS;
#if UNITY_4 || UNITY_5 || UNITY_2017_0 || UNITY_2017_1 || UNITY_2017_2
            case BuildTargetGroup.Tizen:
                return BuildTarget.Tizen;
#endif
#if UNITY_4 || UNITY_5_0 || UNITY_5_1 || UNITY_5_2
             case BuildTargetGroup.XBOX360:
                return BuildTarget.XBOX360;
#endif
            case BuildTargetGroup.XboxOne:
                return BuildTarget.XboxOne;

#if UNITY_4 || UNITY_5_0 || UNITY_5_1 || UNITY_5_2 || UNITY_5_3 || UNITY_5_4 || UNITY_5_5
              case BuildTargetGroup.PS3:
                return BuildTarget.PS3;
#endif
#if UNITY_4 || UNITY_5 || UNITY_2017 || UNITY_2018_0 || UNITY_2018_1 || UNITY_2018_2
            case BuildTargetGroup.PSP2:
                return BuildTarget.PSP2;
#endif
            case BuildTargetGroup.PS4:
                return BuildTarget.PS4;

            case BuildTargetGroup.WSA:
                return BuildTarget.WSAPlayer;

            case BuildTargetGroup.WebGL:
                return BuildTarget.WebGL;

#if UNITY_4 || UNITY_5 || UNITY_2017_0 || UNITY_2017_1 || UNITY_2017_2
            case BuildTargetGroup.SamsungTV:
                return BuildTarget.SamsungTV;
#endif
            default:
                return EditorUserBuildSettings.activeBuildTarget;
        }
    }

    /// <summary>
    /// 目录合并
    /// </summary>
    /// <param name="sourceDirName"></param>
    /// <param name="destDirName"></param>
    static void DirectoryMerge(string sourceDirName, string destDirName)
    {
        if (!Directory.Exists(sourceDirName))
            return;

        const string title = "Hold On";

        string info = "Delete files from dest not in source";
        float progress = 0;

        if (Directory.Exists(destDirName))
        {
            var destFiles = Directory.GetFiles(destDirName, "*.*", SearchOption.AllDirectories);
            foreach (var file in destFiles)
            {
                if (!File.Exists(sourceDirName + file.Remove(0, destDirName.Length)))
                    File.Delete(file);

                progress += 1.0f / destFiles.Length;
                EditorUtility.DisplayProgressBar(title, info, progress);
            }
        }

        info = "Copy changed and new files from source to dest";
        progress = 0;

        // Then copy changed and new files from source to dest
        var sourceFiles = Directory.GetFiles(sourceDirName, "*.*", SearchOption.AllDirectories);
        foreach (var file in sourceFiles)
        {
            var destFile = destDirName + file.Remove(0, sourceDirName.Length);

            progress += 1.0f / sourceFiles.Length;
            EditorUtility.DisplayProgressBar(title, info, progress);

            if (File.Exists(destFile) && File.GetLastWriteTimeUtc(file) == File.GetLastWriteTimeUtc(destFile) && new FileInfo(file).Length == new FileInfo(destFile).Length)
                continue;

            Directory.CreateDirectory(Path.GetDirectoryName(destFile));

            File.Copy(file, destFile, true);
            File.SetCreationTime(destFile, File.GetCreationTime(file));
            File.SetLastAccessTime(destFile, File.GetLastAccessTime(file));
            File.SetLastWriteTime(destFile, File.GetLastWriteTime(file));
        }

        EditorUtility.ClearProgressBar();
    }

}
