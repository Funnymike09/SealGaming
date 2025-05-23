using System;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.Tilemaps;
using Vector3 = UnityEngine.Vector3;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;

public class GridManager : MonoBehaviour
{

    public GridInformation gridInfo;
    
    public static GridManager instance;

    [HideInInspector] public Grid grid;

    public Dictionary<String, String> buildings = new Dictionary<string, string>();
    
    public int dayIndex;
    
    public GameObject[] beachTiles;
    [SerializeField]
    private Canvas HBcanvas;
    [SerializeField]
    private Camera HBcamera;
   
    [Header("Grid")]
    public int gridMinX;
    public int gridMaxX;
    public int gridMinZ;
    public int gridMaxZ; 
    
    [Header("Spawn Rates")]
    [SerializeField] private float sealSpawnRate;
    [SerializeField] private float trashSpawnRate;
    
    [HideInInspector] public bool isBuildingBeingPlaced;

    void Awake()
    {
        if (instance && instance != this) 
        {
            Destroy(this);
        }
        else 
        {
            instance = this;
        }

        grid = GameObject.Find("Grid").GetComponent<Grid>();
        
        InitialiseDict();
        
        SetUpGrid();
        
        dayIndex = 1;
        
    }

    void Start()
    {
        beachTiles = GameObject.FindGameObjectsWithTag("Beach");
        
        InvokeRepeating(nameof(SpawnSeal), 1, sealSpawnRate);
        // InvokeRepeating(nameof(AdvanceDay), 120, 120);
        InvokeRepeating(nameof(SpawnTrash), 1, trashSpawnRate);
    }
    
    void AdvanceDay() 
    {
        dayIndex += 1;
    }
    

    void InitialiseDict() 
    {
        buildings.Add("Test", "Buildings/Test");
        buildings.Add("BigTest", "Buildings/BigTest");
        buildings.Add("Seal Bathing Area", "Buildings/Seal Bathing Area");
        buildings.Add("Generator1", "Buildings/Generator1");
        buildings.Add("Seal Bathing Area2", "Buildings/Seal Bathing Area2");
        buildings.Add("Generator2", "Buildings/Generator2");
        buildings.Add("Seal Bathing Area3", "Buildings/Seal Bathing Area 3");
        buildings.Add("Generator3", "Buildings/Generator3");
        buildings.Add("Seal Centre", "Buildings/Seal Centre");

    }
    
    void SetUpGrid() 
    {
        for(int i = gridMinX; i < gridMaxX; i++) 
        {
            for(int j = gridMinZ; j < gridMaxZ; j++) {
                
                Vector3Int currentPos = new Vector3Int(i, j, 0);
                gridInfo.SetPositionProperty(currentPos, "CanBePlacedHere", 1);

            }
        }

    }
    
    public bool CanBuildingBePlaced(Vector3 pos) 
    {
        Vector3 normalisedPos = new Vector3(pos.x, 0, pos.z);
        Vector3Int newPos = grid.WorldToCell(normalisedPos);
        if(gridInfo.GetPositionProperty(newPos, "CanBePlacedHere", 0) == 1) 
        {
            return true;
        }
        else 
        {
            return false;
        }
    }
    
    public void BuildingPlaced(Vector3 pos) 
    {
        Vector3 normalisedPos = new Vector3(pos.x, 0, pos.z);
        Vector3Int newPos = grid.WorldToCell(normalisedPos);
      
        
        gridInfo.SetPositionProperty(newPos, "CanBePlacedHere", 0);
        print("building placed at: " + newPos);
        
    }
    
    public void SpawnSeal() 
    {
        Vector3 adjustedPosition = beachTiles[Random.Range(0, beachTiles.Length)].transform.position;
        adjustedPosition.y = adjustedPosition.y + 0.5f;
        GameObject seal = Instantiate(Resources.Load<GameObject>("Seals/AtlanticSeal"), adjustedPosition, Quaternion.Euler(-90, UnityEngine.Random.Range(0, 360), 0));
        seal.GetComponent<SealInfo>().SetupHealthBar(HBcanvas, HBcamera);
    }
    
    public void SpawnTrash() 
    {
        Vector3 adjustedPosition = beachTiles[Random.Range(0, beachTiles.Length)].transform.position;
        adjustedPosition.y = adjustedPosition.y + 0.5f;
        GameObject trash = Instantiate(Resources.Load<GameObject>("Buildings/TrashTile"), adjustedPosition, Quaternion.identity);
    }

}
  