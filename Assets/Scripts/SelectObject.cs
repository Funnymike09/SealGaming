using Unity.VisualScripting;
using UnityEngine;

public class SelectObject : MonoBehaviour
{
    
    private Camera cam;
    private GameObject currentObject;
    private bool isSealBeingMoved;
    private Vector3 returnPosition;
    
    [SerializeField] private LayerMask gameLayer;
    [SerializeField] private LayerMask moveLayer;

    void Awake()
    {
        cam = Camera.main;
        isSealBeingMoved = false;
    }

    void Update()
    {
        if (GridManager.instance.isBuildingBeingPlaced) return;
    
        if(Input.GetMouseButtonDown(0) && !isSealBeingMoved) 
        {

            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, gameLayer)) 
            {
                
                if(currentObject != null) 
                {
                    Destroy(currentObject.GetComponent<Outline>());
                }

                currentObject = hit.transform.gameObject;
                returnPosition = hit.transform.position;
                if(currentObject.GetComponent<Outline>() == null) 
                {
                    currentObject.AddComponent<Outline>();
                }
            }

            if(!Physics.Raycast(ray, out hit, 100, gameLayer))
            {
                if (currentObject != null) 
                {
                    Destroy(currentObject.GetComponent<Outline>());
                    currentObject = null;
                }
            }
            
            if (GridManager.instance.isBuildingBeingPlaced == true) 
            {
                Destroy(currentObject.GetComponent<Outline>());
                currentObject = null;
            }
            
            

        }
        
        if (currentObject == null) return;
        if(currentObject.layer == LayerMask.NameToLayer("Seal")) 
        {
            SealMove();
        }
    }
    
    void SealMove() 
    {
        isSealBeingMoved = true;    
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        Cursor.visible = false;
        
        if(Physics.Raycast(ray, out hit, 100, gameLayer)) 
        {
            Vector3 adjustedPosition = hit.transform.position;
            adjustedPosition.y = adjustedPosition.y + 0.5f;
            currentObject.transform.position = adjustedPosition;
        }
        
        if(Input.GetMouseButtonDown(1)) 
        {
            currentObject.transform.position = returnPosition;
            isSealBeingMoved = false;
            Destroy(currentObject.GetComponent<Outline>());
            currentObject = null;
            Cursor.visible = true;
        }
    }

}
