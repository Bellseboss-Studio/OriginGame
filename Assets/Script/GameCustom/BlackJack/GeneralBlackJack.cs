using SystemOfExtras;
using SystemOfExtras.GlobalInformationPath;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GeneralBlackJack : MonoBehaviour
{
    [SerializeField] private Presentations presentations;
    [SerializeField] private Button winBtn, loseBtn;
    [SerializeField] private GameLogic game;
    private TeaTime _presentationEnemyState, _presentationPlayerState, _preparingGameState, _gameState, _winGameState, _loseGameState, _definitionOfGame;
    [SerializeField] private int healthEnemy, healthPlayer;
    [SerializeField] private int damageEnemy, damagePlayer;

    private void Start()
    {
        HideButtons();
        ServiceLocator.Instance.GetService<ILoadScene>().Lock();
        _presentationEnemyState = this.tt().Pause().Add(() =>
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
            _presentationPlayerState.Play();
        });

        _presentationPlayerState = this.tt().Pause().Add(() =>
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
            _preparingGameState.Play();
        });

        _preparingGameState = this.tt().Pause().Add(() =>
        {
            game.DrawCards();
        }).Loop(handle =>
        {
            if (game.DrawCardsIsFinished())
            {
                handle.Break();   
            }
        }).Add(() =>
        {
            _gameState.Play();
        });

        _gameState = this.tt().Pause().Add(() =>
        {
            //show buttons
            StartGame();
            Debug.Log("In Game");
        }).Loop(handle =>
        {
            if (game.StateOfGame > 0)
            {
                handle.Break();
            }
        }).Add(() =>
        {
            Debug.Log("Game Finished");
            ServiceLocator.Instance.GetService<ILoadScene>().Lock();
            switch (game.StateOfGame)
            {
                case 1:
                    _winGameState.Play();
                    break;
                case 2:
                    _loseGameState.Play();
                    break;
            }
        });

        _winGameState = this.tt().Pause().Add(() =>
        {
            Debug.Log("Game Win");
            var totalDamage = damagePlayer * game.LoadToPlayer();
            healthEnemy -= totalDamage;
            presentations.AttackToEnemyAnimation(totalDamage);
        }).Loop(handle =>
        {
            if (presentations.FinishAttackToEnemy())
            {
                handle.Break();
            }
        }).Add(() =>
        {
            _definitionOfGame.Play();
        }); 
        _loseGameState = this.tt().Pause().Add(() =>
        {
            Debug.Log("Game Lose");
            var totalDamage = damageEnemy * game.LoadToEnemy();
            healthPlayer -= totalDamage;
            presentations.AttackToPlayerAnimation(totalDamage);
        }).Loop(handle =>
        {
            if (presentations.FinishAttackToPlayer())
            {
                handle.Break();
            }
        }).Add(() =>
        {
            _definitionOfGame.Play();
        });

        _definitionOfGame = this.tt().Pause().Add(() =>
        {
            if (healthEnemy <= 0)
            {
                ServiceLocator.Instance.GetService<IGlobalInformation>().WinHexagon();
                GoToCityBuilding();
            }

            if (healthPlayer <= 0)
            {
                ServiceLocator.Instance.GetService<IGlobalInformation>().LoseHexagon();
                GoToCityBuilding();
            }

            _preparingGameState.Play();
        });
        
        _presentationEnemyState.Play();
        ServiceLocator.Instance.GetService<ILoadScene>().Open(() => { });
    }

    public void GoToCityBuilding()
    {
        ServiceLocator.Instance.GetService<ILoadScene>().Unlock();
        _presentationEnemyState.Stop();
        _presentationPlayerState.Stop();
        _preparingGameState.Stop();
        ServiceLocator.Instance.GetService<ILoadScene>().Close(() =>
        {
            SceneManager.LoadScene(3);
        });
    }


    private void StartGame()
    {
        game.StartGame();
    }

    private void HideButtons()
    {
        winBtn.gameObject.SetActive(false);
        loseBtn.gameObject.SetActive(false);
    }
}