using System.Collections;
using UnityEngine;

public class BotDeck : DeckForGame
{
    [SerializeField] private bool isFinishTurn;
    
    public override bool IsFinishTurn()
    {
        return isFinishTurn;
    }

    public override void PlaceCard()
    {
        base.PlaceCard();
        isFinishTurn = true;
    }

    public override void FinishTurn()
    {
    }

    public override void BeginTurn()
    {
        base.BeginTurn();
        isFinishTurn = false;
        StartCoroutine(PlaceCardCoroutine());
    }

    private IEnumerator PlaceCardCoroutine()
    {
        yield return new WaitForSeconds(2);
        PlaceCard();
    }
}