using UnityEngine;

public class PlayerDeck : DeckForGame
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

    public override void BeginTurn()
    {
        base.BeginTurn();
        isFinishTurn = false;
    }

    public override void FinishTurn()
    {
        
    }
}