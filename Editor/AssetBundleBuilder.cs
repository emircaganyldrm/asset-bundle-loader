using System.IO;
using UnityEditor;

public class AssetBundleBuilder : Editor
{
    [MenuItem("Tools/Build Asset Bundles/Build for IOS")]
    private static void BuildAssetBundlesIOS()
    {
        var assetBundleDirectory = "Assets/StreamingAssets/IOS";
        
        if (!Directory.Exists(assetBundleDirectory))
        {
            Directory.CreateDirectory(assetBundleDirectory);
        }
        
        BuildPipeline.BuildAssetBundles(assetBundleDirectory, BuildAssetBundleOptions.None, BuildTarget.iOS);
        AssetDatabase.Refresh();
    }
    
    [MenuItem("Tools/Build Asset Bundles/Build for Android")]
    private static void BuildAssetBundlesAndroid()
    {
        var assetBundleDirectory = "Assets/StreamingAssets/Android";
        
        if (!Directory.Exists(assetBundleDirectory))
        {
            Directory.CreateDirectory(assetBundleDirectory);
        }
        
        BuildPipeline.BuildAssetBundles(assetBundleDirectory, BuildAssetBundleOptions.None, BuildTarget.Android);
        AssetDatabase.Refresh();
    }
}
