public interface IGameLogic
{
    void Sum(int cardNumber);
    void AddLoad();
    void SetGame();
    bool IsSetGame();
    void DontPassThisTurn();
    void EvaluateCard(CardInWord card);
    int TotalNumberInGame();
}