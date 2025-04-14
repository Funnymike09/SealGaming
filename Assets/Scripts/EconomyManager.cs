using System.Collections;
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
    public float currentEnergy { get; private set; }
    public float currentInfluence { get; private set; }

    [Header("Popup text variables")]
    public GameObject textPopupPrefab;
    [SerializeField] private Transform popupParent;
    public Vector3[] popupSpawnLocations = new Vector3[5]; // 0 = money, 1 = manpower, 2 = electro
    public float textMoveSpeed;
    public float stayTime;


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
        StartCoroutine(PopupText(popupSpawnLocations[0], amount.ToString(), Color.green, stayTime));
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

    public IEnumerator PopupText(Vector2 screenPos, string displayString, Color color, float stayTime) // create a popup showing how much money they earn / spend. QOL  // might need to add a empty game obj to each price tag 
    {                                               
        GameObject obj = Instantiate(textPopupPrefab, popupParent);
        obj.GetComponent<RectTransform>().anchoredPosition = screenPos;
        obj.transform.SetAsLastSibling();
        TextMeshProUGUI objText = obj.GetComponent<TextMeshProUGUI>();
        objText.text = displayString;
        objText.color = color;

        float timer = 0f;
        while (timer < 1f)
        {
            obj.transform.position = new Vector2(obj.transform.position.x, obj.transform.position.y + textMoveSpeed * Time.deltaTime);
            timer += Time.deltaTime;

            if (obj.transform.position.y > 514)
            {
                timer = 1f;
            }

            yield return null;
        }

        yield return new WaitForSeconds(stayTime);

        //timer = 0f;
        while (objText.color.a > 0)
        {
            objText.color = new Color(objText.color.r, objText.color.g, objText.color.b, objText.color.a - Time.deltaTime);
            //timer += Time.deltaTime;
            yield return null;
        }
        Destroy(obj);

    }
}
