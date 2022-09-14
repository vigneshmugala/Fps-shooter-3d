using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuController : MonoBehaviour
{
    [Header("Sound Settings")]

    public Slider volumeSlider;
    public TMP_Text volumeText;
    public float defaultVolume = 0.65f;
    public GameObject confirmationTick;





    [Header ("Levels to Load")]
    

    public string newLeveltoLoad;
    public string levelToLoad;
    public GameObject noSavedGameUI;
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
        }
    }


}
