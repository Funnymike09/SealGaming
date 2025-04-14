using UnityEngine;
using TMPro;
using UnityEngine.UI;

//Basically I gotta have it so that this all revolves around the currentPage variable, each number sets everything to correspond to a specific page.
//It won't be clean but the best way to do this is just a lot of if statements I think.
//Hard part will be implementing the buy button, just remember to set it up with the click thing,

public class CatalogueBrowse : MonoBehaviour
{
    public int currentPage;
    private TextMeshProUGUI currentText;
    private TextMeshProUGUI currentMoneyText;
    private Image Icon;
    [SerializeField] private string buyButtonCurrent;
    private string line1;

    public GameObject Image;
    public GameObject Text;
    public GameObject Money;
    public GameObject buyTest;
    public GameObject buyBigTest;
    public GameObject buyBath;
    Button.ButtonClickedEvent onClick;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentPage = 0;
        currentText = Text.GetComponent<TextMeshProUGUI>();
        currentMoneyText = Money.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentPage >= 6)
        {
            currentPage = 5;
        }

        if (currentPage <= -1)
        {
            currentPage = 0;
        }

        if (currentPage == 0)
        {
            currentText.text = "A modest care facility for the housing, caring and sheltering of seals. Comes with all the necessities such as air filtration, living space and offices.";
            currentMoneyText.text = "200";
            if (Image.activeSelf)
            {
                buyTest.SetActive(true);
                buyBigTest.SetActive(false);
                buyBath.SetActive(false);
            }
        }

        else if (currentPage == 1)
        {
            currentText.text = "A small building that is constantly generating energy for the island and its buildings through use of wind power.";
            currentMoneyText.text = "250";
            if (Image.activeSelf)
            {
                buyTest.SetActive(false);
                buyBigTest.SetActive(true);
                buyBath.SetActive(false);
            }
        }

        else if (currentPage == 2)
        {
            currentText.text = "A small bath you can put a local sea in to raise its happiness and clean it if it has become unhygenic. To use, drag and drop a seal onto it.";
            currentMoneyText.text = "300";
            if (Image.activeSelf)
            {
                buyTest.SetActive(false);
                buyBigTest.SetActive(false);
                buyBath.SetActive(true);
            }
        }

        else if (currentPage == 3)
        {
            currentText.text = "There is no building assigned to this page as of yet.";
            currentMoneyText.text = "0";
            if (Image.activeSelf)
            {
                buyTest.SetActive(false);
                buyBigTest.SetActive(false);
                buyBath.SetActive(false);
            }
        }

        else if (currentPage == 4)
        {
            currentText.text = "There is no building assigned to this page as of yet.";
            currentMoneyText.text = "0";
            if (Image.activeSelf)
            {
                buyTest.SetActive(false);
                buyBigTest.SetActive(false);
                buyBath.SetActive(false);
            }
        }

        else if (currentPage == 5)
        {
            currentText.text = "There is no building assigned to this page as of yet.";
            currentMoneyText.text = "0";
            if (Image.activeSelf)
            {
                buyTest.SetActive(false);
                buyBigTest.SetActive(false);
                buyBath.SetActive(false);
            }
        }
    }

    public void flipLeft()
    {
        currentPage--;
    }

    public void flipRight()
    {
        currentPage++;
    }
}
