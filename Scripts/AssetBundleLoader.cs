using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using Object = UnityEngine.Object;

public class AssetBundleLoader : MonoBehaviour
{
    public static AssetBundleLoader Instance { get; private set; }
    
    [SerializeField] private string libraryUrlIOS;
    [SerializeField] private string libraryUrlAndroid;

    private const string _cacheHash = "hireMeYouWontRegret";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void LoadBundle(string bundleName)
    {
        StartCoroutine(CacheCoroutine(bundleName));
    }

    /// <summary>
    /// Loads given bundle from the server and calls onLoaded action with the loaded asset.
    /// </summary>
    /// <param name="bundleName">The AssetBundle's name</param>
    /// <param name="assetName">Asset's file name</param>
    /// <param name="onLoaded">Action that returns the loaded object on load completed</param>
    /// <typeparam name="T">The object type that you want to return</typeparam>
    public void LoadObject<T>(string bundleName, string assetName, Action<T> onLoaded = null) where T : Object
    {
        StartCoroutine(LoadObjectCoroutine<T>(bundleName, assetName, asset => onLoaded?.Invoke(asset)));
    }
    
    private IEnumerator CacheCoroutine(string bundleName)
    {
        string uri = GetURI(bundleName);

        using (UnityWebRequest request = new(uri))
        {
            request.downloadHandler = new DownloadHandlerAssetBundle(uri, Hash128.Parse(_cacheHash), 0);
            yield return request.SendWebRequest();

            Debug.Log($"Downloaded {request.downloadedBytes * 0.000001} MBs from {uri}");
            AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(request);

            if (bundle == null)
            {
                Debug.LogWarning("Couldn't load bundle");
                yield break;
            }

            bundle.Unload(false);
        }
    }

    private IEnumerator LoadObjectCoroutine<T>(string bundleName, string assetName, Action<T> onLoaded = null) where T : Object
    {
        string uri = GetURI(bundleName);

        using (UnityWebRequest request = new(uri,UnityWebRequest.kHttpVerbGET))
        {
            request.downloadHandler = new DownloadHandlerAssetBundle(uri, Hash128.Parse(_cacheHash), 0);
            yield return request.SendWebRequest();

            Debug.Log($"Downloaded {request.downloadedBytes * 0.000001} MBs from {uri}");
            AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(request);

            if (bundle == null)
            {
                Debug.LogWarning("Couldn't load bundle");
                yield break;
            }
                
            T asset = bundle.LoadAsset<T>(assetName);
            onLoaded?.Invoke(asset);
            bundle.Unload(false);
        }
    }

    public bool IsBundleCached(string bundleName)
    {
        return Caching.IsVersionCached(GetURI(bundleName), Hash128.Parse(_cacheHash));
    }

    private string GetURI(string bundleName)
    {
#if UNITY_ANDROID
        return $"{libraryUrlAndroid}/{bundleName}";
#elif UNITY_IOS
        return $"{libraryUrlIOS}/{bundleName}";
#else
        Debug.LogError("Unsupported platform");
        return null;
#endif
    }
}