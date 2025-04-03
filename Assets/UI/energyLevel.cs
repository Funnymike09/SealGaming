using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
        // energyText.text = energyValue.ToString("" + Mathf.Floor(energyValue));
        // energyText.text = energyValue.ToString(Mathf.Floor(energyValue) + "%");
    }
}
