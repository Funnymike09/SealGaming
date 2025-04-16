using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;
    
    public QuestDataSO currentQuest { get; private set; }
    
    public List<QuestDataSO> quests = new List<QuestDataSO>();
    
    int questIndex = 0;

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
        
        
        currentQuest = quests[questIndex];
    }
    
    public void IncrementQuest() 
    {
        currentQuest.currentValue += 1;
    
        if (currentQuest.currentValue == currentQuest.maxValue) 
        {
            EconomyManager.instance.AddMoney(currentQuest.rewardValue);
            EconomyManager.instance.UpdateUI();
            questIndex += 1;
            currentQuest = quests[questIndex];
        }
    
    }
    
    
    
}
