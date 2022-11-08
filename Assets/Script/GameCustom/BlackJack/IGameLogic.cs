using System;
using UnityEngine;

public interface IGameLogic
{
    void Sum(int cardNumber);
    void AddLoad();
    void SetGame();
    bool IsSetGame();
    void DontPassThisTurn();
    void EvaluateCard(CardInWord card);
    int TotalNumberInGame();
    Vector2 GetPosition();
    Action OnBeggingOfTurn { get; set; }
    Action OnFinishingOfTurn { get; set; }
}