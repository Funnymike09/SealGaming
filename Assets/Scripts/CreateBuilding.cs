using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using Vector3 = UnityEngine.Vector3;


public class CreateBuilding : MonoBehaviour
{
    [Header("Layer")]
    [SerializeField] private LayerMask gameLayer;
    
    private GameObject buildingParent;
    
    public GridInformation gridInfo;
    [SerializeField]
    AudioClip Place;

    GameObject tempBuilding;
    BuildingBase tempBuildingInfo;
    
    public bool sealCentrePlaced;

    void Awake()
    {
        buildingParent = GameObject.Find("BuildingParent");
    }

    public void BuildingCreate(string buildingName) 
    {
       
        if (EconomyManager.instance.currentMoney - Resources.Load<GameObject>(GridManager.instance.buildings[buildingName])
            .GetComponentInChildren<BuildingBase>().purchaseCost < 0
        || EconomyManager.instance.currentEnergy - Resources.Load<GameObject>(GridManager.instance.buildings[buildingName]).
            GetComponentInChildren<BuildingBase>().energyCost < 0
        || EconomyManager.instance.currentWorkPower < Resources.Load<GameObject>(GridManager.instance.buildings[buildingName]).
            GetComponentInChildren<BuildingBase>().workPowerRequirement)
        {
            return;
        }
        
        tempBuilding = Instantiate(Resources.Load<GameObject>(GridManager.instance.buildings[buildingName]), Vector3.zero, UnityEngine.Quaternion.identity, buildingParent.transform);
        tempBuildingInfo = tempBuilding.GetComponentInChildren<BuildingBase>();
        tempBuildingInfo.gameObject.AddComponent<Outline>();
        GridManager.instance.isBuildingBeingPlaced = true;
        
    }

    void Update()
    {
        if (tempBuilding != null) 
        {
            PlacingBuilding();
            
            if(Input.GetMouseButtonDown(0) && GridManager.instance.CanBuildingBePlaced(tempBuilding.transform.position) && tempBuildingInfo.canBePlaced)
            { 
                GridManager.instance.BuildingPlaced(tempBuilding.transform.position);
                EconomyManager.instance.RemoveMoney(tempBuildingInfo.purchaseCost);
                EconomyManager.instance.RemoveEnergy(tempBuildingInfo.energyCost);
                EconomyManager.instance.AddEnergy(tempBuildingInfo.energyProduced);
                EconomyManager.instance.AddWorkPower(tempBuildingInfo.workPowerProduced);
                EconomyManager.instance.UpdateUI();
                if (QuestManager.instance.currentQuest != null) 
                {
                    if (QuestManager.instance.currentQuest.type == QuestDataSO.QUEST_TYPE.BUILDING) QuestManager.instance.IncrementQuest();
                }
                EconomyManager.instance.economyTickEvent.AddListener(tempBuildingInfo.ProduceMoney);
                Destroy(tempBuildingInfo.gameObject.GetComponent<Outline>());
                tempBuilding = null;
                tempBuildingInfo = null;
                GridManager.instance.isBuildingBeingPlaced = false;
                Cursor.visible = true;
                AudioManager.singleton.PlaySoundList(gameObject);
            }
            
            if(Input.GetMouseButtonDown(1)) 
            {
                Destroy(tempBuilding);
                tempBuilding = null;
                tempBuildingInfo = null;
                GridManager.instance.isBuildingBeingPlaced = false;
                Cursor.visible = true;
            }
        }

    }

    void PlacingBuilding() 
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
        Cursor.visible = false;
        
        
        if (tempBuildingInfo.gameObject.GetComponent<Outline>()) 
        {
            tempBuildingInfo.gameObject.GetComponent<Outline>().OutlineColor = tempBuildingInfo.canBePlaced == true ? Color.green : Color.red;
        }
        
        
        if(Physics.Raycast(ray, out hit, 100, gameLayer)) 
        {
            Vector3 adjustedPosition = hit.transform.position;
            adjustedPosition.y = adjustedPosition.y + 1;
            tempBuilding.transform.position = adjustedPosition;
        }
        
        if (Input.GetKeyDown(KeyCode.R)) 
        {
            tempBuilding.transform.Rotate(transform.rotation.x, 90f, transform.rotation.z);
        }
        
        
    }
}
