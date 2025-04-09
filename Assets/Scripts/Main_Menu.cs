using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Main_Menu : MonoBehaviour
{
    public FullScreenPassRendererFeature FullScreenPassRendererFeature;
    public Toggle toggleButton;

    void Start()
    {

        if (FullScreenPassRendererFeature == null)
        {
            Debug.LogError("FullScreenPassRendererFeature is not assigned!");
            return;
        }

        if (toggleButton != null && FullScreenPassRendererFeature != null)
        {
            FullScreenPassRendererFeature.SetActive(toggleButton.isOn);
        }


    }

    public void OnToggleChange(bool isOn)
    {
        if(FullScreenPassRendererFeature != null)
        {
            FullScreenPassRendererFeature.SetActive(isOn);
        }

        else
        {
            FullScreenPassRendererFeature.SetActive(false);
        }
    }



    public void Quit()
    {
        Application.Quit();
        Debug.Log("Quit");
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Alpha TestBuild");
    }

    public void LoadWebsite()
    {
        Application.OpenURL("https://www.sealrescueireland.org/what-we-do/rescue/");
    }

}
