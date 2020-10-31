using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
#region UNITY_EDITOR
#if UNITY_EDITOR
using UnityEditor;
#endif
#endregion

/* 
*********************************************************************
Copyright (C) 2019 The Company Name
File Name:           BaseBestHTTPAssetBundle.cs
Author:              Korchagin
CreateTime:          2019
********************************************************************* 
*/

public class BaseBestHTTPAssetBundle
{
#if UNITY_WEBGL && !UNITY_EDITOR
    public string url = "https://besthttp.azurewebsites.net/Content/AssetBundle_v5.html";
#else
    public string url = "https://besthttp.azurewebsites.net/Content/AssetBundle.html";
#endif

    protected string status = "Waiting for user interaction";
    protected AssetBundle cachedBundle;
    protected Texture2D texture;
    protected bool downloading;
    //protected Action<HTTPRequestStates, AssetBundle> callback;

    //public event Action<string> onProgressMsg;

    //public BaseBestHTTPAssetBundle(string url, Action<HTTPRequestStates, AssetBundle> callback)
    //{
    //    this.url = url;
    //    this.callback = callback;
    //}
    //public virtual void OnStart(string url, Action<HTTPRequestStates, AssetBundle> callback)
    //{
    //    this.url = url;
    //    this.callback = callback;
    //    CoroutineTaskManager.Instance.CreateAndExecuteTask(DownloadAssetBundle(url, callback));
    //}

    public virtual void OnStart()
    {
        //CoroutineTaskManager.Instance.CreateAndExecuteTask(DownloadAssetBundle(url, callback));
    }

    public virtual void UnloadBundle()
    {
        if (cachedBundle != null)
        {
            cachedBundle.Unload(true);
            cachedBundle = null;
        }
    }

    public virtual void OnGameObDestroy()
    {
        UnloadBundle();
    }

    //protected virtual IEnumerator OnProgress()
    //{
    //    while (downloading && request != null)
    //    {
    //        if (onProgressMsg != null) onProgressMsg("");
    //        yield return null;
    //    }
    //}
    //private HTTPRequest request;
    //private float process;

    //private void Progress(HTTPRequest originalRequest, int downloaded, int downloadLength)
    //{
    //    process = downloaded * 1.0f / downloadLength; 
    //}

//    protected virtual IEnumerator DownloadAssetBundle(string url, Action<HTTPRequestStates, AssetBundle> callback)
//    {
//        downloading = true;
//        request = new HTTPRequest(new Uri(url)).Send();
//        status = "Download started";
//        if (onProgressMsg != null) onProgressMsg(status);
//        //CoroutineTaskManager.Instance.CreateAndExecuteTask(OnProgress());
//        AssetBundle ab = null;
//        while (request.State < HTTPRequestStates.Finished)
//        {
//            yield return new WaitForSeconds(0.1f);
//            status += ".";
//            if (onProgressMsg != null) onProgressMsg(status);
//        }
//        downloading = false;

//        switch (request.State)
//        {
//            case HTTPRequestStates.Finished:
//                if (request.Response.IsSuccess)
//                {
//#if !BESTHTTP_DISABLE_CACHING && (!UNITY_WEBGL || UNITY_EDITOR)
//                    status = string.Format("AssetBundle downloaded! Loaded from local cache: {0}", request.Response.IsFromCache.ToString());
//#else
//                    status = "AssetBundle downloaded!";
//                    if (onProgressMsg != null) onProgressMsg(status);
//#endif
//                    AssetBundleCreateRequest async =
//#if UNITY_5_3
//                            AssetBundle.LoadFromMemoryAsync(request.Response.Data);
//#else
//                            AssetBundle.LoadFromMemoryAsync(request.Response.Data);
//#endif
//                    yield return async;
//                    ab = async.assetBundle;
//                    //yield return CoroutineTaskManager.Instance.CreateAndExecuteTask(ProcessAssetBundle(async.assetBundle));
//                }
//                else
//                {
//                    status = string.Format("Request finished Successfully, but the server sent an error. Status Code: {0}-{1} Message: {2}",
//                                                    request.Response.StatusCode,
//                                                    request.Response.Message,
//                                                    request.Response.DataAsText);
//                    Debug.LogWarning(status);
//                    if (onProgressMsg != null) onProgressMsg(status);
//                }

//                break;
//            case HTTPRequestStates.Error:
//                status = "Request Finished with Error! " + (request.Exception != null ? (request.Exception.Message + "\n" + request.Exception.StackTrace) : "No Exception");
//                Debug.LogError(status);
//                if (onProgressMsg != null) onProgressMsg(status);
//                break;
//            case HTTPRequestStates.Aborted:
//                status = "Request Aborted!";
//                Debug.LogWarning(status);
//                if (onProgressMsg != null) onProgressMsg(status);
//                break;
//            case HTTPRequestStates.ConnectionTimedOut:
//                status = "Connection Timed Out!";
//                Debug.LogError(status);
//                if (onProgressMsg != null) onProgressMsg(status);
//                break;
//            case HTTPRequestStates.TimedOut:
//                status = "Processing the request Timed Out!";
//                Debug.LogError(status);
//                if (onProgressMsg != null) onProgressMsg(status);
//                break;
//        }

//        if (callback != null) callback(request.State, ab);
//    }

    protected virtual IEnumerator ProcessAssetBundle(AssetBundle bundle)
    {
        if (bundle == null)
            yield break;

        // Save the bundle for future use
        cachedBundle = bundle;

        // Start loading the asset from the bundle
        var asyncAsset =
#if UNITY_5
            cachedBundle.LoadAssetAsync("9443182_orig", typeof(Texture2D));
#else

        cachedBundle.LoadAssetAsync("9443182_orig", typeof(Texture2D));
#endif

        // wait til load
        yield return asyncAsset;

        // get the texture
        texture = asyncAsset.asset as Texture2D;
    }


}
