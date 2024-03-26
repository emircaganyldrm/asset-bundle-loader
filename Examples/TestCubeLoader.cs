using UnityEngine;

public class TestCubeLoader : MonoBehaviour
{
    private void Start()
    {
        AssetBundleLoader.Instance.LoadObject<GameObject>("testbundle", "TestCube", (obj) =>
        {
            var cube = Instantiate(obj);
            cube.AddComponent<TestCubeController>();
        });
    }
}
