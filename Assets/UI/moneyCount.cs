using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class moneyCount : MonoBehaviour
{
    private int counter;
    public GameObject moneyValue;
    public TextMeshProUGUI counterText;

    // Update is called once per frame
    void Update()
    {
        // counter = moneyValue.GetComponent<moneyJar>().currentMoney;
        // counterText.text = counter.ToString();
        // counterText.text = counter.ToString("�" + counter);
    }
}
