using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class moneyJar : MonoBehaviour
{
    public Sprite currentSprite0;
    public Sprite currentSprite1;
    public Sprite currentSprite2;
    public Sprite currentSprite3;
    public Sprite currentSprite4;

    public int currentMoney;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentMoney <= 0)
        {
            gameObject.GetComponent<Image>().sprite = currentSprite0;
        }

        else if (currentMoney < 100 && currentMoney >= 0)
        {
            gameObject.GetComponent<Image>().sprite = currentSprite1;

        }

        else if (currentMoney < 500 && currentMoney >= 100)
        {
            gameObject.GetComponent<Image>().sprite = currentSprite2;

        }

        else if (currentMoney < 1500 && currentMoney >= 500)
        {
            gameObject.GetComponent<Image>().sprite = currentSprite3;

        }

        else if (currentMoney >= 1500)
        {
            gameObject.GetComponent<Image>().sprite = currentSprite4;

        }
    }
}
