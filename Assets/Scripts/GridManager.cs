using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Vector3 = UnityEngine.Vector3;

public class GridManager : MonoBehaviour
{

    public GridInformation gridInfo;
    
    public static GridManager instance;

    [HideInInspector] public Grid grid;

    public Dictionary<String, String> buildings = new Dictionary<string, string>();
   
    [Header("Grid")]
    public int gridMinX;
    public int gridMaxX;
    public int gridMinZ;
    public int gridMaxZ; 
    
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

    }

    void InitialiseDict() 
    {
        buildings.Add("Test", "Buildings/Test");

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
        
    }

}
  