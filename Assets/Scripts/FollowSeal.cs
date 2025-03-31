using UnityEngine;

public class FollowSeal : MonoBehaviour
{
    [SerializeField]
    private Transform Target;
    [SerializeField]
    private Vector3 Offset;

    private void Update()
    {
        transform.position = Target.position + Offset;
    }
}
