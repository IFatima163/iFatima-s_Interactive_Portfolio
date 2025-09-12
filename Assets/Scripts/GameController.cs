using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum GameState { FreeRoam, Dialog, Image , Combined, Alternate, ui}

public class GameController : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] GameObject optionsMenu;
    [SerializeField] GameObject mapExpanded;
    [SerializeField] GameObject quitPanel;

    private GameState state;

    private void Start()
    {
        DialogManager.Instance.OnShowDialog += () =>
        {
            state = GameState.Dialog;
        };
        DialogManager.Instance.OnHideDialog += () =>
        {
            if (state == GameState.Dialog)
                state = GameState.FreeRoam;
        };
        ImageManager.Instance.OnShowImage += () =>
        {
            state = GameState.Image;
        };
        ImageManager.Instance.OnHideImage += () =>
        {
            if (state == GameState.Image)
                state = GameState.FreeRoam;
        };
        CombinedManager.Instance.OnCombinedShow += () =>
        {
            state = GameState.Combined;
        };
        CombinedManager.Instance.OnCombinedHide += () =>
        {
            if (state == GameState.Combined)
                state = GameState.FreeRoam;
        };
        AlternateManager.Instance.OnAlternateShow += () =>
        {
            state = GameState.Alternate;
        };
        AlternateManager.Instance.OnAlternateHide += () =>
        {
            if (state == GameState.Alternate)
                state = GameState.FreeRoam;
        };
    }

    private void Update()
    { int number = 0;
        if (state == GameState.FreeRoam)
        {
            playerController.HandleUpdate();
        }
        else if (state == GameState.Dialog)
        {
            DialogManager.Instance.HandleUpdate();
        }
        else if (state == GameState.Image)
        {
            ImageManager.Instance.HandleUpdate();
        }
        else if (state == GameState.Combined)
        {
            CombinedManager.Instance.HandleUpdate();
        }
        else if (state == GameState.Alternate)
        {
            number++;
            AlternateManager.Instance.HandleUpdate();
        }
    }

    public void ShowMap()
    {
        mapExpanded.SetActive(true);

        state = GameState.ui;
    }

    public void CloseMap()
    {
        mapExpanded.SetActive(false);

        state = GameState.FreeRoam;
    }

    public void ShowOptions()
    {
        optionsMenu.SetActive(true);

        state = GameState.ui;
    }

    public void CloseMenu()
    {
        optionsMenu.SetActive(false);

        state = GameState.FreeRoam;
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
        state = GameState.FreeRoam;

        Application.OpenURL("https://itch.io/");
        Application.Quit();
    }
}
