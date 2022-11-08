using System;
using System.Collections;
using UnityEngine;

public class BotDeck : DeckForGame
{
    [SerializeField] private int numberOfDontOverBot;
    private IEnumerator placeCard;

    private void Awake()
    {
        placeCard = PlaceCardCoroutine();
    }

    public override void FinishTurn()
    {
    }

    public override void BeginTurn()
    {
        base.BeginTurn();
        StartCoroutine(PlaceCardCoroutine());
        isFinishTurn = false;
    }

    private IEnumerator PlaceCardCoroutine()
    {
        yield return new WaitForSeconds(2);
        //Here is the logic for IA of Enemy
        if (!_gameLogic.IsSetGame() && _gameLogic.TotalNumberInGame() > numberOfDontOverBot)
        {
            SetGame();
            Debug.Log("Set from bot");
        }
        else
        {
            PlaceCard();   
        }
    }

    public override void Restart()
    {
        base.Restart();
        StopCoroutine(placeCard);
    }
}