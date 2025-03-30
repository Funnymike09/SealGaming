using TMPro;
using UnityEditor.Rendering.Universal.ShaderGraph;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EconomyManager : MonoBehaviour
{

    public static EconomyManager instance;

    public TextMeshProUGUI moneyText, workPowerText, energyText, dayText;


    public float currentMoney { get; private set; }
    public float currentWorkPower { get; private set; }
    public float currentEnergy { get; private set ; }
    public float currentInfluence { get; private set; }

    [Header("Starting Vaules")]
    [SerializeField] int startingMoney;

    [Header("Enconomy")]
    [SerializeField] float enconomyTickLength;

    public UnityEvent economyTickEvent = new UnityEvent();

    void Awake()
    {
        if (instance != null && instance != this) 
        {
            Destroy(this);
        }
        else 
        {
            instance = this;
        }
        
        currentMoney = startingMoney;

        moneyText.text = "Money: " + currentMoney + "$";
        workPowerText.text = "Work Power: " + currentWorkPower;
        energyText.text = "Energy: " + currentEnergy;
        dayText.text = "Day: " + GridManager.instance.dayIndex;
        economyTickEvent.AddListener(UpdateUI);
        InvokeRepeating(nameof(EnconomyTick), enconomyTickLength, enconomyTickLength);

    }

    public void EnconomyTick() 
    {
        economyTickEvent.Invoke();    
        UpdateUI();
    }

    public void UpdateUI() 
    {
        moneyText.text = "Money: " + (int)currentMoney + "$";
        workPowerText.text = "Work Power: " + (int)currentWorkPower;
        energyText.text = "Energy: " + (int)currentEnergy;
        dayText.text = "Day: " + GridManager.instance.dayIndex;
    }
    
    public void AddMoney(float amount) 
    {
        currentMoney += amount;
    }
    
    public void RemoveMoney(float amount) 
    {
        currentMoney -= amount;
    }

}
