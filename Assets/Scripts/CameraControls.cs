using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class CameraControls : MonoBehaviour
{
    private Vector3 _origin;
    private Vector3 _difference;
    private Vector3 _resetCamera;
    private Camera cam;

    private float zoom;
    public float zoomMutiplier = 4;
    private float minZ = 2f;
    private float maxZ = 10f;
    private float vlocity = 0f;
    public float smoothTime = 0.25f;


    private bool _drag = false;

    [SerializeField] private float scale;

    private void Start()
    {
        cam = Camera.main;
        _resetCamera = cam.transform.position;
        zoom = cam.orthographicSize;
    }

    private void Update()
    {
        if (Input.GetMouseButton(2) )
        {
            _difference = cam.ScreenToWorldPoint(Input.mousePosition) - cam.transform.position;
            if (_drag == false)
            {
                _drag = true;
                _origin = cam.ScreenToWorldPoint(Input.mousePosition);
            }
        }
        else
        {
            _drag = false;
        }

        if (_drag)
        {
            cam.transform.position = _origin - _difference;
        }
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        zoom -= scroll * zoomMutiplier;
        zoom = Mathf.Clamp(zoom, minZ, maxZ);
        cam.orthographicSize = Mathf.SmoothDamp(cam.orthographicSize, zoom, ref vlocity, smoothTime);
        //cam.orthographicSize += Input.mouseScrollDelta.y * scale * -1;
       // cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, 1, 10);

    }


}