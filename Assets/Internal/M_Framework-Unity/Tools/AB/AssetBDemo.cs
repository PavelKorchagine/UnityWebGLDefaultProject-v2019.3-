using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/* 
*********************************************************************
Copyright (C) 2019 The Company Name
File Name:           AssetBDemo.cs
Author:              Korchagin
CreateTime:          2019
********************************************************************* 
*/

public class AssetBDemo : MonoBehaviour
{
    public string urlBase = "";
    public string[] reUrls;
    public string[] gameObNames;
    public BaseBestHTTPAssetBundle bsab;
    public bool isStartDownding;
    public List<AssetBundle> assetBundles = new List<AssetBundle>();
    public Transform parent;

    public void StartDown() 
    {
#if UNITY_EDITOR
        urlBase = Application.dataPath + "/../AssetBundles/WebGL/";
        urlBase = "http://112.80.18.202:8899/outPut/StreamingAssets/";
#elif UNITY_WEBGL
        urlBase = Application.streamingAssetsPath + "/";
#else
        urlBase = Application.streamingAssetsPath + "/";
#endif

        //CoroutineTaskManager.Instance.CreateAndExecuteTask(Down());
        
    }

    int reU = -1;

    //private IEnumerator Down()
    //{
    //    if (isDownding || reU > reUrls.Length - 2) yield break;
    //    reU++;
    //    yield return null;
    //    var realUrl = urlBase + reUrls[reU];
    //    BaseBestHTTPAssetBundle bsab = new BaseBestHTTPAssetBundle(realUrl, DownABCallBack);
    //    bsab.onProgressMsg += Bsab_onProgressMsg;
    //    bsab.OnStart();
    //    isDownding = true;
    //    GameFacade.Facade.ShowMsgOnGUI(realUrl.ToString() + " 开始下载。。。。");
    //    yield return new WaitUntil(() => isDownding == false);
    //    GameFacade.Facade.ShowMsgOnGUI(realUrl.ToString() + " 下载并实例完成！");

    //    //AssetBundle bundle = assetBundles[0];
    //    //CoroutineTaskManager.Instance.CreateAndExecuteTask(ProcessAssetBundle(bundle, "Robot"));

    //}

    private void Bsab_onProgressMsg(string progressMsg)
    {
        //GameFacade.Facade.ShowMsgOnGUI(progressMsg);
    }

    private bool isDownding;

    //private void DownABCallBack(HTTPRequestStates states, AssetBundle bundle)
    //{
    //    GameFacade.Facade.ShowMsgOnGUI(states.ToString() + " ....72行");

    //    switch (states)
    //    {
    //        case HTTPRequestStates.Initial:
    //            break;
    //        case HTTPRequestStates.Queued:
    //            break;
    //        case HTTPRequestStates.Processing:
    //            break;
    //        case HTTPRequestStates.Finished:
    //            //assetBundles.Add(bundle);
    //            CoroutineTaskManager.Instance.CreateAndExecuteTask(ProcessAssetBundle(bundle, gameObNames[reU]));
    //            break;
    //        case HTTPRequestStates.Error:
    //            break;
    //        case HTTPRequestStates.Aborted:
    //            break;
    //        case HTTPRequestStates.ConnectionTimedOut:
    //            break;
    //        case HTTPRequestStates.TimedOut:
    //            break;
    //        default:
    //            break;
    //    }
    //}

    private IEnumerator ProcessAssetBundle(AssetBundle bundle, string assetName)
    {
        if (bundle == null)
            yield break;
        var asyncAsset =
#if UNITY_5
        bundle.LoadAssetAsync(assetName, typeof(GameObject));
#else
        bundle.LoadAssetAsync(assetName, typeof(GameObject));
#endif
        yield return asyncAsset;
        GameObject go = asyncAsset.asset as GameObject;
        Instantiate(go, parent);
        bundle.Unload(false);
        yield return new WaitForSeconds(2);

        isDownding = false;
    }
}
