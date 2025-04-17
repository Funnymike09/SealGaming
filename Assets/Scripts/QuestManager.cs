using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;
    
    public QuestDataSO currentQuest { get; private set; }
    
    public List<QuestDataSO> quests = new List<QuestDataSO>();
    
    int questIndex = 0;
    
    public int currentValue;

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
        }
    
    }
    
    
    
}
