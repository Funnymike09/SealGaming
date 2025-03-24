using UnityEngine;
using UnityEngine.Video;

public class BuildingBase : MonoBehaviour
{
    [Header("Econ Values")]
    [SerializeField] private float moneyProduced;
    public float purchaseCost;
    
    public bool canBePlaced;

    void Awake()
    {
        canBePlaced = true;
    }

    public void ProduceMoney() 
    {
        EconomyManager.instance.AddMoney(moneyProduced);
    }


    public void OnTriggerStay(Collider other)
    {
        canBePlaced = false;
        print("trigger entered");
    }

    public void OnTriggerExit(Collider other)
    {
        canBePlaced = true;
        print("trigger exited");
    }


}
