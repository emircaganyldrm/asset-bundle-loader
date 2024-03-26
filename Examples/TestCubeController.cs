using UnityEngine;

public class TestCubeController : MonoBehaviour
{
    private void Update()
    {
        transform.position =  new Vector3(Mathf.Cos(Time.time), Mathf.Sin(Time.time), transform.position.z);
        transform.Rotate(Vector3.one * Time.deltaTime * 100);
    }
}
