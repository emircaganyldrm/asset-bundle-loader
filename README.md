# Asset Bundle Loader Documentation
Asset Bundle Loader is a package that lets you download any **[unity asset bundle](https://docs.unity3d.com/Manual/AssetBundlesIntro.html)** on the server and implement into the game

## Simple Usage 🚀

 1. Build your asset bundles with Udo/Build Asset Bundles/Build for IOS
    or Android
 2. Upload your asset bundles to your server (you can use github public repos' raw files for testing purposes)
 3. Add AssetBundleLoader.cs singleton to your scene
 4. Fill the url fields for both ios and android that has the asset bundles in the singleton
 5. Now you can use `AssetBundleLoader.Instance.LoadObject<T>(bundlename, assetname, onLoaded)` to load any object.

### LoadObject< T > Generic Method
This method loads given bundle from the server and calls onLoaded action with the loaded asset.

**Example**

    AssetBundleLoader.Instance.LoadObject<RuntimeAnimatorController>(_conceptData.downloadBundleName, _conceptData.downloadAssetName,  
    o =>  
    {  
        _conceptData.downloadedAC = o;  
        SceneController.Instance.LoadScene(_conceptData.sceneBuildIndex, OnConceptSceneLoaded);  
    });

### Caching Bundles
If you want to cache a bundle and load later on you can use `AssetBundleLoader.Instance.LoadBundle(bundleName)` to download the bundle.

Then if you want to use the downloaded bundle just call `LoadObject<>()` method and if cached properly script won't download again and return the asset from the downloaded bundle.

You can check cached bundles by calling `bool IsBundleCached(string bundleName)` from the singleton.

In `AssetBundleLoader.cs` there is a constant string called `_cacheHash` . This string is kind of a cache id for the downloaded content if you want to test caching you can change the string value.
