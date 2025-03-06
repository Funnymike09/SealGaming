using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EconomyManager : MonoBehaviour
{

    public static EconomyManager instance;

    public TextMeshProUGUI moneyText, workPowerText, energyText;


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

        moneyText.text = "Money: " + currentMoney + "$";

        economyTickEvent.AddListener(UpdateUI);
        InvokeRepeating(nameof(EnconomyTick), enconomyTickLength, enconomyTickLength);

    }

    void EnconomyTick() 
    {
        economyTickEvent.Invoke();    
    }

    void UpdateUI() 
    {

    }

}
