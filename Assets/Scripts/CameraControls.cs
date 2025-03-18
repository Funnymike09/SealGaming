using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class CameraControls : MonoBehaviour
{
    private Vector3 _origin;
    private Vector3 _difference;
    private Vector3 _resetCamera;
    private Camera cam;

    private bool _drag = false;

    [SerializeField] private float scale;

    private void Start()
    {
        cam = Camera.main;
        _resetCamera = cam.transform.position;
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

        cam.orthographicSize += Input.mouseScrollDelta.y * scale * -1;
        cam.orthographicSize = Mathf.Clamp(cam.orthographicSize, 1, 10);

    }


}