using SystemOfExtras;
using SystemOfExtras.GlobalInformationPath;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GeneralBlackJack : MonoBehaviour
{
    [SerializeField] private Presentations presentations;
    [SerializeField] private Button winBtn, loseBtn;
    [SerializeField] private DeckForGame playerDeck, botDeck;
    private TeaTime _presentationEnemy, _presentationPlayer, _preparingGame, _game, _winGame, _loseGame;
    private int stateOfGame;//0: inGame; 1: Win; 2: Lose

    private void Start()
    {
        HideButtons();
        ServiceLocator.Instance.GetService<ILoadScene>().Lock();
        _presentationEnemy = this.tt().Pause().Add(() =>
        {
            presentations.StartPresentationEnemy();
        }).Loop(handler =>
        {
            if (presentations.FinishEnemyPresentation)
            {
                handler.Break();
            }
        }).Add(() =>
        {
            _presentationPlayer.Play();
        });

        _presentationPlayer = this.tt().Pause().Add(() =>
        {
            presentations.StartPresentationPlayer();
        }).Loop(handler =>
        {
            if (presentations.FinishPlayerPresentation)
            {
                handler.Break();
            }
        }).Add(() =>
        {
            _preparingGame.Play();
        });

        _preparingGame = this.tt().Pause().Add(() =>
        {
            botDeck.DrawCards();
            playerDeck.DrawCards();
        }).Loop(handle =>
        {
            if (playerDeck.DrawIsFinished && botDeck.DrawIsFinished)
            {
                handle.Break();   
            }
        }).Add(() =>
        {
            ServiceLocator.Instance.GetService<ILoadScene>().Unlock();
            _game.Play();
        });

        _game = this.tt().Pause().Add(() =>
        {
            //show buttons
            StartGame();
            Debug.Log("In Game");
        }).Loop(handle =>
        {
            if (stateOfGame > 0)
            {
                handle.Break();
            }
        }).Add(() =>
        {
            Debug.Log("Game Finished");
            ServiceLocator.Instance.GetService<ILoadScene>().Lock();
            switch (stateOfGame)
            {
                case 1:
                    _winGame.Play();
                    break;
                case 2:
                    _loseGame.Play();
                    break;
            }
        });

        _winGame = this.tt().Pause().Add(() =>
        {
            Debug.Log("Game Win");
            presentations.WinGameAnimation();
        }).Loop(handle =>
        {
            if (presentations.FinishPresentationOfWinner)
            {
                handle.Break();
            }
        }).Add(() =>
        {
            ServiceLocator.Instance.GetService<IGlobalInformation>().WinHexagon();
            GoToCityBuilding();
        }); 
        _loseGame = this.tt().Pause().Add(() =>
        {
            Debug.Log("Game Lose");
            presentations.LoseGameAnimation();
        }).Loop(handle =>
        {
            if (presentations.FinishPresentationOfWinner)
            {
                handle.Break();
            }
        }).Add(() =>
        {
            ServiceLocator.Instance.GetService<IGlobalInformation>().LoseHexagon();
            GoToCityBuilding();
        }); 

        _presentationEnemy.Play();
        ServiceLocator.Instance.GetService<ILoadScene>().Open(() => { });
    }

    public void GoToCityBuilding()
    {
        ServiceLocator.Instance.GetService<ILoadScene>().Unlock();
        _presentationEnemy.Stop();
        _presentationPlayer.Stop();
        _preparingGame.Stop();
        ServiceLocator.Instance.GetService<ILoadScene>().Close(() =>
        {
            SceneManager.LoadScene(3);
        });
    }
    
    
    public void StartGame()
    {
        //winBtn.gameObject.SetActive(true);
        //loseBtn.gameObject.SetActive(true);
    }

    private void HideButtons()
    {
        winBtn.gameObject.SetActive(false);
        loseBtn.gameObject.SetActive(false);
    }

    public void Win()
    {
        stateOfGame = 1;
    }
    public void Lose()
    {
        stateOfGame = 2;
    }
}