using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.Rendering;

public class Main_Menu : MonoBehaviour
{
    public Material outlineMaterial;
    public Toggle toggleButton;
    public Slider SoundSlider;
    public AudioMixer masterMixer;

    void Start()
    {
        SetVolume(PlayerPrefs.GetFloat("SavedMasterVolume"));
        if (SoundSlider != null)
        {
            SoundSlider.value = SoundSlider.maxValue;
        }
        if(toggleButton != null)
        {
            toggleButton.onValueChanged.AddListener(OnToggleChange);
            OnToggleChange(toggleButton.isOn);
        }


    }

    public void SetVolume(float value)
    {
        if(value < 1f)
        {
            value = 0.001f;
        }

        RefreshSlider(value);
        PlayerPrefs.SetFloat("SavedMasterVolume", value);
        masterMixer.SetFloat("MasterVolume", Mathf.Log10(value / 100) * 20f);
    }

    public void SetVolumeFromSlider()
    {
        SetVolume(SoundSlider.value);
    }

    public void RefreshSlider(float value)
    {
        SoundSlider.value = value;
    }

    public void OnToggleChange(bool isOn)
    {
        if(outlineMaterial != null)
        {
            float outlineValue = isOn ? 0.237f : 0f;
            outlineMaterial.SetFloat("_OutlineThickness", outlineValue);
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
