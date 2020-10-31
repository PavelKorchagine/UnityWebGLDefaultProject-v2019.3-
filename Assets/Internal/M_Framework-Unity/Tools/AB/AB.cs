using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Object = UnityEngine.Object;

namespace MFramework_Unity.Tools
{
    public class AB
    {
        public static AssetBundle LoadFromFile(string path, uint crc = 0, ulong offset = 0)
        {
            return AssetBundle.LoadFromFile(path, crc, offset);
        }

        public static void LoadFromFileAsync(string path, out AssetBundle bundle, Action<AsyncOperation> completed = null, uint crc = 0, ulong offset = 0)
        {
            AssetBundleCreateRequest crq = AssetBundle.LoadFromFileAsync(path, crc, offset);
            bundle = crq.assetBundle;
            if (completed != null)
                crq.completed += completed;
        }

        private static void Crq_completed(AsyncOperation obj)
        {

        }
        public static T LoadAsset<T>(AssetBundle bundle, string _name) where T : Component
        {
            return bundle.LoadAsset<T>(_name);
        }
        public static T LoadAssetAsync<T>(AssetBundle bundle, string _name, Action<AsyncOperation> completed = null) where T : Component
        {
            AssetBundleRequest request = bundle.LoadAssetAsync<T>(_name);
            request.completed += completed;
            return request.asset as T;
        }
        public static T[] LoadAllAssets<T>(AssetBundle bundle) where T : Component
        {
            return bundle.LoadAllAssets<T>();
        }
        public static Object[] LoadAllAssets(AssetBundle bundle)
        {
            return bundle.LoadAllAssets();
        }

    }
}
