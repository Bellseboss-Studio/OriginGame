using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuLogic : MonoBehaviour
{
    [SerializeField] private Button start, credits, controls, options, exit;
    [SerializeField] private string nextScene;
    [SerializeField] private GameObject mainMenuPanel, creditsPanel, controlsPanel, optionsPanel;

    private void Start()
    {
        start.onClick.AddListener(StartGame);
        credits.onClick.AddListener(ShowCredits);
        controls.onClick.AddListener(ShowControls);
        options.onClick.AddListener(ShowOptions);
        exit.onClick.AddListener(ExitGame);
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
    }

    public void GoBackMainMenu()
    {
        HideAllPanels();
        mainMenuPanel.SetActive(true);
    }

    private void StartGame()
    {
        SceneManager.LoadScene(nextScene);
    }
}
