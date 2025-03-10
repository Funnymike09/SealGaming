using UnityEngine;

public class EnableCameraDepth : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Camera.main.depthTextureMode = DepthTextureMode.Depth;
    }

}
