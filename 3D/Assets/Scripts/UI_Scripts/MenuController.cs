using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuController : MonoBehaviour
{
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

}
