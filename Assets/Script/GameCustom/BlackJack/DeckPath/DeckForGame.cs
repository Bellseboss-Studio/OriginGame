using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public abstract class DeckForGame : MonoBehaviour, IDeckForGame
{
    [SerializeField] private GameObject deck, originDeck, finishDeck;
    [SerializeField] private GameObject primaryCard, secondaryCard, pointInTheTable;
    [SerializeField] private CardInWord primaryCardInWord, secondaryCardInWord;
    [SerializeField] private CardInWord cardPrefab;
    [SerializeField] private int cartsInHand;
    [SerializeField] private List<Card> deckOfCards;
    [SerializeField] private List<Card> deckFinish;
    [SerializeField] private bool isFinishTurn;
    [SerializeField] private Camera camera;
    protected bool _drawIsFinished;
    private List<CardInWord> cartsToPlace;
    protected IGameLogic _gameLogic;

    private void Start()
    {
        cartsToPlace = new List<CardInWord>();
        deck.transform.position = originDeck.transform.position;
        deckFinish = new List<Card>();
        foreach (var card in deckOfCards)
        {
            for (int i = 0; i < card.repeat; i++)
            {
                deckFinish.Add(card);   
            }
        }
    }

    public void DrawCards()
    {
        Sequence drawCards = DOTween.Sequence();
        drawCards.Append(deck.transform.DOMove(finishDeck.transform.position, .5f))
            .Append(DeliverCard())
            .OnComplete(() =>
            {
                _drawIsFinished = true;
            });
    }

    public virtual void ComeBackCards()
    {
        deck.transform.DOMove(originDeck.transform.position, 1);
    }

    private Tween DeliverCard()
    {
        cartsInHand++;
        switch (cartsInHand)
        {
            case 1:
                secondaryCardInWord = CreateCard();
                secondaryCardInWord.name = "secondary";
                secondaryCardInWord.transform.position = deck.transform.position;
                return secondaryCardInWord.transform.DOMove(secondaryCard.transform.position, 1);
            case 2:
                primaryCardInWord = secondaryCardInWord;
                primaryCardInWord.name = "primary";
                secondaryCardInWord = CreateCard();
                secondaryCardInWord.name = "secondary";
                secondaryCardInWord.transform.position = deck.transform.position;
                secondaryCardInWord.transform.DOMove(secondaryCard.transform.position, 1);
                return primaryCardInWord.transform.DOMove(primaryCard.transform.position, .5f);
        }

        throw new Exception("cards in hand overload");
    }

    private CardInWord CreateCard()
    {
        var cardInWord = Instantiate(cardPrefab, transform);
        cardInWord.Configurate(deckFinish[Random.Range(0, deckFinish.Count)]);
        return cardInWord;
    }

    public virtual void PlaceCard()
    {
        cartsInHand--;
        primaryCardInWord.transform.DOMove(pointInTheTable.transform.position, .5f);
        primaryCardInWord.name = "outOfContext";
        _gameLogic.Sum(primaryCardInWord.Card.number);
        cartsToPlace.Add(primaryCardInWord);
        _gameLogic.EvaluateCard(primaryCardInWord);
        primaryCardInWord = null;
        _gameLogic.AddLoad();
        isFinishTurn = true;
    }

    public virtual void PassTurn()
    {
        if (_gameLogic.IsSetGame())
        {
            Debug.Log("Don't pass");
            _gameLogic.DontPassThisTurn();
            return;
        }
        Debug.Log("Do pass");
        isFinishTurn = true;
    }

    public virtual void SetGame()
    {
        if (_gameLogic.IsSetGame()) return;
        PlaceCard();
        _gameLogic.AddLoad();
        _gameLogic.SetGame();
    }

    public bool DrawIsFinished => _drawIsFinished;

    public virtual void BeginTurn()
    {
        TakeCard();
        isFinishTurn = false;
    }

    private void TakeCard()
    {
        if (cartsInHand >= 2) return;
        cartsInHand++;
        primaryCardInWord = secondaryCardInWord;
        primaryCardInWord.name = "primary";
        primaryCardInWord.EnabledDragComponent(camera, this);
        secondaryCardInWord = CreateCard();
        secondaryCardInWord.name = "secondary";
        secondaryCardInWord.transform.position = deck.transform.position;
        secondaryCardInWord.transform.DOMove(secondaryCard.transform.position, 1);
        primaryCardInWord.transform.DOMove(primaryCard.transform.position, .5f);
    }

    
    public virtual bool IsFinishTurn()
    {
        return isFinishTurn;
    }

    public abstract void FinishTurn();

    public void Configure(IGameLogic gameLogic)
    {
        _gameLogic = gameLogic;
    }

    public virtual void BeginGame()
    {
        _drawIsFinished = false;
        cartsInHand = 0;
        foreach (var cardInWord in cartsToPlace)
        {
            Destroy(cardInWord.gameObject);
        }
        cartsToPlace = new List<CardInWord>();
        
        if (secondaryCardInWord != null)
        {
            Destroy(secondaryCardInWord.gameObject);
        }
        if (primaryCardInWord != null)
        {
            Destroy(primaryCardInWord.gameObject);
        }
        isFinishTurn = true;
    }

    public virtual void Restart()
    {
        
    }
}