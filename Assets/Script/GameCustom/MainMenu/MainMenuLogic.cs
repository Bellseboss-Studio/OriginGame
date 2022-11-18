using System;
using Gameplay;
using SystemOfExtras;
using SystemOfExtras.GlobalInformationPath;
using SystemOfExtras.SavedData;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuLogic : MonoBehaviour
{
    [SerializeField] private Button start, credits, controls, options, exit, login;
    [SerializeField] private int nextScene;
    [SerializeField] private GameObject mainMenuPanel, creditsPanel, controlsPanel, optionsPanel, loginPanel;
    [SerializeField] private TMP_InputField inputNick;

    private void Start()
    {
        HideAllPanels();
        start.onClick.AddListener(StartGame);
        credits.onClick.AddListener(ShowCredits);
        controls.onClick.AddListener(ShowControls);
        options.onClick.AddListener(ShowOptions);
        exit.onClick.AddListener(ExitGame);
        login.onClick.AddListener(Login);
        if (!ServiceLocator.Instance.GetService<IGlobalInformation>().IsAuthenticated())
        {
            loginPanel.SetActive(true);
        }
        else
        {
            GoBackMainMenu();
        }

        ServiceLocator.Instance.GetService<IAudioService>().StayInMenu();
        ServiceLocator.Instance.GetService<ILoadScene>().Open(() => { });
    }

    private void Login()
    {
        var nick = inputNick.text;
        inputNick.text = string.Empty;
        ServiceLocator.Instance.GetService<IGlobalInformation>().SaveNickName(nick);
        GoBackMainMenu();
    }

    private void ExitGame()
    {
#if UNITY_EDITOR
        //Quitarle el modo play al juego
#else
        Application.Quit(0);
#endif
    }

    private void ShowOptions()
    {
        HideAllPanels();
        optionsPanel.SetActive(true);
    }

    private void ShowControls()
    {
     
        HideAllPanels();
        controlsPanel.SetActive(true);   
    }

    private void ShowCredits()
    {
        HideAllPanels();
        creditsPanel.SetActive(true);
    }

    private void HideAllPanels()
    {
        mainMenuPanel.SetActive(false);
        creditsPanel.SetActive(false);
        optionsPanel.SetActive(false);
        controlsPanel.SetActive(false);
        loginPanel.SetActive(false);
    }

    public void GoBackMainMenu()
    {
        HideAllPanels();
        mainMenuPanel.SetActive(true);
    }

    private void StartGame()
    {
        ServiceLocator.Instance.GetService<IAudioService>().Click();
        ServiceLocator.Instance.GetService<ILoadScene>().Close(() =>
        {
            //configure more things
            SceneManager.LoadScene(nextScene); 
        });
    }
}
