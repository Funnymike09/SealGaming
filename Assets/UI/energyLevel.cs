using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class energyLevel : MonoBehaviour
{
    public float energyValue;
    public TextMeshProUGUI energyText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        energyText.text = energyValue.ToString();
    }
}
