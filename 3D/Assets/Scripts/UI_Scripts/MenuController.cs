using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Unity.VisualScripting;

public class MenuController : MonoBehaviour
{
    [Header("Sound Settings")]

    public Slider volumeSlider;
    public TMP_Text volumeText;
    public float defaultVolume = 0.65f;

    [Header("Gameplay Settings")]

    public Slider sensitivitySlider;
    public Toggle invertYToggle;
    public int defaultSensitivity = 4;
    public int mainSensitivity;
    public TMP_Text sensitivityText;

    [Header("Graphics Settings")]

    public Slider brightnessSlider;
    public Toggle FullscreenToggle;
    public TMP_Dropdown qualityDropdown;
    public TMP_Text brightnessText;

    private int qualityLevel;
    private bool isFullScreen;
    private float brightnessLevel;

    [Header("Resolution Settings")]

    public TMP_Dropdown resolutionDropdown;
    private Resolution[] resolutions;


    [Header("Confirmation Box")]
    public GameObject confirmationTick;

    [Header ("Levels to Load")]
    
    public string newLeveltoLoad;
    public string levelToLoad;
    public GameObject noSavedGameUI;

    public void Start()
    {
        resolutions = Screen.resolutions;
        int currentRes;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();
        for(int i=0; i<resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentRes = i;
            }

        }
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution res = resolutions[resolutionIndex];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);

    }




    public void NewGameOnYes()
    {
        SceneManager.LoadScene(levelToLoad);
    }

    public void LoadGameOnYes()
    {
        if(PlayerPrefs.HasKey("SavedGame"))
        {
            levelToLoad = PlayerPrefs.GetString("SavedLevel");
            SceneManager.LoadScene(levelToLoad);
        }
        else
        {
            noSavedGameUI.SetActive(true);
        }

    }

    public void ExitUI()
    {
        Application.Quit();
    }


    public void SetVolume(float vol)
    {
        AudioListener.volume = vol;
        volumeText.text = vol.ToString("0.0");
    }

    public void VolumeApply()
    {
        PlayerPrefs.SetFloat("masterVolume", AudioListener.volume);
        StartCoroutine(ConfirmationBox());
    }

    public IEnumerator ConfirmationBox()
    {
        confirmationTick.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        confirmationTick.SetActive(false);
    }

    public void ResetButton(string menuType)
    {
        if(menuType == "Audio")
        {
            PlayerPrefs.SetFloat("masterVolume", defaultVolume);
            volumeSlider.value = defaultVolume;
            volumeText.text = defaultVolume.ToString("0.0");
            StartCoroutine(ConfirmationBox());

        }

        if(menuType == "Gameplay")
        {
            sensitivityText.text = defaultSensitivity.ToString("0");
            sensitivitySlider.value = defaultSensitivity;
            invertYToggle.isOn = false;
            mainSensitivity = defaultSensitivity;
            GameplayApply();

        }
    }

    public void SetSensitivity(float sensitivity)
    {
        mainSensitivity = Mathf.RoundToInt(sensitivity);
        sensitivityText.text = mainSensitivity.ToString("0");

    }


    public void SetBrightness(float brightness)
    {
        brightnessLevel = brightness;
        brightnessText.text = brightnessLevel.ToString("0.0");
    }


    public void SetQualityLevel(int qualityIndex)
    {
        qualityLevel = qualityIndex;
    }

    public void SetFullScreen(bool isFull)
    {
        isFullScreen = isFull;
    }

    public void GraphicsApply()
    {
        PlayerPrefs.SetFloat("masterBrightness", brightnessLevel);

        PlayerPrefs.SetInt("masterQualityLevel", qualityLevel);
        QualitySettings.SetQualityLevel(qualityLevel);

        PlayerPrefs.SetInt("masterFullScreen", (isFullScreen ? 1 : 0));
        Screen.fullScreen = isFullScreen;

        StartCoroutine(ConfirmationBox());
    }


    public void GameplayApply()
    {
        if(invertYToggle.isOn)
        {
            PlayerPrefs.SetInt("masterInvertY", 1);
        }
        else
        {
            PlayerPrefs.SetInt("masterInvertY", 0);
        }
        PlayerPrefs.SetFloat("masterSensitivity", mainSensitivity);
        StartCoroutine(ConfirmationBox());
    }


}
