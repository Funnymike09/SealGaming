using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class PixelArtCamera : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private RawImage _rawImage;

    [SerializeField] private int _cameraHeight;
    private RenderTexture _rendertexture;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateRenderTexture();
    }

   public void UpdateRenderTexture()
    {
        if(_rendertexture != null)
        {
            _rendertexture.Release();
        }

        float aspectRatio = (float)Screen.width / Screen.height;
        int cameraWidth = Mathf.RoundToInt(aspectRatio * _cameraHeight);

        _rendertexture = new RenderTexture(cameraWidth, _cameraHeight, 16, RenderTextureFormat.ARGB32);
        _rendertexture.filterMode = FilterMode.Point;

        _rendertexture.Create();
        _camera.targetTexture = _rendertexture;
        _rawImage.texture = _rendertexture;
    }
}
