using TMPro;
using UnityEngine;

public class WorkerCount : MonoBehaviour
{
    public int workerCount;
    public TextMeshProUGUI workerText;

    // Update is called once per frame
    void Update()
    {
        //counter = 
        workerText.text = workerCount.ToString();
        // counterText.text = counter.ToString("€" + counter);
    }
}
