using System;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestDataSO", menuName = "Scriptable Objects/QuestDataSO")]
public class QuestDataSO : ScriptableObject
{
    [Flags]
    public enum QUEST_TYPE
    {
        SEAL = 1,
        BUILDING = 2,
        TRASH = 4
    }


    public string questName;
    public string questDescription;
    public QUEST_TYPE type;
    public int maxValue;
    public int rewardValue;
    
        
}
