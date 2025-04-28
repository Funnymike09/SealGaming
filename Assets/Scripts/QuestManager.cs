using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;
    
    public QuestDataSO currentQuest { get; private set; }
    
    public List<QuestDataSO> quests = new List<QuestDataSO>();
    
    int questIndex = 0;
    
    public int currentValue;
    
    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI questTitle;
    [SerializeField] private TextMeshProUGUI questDescription;
    [SerializeField] private TextMeshProUGUI questMaxValue;
    [SerializeField] private TextMeshProUGUI questCurrentValue;
    [SerializeField] private TextMeshProUGUI questReward;
   

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
        
        currentValue = 0;
        currentQuest = quests[questIndex];
        UpdateUI();
    }
    
    public void IncrementQuest() 
    {
        currentValue += 1;
    
        if (currentValue == currentQuest.maxValue) 
        {
            EconomyManager.instance.AddMoney(currentQuest.rewardValue);
            EconomyManager.instance.UpdateUI();
            questIndex += 1;
            if (questIndex >= quests.Count) 
            {
                currentQuest = null;
                return;
            }
             
            currentQuest = quests[questIndex];
            currentValue = 0;
            UpdateUI();
        }
    
    }

    void UpdateUI()
    {
        questTitle.text = currentQuest.questName;
        questDescription.text = currentQuest.questDescription;
        questMaxValue.text = currentQuest.maxValue.ToString();
        questCurrentValue.text = currentValue.ToString();
        questReward.text = currentQuest.rewardValue.ToString() + "$";
    }

    

}
