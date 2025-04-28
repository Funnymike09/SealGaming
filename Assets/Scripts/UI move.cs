using UnityEngine;

public class UImove : MonoBehaviour
{
    public int Distance;
    private RectTransform RT;

    private void Start()
    {
        RT = gameObject.GetComponent<RectTransform>();
    }

    public void MoveUP()
    {
       RT.position = new Vector3(RT.position.x, RT.position.y + Distance, RT.position.z);
    }
    public void MoveDOWN()
    {
        RT.position = new Vector3(RT.position.x, RT.position.y - Distance, RT.position.z);
    }
}
