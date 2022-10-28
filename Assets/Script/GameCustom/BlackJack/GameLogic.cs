using SystemOfExtras;
using TMPro;
using UnityEngine;

public class GameLogic : MonoBehaviour, IGameLogic
{
    [SerializeField] private DeckForGame playerDeck, botDeck;
    [SerializeField] private int maxNumberInGame;
    [SerializeField] private TextMeshProUGUI totalNumber, loadsToPlayer, loadsToEnemy;
    private int _stateOfGame;//0: inGame; 1: Win; 2: Lose
    private int _loadPlayer, _loadBot, _totalNumber;
    private bool isTurnOfPlayer;

    private TeaTime _turnOfPlayer, _turnOfBot, _intermediateTurn;
    private bool _setGame;
    private IGeneralBlackJack _generalBlackJack;

    private void Start()
    {
        _turnOfBot = this.tt().Pause().Add(() =>
        {
            botDeck.BeginTurn();
        }).Loop(handle =>
        {
            if (botDeck.IsFinishTurn())
            {
                handle.Break();
            }
        }).Add(() =>
        {
            isTurnOfPlayer = true;
            botDeck.FinishTurn();
            _intermediateTurn.Play();
        });
        _turnOfPlayer = this.tt().Pause().Add(() =>
        {
            playerDeck.BeginTurn();
        }).Loop(handle =>
        {
            if (playerDeck.IsFinishTurn())
            {
                handle.Break();
            }
        }).Add(() =>
        {
            isTurnOfPlayer = false;
            playerDeck.FinishTurn();
            _intermediateTurn.Play();
        });
        
        _intermediateTurn = this.tt().Pause().Add(() =>
        {
            if (isTurnOfPlayer)
            {
                _turnOfPlayer.Play();
            }
            else
            {
                _turnOfBot.Play();
            }
        });

        playerDeck.Configure(this);
        botDeck.Configure(this);
    }

    public int LoadPlayer => _loadPlayer;
    public int LoadEnemy => _loadBot;
    public int StateOfGame => _stateOfGame;

    public void DrawCards()
    {
        botDeck.DrawCards();
        playerDeck.DrawCards();
    }

    public bool DrawCardsIsFinished()
    {
        return playerDeck.DrawIsFinished && botDeck.DrawIsFinished;
    }

    public void StartGame()
    {
        ServiceLocator.Instance.GetService<ILoadScene>().Unlock();
        _stateOfGame = 0;
        isTurnOfPlayer = false;
        _intermediateTurn.Play();
    }
    
    public void Win()
    {
        _stateOfGame = 1;
    }
    public void Lose()
    {
        _stateOfGame = 2;
    }

    public void Sum(int cardNumber)
    {
        _totalNumber += cardNumber;
        totalNumber.text = $"Total: {_totalNumber}";
        if (maxNumberInGame == _totalNumber)
        {
            EvaluateToTotalAdded();   
        }
        if (_totalNumber > maxNumberInGame)
        {
            if (isTurnOfPlayer)
            {
                Lose();
            }
            else
            {
                Win();
            }
        }
    }

    private void EvaluateToTotalAdded()
    {
        if (isTurnOfPlayer)
        {
            //Treinta y dos to player
            ShowAnimationToPlayer32();
            AddLoad();
        }
        else
        {
            //Treinta y dos to bot
            ShowAnimationToBot32();
            AddLoad();
        }
    }

    private void ShowAnimationToBot32()
    {
        //TODO animation for show 32 points for bot
        Debug.Log("Is a 32 from Bot!");
    }

    private void ShowAnimationToPlayer32()
    {
        //TODO animation for show 32 points for player
        Debug.Log("Is a 32 from Player!");
    }

    public void AddLoad()
    {
        if (isTurnOfPlayer)
        {
            _loadPlayer++;
            loadsToPlayer.text = $"X {_loadPlayer}";
        }
        else
        {
            _loadBot++;
            loadsToEnemy.text = $"X {_loadBot}";
        }
    }

    public void SetGame()
    {
        _setGame = true;
        //TODO animation to show the set game
    }

    public bool IsSetGame()
    {
        return _setGame;
    }

    public void DontPassThisTurn()
    {
        _generalBlackJack.ShowMessage("Dont pass this time", "the game is SET");
    }

    public void EvaluateCard(CardInWord card)
    {
        if (card.Card.number == 0)
        {
            AddLoad();
            ShowComodinEvent();
        }
    }

    public int TotalNumberInGame()
    {
        return _totalNumber;
    }

    private void ShowComodinEvent()
    {
        //TODO animation for  show the card is a joke
        Debug.Log("Is a JOKE!");
    }

    public int LoadToPlayer()
    {
        return _loadPlayer;
    }

    public int LoadToEnemy()
    {
        return _loadBot;
    }

    public void BeginGame()
    {
        _stateOfGame = 0;
        _loadPlayer = 0;
        _loadBot = 0;
        _totalNumber = 0;
        isTurnOfPlayer = false;
        _setGame = false;       
        botDeck.BeginGame();
        playerDeck.BeginGame();
    }

    public void Configurate(IGeneralBlackJack generalBlackJack)
    {
        _generalBlackJack = generalBlackJack;
    }

    public void GameFinished()
    {
        //botDeck.Restart();
    }
}