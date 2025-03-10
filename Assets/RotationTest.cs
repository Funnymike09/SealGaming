using UnityEngine;

public class RotationTest : MonoBehaviour
{
    private float rotationSpeed = -10f;

    // Update is called once per frame
    private void Start()
    {
        transform.rotation = Quaternion.identity;  
    }
    void Update()
    {
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0, Space.World);
    }
}
