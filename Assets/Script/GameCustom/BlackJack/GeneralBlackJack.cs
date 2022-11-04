using SystemOfExtras;
using SystemOfExtras.GlobalInformationPath;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GeneralBlackJack : MonoBehaviour, IGeneralBlackJack
{
    [SerializeField] private Presentations presentations;
    [SerializeField] private Button winBtn, loseBtn;
    [SerializeField] private GameLogic game;
    private TeaTime _presentationEnemyState, _presentationPlayerState, _preparingGameState, _gameState, _winGameState, _loseGameState, _definitionOfGame;
    [SerializeField] private int healthEnemy, healthPlayer;
    [SerializeField] private int damageEnemy, damagePlayer;
    [SerializeField] private TextMeshProUGUI healthTextPlayer, attackTextPlayer, healthTextBot;
    [SerializeField] private int sceneCityBuilding;

    private void Start()
    {
        HideButtons();
        game.Configurate(this);
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
            game.BeginGame();
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
            game.StartGame();
            Debug.Log("In Game");
        }).Loop(handle =>
        {
            if (game.StateOfGame > 0)
            {
                handle.Break();
            }
            
            attackTextPlayer.text = $"Attack: {damageEnemy * game.LoadToPlayer()}";
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

            game.GameFinished();
        });

        _winGameState = this.tt().Pause().Add(() =>
        {
            Debug.Log("Game Win");
            var totalDamage = damagePlayer * game.LoadToPlayer();
            healthEnemy -= totalDamage;
            healthTextBot.text = $"Health: {healthEnemy}";
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
            healthTextPlayer.text = $"Health: {healthPlayer}";
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
        attackTextPlayer.text = $"Attack: {damageEnemy * game.LoadToPlayer()}";
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
            SceneManager.LoadScene(sceneCityBuilding);
        });
    }

    private void HideButtons()
    {
        winBtn.gameObject.SetActive(false);
        loseBtn.gameObject.SetActive(false);
    }

    public void ShowMessage(string title, string message)
    {
        //TODO show the message to player
    }
}