using System.Numerics;
using UnityEngine;
using UnityEngine.Tilemaps;
using Vector3 = UnityEngine.Vector3;


public class CreateBuilding : MonoBehaviour
{
    [Header("Layer")]
    [SerializeField] private LayerMask gameLayer;
    
    private GameObject buildingParent;
    
    public GridInformation gridInfo;

    GameObject tempBuilding;

    void Awake()
    {
        buildingParent = GameObject.Find("BuildingParent");
    }

    public void BuildingCreate(string buildingName) 
    {
        tempBuilding = Instantiate(Resources.Load<GameObject>(GridManager.instance.buildings[buildingName]), Vector3.zero, UnityEngine.Quaternion.identity, buildingParent.transform);
        GridManager.instance.isBuildingBeingPlaced = true;
    }

    void Update()
    {
        if (tempBuilding != null) 
        {
            PlacingBuilding();
            
            if(Input.GetMouseButtonDown(0) && GridManager.instance.CanBuildingBePlaced(tempBuilding.transform.position))
            { 
                GridManager.instance.BuildingPlaced(tempBuilding.transform.position);
                EconomyManager.instance.economyTickEvent.AddListener(tempBuilding.GetComponent<BuildingBase>().ProduceMoney);
                tempBuilding = null;
                GridManager.instance.isBuildingBeingPlaced = false;
            }
            
            if(Input.GetMouseButtonDown(1)) 
            {
                Destroy(tempBuilding);
                tempBuilding = null;
                GridManager.instance.isBuildingBeingPlaced = false;
            }
        }
    }

    void PlacingBuilding() 
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        if(Physics.Raycast(ray, out hit, 100, gameLayer)) 
        {
            Vector3 adjustedPosition = hit.transform.position;
            adjustedPosition.y = adjustedPosition.y + 1;
            tempBuilding.transform.position = adjustedPosition;
        }
    }
}
