using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class EventManager : MonoBehaviour
{
    public static EventManager instance;

    [Header("Event Parameters")]
    public float eventDuration; // In Seconds
    public int probabilityThreshold;
    public int probablityDecrement;
    public float eventCheckInterval;

    public string activeEvent = "Default";
    public List<string> events = new List<string>(); //Events must be added to list before use

    [SerializeField] private GameObject eventPopup;
    [SerializeField] private TextMeshProUGUI popupText;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
        eventPopup.SetActive(false);

        InvokeRepeating("EventCheck", eventCheckInterval, eventCheckInterval);
    }

    public void CallEvent(string eventName)
    {
        switch (eventName)
        {
            //CALL EVENTS HERE
            case("Test"):
                Test();
                popupText.text = "You have been granted 1000 dollars!";
                eventPopup.SetActive(true);
                StartCoroutine(nameof(DisableText));
        
                break;
        }
            
    }

    private void EventCheck()
    {
        if (activeEvent != default)
        {
            int rollEvent = Random.Range(0, 100);
            if (rollEvent > probabilityThreshold)
            {
                CallEvent(events[Random.Range(0, events.Count)]);
                probabilityThreshold = 100;
                return;
            }
            probabilityThreshold -= probablityDecrement;
        }
    }
    
    IEnumerator DisableText() 
    {
        yield return new WaitForSeconds(5.0f);
        
        eventPopup.SetActive(false);
    }

    //EVENTS GO UNDER HERE
    
    public void Test() 
    {
        EconomyManager.instance.AddMoney(1000);
        EconomyManager.instance.UpdateUI();
        activeEvent = "Test";
    }
    
    
    
    
}