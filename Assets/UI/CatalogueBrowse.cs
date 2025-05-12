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
    public GameObject buyGen;
    public GameObject buyBath;
    public GameObject buyGen2;
    public GameObject buyBath2;
    public GameObject buyGen3;
    public GameObject buyBath3;

    public Sprite lvl3Gen;
    public Sprite lvl1Bath;
    public Sprite lvl2Gen;
    public Sprite lvl2Bath;
    public Sprite lvl3Bath;
    public Sprite lvl1Gen;
    public Sprite Centre;

    private Animator anim;
    Button.ButtonClickedEvent onClick;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentPage = 0;
        currentText = Text.GetComponent<TextMeshProUGUI>();
        currentMoneyText = Money.GetComponent<TextMeshProUGUI>();
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentPage >= 7)
        {
            currentPage = 6;
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
                buyGen.SetActive(false);
                buyBath.SetActive(false);
                buyGen2.SetActive(false);
                buyBath2.SetActive(false);
                buyGen3.SetActive(false);
                buyBath3.SetActive(false);
                Image.GetComponent<Image>().sprite = Centre; //Placeholder until Finn gives centre icon
            }
        }

        else if (currentPage == 1)
        {
            currentText.text = "A small building that generates some energy for the island and its buildings through use of wind power.";
            currentMoneyText.text = "250";
            if (Image.activeSelf)
            {
                buyTest.SetActive(false);
                buyGen.SetActive(true);
                buyBath.SetActive(false);
                buyGen2.SetActive(false);
                buyBath2.SetActive(false);
                buyGen3.SetActive(false);
                buyBath3.SetActive(false);
                Image.GetComponent<Image>().sprite = lvl1Gen;
            }
        }

        else if (currentPage == 2)
        {
            currentText.text = "A small bath you can put a local seal in to raise its happiness and clean it if it has become unhygenic. To use, drag and drop a seal onto it.";
            currentMoneyText.text = "300";
            if (Image.activeSelf)
            {
                buyTest.SetActive(false);
                buyGen.SetActive(false);
                buyBath.SetActive(true);
                buyGen2.SetActive(false);
                buyBath2.SetActive(false);
                buyGen3.SetActive(false);
                buyBath3.SetActive(false);
                Image.GetComponent<Image>().sprite = lvl1Bath;
            }
        }

        else if (currentPage == 3)
        {
            currentText.text = "A medium building that efficiently generates energy for the island and its buildings through use of wind power.";
            currentMoneyText.text = "400";
            if (Image.activeSelf)
            {
                buyTest.SetActive(false);
                buyGen.SetActive(false);
                buyBath.SetActive(false);
                buyGen2.SetActive(true);
                buyBath2.SetActive(false);
                buyGen3.SetActive(false);
                buyBath3.SetActive(false);
                Image.GetComponent<Image>().sprite = lvl2Gen;
            }
        }

        else if (currentPage == 4)
        {
            currentText.text = "A medium bath you can put multipile local seals in to raise happiness and clean. To use, drag and drop seals onto it.";
            currentMoneyText.text = "500";
            if (Image.activeSelf)
            {
                buyTest.SetActive(false);
                buyGen.SetActive(false);
                buyBath.SetActive(false);
                buyGen2.SetActive(false);
                buyBath2.SetActive(true);
                buyGen3.SetActive(false);
                buyBath3.SetActive(false);
                Image.GetComponent<Image>().sprite = lvl2Bath;
            }
        }

        else if (currentPage == 5)
        {
            currentText.text = "A large building that generates massive amounts of energy for the island and its buildings through use of wind power.";
            currentMoneyText.text = "800";
            if (Image.activeSelf)
            {
                buyTest.SetActive(false);
                buyGen.SetActive(false);
                buyBath.SetActive(false);
                buyGen2.SetActive(false);
                buyBath2.SetActive(false);
                buyGen3.SetActive(true);
                buyBath3.SetActive(false);
                Image.GetComponent<Image>().sprite = lvl3Gen;
            }
        }

        else if (currentPage == 6)
        {
            currentText.text = "A large bath you can put a jamboree of local seals in to drastically raise happiness and clean. To use, drag and drop seals onto it.";
            currentMoneyText.text = "900";
            if (Image.activeSelf)
            {
                buyTest.SetActive(false);
                buyGen.SetActive(false);
                buyBath.SetActive(false);
                buyGen2.SetActive(false);
                buyBath2.SetActive(false);
                buyGen3.SetActive(false);
                buyBath3.SetActive(true);
                Image.GetComponent<Image>().sprite = lvl3Bath;
            }
        }
    }

    public void flipLeft()
    {
        anim.Play("ChangePage");
        currentPage--;
    }

    public void flipRight()
    {
        anim.Play("ChangePage");
        currentPage++;
    }
}
