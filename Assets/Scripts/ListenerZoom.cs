using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class ListenerZoom : MonoBehaviour
{
   public Camera Camera;   
    private float targetLZ;
    //private float targetVZ;
    public float zoomMutiplier = 4;
    public float zoomMutiplierV = 1;

    private float minLZ = -2;
    private float maxLZ = 3;
   // private float minVZ = 0.4f;
   // private float maxVZ = 0.6f;
    private float vlocityL = 0;
    //private float vlocityV = 0f;
    public float smoothTime = 0.25f;
    Vector3 Lz;

    AudioSource sourceA;


    private void Start()
    {
      //  sourceA = GetComponent<AudioSource>();
      //  targetVZ = sourceA.volume;
        Lz = transform.position;
        targetLZ = Lz.z;
    }
    private void FixedUpdate()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        float LzNew;
       // float VolNew;
       // targetVZ -= scroll * zoomMutiplier;
        targetLZ -= scroll * zoomMutiplier;
        targetLZ = Mathf.Clamp(targetLZ, minLZ, maxLZ);
      //  targetVZ = Mathf.Clamp(targetVZ, minVZ, maxVZ);
        LzNew = Mathf.SmoothDamp(gameObject.transform.localPosition.z, targetLZ, ref vlocityL, smoothTime);
       // VolNew = Mathf.SmoothDamp(sourceA.volume, targetVZ, ref vlocityV, smoothTime);


        gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y, LzNew);
       // sourceA.volume = VolNew;
        





    }
   
}
