using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public Material outlineMaterial;
    public Toggle toggleButton;
    public Slider slider;
    public AudioMixer masterMixer;
    private bool isPaused;
    public GameObject PauseUI;
    public CameraControls Controls;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetComponent<CameraControls>().enabled = true;
        isPaused = false;
        PauseUI.SetActive(false);
        SetVolume(PlayerPrefs.GetFloat("SavedMasterVolume"));
        if (slider != null)
        {
            slider.value = slider.maxValue;
        }
        if (toggleButton != null)
        {
            toggleButton.onValueChanged.AddListener(OnToggleChange);
            OnToggleChange(toggleButton.isOn);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
            Debug.Log("Paused");
        }
    }

    public void Pause()
    {
        isPaused = !isPaused;

        if(isPaused)
        {
            Time.timeScale = 0;
            PauseUI.SetActive(true);
            GetComponent<CameraControls>().enabled = false;
        }

        else
        {
            Time.timeScale = 1;
            PauseUI.SetActive(false);
            GetComponent<CameraControls>().enabled = true;
        }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        //GetComponent<CameraControls>().enabled = true;
    }
    public void SetVolume(float value)
    {
        if (value < 1f)
        {
            value = 0.001f;
        }
        RefreshSlider(value);
        PlayerPrefs.SetFloat("SaveMasterVolume", value);
        masterMixer.SetFloat("MasterVolume", Mathf.Log10(value / 100) * 20);

    }

    public void SetVolumeFromSlider()
    {
        SetVolume(slider.value);
    }

    public void RefreshSlider(float value)
    {
        slider.value = value;
    }

    public void OnToggleChange(bool isOn)
    {
        if(outlineMaterial != null)
        {
            float outlineValue = isOn ? 0.237f : 0f;
            outlineMaterial.SetFloat("_OutlineThickness", outlineValue);
        }
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void LoadWebsite()
    {
        Application.OpenURL("https://www.sealrescueireland.org/what-we-do/rescue/");
    }
}
