using MFramework_Unity.Tools;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class ABGreaterDemo : ABBaseFactory
{
    public override void OnInit(params object[] args)
    {
    }
    public override void OnInit()
    {
#if UNITY_EDITOR
        //Debug.Log("OnInit");
        //OnEditor();
#elif UNITY_WEBGL
        //HelloString("OnInit");
        //Console("OnInit");
        //OnWebGl();
#endif

    }

    private void OnEditor()
    {
        string path = Application.dataPath + "/../AssetBundles/WebGL/exenvir";
        if (!File.Exists(path))
        {
            Debug.Log(path + " 文件不存在");
            return;
        }
        else
        {
            Debug.Log(path + " 文件存在");
        }

        AssetBundle myLoadedAssetBundle = AssetBundle.LoadFromFile(path);

        var prefab = myLoadedAssetBundle.LoadAsset<GameObject>("ExEnvir");
        //实例化
        //卸载包中资源的内存
        myLoadedAssetBundle.Unload(false);

        path = Application.dataPath + "/../AssetBundles/WebGL/exinter";
        if (!File.Exists(path))
        {
            Debug.Log(path + " 文件不存在");
            return;
        }
        else
        {
            Debug.Log(path + " 文件存在");
        }

        myLoadedAssetBundle = AssetBundle.LoadFromFile(path);
        prefab = myLoadedAssetBundle.LoadAsset<GameObject>("ExInter");
        //实例化
        //卸载包中资源的内存
        myLoadedAssetBundle.Unload(false);
    }

    private void OnWebGl()
    {
#if UNITY_WEBGL
        //HelloString(" StartCoroutine UnityWebDownAB");
        //Console(" StartCoroutine UnityWebDownAB");
#endif
        Debug.Log(" StartCoroutine WWWDownAB");
    }
    private AssetBundle bundle;
    private UnityWebRequest request;
    private bool isStartDown;
    private WWW www;

    protected override void OnUpdate()
    {
        base.OnUpdate();
        if (isStartDown && www != null)
        {
        }
    }

    private IEnumerator UnityWebDownAB()
    {
        //texture是一个文件夹，里面放了多张图片
        string uri = Application.streamingAssetsPath + "/exenvir";
        yield return new WaitForSeconds(1);

        yield return null;

        //request = UnityWebRequestAssetBundle.GetAssetBundle(uri, 0);
        isStartDown = true;
        yield return request.SendWebRequest();
        if (!string.IsNullOrEmpty(request.error))
        {
            Debug.Log("URL:" + uri + " ERROR:" + request.error);
#if UNITY_WEBGL
            //HelloString("URL:" + uri + " ERROR:" + request.error);
            //gamefacade.ShowMsgOnGUI("URL:" + uri + " ERROR:" + request.error);
#endif
            yield break;
        }
        isStartDown = false;
        bundle = DownloadHandlerAssetBundle.GetContent(request);
        InstantiatePrefab(LoadAssetBundle("ExEnvir"));

//        uri = Application.streamingAssetsPath + "/exinter";
//        request = UnityWebRequest.GetAssetBundle(uri, 0);
//        yield return request.SendWebRequest();
//        if (!string.IsNullOrEmpty(request.error))
//        {
//            Debug.Log("URL:" + uri + " ERROR:" + request.error);
//            gamefacade.ShowMsgOnGUI("URL:" + uri + " ERROR:" + request.error);
//#if UNITY_WEBGL
//            HelloString("URL:" + uri + " ERROR:" + request.error);
//            gamefacade.ShowMsgOnGUI("URL:" + uri + " ERROR:" + request.error);
//#endif
//            yield break;
//        }
//        bundle = DownloadHandlerAssetBundle.GetContent(request);
//        InstantiatePrefab(LoadAssetBundle("ExInter"));
    }

    private IEnumerator WWWDownAB()
    {
        //texture是一个文件夹，里面放了多张图片
        string uri = Application.streamingAssetsPath + "/exenvir.unity3d";
        yield return new WaitForSeconds(1);

        yield return null;

        www = new WWW(uri);
        isStartDown = true;
        yield return www;

        if (!string.IsNullOrEmpty(www.error))
        {
            Debug.LogError("URL:" + uri + " ERROR:" + www.error);
            //yield break;
        }
        isStartDown = false;
        bundle = www.assetBundle;
        InstantiatePrefab(LoadAssetBundle("ExEnvir"));

        //uri = Application.streamingAssetsPath + "/exinter";
        //www = new WWW(uri);
        //yield return www;
        //if (!string.IsNullOrEmpty(www.error))
        //{
        //    Debug.LogError("URL:" + uri + " ERROR:" + www.error);
        //    gamefacade.ShowMsgOnGUI("URL:" + uri + " ERROR:" + www.error);
        //    yield break;
        //}
        //bundle = www.assetBundle;

        //InstantiatePrefab(LoadAssetBundle("ExInter"));
    }

    private void InstantiatePrefab(GameObject prefab)
    {
        //Console(" InstantiatePrefab  " + prefab);
        //卸载包中资源的内存
        bundle.Unload(false);
    }

    private GameObject LoadAssetBundle(string _name)
    {
       return bundle.LoadAsset<GameObject>(_name);
    }

    protected override void OnGODestroy()
    {
        base.OnGODestroy();
        Debug.Log("OnObjDestroy");

    }
}
