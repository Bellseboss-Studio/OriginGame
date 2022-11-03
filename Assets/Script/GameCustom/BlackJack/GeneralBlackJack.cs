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
    [SerializeField] private int healthEnemy;
    [SerializeField] private int damageEnemy;
    [SerializeField] private TextMeshProUGUI healthTextPlayer, attackTextPlayer, healthTextBot, damageEnemyText;
    [SerializeField] private int sceneCityBuilding;
    [SerializeField] private int tokensForGame;
    private int _damagePlayer;
    private int _healthPlayer;

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
            
            attackTextPlayer.text = $"Damage: {_damagePlayer * game.LoadToPlayer()}";
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
            var totalDamage = _damagePlayer * game.LoadToPlayer();
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
            _healthPlayer -= totalDamage;
            healthTextPlayer.text = $"Health: {_healthPlayer}";
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
                ServiceLocator.Instance.GetService<IGlobalInformation>().ReceiveToken(tokensForGame);
            }

            if (_healthPlayer <= 0)
            {
                ServiceLocator.Instance.GetService<IGlobalInformation>().LoseHexagon();
                GoToCityBuilding();
                ServiceLocator.Instance.GetService<IGlobalInformation>().ReceiveToken(tokensForGame / 2);
            }

            _preparingGameState.Play();
        });
        
        _presentationEnemyState.Play();
        
        _damagePlayer = ServiceLocator.Instance.GetService<IStatsInformation>().GetDamage();
        _healthPlayer = ServiceLocator.Instance.GetService<IStatsInformation>().GetHealth();
        
        attackTextPlayer.text = $"Damage: {_damagePlayer}";
        healthTextPlayer.text = $"Health: {_healthPlayer}";
        
        healthEnemy = ServiceLocator.Instance.GetService<IStatsInformation>().GetEnemyHealth();
        damageEnemy = ServiceLocator.Instance.GetService<IStatsInformation>().GetEnemyDamage();
        
        damageEnemyText.text = $"Damage: {damageEnemy}";
        healthTextBot.text = $"Health: {healthEnemy}";
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