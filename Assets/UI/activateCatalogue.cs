using UnityEngine;

public class activateCatalogue : MonoBehaviour
{
    public GameObject catalogueMenu;
    private bool catalogueOpened = false;
    public GameObject lArrow;
    public GameObject rArrow;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    // Push Fix
    void Update()
    {
        /*if (catalogueOpened == true)
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                for (int i = 0; i < catalogueMenu.transform.childCount; i++)
                {
                    catalogueMenu.transform.GetChild(i).gameObject.SetActive(false);
                    lArrow.SetActive(false);
                    rArrow.SetActive(false);
                }

                catalogueOpened = false;
            }
        }*/
    }

    public void openClose()
    {
        for (int i = 0; i < catalogueMenu.transform.childCount; i++)
        {
            catalogueMenu.transform.GetChild(i).gameObject.SetActive(true);
            lArrow.SetActive(true);
            rArrow.SetActive(true);
        }

        catalogueOpened = true;
    }

    public void closeThing()
    {
        for (int i = 0; i < catalogueMenu.transform.childCount; i++)
        {
            catalogueMenu.transform.GetChild(i).gameObject.SetActive(false);
            lArrow.SetActive(false);
            rArrow.SetActive(false);
        }

        catalogueOpened = false;
    }
}
