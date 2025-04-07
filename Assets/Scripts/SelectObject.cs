using System.Runtime.ConstrainedExecution;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class SelectObject : MonoBehaviour
{
    
    private Camera cam;
    public GameObject currentObject;
    public bool isSealBeingMoved;
    private Vector3 returnPosition;
    [SerializeField]
    AudioResource Huh;

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
        
        if (currentObject == null) 
        {
            Cursor.visible = true;
            return;
        }
        if (currentObject.layer == LayerMask.NameToLayer("Seal")) 
        {
            SealMove();
          
            GameObject[] sealBuildings = GameObject.FindGameObjectsWithTag("SealBuilding");
            foreach(GameObject go in sealBuildings) 
            {
                go.AddComponent<Outline>();
                go.GetComponent<Outline>().OutlineColor = Color.green;
            }
        }
        if (Input.GetMouseButtonDown(0) && isSealBeingMoved)
        {
             AudioManager.singleton.PlaySoundListOnce(currentObject,Huh);
        }

    }
    
    void SealMove() 
    {
        isSealBeingMoved = true;  
        
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        Cursor.visible = false;
        currentObject.GetComponent<Tip>().Show();
        
        if(Physics.Raycast(ray, out hit, 100, moveLayer)) 
        {
            Vector3 adjustedPosition = hit.transform.position;
            adjustedPosition.y = adjustedPosition.y + 0.5f;
            currentObject.transform.position = adjustedPosition;
        }
        
        if(Input.GetMouseButtonDown(1)) 
        {
            currentObject.transform.position = returnPosition;
            isSealBeingMoved = false;
            currentObject.GetComponent<Tip>().ShowOver();
            Destroy(currentObject.GetComponent<Outline>());
            currentObject = null;
            Cursor.visible = true;
            GameObject[] sealBuildings = GameObject.FindGameObjectsWithTag("SealBuilding");
            foreach(GameObject go in sealBuildings) 
            {
                Destroy(go.GetComponent<Outline>());
            }
        }
    }

}
