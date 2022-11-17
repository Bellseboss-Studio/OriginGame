public interface IGeneralBlackJack
{
    void ShowMessage(string title, string message);
    bool MessageHasBeenDelivered();
    void AddLoadPlayer();
    void AddLoadEnemy();
    void PlayerSetGame();
    void EnemySetGame();
}