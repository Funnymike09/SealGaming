using TMPro;
using UnityEngine;
using UnityEngine.Audio;
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
    [SerializeField] int startingEnergy;
    [SerializeField] int startingWorkPower;

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
        currentEnergy = startingEnergy;
        currentWorkPower = startingWorkPower;

        moneyText.text = currentMoney.ToString();
        workPowerText.text = currentWorkPower.ToString();
        energyText.text = currentEnergy.ToString();
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
        moneyText.text = currentMoney.ToString();
        workPowerText.text = currentWorkPower.ToString();
        energyText.text = currentEnergy.ToString();
        dayText.text = "Day: " + GridManager.instance.dayIndex;
    }
    
    public void AddMoney(float amount) 
    {
        currentMoney += amount;
        //AudioManager.singleton.PlaySoundListOnce(gameObject, moner);
    }
    
    public void RemoveMoney(float amount) 
    {
        currentMoney -= amount;
    }
    
    public void AddEnergy(float amount) 
    {
        currentEnergy += amount;
    }
    
    public void RemoveEnergy(float amount) 
    {
        currentEnergy -= amount;
    }
    
    public void AddWorkPower(float amount) 
    {
        currentWorkPower += amount;
    }
    
    public void RemoveWorkPower(float amount) 
    {
        currentWorkPower -= amount;
    }

}
