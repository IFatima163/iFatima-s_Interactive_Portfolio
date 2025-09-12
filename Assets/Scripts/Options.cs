using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject optionsMenu;
    [SerializeField] GameObject quitPanel;
    [SerializeField] GameObject stillPortfolio;
    [SerializeField] GameObject loadingPanel;   

    public void PlayGame()
    {
        loadingPanel.SetActive(true);
        //mainMenu.SetActive(false);

        SceneManager.LoadSceneAsync(1);
    }

    public void StillPortfolioConfirmation()
    {
        stillPortfolio.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void BackToMenu()
    {
        mainMenu.SetActive(true);
        stillPortfolio.SetActive(false);
    }

    public void OpenStillPortfolio()
    {
        mainMenu.SetActive(true);
        stillPortfolio.SetActive(false);

        Application.OpenURL("https://drive.google.com/drive/folders/1HFDg93ff1aX8t3XaUEnSqyxp6LAvi2RG?usp=drive_link");
    }

    public void ShowOptions()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }

    public void CloseMenu()
    {
        mainMenu.SetActive(true);
        optionsMenu.SetActive(false);
    }

    public void QuitButton()
    {
        optionsMenu.SetActive(false);
        quitPanel.SetActive(true);
    }

    public void BackToOptions()
    {
        optionsMenu.SetActive(true);
        quitPanel.SetActive(false);
    }

    public void QuitPortfolio()
    {
        Application.OpenURL("https://itch.io/");
        Application.Quit();
    }
}
