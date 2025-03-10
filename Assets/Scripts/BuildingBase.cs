using UnityEngine;

public class BuildingBase : MonoBehaviour
{
    
    [SerializeField] private float moneyProduced;
    
    
    public void ProduceMoney() 
    {
        EconomyManager.instance.AddMoney(moneyProduced);
    }
    
    
    
}
