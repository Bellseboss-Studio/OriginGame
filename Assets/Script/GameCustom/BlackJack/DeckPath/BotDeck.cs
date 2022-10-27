using System;
using System.Collections;
using UnityEngine;

public class BotDeck : DeckForGame
{
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
    }

    private IEnumerator PlaceCardCoroutine()
    {
        yield return new WaitForSeconds(2);
        //Here is the logic for IA of Enemy
        PlaceCard();
    }

    public override void Restart()
    {
        base.Restart();
        StopCoroutine(placeCard);
    }
}