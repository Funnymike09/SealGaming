using Unity.VisualScripting;
using UnityEngine;

public class SelectObject : MonoBehaviour
{
    
    private Camera cam;
    private GameObject currentObject;
    
    [SerializeField] private LayerMask gameLayer;

    void Awake()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0)) 
        {

            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            Debug.Log("fired");

            if (Physics.Raycast(ray, out hit, 100, gameLayer)) 
            {
                
                if(currentObject != null) 
                {
                    Destroy(currentObject.GetComponent<Outline>());
                }

                currentObject = hit.transform.gameObject;
                currentObject.AddComponent<Outline>();

            }

            if(!Physics.Raycast(ray, out hit, 100, gameLayer))
            {
                if (currentObject != null) {
                    Destroy(currentObject.GetComponent<Outline>());
                    currentObject = null;
                }
            }

        }

    }

}
