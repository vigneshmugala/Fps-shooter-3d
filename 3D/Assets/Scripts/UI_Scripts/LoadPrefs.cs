using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoadPrefs : MonoBehaviour
{
    [Header("General Settings")]
    [SerializeField] private bool canUse;
    public MenuController menuContoller;

    [Header("Volume Settings")]
    public Slider volumeSlider;
    public TMP_Text volumeText;

    [Header("Graphics Settings")]
    public Slider brightnessSlider;
    public TMP_Text brightnessText;
    public Toggle isFullScreentoggle;

    [Header("Quality Settings")]
    public TMP_Dropdown qualityDropdown;

    [Header("Gameplay Settings")]
    public Toggle invertYtoggle;
    public Slider sensitivitySlider;
    public TMP_Text sensitivityText;


    private void Awake()
    {
        if(canUse)
        {
            //Load Volume Settings from PlayerPrefs Class.
            if(PlayerPrefs.HasKey("masterVolume"))
            {
                float volumeToSet = PlayerPrefs.GetFloat("masterVolume");
                volumeSlider.value = volumeToSet;
                volumeText.text = volumeToSet.ToString("0.0");
                AudioListener.volume = volumeToSet;
            }
            else
            {
                menuContoller.ResetButton("Audio");
            }
            //Load Sensitivity Settings from PlayerPrefs Class
            if(PlayerPrefs.HasKey("masterSensitivity"))
            {
                float sensitivityToSet = PlayerPrefs.GetFloat("masterSensitivity");
                sensitivitySlider.value = sensitivityToSet;
                sensitivityText.text = sensitivityToSet.ToString("0");
                menuContoller.mainSensitivity = Mathf.RoundToInt(sensitivityToSet);

            }
            //Load Brightness Settings from PlayerPrefs Class
            if (PlayerPrefs.HasKey("masterBrightness"))
            {
                float brightnessToSet = PlayerPrefs.GetFloat("masterBrightness");
                brightnessSlider.value = brightnessToSet;
                brightnessText.text = brightnessToSet.ToString("0.0");
                //Change brightness in this line with something
            }
            
            //Load Invert Y toggle (on/off) using Settings from PlayerPrefs Class
            if(PlayerPrefs.HasKey("masterInvertY"))
            {
                int invertYvaluetoSet = PlayerPrefs.GetInt("masterInvertY");
                if (invertYvaluetoSet == 1)
                    invertYtoggle.isOn = true;
                else
                    invertYtoggle.isOn = false;
            }

            //Load Full Screen toggle (on/off) using Settings from PlayerPrefs Class
            if(PlayerPrefs.HasKey("masterFullScreen"))
            {
                int fullScreenValtoSet = PlayerPrefs.GetInt("masterFullScreen");
                if (fullScreenValtoSet == 1)
                {
                    isFullScreentoggle.isOn = true;
                    Screen.fullScreen = true;
                }
                else
                {
                    isFullScreentoggle.isOn = false;
                    Screen.fullScreen = false;
                }
                  
            }
            //Load Quality Settings from PlayerPrefs Class
            if(PlayerPrefs.HasKey("masterQualityLevel"))
            {
                int qualityLeveltoSet = PlayerPrefs.GetInt("masterQualityLevel");
                qualityDropdown.value = qualityLeveltoSet;
                QualitySettings.SetQualityLevel(qualityLeveltoSet);
            }

        }
    }
}
