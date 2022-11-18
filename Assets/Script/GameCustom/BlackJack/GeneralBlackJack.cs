using System;
using SystemOfExtras;
using SystemOfExtras.GlobalInformationPath;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GeneralBlackJack : MonoBehaviour, IGeneralBlackJack
{
    [SerializeField] private Presentations presentations;
    [SerializeField] private Button skipTutorial;
    [SerializeField] private GameLogic game;
    private TeaTime _presentationEnemyState, _presentationPlayerState, _preparingGameState, _gameState, _winGameState, _loseGameState, _definitionOfGame;
    private TeaTime _tutorial, _skipTutorial;
    [SerializeField] private int healthEnemy;
    [SerializeField] private int damageEnemy;
    [SerializeField] private TextMeshProUGUI healthTextPlayer, attackTextPlayer, healthTextBot, damageEnemyText;
    [SerializeField] private int sceneCityBuilding;
    [SerializeField] private int tokensForGame;
    [SerializeField] private NewGamePlayBlackJack gameplay;
    [SerializeField] private Camera cameraInBattle;
    [SerializeField] private Renderer table;
    [SerializeField] private ShowMessageTransitionally message;
    private int _damagePlayer;
    private int _healthPlayer;
    private bool _skipTutorialResult;

    private void Start()
    {
        skipTutorial.gameObject.SetActive(true);
        skipTutorial.onClick.AddListener(() =>
        {
            _skipTutorialResult = true;
        });
        HideButtons();
        game.Configurate(this);
        ServiceLocator.Instance.GetService<ILoadScene>().Lock();
        gameplay.ResetLoadsEnemy();
        gameplay.ResetLoadsPlayer();
        SetMaterialInTable();

        _tutorial = this.tt().Pause().Add(() =>
        {
            presentations.StartPresentationEnemy();
        }).Loop(handler =>
        {
            if (presentations.FinishEnemyPresentation || _skipTutorialResult)
            {
                handler.Break();
            }
        }).Add(() =>
        {
            if (_skipTutorialResult)
            {
                StopAllTutorial();
                return;
            }
            presentations.StartPresentationPlayer();
        }).Loop(handler =>
        {
            if (presentations.FinishPlayerPresentation || _skipTutorialResult)
            {
                handler.Break();
            }
        }).Add(() =>
        {
            if (_skipTutorialResult)
            {
                StopAllTutorial();
                return;
            }
            presentations.StartPreparingGame();
        }).Loop(handle =>
        {
            if (presentations.FinishPresentation || _skipTutorialResult)
            {
                handle.Break();
            }
        }).Add(() =>
        {
            presentations.StopAllPresentations();
            _preparingGameState.Play();
            cameraInBattle.gameObject.SetActive(true);
        });

        _preparingGameState = this.tt().Pause().Add(() =>
        {
            skipTutorial.gameObject.SetActive(false);
            gameplay.ResetLoadsEnemy();
            gameplay.ResetLoadsPlayer();
            game.BeginGame();
            game.DrawCards();
            game.DisableSetPower();
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
            gameplay.UpdateHpEnemy(healthEnemy);
            gameplay.UpdateHpPlayer(_healthPlayer);
            if (healthEnemy <= 0)
            {
                ServiceLocator.Instance.GetService<IGlobalInformation>().WinHexagon();
                ServiceLocator.Instance.GetService<ILoadScene>().ShowMessageWithOneButton("You Win",
                    $"Win {tokensForGame} for play the roulette", "Go to CityBuilding",
                    GoToCityBuilding, GoToCityBuilding);
                ServiceLocator.Instance.GetService<IGlobalInformation>().ReceiveToken(tokensForGame);
            }

            if (_healthPlayer <= 0)
            {
                ServiceLocator.Instance.GetService<IGlobalInformation>().LoseHexagon();
                ServiceLocator.Instance.GetService<ILoadScene>().ShowMessageWithOneButton("You Lose",
                    $"Win {tokensForGame} for play the roulette", "Go to CityBuilding",
                    GoToCityBuilding, GoToCityBuilding);
                ServiceLocator.Instance.GetService<IGlobalInformation>().ReceiveToken(tokensForGame / 2);
            }

            _preparingGameState.Play();
        });
        
        _tutorial.Play();
        
        _damagePlayer = ServiceLocator.Instance.GetService<IStatsInformation>().GetDamage();
        _healthPlayer = ServiceLocator.Instance.GetService<IStatsInformation>().GetHealth();
        
        attackTextPlayer.text = $"Damage: {_damagePlayer}";
        healthTextPlayer.text = $"Health: {_healthPlayer}";
        
        healthEnemy = ServiceLocator.Instance.GetService<IStatsInformation>().GetEnemyHealth();
        damageEnemy = ServiceLocator.Instance.GetService<IStatsInformation>().GetEnemyDamage();
        
        damageEnemyText.text = $"Damage: {damageEnemy}";
        healthTextBot.text = $"Health: {healthEnemy}";
        gameplay.Configure(_healthPlayer, healthEnemy);
        cameraInBattle.gameObject.SetActive(false);
        ServiceLocator.Instance.GetService<ILoadScene>().Open(() => { });
    }

    private void StopAllTutorial()
    {
        _tutorial.Stop();
        _preparingGameState.Play();
        presentations.StopAllPresentations();
        message.CloseWindows();
    }

    private void SetMaterialInTable()
    {
        table.materials[1].SetFloat("Is_Rocoso",0);
        table.materials[1].SetFloat("Is_Arido",0);
        table.materials[1].SetFloat("Is_Hierva",0);
        try
        {
            var hexagonInBet = ServiceLocator.Instance.GetService<IGlobalInformation>().GetHexagonInBet();
            switch (hexagonInBet.TypeOfHexa())
            {
                case 1:
                    table.materials[1].SetFloat("Is_Rocoso",1);
                    break;
                case 2:
                    table.materials[1].SetFloat("Is_Arido",1);
                    break;
                case 3:
                    table.materials[1].SetFloat("Is_Hierva",1);
                    break;
            }
        }
        catch (Exception e)
        {
            // ignored
        }
    }

    public void GoToCityBuilding()
    {
        ServiceLocator.Instance.GetService<ILoadScene>().Unlock();
        _preparingGameState.Stop();
        ServiceLocator.Instance.GetService<ILoadScene>().Close(() =>
        {
            SceneManager.LoadScene(sceneCityBuilding);
        });
    }

    private void HideButtons()
    {
    }

    public void ShowMessage(string title, string message)
    {
        this.message.ShowMessage(title, message);
    }

    public bool MessageHasBeenDelivered()
    {
        return message.IsFinishPresentation;
    }

    public void AddLoadPlayer()
    {
        gameplay.AddLoadPlayer();
    }

    public void AddLoadEnemy()
    {
        gameplay.AddLoadEnemy();
    }

    public void PlayerSetGame()
    {
        gameplay.SetFromPlayer();
    }

    public void EnemySetGame()
    {
        gameplay.SetFromEnemy();
    }
}